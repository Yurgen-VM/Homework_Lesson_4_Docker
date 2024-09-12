using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Lesson_3_GraphQL.Models
{

    // "Host = localhost; Username = postgres; Password = example; Database = MarketDB;"

    public class MarketContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
        public virtual DbSet<Storehouse> Storehouse { get; set; }

        private readonly string? _connectionString;

        public MarketContext() { }

        public MarketContext(string? connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.None).UseLazyLoadingProxies().UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.HasKey(e => e.Id).HasName("product_pkey");
                entity.HasIndex(e => e.Name).IsUnique();

                entity.Property(e => e.Name)
                .HasColumnName("product_name")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(1024);

                entity.Property(e => e.Price)
                .HasColumnName("price")
                .IsRequired();

                entity.HasOne(e => e.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId)
                .HasConstraintName("category_id_fk");

                entity.HasMany(p => p.Storage)
                .WithOne(p => p.Products)
                .HasForeignKey(p => p.ProductId)
                .HasConstraintName("storages_fk");

            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("category_pkey");

                entity.ToTable("category_of_product");

                entity.HasIndex(x => x.Name)
                .IsUnique();

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.Property(e => e.Name)
                .HasColumnName("category_name")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(255);

                entity.HasMany(e => e.Products)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .HasConstraintName("products_fk");

            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("store_pkey");

                entity.ToTable("storage");

                entity.Property(e => e.Id)
                .HasColumnName("storage_id");
                 
                entity.Property(e => e.Number)
                .HasColumnType("int")
                .HasColumnName("number");

                entity.Property(e => e.Section)
                .HasColumnType("int")
                .HasColumnName("section");

                entity.Property(e => e.ProductId)
                .HasColumnName("product_id");

                entity.Property(e => e.Quantity)
                .HasColumnName("quantity");               
        
                entity.HasOne(e => e.Products)
                .WithMany(p => p.Storage)
                .HasForeignKey(p => p.ProductId)
                .HasConstraintName("product_fk");
            });

            modelBuilder.Entity<Storehouse>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("storehouse_pkey");

                entity.ToTable("storehouses");

                entity.Property(e => e.Id)
                .HasColumnName("storehouse_id");

                entity.Property(e => e.Name)
                .HasColumnName("name");

                entity.HasMany(e => e.Storages)
                .WithOne(e => e.Storehouse)
                .HasForeignKey(k => k.StorehouseId)
                .HasConstraintName("storage_fkey");
            });
        }
    }
}
