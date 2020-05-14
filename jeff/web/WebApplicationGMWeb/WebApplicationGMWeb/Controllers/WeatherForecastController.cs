using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplicationGMWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        static List<Sprite> sprites;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;

            sprites = new List<Sprite>()
            {
                new Sprite(){
                    Direction = new Direction()
                    {
                         X = 1, Y = 3
                    },
                    Location = new Location()
                    {
                         X = 0, Y = 0
                    },
                    Speed = 0
                },
                new Sprite(){
                    Direction = new Direction()
                    {
                         X = 2, Y = 2
                    },
                    Location = new Location()
                    {
                         X = 0, Y = 0
                    },
                    Speed = 0
                },
                new Sprite(){
                    Direction = new Direction()
                    {
                         X = 3, Y = 1
                    },
                    Location = new Location()
                    {
                         X = 0, Y = 0
                    },
                    Speed = 0
                },
            };
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
