using IMS.CoreBussiness;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsByNameAsync(string name);
        Task AddProductAsync(Product product);
        Task<Product?> GetProductByIdAsync(int productId);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId);
    }
}
