using ArtMarketPlaceAPI.Dto.Request;
using Domain_Layer.Interfaces.Inquiry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtMarketPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InquiryController(IInquiryService service) : ControllerBase
    {
        private readonly IInquiryService _service = service;

        #region GET
        [HttpGet("artisan/{username}")]
        [Authorize (Roles = "Artisan,Admin")]
        public async Task<IActionResult> GetInquiriesForAnArtisanByUsername(string username)
        {
            var inquiries = await _service.GetAllInquiriesForArtistAsync(username);
            return Ok(inquiries);
        }

        [HttpGet("customer/{username}")]
        [Authorize(Roles = "Customer, Admin")]
        public async Task<IActionResult> GetInquiriesForAnCustomerByUsername(string username)
        {
            var inquiries = await _service.GetAllInquiriesFromCustomerAsync(username);
            return Ok(inquiries);
        }
        #endregion

        #region POST
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateInquiry(InquiryRequestDto request)
        {
            var inquiry = await _service.AddInquiryAsync(new Domain_Layer.Entities.Inquiry
            {
                Title = request.Title,
                Description = request.Description,
                WantConsultation = request.WantConsultation,
                CustomerId = request.CustomerId,
                ArtisanId = request.ArtisanId,
            });
            return Ok(inquiry);
        }
        #endregion

        #region PUT
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddInquiry(InquiryRequestDto request, int id)
        {
            var inquiry = await _service.UpdateInquiryAsync(new Domain_Layer.Entities.Inquiry
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                WantConsultation = request.WantConsultation,
                CustomerId = request.CustomerId,
                ArtisanId = request.ArtisanId,
            });
            return Ok(inquiry);
        }
        #endregion

        #region DELETE
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Customer, Admin")]
        public async Task<IActionResult> DeleteInquiry(int id)
        {
            var inquiry = await _service.DeleteInquiryAsync(id);
            return inquiry ? Ok(inquiry) : NotFound();
        }

        [HttpDelete]
        [Authorize(Roles = "Customer, Admin")]
        public async Task<IActionResult> DeleteInquiries([FromQuery] List<int> ids)
        {
            var inquiry = await _service.DeleteInquiriesAsync(ids);
            return inquiry ? Ok(inquiry) : NotFound();
        }

        #endregion
    }
}
