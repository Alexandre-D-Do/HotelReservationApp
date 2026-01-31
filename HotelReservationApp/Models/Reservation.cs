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
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public TimeSpan Length => EndDate.Subtract(StartDate);

        public Reservation(RoomID roomID, string userName, DateTime startDate, DateTime endDate)
        {
            RoomID = roomID;
            Username = userName;
            StartDate = startDate; 
            EndDate = endDate;
            
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

            return reservation.StartDate < EndDate || reservation.EndDate > StartDate;

        }
    }
}
