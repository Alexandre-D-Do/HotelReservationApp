using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApp.Models
{
    public class RoomID
    {
        public int FloorNumber { get; }
        public int RoomrNumber { get; }
    

        public override bool Equals(object? obj)
        {
            return obj is RoomID roomID && FloorNumber == roomID.FloorNumber && RoomrNumber == roomID.RoomrNumber;
        }

        public override string ToString()
        {
            return $"{FloorNumber}{RoomrNumber}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FloorNumber, RoomrNumber);
        }
    }
}

