using IMS.CoreBussiness;

namespace IMS.UseCases
{
    public interface IViewProductByIdUseCase
    {
        Task<Product> ExecuteAsync(int productId);
    }
}