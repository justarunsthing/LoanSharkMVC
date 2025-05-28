using MVCSiteTemplate.Models;

namespace MVCSiteTemplate.Helpers
{
    public class LoanHelper
    {
        public Loan GetPayments(Loan loan)
        {
            loan.Payment = CalculatePayment(loan.Amount, loan.Rate, loan.Term);

            var balance = loan.Amount; // Balance equals loan amount on first month
            var totalInterest = 0.0m; //Interest paid on the month, 0 on first month
            var monthlyInterest = 0.0m; // Same as above

            // Subtracting interest from payment tells us how much went to the principal to pay off the loan
            var monthlyPrincipal = 0.0m;
            var monthlyRate = CalculateMonthlyRate(loan.Rate);

            // Loop from 1 to the end of the term
            for (int month = 1; month <= loan.Term; month++)
            {
                monthlyInterest = CalculateMonthlyInterest(balance, monthlyRate);

                // Running total of interest, each month add to it
                totalInterest += monthlyInterest;
                monthlyPrincipal = loan.Payment - monthlyInterest;
                balance -= monthlyPrincipal;

                // Add to loan payment schedule
                var loanPayment = new LoanPayment
                {
                    Month = month,
                    Payment = loan.Payment,
                    MonthlyPrincipal = monthlyPrincipal,
                    MonthlyInterest = monthlyInterest,
                    TotalInterest = totalInterest,
                    Balance = balance
                };

                // Push loanPayment object into the loan model
                loan.Payments.Add(loanPayment);
            }

            // Push values to the properties of the loan model
            loan.TotalInterest = totalInterest;
            loan.TotalCost = loan.Amount + totalInterest; // Amount borrowed plus total interest over life of the loan

            return loan;
        }

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