using Business_Layer.Exceptions;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Order;
using Domain_Layer.Interfaces.PaymentDetails;
using Domain_Layer.Interfaces.Product;
using Domain_Layer.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class OrderService(IOrderRepository repository, IUserService userService, IProductService productService, IPaymentDetailsService paymentDetailsService) : IOrderService
    {
        private readonly IOrderRepository _repository = repository;
        private readonly IUserService _userService = userService;
        private readonly IProductService _productService = productService;
        private readonly IPaymentDetailsService _paymentDetailsService = paymentDetailsService;
        //DES METHODES VONT SUREMENT ETRE SUPPRIMER
        //TODO AFFICHER STOCK - RESERVE STOCK SUR L'UI

        public async Task<Order> AddOrderAsync(Order order)
        {
            //Checker avec les quantité des produit d'orderProduct est augmenté la reserver stock
            var customer = await _userService.GetUserByIdAsync(order.CustomerId); //Si customer existe pas le GetUserById lance une exception
            //Verifier les produits s'ils existent et s'ils sont en stock , si oui on reserve la quantité demandé de produit
            var orderProducts = order.OrderProducts.ToList();
            List<Product> dbProductsToUpdate = new List<Product>();
            foreach (var orderProduct in orderProducts)
            {
                var productDb = await _productService.GetProductByIdAsync(orderProduct.ProductId);
                if (orderProduct.Quantity > productDb.Stock - productDb.ReservedStock) throw new BusinessException($"The {productDb.Name} is Out Of Stock!");
                productDb.ReservedStock += orderProduct.Quantity;
                dbProductsToUpdate.Add(productDb); //On met les produits dans une liste pour les update après les vérifications de stocks.
            }

            dbProductsToUpdate.ForEach(async x => await _productService.UpdateProductAsync(x));

            return await _repository.AddOrderAsync(order);
        }

        
        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _repository.DeleteOrderAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllOrderOfCustomerAsync(int customerId)
        {
            var customer = await _userService.GetUserByIdAsync(customerId);

            return await _repository.GetAllOrderOfCustomerAsync(customerId);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _repository.GetAllOrdersAsync();
        }

        public async Task<Order> GetOrderByCodeAsync(string code)
        {
            var order = await _repository.GetOrderByCodeAsync(code);
            if (order == null) throw new NotFoundException("Order not found!");
            return order;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = await _repository.GetOrderByIdAsync(id);
            if (order == null) throw new NotFoundException("Order not found!");
            return order;
        }

        public async Task<Order> GetOrderOfAnShipmentAsync(int shipmentId)
        {
            var order = await _repository.GetOrderOfAnShipmentAsync(shipmentId);
            if (order == null) throw new NotFoundException("Order not found!");
            return order;
        }

        public async Task<double> GetOrderTotalPriceAsync(int id)
        {
            var order = await _repository.GetOrderByIdAsync(id);
            if (order == null) throw new NotFoundException("Order not found!");
            double totalPrice = 0.0;
            foreach(var orderProduct in order.OrderProducts)
            {
                totalPrice += orderProduct.UnitPrice * orderProduct.Quantity;
            }
            return totalPrice;

        }

        public async Task<PaymentDetail> PayOrderAsync(int orderId, PaymentDetail paymentDetail)
        {
            var order = await _repository.GetOrderByIdAsync(orderId);
            if (order == null) throw new NotFoundException("Order not found");
            var addedPaymentDetail = await _paymentDetailsService.AddPaymentDetailsAsync(paymentDetail);
            //Retire du stock, les produits quand on paye la commande.
            List<Product> dbProductsToUpdate = new List<Product>();
            foreach (var op in order.OrderProducts)
            {
                var productDb = await _productService.GetProductByIdAsync(op.ProductId);
                productDb.Stock -= op.Quantity;
                productDb.ReservedStock -= op.Quantity;
                dbProductsToUpdate.Add(productDb);
            }
            dbProductsToUpdate.ForEach(async x => await _productService.UpdateProductAsync(x));

            return paymentDetail;
        }

        public Task<Order> ShipOrderAsync(int orderId, int deliveryPartnerId, int artisanId)
        {
            //Traitement de faire un shipment etc choisir un delivery guy etc par artiste ATTENTION ARTISTE PEUT SHIPPER 
            //QUE LES PRODUITS QUI LUI APPARTIENT DONC PRENDRE UNIQUEMENT PRODUIT LUI APPARTENANT
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
