using System.Diagnostics;
using Hospital.DataAccess;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationDbContext DbContext = new ApplicationDbContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var internaldoctors = DbContext.doctors.Where(e => e.Specialization == "Cardiology" || e.Specialization == "Dermatology" || e.Specialization == "Neurology");
            var Pediatricsdoctors = DbContext.doctors.Where(e => e.Specialization == "Pediatrics");
            var Surgerydoctors = DbContext.doctors.Where(e => e.Specialization == "Orthopedics");
            var doctors = new
            {
                Internal = internaldoctors.ToList(),
                Pediatrics = Pediatricsdoctors.ToList(),
                Surgery = Surgerydoctors.ToList()
            };

            return View(doctors);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
