namespace pionnerTask.Services
{
    public interface IEmployeesService
    {
        Task<List<Employee>> GetAllAsync();
        Task Delete(Employee employee);
        Task<Employee?> GetByIdAsync(int Id);
        Task AddAsync(Employee employee);
        Task Update(Employee employee);

    }
}
