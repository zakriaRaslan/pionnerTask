

namespace pionnerTask.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [MaxLength(40)]
        public string Name { get; set; } =  string.Empty;
        public int Code { get; set; }
        public ICollection<EmployeePropertieValue>? PropertiesValues { get; set; }
    }
}
