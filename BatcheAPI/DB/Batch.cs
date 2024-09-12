namespace BatcheAPI.DB
{
    public class Batch
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int SupplierId { get; set; }
        public int ProductId { get; set; }        
        public int Size { get; set; }
        public DateTime DateOfReceipt { get; set; } = DateTime.Now;
        public virtual Product? Product { get; set; }
        public virtual Supplier? Supplier { get; set; }
    }
}
