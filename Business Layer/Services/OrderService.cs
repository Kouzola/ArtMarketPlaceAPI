using Business_Layer.Exceptions;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Order;
using Domain_Layer.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class OrderService(IOrderRepository repository, IUserService userService) : IOrderService
    {
        private readonly IOrderRepository _repository = repository;
        private readonly IUserService _userService = userService;
        //DES METHODES VONT SUREMENT ETRE SUPPRIMER

        public Task<Order> AddOrderAsync(Order order)
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
            //Retire du stock, les produits car quand on paye la commande.
            throw new NotImplementedException();
        }

        public Task<Order> ShipOrderAsync(int orderId, int deliveryPartnerId)
        {
            //Traitement de faire un shipment etc choisir un delivery guy etc
            throw new NotImplementedException();
        }

        public Task<Order> UpdateOrderStatus(int orderId, OrderStatus status)
        {
            throw new NotImplementedException();
        }

        public Task ValidateAndProcessOrderAsync(Order order)
        {
            //Checker si tous les produits sont validés et passer en order CONFIRM
            throw new NotImplementedException();
        }

        public Task<bool> CancelOrderAsync(int id)
        {
            //CANCEL DONC UPDATE SON STATUS
            throw new NotImplementedException();
        }

        public Task<bool> ConfirmOrderAsync(int id)
        {
            //METTRE DANS UPDATE STATUS
            throw new NotImplementedException();
        }

        public async Task<bool> ValidateProductsInOrderAsync(int orderId, int artisanId)
        {
            var order = await _repository.GetOrderByIdAsync(orderId);
            if (order == null) throw new NotFoundException("Order not found!");
            if (order.Status == OrderStatus.NOT_PAYED) throw new BusinessException("The order is not payed yet");
            var artisan = await _userService.GetUserByIdAsync(artisanId);
            if (artisan == null) throw new NotFoundException("Artisan not found!");

            var productsToValidate = order.OrderProducts.Where(order => order.Product.ArtisanId == artisanId).ToList();

            if (!productsToValidate.Any()) throw new BusinessException("No products to validate for this artisan in the order.");

            foreach (var product in productsToValidate)
            {
                product.IsValidatedByArtisan = true;
            }

            await ValidateAndProcessOrderAsync(order);

            await UpdateOrderAsync(order);
            return true;



        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            var updatedOrder = await _repository.UpdateOrderAsync(order);
            if (updatedOrder == null) throw new NotFoundException("Order not found!");
            return updatedOrder;
        }
    }
}
