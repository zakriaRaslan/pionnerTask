using System.ComponentModel.DataAnnotations;

namespace PioneerTaskApi.Models
{
    public class CustomProperty
    {
        public int Id { get; set; }
        [MaxLength(40)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(40)]
        public string DataType { get; set; } = string.Empty;
        public bool IsRequired { get; set; }
        public List<string>? Options { get; set; } // Only For dropdown Data Type
        public ICollection<EmployeeCustomPropValue>? EmployeeCustomPropValues { get; set; }
    }
}
