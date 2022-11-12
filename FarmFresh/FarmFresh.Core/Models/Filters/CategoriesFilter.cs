using FarmFresh.Common.Models;

namespace FarmFresh.Core.Models.Filters
{
    public class CategoriesFilter : PaginationFilter
    {
        public string OrderBy { get; set; } = "CategoryName asc"; // desc for descending order
    }
}
