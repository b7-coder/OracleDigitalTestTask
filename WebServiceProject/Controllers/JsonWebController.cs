using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace WebServiceProject.Controllers
{
    public class JsonWebController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public JsonWebController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public JsonResult SendJson() 
        {

            return new JsonResult(new
            {
                MaxJsonLength = Int32.MaxValue
            });
        }
    }
}
