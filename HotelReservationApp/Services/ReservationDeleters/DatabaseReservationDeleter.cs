using HotelReservationApp.DbContexts;
using HotelReservationApp.DTOs;
using HotelReservationApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationApp.Services.ReservationDeleters
{
    public class DatabaseReservationDeleter : IReservationDeleter
    {
        string _connectionString;

        public DatabaseReservationDeleter(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task DeleteReservation(Reservation reservation)
        {
            var contextOptions = new DbContextOptionsBuilder<HotelReservationAppDbContext>().UseSqlServer(_connectionString).Options;
            using (HotelReservationAppDbContext context = new HotelReservationAppDbContext(contextOptions))
            {
                ReservationDTO reservationDTO = ToReservationDTO(reservation);
                var reservationToDelete = context.Reservations.FirstOrDefault(r => 
                    r.FloorNumber == reservationDTO.FloorNumber &&
                    r.RoomNumber == reservationDTO.RoomNumber &&
                    r.Username == reservationDTO.Username &&
                    r.StartDate == reservationDTO.StartDate &&
                    r.EndDate == reservationDTO.EndDate);
                if (reservationToDelete != null)
                {
                    context.Reservations.Remove(reservationToDelete);
                    await context.SaveChangesAsync();
                }
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
