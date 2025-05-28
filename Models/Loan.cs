namespace MVCSiteTemplate.Models
{
    public class Loan
    {
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public int Term { get; set; } // In months
        public decimal Payment { get; set; } // Monthly payment
        public decimal TotalInterest { get; set; } // Interest over life of the loan
        public decimal TotalCost { get; set; } // Amount borrowed plus total interest
    }
}
