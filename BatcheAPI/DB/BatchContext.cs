using Microsoft.EntityFrameworkCore;

namespace BatcheAPI.DB
{
    public class BatchContext : DbContext
    {
        public virtual DbSet<Batch> Batches { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        private readonly string? _connectionString;

        public BatchContext() { }

        public BatchContext(string? connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.None).UseLazyLoadingProxies().UseNpgsql(_connectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {

                entity.ToTable("products");
                entity.HasKey(p => p.Id)
                .HasName("product_pkey");

                entity.HasIndex(p => p.Name)
                .IsUnique();
                entity.Property(p => p.Name)
                .HasColumnName("product_name")
                .HasMaxLength(255);

                entity.Property(p => p.Weight)
                .HasColumnName("product_weight");

                entity.Property(p => p.ExpirationDate)
                .HasColumnName("expiration_date");

                entity.HasMany(p => p.Batches)
                .WithOne(b => b.Product)
                .HasForeignKey(b => b.ProductId)
                .HasConstraintName("batch_fkey");

                entity.HasMany(p => p.ProductSuppliers)
                .WithOne(ps => ps.Product)
                .HasForeignKey(ps => ps.ProductId)
                .HasConstraintName("product_supplier_fkey");

            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.ToTable("batches");

                entity.HasKey(p => p.Id)
                .HasName("batch_pkey");

                entity.HasIndex(p => p.Number)
                .IsUnique();

                entity.Property(p => p.Number)
                .HasColumnName("batch_number");
                

                entity.Property(p => p.SupplierId)
                .HasColumnName("supplier_id");

                entity.Property(p => p.ProductId)
                .HasColumnName("product_id");

                entity.Property(p => p.Size)
                .HasColumnName("size");

                entity.Property(p => p.DateOfReceipt)
                .HasColumnName("date_of_receipt");

                entity.HasOne(p => p.Product)
                .WithMany(p => p.Batches)
                .HasForeignKey(p => p.ProductId)
                .HasConstraintName("batch_product_fkey");

                entity.HasOne(p => p.Supplier)
                .WithMany(p => p.Batches)
                .HasForeignKey(p => p.SupplierId)
                .HasConstraintName("batch_supplier_fkey");

            });

            modelBuilder.Entity<Supplier>(entity =>
            {

                entity.ToTable("suppliers");

                entity.HasKey(p => p.Id)
                .HasName("supplier_pkey");

                entity.Property(p => p.Id)
                .HasColumnName("supplier_id");

                entity.Property(p => p.Name)
                .HasColumnName("name");

                entity.Property(p => p.INN)
                .HasMaxLength(12)
                .HasColumnName("INN");

                entity.HasMany(p => p.Batches)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId)
                .HasConstraintName("supplier_batch_fkey");

                entity.HasMany(p => p.ProductSuppliers)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId)
                .HasConstraintName("supplier_ps_fkey");
            });

            modelBuilder.Entity<ProductSupplier>(entity =>
            {

                entity.HasKey(p => new { p.ProductId, p.SupplierId });

                entity.HasOne(p => p.Product)
               .WithMany(p => p.ProductSuppliers)
               .HasForeignKey(p => p.ProductId)
               .HasConstraintName("product_ps_fkey");

                entity.HasOne(p => p.Supplier)
               .WithMany(p => p.ProductSuppliers)
               .HasForeignKey(p => p.SupplierId)
               .HasConstraintName("supplier_ps_fkey");

            });
        }

    }

}
