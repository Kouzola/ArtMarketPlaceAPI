using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Interfaces.Shipment
{
    public interface IShipmentRepository
    {
        Task<IEnumerable<Entities.Shipment>> GetAllShipmentOfAnOrder(int orderId);
        Task<IEnumerable<Entities.Shipment>> GetAllShipmentOfAnDeliveryPartner(int deliveryPartnerId);
        Task<Entities.Shipment?> GetShipmentByTrackingNumberAsync(string trackingNumber);
        Task<Entities.Shipment?> GetShipmentByIdAsync(int id);
        Task<Entities.Shipment> AddShipmentAsync(Entities.Shipment shipment);
        Task<Entities.Shipment?> UpdateShipmentAsync(Entities.Shipment shipment);
        Task<bool> DeleteShipmentAsync(int shipmentId);
    }
}
