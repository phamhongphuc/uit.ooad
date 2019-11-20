using System;
using System.Linq;
using Realms;
using uit.hotel.Businesses;

namespace uit.hotel.Models
{
    public class RoomKind : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfBeds { get; set; }
        public int AmountOfPeople { get; set; }
        public bool IsActive { get; set; }

        [Backlink(nameof(Room.RoomKind))]
        public IQueryable<Room> Rooms { get; }

        [Backlink(nameof(Price.RoomKind))]
        public IQueryable<Price> Prices { get; }

        [Backlink(nameof(VolatilityPrice.RoomKind))]
        public IQueryable<VolatilityPrice> VolatilityPrices { get; }

        public Price GetPrice(DateTimeOffset date)
        {
            Price select = null;
            foreach (var price in Prices)
                if (
                    date >= price.EffectiveStartDate &&
                    (select == null || select.EffectiveStartDate < price.EffectiveStartDate)
                )
                    select = price;
            if (select == null) throw new Exception("Loại phòng này chưa được cài đặt giá");
            return select;
        }

        public RoomKind GetManaged()
        {
            var roomKind = RoomKindBusiness.Get(Id);
            if (roomKind == null)
                throw new Exception("Mã loại phòng không tồn tại");
            return roomKind;
        }
    }
}
