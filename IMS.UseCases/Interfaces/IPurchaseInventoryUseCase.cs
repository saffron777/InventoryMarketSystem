using IMS.CoreBussiness;

namespace IMS.UseCases
{
    public interface IPurchaseInventoryUseCase
    {
        Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, string dondeBy);
    }
}