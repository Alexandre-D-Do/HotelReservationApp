using HotelReservationApp.DbContexts;
using HotelReservationApp.DTOs;
using HotelReservationApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace HotelReservationApp.Services.ReservationCreators
{
    public class DatabaseReservationCreator : IReservationCreator
    {
        string _connectionString;

        public DatabaseReservationCreator(string connectionString)
        {
            _connectionString=connectionString;
        }

        public async Task CreateReservation(Reservation reservation)
        {

            var contextOptions = new DbContextOptionsBuilder<HotelReservationAppDbContext>().UseSqlServer(_connectionString).Options;

            using (HotelReservationAppDbContext context = new HotelReservationAppDbContext(contextOptions))
            {
                ReservationDTO reservationDTO = ToReservationDTO(reservation);

                context.Reservations.Add(reservationDTO);
                await context.SaveChangesAsync();
            }
        }

        private static ReservationDTO ToReservationDTO(Reservation reservation)
        {
            return new ReservationDTO()
            {
                FloorNumber = reservation.RoomID?.FloorNumber ?? 0,
                RoomNumber = reservation.RoomID?.RoomNumber ?? 0,
                Username = reservation.Username,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
            };
        }
    }
}
