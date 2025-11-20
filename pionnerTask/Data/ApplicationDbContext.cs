
namespace pionnerTask.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<EmployeePropertieValue> EmployeePropertiesValue { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CustomProperty> CustomProperties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
