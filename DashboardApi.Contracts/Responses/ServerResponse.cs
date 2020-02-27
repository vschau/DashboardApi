using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardApi.Contracts.Responses
{
    public class ServerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOnline { get; set; }
    }
}
