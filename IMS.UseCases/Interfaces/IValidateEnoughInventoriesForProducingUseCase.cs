using IMS.CoreBussiness;

namespace IMS.UseCases
{
    public interface IValidateEnoughInventoriesForProducingUseCase
    {
        Task<bool> ExecuteAsync(Product product, int quantity);
    }
}