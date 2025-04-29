using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entities
{
    public class Shipment
    {
        public int Id { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? EstimatedArrivalDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string TrackingNumber { get; set; } = null!;
        public ShipmentStatus Status { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        //Relation related Field
        public Order Order { get; set; } = null!;
        public int OrderId { get; set; }
        public User DeliveryPartner { get; set; } = null!;
        public int DeliveryPartnerId { get; set; }
    }

    public enum ShipmentStatus
    {
        PENDING_PICKUP,
        IN_TRANSIT,
        OUT_FOR_DELIVERY,
        DELIVERED,
        LOST
    }
}
