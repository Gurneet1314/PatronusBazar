namespace PatronusBazar.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int PaymentId { get; set; }
    }
}
