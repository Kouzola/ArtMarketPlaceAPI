using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entities
{
    public class Product
    {
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Reference { get; set; } = null!;
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; } = null!;
        public bool Available { get; set; } = true;
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        //Relation related Field
        public User Artisan { get; set; } = null!;
        public int ArtisanId { get; set; }
        public Category Category { get; set; } = null!;
        public int CategoryId { get; set; }
        public ICollection<Review> Reviews { get; } = new List<Review>();
        public ICollection<Customization> Customizations { get; } = new List<Customization>();
        public ICollection<OrderProduct> OrderProducts { get; } = new List<OrderProduct>();
    }
}
