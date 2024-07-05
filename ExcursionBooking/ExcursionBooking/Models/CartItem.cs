namespace ExcursionBooking.Models
{
    public class CartItem 
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; } // New field for User association
    }
}
