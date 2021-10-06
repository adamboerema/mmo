using System;
namespace Common.Model.Character
{
    public struct Bounds
    {
        public int Height { get; set; }

        public int Width { get; set; }

        public double Horizontal => Math.Sqrt(Math.Pow(Height, 2) + Math.Pow(Width, 2));

        public Bounds(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
