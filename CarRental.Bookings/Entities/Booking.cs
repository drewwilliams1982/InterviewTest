namespace CarRental.Bookings.Entities
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class Booking
    {
        public int CarId { get; set; }

        public string Name { get; set; }

        public float TotalCost { get; set; }

        [XmlElement(DataType = "date")]
        public DateTime RentalDate { get; set; }

        [XmlElement(DataType = "date")]
        public DateTime ReturnDate { get; set; }
    }
}