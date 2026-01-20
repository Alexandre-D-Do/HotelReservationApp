using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApp.Models
{
    internal class Reservation
    {
        public RoomID RoomID { get; }
        public DateTime StartTIme { get; }
        public DateTime EndTIme { get; }
    }
}
