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
    public class ProductRepository: IProductRepository
    {
        private readonly IMSContext db;

        public ProductRepository(IMSContext db)
        {
            this.db = db;
        }

        public async Task AddProductAsync(Product product)
        {
            if (db.Products.Any(x => x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();

            //var prods = db.Products.Include(x => x.ProductInventories).ThenInclude(x => x.Inventory).ToList();


        }

        public async Task DeleteProductAsync(int productId)
        {
            var prod = await db.Products.FindAsync(productId);

            if (prod == null) return;

            prod.IsActive = false;
            db.Products.Update(prod);
            await db.SaveChangesAsync();

        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await db.Products
                .Include(x=> x.ProductInventories)
                .ThenInclude(x=> x.Inventory)
                .FirstOrDefaultAsync(x=> x.ProducId == productId);
        }

        public async Task<List<Product>> GetProductsByNameAsync(string name)
        {
            return await db.Products.Where(x => (x.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(name)) && x.IsActive==true).ToListAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            if( db.Products.Any(x => x.ProductName.Contains(product.ProductName, StringComparison.OrdinalIgnoreCase))) return;


            var prod = await db.Products.FindAsync(product.ProducId);

            if (prod == null) return;

            prod.ProductName = product.ProductName;
            prod.ProductInventories = product.ProductInventories;
            prod.Price = product.Price;
            prod.Quantity = product.Quantity;
            prod.ProductInventories = prod.ProductInventories;

            db.Products.Update(prod);
            await db.SaveChangesAsync(true);      
        }
    }
}
