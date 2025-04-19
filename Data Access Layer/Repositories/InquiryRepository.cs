using Data_Access_Layer.AppDbContext;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Inquiry;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class InquiryRepository : IInquiryRepository
    {
        private readonly ArtMarketPlaceDbContext _context;
        public InquiryRepository(ArtMarketPlaceDbContext context) 
        {
            _context = context;
        }
        public async Task<Inquiry> AddInquiryAsync(Inquiry inquiry)
        {
            await _context.Inquiries.AddAsync(inquiry);
            await _context.SaveChangesAsync();
            return inquiry;
        }

        public async Task<bool> DeleteInquiriesAsync(List<int> ids)
        {
            var inquiries = await _context.Inquiries
                            .Where(i => ids.Contains(i.Id))
                            .ToListAsync();

            if (!inquiries.Any()) return false;

            _context.Inquiries.RemoveRange(inquiries);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteInquiryAsync(int id)
        {
            var inquiry = await _context.Inquiries.FindAsync(id);
            if (inquiry == null) return false;
            _context.Inquiries.Remove(inquiry);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Inquiry>> GetAllInquiriesAsync()
        {
            return await _context.Inquiries.ToListAsync();
        }

        public async Task<Inquiry?> GetInquiriesByIdAsync(int id)
        {
            return await _context.Inquiries.FindAsync(id);
        }

        public async Task<Inquiry?> UpdateInquiryAsync(Inquiry inquiry)
        {
            var inquiryToUpdate = await _context.Inquiries.FirstOrDefaultAsync(i =>  i.Id == inquiry.Id);
            if (inquiryToUpdate == null) return null;

            inquiryToUpdate.Title = inquiry.Title;
            inquiryToUpdate.Description = inquiry.Description;
            inquiryToUpdate.WantConsultation = inquiry.WantConsultation;
            inquiryToUpdate.ArtisanId = inquiry.ArtisanId;
            inquiryToUpdate.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return inquiryToUpdate;
        }
    }
}
