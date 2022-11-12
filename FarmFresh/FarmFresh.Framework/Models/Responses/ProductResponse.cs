namespace FarmFresh.Framework.Models.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string KeyInformation { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageBase64 { get; set; }

        public string Country { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

        public bool IsActive { get; set; }

        public IList<CategoryResponse> Categories { get; set; }
    }
}
