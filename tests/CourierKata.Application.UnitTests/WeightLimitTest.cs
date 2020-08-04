using System;
using Xunit;

namespace CourierKata.Application.UnitTests
{
    public class WeightLimitTest
    {
        [Theory]
        [InlineData(2 , ParcelSize.Small, 2)]
        [InlineData(4, ParcelSize.Medium, 5)]
        [InlineData(6, ParcelSize.Large, 9)]
        [InlineData(8, ParcelSize.ExtraLarge, 14)]
        public void ShoudReturnTheCostOfTheExcessWeight(decimal cost, ParcelSize key, double weight)
        {
            //Arrange
            //The test values are defined on the attribute "InlineDate"

            //Act
            var testCost = WeightLimit.GetCost(key, weight);

            //Assert
            Assert.Equal(cost, testCost);
        }

        [Theory]
        [InlineData(0, ParcelSize.Small, 1)]
        [InlineData(0, ParcelSize.Medium, 3)]
        [InlineData(0, ParcelSize.Large, 6)]
        [InlineData(0, ParcelSize.ExtraLarge, 10)]
        public void ShoudReturnZeroCost_WhenThereIsNoExcessWeight(decimal cost, ParcelSize key, double weight)
        {
            //Arrange
            //The test values are defined on the attribute "InlineDate"

            var testCost = WeightLimit.GetCost(key, weight);

            Assert.Equal(cost, testCost);
        }

        [Fact]
        public void ShoudThrowArgumentOutOfRangeException_WhenKeyDoesNotExist()
        {
            var nonExistingKey = (ParcelSize)0;

            Assert.Throws<ArgumentOutOfRangeException>(() => SizeCost.GetCost(nonExistingKey));
        }
    }
}
