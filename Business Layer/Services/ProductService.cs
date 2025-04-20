using Business_Layer.Exceptions;
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

        public async Task<Product> AddProductAsync(Product product)
        {
            var artisanIdentifier = product.Artisan.UserName.Substring(0, 2).ToUpper() + product.Artisan.FirstName.Substring(0,1).ToUpper() + product.Artisan.LastName.Substring(0,1).ToUpper();
            var ProductsCountOfArtisan = (await GetProductsByArtisanAsync(product.ArtisanId)).Count();
            var uniqueId = Guid.NewGuid().ToString("N").Substring(0,6).ToUpper();
            var productReference = artisanIdentifier + "-" + ProductsCountOfArtisan + "-" + uniqueId;
            product.Reference = productReference;
            return await _repository.AddProductAsync(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _repository.DeleteProductAsync(id);
        }

        public async Task<bool> DeleteProductsAsync(List<int> ids)
        {
            return await _repository.DeleteProductsAsync(ids);
        }

        public async Task<Product> ToggleProductAvailability(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null) throw new NotFoundException("Product not found!");
            if(product.Available) product.Available = false;
            else product.Available = true;
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null) throw new NotFoundException("Product not found!");
            return product;
        }

        public async Task<Product> GetProductByReferenceAsync(string reference)
        {
            var product = await _repository.GetProductByReferenceAsync(reference);
            if (product == null) throw new NotFoundException("Product not found!");
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsByArtisanAsync(int artisanId)
        {
            return await _repository.GetProductsByArtisanAsync(artisanId);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _repository.GetProductsByCategoryAsync(categoryId);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var updatedProduct = await _repository.UpdateProductAsync(product);
            if (updatedProduct == null) throw new NotFoundException("Product not found!");
            return updatedProduct;
        }
    }
}
