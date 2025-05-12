using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Interfaces.Category
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Entities.Category>> GetAllCategoriesAsync();
        Task<Entities.Category?> GetCategoryByIdAsync(int id);
        Task<Entities.Category?> GetCategoryByNameAsync(string name);
        Task<Entities.Category> AddCategoryAsync(Entities.Category category);
        Task<Entities.Category?> UpdateCategoryAsync(Entities.Category category);
        Task<bool> DeleteCategorybyIdAsync(int id);

    }
}
