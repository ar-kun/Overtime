using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IJoinPaymentDetailRepository _joinPaymentDetailRepository;

        public EmployeeController(IJoinPaymentDetailRepository joinPaymentDetailRepository)
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

        public IActionResult MyOvertime()
        {
            return View();
        }

        public async Task<IActionResult> PayrollDetails(Guid guid)
        {
            var result = await _joinPaymentDetailRepository.GetDetails(guid);
            return View(result.Data);
        }
    }
}
