using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchSchool.Models;
using SearchSchool.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SearchSchool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolController : ControllerBase
    {
        private ISchoolService _schoolService;


        private readonly ILogger<SchoolController> _logger;

        public SchoolController(ILogger<SchoolController> logger, ISchoolService schoolService)
        {
            _logger = logger;
            _schoolService = schoolService;
        }

        /// <summary>
        /// Get All or based on the limit param.
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<List<School>> Get([FromQuery] int? limit)
        {
            return _schoolService.Get(limit);
        }

        /// <summary>
        /// Get Schools Near by Me(Default is 3KM radius)
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        [HttpGet("near-me")]
        public Task<List<School>> GetSchoolNearByMe([FromQuery, Required] double latitude, [FromQuery, Required] double longitude, [FromQuery] int? distance)
        {
            return _schoolService.GetSchoolFromRadius(latitude, longitude, distance == null ? 3000 : distance.GetValueOrDefault() * 1000);
        }
    }
}
