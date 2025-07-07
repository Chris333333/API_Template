namespace Data.Entities.DatabaseDB
{
    public class ChildEntity
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public int ParentId { get; set; }
        public ParentEntity? Parent { get; set; }
    }
}
