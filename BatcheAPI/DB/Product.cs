namespace BatcheAPI.DB
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Weight { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual ICollection<Batch> Batches { get; set; } = new List<Batch>();
        public virtual ICollection<ProductSupplier> ProductSuppliers { get; set; } = new List<ProductSupplier>(); 
    }
}
