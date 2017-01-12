namespace CarRental.Bookings
{
    using System.Collections.Generic;
    using CarRental.Bookings.Entities;

    public class BookingRepository
    {
        private List<Booking> _cars = new List<Booking>();
        private List<Booking> _vans = new List<Booking>();
        
        public List<Booking> GetCarBookings()
        {
            return _cars;
        }

        public void AddCarBooking(Booking b)
        {
            this._cars.Add(b);
        }

        public List<Booking> GetVanBookings()
        {
            return _vans;
        }

        public void AddVanBooking(Booking b)
        {
            this._vans.Add(b);
        }
    }
}