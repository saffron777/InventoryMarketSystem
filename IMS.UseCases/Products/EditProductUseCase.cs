using IMS.CoreBussiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class EditProductUseCase : IEditProductUseCase
    {
        private readonly IProductRepository productrepository;

        public EditProductUseCase(IProductRepository productrepository)
        {
            this.productrepository = productrepository;
        }
        public async Task Execute(Product product)
        {
            await this.productrepository.UpdateProductAsync(product);
        }
    }
}
