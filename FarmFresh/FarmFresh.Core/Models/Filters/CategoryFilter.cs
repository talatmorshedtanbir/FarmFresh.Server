using FarmFresh.Common.Models;

namespace FarmFresh.Core.Models.Filters
{
    public class CategoryFilter : PaginationFilter
    {
        public string OrderBy { get; set; } = "CategoryName asc";
    }
}
