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
        private readonly Namespace1Options _options;

        public WeatherForecastController(IOptionsSnapshot<Namespace1Options> options)
        {
            _options = options.Value;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { _options.Name, _options.Company };
        }
    }
}
