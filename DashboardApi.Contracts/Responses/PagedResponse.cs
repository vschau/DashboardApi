using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardApi.Contracts.Responses
{
    public class PagedResponse<T>
    {
        public PagedResponse() { }

        public PagedResponse(IEnumerable<T> data)
        {
            Data = data;
        }

        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int? PageSize { get; set; }
        public int? pageIndex { get; set; }
        public int TotalPages { get; set; }
    }
}
