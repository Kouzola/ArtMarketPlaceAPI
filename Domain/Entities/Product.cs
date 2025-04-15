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
        public string Name { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
        public bool Available { get; set; } = true;
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        //Relation related Field
        public User Artisan { get; set; }
        public int ArtisanId { get; set; }
        public Category category { get; set; }
        public int CategoryId { get; set; }
        public ICollection<Review> Reviews { get; } = new List<Review>();

    }
}
