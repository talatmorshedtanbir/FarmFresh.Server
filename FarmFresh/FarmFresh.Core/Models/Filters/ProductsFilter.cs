using FarmFresh.Common.Models;

namespace FarmFresh.Core.Models.Filters
{
    public class ProductsFilter : PaginationFilter
    {
        public string OrderBy { get; set; } = "Title asc"; // desc for descending order
        public long CategoryId { get; set; } = 0;
    }
}
