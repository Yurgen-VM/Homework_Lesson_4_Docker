namespace Lesson_3_GraphQL.Models.DTO
{
    public class PostProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;       
        public double Price { get; set; }       
        public int CategoryId { get; set; }
    }
}
