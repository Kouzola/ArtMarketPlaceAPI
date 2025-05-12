using Data_Access_Layer.AppDbContext;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Shipment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class ShipmentRepository(ArtMarketPlaceDbContext context) : IShipmentRepository
    {
        private readonly ArtMarketPlaceDbContext _context = context;

        public async Task<Shipment> AddShipmentAsync(Shipment shipment)
        {
            var shipmentAdded = await _context.Shipments.AddAsync(shipment);
            await _context.SaveChangesAsync();
            return shipmentAdded.Entity;
        }

        public async Task<bool> DeleteShipmentAsync(int shipmentId)
        {
            var shipment = await _context.Shipments.FindAsync(shipmentId);
            if(shipment == null) return false;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Shipment>> GetAllShipmentOfAnDeliveryPartner(int deliveryPartnerId)
        {
            var shipments = await _context.Shipments
                .Where(s => s.DeliveryPartnerId == deliveryPartnerId)
                .Include(s => s.Order)
                .Include(s => s.Products)
                .Include(s => s.DeliveryPartner)
                .ToListAsync();

            return shipments;
        }

        public async Task<IEnumerable<Shipment>> GetAllShipmentOfAnOrder(int orderId)
        {
            var shipments = await _context.Shipments
                .Where(s => s.OrderId == orderId)
                .Include(s => s.Order)
                .Include(s => s.DeliveryPartner)
                .Include(s => s.Products)
                .ToListAsync();

            return shipments;
        }

        public async Task<Shipment?> GetShipmentByIdAsync(int id)
        {
            var shipment = await _context.Shipments
                .Include(s => s.Order)
                .Include(s => s.DeliveryPartner)
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.Id == id);
            if(shipment == null) return null;
            return shipment;
        }

        public async Task<Shipment?> GetShipmentByTrackingNumberAsync(string trackingNumber)
        {
            var shipment = await _context.Shipments
                .Include(s => s.Order)
                .Include(s => s.DeliveryPartner)
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.TrackingNumber == trackingNumber);
            if (shipment == null) return null;
            return shipment;
        }

        public async Task<Shipment?> UpdateShipmentAsync(Shipment shipment)
        {
            var shipmentToUpdate = await _context.Shipments
                .Include(s => s.Order)
                .Include(s => s.DeliveryPartner)
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.Id == shipment.Id);

            if(shipmentToUpdate == null) return null;

            shipmentToUpdate.ShippingDate = shipment.ShippingDate;
            shipmentToUpdate.EstimatedArrivalDate = shipment.EstimatedArrivalDate;
            shipmentToUpdate.ArrivalDate = shipment.ArrivalDate;
            shipmentToUpdate.TrackingNumber = shipment.TrackingNumber;
            shipmentToUpdate.Status = shipment.Status;
            shipmentToUpdate.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();  
            return shipmentToUpdate;
        }
    }
}
