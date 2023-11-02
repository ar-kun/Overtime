using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class ManagerController : Controller
    {

        private readonly IJoinPaymentDetailRepository _joinPaymentDetailRepository;

        public ManagerController(IJoinPaymentDetailRepository joinPaymentDetailRepository)
        {
            _joinPaymentDetailRepository = joinPaymentDetailRepository;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Payroll()
        {
            return View();
        }

        public IActionResult OvertimeSchedule()
        {
            return View();
        }

        public async Task<IActionResult> PayrollDetails(Guid guid)
        {
            var result = await _joinPaymentDetailRepository.GetDetails(guid);
            return View(result.Data);
        }

        public IActionResult Employees()
        {
            return View();
        }
    }
}
