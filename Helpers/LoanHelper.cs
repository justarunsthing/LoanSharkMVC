namespace MVCSiteTemplate.Helpers
{
    public class LoanHelper
    {
        private decimal CalculatePayment(decimal amount, decimal rate, int term)
        {
            var monthlyRate = CalculateMonthlyRate(rate);
            var amountDouble = Convert.ToDouble(amount);
            var rateDouble = Convert.ToDouble(monthlyRate);
            var paymentDouble = (amountDouble * rateDouble) / (1 - Math.Pow(1 + rateDouble, -term));

            return Convert.ToDecimal(paymentDouble);
        }

        private decimal CalculateMonthlyRate(decimal rate)
        {
            return rate / 1200;
        }

        private decimal CalculateMonthlyInterest(decimal balance, decimal monthlyRate)
        {
            return balance * monthlyRate;
        }
    }
}