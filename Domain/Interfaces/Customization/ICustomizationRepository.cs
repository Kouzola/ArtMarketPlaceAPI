using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Interfaces.Customization
{
    public interface ICustomizationRepository
    {
        Task<IEnumerable<Entities.Customization>> GetAllCustomizationAsync();
        Task<IEnumerable<Entities.Customization>> GetAllCustomizationForAProductAsync(int productId);
        Task<Entities.Customization?> GetCustomizationByIdAsync(int id);
        Task<Entities.Customization> AddCustomizationAsync(Entities.Customization customization);
        Task<Entities.Customization?> UpdateCustomizationAsync(Entities.Customization customization);
        Task<bool> DeleteCustomizationAsync(int id);
    }
}
