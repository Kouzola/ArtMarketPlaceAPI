using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Interfaces.Order
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Entities.Order>> GetAllOrdersAsync();
        Task<IEnumerable<Entities.Order>> GetAllOrderOfCustomerAsync(int customerId);
        Task<Entities.Order?> GetOrderOfAnShipmentAsync(int shipmentId);
        Task<Entities.Order?> GetOrderByIdAsync(int id);
        Task<Entities.Order?> GetOrderByCodeAsync(string code);
        Task<Entities.Order> AddOrderAsync(Entities.Order order);
        Task<Entities.Order?> UpdateOrderAsync(Entities.Order order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
