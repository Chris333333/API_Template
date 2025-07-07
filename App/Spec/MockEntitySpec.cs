using Data.Entities.DatabaseDB;

namespace App.Spec
{
    public class MockEntitySpec : BaseSpec<MockEntity>
    {
        // Constructor for fetching a single entity by ID
        public MockEntitySpec(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Related);
        }
        // Constructor for fetching a paginated list of entities
        public MockEntitySpec(BaseSpecParams specParams) : base()
        {
            AddInclude(x => x.Related);
            ApplyPaging(specParams.PageIndex, specParams.PageSize);
        }
    }
}