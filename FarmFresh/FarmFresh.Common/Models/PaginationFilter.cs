using System.ComponentModel.DataAnnotations;

namespace FarmFresh.Common.Models
{
    public class PaginationFilter
    {
        public string SearchText { get; set; } = string.Empty;

        const int maxPageSize = 50;

        private int _pageSize = 10;

        [Range(1, UInt16.MaxValue, ErrorMessage = "PageNumber cannot be zero/negative or more than 65535")]
        public int PageNumber { get; set; } = 1;

        [Range(1, UInt16.MaxValue, ErrorMessage = "PageSize cannot be zero/negative or more than 65535")]
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
