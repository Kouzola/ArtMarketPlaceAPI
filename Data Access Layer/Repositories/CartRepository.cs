using Data_Access_Layer.AppDbContext;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Cart;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class CartRepository(ArtMarketPlaceDbContext context) : ICartRepository
    {
        private readonly ArtMarketPlaceDbContext _context = context;

        public async Task<Cart> AddCartAsync(Cart cart)
        {
            var addedCart = await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return addedCart.Entity;
        }

        public async Task<bool> DeleteCartAsync(int cartId)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart == null) return false;
            _context.Remove(cart);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Cart?> GetCartByIdAsync(int cartId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                    .ThenInclude(ci => ci.Product)
                    .ThenInclude(c => c.Customizations)
                .FirstOrDefaultAsync(c => c.Id == cartId);
            if (cart == null) return null;
            return cart;
        }

        public async Task<Cart?> GetCartByUserIdAsync(int userId)
        {
            var cart = await _context.Carts.Include(c => c.Items).ThenInclude(ci => ci.Product).ThenInclude(c => c.Customizations).FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null) return null;
            return cart;
        }

        public async Task<Cart?> UpdateCartAsync(Cart cart)
        {
            var cartToUpdate = await _context.Carts.Include(c => c.Items).ThenInclude(ci => ci.Product).ThenInclude(c => c.Customizations).FirstOrDefaultAsync(c => c.Id == cart.Id);
            if (cartToUpdate == null) return null;

            cartToUpdate.Items = cart.Items;
            cartToUpdate.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return cartToUpdate;

        }
    }
}
