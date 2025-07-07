namespace Data.Entities.DatabaseDB
{
    public class MockEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public RelatedEntity? Related { get; set; }
    }
}
