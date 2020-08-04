using System;
using System.Collections.Generic;

namespace CourierKata.Application
{
    public static class WeightLimit
    {
        private static readonly IDictionary<ParcelSize, double> WeightLimits = new Dictionary<ParcelSize, double>()
        {
            { ParcelSize.Small, 1 },
            { ParcelSize.Medium, 3 },
            { ParcelSize.Large, 6 },
            { ParcelSize.ExtraLarge, 10 }
        };

        private const int ExcessWeightCostRate = 2;

        public static decimal GetCost(ParcelSize size, double weight)
        {
            var hasKey = WeightLimits.TryGetValue(size, out var limitWeight);
            if (!hasKey)
            {
                throw new ArgumentOutOfRangeException();
            }

            var extraCost = 0m;
            if (weight > limitWeight)
            {
                extraCost = (decimal)(weight - limitWeight) * ExcessWeightCostRate;
            }

            return extraCost;
        }
    }
}
