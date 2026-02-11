using HotelReservationApp.DbContexts;
using HotelReservationApp.DTOs;
using HotelReservationApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace HotelReservationApp.Services.ReservationProviders
{
    public class DatabaseReservationProvider : IReservationProvider
    {
        string _connectionString;

        public DatabaseReservationProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            var contextOptions = new DbContextOptionsBuilder<HotelReservationAppDbContext>().UseSqlServer(_connectionString).Options;

            using (HotelReservationAppDbContext context = new HotelReservationAppDbContext(contextOptions))
            {
                IEnumerable<ReservationDTO> reservationDTOs = await context.Reservations.ToListAsync();

                return reservationDTOs.Select(reservation => ToReservation(reservation));
            }
        }

        private static Reservation ToReservation(ReservationDTO reservationDTO)
        {
            return new Reservation(new RoomID(reservationDTO.FloorNumber.ToString(), reservationDTO.RoomNumber.ToString()), reservationDTO.Username, reservationDTO.StartDate, reservationDTO.EndDate);
        }
    }
}
