using API.Contracts;
using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Endpoint to retrieve all Employee data
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _employeeRepository.GetAll();
            if (!result.Any())
            {
                // Mengembalikan pesan jika tidak ada data yang ditemukan menggunakan error handler
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            var data = result.Select(x => (EmployeeDto)x);

            // Mengembalikan data Employee jika ada menggunakan handler
            return Ok(new ResponseOKHandler<IEnumerable<EmployeeDto>>(data));
        }

        // Endpoint to retrieve Employee data based on GUID
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _employeeRepository.GetByGuid(guid);
            if (result is null)
            {
                // Mengembalikan pesan jika ID tidak ditemukan menggunakan error handler
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "ID Not Found"
                });
            }

            // Mengembalikan data Employee jika ditemukan
            return Ok(new ResponseOKHandler<EmployeeDto>((EmployeeDto)result));
        }

        // Endpoint for creating new Employee data
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto createEmployeeDto)
        {
            try
            {
                Employee toCreate = createEmployeeDto;
                toCreate.Nik = GenerateHandler.GenerateNIK(_employeeRepository.GetLastNik());
                var result = _employeeRepository.Create(toCreate);

                // Returns the Employee data that was just created
                return Ok(new ResponseOKHandler<EmployeeDto>("Employee added successfully", (EmployeeDto)result));
            }
            catch (ExceptionHandler ex)
            {
                // Displays an error message if there is an error on the server/database
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to add employee data",
                    Error = ex.Message
                });
            }
        }

        // Endpoint to update Employee data based on GUID
        [HttpPut]
        public IActionResult Update(UpdateEmployeeDto employeeDto)
        {
            try
            {
                var entity = _employeeRepository.GetByGuid(employeeDto.Guid);
                if (entity is null)
                {
                    // Returns a message if the ID is not found using the error handler
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Id Not Found"
                    });
                }

                Employee toUpdate = employeeDto;
                toUpdate.CreatedDate = entity.CreatedDate;
                //toUpdate.Nik = entity.Nik;

                _employeeRepository.Update(toUpdate);

                return Ok(new ResponseOKHandler<EmployeeDto>("Employee data updated successfully")); // Mengembalikan pesan sukses jika pembaruan berhasil
            }
            catch (ExceptionHandler ex)
            {
                // Displays an error message if there is an error on the server/database
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to update data",
                    Error = ex.Message
                });
            }
        }

        // Endpoint to delete Employee data based on GUID
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var existingEmployee = _employeeRepository.GetByGuid(guid);
                if (existingEmployee is null)
                {
                    // Returns a message if the ID is not found using the error handler
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Id Not Found"
                    });
                }
                _employeeRepository.Delete(existingEmployee);

                return Ok(new ResponseOKHandler<EmployeeDto>("Data deleted successfully"));  // Mengembalikan pesan sukses jika penghapusan berhasil
            }
            catch (ExceptionHandler ex)
            {
                // Displays an error message if there is an error on the server/database
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