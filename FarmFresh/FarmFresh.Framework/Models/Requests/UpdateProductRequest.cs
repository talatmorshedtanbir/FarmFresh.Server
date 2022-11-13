namespace FarmFresh.Framework.Models.Requests
{
    public class UpdateProductRequest
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string KeyInformation { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageBase64 { get; set; }

        public string Country { get; set; }

        public DateTime? LastModified { get; set; }

        public IList<long> Categories { get; set; }
    }
}
