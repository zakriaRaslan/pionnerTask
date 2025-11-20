namespace pionnerTask.Services
{
    public interface IPropertiesSevice
    {
        Task<List<CustomProperty>> GetAllAsync();
        Task Delete(int Id);
        Task<CustomProperty> GetByIdAsync(int Id);
        Task AddAsync(CustomProperty customPropertie);
        Task  UpdateAsync(CustomProperty customPropertie);
    }
}
