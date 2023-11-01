using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class PayrollController : Controller
    {
        private readonly IPaymentDetailRepository _paymentDetailRepository;

        public PayrollController(IPaymentDetailRepository paymentDetailRepository)
        {
            _paymentDetailRepository = paymentDetailRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetPayrollList()
        {
            var result = await _paymentDetailRepository.Get();
            return Json(result);
        }

        public async Task<IActionResult> Details(Guid guid)
        {
            var result = await _paymentDetailRepository.Get(guid);
            return View(result.Data);
        }
    }
}
