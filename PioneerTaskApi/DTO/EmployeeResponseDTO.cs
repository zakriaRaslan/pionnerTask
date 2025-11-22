namespace PioneerTaskApi.DTO
{
    public class EmployeeResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Code { get; set; }
        public Dictionary<int, string>? CustomProperties { get; set; }
    }
}
