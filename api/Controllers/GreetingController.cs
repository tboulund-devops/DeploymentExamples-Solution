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
            // Bad approach: We are not creating an indexed field
            // Log.Logger.Information(name + " is asking for a greeting");
            
            // Better approach
            Log.Logger.Information("{Name} is asking for a greeting", new { Name = name });
            return $"Well hello there, {name}! This greeting comes from the {GetNameFromEnv()} environment";
        }

        private string GetNameFromEnv() {
            return Environment.GetEnvironmentVariable("EnvironmentName") ?? "Unknown";
        }
    }
}
