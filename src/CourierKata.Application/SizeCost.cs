using System;
using System.Collections.Generic;

namespace CourierKata.Application
{
    public static class SizeCost
    {
        private static readonly IDictionary<ParcelSize, decimal> SizeCosts = new Dictionary<ParcelSize, decimal>()
        {
            { ParcelSize.Small, 3 },
            { ParcelSize.Medium, 8 },
            { ParcelSize.Large, 15 },
            { ParcelSize.ExtraLarge, 25 }
        };

        public static decimal GetCost(ParcelSize size)
        {
            var hasKey = SizeCosts.TryGetValue(size, out var cost);
            if (!hasKey)
            {
                throw new ArgumentOutOfRangeException();
            }

            return cost;
        }
    }
}
