using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CourierKata.Application
{
    public class ParcelOrder
    {
        public decimal TotalDiscount { get; }
        public decimal TotalCost { get; private set; }
        public decimal? SpeedyShippingCost { get; private set; }
        public ICollection<Parcel> Parcels { get; } = new List<Parcel>();
        public ICollection<Discount> Discounts { get; private set; } = new List<Discount>();


        public ParcelOrder(Parcel parcel, bool speedyShipping = false)
        {
            Parcels.Add(parcel);
            SetCost();
            SetSpeedyShipping(speedyShipping);
        }

        public ParcelOrder(ICollection<Parcel> parcels, bool speedyShipping = false)
        {
            Parcels = parcels;
            SetCost();

            ApplyDiscounts(4, ParcelSize.Small);
            ApplyDiscounts(3, ParcelSize.Medium);
            ApplyDiscounts(5);

            TotalDiscount = Discounts.Sum(x => x.Value);
            TotalCost -= TotalDiscount;

            SetSpeedyShipping(speedyShipping);
        }

        private void SetCost()
        {
            foreach (var parcel in Parcels)
            {
                var cost = SizeCost.GetCost(parcel.Size);
                cost += WeightLimit.GetCost(parcel.Size, parcel.Weight);
                var heavyParcelCost = WeightLimit.GetHeavyParcelCost(parcel.Weight);

                parcel.Cost = cost > heavyParcelCost ? heavyParcelCost : cost;

                TotalCost += parcel.Cost;
            }
        }

        private void SetSpeedyShipping(bool speedyShipping)
        {
            if (speedyShipping)
            {
                SpeedyShippingCost = TotalCost;
                TotalCost += TotalCost;
            }
        }

        private void ApplyDiscounts(int nthNumber, ParcelSize? parcelSize = null)
        {
            var totalDiscounts = Parcels.Count(x => x.Size == parcelSize || !parcelSize.HasValue) / nthNumber;

            var parcelsDiscount = Parcels
                .OrderBy(x => x.Cost)
                .Where(x => 
                    (x.Size == parcelSize || !parcelSize.HasValue) 
                    && !Discounts.Any(y => y.DiscountParcel.Id == x.Id))
                .Take(totalDiscounts);

            foreach (var parcel in parcelsDiscount)
            {
                Discounts.Add(new Discount { Value = parcel.Cost, DiscountParcel = parcel });
            }
        }

    }
}
