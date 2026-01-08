using Microsoft.AspNetCore.Mvc;

namespace CFA_EFC_WEPAPI.Controllers
{
    // ApiController attribute indicates that this controller responds to web API requests
    // Route attribute defines the routing pattern for the controller
    // The route "[controller]" means that the controller's name (without "Controller" suffix) will be used as the base route
    // https://localhost:7008/WeatherForecast
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        // ILogger is a default interface used in .NET for logging information, warnings, errors, etc.
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // HttpGet attribute indicates that this method responds to HTTP GET requests
        // IEnumerable<WeatherForecast> indicates that the method returns a collection of WeatherForecast objects
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //Enumerable.Range generates a sequence of integers from 1 to 5 randomly
            //Iterate over each index to create a new WeatherForecast object for 5 times
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                //"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
                Summary = Summaries[Random.Shared.Next(Summaries.Length)] //10
            })
            .ToArray(); // Convert the IEnumerable to an array and return it
        }
    }
}
