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
        // How many records are being returned
        public int TotalCount { get; set; }
        public int? PageSize { get; set; }
        // aka current page
        public int? pageIndex { get; set; }
        public int TotalPages { get; set; }
        //public string PreviousPage { get; set; }
        //public string NextPage { get; set; }
    }
}
