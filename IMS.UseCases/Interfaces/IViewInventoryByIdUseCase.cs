using IMS.CoreBussiness;

namespace IMS.UseCases
{
    public interface IViewInventoryByIdUseCase
    {
        Task<Inventory?> ExecuteAsync(int inventoryId);
    }
}