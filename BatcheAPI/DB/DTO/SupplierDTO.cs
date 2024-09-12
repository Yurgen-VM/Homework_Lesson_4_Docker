namespace BatcheAPI.DB.DTO
{
    public class SupplierDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? INN { get; set; }
    }
}
