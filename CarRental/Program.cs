namespace CarRental
{
    using CarRental.Bookings;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            BookingService bookingService = new BookingService(new BookingRepository());
            Console.Out.WriteLine("Vehicle Rentals Ltd.");

            while (true)
            {
                Console.Out.WriteLine("1. Make a car booking");
                Console.Out.WriteLine("2. Make a van booking");
                Console.Out.WriteLine("Enter your selection:");
                var input = Console.In.ReadLine();

                if (input == "1")
                {
                    bookingService.MakeCarBooking(GetCar(), GetDate(), GetDuration(), GetDiscount(), GetName());
                }
                else if (input == "2")
                {
                    bookingService.MakeVanBooking(GetVan(), GetDate(), GetDuration(), GetDiscount(), GetName());
                }
                else break;
            }
        }

        #region Don't worry about this code unless you need to
        static Bookings.Entities.Car GetCar()
        {
            Console.Out.WriteLine("Select Car:");
            var cars = new List<Bookings.Entities.Car>
            {
                new Bookings.Entities.Car { Id = 1, Make = "Ford", Model = "Focus", DailyCost = 80, Mileage = 11000, Style = Bookings.Entities.CarStyle.HatchBack },
                new Bookings.Entities.Car { Id = 2, Make = "Honda", Model = "Civic", DailyCost = 80, Mileage = 12000, Style = Bookings.Entities.CarStyle.HatchBack },
                new Bookings.Entities.Car { Id = 3, Make = "Seat", Model = "Leon", DailyCost = 80, Mileage = 13000, Style = Bookings.Entities.CarStyle.HatchBack },
                new Bookings.Entities.Car { Id = 4, Make = "BMW", Model = "3 Series", DailyCost = 100, Mileage = 14000, Style = Bookings.Entities.CarStyle.Saloon }
            };
            int idx = 1;
            cars.ForEach(c => Console.Out.WriteLine("{0}: {1} {2}", idx++, c.Make, c.Model));

            var input = int.Parse(Console.In.ReadLine());

            return cars.ElementAt(input - 1);
        }

        static Bookings.Entities.Van GetVan()
        {
            Console.Out.WriteLine("Select Van:");
            var vans = new List<Bookings.Entities.Van>
            {
                new Bookings.Entities.Van { Id = 5, Make = "Merc", Model = "Sprinter", DailyCost = 150, Mileage = 11000, WheelBase = Bookings.Entities.WheelBase.Short },
                new Bookings.Entities.Van { Id = 6, Make = "Ford", Model = "Transit", DailyCost = 150, Mileage = 12000, WheelBase = Bookings.Entities.WheelBase.Short },
                new Bookings.Entities.Van { Id = 7, Make = "Renault", Model = "Box", DailyCost = 160, Mileage = 13000, WheelBase = Bookings.Entities.WheelBase.Long },
                new Bookings.Entities.Van { Id = 8, Make = "Toyota", Model = "Toyota Van", DailyCost = 200, Mileage = 14000, WheelBase = Bookings.Entities.WheelBase.Long }
            };
            int idx = 1;
            vans.ForEach(v => Console.Out.WriteLine("{0}: {1} {2}", idx++, v.Make, v.Model));

            var input = int.Parse(Console.In.ReadLine());

            return vans.ElementAt(input - 1);
        }

        static DateTime GetDate() {
            Console.Out.WriteLine("Enter date (yyyy-MM-dd):");
            var input = Console.In.ReadLine();
            DateTime dt = DateTime.ParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            return dt;
        }

        static int GetDuration()
        {
            Console.Out.WriteLine("Enter duration:");
            return int.Parse(Console.In.ReadLine());
        }

        static float GetDiscount()
        {
            Console.Out.WriteLine("Any discount to apply (0 for none):");
            return float.Parse(Console.In.ReadLine());
        }

        static string GetName()
        {
            Console.Out.WriteLine("Enter customer name:");
            return Console.In.ReadLine();
        }
        #endregion
    }
}
