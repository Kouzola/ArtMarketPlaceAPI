using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain_Layer.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Score { get; set; }
        public string? ArtisanAnswer { get; set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        //Relation related Field
        public int CustomerId { get; set; }
        public User Customer { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

    }
}