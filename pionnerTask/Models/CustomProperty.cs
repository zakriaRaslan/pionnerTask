namespace pionnerTask.Models
{
    public class CustomProperty
    {
        public int Id { get; set; }
        [MaxLength(40)]
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsRequired { get; set; } = false;
        public List<string>? Options { get; set; }
        public ICollection<EmployeePropertieValue>? EmployeePropertieValues { get; set; }
    }
}
