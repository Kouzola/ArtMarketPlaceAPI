using Business_Layer.Exceptions;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Order;
using Domain_Layer.Interfaces.Shipment;
using Domain_Layer.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class ShipmentService(IShipmentRepository repository, IOrderService orderService, IUserService userService) : IShipmentService
    {
        private readonly IShipmentRepository _repository = repository;
        private readonly IOrderService _orderService = orderService;
        private readonly IUserService _userService = userService;

        public async Task<Shipment> AddShipmentAsync(Shipment shipment)
        {
            if (await _orderService.GetOrderByIdAsync(shipment.OrderId) == null) throw new NotFoundException("Order not found!");
            if (await _userService.GetUserByIdAsync(shipment.DeliveryPartnerId) == null) throw new NotFoundException("Delivery Partner not found!");

            //Génération du tracking number
            string guid = Guid.NewGuid().ToString();
            string guidDigits = Regex.Replace(guid, @"[^\d]", ""); 
            var generatedTrackingNumber = $"AMP-{guidDigits}";
            shipment.TrackingNumber = generatedTrackingNumber;

            return await _repository.AddShipmentAsync(shipment);
        }

        public async Task<bool> DeleteShipmentAsync(int shipmentId)
        {
            var isDeleted = await _repository.DeleteShipmentAsync(shipmentId);
            if (!isDeleted) throw new NotFoundException("Shipment not found!");
            return true;
        }

        public async Task<IEnumerable<Shipment>> GetAllShipmentOfAnDeliveryPartner(int deliveryPartnerId)
        {
            if (await _userService.GetUserByIdAsync(deliveryPartnerId) == null) throw new NotFoundException("Delivery Partner not found!");
            var shipments = await _repository.GetAllShipmentOfAnDeliveryPartner(deliveryPartnerId);
            return shipments;
        }

        public async Task<IEnumerable<Shipment>> GetAllShipmentOfAnOrder(int orderId)
        {
            if (await _orderService.GetOrderByIdAsync(orderId) == null) throw new NotFoundException("Order not found!");
            var shipments = await _repository.GetAllShipmentOfAnOrder(orderId);
            return shipments;
        }

        public async Task<Shipment> GetShipmentByIdAsync(int id)
        {
            var shipment = await _repository.GetShipmentByIdAsync(id);
            if (shipment == null) throw new NotFoundException("Shipment not found!");
            return shipment;
        }

        public async Task<Shipment> GetShipmentByTrackingNumberAsync(string trackingNumber)
        {
            var shipment = await _repository.GetShipmentByTrackingNumberAsync(trackingNumber);
            if(shipment == null) throw new NotFoundException("Shipment not found!");
            return shipment;
        }

        public async Task<Shipment> UpdateEstimatedTimeArrival(int shipmentId, DateTime estimatedTime)
        {
            var shipment = await GetShipmentByIdAsync(shipmentId);
            shipment.EstimatedArrivalDate = estimatedTime;
            var updatedShipment = await _repository.UpdateShipmentAsync(shipment);
            return updatedShipment!;
        }

        public async Task<Shipment> UpdateShipmentDeliveryStatus(int shipmentId, ShipmentStatus shipmentStatus)
        {
            var shipment = await GetShipmentByIdAsync(shipmentId);
            var actualStatus = shipment.Status;
            //Quand c in transit faut mettre une date de livraison estimé et init la shipping date
            switch (actualStatus)
            {
                case ShipmentStatus.PENDING_PICKUP:
                    if (shipmentStatus == ShipmentStatus.IN_TRANSIT)
                    {
                        await _orderService.UpdateOrderStatus(shipment.OrderId, OrderStatus.SHIPPED);
                        shipment.Status = ShipmentStatus.IN_TRANSIT;
                        shipment.ShippingDate = DateTime.Now;
                        shipment.EstimatedArrivalDate = DateTime.Now.AddDays(3).Date;
                    }
                    else if (shipmentStatus == ShipmentStatus.LOST) shipment.Status = ShipmentStatus.LOST;
                    break;
                    //Le gars amène le colis a son entrepot
                case ShipmentStatus.IN_TRANSIT:
                    if(shipmentStatus == ShipmentStatus.OUT_FOR_DELIVERY)
                    {
                        if (shipment.EstimatedArrivalDate != DateTime.Now.Date) shipment.EstimatedArrivalDate = DateTime.Now.Date;
                        shipment.Status = ShipmentStatus.OUT_FOR_DELIVERY;
                    }
                    break;
                    //Le gars est en train de livrer le colis vers le client
                case ShipmentStatus.OUT_FOR_DELIVERY:
                    if(shipmentStatus == ShipmentStatus.DELIVERED)
                    {
                        shipment.Status = ShipmentStatus.DELIVERED;
                        await _orderService.UpdateOrderStatus(shipment.OrderId, OrderStatus.DELIVERED);
                        shipment.ArrivalDate = DateTime.Now;
                    }
                    break;

                case ShipmentStatus.LOST:
                    //Not Implemented Yet
                    break;
            }
            var updatedShipment = await _repository.UpdateShipmentAsync(shipment);
            return updatedShipment!;
        }
    }
}
