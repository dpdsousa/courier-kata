using System;
using Xunit;

namespace CourierKata.Application.UnitTests
{
    public class SizeCostTest
    {
        [Theory]
        [InlineData(3, ParcelSize.Small)]
        [InlineData(8, ParcelSize.Medium)]
        [InlineData(15, ParcelSize.Large)]
        [InlineData(25, ParcelSize.ExtraLarge)]
        public void ShoudReturnTheCostCorrespondingToTheSizeKey(decimal cost, ParcelSize key)
        {
            //Arrange
            //The test values are defined on the attribute "InlineDate"

            //Act
            var testCost = SizeCost.GetCost(key);

            //Assert
            Assert.Equal(cost, testCost);
        }

        [Fact]
        public void ShoudThrowArgumentOutOfRangeException_WhenKeyDoesNotExist()
        {
            var nonExistingKey = (ParcelSize) 0;

            Assert.Throws<ArgumentOutOfRangeException>(() => SizeCost.GetCost(nonExistingKey));
        }
    }
}
