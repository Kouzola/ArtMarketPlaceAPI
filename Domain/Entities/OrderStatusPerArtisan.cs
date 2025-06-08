using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entities
{
    public class OrderStatusPerArtisan
    {
        public int OrderId {  get; set; }
        public int ArtisanId { get; set; }

        public Order Order { get; set; } = null!;

        public User Artisan { get; set; } = null!;
        public int Status { get; set; } //0 -> PENDING , 1 -> CONFIRM, 2 -> SHIPPED
    }
}
