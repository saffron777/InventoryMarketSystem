using IMS.CoreBussiness;

namespace IMS.UseCases
{
    public interface IEditInventoryUseCase
    {
        Task ExecuteAsync(Inventory inventory);
    }
}