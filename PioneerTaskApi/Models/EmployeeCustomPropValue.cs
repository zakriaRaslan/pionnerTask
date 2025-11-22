namespace PioneerTaskApi.Models
{
    public class EmployeeCustomPropValue
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public int CustomPropertyId { get; set; }
        public CustomProperty? CustomProperty { get; set; }
    }
}
