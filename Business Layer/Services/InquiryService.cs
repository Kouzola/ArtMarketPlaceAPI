using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Inquiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class InquiryService(IInquiryService service) : IInquiryService
    {
        private readonly IInquiryService _service = service;

        public Task<Inquiry> AddInquiryAsync(Inquiry inquiry)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteInquiries(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteInquiryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Inquiry>> GetAllInquiriesForArtistAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Inquiry>> GetAllInquiriesFromCustomerAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<Inquiry?> GetInquiriesByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Inquiry?> UpdateInquiryAsync(Inquiry inquiry)
        {
            throw new NotImplementedException();
        }
    }
}
