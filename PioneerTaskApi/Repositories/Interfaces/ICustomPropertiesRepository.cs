using PioneerTaskApi.Models;

namespace PioneerTaskApi.Repositories.Interfaces
{
    public interface ICustomPropertiesRepository
    {
        Task<List<CustomProperty>> GetAllCustomPropertiesAsync();
        Task<CustomProperty?> GetCustomPropertyByIdAsync(int id);
        Task<CustomProperty?> GetCustomPropertyByIdWithPropValuesAsync(int id);
        Task CreateCustomPropertyAsync(CustomProperty customProperty);
        Task UpdateCustomPropertyAsync(CustomProperty customProperty);
        Task DeleteCustomPropertyAsync(CustomProperty customProperty);

    }
}
