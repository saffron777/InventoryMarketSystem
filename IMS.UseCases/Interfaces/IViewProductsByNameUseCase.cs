using IMS.CoreBussiness;

namespace IMS.UseCases
{
    public interface IViewProductsByNameUseCase
    {
        Task<List<Product>> ExecuteAsync(string name = "");
    }
}