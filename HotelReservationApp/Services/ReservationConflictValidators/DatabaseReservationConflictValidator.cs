using HotelReservationApp.DbContexts;
using HotelReservationApp.DTOs;
using HotelReservationApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace HotelReservationApp.Services.ReservationConflictValidators
{
    public class DatabaseReservationConflictValidator : IReservationConflictValidator
    {
        public async Task<Reservation> GetConflictingReservation(Reservation reservation)
        {
            var contextOptions = new DbContextOptionsBuilder<HotelReservationAppDbContext>().UseSqlServer(ConfigurationManager.ConnectionStrings["ReservationsDatabase"].ConnectionString).Options;

            using (HotelReservationAppDbContext context = new HotelReservationAppDbContext(contextOptions))
            {
                return await context.Reservations.Select(reservation => ToReservation(reservation)).FirstOrDefaultAsync(reservation => reservation.Conflicts(reservation));
            }
        }
        private static Reservation ToReservation(ReservationDTO reservationDTO)
        {
            return new Reservation(new RoomID(reservationDTO.FloorNumber.ToString(), reservationDTO.RoomNumber.ToString()), reservationDTO.Username, reservationDTO.StartDate, reservationDTO.EndDate);
        }
    }
}
