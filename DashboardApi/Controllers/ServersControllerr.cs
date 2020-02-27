using DashboardApi.Contracts.Requests;
using DashboardApi.Contracts.Responses;
using DashboardApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly IServerRepository _serverRepository;

        public ServersController(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetServers()
        {
            var servers = await _serverRepository.GetServersAsync();

            var serverResponse = servers.Select(s => new ServerResponse
            {
                Id = s.Id,
                Name = s.Name,
                IsOnline = s.IsOnline
            });

            return Ok(serverResponse);
        }

        // GET: /api/servers/2
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServerById(int id)
        {
            var server = await _serverRepository.GetServerByIdAsync(id);

            var serverResponse = new ServerResponse
            {
                Id = server.Id,
                Name = server.Name,
                IsOnline = server.IsOnline
            };

            return Ok(serverResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServerStatus(int id, [FromBody] ServerRequest serverRequest)
        {
            var updated = await _serverRepository.UpdateServerStatusAsync(id, serverRequest);
            if (updated)
                return NoContent();

            return NotFound();
        }
    }
}
