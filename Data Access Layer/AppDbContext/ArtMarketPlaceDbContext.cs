using Data_Access_Layer.Configurations;
using Domain_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.AppDbContext
{
    public class ArtMarketPlaceDbContext(DbContextOptions<ArtMarketPlaceDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Inquiry> Inquiry { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customization> Customizations { get; set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderStatusPerArtisan> OrderStatusPerStatus {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new InquiryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new ShipmentConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CustomizationConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentDetailConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusPerArtisanConfiguration());

            var admin = new User
            {
                Id = 1,
                UserName = "admin",
                Password = "AC9126E7F5A5CF1564991D85C03CB1C2",
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@example.com",
                Role = Role.Admin,
                Active = true,
                CreatedAt = DateTime.Parse("2025-04-19 11:41:06.6633333"),
            };

            var customer1 = new User
            {
                Id = 2,
                UserName = "Jean",
                Password = "5FCF306D2BA332CDCC0EDE5C8158138D",
                FirstName = "Jean",
                LastName = "Bon",
                Email = "jean@example.com",
                Role = Role.Customer,
                Active = true,
                CreatedAt = DateTime.Parse("2025-04-19 12:31:59.8466667")
            };

            var artisan1 = new User
            {
                Id = 3,
                UserName = "Paul",
                Password = "33073B61D3610D08BBEF298767FE5572",
                FirstName = "Paul",
                LastName = "Sevran",
                Email = "paul@example.com",
                Role = Role.Artisan,
                Active = true,
                CreatedAt = DateTime.Parse("2025-04-19 12:36:25.0366667")
            };

            var customer2 = new User
            {
                Id = 4,
                UserName = "Julien1",
                Password = "2562122D9A3F07BEB424CAE6CBB45ECE",
                FirstName = "Julien",
                LastName = "De Gaulle",
                Email = "julien.degol@gmail.com",
                Role = Role.Customer,
                Active = true,
                CreatedAt = DateTime.Parse("2025-05-17 23:57:57.8000000")
            };

            var artisan2 = new User
            {
                Id = 5,
                UserName = "Adrien",
                Password = "FA6E8A721B9885D39DCC052976ED5CD8",
                FirstName = "Adrien",
                LastName = "Dupont",
                Email = "a.dupont@gmail.com",
                Role = Role.Artisan,
                Active = true,
                CreatedAt = DateTime.Parse("2025-06-08 12:52:41.3333333")
            };

            var delivery1 = new User
            {
                Id = 6,
                UserName = "Gabin",
                Password = "6314BAF29B5CCD2038277303DA8FE59B",
                FirstName = "Gabin",
                LastName = "Latour",
                Email = "g.latour@gmail.com",
                Role = Role.Delivery,
                Active = true,
                CreatedAt = DateTime.Parse("2025-06-08 13:04:56.8633333")
            };

            var delivery2 = new User
            {
                Id = 7,
                UserName = "Kevin",
                Password = "125EA6D54A96CBFCF5144EE10A10F672",
                FirstName = "Kevin",
                LastName = "Statam",
                Email = "kevin@deliveryman.com",
                Role = Role.Delivery,
                Active = true,
                CreatedAt = DateTime.Parse("2025-04-19 12:37:19.6633333")
            };
            var category1 = new Category
            {
                Id = 1,
                Name = "Poterie",
                Description = "Assiettes, bols ou tasses en céramiques."
            };
            var category2 = new Category
            {
                Id = 2,
                Name = "Bijoux",
                Description = "Bijoux en or ou argent fait maison."
            };

            var product1 = new Product
            {
                Id = 1,
                Name = "Bol en céramique",
                Description = "Bol en céramique solide",
                Reference = "PAPS-1-82BD9C",
                Price = 19.99,
                Stock = 5,
                Image = "46494bf1ef2343bcaf6ef3846296a4e5.jpg",
                Available = true,
                ArtisanId = 3,
                CategoryId = 1,
                ReservedStock = 0
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "Assiette Creuse",
                Description = "Assiette Creuse en céramique.",
                Reference = "PAPS-2-F8E0D5",
                Price = 10,
                Stock = 10,
                Image = "aa1b85d03b774fb18d00ad1a7287f268.jpg",
                Available = true,
                ArtisanId = 3,
                CategoryId = 1,
                ReservedStock = 0
            };

            var product3 = new Product
            {
                Id = 3,
                Name = "Assiette",
                Description = "Assiette en céramique.",
                Reference = "PAPS-3-3B66A5",
                Price = 15,
                Stock = 5,
                Image = "b2e82282131c46bb9f91dd22f078e6da.jpg",
                Available = true,
                ArtisanId = 3,
                CategoryId = 1,
                ReservedStock = 0
            };

            var product4 = new Product
            {
                Id = 4,
                Name = "Collier celte",
                Description = "Collier celte de l'antiquité",
                Reference = "ADAD-0-EFE65F",
                Price = 49.99,
                Stock = 9,
                Image = "e875c773f7624e078a4258cf5c6ed26b.jpeg",
                Available = true,
                ArtisanId = 5,
                CategoryId = 2,
                ReservedStock = 0
            };

            var product5 = new Product
            {
                Id = 5,
                Name = "Bracelet de Fleur",
                Description = "Un beau bracelet de fleur en or.",
                Reference = "ADAD-1-4110F7",
                Price = 15,
                Stock = 10,
                Image = "c74aa49fdeec41b69fe993de0fdfc46e.jpg",
                Available = true,
                ArtisanId = 5,
                CategoryId = 2,
                ReservedStock = 0
            };
            var customization1 = new Customization
            {
                Id = 1,
                Name = "Couleur personnalisé",
                Description = "Couleur de votre choix.",
                Price = 5.99,
                ProductId = 1
            };
            var customization2 = new Customization
            {
                Id = 2,
                Name = "Taille personnalisé",
                Description = "Taille de votre choix.",
                Price = 10,
                ProductId = 2
            };
            var order1 = new Order
            {
                Id = 1,
                Code = "4e0cc3da6c204423",
                OrderDate = DateTime.Parse("2025-06-08 13:41:27.0633333"),
                Status = OrderStatus.PENDING,
                ShippingOption = ShippingOption.FAST,
                CustomerId = 4,
            };
            var orderProduct = new OrderProduct
            {
                Id = 1,
                OrderId = 1,
                ProductId = 4,
                Quantity = 1,
                UnitPrice = 49.99,
                IsValidatedByArtisan = false
            };
            var orderPerStatus = new OrderStatusPerArtisan
            {
                OrderId = 1,
                ArtisanId = 5,
                Status = 0
            };
            var paymentDetail1 = new PaymentDetail
            {
                Id = 1,
                PaymentMethod = "BANCONTACT",
                Amount = 49.99,
                PaymentDate = DateTime.Parse("2025-06-08 15:41:30.6052851"),
                OrderId = 1
            };

            modelBuilder.Entity<User>().HasData(admin, customer1, customer2, artisan1, artisan2, delivery1, delivery2);
            modelBuilder.Entity<User>().OwnsOne(u => u.Address).HasData(
                new { UserId = 1, Street = "123 Admin St", City = "AdminCity", PostalCode = "12345", Country = "AdminLand" },
                new { UserId = 2, Street = "123 Rue de Paris", City = "Paris", PostalCode = "7500", Country = "France" },
                new { UserId = 3, Street = "456 Rue de Lyon", City = "Lyon", PostalCode = "69000", Country = "France" },
                new { UserId = 4, Street = "Rue des Combattants 14", City = "Paris", PostalCode = "10000", Country = "France" },
                new { UserId = 5, Street = "Rue des Potiers", City = "Bordeaux", PostalCode = "25600", Country = "France" },
                new { UserId = 6, Street = "Rue des Livreurs", City = "Bordeaux", PostalCode = "23503", Country = "France" },
                new { UserId = 7, Street = "789 Rue de Marseille", City = "Marseille", PostalCode = "13000", Country = "France" }
            );
            modelBuilder.Entity<Category>().HasData(category1, category2);
            modelBuilder.Entity<Product>().HasData(product1, product2, product3, product4, product5);
            modelBuilder.Entity<Customization>().HasData(customization1, customization2);
            modelBuilder.Entity<Order>().HasData(order1);
            modelBuilder.Entity<OrderProduct>().HasData(orderProduct);
            modelBuilder.Entity<OrderStatusPerArtisan>().HasData(orderPerStatus);
            modelBuilder.Entity<PaymentDetail>().HasData(paymentDetail1);

        }

    }
}
