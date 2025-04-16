using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public ShippingOption ShippingOption { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        //Relation related Field
        public User Customer { get; set; }
        public int CustomerId { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public PaymentDetail PaymentDetail { get; set; }
        public ICollection<Shipment> Shipments { get; } = new List<Shipment>();

    }

    public enum OrderStatus
    {
        PENDING,
        CONFIRM,
        SHIPPED,
        DELIVERED,
        CANCEL
    }

    public enum ShippingOption
    {
        NORMAL,
        FAST
    }
}
