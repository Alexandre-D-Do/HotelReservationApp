using HotelReservationApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApp.Models
{
    public class ReservationBook
    {
        private readonly List<Reservation> reservations;

        public ReservationBook()
        {
            reservations = new List<Reservation>(); 
        }

        /// <summary>
        /// Get reservation for user.
        /// </summary>
        /// <returns>All current reservations.</returns>
        public IEnumerable<Reservation> GetAllReservations()
        {
            return reservations;
        }

        /// <summary>
        /// Add a new reservation.
        /// </summary>
        /// <param name="reservation">The reservation to be added.</param>
        /// <exception cref="ReservationConflictException">
        /// New reservation conflicts with existing reservation.
        /// </exception>
        public void AddReservation(Reservation reservation)
        {
            foreach (Reservation existingReservation in reservations)
            {
                if (existingReservation.Conflicts(reservation)) 
                {
                    throw new ReservationConflictException(existingReservation, reservation);
                }
            }
            reservations.Add(reservation);
        }
    }
}
