namespace BatcheAPI.DB.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Weight { get; set; }
    }
}
