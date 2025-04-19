using Business_Layer.Exceptions;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Inquiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class InquiryService(IInquiryRepository repository) : IInquiryService
    {
        private readonly IInquiryRepository _repository = repository;

        public async Task<Inquiry> AddInquiryAsync(Inquiry inquiry)
        {
            return await _repository.AddInquiryAsync(inquiry);
        }

        public async Task<bool> DeleteInquiriesAsync(List<int> ids)
        {
           return await _repository.DeleteInquiriesAsync(ids);
        }

        public async Task<bool> DeleteInquiryAsync(int id)
        {
            return await _repository.DeleteInquiryAsync(id);
        }

        public async Task<IEnumerable<Inquiry>> GetAllInquiriesForArtistAsync(string username)
        {
            return (await _repository.GetAllInquiriesAsync()).Where(i => i.Artisan.UserName == username);
        }

        public async Task<IEnumerable<Inquiry>> GetAllInquiriesFromCustomerAsync(string username)
        {
            return (await _repository.GetAllInquiriesAsync()).Where(i => i.Customer.UserName == username);
        }

        public async Task<Inquiry?> GetInquiriesByIdAsync(int id)
        {
            var inquiry = await _repository.GetInquiriesByIdAsync(id);
            if (inquiry == null) throw new NotFoundException("Inquiry not found!");
            return inquiry;
        }

        public async Task<Inquiry?> UpdateInquiryAsync(Inquiry inquiry)
        {
            var inquiryUpdated = await _repository.UpdateInquiryAsync(inquiry);
            if (inquiryUpdated == null) throw new NotFoundException("Inquiry not found!");
            return inquiryUpdated;
        }
    }
}
