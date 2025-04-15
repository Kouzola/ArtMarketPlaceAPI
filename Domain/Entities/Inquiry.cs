using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain_Layer.Entities
{
    public class Inquiry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool WantConsultation { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        //Relation related Field
        public int CustomerId { get; set; }
        public User Customer { get; set; }
        public int ArtisanId { get; set; }
        public User Artisan { get; set; }
    }
}