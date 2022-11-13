namespace FarmFresh.Framework.Models.Requests
{
    public class UpdateStoreRequest
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public Guid? LastModifiedBy { get; set; }
    }
}
