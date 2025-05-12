using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; } = null!;
        public int UserId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
