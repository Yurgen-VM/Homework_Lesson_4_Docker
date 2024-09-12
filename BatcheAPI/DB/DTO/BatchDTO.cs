namespace BatcheAPI.DB.DTO
{
    public class BatchDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public int Size { get; set; }
        public DateTime DateOfReceipt { get; set; }
    }
}
