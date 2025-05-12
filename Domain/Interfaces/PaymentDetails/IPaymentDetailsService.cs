using Domain_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Interfaces.PaymentDetails
{
    public interface IPaymentDetailsService
    {
        Task<PaymentDetail?> GetPaymentDetailsByIdAsync(int id);
        Task<PaymentDetail?> GetPaymentDetailsByOrderIdAsync(int orderId);
        Task<PaymentDetail> AddPaymentDetailsAsync(PaymentDetail paymentDetail);
        Task<PaymentDetail?> UpdatePaymentDetailsAsync(PaymentDetail paymentDetail);
        Task<bool> DeletePaymentDetailsAsync(int id);
    }
}
