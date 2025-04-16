using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Role Role { get; set; }
        public bool Active { get; set; } = true;
        public Address Address { get; set; } = null!;
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        //Relation related Field
        public ICollection<Product> Products { get; } = new List<Product>();
        public ICollection<Review> Reviews { get; } = new List<Review>();
        public ICollection<Inquiry> InquiriesAsCustomer { get; } = new List<Inquiry>(); //TODO : Utiliser une vérif métier pour que pas les delivery guy peut avoir des inquiries
        public ICollection<Inquiry> InquiriesAsArtisan { get; } = new List<Inquiry>();
        public ICollection<Order> Orders { get; } = new List<Order>();
        public ICollection<Shipment> Shipments { get; } = new List<Shipment>(); //TODO: Verif métier pour uniquement les delivery guys

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

    }

    public class Address
    {
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
    }

    public enum Role
    {
        Customer,
        Artisan,
        Delivery,
        Admin
    }
}
