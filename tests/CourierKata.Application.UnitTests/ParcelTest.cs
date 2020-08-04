using System;
using Xunit;

namespace CourierKata.Application.UnitTests
{
    public class ParcelTest
    {
        [Theory]
        [InlineData(ParcelSize.Small, 1, 1, 1, 0)]
        [InlineData(ParcelSize.Medium, 25, 25, 25, 0)]
        [InlineData(ParcelSize.Large, 75, 75, 75, 0)]
        [InlineData(ParcelSize.ExtraLarge, 10, 50, 150, 0)]
        public void ShouldCreateAParcelAndSetTheSpecificSize_DependingOnTheMeasurements(ParcelSize result, int length, int width, int heigth, double weight)
        {
            //Arrange
            //The test values are defined on the attribute "InlineDate"

            //Act
            var newParcel = new Parcel(length, width, heigth, weight);

            //Assert
            Assert.Equal(result, newParcel.Size);
            Assert.Equal(length, newParcel.Length);
            Assert.Equal(width, newParcel.Width);
            Assert.Equal(heigth, newParcel.Heigth);
            Assert.Equal(weight, newParcel.Weight);
        }
    }
}
