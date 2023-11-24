namespace CarRental.Bookings.Entities
{
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
}