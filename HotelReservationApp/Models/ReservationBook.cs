using HotelReservationApp.Exceptions;
using HotelReservationApp.Services.ReservationConflictValidators;
using HotelReservationApp.Services.ReservationCreators;
using HotelReservationApp.Services.ReservationProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApp.Models
{
    public class ReservationBook
    {
        private readonly IReservationProvider _reservationProvider;
        private readonly IReservationCreator _reservationCreator;
        private readonly IReservationConflictValidator _reservationConflictValidator;

        public ReservationBook(IReservationProvider reservationProvider, IReservationCreator reservationCreator, IReservationConflictValidator reservationConflictValidator)
        {
            _reservationProvider = reservationProvider;
            _reservationCreator = reservationCreator;
            _reservationConflictValidator = reservationConflictValidator;
        }

        /// <summary>
        /// Get reservation for user.
        /// </summary>
        /// <returns>All current reservations.</returns>
        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationProvider.GetAllReservations();
        }

        /// <summary>
        /// Add a new reservation.
        /// </summary>
        /// <param name="reservation">The reservation to be added.</param>
        /// <exception cref="ReservationConflictException">
        /// New reservation conflicts with existing reservation.
        /// </exception>
        public async Task AddReservation(Reservation reservation)
        {
            Reservation conflictingReservation = await _reservationConflictValidator.GetConflictingReservation(reservation);

            if (conflictingReservation != null)
            {
                throw new ReservationConflictException(conflictingReservation, reservation);
            }

            await _reservationCreator.CreateReservation(reservation);
        }
    }
}
