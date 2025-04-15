using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain_Layer.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        //Relation related Field
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}