using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CourierKata.Application.UnitTests
{
    public class ParcelOrderTest
    {
        [Theory]
        [InlineData(ParcelSize.Small, 1, 1, 1, 0)]
        [InlineData(ParcelSize.Medium, 25, 25, 25, 0)]
        [InlineData(ParcelSize.Large, 75, 75, 75, 0)]
        [InlineData(ParcelSize.ExtraLarge, 10, 50, 150, 0)]
        public void ShouldCreateAParcelOrderAndSetTheTotalCost(ParcelSize result, int length, int width, int heigth, double weigth)
        {
            //Arrange
            //The test values are defined on the attribute "InlineDate"
            var testParcel = new Parcel(length, width, heigth, weigth);

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
        public void ShouldCreateAParcelOrderWithSeveralParcelsAndSetTheTotalCost()
        {
            var testParcels = new List<Parcel>
            {
                new Parcel(1, 1, 1, 0),
                new Parcel(75, 75, 75, 0),
                new Parcel(150, 150, 150, 0)
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
            var weight = 0;
            var testParcel = new Parcel(length, width, heigth, weight);
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

        [Fact]
        public void ShouldCreateAParcelOrderAndSetTheTotalCostWithExtraWeight()
        {
            var length = 1;
            var width = 1;
            var heigth = 1;
            var weight = 6;
            var testParcel = new Parcel(length, width, heigth, weight);

            var testParcelOrder = new ParcelOrder(testParcel);

            var expectedTotalCost = SizeCost.GetCost(ParcelSize.Small) + WeightLimit.GetCost(ParcelSize.Small, weight);

            Assert.Single(testParcelOrder.Parcels);
            Assert.Equal(expectedTotalCost, testParcelOrder.TotalCost);
            Assert.All(testParcelOrder.Parcels, x =>
            {
                Assert.Equal(expectedTotalCost, x.Cost);
            });
            Assert.Null(testParcelOrder.SpeedyShippingCost);
        }

        [Fact]
        public void ShouldCreateAParcelOrderAndSetTheTotalForAHeavyParcel()
        {
            var length = 1;
            var width = 1;
            var heigth = 1;
            var weight = 100d;
            var testParcel = new Parcel(length, width, heigth, weight);

            var testParcelOrder = new ParcelOrder(testParcel);

            var expectedTotalCost = WeightLimit.GetHeavyParcelCost(weight);

            Assert.Single(testParcelOrder.Parcels);
            Assert.Equal(expectedTotalCost, testParcelOrder.TotalCost);
            Assert.All(testParcelOrder.Parcels, x =>
            {
                Assert.Equal(expectedTotalCost, x.Cost);
            });
            Assert.Null(testParcelOrder.SpeedyShippingCost);
        }
    }
}
