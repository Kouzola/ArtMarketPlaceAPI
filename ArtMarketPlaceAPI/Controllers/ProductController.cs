using ArtMarketPlaceAPI.Dto.Mappers;
using ArtMarketPlaceAPI.Dto.Request;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Category;
using Domain_Layer.Interfaces.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtMarketPlaceAPI.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize(Roles = "Customer,Artisan,Admin")]
    public class ProductController(ICategoryService categoryService, IProductService productService, IFileService fileService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IProductService _productService = productService;
        private readonly IFileService _fileService = fileService;

        #region Category
        #region GET
        [HttpGet("Categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories.Select(c => c.MapToDto()));
        }

        [HttpGet("Categories/{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category.MapToDto());
        }

        [HttpGet("Categories/{name}")]
        public async Task<IActionResult> GetCategoryById(string name)
        {
            var category = await _categoryService.GetCategoryByNameAsync(name);
            return Ok(category.MapToDto());
        }
        #endregion

        #region POST
        [HttpPost("Categories")]
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
        [HttpPut("Categories/{id:int}")]
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
        [HttpDelete("Categories/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategorybyIdAsync(id);
            if (response) return Ok($"Product with id : {id} deleted");
            return NotFound();
        }
        #endregion
        #endregion

        #region Product
        #region GET
        [HttpGet("Products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products.Select(p => p.MapToDto()));
        }

        [HttpGet("Products/{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product.MapToDto());
        }
        [HttpGet("Products/by-reference/{reference}")]
        public async Task<IActionResult> GetProductByReference(string reference)
        {
            var product = await _productService.GetProductByReferenceAsync(reference);
            return Ok(product.MapToDto());
        }
        [HttpGet("Products/by-artisanId/{artisanId:int}")]
        public async Task<IActionResult> GetProductsByArtisan(int artisanId)
        {
            var products = await _productService.GetProductsByArtisanAsync(artisanId);
            return Ok(products.Select(p => p.MapToDto()));
        }

        [HttpGet("Products/by-categoryId/{categoryId:int}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products.Select(p => p.MapToDto()));
        }
        #endregion

        #region POST
        [HttpPost("Products")]
        [Authorize(Roles = "Artisan")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddProduct([FromForm] ProductRequestDto request)
        {
            var product = await _productService.AddProductAsync(new Domain_Layer.Entities.Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                Image = await _fileService.SaveImageFileAsync(request.ImageFile),
                Available = request.Available,
                ArtisanId = request.ArtisanId,
                CategoryId = request.CategoryId,
            });

            return Ok(product.MapToDto());
        }
        #endregion

        #region PUT
        [HttpPut("Products/{id:int}")]
        [Authorize(Roles = "Artisan, Admin")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductRequestDto request)
        {
            var productToUpdate = await _productService.GetProductByIdAsync(id);
            if (productToUpdate == null) return NotFound("Product not found!");
            var updatedProduct = new Domain_Layer.Entities.Product
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                Available = request.Available,
                CategoryId = request.CategoryId,
                Image = productToUpdate.Image,
            };
            //Check si même image, si pas on remplace et on supprime l'ancienne
            if (productToUpdate.Image != request.ImageFile.FileName)
            {
                _fileService.DeleteImageFileAsync(productToUpdate.Image);
                updatedProduct.Image = await _fileService.SaveImageFileAsync(request.ImageFile);
            }
            
            var product = await _productService.UpdateProductAsync(updatedProduct);

            return Ok(product.MapToDto());

        }
        #endregion

        #region DELETE
        [HttpDelete("Products/{id:int}")]
        [Authorize(Roles = "Artisan, Admin")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            var response = await _productService.DeleteProductAsync(product);
            if (!response) return NotFound("Product not found!");
             _fileService.DeleteImageFileAsync(product.Image);
            return Ok("Product deleted.");
        }

        [HttpDelete("Products")]
        [Authorize(Roles = "Artisan, Admin")]
        public async Task<IActionResult> DeleteProductsById(List<int> ids)
        {
            List<Product> productToDelete = new List<Product>();
            foreach (var id in ids)
            {
                var product = await _productService.GetProductByIdAsync(id);
                productToDelete.Add(product);
            }
            var response = await _productService.DeleteProductsAsync(productToDelete);
            if (!response) return NotFound("One or multiple products don't exist.");
            foreach (var product in productToDelete) _fileService.DeleteImageFileAsync(product.Image);
            return Ok("Products deleted.");
        }
        #endregion
        #endregion

        #region Customization

        #endregion
    }
}
