using HotelReservationApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationApp.Services.ReservationDeleters
{
    public interface IReservationDeleter
    {
        Task DeleteReservation(Reservation reservation);
    }
}
