using API.Contracts;
using API.DTOs.Overtimes;
using API.Models;
using API.Utilities.Enums;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Principal;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OvertimeController : ControllerBase
    {
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPaymentDetailRepository _paymentDetailRepository;
        private readonly IEmailHandler _emailHandler;

        public OvertimeController(IOvertimeRepository overtimeRepository, IEmployeeRepository employeeRepository, IPaymentDetailRepository paymentDetailRepository, IEmailHandler emailHandler)
        {
            _overtimeRepository = overtimeRepository;
            _employeeRepository = employeeRepository;
            _paymentDetailRepository = paymentDetailRepository;
            _emailHandler = emailHandler;
        }

        // Endpoint to display Overtime details by ManagerGuid
        [HttpGet("manager-guid/{guid}")]
        public IActionResult GetAllByManagerGuid(Guid guid)
        {
            var overtimeRequests = _overtimeRepository.GetByManagerGuid(guid);
            var employees = _employeeRepository.GetAll();

            if (!overtimeRequests.Any())
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "No Overtime Requests found"
                });
            }

            // Filter and update Overtimes Status
            foreach (var overtime in overtimeRequests)
            {
                if (overtime.Status == StatusLevel.WaitingForPayment)
                {
                    // Update the status to 'Finished'
                    var paymentDetail = _paymentDetailRepository.GetByGuid(overtime.Guid);
                    if(paymentDetail.PaymentStatus == PaymentLevel.Paid)
                    {
                        overtime.Status = StatusLevel.Finished;
                        _overtimeRepository.Update(overtime);
                    }
                }
                else if (overtime.Status == StatusLevel.OnGoing && overtime.DateRequest.Date < DateTime.Now.Date)
                {
                    // Update the status to 'WaitingForPayment'
                    overtime.Status = StatusLevel.OnGoing;
                    _overtimeRepository.Update(overtime);
                }
                else if (overtime.Status == StatusLevel.Approved && overtime.DateRequest.Date == DateTime.Now.Date)
                {
                    // Update the status to 'OnGoing'
                    overtime.Status = StatusLevel.OnGoing;
                    _overtimeRepository.Update(overtime);
                }
            }

            var overtimeDetails = from o in overtimeRequests
                                  join e in employees on o.EmployeeGuid equals e.Guid
                                  join m in employees on e.ManagerGuid equals m.Guid
                                  select new OvertimeReqDetailDto
                                  {
                                      Guid = o.Guid,
                                      EmployeeGuid = o.EmployeeGuid,
                                      FullName = string.Concat(e.FirstName, " ", e.LastName),
                                      Gender = e.Gender.ToString(),
                                      Email = e.Email,
                                      PhoneNumber = e.PhoneNumber,
                                      Salary = e.Salary,
                                      ManagerGuid = e.ManagerGuid,
                                      ManagerFullName = string.Concat(m.FirstName, " ", m.LastName),
                                      DateRequest = o.DateRequest,
                                      Duration = o.Duration,
                                      Status = o.Status.ToString(),
                                      Remarks = o.Remarks,
                                      TypeOfDay = o.TypeOfDay.ToString(),
                                  };
            return Ok(new ResponseOKHandler<IEnumerable<OvertimeReqDetailDto>>(overtimeDetails));
        }

        // Endpoint to display all Overtime details
        [HttpGet("req-details")]
        public IActionResult GetDetails()
        {
            var overtimes = _overtimeRepository.GetAll();
            var employees = _employeeRepository.GetAll();

            if (!(employees.Any() && overtimes.Any()))
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            // Filter and update Overtimes Status
            foreach (var overtime in overtimes)
            {
                if (overtime.Status == StatusLevel.WaitingForPayment)
                {
                    // Update the status to 'Finished'
                    var paymentDetail = _paymentDetailRepository.GetByGuid(overtime.Guid);
                    if (paymentDetail.PaymentStatus == PaymentLevel.Paid)
                    {
                        overtime.Status = StatusLevel.Finished;
                        _overtimeRepository.Update(overtime);
                    }
                }
                else if (overtime.Status == StatusLevel.OnGoing && overtime.DateRequest.Date < DateTime.Now.Date)
                {
                    // Update the status to 'WaitingForPayment'
                    overtime.Status = StatusLevel.OnGoing;
                    _overtimeRepository.Update(overtime);
                }
                else if (overtime.Status == StatusLevel.Approved && overtime.DateRequest.Date == DateTime.Now.Date)
                {
                    // Update the status to 'OnGoing'
                    overtime.Status = StatusLevel.OnGoing;
                    _overtimeRepository.Update(overtime);
                }
            }

            var overtimeDetails = from o in overtimes
                                  join e in employees on o.EmployeeGuid equals e.Guid
                                  join m in employees on e.ManagerGuid equals m.Guid
                                  select new OvertimeReqDetailDto
                                  {
                                      Guid = o.Guid,
                                      EmployeeGuid = o.EmployeeGuid,
                                      FullName = string.Concat(e.FirstName, " ", e.LastName),
                                      Gender = e.Gender.ToString(),
                                      Email = e.Email,
                                      PhoneNumber = e.PhoneNumber,
                                      Salary = e.Salary,
                                      ManagerGuid = e.ManagerGuid,
                                      ManagerFullName = string.Concat(m.FirstName, " ", m.LastName),
                                      DateRequest = o.DateRequest,
                                      Duration = o.Duration,
                                      Status = o.Status.ToString(),
                                      Remarks = o.Remarks,
                                      TypeOfDay = o.TypeOfDay.ToString(),
                                  };

            return Ok(new ResponseOKHandler<IEnumerable<OvertimeReqDetailDto>>(overtimeDetails));
        }

        // Endpoint to display Overtime request by EmployeeGuid
        [HttpGet("employee-guid/{guid}")]
        public IActionResult GetAllByEmployeeGuid(Guid guid)
        {
            var employeeOvertimeRequests = _overtimeRepository.GetByEmployeeGuid(guid);
            if (!employeeOvertimeRequests.Any())
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "No Overtime Requests found"
                });
            }
            var data = employeeOvertimeRequests.Select(x => (OvertimeDto)x);
            return Ok(new ResponseOKHandler<IEnumerable<OvertimeDto>>(data));
        }

        // Endpoint to retrieve all Overtime data
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _overtimeRepository.GetAll();

            if (!result.Any())
            {
                // Mengembalikan pesan jika tidak ada data yang ditemukan
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            // Filter and update Overtimes Status
            foreach (var overtime in result)
            {
                if (overtime.Status == StatusLevel.WaitingForPayment)
                {
                    // Update the status to 'Finished'
                    var paymentDetail = _paymentDetailRepository.GetByGuid(overtime.Guid);
                    if (paymentDetail.PaymentStatus == PaymentLevel.Paid)
                    {
                        overtime.Status = StatusLevel.Finished;
                        _overtimeRepository.Update(overtime);
                    }
                }
                else if (overtime.Status == StatusLevel.OnGoing && overtime.DateRequest.Date < DateTime.Now.Date)
                {
                    // Update the status to 'WaitingForPayment'
                    overtime.Status = StatusLevel.OnGoing;
                    _overtimeRepository.Update(overtime);
                }
                else if (overtime.Status == StatusLevel.Approved && overtime.DateRequest.Date == DateTime.Now.Date)
                {
                    // Update the status to 'OnGoing'
                    overtime.Status = StatusLevel.OnGoing;
                    _overtimeRepository.Update(overtime);
                }
            }
            
            var data = result.Select(x => (OvertimeDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<OvertimeDto>>(data));
        }

        // Endpoint to retrieve Overtime data based on GUID
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _overtimeRepository.GetByGuid(guid);
            if (result is null)
            {
                // Mengembalikan pesan jika ID tidak ditemukan
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "ID Not Found"
                });
            }

            // Filter and update Overtimes Status
            if (result.Status == StatusLevel.WaitingForPayment)
            {
                // Update the status to 'Finished'
                var paymentDetail = _paymentDetailRepository.GetByGuid(result.Guid);
                if (paymentDetail.PaymentStatus == PaymentLevel.Paid)
                {
                    result.Status = StatusLevel.Finished;
                    _overtimeRepository.Update(result);
                }
            }
            else if (result.Status == StatusLevel.Approved && result.DateRequest.Date == DateTime.Now.Date)
            {
                // Update the status to 'WaitingForPayment'
                result.Status = StatusLevel.OnGoing;
                _overtimeRepository.Update(result);
            }
            else if (result.Status == StatusLevel.Approved && result.DateRequest.Date == DateTime.Now.Date)
            {
                // Update the status to 'OnGoing'
                result.Status = StatusLevel.OnGoing;
                _overtimeRepository.Update(result);
            }

            // Mengembalikan data Employee jika ditemukan
            return Ok(new ResponseOKHandler<OvertimeDto>((OvertimeDto)result));
        }

        // Endpoint for creating new Overtime request
        [HttpPost]
        public IActionResult Create(CreateOvertimeDto createOvertimeDto)
        {
            try
            {
                var result = _overtimeRepository.Create(createOvertimeDto);
                var employee = _employeeRepository.GetByGuid(createOvertimeDto.EmployeeGuid);
                var managerEmail = _employeeRepository.GetEmail(employee.ManagerGuid);

                // Send Request to smtp
                //_emailHandler.Send("Overtime Request", 
                                    //    $"Hello sir, {string.Concat(employee.FirstName + " " + employee.LastName)} has just submitted a request for overtime and waiting for a response from you. Thank You :)",
                                     //   managerEmail);

                // Mengembalikan data Employee yang baru saja dibuat
                return Ok(new ResponseOKHandler<OvertimeDto>((OvertimeDto)result));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

        // Endpoint to update Overtime data based on GUID
        [HttpPut]
        public IActionResult Update(UpdateOvertimeDto updateOvertimeDto)
        {
            try
            {
                var entity = _overtimeRepository.GetByGuid(updateOvertimeDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Id Not Found"
                    });
                }

                Overtime toUpdate = updateOvertimeDto;
                _overtimeRepository.Update(toUpdate);

                return Ok(new ResponseOKHandler<OvertimeDto>("Data updated successfully")); // Mengembalikan pesan sukses jika pembaruan berhasil
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to update data",
                    Error = ex.Message
                });
            }
        }

        // Endpoint to delete Overtime data based on GUID
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var existingBooking = _overtimeRepository.GetByGuid(guid);
                if (existingBooking is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Id Not Found"
                    });
                }

                _overtimeRepository.Delete(existingBooking);

                return Ok(new ResponseOKHandler<OvertimeDto>("Data deleted successfully"));  // Mengembalikan pesan sukses jika penghapusan berhasil
            }
            catch (ExceptionHandler ex)
            {
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