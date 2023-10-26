using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
