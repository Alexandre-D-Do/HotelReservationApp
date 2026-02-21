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
        string _connectionString;

        public DatabaseReservationConflictValidator(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Reservation> GetConflictingReservation(Reservation reservation)
        {
            var contextOptions = new DbContextOptionsBuilder<HotelReservationAppDbContext>().UseSqlServer(_connectionString).Options;

            using (HotelReservationAppDbContext context = new HotelReservationAppDbContext(contextOptions))
            {
                ReservationDTO reservationDTO = await context.Reservations
                    .Where(r => r.FloorNumber == reservation.RoomID.FloorNumber)
                    .Where(r => r.RoomNumber == reservation.RoomID.RoomNumber)
                    .Where(r => r.EndDate > reservation.StartDate)
                    .Where(r => r.StartDate < reservation.EndDate)
                    .FirstOrDefaultAsync();

                if (reservationDTO == null)
                {
                    return null;
                }

                return ToReservation(reservationDTO);
            }
        }
        private static Reservation ToReservation(ReservationDTO reservationDTO)
        {
            return new Reservation(new RoomID(reservationDTO.FloorNumber, reservationDTO.RoomNumber), reservationDTO.Username, reservationDTO.StartDate, reservationDTO.EndDate);
        }
    }
}
