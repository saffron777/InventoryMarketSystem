using IMS.CoreBussiness;

namespace IMS.UseCases
{
    public interface IEditProductUseCase
    {
        Task Execute(Product product);
    }
}