using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApp.Models
{
    public class ReservationBook
    {
        private readonly Dictionary<RoomID, List<Reservation>> roomsToReservations;
        public ReservationBook()
        {
            roomsToReservations = new Dictionary<RoomID, List<Reservation>>(); 
        }

    }
}
