using IMS.CoreBussiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.EFCore
{
    public class ProductTransactionRepository : IProductTransactionRepository
    {
        private readonly IMSContext db;
        private readonly IProductRepository productRepository;

        public ProductTransactionRepository(IMSContext db, IProductRepository productRepository)
        {
            this.db = db;
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductTransaction>> GetProductTransactionAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType)
        {
           if(dateTo.HasValue)  dateTo = dateTo.Value.AddDays(1);
            var query = from pt in db.ProductTransactions
                        join prod in db.Products on pt.ProductId equals prod.ProducId
                        where
                        (string.IsNullOrWhiteSpace(productName) || prod.ProductName.ToLower() == productName.ToLower() ) &&
                        (!dateFrom.HasValue || pt.TransactionDate.Date >= dateFrom.Value.Date) &&
                        (!dateTo.HasValue || pt.TransactionDate.Date <= dateTo.Value.Date) &&
                        (!transactionType.HasValue || pt.ActivityType == transactionType)
                        select pt;

            return await query.Include(x=> x.Product).ToListAsync();
        }

        public async Task ProduceAsync(string productionNumber, Product product, int quantity, double price, string doneBy)
        {
            //var prod = await db.Products
            //    .Include(x => x.ProductInventories)
            //    .ThenInclude(x => x.Inventory)
            //    .FirstOrDefaultAsync(x => x.ProducId == product.ProducId);

            var prod = await this.productRepository.GetProductByIdAsync(product.ProducId);

            if (prod != null)
            {
                foreach (var pi in prod.ProductInventories)
                {
                    int qtyBefore = pi.Inventory.Quantity;
                    pi.Inventory.Quantity -= quantity * pi.InventoryQuantity;

                    this.db.InventoryTransactions.Add(new InventoryTransaction
                    {
                        ProductionNumber = productionNumber,                        
                        QuantityBefore = qtyBefore,
                        QuantityAfter = pi.Inventory.Quantity ,
                        ActivityType = InventoryTransactionType.ProduceProduct,
                        TransactionDate = DateTime.Now,
                        DoneBy = doneBy,
                        UnitPrice = price * quantity,

                    });
                }
            }

            

            this.db.ProductTransactions.Add(new ProductTransaction
            {
                ProductionNumber = productionNumber,
                ProductId = product.ProducId,
                QuantityBefore = product.Quantity,
                ActivityType = ProductTransactionType.ProduceProduct,
                QuantityAfter = product.Quantity + quantity,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                UnitPrice = price

            });

            await db.SaveChangesAsync();
        }

        public async Task SellProductAsync(string saleOrderNumber, Product product, int quantity, double price, string doneBy)
        {
            this.db.ProductTransactions.Add(new ProductTransaction
            {
                SalesOrderNumber = saleOrderNumber,
                ProductId = product.ProducId,
                QuantityBefore = product.Quantity,
                QuantityAfter = product.Quantity - quantity,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                UnitPrice = price,
                 ActivityType = ProductTransactionType.SellProduct,
            });

            await this.db.SaveChangesAsync();
        }
    }
}
