using System.ComponentModel.DataAnnotations;

namespace pionnerTask.Models
{
    public class EmployeePropertieValue
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public string? Value { get; set; }
        public CustomProperty? CustomProperty { get; set; } 
        public int CustomPropertyId { get; set; }
    }
}
