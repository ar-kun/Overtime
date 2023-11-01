using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class PayrollController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
