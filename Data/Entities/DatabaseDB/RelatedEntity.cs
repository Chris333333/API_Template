namespace Data.Entities.DatabaseDB
{
    public class RelatedEntity
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int MockEntityId { get; set; }
        public MockEntity? Mock { get; set; }
    }
}
