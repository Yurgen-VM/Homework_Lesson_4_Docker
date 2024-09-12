namespace BatcheAPI.DB
{
    public class Supplier
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? INN { get; set; }
        public virtual ICollection<Batch> Batches { get; set; } = new List<Batch>();
        public virtual ICollection <ProductSupplier> ProductSuppliers { get; set; } = new List<ProductSupplier>();
    }
}
