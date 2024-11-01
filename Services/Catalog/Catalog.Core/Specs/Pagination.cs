namespace Catalog.Core.Specs
{
    public class Pagination<T> where T : class
    {

        public Pagination()
        {

        }
        public Pagination(int PageIndex, int PageSize, int count, IReadOnlyList<T> data)
        {
            pageIndex = PageIndex;
            pageSize = PageSize;
            pageCount = count;
            Data = data;
        }
        private int pageIndex { get; set; }
        private int pageSize { get; set; }
        private int pageCount { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
