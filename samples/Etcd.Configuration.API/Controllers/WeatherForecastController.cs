using Etcd.Configuration.API.ConfigOptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Etcd.Configuration.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Namespace1Options _namespace1Options;

        public WeatherForecastController(IConfiguration configuration,
            IOptionsSnapshot<Namespace1Options> namespace1Options)
        {
            _configuration = configuration;
            _namespace1Options = namespace1Options.Value;
        }


        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { _namespace1Options.Name, _namespace1Options.Company };
        }
    }
}
