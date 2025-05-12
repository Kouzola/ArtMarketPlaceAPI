using Business_Layer.Exceptions;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Category;
using Domain_Layer.Interfaces.Product;
using Domain_Layer.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class ProductService(IProductRepository repository, ICategoryService categoryService, IUserService userService) : IProductService
    {
        private readonly IProductRepository _repository = repository;
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IUserService _userService = userService;
        public async Task<Product> AddProductAsync(Product product)
        {
            var artisan = await _userService.GetUserByIdAsync(product.ArtisanId);
            if (artisan == null || artisan.Role != Role.Artisan) throw new NotFoundException("Artisan not found");
            await _categoryService.GetCategoryByIdAsync(product.CategoryId); // Check si category existe
            var artisanIdentifier = artisan.UserName.Substring(0, 2).ToUpper() + artisan.FirstName.Substring(0,1).ToUpper() + artisan.LastName.Substring(0,1).ToUpper();
            var ProductsCountOfArtisan = (await GetProductsByArtisanAsync(product.ArtisanId)).Count();
            var uniqueId = Guid.NewGuid().ToString("N").Substring(0,6).ToUpper();
            var productReference = artisanIdentifier + "-" + ProductsCountOfArtisan + "-" + uniqueId;
            product.Reference = productReference;
            return await _repository.AddProductAsync(product);
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            return await _repository.DeleteProductAsync(product);
        }

        public async Task<bool> DeleteProductsAsync(List<Product> products)
        {
            return await _repository.DeleteProductsAsync(products);
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
            var artisan = await _userService.GetUserByIdAsync(artisanId);
            if(artisan == null) throw new NotFoundException("Artisan not found");
            return await _repository.GetProductsByArtisanAsync(artisanId);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            if (category == null) throw new NotFoundException("Category not found");
            return await _repository.GetProductsByCategoryAsync(categoryId);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var updatedProduct = await _repository.UpdateProductAsync(product);
            return updatedProduct!;
        }
    }
}
