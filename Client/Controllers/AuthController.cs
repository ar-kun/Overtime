using API.DTOs.Accounts;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AuthController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginDto login)
        {
            var result = await _accountRepository.Login(login);

            if (result.Status == "OK")
            {
                HttpContext.Session.SetString("JWToken", result.Data.Token);
                var JWTokenn = result.Data.Token;
                var dataClaims = await _accountRepository.Claims(JWTokenn);
                var role = dataClaims.Data.Role;
                if (role.Contains("Employee") && !role.Contains("Manager") && !role.Contains("Payroll"))
                {
                    return RedirectToAction("Dashboard", "Employee");
                }
                else if (role.Contains("Employee") && role.Contains("Manager"))
                {
                    // Display a page that allows the user to choose which dashboard to go to
                    return View("ChooseDashboardManager");
                }
                else if (role.Contains("Employee") && role.Contains("Payroll"))
                {
                    // Display a page that allows the user to choose which dashboard to go to
                    return View("ChooseDashboardPayroll");
                }
                else
                {
                    // Handle the case where the user doesn't have any roles
                    return View("NoAccess");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Logout/")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
