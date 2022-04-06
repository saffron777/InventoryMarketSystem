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
    public class InventoryTransactionRepository : IInventoryTransactionRepository
    {
        private readonly IMSContext db;

        public InventoryTransactionRepository(IMSContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<InventoryTransaction>> GetInventoryTransactionsAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType)
        {
            if (dateTo.HasValue) dateTo = dateTo.Value.AddDays(1);

            var query = from it in db.InventoryTransactions
                        join inv in db.Inventories on it.InventoryId equals inv.InventoryId
                        where 
                        (string.IsNullOrWhiteSpace(inventoryName) || inv.InventoryName.ToLower() == inventoryName.ToLower()) && 
                        (!dateFrom.HasValue || it.TransactionDate.Date >= dateFrom.Value.Date) &&
                        (!dateTo.HasValue || it.TransactionDate.Date <= dateTo.Value.Date) &&
                        (!transactionType.HasValue || it.ActivityType == transactionType)
                        select it;

            return await query.Include(x => x.Inventory).ToListAsync();
        }

        public async Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, double price, string doneBy)
        {
            this.db.InventoryTransactions.Add(new InventoryTransaction
            {
                InventoryId = inventory.InventoryId,
                PONumber = poNumber,
                QuantityBefore = inventory.Quantity,
                QuantityAfter = inventory.Quantity + quantity,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                 UnitPrice = price * quantity,

            });

            await this.db.SaveChangesAsync();
        }
    }
}
