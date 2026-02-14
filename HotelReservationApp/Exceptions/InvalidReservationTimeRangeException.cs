using HotelReservationApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationApp.Exceptions
{
    public class InvalidReservationTimeRangeException : Exception
    {
        public Reservation Reservation { get; }

        public InvalidReservationTimeRangeException(Reservation reservation)
        {
            Reservation = reservation;
        }
    }
}
