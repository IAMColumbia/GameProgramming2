using System;

namespace WebApplicationGMWeb
{
    public class DTO
    {
    }


    public class Rootobject
    {
        public Sprite[] sprites { get; set; }
    }

    public class Sprite
    {
        public Location Location { get; set; }
        public Direction Direction { get; set; }
        public float Speed { get; set; }
    }

    public class Location
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    public class Direction
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

}
