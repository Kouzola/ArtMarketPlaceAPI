using Domain_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Interfaces.Order
{
    public interface IOrderService
    {
        Task<IEnumerable<Entities.Order>> GetAllOrdersAsync();
        Task<IEnumerable<Entities.Order>> GetAllOrderOfCustomerAsync(int customerId);
        Task<Entities.Order> GetOrderOfAnShipmentAsync(int shipmentId);
        Task<Entities.Order> GetOrderByIdAsync(int id);
        Task<Entities.Order> GetOrderByCodeAsync(string code);
        Task<Entities.Order> AddOrderAsync(Entities.Order order);
        Task<Entities.Order> UpdateOrderAsync(Entities.Order order);
        Task<bool> DeleteOrderAsync(int id);
        Task<double> GetOrderTotalPriceAsync(int id);

        //Transport
        Task<Entities.Order> UpdateOrderStatusAsync(int orderId, OrderStatus status);
        Task<bool> ShipOrderAsync(int orderId, int deliveryPartnerId, int artisanId);
        //Payment
        Task<PaymentDetail> PayOrderAsync(int orderId, PaymentDetail paymentDetail);
        //Validation
        Task<bool> CancelOrderAsync(int orderId);
        Task ValidateAndProcessOrderAsync(Entities.Order order); //Condition OK => Processus lancer paiement etc
        Task<bool> ValidateProductsInOrderAsync(int orderId, int artisanId);

    }
}
