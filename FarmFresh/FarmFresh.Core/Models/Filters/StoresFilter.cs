using FarmFresh.Common.Models;

namespace FarmFresh.Core.Models.Filters
{
    public class StoresFilter : PaginationFilter
    {
        public string OrderBy { get; set; } = "Name asc"; // desc for descending order
    }
}
