using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Interfaces.Inquiry
{
    public interface IInquiryService
    {
        Task<IEnumerable<Entities.Inquiry>> GetAllInquiriesFromCustomerAsync(int customerId);
        Task<IEnumerable<Entities.Inquiry>> GetAllInquiriesForArtistAsync(int artisanId);
        Task<Entities.Inquiry> GetInquiriesByIdAsync(int id);
        Task<Entities.Inquiry> AddInquiryAsync(Entities.Inquiry inquiry);
        Task<Entities.Inquiry> UpdateInquiryAsync(Entities.Inquiry inquiry);
        Task<Entities.Inquiry> AnswerToInquiry(int inquiryId, string answer);
        Task<bool> DeleteInquiryAsync(int id);
        Task<bool> DeleteInquiriesAsync(List<int> ids);

    }
}
