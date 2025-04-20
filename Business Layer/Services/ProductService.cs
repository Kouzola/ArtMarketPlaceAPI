using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class ProductService(IProductRepository repository) : IProductService
    {
        private readonly IProductRepository _repository = repository;
        //TODO : Générer automatique la référence 
        //TODO : Image peut pas être la même donc vérifier ca ici
        //TODO : Pas oublier les notfound exception et tout
        public Task<Product> AddProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductsAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<Product> DesactivateProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByReferenceAsync(string reference)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsByArtisanAsync(int artisanId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
