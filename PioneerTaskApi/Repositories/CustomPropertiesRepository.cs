using Microsoft.EntityFrameworkCore;
using PioneerTaskApi.Data;
using PioneerTaskApi.Models;
using PioneerTaskApi.Repositories.Interfaces;

namespace PioneerTaskApi.Repositories
{
    public class CustomPropertiesRepository : ICustomPropertiesRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomPropertiesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomProperty>> GetAllCustomPropertiesAsync()
        {
            return await _context.CustomProperties.AsNoTracking().ToListAsync();
        }
        public async Task<CustomProperty?> GetCustomPropertyByIdAsync(int id)
        {
            return await _context.CustomProperties.FindAsync(id);
        }

        public async Task<CustomProperty?> GetCustomPropertyByIdWithPropValuesAsync(int id)
        {
            return await _context.CustomProperties
                .Include(cp => cp.EmployeeCustomPropValues)
                .FirstOrDefaultAsync(cp => cp.Id == id);
        }


        public async Task CreateCustomPropertyAsync(CustomProperty customProperty)
        {
            await _context.CustomProperties.AddAsync(customProperty);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomPropertyAsync(CustomProperty customProperty)
        {
            _context.CustomProperties.Update(customProperty);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCustomPropertyAsync(CustomProperty customProperty)
        {
           _context.CustomProperties.Remove(customProperty);
            await _context.SaveChangesAsync();
        }


   
    }
}
