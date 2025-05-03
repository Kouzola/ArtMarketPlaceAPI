using Business_Layer.Exceptions;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Cart;
using Domain_Layer.Interfaces.Order;
using Domain_Layer.Interfaces.PaymentDetails;
using Domain_Layer.Interfaces.Product;
using Domain_Layer.Interfaces.Shipment;
using Domain_Layer.Interfaces.User;

namespace Business_Layer.Services
{
    public class OrderService(IOrderRepository repository, IUserService userService, 
        IProductService productService, IPaymentDetailsService paymentDetailsService,
        IShipmentService shipmentService, ICartService cartService) : IOrderService
    {
        private readonly IOrderRepository _repository = repository;
        private readonly IUserService _userService = userService;
        private readonly IProductService _productService = productService;
        private readonly IPaymentDetailsService _paymentDetailsService = paymentDetailsService;
        private readonly IShipmentService _shipmentService = shipmentService;
        private readonly ICartService _cartService = cartService;
        //TODO AFFICHER STOCK - RESERVE STOCK SUR L'UI
        //Les appel de GET avec des services vérifient dèja l'existence de l'entité
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
            dbProductsToUpdate.ForEach(async x => await _productService.UpdateProductAsync(x));

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

        public async Task<PaymentDetail> PayOrderAsync(int orderId, PaymentDetail paymentDetail, int cartId)
        {
            var order = await _repository.GetOrderByIdAsync(orderId);
            if (order == null) throw new NotFoundException("Order not found");
            double orderTotalPrice = await GetOrderTotalPriceAsync(orderId);
            if (paymentDetail.Amount != orderTotalPrice) throw new BusinessException("The amount of the payment does not match the price of the order!");
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

            await UpdateOrderStatusAsync(orderId,OrderStatus.PENDING);

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
            if(!artisanProduct.Any()) throw new BusinessException("No products to ship for this artisan in the order.");
            var shipment = await _shipmentService.AddShipmentAsync(new Shipment
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
                    if (status == OrderStatus.PENDING) actualOrderStatus = OrderStatus.PENDING;
                    else if (status == OrderStatus.CANCEL) actualOrderStatus = OrderStatus.CANCEL;
                    break;
                case OrderStatus.PENDING:
                    if (status == OrderStatus.CONFIRM) actualOrderStatus = OrderStatus.CONFIRM;
                    break;
                case OrderStatus.CONFIRM:
                    if (status == OrderStatus.SHIPPED) actualOrderStatus = OrderStatus.SHIPPED;
                    break;
                case OrderStatus.SHIPPED:
                    if (status == OrderStatus.DELIVERED) actualOrderStatus = OrderStatus.DELIVERED;
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
            if(arrAllProductConfirmed) await UpdateOrderStatusAsync(order.Id, OrderStatus.CONFIRM);
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
            dbProductsToUpdate.ForEach(async x => await _productService.UpdateProductAsync(x));

            return true;
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
