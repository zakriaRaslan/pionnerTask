
namespace pionnerTask.Services
{
    public class PropertiesService : IPropertiesSevice
    {
        private readonly ApplicationDbContext _context;
        public PropertiesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomProperty>> GetAllAsync()
        {
            List<CustomProperty> customProperties = await _context.CustomProperties.AsNoTracking().ToListAsync();
            return customProperties;
        }

        public async Task<CustomProperty> GetByIdAsync(int Id)
        {
           CustomProperty? property = await _context.CustomProperties.AsNoTracking().FirstOrDefaultAsync(p => p.Id == Id);
             
           return property;
        }
     

        public async Task UpdateAsync(CustomProperty customPropertie)
        {
            _context.Update(customPropertie);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int Id)
        {
            CustomProperty property = _context.CustomProperties.FirstOrDefault(p => p.Id == Id)!;
            _context.Remove(property);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task AddAsync(CustomProperty customPropertie)
        {
            await _context.AddAsync(customPropertie);
            await _context.SaveChangesAsync();
        }
    }
}
