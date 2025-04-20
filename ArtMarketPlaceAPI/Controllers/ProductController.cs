using ArtMarketPlaceAPI.Dto.Mappers;
using ArtMarketPlaceAPI.Dto.Request;
using Domain_Layer.Interfaces.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtMarketPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        #region Category
        #region GET
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories.Select(c => c.MapToDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category.MapToDto());
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCategoryById(string name)
        {
            var category = await _categoryService.GetCategoryByNameAsync(name);
            return Ok(category.MapToDto());
        }
        #endregion

        #region POST
        [HttpPost]
        [Authorize(Roles = "Artisan, Admin")]
        public async Task<IActionResult> AddCategory(CategoryRequestDto request)
        {
            var category = await _categoryService.AddCategoryAsync(new Domain_Layer.Entities.Category
            {
                Name = request.Name,
                Description = request.Description,
            });
            return Ok(category.MapToDto());
        }
        #endregion

        #region PUT
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryRequestDto request)
        {
            var category = await _categoryService.UpdateCategoryAsync(new Domain_Layer.Entities.Category
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
            });
            return Ok(category.MapToDto());
        }
        #endregion

        #region DELETE
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategorybyIdAsync(id);
            if(response) return Ok($"Product with id : {id} deleted");
            return NotFound();
        }
        #endregion
        #endregion

        #region Product

        #endregion

        #region Customization

        #endregion
    }
}
