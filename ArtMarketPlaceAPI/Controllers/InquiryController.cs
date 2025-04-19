using ArtMarketPlaceAPI.Dto.Mappers;
using ArtMarketPlaceAPI.Dto.Request;
using Domain_Layer.Interfaces.Inquiry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArtMarketPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InquiryController(IInquiryService service) : ControllerBase
    {
        private readonly IInquiryService _service = service;

        #region GET
        [HttpGet("artisan/{artisanId:int}")]
        [Authorize (Roles = "Artisan,Admin")]
        public async Task<IActionResult> GetInquiriesForAnArtisanByUsername(int artisanId)
        {
            var currentUserId = User.FindFirst("id")?.Value;

            if (currentUserId != artisanId.ToString() && User.FindFirstValue(ClaimTypes.Role) != "admin") return Forbid();

            var inquiries = await _service.GetAllInquiriesForArtistAsync(artisanId);
            return Ok(inquiries.Select(i => i.MapToDto()));
        }

        [HttpGet("customer/{customerId:int}")]
        [Authorize(Roles = "Customer, Admin")]
        public async Task<IActionResult> GetInquiriesForAnCustomerByUsername(int customerId)
        {
            var currentUserId = User.FindFirst("id")?.Value;

            if (currentUserId != customerId.ToString() && User.FindFirstValue(ClaimTypes.Role) != "admin") return Forbid();

            var inquiries = await _service.GetAllInquiriesFromCustomerAsync(customerId);
            return Ok(inquiries.Select(i => i.MapToDto()));
        }
        #endregion

        #region POST
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddInquiry(InquiryRequestDto request)
        {
            var inquiry = await _service.AddInquiryAsync(new Domain_Layer.Entities.Inquiry
            {
                Title = request.Title,
                Description = request.Description,
                WantConsultation = request.WantConsultation,
                CustomerId = request.CustomerId,
                ArtisanId = request.ArtisanId,
            });
            return Ok(inquiry.MapToDto());
        }
        #endregion

        #region PUT
        [HttpPut("customer/{id:int}")]
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
            return Ok(inquiry.MapToDto());
        }

        [HttpPut("artisan/{id:int}")]
        [Authorize(Roles = "Artisan")]
        public async Task<IActionResult> AnswerToInquiry(int id, [FromBody] string answer)
        {
            //Check si l'inquiry lui appartient
            var currentUserId = User.FindFirst("id")?.Value;
            var inquiry = await _service.GetInquiriesByIdAsync(id);
            if (inquiry!.ArtisanId.ToString() != currentUserId) return Forbid();
            //Repondre
            var updatedInquiry = await _service.AnswerToInquiry(id, answer);
            return Ok(updatedInquiry.MapToDto());
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
