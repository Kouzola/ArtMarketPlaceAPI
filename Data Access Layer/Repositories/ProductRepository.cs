using Data_Access_Layer.AppDbContext;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class ProductRepository(ArtMarketPlaceDbContext context) : IProductRepository
    {
        private readonly ArtMarketPlaceDbContext _context = context;
        public async Task<Product> AddProductAsync(Product product)
        {
            var productAdded = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return productAdded.Entity;
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductsAsync(List<Product> products)
        {
            _context.Products.RemoveRange(products);
            return await _context.SaveChangesAsync() >= products.Count;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _context.Products
                            .Include(p => p.Artisan)
                            .Include(p => p.Category)
                            .Include(p => p.Customizations)
                            .Include(p => p.Reviews)
                            .ToListAsync();

            return products;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                            .Include(p => p.Artisan)
                            .Include(p => p.Category)
                            .Include(p => p.Customizations)
                            .Include(p => p.Reviews)
                            .FirstOrDefaultAsync(p => p.Id == id);

            return product == null ? null : product;
        }

        public async Task<Product?> GetProductByReferenceAsync(string reference)
        {
            var product = await _context.Products
                            .Include(p => p.Artisan)
                            .Include(p => p.Category)
                            .Include(p => p.Customizations)
                            .Include(p => p.Reviews)
                            .FirstOrDefaultAsync(p => p.Reference == reference);

            return product == null ? null : product;
        }

        public async Task<IEnumerable<Product>> GetProductsByArtisanAsync(int artisanId)
        {
            var product = await _context.Products
                            .Include(p => p.Artisan)
                            .Include(p => p.Category)
                            .Include(p => p.Customizations)
                            .Include(p => p.Reviews)
                            .Where(p => p.ArtisanId ==  artisanId)
                            .ToListAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            var product = await _context.Products
                            .Include(p => p.Artisan)
                            .Include(p => p.Category)
                            .Include(p => p.Customizations)
                            .Include(p => p.Reviews)
                            .Where(p => p.CategoryId == categoryId)
                            .ToListAsync();

            return product;
        }

        public async Task<Product?> UpdateProductAsync(Product product)
        {
            var productToUpdate = await _context.Products.FindAsync(product.Id);
            if (productToUpdate == null) return null;

            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;
            productToUpdate.Stock = product.Stock;
            productToUpdate.ReservedStock = product.ReservedStock;
            productToUpdate.Image = product.Image;
            productToUpdate.Available = product.Available;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return productToUpdate;
        }
    }
}
