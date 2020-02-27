using DashboardApi.Contracts.Requests;
using DashboardApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardApi.Repositories
{
    public interface IServerRepository
    {
        Task<List<Server>> GetServersAsync();
        Task<Server> GetServerByIdAsync(int id);
        Task<bool> UpdateServerStatusAsync(int id, ServerRequest serverRequest);
    }
}
