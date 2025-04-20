using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Interfaces.Product
{
    public interface IProductService
    {
        Task<IEnumerable<Entities.Product>> GetAllProductsAsync();
        Task<IEnumerable<Entities.Product>> GetProductsByArtisanAsync(int artisanId);
        Task<IEnumerable<Entities.Product>> GetProductsByCategoryAsync(int categoryId);
        Task<Entities.Product> GetProductByIdAsync(int id);
        Task<Entities.Product> GetProductByReferenceAsync(string reference);
        Task<Entities.Product> AddProductAsync(Entities.Product product);
        Task<Entities.Product> UpdateProductAsync(Entities.Product product);
        Task<Entities.Product> ToggleProductAvailability(int id);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> DeleteProductsAsync(List<int> ids);
    }
}
