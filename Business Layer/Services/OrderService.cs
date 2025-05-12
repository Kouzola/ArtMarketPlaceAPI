using Business_Layer.Exceptions;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Cart;
using Domain_Layer.Interfaces.Order;
using Domain_Layer.Interfaces.PaymentDetails;
using Domain_Layer.Interfaces.Product;
using Domain_Layer.Interfaces.Shipment;
using Domain_Layer.Interfaces.User;
using System.Text.RegularExpressions;

namespace Business_Layer.Services
{
    public class OrderService(IOrderRepository repository, IUserService userService,
        IProductService productService, IPaymentDetailsRepository paymentDetailRepository,
        IShipmentRepository shipmentRepository, ICartService cartService) : IOrderService
    {
        private readonly IOrderRepository _repository = repository;
        private readonly IUserService _userService = userService;
        private readonly IProductService _productService = productService;
        private readonly IPaymentDetailsRepository _paymentDetailsRepository = paymentDetailRepository;
        private readonly IShipmentRepository _shipmentRepository = shipmentRepository;
        private readonly ICartService _cartService = cartService;
        //TODO AFFICHER STOCK - RESERVE STOCK SUR L'UI
        //Les appel de GET avec des services vérifient dèja l'existence de l'entité
        #region ORDER
        public async Task<Order> CreateOrderFromCartAsync(int cartId, int customerId)
        {
            //Checker avec les quantité des produit d'orderProduct est augmenté la reserver stock
            var customer = await _userService.GetUserByIdAsync(customerId);
            var cart = await _cartService.GetCartByIdAsync(cartId);
            List<OrderProduct> orderProducts = new List<OrderProduct>();
            List<Product> dbProductsToUpdate = new List<Product>();
            foreach (var cartItem in cart!.Items)
            {
                //Rajoute le orderProduct
                var orderProduct = new OrderProduct
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Product.Price
                };
                orderProducts.Add(orderProduct);

                //Ajoute les reservedStock
                var productDb = await _productService.GetProductByIdAsync(cartItem.ProductId);
                if (cartItem.Quantity > productDb.Stock - productDb.ReservedStock) throw new BusinessException($"The {productDb.Name} is Out Of Stock!");
                productDb.ReservedStock += orderProduct.Quantity;
                dbProductsToUpdate.Add(productDb); //On met les produits dans une liste pour les update après les vérifications de stocks.
            }

            //update les reservedStock des produits
            foreach (var product in dbProductsToUpdate)
            {
                await _productService.UpdateProductAsync(product);
            }

            var order = new Order
            {
                CustomerId = customerId,
                Code = Guid.NewGuid().ToString("N").Substring(0, 16),
                Status = OrderStatus.NOT_PAYED,
                OrderProducts = orderProducts,
            };

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

        public async Task<IEnumerable<Order>> GetAllOrderForAnArtisanAsync(int artisanId)
        {
            return await _repository.GetAllOrderForAnArtisanAsync(artisanId);
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
            foreach (var orderProduct in order.OrderProducts)
            {
                totalPrice += orderProduct.UnitPrice * orderProduct.Quantity;
            }
            return Math.Round(totalPrice, 2);

        }

        public async Task<PaymentDetail> PayOrderAsync(int orderId, PaymentDetail paymentDetail, int cartId)
        {
            var order = await _repository.GetOrderByIdAsync(orderId);
            if (order == null || order.Status != OrderStatus.NOT_PAYED) throw new NotFoundException("Order not found");
            double orderTotalPrice = await GetOrderTotalPriceAsync(orderId);
            if (paymentDetail.Amount != orderTotalPrice) throw new BusinessException("The amount of the payment does not match the price of the order!");
            var addedPaymentDetail = await AddPaymentDetailsAsync(paymentDetail);
            //Retire du stock, les produits quand on paye la commande.
            List<Product> dbProductsToUpdate = new List<Product>();
            foreach (var op in order.OrderProducts)
            {
                var productDb = await _productService.GetProductByIdAsync(op.ProductId);
                productDb.Stock -= op.Quantity;
                productDb.ReservedStock -= op.Quantity;
                dbProductsToUpdate.Add(productDb);
            }
            foreach (var product in dbProductsToUpdate)
            {
                await _productService.UpdateProductAsync(product);
            }

            await UpdateOrderStatusAsync(orderId, OrderStatus.PENDING);

            //Deleting Cart
            await _cartService.DeleteCartAsync(cartId);

            return paymentDetail;
        }

        public async Task<bool> ShipOrderAsync(int orderId, int deliveryPartnerId, int artisanId)
        {
            var order = await _repository.GetOrderByIdAsync(orderId);
            if (order == null) throw new NotFoundException("Order not found!");
            var deliveryPartner = await _userService.GetUserByIdAsync(deliveryPartnerId);
            var artisan = await _userService.GetUserByIdAsync(artisanId);
            var artisanProduct = order.OrderProducts.Where(p => p.Product.ArtisanId == artisanId).Select(op => op.Product).ToList();
            //création du Shipment avec les produits valider par l'artisan
            if (!artisanProduct.Any()) throw new BusinessException("No products to ship for this artisan in the order.");
            var shipment = await AddShipmentAsync(new Shipment
            {
                Status = ShipmentStatus.PENDING_PICKUP,
                OrderId = orderId,
                DeliveryPartnerId = deliveryPartnerId,
                Products = artisanProduct
            });

            return true;
        }

        public async Task<Order> UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            //Switch Case avec les états
            var order = await _repository.GetOrderByIdAsync(orderId);
            if (order == null) throw new NotFoundException("Order not found!");
            var actualOrderStatus = order.Status;
            switch (actualOrderStatus)
            {
                case OrderStatus.NOT_PAYED:
                    if (status == OrderStatus.PENDING) order.Status = OrderStatus.PENDING;
                    else if (status == OrderStatus.CANCEL) order.Status = OrderStatus.CANCEL;
                    break;
                case OrderStatus.PENDING:
                    if (status == OrderStatus.CONFIRM) order.Status = OrderStatus.CONFIRM;
                    break;
                case OrderStatus.CONFIRM:
                    if (status == OrderStatus.SHIPPED) order.Status = OrderStatus.SHIPPED;
                    break;
                case OrderStatus.SHIPPED:
                    if (status == OrderStatus.DELIVERED) order.Status = OrderStatus.DELIVERED;
                    break;

                case OrderStatus.DELIVERED:
                    break;

                case OrderStatus.CANCEL:
                    break;
            }
            var updatedOrder = await UpdateOrderAsync(order);
            return updatedOrder;
        }

