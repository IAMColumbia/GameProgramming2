using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplicationGMWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GhostController : ControllerBase
    {

        static List<Sprite> sprites;

        private readonly ILogger<GhostController> _logger;

        public GhostController(ILogger<GhostController> logger)
        {
            _logger = logger;
            sprites = new List<Sprite>()
            {
                new Sprite(){
                    Direction = new Direction()
                    {
                         X = 0, Y = 0
                    },
                    Location = new Location()
                    {
                         X = 1, Y = 3
                    },
                    Speed = 0
                },
                new Sprite(){
                    Direction = new Direction()
                    {
                         X = 0, Y = 2 
                    },
                    Location = new Location()
                    {
                         X = 2, Y = 2
                    },
                    Speed = 0
                },
                new Sprite(){
                    Direction = new Direction()
                    {
                         X = 0, Y = 0
                    },
                    Location = new Location()
                    {
                         X = 3, Y = 1
                    },
                    Speed = 0
                }
                ,
                new Sprite(){
                    Direction = new Direction()
                    {
                         X = 4, Y = 1
                    },
                    Location = new Location()
                    {
                         X = 0, Y = 1
                    },
                    Speed = 0
                }
            };
        }


        [HttpGet]
        public Rootobject Get()
        {
            Rootobject ro = new Rootobject();
            ro.sprites = sprites.ToArray();
            return ro;
        }
    }
}