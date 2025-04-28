using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Interfaces.Cart
{
    public interface ICartService
    {
        Task<Entities.Cart?> GetCartByUserIdAsync(int userId);
        Task<Entities.Cart> AddCartAsync(Entities.Cart cart);
        Task<bool> DeleteCartAsync(int cartId);
        Task<Entities.Cart?> RemoveItemFromCartAsync(int cartId, int productId);
        Task<Entities.Cart?> AddItemToCartAsync(int cartId, int productId);
    }
}
