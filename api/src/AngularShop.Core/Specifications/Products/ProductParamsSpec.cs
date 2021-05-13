namespace AngularShop.Core.Specifications.Products
{
    public class ProductParamsSpec
    {
        private const int MaxPageSize = 50;
        private const int MinPageSize = 1;

        private int _pageIndex = MinPageSize;
        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = (value < MinPageSize) ? MinPageSize : value;
        }

        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int TotalPages { get; set; }
        public int TotalItem { get; set; }

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string Sort { get; set; } = "nameAsc";

        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower().Trim();
        }
    }
}