using Microsoft.EntityFrameworkCore;
using PioneerTaskApi.Data;
using PioneerTaskApi.Models;
using PioneerTaskApi.Repositories.Interfaces;

namespace PioneerTaskApi.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeesRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.Include(e=>e.EmployeeCustomPropValues).ThenInclude(e=>e.CustomProperty).ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee?> GetEmployeeByIdWithPropertiesAsync(int id)
        {
            return await _context.Employees.Include(e => e.EmployeeCustomPropValues).ThenInclude(pv => pv.CustomProperty).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
              await _context.Employees.AddAsync(employee);           
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);          
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }



        
    }
}
