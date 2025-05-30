using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCSiteTemplate.Helpers;
using MVCSiteTemplate.Models;

namespace MVCSiteTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult App()
        {
            var loan = new Loan
            {
                Amount = 15000m,
                Payment = 0.0m,
                Rate = 3.5m,
                Term = 60,
                TotalCost = 0.0m,
                TotalInterest = 0.0m
            };

            return View(loan);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult App(Loan loan)
        {
            var helper = new LoanHelper();
            var newLoan = helper.GetPayments(loan);

            return View(newLoan);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
