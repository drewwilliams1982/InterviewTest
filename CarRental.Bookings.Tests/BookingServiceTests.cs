namespace CarRental.Bookings.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using CarRental.Bookings.Entities;

    using NUnit.Framework;

    [TestFixture]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2187:TestCases should contain tests", Justification = "<Pending>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class BookingServiceTests
    {
        private BookingService CreateServiceUnderTest(BookingRepository bookingRepository)
        {
            return new BookingService(bookingRepository);
        }

        [TestFixture]
        public class MakeCarBookingTests : BookingServiceTests
        {
            [TestFixture]
            public class ClashDetectionTests : MakeCarBookingTests
            {
                private static readonly Car _smallCar = new Car { Id = 1, DailyCost = 20, Make = "Volkswagen", Model = "Polo" };
                private static readonly Car _mediumCar = new Car { Id = 2, DailyCost = 40, Make = "Dacia", Model = "Sandero" };
                private static readonly Car _largeCar = new Car { Id = 3, DailyCost = 60, Make = "Skoda", Model = "Kodiaq" };
                private static readonly Car _luxuryCar = new Car { Id = 4, DailyCost = 100, Make = "Audi", Model = "A7" };

                [Test]
                [TestCaseSource(nameof(BookingServiceClashDetectionNegativeTestCases))]
                public void it_should_throw_an_exception_when_the_car_booking_clashes_with_an_existing_booking(BookingServiceClashDetectionTestCaseData clashDetectionTestCaseData)
                {
                    // Act                    
                    var bookingService = CreateServiceUnderTest(clashDetectionTestCaseData.ExistingBookings);

                    // Assert
                    TestContext.WriteLine($"{clashDetectionTestCaseData.TestDescription}{Environment.NewLine}");
                    TestContext.WriteLine(clashDetectionTestCaseData.ForTestContext());
                    Assert.Throws<Exception>(() => bookingService.MakeCarBooking(clashDetectionTestCaseData.Car, clashDetectionTestCaseData.StartDate, clashDetectionTestCaseData.Duration, clashDetectionTestCaseData.Discount, clashDetectionTestCaseData.Name));
                }

                [Test]
                [TestCaseSource(nameof(BookingServiceClashDetectionPositiveTestCases))]
                public void it_should_add_the_booking_to_the_repository_when_the_car_booking_doesnt_clash_with_an_existing_booking(BookingServiceClashDetectionTestCaseData clashDetectionTestCaseData)
                {
                    // Act
                    var bookingService = CreateServiceUnderTest(clashDetectionTestCaseData.ExistingBookings);
                    bookingService.MakeCarBooking(clashDetectionTestCaseData.Car, clashDetectionTestCaseData.StartDate, clashDetectionTestCaseData.Duration, clashDetectionTestCaseData.Discount, clashDetectionTestCaseData.Name);

                    // Assert
                    TestContext.WriteLine($"{clashDetectionTestCaseData.TestDescription}{Environment.NewLine}");
                    TestContext.WriteLine(clashDetectionTestCaseData.ForTestContext());
                    var findMyBooking = clashDetectionTestCaseData.ExistingBookings.GetCarBookings().FirstOrDefault(x => x.CarId == clashDetectionTestCaseData.Car.Id && x.Name == clashDetectionTestCaseData.Name && x.RentalDate == clashDetectionTestCaseData.StartDate);
                    Assert.IsNotNull(findMyBooking);
                }

                public static IEnumerable  BookingServiceClashDetectionPositiveTestCases
                {
                    get
                    {
                        var dateTimeToday = DateTime.Now.Date;
                        var dateTimeTomorrow = dateTimeToday.AddDays(1);
                        var dateTimeTwoDaysAgo = dateTimeToday.AddDays(-2);
                        var dateTimeTwoDaysTime = dateTimeToday.AddDays(2);
                        string testDescription;

                        testDescription = $"No existing Car Bookings.";
                        var testCase1 = new BookingServiceClashDetectionTestCaseData(testDescription, _smallCar, 0, 1, "Small Car for 1 day from today", dateTimeToday);
                        yield return new TestCaseData(testCase1);

                        testDescription = $"No existing Bookings for the same Car.";
                        var existingSmallCarBookingForToday = new Booking() { CarId = _smallCar.Id, Name = "Small for 1 day from today", RentalDate = dateTimeToday.Date, ReturnDate = dateTimeTomorrow };
                        var existingLargeCarBookingForToday = new Booking() { CarId = _largeCar.Id, Name = "Large for 1 day from today", RentalDate = dateTimeToday.Date, ReturnDate = dateTimeTomorrow };
                        var existingLuxuryCarBookingForToday = new Booking() { CarId = _luxuryCar.Id, Name = "Luxury for 1 day from today", RentalDate = dateTimeToday.Date, ReturnDate = dateTimeTomorrow };
                        var testCase2 = new BookingServiceClashDetectionTestCaseData(testDescription, _mediumCar, 0, 1, "Medium Car for 1 day", dateTimeToday, existingSmallCarBookingForToday, existingLargeCarBookingForToday, existingLuxuryCarBookingForToday);
                        yield return new TestCaseData(testCase2);

                        testDescription = $"No existing Bookings for the same Car on the same day.";
                        var existingSmallCarBookingEndsTwoDaysAgo = new Booking() { CarId = _smallCar.Id, Name = "Small for 1 day from 2 days ago", RentalDate = dateTimeTwoDaysAgo, ReturnDate = dateTimeToday.Date.AddDays(-1) };
                        var existingSmallCarBookingStartsInTwoDays = new Booking() { CarId = _smallCar.Id, Name = "Small for 1 day in 2 days time", RentalDate = dateTimeTwoDaysTime, ReturnDate = dateTimeToday.Date.AddDays(3) };
                        var testCase3 = new BookingServiceClashDetectionTestCaseData(testDescription, _mediumCar, 0, 1, "Medium Car for 1 day", dateTimeToday, existingSmallCarBookingForToday, existingSmallCarBookingEndsTwoDaysAgo, existingSmallCarBookingStartsInTwoDays);
                        yield return new TestCaseData(testCase3);
                    }
                }

                public static IEnumerable  BookingServiceClashDetectionNegativeTestCases
                {
                    get
                    {
                        var dateTimeToday = DateTime.Now.Date;
                        var dateTimeYesterday = dateTimeToday.AddDays(-1);
                        var dateTimeTomorrow = dateTimeToday.AddDays(1);
                        var dateTimeTwoDaysAgo = dateTimeToday.AddDays(-2);
                        var dateTimeTwoDaysTime = dateTimeToday.AddDays(2);
                        string testDescription;

                        testDescription = $"Existing Booking for the same Car.{Environment.NewLine}e.g. Requested from 15Jul to 16Jul, but Booking exists for 15Jul to 16Jul.";
                        var existingLuxuryCarBookingForToday = new Booking() { CarId = _luxuryCar.Id, Name = "Luxury for 1 day from today", RentalDate = dateTimeToday.Date, ReturnDate = dateTimeTomorrow };
                        var testCase1 = new BookingServiceClashDetectionTestCaseData(testDescription, _luxuryCar, 0, 1, "Luxury Car for 1 day", dateTimeToday, existingLuxuryCarBookingForToday);
                        yield return new TestCaseData(testCase1);

                        testDescription = $"Existing Booking for the same Car is currently with a customer.{Environment.NewLine}e.g. Requested from 15Jul to 16Jul, but Booking exists for 14Jul to 16Jul.";
                        var existingLargeCarBookingForTwoDaysFromYesterday = new Booking() { CarId = _largeCar.Id, Name = "Large for 2 days from yesterday", RentalDate = dateTimeYesterday, ReturnDate = dateTimeTomorrow };
                        var testCase3 = new BookingServiceClashDetectionTestCaseData(testDescription, _largeCar, 0, 1, "Large Car for 1 day", dateTimeToday, existingLargeCarBookingForTwoDaysFromYesterday);
                        yield return new TestCaseData(testCase3);

                        testDescription = $"Existing Booking for the same Car needs Valeting.{Environment.NewLine}e.g. Requested from 15Jul to 16Jul, but Booking exists for 14Jul to 15Jul.";
                        var existingMediumCarBookingForYesterday = new Booking() { CarId = _mediumCar.Id, Name = "Medium for 1 day from yesterday", RentalDate = dateTimeYesterday, ReturnDate = dateTimeToday };
                        var testCase2 = new BookingServiceClashDetectionTestCaseData(testDescription, _mediumCar, 0, 1, "Medium Car for 1 day", dateTimeToday, existingMediumCarBookingForYesterday);
                        yield return new TestCaseData(testCase2);

                        testDescription = $"Existing Booking for the same Car is booked by a customer.{Environment.NewLine}e.g. Requested from 15Jul to 17Jul, but Booking exists for 16Jul to 17Jul.";
                        var existingSmallCarBookingForTomorrow = new Booking() { CarId = _smallCar.Id, Name = "Small for 1 day from tomorrow", RentalDate = dateTimeTomorrow, ReturnDate = dateTimeTwoDaysTime };
                        var testCase4 = new BookingServiceClashDetectionTestCaseData(testDescription, _smallCar, 0, 2, "Small Car for 2 days", dateTimeToday, existingSmallCarBookingForTomorrow);
                        yield return new TestCaseData(testCase4);

                        testDescription = $"Existing Booking for the same Car needs Valeting, but it is booked by a customer.{Environment.NewLine}e.g. Requested from 15Jul to 16Jul, but Booking exists for 16Jul to 17Jul.";
                        var existingLuxuryCarBookingForTomorrow = new Booking() { CarId = _luxuryCar.Id, Name = "Luxury for 1 day from tomorrow", RentalDate = dateTimeTomorrow, ReturnDate = dateTimeTwoDaysTime };
                        var testCase5 = new BookingServiceClashDetectionTestCaseData(testDescription, _luxuryCar, 0, 1, "Luxury Car for 1 day", dateTimeToday, existingLuxuryCarBookingForTomorrow);
                        yield return new TestCaseData(testCase5);

                        testDescription = $"Existing Booking for the same Car is currently with a customer.{Environment.NewLine}e.g. Requested from 15Jul to 16Jul, but Booking exists for 14Jul to 17Jul.";
                        var existingMediumCarBookingForThreeDaysFromYesterday = new Booking() { CarId = _mediumCar.Id, Name = "Medium for 3 days from yesterday", RentalDate = dateTimeYesterday, ReturnDate = dateTimeTwoDaysTime };
                        var testCase6 = new BookingServiceClashDetectionTestCaseData(testDescription, _mediumCar, 0, 1, "Medium Car for 1 day", dateTimeToday, existingMediumCarBookingForThreeDaysFromYesterday);
                        yield return new TestCaseData(testCase6);
                    }
                }

                [Serializable]
                public class BookingServiceClashDetectionTestCaseData
                {
                    protected BookingServiceClashDetectionTestCaseData()
                    {
                    }

                    public BookingServiceClashDetectionTestCaseData(string testDescription, Car car, float discount, int duration, string name, DateTime startDate, params Booking[] existingCarBookings)
                    {
                        TestDescription = testDescription;
                        Car = car;
                        Discount = discount;
                        Duration = duration;
                        Name = name;
                        StartDate = startDate;

                        ExistingBookings = new BookingRepository();
                        if (existingCarBookings != null && existingCarBookings.Any())
                        {
                            foreach (var existingBooking in existingCarBookings)
                            {
                                ExistingBookings.AddCarBooking(existingBooking);
                            }
                        }                        
                    }

                    [XmlIgnore]
                    public string TestDescription { get; set; }

                    public Car Car { get; set; }

                    public float Discount { get; set; }

                    public int Duration { get; set; }

                    [XmlIgnore]
                    public BookingRepository ExistingBookings { get; }

                    public List<Booking> Bookings
                    {
                        get
                        {
                            return this.ExistingBookings.GetCarBookings();
                        }
                    }

                    public string Name { get; set; }

                    [XmlElement(DataType = "date")]
                    public DateTime StartDate { get; set; }

                    internal string ForTestContext()
                    {
                        var xmlSerializer = new XmlSerializer(this.GetType());
                        
                        using(var textWriter = new StringWriter())
                        {
                            xmlSerializer.Serialize(textWriter, this);
                            return textWriter.ToString();
                        }
                    }
                }
            }

            [TestFixture]
            [Ignore("This will be assessed within the interview.")]
            public class DiscountAppliedTests : MakeCarBookingTests
            {
                [Test]
                public void it_should_apply_a_discount_to_the_total_cost_when_a_discount_is_applied()
                {
                }
            }
        }
    }
}