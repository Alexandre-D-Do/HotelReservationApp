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
        public int RoomNumber { get; }

        public RoomID(string floorNumber, string roomNumber)
        {
            FloorNumber = int.Parse(floorNumber);
            RoomNumber = int.Parse(roomNumber);
        }

        /// <summary>
        /// Check if object is the same as the current RoomID instance.
        /// </summary>
        /// <param name="obj">The object to be checked.</param>
        /// <returns>True or False.</returns>
        public override bool Equals(object? obj)
        {
            return obj is RoomID roomID && FloorNumber == roomID.FloorNumber && RoomNumber == roomID.RoomNumber;
        }

        /// <summary>
        /// Convert RoomID to a string.
        /// </summary>
        /// <returns>A string consisting of FloorNumber and RoomNumber.</returns>
        public override string ToString()
        {
            return $"{FloorNumber}{RoomNumber}";
        }

        /// <summary>
        /// Get a HashCode for the RoomID.
        /// </summary>
        /// <returns>A unique HashCode for the RoomID.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(FloorNumber, RoomNumber);
        }

        
    }
}

