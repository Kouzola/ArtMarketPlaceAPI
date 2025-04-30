using Data_Access_Layer.AppDbContext;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Cart;
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

        public Task<Cart> AddCartAsync(Cart cart)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCartAsync(int cartId)
        {
            throw new NotImplementedException();
        }

        public Task<Cart?> GetCartByIdAsync(int cartId)
        {
            throw new NotImplementedException();
        }

        public Task<Cart?> GetCartByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> UpdateCartAsync(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}
