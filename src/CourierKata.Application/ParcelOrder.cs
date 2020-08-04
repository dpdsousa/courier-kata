using System.Collections.Generic;

namespace CourierKata.Application
{
    public class ParcelOrder
    {
        public decimal TotalCost { get; private set; }
        public decimal? SpeedyShippingCost { get; private set; }
        public ICollection<Parcel> Parcels { get; } = new HashSet<Parcel>();


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
            SetSpeedyShipping(speedyShipping);
        }


        private void SetCost()
        {
            foreach (var parcel in Parcels)
            {
                var cost = SizeCost.GetCost(parcel.Size);
                parcel.Cost = cost;
                TotalCost += cost;
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

    }
}
