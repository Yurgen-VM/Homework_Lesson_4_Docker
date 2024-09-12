namespace Lesson_3_GraphQL.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StorehouseId { get; set; }
        public int Quantity { get; set; }
        public int Number { get; set; }
        public int Section { get; set; }
        public virtual Product? Products { get; set; }
        public virtual Storehouse? Storehouse { get; set; }        
    }
}
