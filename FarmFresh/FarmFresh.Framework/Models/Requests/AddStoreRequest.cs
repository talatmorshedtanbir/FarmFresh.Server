namespace FarmFresh.Framework.Models.Requests
{
    public class AddStoreRequest
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public Guid? CreatedBy { get; set; }
    }
}
