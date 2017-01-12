using System.Collections.Generic;

namespace CarRental
{
    using System;

    public class Booking
    {
        public int CarId { get; set; }

        public string Name { get; set; }

        public decimal TotalCost { get; set; }

        public DateTime RentalDate { get; set; }

        public DateTime ReturnDate { get; set; }
    }

    public class Car
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public decimal DailyCost { get; set; }

        public int Mileage { get; set; }

        public CarStyle Style { get; set; }
    }

    public enum CarStyle
    {
        HatchBack, Sports, SUV, Saloon
    }

    public class Van
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public decimal DailyCost { get; set; }

        public int Mileage { get; set; }

        public WheelBase WheelBase { get; set; }
    }

    public enum WheelBase
    {
        Long, Short
    }

    public class BookingService
    {
        private readonly BookingRepository _repo;

        public BookingService(BookingRepository repo)
        {
            _repo = repo;
        }

        public Booking MakeCarBooking(Car car, DateTime start, int duration, float discount, string name)
        {
            // todo:: validate

            var bookings = _repo.GetCarBookings();

            var booking = new Booking();
            booking.CarId = car.Id;
            booking.RentalDate = start;
            booking.ReturnDate = start.AddDays(duration);
            booking.TotalCost = car.DailyCost * duration;
            booking.Name = name;

            // todo:: check for clashes

            return booking;
        }

        public Booking MakeVanBooking(Van car, DateTime start, int duration, float discount, string name)
        {
            return null;
        }
    }

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
