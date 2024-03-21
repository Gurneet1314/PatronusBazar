namespace PatronusBazar.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
