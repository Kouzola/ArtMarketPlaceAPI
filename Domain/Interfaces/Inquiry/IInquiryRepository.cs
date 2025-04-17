using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Interfaces.Inquiry
{
    public interface IInquiryRepository
    {
        Task<IEnumerable<Entities.Inquiry>> GetAllInquiriesAsync();
        Task<Entities.Inquiry?> GetInquiriesByIdAsync(int id);
        Task<Entities.Inquiry> AddInquiryAsync(Entities.Inquiry inquiry);
        Task<Entities.Inquiry?> UpdateInquiryAsync(Entities.Inquiry inquiry);
        Task<bool> DeleteInquiryAsync(int id);
    }
}
