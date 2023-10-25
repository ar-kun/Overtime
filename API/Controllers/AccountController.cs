using API.Contracts;
using API.DTOs.Accounts;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmailHandler _emailHandler;
        private readonly ITokenHandler _tokenHandler;

        public AccountController(IAccountRepository accountRepository, IAccountRoleRepository accountRoleRepository, IEmployeeRepository employeeRepository, IRoleRepository roleRepository, IEmailHandler emailHandler, ITokenHandler tokenHandler)
        {
            _accountRepository = accountRepository;
            _accountRoleRepository = accountRoleRepository;
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
            _emailHandler = emailHandler;
            _tokenHandler = tokenHandler;
        }

        [HttpPost("forgotpassword")] // Endpoint HTTP POST requests for forgotpassword.
        [AllowAnonymous] // Bypasses authorization statements.
        public IActionResult ForgotPassword(string email)
        {
            // Check if email already exist or not
            var employees = _employeeRepository.GetAll();
            var employee = employees.FirstOrDefault(e => e.Email == email);
            if (employee is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email not found"
                });
            }
            // Check if account already exist or not
            var account = _accountRepository.GetByGuid(employee.Guid);
            if (account is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Account not found"
                });
            }
            // Generate 6 random numbers
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            // Store the OTP in the database along with email and expiration time
            account.Otp = otp;
            account.ExpiredDate = DateTime.Now.AddMinutes(5);
            account.IsUsed = false;
            _accountRepository.Update(account);
            // Send OTP to smtp
            _emailHandler.Send("Forgot password", $"Your OTP is {account.Otp}", email);
            return Ok(new ResponseOKHandler<string>("OTP has been send to your email"));
        }

        [HttpPost("changepassword")] // Endpoint HTTP POST requests for changepassword.
        [AllowAnonymous] // Bypasses authorization statements.
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            try
            {
                // Get account by email
                var employees = _employeeRepository.GetAll();
                var employee = employees.FirstOrDefault(e => e.Email == changePasswordDto.Email);
                var account = _accountRepository.GetByGuid(employee.Guid);
                // Check if OTP is valid
                if (account == null || account.Otp != changePasswordDto.Otp)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Invalid OTP"
                    });
                }
                // Check if OTP has been used
                if (account.IsUsed)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "OTP has been used"
                    });
                }
                // Check if OTP has expired
                if (DateTime.Now > account.ExpiredDate)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "OTP has expired"
                    });
                }
                // Check if new password and confirm password match
                if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "New password and confirm password do not match"
                    });
                }
                // Assigns Guid, CreatedDate, and Password property of the toChangePassword object.
                Account toChangePassword = changePasswordDto;
                toChangePassword.Guid = account.Guid;
                toChangePassword.CreatedDate = account.CreatedDate;
                toChangePassword.Password = HashHandler.HashPassword(changePasswordDto.NewPassword);
                _accountRepository.Update(toChangePassword);
                return Ok(new ResponseOKHandler<string>("Password updated successfully"));
            }
            catch (ExceptionHandler ex)
            {
                // Returns a 500 Internal Server Error response with a new ResponseErrorHandler object.
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to change password",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("register")] // Endpoint HTTP POST requests for register.
        [AllowAnonymous] // Bypasses authorization statements.
        public IActionResult Register(RegisterDto registerDto)
        {
            using var _context = _accountRepository.GetContext();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Check if email already exists in the database
                    var employeeExists = _employeeRepository.GetAll();
                    var employeeExist = employeeExists.FirstOrDefault(e => e.Email == registerDto.Email);
                    if (employeeExist is not null)
                    {
                        return BadRequest(new ResponseErrorHandler
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Status = HttpStatusCode.BadRequest.ToString(),
                            Message = "Email already exists"
                        });
                    }
                    // Check if password and confirm password match
                    if (registerDto.Password != registerDto.ConfirmPassword)
                    {
                        return BadRequest(new ResponseErrorHandler
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Status = HttpStatusCode.BadRequest.ToString(),
                            Message = "Password and confirm password do not match"
                        });
                    }
                    // Create a new employee object 
                    var employeeToCreate = new Employee
                    {
                        FirstName = registerDto.FirstName,
                        LastName = registerDto.LastName,
                        BirthDate = registerDto.BirthDate,
                        Gender = registerDto.Gender,
                        HiringDate = registerDto.HiringDate,
                        Email = registerDto.Email,
                        PhoneNumber = registerDto.PhoneNumber,
                        Salary = registerDto.Salary
                    };
                    employeeToCreate.Nik = GenerateHandler.GenerateNik(_employeeRepository.GetLastNik());
                    var employeeResult = _employeeRepository.Create(employeeToCreate);
                    // Create a new account object
                    var accountToCreate = new Account
                    {
                        Guid = employeeResult.Guid,
                        Password = registerDto.Password,
                        Otp = 0,
                        IsUsed = true,
                        ExpiredDate = DateTime.Now
                    };
                    accountToCreate.Password = HashHandler.HashPassword(accountToCreate.Password);
                    var accountResult = _accountRepository.Create(accountToCreate);
                    // Create a new account role object
                    var accountRoleToCreatee = new AccountRole
                    {
                        AccountGuid = accountResult.Guid,
                        RoleGuid = _roleRepository.GetDefaultRoleGuid() ?? throw new Exception("Default Role Not Found")
                    };
                    var accountRoleResult = _accountRoleRepository.Create(accountRoleToCreatee);
                    // Save changes to database
                    _context.SaveChanges();
                    transaction.Commit();
                    // Return a success response
                    return Ok(new ResponseOKHandler<string>("User registered successfully"));
                }
                catch (ExceptionHandler ex)
                {
                    // Rollback transaction if there is an exception
                    transaction.Rollback();
                    // Returns a 500 Internal Server Error response with a new ResponseErrorHandler object
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status500InternalServerError,
                        Status = HttpStatusCode.InternalServerError.ToString(),
                        Message = "Failed to Register Account",
                        Error = ex.Message
                    });
                }
            }
        }

        [HttpPost("login")] // Endpoint HTTP POST requests for login.
        [AllowAnonymous] // Bypasses authorization statements.
        public IActionResult Login(LoginDto loginDto)
        {
            try
            {
                // Check if email already exist or not
                var employees = _employeeRepository.GetAll();
                var employee = employees.FirstOrDefault(e => e.Email == loginDto.Email);
                if (employee == null)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Email is invalid"
                    });
                }
                // Check if password is correct
                var account = _accountRepository.GetByGuid(employee.Guid);
                var verifyPassword = HashHandler.VerifyPassword(loginDto.Password, account.Password);
                if (!verifyPassword)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Password is invalid"
                    });
                }
                // Generates a token from the specified collection of claims using the token handler.
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Email, employee.Email));
                claims.Add(new Claim(ClaimTypes.Name, string.Concat(employee.FirstName + " " + employee.LastName)));
                // Add RoleName claim
                var getRoleName = from ar in _accountRoleRepository.GetAll()
                                  join r in _roleRepository.GetAll() on ar.RoleGuid equals r.Guid
                                  where ar.AccountGuid == account.Guid
                                  select r.Name;
                foreach (var roleName in getRoleName)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleName));
                }
                var generateToken = _tokenHandler.Generate(claims);
                // Return a success response
                return Ok(new ResponseOKHandler<object>("Login successfully", new { Token = generateToken }));
            }
            catch (ExceptionHandler ex)
            {
                // Returns a 500 Internal Server Error response with a new ResponseErrorHandler object.
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to Login",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("GetClaims/{token}")]
        public IActionResult GetClaims(string token)
        {
            var claims = _tokenHandler.ExtractClaimsFromJwt(token);
            return Ok(new ResponseOKHandler<ClaimsDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Claims has been retrieved",
                Data = claims
            });
        }

        [HttpGet] // Endpoint HTTP GET requests for GetAll()
        public IActionResult GetAll()
        {
            // Check if there is any data account
            var result = _accountRepository.GetAll();
            if (!result.Any())
            {
                // Returns a 404 Not Found response with code, status and message if the result is empty.
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            // Converts each Account object in the result to a AccountDto object
            var data = result.Select(x => (AccountDto)x);
            return Ok(new ResponseOKHandler<IEnumerable<AccountDto>>(data));
        }

        [HttpGet("{guid}")] // Endpoint HTTP GET requests with a GUID parameter in the URL.
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountRepository.GetByGuid(guid);
            if (result is null)
            {
                // Returns a 404 Not Found response with code, status and message if the result is empty.
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            // Returns a 200 OK response with the EmployeeDto object created from the result object.
            return Ok(new ResponseOKHandler<AccountDto>((AccountDto)result));
        }

        [HttpPost] // Endpoint HTTP POST requests for create account.
        public IActionResult Create(CreateAccountDto accountDto)
        {
            try
            {
                Account toCreate = accountDto;
                // Create hashing password
                toCreate.Password = HashHandler.HashPassword(accountDto.Password);
                var result = _accountRepository.Create(toCreate);
                return Ok(new ResponseOKHandler<AccountDto>((AccountDto)result));
            }
            catch (ExceptionHandler ex)
            {
                // Returns a 500 Internal Server Error response with a new ResponseErrorHandler object
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

        [HttpPut] // Endpoint HTTP PUT requests for update account
        public IActionResult Update(AccountDto accountDto)
        {
            try
            {
                var entity = _accountRepository.GetByGuid(accountDto.Guid);
                if (entity is null)
                {
                    // Returns a 404 Not Found response if the entity is null.
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                // Assigns the CreatedDate and Password property of the toUpdate object
                Account toUpdate = accountDto;
                toUpdate.CreatedDate = entity.CreatedDate;
                toUpdate.Password = HashHandler.HashPassword(accountDto.Password);
                // Calls the Update method of the _accountRepository field with the toUpdate object
                _accountRepository.Update(toUpdate);
                return Ok(new ResponseOKHandler<string>("Data Updated"));
            }
            catch (ExceptionHandler ex)
            {
                // Returns a 500 Internal Server Error response with a new ResponseErrorHandler object.
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete("{guid}")] // Endpoint HTTP DELETE requests for delete account
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var entity = _accountRepository.GetByGuid(guid);
                if (entity is null)
                {
                    // Returns a 404 Not Found response with a new ResponseErrorHandler object.
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                _accountRepository.Delete(entity);
                return Ok(new ResponseOKHandler<string>("Data Deleted"));
            }
            catch (ExceptionHandler ex)
            {
                // Returns a 500 Internal Server Error response with a new ResponseErrorHandler object.
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to delete data",
                    Error = ex.Message
                });
            }
        }
    }
}