        public async Task ValidateAndProcessOrderAsync(Order order)
        {
            //Checker si tous les produits sont validés et passer en order CONFIRM et UPDATE EN CONFIRM
            var arrAllProductConfirmed = order.OrderProducts.All(op => op.IsValidatedByArtisan);
            if (arrAllProductConfirmed) await UpdateOrderStatusAsync(order.Id, OrderStatus.CONFIRM);
        }

        public async Task<bool> CancelOrderAsync(int id)
        {
            //CANCEL DONC UPDATE SON STATUS en cancel et retire les reserved stock etc etc
            var order = await _repository.GetOrderByIdAsync(id);
            if (order == null) throw new NotFoundException("Order not found!");
            if (order.Status != OrderStatus.NOT_PAYED) throw new BusinessException("Cannot cancel an order already payed!");
            //Remettre les reserved stock a jour
            List<Product> dbProductsToUpdate = new List<Product>();
            foreach (var op in order.OrderProducts)
            {
                var productDb = await _productService.GetProductByIdAsync(op.ProductId);
                productDb.ReservedStock -= op.Quantity;
                dbProductsToUpdate.Add(productDb);
            }

            foreach (var product in dbProductsToUpdate)
            {
                await _productService.UpdateProductAsync(product);
            }

            var isCancel = await DeleteOrderAsync(id);
            return isCancel;
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
        #endregion

        #region Shipment
        public async Task<Shipment> AddShipmentAsync(Shipment shipment)
        {
            if (await GetOrderByIdAsync(shipment.OrderId) == null) throw new NotFoundException("Order not found!");
            if (await _userService.GetUserByIdAsync(shipment.DeliveryPartnerId) == null) throw new NotFoundException("Delivery Partner not found!");

            //Génération du tracking number
            string guid = Guid.NewGuid().ToString();
            string guidDigits = Regex.Replace(guid, @"[^\d]", "");
            var generatedTrackingNumber = $"AMP-{guidDigits}";
            shipment.TrackingNumber = generatedTrackingNumber;

            return await _shipmentRepository.AddShipmentAsync(shipment);
        }

        public async Task<bool> DeleteShipmentAsync(int shipmentId)
        {
            var isDeleted = await _shipmentRepository.DeleteShipmentAsync(shipmentId);
            if (!isDeleted) throw new NotFoundException("Shipment not found!");
            return true;
        }

        public async Task<IEnumerable<Shipment>> GetAllShipmentOfAnDeliveryPartnerAsync(int deliveryPartnerId)
        {
            if (await _userService.GetUserByIdAsync(deliveryPartnerId) == null) throw new NotFoundException("Delivery Partner not found!");
            var shipments = await _shipmentRepository.GetAllShipmentOfAnDeliveryPartner(deliveryPartnerId);
            return shipments;
        }

        public async Task<IEnumerable<Shipment>> GetAllShipmentOfAnOrderAsync(int orderId)
        {
            if (await GetOrderByIdAsync(orderId) == null) throw new NotFoundException("Order not found!");
            var shipments = await _shipmentRepository.GetAllShipmentOfAnOrder(orderId);
            return shipments;
        }

        public async Task<Shipment> GetShipmentByIdAsync(int id)
        {
            var shipment = await _shipmentRepository.GetShipmentByIdAsync(id);
            if (shipment == null) throw new NotFoundException("Shipment not found!");
            return shipment;
        }

        public async Task<Shipment> GetShipmentByTrackingNumberAsync(string trackingNumber)
        {
            var shipment = await _shipmentRepository.GetShipmentByTrackingNumberAsync(trackingNumber);
            if (shipment == null) throw new NotFoundException("Shipment not found!");
            return shipment;
        }

        public async Task<Shipment> UpdateEstimatedTimeArrivalAsync(int shipmentId, DateTime estimatedTime)
        {
            var shipment = await GetShipmentByIdAsync(shipmentId);
            shipment.EstimatedArrivalDate = estimatedTime;
            var updatedShipment = await _shipmentRepository.UpdateShipmentAsync(shipment);
            return updatedShipment!;
        }

        public async Task<Shipment> UpdateShipmentDeliveryStatusAsync(int shipmentId, ShipmentStatus shipmentStatus)
        {
            var shipment = await GetShipmentByIdAsync(shipmentId);
            var actualStatus = shipment.Status;
            //Quand c in transit faut mettre une date de livraison estimé et init la shipping date
            switch (actualStatus)
            {
                case ShipmentStatus.PENDING_PICKUP:
                    if (shipmentStatus == ShipmentStatus.IN_TRANSIT)
                    {
                        shipment.Status = ShipmentStatus.IN_TRANSIT;
                        shipment.ShippingDate = DateTime.Now;
                        shipment.EstimatedArrivalDate = DateTime.Now.AddDays(3).Date;

                        //Si tous les packets sont en transit, on peut update la order au status de SHIPPED
                        var shipmentsOfAnOrder = await GetAllShipmentOfAnOrderAsync(shipment.OrderId);
                        if (shipmentsOfAnOrder.All(s => s.Status == ShipmentStatus.IN_TRANSIT)) await UpdateOrderStatusAsync(shipment.OrderId, OrderStatus.SHIPPED);
                    }
                    else if (shipmentStatus == ShipmentStatus.LOST) shipment.Status = ShipmentStatus.LOST;
                    break;
                //Le gars amène le colis a son entrepot
                case ShipmentStatus.IN_TRANSIT:
                    if (shipmentStatus == ShipmentStatus.OUT_FOR_DELIVERY)
                    {
                        if (shipment.EstimatedArrivalDate != DateTime.Now.Date) shipment.EstimatedArrivalDate = DateTime.Now.Date;
                        shipment.Status = ShipmentStatus.OUT_FOR_DELIVERY;
                    }
                    break;
                //Le gars est en train de livrer le colis vers le client
                case ShipmentStatus.OUT_FOR_DELIVERY:
                    if (shipmentStatus == ShipmentStatus.DELIVERED)
                    {
                        shipment.Status = ShipmentStatus.DELIVERED;
                        shipment.ArrivalDate = DateTime.Now;

                        //Si tous les packets sont delivered, on peut update la order au status de DELIVERED
                        var shipmentsOfAnOrder = await GetAllShipmentOfAnOrderAsync(shipment.OrderId);
                        if (shipmentsOfAnOrder.All(s => s.Status == ShipmentStatus.DELIVERED)) await UpdateOrderStatusAsync(shipment.OrderId, OrderStatus.DELIVERED);
                    }
                    break;

                case ShipmentStatus.LOST:
                    //Not Implemented Yet
                    break;
            }
            var updatedShipment = await _shipmentRepository.UpdateShipmentAsync(shipment);
            return updatedShipment!;
        }
    #endregion

        #region PaymentDetail
    public async Task<PaymentDetail> AddPaymentDetailsAsync(PaymentDetail paymentDetail)
        {
            if (await GetOrderByIdAsync(paymentDetail.OrderId) == null) throw new NotFoundException("Order not found!");
            return await _paymentDetailsRepository.AddPaymentDetailsAsync(paymentDetail);
        }

        public async Task<bool> DeletePaymentDetailsAsync(int id)
        {
            var isDeleted = await _paymentDetailsRepository.DeletePaymentDetailsAsync(id);
            if (!isDeleted) throw new NotFoundException("Payment Detail not found!");
            return true;
        }

        public async Task<PaymentDetail?> GetPaymentDetailsByIdAsync(int id)
        {
            var paymentDetail = await _paymentDetailsRepository.GetPaymentDetailsByIdAsync(id);
            if (paymentDetail == null) throw new NotFoundException("Payment Detail not found!");
            return paymentDetail;
        }

        public async Task<PaymentDetail?> GetPaymentDetailsByOrderIdAsync(int orderId)
        {
            if (await GetOrderByIdAsync(orderId) == null) throw new NotFoundException("Order not found!");

            var paymentDetail = await _paymentDetailsRepository.GetPaymentDetailsByOrderIdAsync(orderId);
            if (paymentDetail == null) throw new NotFoundException("Payment Detail not found!");
            return paymentDetail;

        }

        public async Task<PaymentDetail?> UpdatePaymentDetailsAsync(PaymentDetail paymentDetail)
        {
            var updatedPaymentDetail = await _paymentDetailsRepository.UpdatePaymentDetailsAsync(paymentDetail);
            if (updatedPaymentDetail == null) throw new NotFoundException("Payment Detail not found!");
            return updatedPaymentDetail;
        }
        #endregion

    }
}

