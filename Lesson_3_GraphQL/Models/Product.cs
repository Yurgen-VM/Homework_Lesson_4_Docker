namespace Lesson_3_GraphQL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<Storage> Storage { get; set; } = new List<Storage>();
    }
}
