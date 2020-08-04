using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CourierKata.Application.UnitTests
{
    public class ParcelOrderTest
    {
        [Theory]
        [InlineData(ParcelSize.Small, 1, 1, 1)]
        [InlineData(ParcelSize.Medium, 25, 25, 25)]
        [InlineData(ParcelSize.Large, 75, 75, 75)]
        [InlineData(ParcelSize.ExtraLarge, 10, 50, 150)]
        public void ShouldCreateAParcelOrder_AndSetTheTotalCost(ParcelSize result, int length, int width, int heigth)
        {
            //Arrange
            //The test values are defined on the attribute "InlineDate"
            var testParcel = new Parcel(length, width, heigth);

            //Act
            var testParcelOrder = new ParcelOrder(testParcel);

            //Assert
            Assert.Single(testParcelOrder.Parcels);
            Assert.Equal(SizeCost.GetCost(result), testParcelOrder.TotalCost);
            Assert.All(testParcelOrder.Parcels, x =>
            {
                Assert.Equal(SizeCost.GetCost(result), x.Cost);
            });
            Assert.Null(testParcelOrder.SpeedyShippingCost);
        }

        [Fact]
        public void ShouldCreateAParcelOrderWithSeveralParcels_AndSetTheTotalCost()
        {
            var testParcels = new List<Parcel>
            {
                new Parcel(1,1,1),
                new Parcel(75,75,75),
                new Parcel(150, 150, 150)
            };

            var testParcelOrder = new ParcelOrder(testParcels);

            var expectedTotalCost = SizeCost.GetCost(ParcelSize.Small) + SizeCost.GetCost(ParcelSize.Large) + SizeCost.GetCost(ParcelSize.ExtraLarge);
            Assert.Equal(testParcels.Count(), testParcelOrder.Parcels.Count());
            Assert.Equal(expectedTotalCost, testParcelOrder.TotalCost);
            Assert.All(testParcelOrder.Parcels, x =>
            {
                Assert.Equal(SizeCost.GetCost(x.Size), x.Cost);
            });
            Assert.Null(testParcelOrder.SpeedyShippingCost);
        }

        [Fact]
        public void ShouldCreateAParcelOrderWithSpeedyShippingValueSet()
        {
            var length = 1;
            var width = 1;
            var heigth = 1;
            var testParcel = new Parcel(length, width, heigth);
            var speedyShipping = true;

            var testParcelOrder = new ParcelOrder(testParcel, speedyShipping);

            var expectedTotalCost = SizeCost.GetCost(ParcelSize.Small) * 2;
            var speedyShippingCost = expectedTotalCost / 2;
            Assert.Single(testParcelOrder.Parcels);
            Assert.Equal(expectedTotalCost, testParcelOrder.TotalCost);
            Assert.All(testParcelOrder.Parcels, x =>
            {
                Assert.Equal(SizeCost.GetCost(x.Size), x.Cost);
            });
            Assert.NotNull(testParcelOrder.SpeedyShippingCost);
            Assert.Equal(speedyShippingCost, testParcelOrder.SpeedyShippingCost);

        }
    }
}
