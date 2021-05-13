using System.Collections.Generic;

namespace AngularShop.Application.Page
{
    public class Pagination<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItemsPage { get; set; }
        public int TotalItems { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}