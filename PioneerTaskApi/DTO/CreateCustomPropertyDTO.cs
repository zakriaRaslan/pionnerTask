using System.ComponentModel.DataAnnotations;

namespace PioneerTaskApi.DTO
{
    public class CreateCustomPropertyDTO
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }=string.Empty;
        [Required]
        public string DataType { get; set; } = string.Empty;
        public bool IsRequierd { get; set; }
        public List<string>? Options { get; set; } // Only For DropDown Type
    }
}
