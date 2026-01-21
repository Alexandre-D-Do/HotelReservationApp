using HotelReservationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApp.Exceptions
{
    internal class ReservationConflictException : Exception
    {
        public Reservation ExistingException { get; }
        public Reservation IncomingReservation { get; }

        public ReservationConflictException(Reservation existingException, Reservation incomingReservation)
        {
            ExistingException = existingException;
            IncomingReservation = incomingReservation;
        }

        public ReservationConflictException(string? message, Reservation existingException, Reservation incomingReservation) : base(message)
        {
            ExistingException = existingException;
            IncomingReservation = incomingReservation;
        }

        public ReservationConflictException(string? message, Exception? innerException, Reservation existingException, Reservation incomingReservation) : base(message, innerException)
        {
            ExistingException = existingException;
            IncomingReservation = incomingReservation;
        }


    }
}
