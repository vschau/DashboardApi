using DashboardApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DashboardApi.Contracts.Requests;

namespace DashboardApi.Repositories
{
    public class SQLServerRepository: IServerRepository
    {
        private DashboardContext _context;

        public SQLServerRepository(DashboardContext context)
        {
            _context = context;
        }

        public async Task<List<Server>> GetServersAsync()
        {
            return await _context.Servers.ToListAsync();
        }

        public async Task<Server> GetServerByIdAsync(int id)
        {
            return await _context.Servers.FindAsync(id);
        }

        public async Task<bool> UpdateServerStatusAsync(int id, ServerRequest serverRequest)
        {
            var server = await GetServerByIdAsync(id);
            if (server == null)
                return false;

            if (serverRequest.PayLoad.ToLower() == "activate")
            {
                server.IsOnline = true;
            }
            else if (serverRequest.PayLoad.ToLower() == "deactivate")
            {
                server.IsOnline = false;
            }

            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }
    }
}
