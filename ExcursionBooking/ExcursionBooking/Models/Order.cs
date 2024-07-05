namespace ExcursionBooking.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; } // New field for User association
    }
}
