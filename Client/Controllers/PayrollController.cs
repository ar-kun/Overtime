using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class PayrollController : Controller
    {
        private readonly IPaymentDetailRepository _paymentDetailRepository;
        private readonly IJoinPaymentDetailRepository _joinPaymentDetailRepository;

        public PayrollController(IPaymentDetailRepository paymentDetailRepository, IJoinPaymentDetailRepository joinPaymentDetailRepository)
        {
            _paymentDetailRepository = paymentDetailRepository;
            _joinPaymentDetailRepository = joinPaymentDetailRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetPayrollList()
        {
            var result = await _joinPaymentDetailRepository.GetDetails();
            return Json(result);
        }

        public async Task<IActionResult> Details(Guid guid)
        {
            var result = await _joinPaymentDetailRepository.GetDetails(guid);
            return View(result.Data);
        }

        // Edit object view using HttpClient
        [HttpGet]
        public async Task<IActionResult> Edit(Guid guid)
        {
            var paymentDetail = await _paymentDetailRepository.Get(guid);
            return View(paymentDetail.Data);
        }
    }
}
