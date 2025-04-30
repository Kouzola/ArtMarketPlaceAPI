using Data_Access_Layer.AppDbContext;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class OrderRepository(ArtMarketPlaceDbContext context) : IOrderRepository
    {
        private readonly ArtMarketPlaceDbContext _context = context;

        public async Task<Order> AddOrderAsync(Order order)
        {
            //Remplir OrderProduct sur le front donc avoir un bon DTO qui remplit ca bien

            //OUVRIR LE ORDER PRODUCT ET RAJOUTER DES TRUCS DEDANS
            var orderAdded = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return orderAdded.Entity;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var orderToDelete = await _context.Orders.FindAsync(id);
            if (orderToDelete == null) return false;
            _context.Orders.Remove(orderToDelete);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Order>> GetAllOrderOfCustomerAsync(int customerId)
        {
            var orders = await _context.Orders.Where(o => o.CustomerId == customerId)
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                .Include(o => o.Shipments)
                .ToListAsync();

            return orders;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                .Include(o => o.Shipments)
                .ToListAsync();

            return orders;
        }

        public async Task<Order?> GetOrderByCodeAsync(string code)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                .Include(o => o.Shipments)
                .FirstOrDefaultAsync(o => o.Code == code);

            if (order == null) return null;
            return order;
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .Include(o => o.Shipments)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return null;
            return order;
        }

        public async Task<Order?> GetOrderOfAnShipmentAsync(int shipmentId)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                .Include(o => o.Shipments)
                .FirstOrDefaultAsync(o => o.Shipments.Any(s => s.Id == shipmentId));

            if (order == null) return null;
            return order;
        }

        public async Task<Order?> UpdateOrderAsync(Order order)
        {
            var orderToUpdated = await _context.Orders.FindAsync(order.Id);
            if(orderToUpdated == null) return null;

            orderToUpdated.OrderDate = order.OrderDate;
            orderToUpdated.Status = order.Status;
            orderToUpdated.ShippingOption = order.ShippingOption;
            orderToUpdated.PaymentDetail = order.PaymentDetail;
            orderToUpdated.UpdatedAt = DateTime.Now;

            //Ouvrir le ORDER PRODUCT ET LE MODIFIER

            await _context.SaveChangesAsync();
            return orderToUpdated;
        }
    }
}
