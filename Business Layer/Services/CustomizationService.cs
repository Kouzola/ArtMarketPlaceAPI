using Business_Layer.Exceptions;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Customization;
using Domain_Layer.Interfaces.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class CustomizationService(ICustomizationRepository repository, IProductService productService) : ICustomizationService
    {
        private readonly ICustomizationRepository _repository = repository;
        private readonly IProductService _productService = productService;

        public async Task<Customization> AddCustomizationAsync(Customization customization)
        {
            await _productService.GetProductByIdAsync(customization.ProductId);//Check si produit existe
            return await _repository.AddCustomizationAsync(customization);
        }

        public async Task<bool> DeleteCustomizationAsync(int id)
        {
            return await _repository.DeleteCustomizationAsync(id);
        }

        public async Task<IEnumerable<Customization>> GetAllCustomizationAsync()
        {
            return await _repository.GetAllCustomizationAsync();
        }

        public async Task<IEnumerable<Customization>> GetAllCustomizationForAProductAsync(int productId)
        {
            await _productService.GetProductByIdAsync(productId);
            return await _repository.GetAllCustomizationForAProductAsync(productId);
        }

        public async Task<Customization> GetCustomizationByIdAsync(int id)
        {
            var customization = await _repository.GetCustomizationByIdAsync(id);
            if (customization == null) throw new NotFoundException("Customization not found!");
            return customization;
        }

        public async Task<Customization> UpdateCustomizationAsync(Customization customization)
        {
            await _productService.GetProductByIdAsync(customization.ProductId);
            var updatedCustomization = await _repository.UpdateCustomizationAsync(customization);
            if (updatedCustomization == null) throw new NotFoundException("Customization not found!");
            return updatedCustomization;
        }
    }
}
