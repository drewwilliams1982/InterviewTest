namespace CarRental
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            Console.Out.WriteLine("Vehicle Rentals Ltd.");

            while (true)
            {
                Console.Out.WriteLine("1. Make a new booking");
                Console.Out.WriteLine("2. List this weeks bookings");
                Console.Out.WriteLine("3. Quit");
                Console.Out.WriteLine("");
                Console.Out.WriteLine("Enter your selection:");
                var input = Console.In.ReadLine();

                if (input == "1")
                {
                    NewBooking();
                }
                else if (input == "2")
                {
                    ListBookings();
                }
                else break;
            }
        }

        static void NewBooking()
        {
            Console.Out.WriteLine("1. Make a car booking");
            Console.Out.WriteLine("2. Make a van booking");
        }

        static void ListBookings()
        {
        }
    }
}
