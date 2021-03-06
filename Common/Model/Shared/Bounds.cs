using System;
namespace Common.Model.Shared
{
    public struct Bounds
    {
        public int Height { get; set; }

        public int Width { get; set; }

        public double Diameter => Math.Sqrt(Math.Pow(Height, 2) + Math.Pow(Width, 2));

        public double Radius => Diameter / 2;

        public Bounds(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
