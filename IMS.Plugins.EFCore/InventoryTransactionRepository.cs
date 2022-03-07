using IMS.CoreBussiness;
using IMS.UseCases.PluginInterfaces;
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
