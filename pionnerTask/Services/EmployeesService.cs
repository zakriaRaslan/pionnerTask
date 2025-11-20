
using System.Threading.Tasks;

namespace pionnerTask.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly ApplicationDbContext _context;
        public EmployeesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync()
        {

            List<Employee> employees = await _context.Employees.Include(e => e.PropertiesValues)          
                                                       .ThenInclude(ev => ev.CustomProperty) 
                                                            .ToListAsync();
            return employees;

        }
        public async Task<Employee?> GetByIdAsync(int Id)
        {
          Employee? employee = await _context.Employees
                .Include(e => e.PropertiesValues)
                .ThenInclude(v => v.CustomProperty)
                .FirstOrDefaultAsync(e => e.Id == Id);
            return employee;
        }
   

        public async Task AddAsync(Employee employee)
        {
           await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

     

        public Task Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

      
    }
}
