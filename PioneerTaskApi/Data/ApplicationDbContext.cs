using Microsoft.EntityFrameworkCore;
using PioneerTaskApi.Models;

namespace PioneerTaskApi.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CustomProperty> CustomProperties { get; set; }
        public DbSet<EmployeeCustomPropValue> EmployeeCustomPropValues { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
       

    }
}
