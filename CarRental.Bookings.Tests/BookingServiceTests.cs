namespace CarRental.Bookings.Tests
{
    using System;
    using CarRental.Bookings.Entities;
    using Xunit;
    public class BookingServiceTests
    {
        [Fact]
        public void when_making_a_car_booking_it_should_calculate_the_daily_rate()
        {
            var repo = new BookingRepository();
            var service = new BookingService(repo);
            var car = new Car();
            car.DailyCost = 100;

            var booking = service.MakeCarBooking(car, DateTime.Today, 1, 0, "Joe Bloggs");
            
            Assert.Equal(100, booking.TotalCost);
        }

        [Fact]
        public void the_return_date_is_calculated_correctly()
        {
            var repo = new BookingRepository();
            var service = new BookingService(repo);
            var car = new Car();
            car.DailyCost = 100;

            var booking = service.MakeCarBooking(car, DateTime.Today, 1, 0, "Joe Bloggs");
            Assert.Equal(100, booking.TotalCost);
        }

        [Fact]
        public void agreed_discount_is_applied()
        {
            Assert.True(false);
        }
    }
}
