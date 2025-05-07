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
        #region ORDER
        Task<IEnumerable<Entities.Order>> GetAllOrderForAnArtisanAsync(int artisanId);
        Task<IEnumerable<Entities.Order>> GetAllOrderOfCustomerAsync(int customerId);
        Task<Entities.Order> GetOrderOfAnShipmentAsync(int shipmentId);
        Task<Entities.Order> GetOrderByIdAsync(int id);
        Task<Entities.Order> GetOrderByCodeAsync(string code);
        Task<Entities.Order> CreateOrderFromCartAsync(int cartId, int customerId);
        Task<Entities.Order> UpdateOrderAsync(Entities.Order order);
        Task<bool> DeleteOrderAsync(int id);
        Task<double> GetOrderTotalPriceAsync(int id);

        //Transport
        Task<Entities.Order> UpdateOrderStatusAsync(int orderId, OrderStatus status);
        Task<bool> ShipOrderAsync(int orderId, int deliveryPartnerId, int artisanId);
        //Payment
        Task<PaymentDetail> PayOrderAsync(int orderId, PaymentDetail paymentDetail, int cartId);
        //Validation
        Task<bool> CancelOrderAsync(int orderId);
        Task ValidateAndProcessOrderAsync(Entities.Order order); //Condition OK => Processus lancer paiement etc
        Task<bool> ValidateProductsInOrderAsync(int orderId, int artisanId);
        #endregion

        #region Shipment
        Task<IEnumerable<Entities.Shipment>> GetAllShipmentOfAnOrderAsync(int orderId);
        Task<IEnumerable<Entities.Shipment>> GetAllShipmentOfAnDeliveryPartnerAsync(int deliveryPartnerId);
        Task<Entities.Shipment> GetShipmentByTrackingNumberAsync(string trackingNumber);
        Task<Entities.Shipment> GetShipmentByIdAsync(int id);
        Task<Entities.Shipment> AddShipmentAsync(Entities.Shipment shipment);
        Task<Entities.Shipment> UpdateShipmentDeliveryStatusAsync(int shipmentId, ShipmentStatus shipmentStatus); //Quand on passe en expedier on va rajouter la date expedition
        Task<Entities.Shipment> UpdateEstimatedTimeArrivalAsync(int shipmentId, DateTime estimatedTime);
        Task<bool> DeleteShipmentAsync(int shipmentId);
        #endregion

        #region PaymentDetail
        Task<PaymentDetail?> GetPaymentDetailsByIdAsync(int id);
        Task<PaymentDetail?> GetPaymentDetailsByOrderIdAsync(int orderId);
        Task<PaymentDetail> AddPaymentDetailsAsync(PaymentDetail paymentDetail);
        Task<PaymentDetail?> UpdatePaymentDetailsAsync(PaymentDetail paymentDetail);
        Task<bool> DeletePaymentDetailsAsync(int id);
        #endregion

    }
}
