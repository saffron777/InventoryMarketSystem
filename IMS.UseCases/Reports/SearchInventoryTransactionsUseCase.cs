using IMS.CoreBussiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.Reports
{
    public class SearchInventoryTransactionsUseCase : ISearchInventoryTransactionsUseCase
    {
        private readonly IInventoryTransactionRepository inventoryTransaction;

        public SearchInventoryTransactionsUseCase(IInventoryTransactionRepository inventoryTransaction)
        {
            this.inventoryTransaction = inventoryTransaction;
        }
        public async Task<IEnumerable<InventoryTransaction>> ExecuteAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType)
        {
            return await this.inventoryTransaction.GetInventoryTransactionsAsync(inventoryName, dateFrom, dateTo, transactionType);
        }
    }
}
