using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Interfaces.Cart
{
    public interface ICartRepository
    {
        Task<Entities.Cart?> GetCartByUserIdAsync(int userId);
        Task<Entities.Cart?> GetCartByIdAsync(int cartId);
        Task<Entities.Cart> AddCartAsync(Entities.Cart cart);
        Task<Entities.Cart?> UpdateCartAsync(Entities.Cart cart);
        Task<bool> DeleteCartAsync(int cartId);
    }
}
