using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApp.Models
{
    public class Reservation
    {
        public RoomID RoomID { get; }
        public string Username { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public TimeSpan Length => EndTime.Subtract(StartTime);

        public Reservation(RoomID roomID, string userName, DateTime startTime, DateTime endTime)
        {
            RoomID = roomID;
            Username = userName;
            StartTime = startTime; 
            EndTime = endTime;
            
        }

        /// <summary>
        /// Check if a reservation conflicts with this reservation.
        /// </summary>
        /// <param name="reservation">The incoming reservation to be checked for conflict.</param>
        /// <returns>True or False.</returns>
        public bool Conflicts(Reservation reservation)
        {
            if (!reservation.RoomID.Equals(RoomID))
            {
                return false;
            }

            return reservation.StartTime < EndTime || reservation.EndTime > StartTime;

        }
    }
}
