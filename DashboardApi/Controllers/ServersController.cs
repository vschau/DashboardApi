using AutoMapper;
using DashboardApi.Contracts.Requests;
using DashboardApi.Contracts.Responses;
using DashboardApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly IServerRepository _serverRepository;
        private readonly IMapper _mapper;

        public ServersController(IServerRepository serverRepository, IMapper mapper)
        {
            _serverRepository = serverRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all the servers in the system
        /// </summary>
        /// <response code="200">Returns all the servers in the system</response>
        [HttpGet]
        public async Task<IActionResult> GetServers()
        {
            var servers = await _serverRepository.GetServersAsync();

            var serverResponse = _mapper.Map<List<ServerResponse>>(servers);

            //var serverResponse = servers.Select(s => new ServerResponse
            //{
            //    Id = s.Id,
            //    Name = s.Name,
            //    IsOnline = s.IsOnline
            //});

            return Ok(serverResponse);
        }

        // GET: /api/servers/2
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServerById(int id)
        {
            var server = await _serverRepository.GetServerByIdAsync(id);

            var serverResponse = _mapper.Map<ServerResponse>(server);
            //var serverResponse = new ServerResponse
            //{
            //    Id = server.Id,
            //    Name = server.Name,
            //    IsOnline = server.IsOnline
            //};

            return Ok(serverResponse);
        }

        /// <summary>
        /// Update server status in the system
        /// </summary>
        /// <response code="204">Updates serer status in the system</response>
        /// <response code="404">Unable to find server</response>
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
