namespace DashboardApi.Contracts.Requests.Queries
{
    public class PaginationQuery
    {
        public PaginationQuery()
        {
            Filter = "";
            SortColumn = "id";
            SortDirection = "asc";
            PageIndex = 1;
            PageSize = 5;
        }

        public PaginationQuery(string filter, string sortColumn, string sortDirection, int pageIndex, int pageSize)
        {
            Filter = filter;
            SortColumn = sortColumn;
            SortDirection = sortDirection;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public string Filter { get; set; } = "";
        public string SortColumn { get; set; } = "Id";
        public string SortDirection { get; set; } = "asc";
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
