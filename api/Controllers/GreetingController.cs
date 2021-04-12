using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreetingController : ControllerBase
    {
        [HttpGet]
        public String Get(string name)
        {
            Log.Logger.Information(name + " is asking for a greeting");
            return $"Well hello there, {name}! This greeting comes from the {GetNameFromEnv()} environment";
        }

        private string GetNameFromEnv() {
            return Environment.GetEnvironmentVariable("EnvironmentName") ?? "Unknown";
        }
    }
}
