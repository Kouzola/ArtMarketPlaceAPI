using Data_Access_Layer.AppDbContext;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Category;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class CategoryRepository(ArtMarketPlaceDbContext context) : ICategoryRepository
    {
        private readonly ArtMarketPlaceDbContext _context = context;
        public async Task<Category> AddCategoryAsync(Category category)
        {
            var categoryToAdd = await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return categoryToAdd.Entity;
        }

        public async Task<bool> DeleteCategorybyIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if(category == null) return false;
            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories
                            .Include(c => c.Products)
                            .ToListAsync();
            return categories;
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories
                            .Include(c => c.Products)
                            .FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return null;
            return category;
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            var category = await _context.Categories
                                .Include(c => c.Products)
                                .FirstOrDefaultAsync(c => c.Name == name.Trim());
            if (category == null) return null;
            return category;
        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            var categoryToUpdate = await _context.Categories.FindAsync(category.Id);
            if (categoryToUpdate == null) return null;

            categoryToUpdate.Name = category.Name;
            categoryToUpdate.Description = category.Description;
            categoryToUpdate.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return categoryToUpdate;
        }
    }
}
