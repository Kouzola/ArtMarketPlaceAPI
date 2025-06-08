using Business_Layer.Exceptions;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Cart;
using Domain_Layer.Interfaces.Customization;
using Domain_Layer.Interfaces.Product;
using Domain_Layer.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class CartService(ICartRepository repository, IProductService productService, ICustomizationService customizationService) : ICartService
    {
        private readonly ICartRepository _repository = repository;
        private readonly IProductService _productService = productService;
        private readonly ICustomizationService _customizationService = customizationService;

        public async Task<Cart> AddCartAsync(Cart cart)
        {
            return await _repository.AddCartAsync(cart);
        }

        public async Task<Cart> AddItemToCartAsync(int customerId, int productId, int quantity, int customizationId)
        {
            var cart = await _repository.GetCartByUserIdAsync(customerId);
            if (cart == null) cart = await AddCartAsync(new Cart
            {
                UserId = customerId,
            });
            List<CartItem> items = cart.Items;
            //Vérification de l'existence des produits ajouter
            var product = await _productService.GetProductByIdAsync(productId);
            if (quantity > product.Stock) throw new BusinessException("Not enough stock!");
            if (customizationId != 0)
            {
                var customization = await _customizationService.GetCustomizationByIdAsync(customizationId);
                if (customization == null) throw new BusinessException("Customization does not exist");
            }

            //On check si on a pas déja le même item dans le cart pour augmenter sa quantité
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId && i.CustomizationId == (customizationId == 0 ? null : customizationId));
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                items.Add(new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity,
                    CustomizationId = (customizationId == 0 ? null : customizationId)
                });
            }       
            var updatedCart = await _repository.UpdateCartAsync(cart);
            if (updatedCart == null) throw new NotFoundException("Cart not found!");
            return updatedCart;
        }

        public async Task<bool> DeleteCartAsync(int id)
        {
            return await _repository.DeleteCartAsync(id);
        }

        public async Task<Cart> GetCartByIdAsync(int id)
        {
            var cart = await _repository.GetCartByIdAsync(id);
            if (cart == null) throw new NotFoundException("Cart not found!");
            return cart;
        }

        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            var cart = await _repository.GetCartByUserIdAsync(userId);
            if (cart == null) throw new NotFoundException("Cart not found!");
            return cart;
        }

        public async Task<Cart> RemoveItemFromCartAsync(int cartId, int productId, int quantity)
        {
            var cart = await _repository.GetCartByIdAsync(cartId);
            if (cart == null) throw new NotFoundException("Cart not found!");
            List<CartItem> items = cart.Items;
            var itemToUpdate = items.FirstOrDefault(item => item.ProductId == productId);
            if (itemToUpdate == null) throw new NotFoundException("Product not found in the cart!");
            if (itemToUpdate.Quantity == quantity) items.Remove(itemToUpdate);
            else itemToUpdate.Quantity -= quantity;
            var updatedCart = await _repository.UpdateCartAsync(cart);
            if (updatedCart == null) throw new NotFoundException("Cart not found!");
            if(updatedCart.Items.Count == 0) await DeleteCartAsync(cartId);
            return updatedCart;
        }
    }
}
