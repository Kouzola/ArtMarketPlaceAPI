using Domain_Layer.Interfaces.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtMarketPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Customer")]
    public class CartController(ICartService cartService) : ControllerBase
    {
        private readonly ICartService _cartService = cartService;

        #region GET

        #endregion

        #region POST

        #endregion

        #region PUT

        #endregion

        #region DELETE

        #endregion
    }
}
