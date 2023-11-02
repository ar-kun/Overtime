using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
