using System.ComponentModel.DataAnnotations;

namespace PioneerTaskApi.DTO
{
    public class CreateEmployeeDTO
    {
        [Required]
        public int Code { get; set; }
        [MaxLength(40)]
        public string Name { get; set; } = string.Empty;
        public Dictionary<int, string>? CustomProperties { get; set; }
    }
}
