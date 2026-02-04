using HotelReservationApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationApp.Services.ReservationConflictValidators
{
    public interface IReservationConflictValidator
    {
        Task<Reservation> GetConflictingReservation(Reservation reservation);
    }
}
