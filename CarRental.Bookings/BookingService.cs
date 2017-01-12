namespace CarRental.Bookings
{
    using System;
    using CarRental.Bookings.Entities;

    public class BookingService
    {
        private readonly BookingRepository _repo;

        public BookingService(BookingRepository repo)
        {
            _repo = repo;
        }

        /* SPEC
         * Book a car from a [start] date for a given number of days [duration].
         * 
         * A specific car can only be booked if it isn't already booked out. After each car booking, 
         * an extra day is required for valeting and can't be booked out on this day.
         * 
         * The [TotalCost] of a car booking is the cars [DailyCost] multiplied by the number of days. The
         * agreed discount is then applied onto final value.
         * 
         * If the booking can't be made, raise an exception highlighting the specific error.
         */
        public Booking MakeCarBooking(Car car, DateTime start, int duration, float discount, string name)
        {
            var booking = new Booking();
            booking.CarId = car.Id;
            booking.RentalDate = start;
            booking.ReturnDate = start.AddDays(duration);
            booking.TotalCost = car.DailyCost * duration;
            booking.Name = name;

            // todo:: check for clashes
            // var bookings = _repo.GetCarBookings();
            //_repo.AddCarBooking(booking);

            return booking;
        }

        /* SPEC
         * Book a van from a [start] date for a given number of days [duration].
         * 
         * A specific van can only be booked if it isn't already booked out. No valet is required for a van.
         * 
         * The daily rate for a van after 5 days is subject to a further 25% discount.
         * The [TotalCost] of a van booking is the vans [DailyCost] multiplied by the number of days. The
         * agreed discount is then applied onto final value.
         * 
         * If the booking can't be made, raise an exception highlighting the specific error.
         */
        public Booking MakeVanBooking(Van van, DateTime start, int duration, float discount, string name)
        {
            // todo:: make booking for van
            
            return null;
        }
    }
}
