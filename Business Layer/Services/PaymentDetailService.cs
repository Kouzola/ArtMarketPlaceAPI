using Business_Layer.Exceptions;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Order;
using Domain_Layer.Interfaces.PaymentDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class PaymentDetailService(IPaymentDetailsRepository repository, IOrderService orderService) : IPaymentDetailsService
    {
        private readonly IPaymentDetailsRepository _repository = repository;
        private readonly IOrderService _orderService = orderService;

        public async Task<PaymentDetail> AddPaymentDetailsAsync(PaymentDetail paymentDetail)
        {
            if (await _orderService.GetOrderByIdAsync(paymentDetail.OrderId) == null) throw new NotFoundException("Order not found!");
            return await _repository.AddPaymentDetailsAsync(paymentDetail);
        }

        public async Task<bool> DeletePaymentDetailsAsync(int id)
        {
            var isDeleted = await _repository.DeletePaymentDetailsAsync(id);
            if (!isDeleted) throw new NotFoundException("Payment Detail not found!");
            return true;
        }

        public async Task<PaymentDetail?> GetPaymentDetailsByIdAsync(int id)
        {
            var paymentDetail = await _repository.GetPaymentDetailsByIdAsync(id);
            if(paymentDetail == null) throw new NotFoundException("Payment Detail not found!");
            return paymentDetail;
        }

        public async Task<PaymentDetail?> GetPaymentDetailsByOrderIdAsync(int orderId)
        {
            if (await _orderService.GetOrderByIdAsync(orderId) == null) throw new NotFoundException("Order not found!");

            var paymentDetail = await _repository.GetPaymentDetailsByOrderIdAsync(orderId);
            if (paymentDetail == null) throw new NotFoundException("Payment Detail not found!");
            return paymentDetail;

        }

        public async Task<PaymentDetail?> UpdatePaymentDetailsAsync(PaymentDetail paymentDetail)
        {
            var updatedPaymentDetail = await _repository.UpdatePaymentDetailsAsync(paymentDetail);
            if (updatedPaymentDetail == null) throw new NotFoundException("Payment Detail not found!");
            return updatedPaymentDetail;
        }
    }
}
