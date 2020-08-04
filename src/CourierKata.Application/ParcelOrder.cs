using System.Collections.Generic;

namespace CourierKata.Application
{
    public class ParcelOrder
    {
        public decimal TotalCost { get; private set; }
        public ICollection<Parcel> Parcels { get; } = new HashSet<Parcel>();


        public ParcelOrder(Parcel parcel)
        {
            Parcels.Add(parcel);
            SetCost();
        }

        public ParcelOrder(ICollection<Parcel> parcels)
        {
            Parcels = parcels;
            SetCost();
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
    }
}
