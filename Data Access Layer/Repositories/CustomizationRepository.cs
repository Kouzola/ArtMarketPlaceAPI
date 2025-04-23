using Data_Access_Layer.AppDbContext;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Customization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class CustomizationRepository(ArtMarketPlaceDbContext context) : ICustomizationRepository
    {
        private readonly ArtMarketPlaceDbContext _context = context;
        public async Task<Customization> AddCustomizationAsync(Customization customization)
        {
            var addedCustomization = await _context.Customizations.AddAsync(customization);
            await _context.SaveChangesAsync();
            return addedCustomization.Entity;
        }

        public async Task<bool> DeleteCustomizationAsync(int id)
        {
            var customization = await _context.Customizations.FindAsync(id);
            if (customization == null) return false;
            _context.Customizations.Remove(customization);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Customization>> GetAllCustomizationAsync()
        {
            var customizations = await _context.Customizations.ToListAsync();
            return customizations;
        }

        public async Task<IEnumerable<Customization>> GetAllCustomizationForAProductAsync(int productId)
        {
            var customizations = await _context.Customizations.Where(c => c.ProductId == productId).ToListAsync();
            return customizations;
        }

        public async Task<Customization?> GetCustomizationByIdAsync(int id)
        {
            var customization = await _context.Customizations.FindAsync(id);
            return customization;
        }

        public async Task<Customization?> UpdateCustomizationAsync(Customization customization)
        {
            var customizationToUpdate = await _context.Customizations.FindAsync(customization.Id);
            if (customizationToUpdate == null) return null;

            customizationToUpdate.Name = customization.Name;
            customizationToUpdate.Description = customization.Description;
            customizationToUpdate.Price = customization.Price;
            customizationToUpdate.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return customizationToUpdate;
        }
    }
}
