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
            var inquiryToAdd = await _context.Inquiry.AddAsync(inquiry);
            await _context.SaveChangesAsync();
            return inquiryToAdd.Entity;
        }

        public async Task<bool> DeleteInquiriesAsync(List<int> ids)
        {
            var inquiries = await _context.Inquiry
                            .Where(i => ids.Contains(i.Id))
                            .ToListAsync();

            if (!inquiries.Any()) return false;

            _context.Inquiry.RemoveRange(inquiries);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteInquiryAsync(int id)
        {
            var inquiry = await _context.Inquiry.FindAsync(id);
            if (inquiry == null) return false;
            _context.Inquiry.Remove(inquiry);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Inquiry>> GetAllInquiriesAsync()
        {
            return await _context.Inquiry
                .Include(i => i.Customer)
                .Include(i => i.Artisan)
                .ToListAsync();
        }

        public async Task<Inquiry?> GetInquiriesByIdAsync(int id)
        {
            return await _context.Inquiry
                .Include(i => i.Customer)
                .Include(i => i.Artisan)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Inquiry?> UpdateInquiryAsync(Inquiry inquiry)
        {
            var inquiryToUpdate = await _context.Inquiry
                .Include(i => i.Customer)
                .Include(i => i.Artisan)
                .FirstOrDefaultAsync(i =>  i.Id == inquiry.Id);
            if (inquiryToUpdate == null) return null;

            inquiryToUpdate.Title = inquiry.Title;
            inquiryToUpdate.Description = inquiry.Description;
            inquiryToUpdate.WantConsultation = inquiry.WantConsultation;
            inquiryToUpdate.ArtisanId = inquiry.ArtisanId;
            inquiryToUpdate.ArtisanResponse = inquiry.ArtisanResponse;
            inquiryToUpdate.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return inquiryToUpdate;
        }
    }
}
