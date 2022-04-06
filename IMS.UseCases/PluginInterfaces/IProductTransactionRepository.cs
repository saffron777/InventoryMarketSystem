using IMS.CoreBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IProductTransactionRepository
    {
        Task ProduceAsync(string productionNumber, Product product, int quantity, double price, string doneBy);
        Task SellProductAsync(string saleOrderNumber, Product product, int quantity, double price, string doneBy);
        Task<IEnumerable<ProductTransaction>> GetProductTransactionAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactiontype);
    }
}
