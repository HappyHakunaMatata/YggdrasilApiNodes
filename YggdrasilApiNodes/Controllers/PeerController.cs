using System;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using YggdrasilApiNodes.Interfaces;
using YggdrasilApiNodes.Models;
using YggdrasilApiNodes.Services;
using YggdrasilApiNodes.Services.Adapter;

namespace YggdrasilApiNodes.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PeerController : ControllerBase 
    {

        private readonly ILogger<PeerController> _logger;
        private readonly AppDbContext _context;


        public PeerController(ILogger<PeerController> logger, AppDbContext context)
		{
            _context = context;
            _logger = logger;
        }
        
        [HttpGet(Name = "GetPeers")]
        public IActionResult GetPeers([SwaggerParameter("Markup language: \njson - 0\n xml - 1\n Default: json")] ApiFormat format = ApiFormat.json)
        {
            IAdaptee json = new JsonAdaptee();
            if (format == ApiFormat.xml)
            {
                json = new XMLAdaptee();
            }
            var adapter = new Adapter(json);
            try
            {
                var result = _context.peer.
                    Include(n => n.ipAddresses).Include(n =>
                    n.location).ThenInclude(n => n!.country).ToList();
                return adapter.GetPeers(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                var exception = new ExceptionModel()
                {
                    Domain = 400,
                    Location = "One or more validation errors occurred.",
                    Info = "Inner exception.",
                    Online = DateTime.Now,
                };
                return adapter.Exception(exception);
            }
        }

        [HttpGet(Name = "GetPeerByID")]
        public IActionResult GetPeerByID(int ID, [SwaggerParameter("Markup language: \njson - 0\n xml - 1\n Default: json")] ApiFormat format = ApiFormat.json)
        {
            IAdaptee json = new JsonAdaptee();
            if (format == ApiFormat.xml)
            {
                json = new XMLAdaptee();
            }
            var adapter = new Adapter(json);
            try
            {
                var result = _context.peer.Where(n => n.Id == ID).
                    Include(n => n.ipAddresses).Include(n =>
                    n.location).ThenInclude(n => n!.country).ToList();
                return adapter.GetPeer(result.FirstOrDefault());
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                var exception = new ExceptionModel()
                {
                    Domain = 400,
                    Location = "One or more validation errors occurred.",
                    Info = "Inner exception.",
                    Online = DateTime.Now,
                };
                return adapter.Exception(exception);
            }
        }

        [HttpGet(Name = "GetPeerByLastOnline")]
        public IActionResult GetPeerByLastOnline(DateTime dateTime, [SwaggerParameter("Markup language: \njson - 0\n xml - 1\n Default: json")] ApiFormat format = ApiFormat.json)
        {
            IAdaptee json = new JsonAdaptee();
            if (format == ApiFormat.xml)
            {
                json = new XMLAdaptee();
            }
            var adapter = new Adapter(json);
            try
            {
                var result = _context.peer.Where(n => n.LastOnline >= dateTime).
                    Include(n => n.ipAddresses).Include(n =>
                    n.location).ThenInclude(n => n!.country).ToList();
                return adapter.GetPeers(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                var exception = new ExceptionModel()
                {
                    Domain = 400,
                    Location = "One or more validation errors occurred.",
                    Info = "Inner exception.",
                    Online = DateTime.Now,
                };
                return adapter.Exception(exception);
            }
        }

        
        [HttpGet(Name = "GetPeerByStatus")]
        public IActionResult GetPeerByStatus([SwaggerParameter("Minimun uptime for peer (%)")] Status status, [SwaggerParameter("Markup language: \njson - 0\n xml - 1\n Default: json")] ApiFormat format = ApiFormat.json)
        {
            IAdaptee json = new JsonAdaptee();
            if (format == ApiFormat.xml)
            {
                json = new XMLAdaptee();
            }
            var adapter = new Adapter(json);
            try
            {
                var bd = _context.peer.
                    Include(n => n.ipAddresses).Include(n =>
                    n.location).ThenInclude(n => n!.country).ToList();
                var result = bd.Select(n => n).Where(n => n.GetUptime() == status.ToString()).ToList();
                return adapter.GetPeers(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                var exception = new ExceptionModel()
                {
                    Domain = 400,
                    Location = "One or more validation errors occurred.",
                    Info = "Inner exception.",
                    Online = DateTime.Now,
                };
                return adapter.Exception(exception);
            }
        }

        [HttpGet(Name = "GetNodesByCountry")]
        public IActionResult GetNodesByCountry(string country, [SwaggerParameter("Markup language: \njson - 0\n xml - 1\n Default: json")] ApiFormat format = ApiFormat.json)
        {
            IAdaptee json = new JsonAdaptee();
            if (format == ApiFormat.xml)
            {
                json = new XMLAdaptee();
            }
            var adapter = new Adapter(json);
            country = country.ToLower();
            try
            {
                var result = _context.peer.Where(n => n.location != null && n.location.country != null && n.location.country.Name == country).
                    Include(n => n.ipAddresses).Include(n =>
                    n.location).ThenInclude(n => n!.country).ToList();
                return adapter.GetPeers(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                var exception = new ExceptionModel()
                {
                    Domain = 400,
                    Location = "One or more validation errors occurred.",
                    Info = "Inner exception.",
                    Online = DateTime.Now,
                };
                return adapter.Exception(exception);
            }
        }

        [HttpGet(Name = "GetCountries")]
        public IActionResult GetCountries([SwaggerParameter("Markup language: \njson - 0\n xml - 1\n Default: json")] ApiFormat format = ApiFormat.json)
        {
            IAdaptee json = new JsonAdaptee();
            if (format == ApiFormat.xml)
            {
                json = new XMLAdaptee();
            }
            var adapter = new Adapter(json);
            try
            {
                var result = _context.countries.GroupBy(n => n.Name).Select(n => n.First()).ToList();
                return adapter.GetCountries(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                var exception = new ExceptionModel()
                {
                    Domain = 400,
                    Location = "One or more validation errors occurred.",
                    Info = "Inner exception.",
                    Online = DateTime.Now,
                };
                return adapter.Exception(exception);
            }
        }

        [HttpGet(Name = "GetLocations")]
        public IActionResult GetLocations([SwaggerParameter("Markup language: \njson - 0\n xml - 1\n Default: json")] ApiFormat format = ApiFormat.json)
        {
            IAdaptee json = new JsonAdaptee();
            if (format == ApiFormat.xml)
            {
                json = new XMLAdaptee();
            }
            var adapter = new Adapter(json);
            try
            {
                var result = _context.locations.Select(n => n.location).Distinct().ToList();
                return adapter.GetLocations(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                var exception = new ExceptionModel()
                {
                    Domain = 400,
                    Location = "One or more validation errors occurred.",
                    Info = "Inner exception.",
                    Online = DateTime.Now,
                };
                return adapter.Exception(exception);
            }
        }


        [HttpGet(Name = "GetPeerByLocation")]
        public IActionResult GetPeerByLocation(string location, [SwaggerParameter("Markup language: \njson - 0\n xml - 1\n Default: json")] ApiFormat format = ApiFormat.json)
        {
            IAdaptee json = new JsonAdaptee();
            if (format == ApiFormat.xml)
            {
                json = new XMLAdaptee();
            }
            var adapter = new Adapter(json);
            location = location.ToLower();
            try
            {
                var result = _context.peer.Where(n => n.location != null && !string.IsNullOrEmpty(n.location.location) && n.location.location == location).
                    Include(n => n.ipAddresses).Include(n =>
                    n.location).ThenInclude(n => n!.country).ToList();
                return adapter.GetPeers(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                var exception = new ExceptionModel()
                {
                    Domain = 400,
                    Location = "One or more validation errors occurred.",
                    Info = "Inner exception.",
                    Online = DateTime.Now,
                };
                return adapter.Exception(exception);
            }
        }

        [HttpGet(Name = "GetIpAddresses")]
        public IActionResult GetIpAddresses(string ip, [SwaggerParameter("Markup language: \njson - 0\n xml - 1\n Default: json")] ApiFormat format = ApiFormat.json)
        {
            IAdaptee json = new JsonAdaptee();
            if (format == ApiFormat.xml)
            {
                json = new XMLAdaptee();
            }
            var adapter = new Adapter(json);
            try
            {
                var result = _context.peer.Include(n => n.ipAddresses).Where(n => n.hostAddress == ip).ToList();
                return adapter.GetIpAddresses(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                var exception = new ExceptionModel()
                {
                    Domain = 400,
                    Location = "One or more validation errors occurred.",
                    Info = "Inner exception.",
                    Online = DateTime.Now,
                };
                return adapter.Exception(exception);
            }
        }
    }
}

