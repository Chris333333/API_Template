namespace Data.Entities.DatabaseDB
{
    public class ParentEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public ICollection<ChildEntity> Children { get; set; } = new List<ChildEntity>();
    }
}
