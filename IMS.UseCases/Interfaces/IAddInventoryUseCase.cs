using IMS.CoreBussiness;

namespace IMS.UseCases
{
    public interface IAddInventoryUseCase
    {
        Task ExecuteAsync(Inventory inventory);
    }
}