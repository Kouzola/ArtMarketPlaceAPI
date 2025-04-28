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
        public DateTime Created { get; private set; }
        public DateTime Updated { get; set; }

        public User User { get; set; } = null!;
        public int UserId { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
