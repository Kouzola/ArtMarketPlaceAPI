using Business_Layer.Exceptions;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Cart;
using Domain_Layer.Interfaces.Product;
using Domain_Layer.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class CartService(ICartRepository repository, IProductService productService, IUserService userService) : ICartService
    {
        private readonly ICartRepository _repository = repository;
        private readonly IProductService _productService = productService;
        private readonly IUserService _userService = userService;

        public async Task<Cart> AddCartAsync(Cart cart)
        {
            return await _repository.AddCartAsync(cart);
        }

        public async Task<Cart?> AddItemToCartAsync(int cartId, int productId, int quantity)
        {
            var cart = await _repository.GetCartByIdAsync(cartId);
            if (cart == null) throw new NotFoundException("Cart not found!");
            List<CartItem> items = cart.Products;
            //Vérification de l'existence des produits ajouter
            var product = await _productService.GetProductByIdAsync(productId);
            items.Add(new CartItem
            {
                CartId = cartId,
                ProductId = productId,
                Quantity = quantity
            });
            return await _repository.UpdateCartAsync(cart);        
        }

        public async Task<bool> DeleteCartAsync(int id)
        {
            return await _repository.DeleteCartAsync(id);
        }

        public async Task<Cart?> GetCartByIdAsync(int id)
        {
            var cart = await _repository.GetCartByIdAsync(id);
            if (cart == null) throw new NotFoundException("Cart not found!");
            return cart;
        }

        public async Task<Cart?> GetCartByUserIdAsync(int userId)
        {
            var cart = await _repository.GetCartByUserIdAsync(userId);
            if (cart == null) throw new NotFoundException("Cart not found!");
            return cart;
        }

        public async Task<Cart?> RemoveItemFromCartAsync(int cartId, int productId, int quantity)
        {
            var cart = await _repository.GetCartByIdAsync(cartId);
            if (cart == null) throw new NotFoundException("Cart not found!");
            List<CartItem> items = cart.Products;
            var itemToUpdate = items.First(item => item.ProductId == productId);
            if (itemToUpdate == null) throw new NotFoundException("Product not found in the cart!");
            if (itemToUpdate.Quantity == quantity) items.Remove(itemToUpdate);
            else itemToUpdate.Quantity -= quantity;
            return await _repository.UpdateCartAsync(cart);
        }
    }
}
