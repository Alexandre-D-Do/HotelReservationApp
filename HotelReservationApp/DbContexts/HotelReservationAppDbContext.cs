using HotelReservationApp.DTOs;
using HotelReservationApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace HotelReservationApp.DbContexts
{
    internal class HotelReservationAppDbContext : DbContext
    {
        public DbSet<ReservationDTO> Reservations { get; set; }

        public HotelReservationAppDbContext(DbContextOptions<HotelReservationAppDbContext> options) : base(options)
        {
        }

        

    }
}
