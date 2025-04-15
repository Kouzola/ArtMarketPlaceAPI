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
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public bool Active { get; set; } = true;
        public Address Address { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        //Relation related Field
        public ICollection<Product> Products { get; } = new List<Product>();
        public ICollection<Review> Reviews { get; } = new List<Review>();
        public ICollection<Inquiry> Inquiries { get; } = new List<Inquiry>(); //TODO : Utiliser une vérif métier pour que pas les delivery guy peut avoir des inquiries
        public ICollection<Order> Orders { get; } = new List<Order>();
        public ICollection<Shipment> Shipments { get; } = new List<Shipment>(); //TODO: Verif métier pour uniquement les delivery guys

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    public enum Role
    {
        Customer,
        Artisan,
        Delivery,
        Admin
    }
}
