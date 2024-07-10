namespace CarRental.Bookings
{
    using System;
    using System.Collections.Generic;
    using CarRental.Bookings.Entities;

    public class BookingService
    {
        private readonly BookingRepository _repo;

        public BookingService(BookingRepository repo)
        {
            _repo = repo;
        }

        /* Part one:
         *      Book a car from a [start] date for a given number of days [duration].
         * 
         *      A specific car can only be booked if it isn't already booked out. After each car booking, 
         *      an extra day is required for cleaning and can't be booked out on this day.
         * 
         *      If the booking can't be made, raise an exception highlighting the specific error.
         * 
         * Part two:
         *      The [TotalCost] of a car booking is the cars [DailyCost] multiplied by the number of days. The
         *      agreed discount is then applied onto final value.
         *
         */
        public Booking MakeCarBooking(Car car, DateTime start, int duration, float discount, string name)
        {
            var booking = new Booking();
            booking.CarId = car.Id;
            booking.StartDate = start;
            booking.ReturnDate = start.AddDays(duration);
            booking.TotalCost = 0; //todo:: part two
            booking.Name = name;

            // bookings contains a list of all historic, current and future bookings for all cars.
            List<Booking> bookings = _repo.GetCarBookings();

            // todo:: part one: check for booking clashes

            _repo.AddCarBooking(booking);

            return booking;
        }

        /*  Part three:
         * Book a van from a [start] date for a given number of days [duration].
         * 
         * A specific van can only be booked if it isn't already booked out. No valet is required for a van.
         * 
         * If the booking can't be made, raise an exception highlighting the specific error.
         * 
         * The daily rate for a van after 5 days is subject to a further 25% discount. For example, if you 
         * rent a van for 7 days at £100 per day, the first 5 days will be charged at £100 per day and the 
         * remaining 2 days will be charged at £75 per day. 
         * 
         * The [TotalCost] of a van booking is the vans [DailyCost] multiplied by the number of days. The
         * agreed discount is then applied onto final value.
         * 
         */
    }
}
