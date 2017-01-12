namespace CarRental.Bookings.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public float DailyCost { get; set; }

        public int Mileage { get; set; }

        public CarStyle Style { get; set; }
    }
}