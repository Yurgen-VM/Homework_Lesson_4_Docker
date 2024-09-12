namespace Market.Models.DTO
{
    public class StorageModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

    }
}
