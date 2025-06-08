using Business_Layer.Exceptions;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class CategoryService(ICategoryRepository repository) : ICategoryService
    {
        private readonly ICategoryRepository _repository = repository;

        public async Task<Category> AddCategoryAsync(Category category)
        {
            var existingCategory = await _repository.GetCategoryByNameAsync(category.Name);
            if (existingCategory != null) throw new AlreadyExistException("This category already exists!");
            return await _repository.AddCategoryAsync(category);
        }

        public async Task<bool> DeleteCategorybyIdAsync(int id)
        {
            return await _repository.DeleteCategorybyIdAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _repository.GetAllCategoriesAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _repository.GetCategoryByIdAsync(id);
            if (category == null) throw new NotFoundException("Category not found!");
            return category;
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            var category = await _repository.GetCategoryByNameAsync(name);
            if (category == null) throw new NotFoundException("Category not found!");
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var categoryUpdated = await _repository.UpdateCategoryAsync(category);
            if (categoryUpdated == null) throw new NotFoundException("Category not found!");

            var existingCategory = await _repository.GetCategoryByNameAsync(category.Name);
            if (existingCategory != null && existingCategory.Id != category.Id) throw new AlreadyExistException("This category already exists!");

            return categoryUpdated;
        }
    }
}
