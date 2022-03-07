using IMS.CoreBussiness;

namespace IMS.UseCases
{
    public interface IAddProductUseCase
    {
        Task ExecuteAsync(Product product);
    }
}