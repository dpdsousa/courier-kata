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

        private const int HeavyParcelExcessWeightCostRate = 1;
        private const double HeavyParcelLimitWeight = 50;
        private const decimal HeavyParcelDefaultCost = 50;


        public static decimal GetCost(ParcelSize size, double weight)
        {
            var limitWeight = GetSizeLimit(size);

            var extraCost = 0m;
            if (weight > limitWeight)
            {
                extraCost = (decimal)(weight - limitWeight) * ExcessWeightCostRate;
            }

            return extraCost;
        }


        public static decimal GetHeavyParcelCost(double weight)
        {
            var extraCost = HeavyParcelDefaultCost;
            if (weight > HeavyParcelLimitWeight)
            {
                extraCost += (decimal)(weight - HeavyParcelLimitWeight) * HeavyParcelExcessWeightCostRate;
            }

            return extraCost;
        }

        private static double GetSizeLimit(ParcelSize size)
        {
            var hasKey = WeightLimits.TryGetValue(size, out var limitWeight);
            if (!hasKey)
            {
                throw new ArgumentOutOfRangeException();
            }

            return limitWeight;
        }
    }
}
