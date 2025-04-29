using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class OrderService(IOrderRepository repository) : IOrderService
    {
        private readonly IOrderRepository _repository = repository;
        //DES METHODES VONT SUREMENT ETRE SUPPRIMER

        public Task<Order> AddOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelOrderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ConfirmOrderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOrderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAllOrderOfCustomerAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByStatusAsync(OrderStatus status)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderOfAnShipmentAsync(int shipmentId)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentDetail> GetOrderPaymentDetailAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<double> GetOrderTotalPriceAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentDetail> PayOrderAsync(int orderId, PaymentDetail paymentDetail)
        {
            throw new NotImplementedException();
        }

        public Task<Order> ShipOrderAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateOrderStatus(int orderId, OrderStatus status)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateAndProcessOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
