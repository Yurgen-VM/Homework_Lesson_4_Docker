namespace Lesson_3_GraphQL.Models
{
    public class Storehouse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;             
        public virtual ICollection<Storage> Storages { get; set; } = new List<Storage>();
    }
}
