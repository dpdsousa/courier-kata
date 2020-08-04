using System.Linq;

namespace CourierKata.Application
{
    public class Parcel
    {
        public int Length { get; }
        public int Width { get; }
        public int Heigth { get; }
        public ParcelSize Size { get; private set; }


        public Parcel(int length, int width, int heigth)
        {
            Length = length;
            Width = width;
            Heigth = heigth;
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
