using Data_Access_Layer.AppDbContext;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.PaymentDetails;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class PaymentDetailsRepository(ArtMarketPlaceDbContext context) : IPaymentDetailsRepository
    {
        private readonly ArtMarketPlaceDbContext _context = context;

        public async Task<PaymentDetail> AddPaymentDetailsAsync(PaymentDetail paymentDetail)
        {
            var paymentDetailAdded = await _context.PaymentDetails.AddAsync(paymentDetail);
            await _context.SaveChangesAsync();
            return paymentDetailAdded.Entity;
        }

        public async Task<bool> DeletePaymentDetailsAsync(int id)
        {
            var paymentDetails = await _context.PaymentDetails.FindAsync(id);
            if(paymentDetails == null) return false;
            _context.PaymentDetails.Remove(paymentDetails);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PaymentDetail?> GetPaymentDetailsByIdAsync(int id)
        {
            var paymentDetails = await _context.PaymentDetails
                .Include(pd => pd.Order)
                .FirstOrDefaultAsync(pd => pd.Id == id);
            if(paymentDetails == null) return null;
            return paymentDetails;
        }

        public async Task<PaymentDetail?> GetPaymentDetailsByOrderIdAsync(int orderId)
        {
            var paymentDetails = await _context.PaymentDetails
                .Include(pd => pd.Order)
                .FirstOrDefaultAsync(pd => pd.OrderId == orderId);

            if (paymentDetails == null) return null;
            return paymentDetails;
        }

        public async Task<PaymentDetail?> UpdatePaymentDetailsAsync(PaymentDetail paymentDetail)
        {
            var paymentDetailToUpdate = await _context.PaymentDetails.FindAsync(paymentDetail.Id);
            if (paymentDetailToUpdate == null) return null;

            paymentDetailToUpdate.PaymentMethod = paymentDetail.PaymentMethod;
            paymentDetailToUpdate.Amount = paymentDetail.Amount;
            paymentDetailToUpdate.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return paymentDetailToUpdate;
        }
    }
}
