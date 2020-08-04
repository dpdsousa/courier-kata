using System;
using System.Linq;

namespace CourierKata.Application
{
    public class Parcel
    {
        public int Length { get; }
        public int Width { get; }
        public int Heigth { get; }
        public double Weight { get; }
        public ParcelSize Size { get; private set; }
        public decimal Cost { get; set; }

        public Parcel(int length, int width, int heigth, double weight)
        {
            Length = length;
            Width = width;
            Heigth = heigth;
            Weight = weight;
            SetSize();
        }

        private void SetSize()
        {
            var biggestSide = new[] { Length, Width, Heigth }.Max();

            if (biggestSide < 10)
            {
                Size = ParcelSize.Small;
            }
            else if (biggestSide < 50)
            {
                Size = ParcelSize.Medium;
            }
            else if (biggestSide < 100)
            {
                Size = ParcelSize.Large;
            }
            else
            {
                Size = ParcelSize.ExtraLarge;
            }
        }
    }
}
