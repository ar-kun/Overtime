using API.DTOs.Overtimes;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    //[Authorize]
    public class DashboardController : Controller
    {
        private readonly IOvertimeRepository repository;

        public DashboardController(IOvertimeRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var listOvertime = new List<OvertimeDto>();

            if(result.Data != null)
            {
                listOvertime = result.Data.ToList();
            }
            return View(listOvertime);
        }

        [HttpPost]
        public async Task<IActionResult> Index(OvertimeDto createOvertime)
        {
            var result = await repository.Post(createOvertime);
            if(result.Status == "200")
            {
                return RedirectToAction(nameof(Index));
            }
            else if (result.Status == "409")
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            return View();
        }

        public IActionResult Overtime()
        {
            return View();
        }
    }
}
