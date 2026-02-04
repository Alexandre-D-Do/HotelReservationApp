using HotelReservationApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationApp.Services.ReservationCreators
{
    public interface IReservationCreator
    {
        Task CreateReservation(Reservation reservation);
    }
}
