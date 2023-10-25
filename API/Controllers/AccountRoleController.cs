using API.Contracts;
using API.DTOs.AccountRoles;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleRepository _accountRoleRepository;

        public AccountRoleController(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        // Endpoint to retrieve all AccountRole data
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountRoleRepository.GetAll();
            if (!result.Any())
            {
                // Returns a message if no data is found
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            var data = result.Select(x => (AccountRoleDto)x);

            // Returns AccountRole data if any
            return Ok(new ResponseOKHandler<IEnumerable<AccountRoleDto>>(data));
        }

        // Endpoint to retrieve AccountRole data based on GUID
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountRoleRepository.GetByGuid(guid);
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
            // Mengembalikan data Employee jika ditemukan
            return Ok(new ResponseOKHandler<AccountRoleDto>((AccountRoleDto)result));
        }

        // Endpoint for creating new AccountRole data
        [HttpPost]
        public IActionResult Create(CreateAccountRoleDto accountRoleDto)
        {
            try
            {
                var result = _accountRoleRepository.Create(accountRoleDto);

                // Returns the AccountRole data that was just created
                return Ok(new ResponseOKHandler<AccountRoleDto>((AccountRoleDto)result));
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

        // Endpoint for updating AccountRole data based on GUID
        [HttpPut]
        public IActionResult Update(AccountRoleDto accountRoleDto)
        {
            try
            {
                var entity = _accountRoleRepository.GetByGuid(accountRoleDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Id Not Found"
                    });
                }

                AccountRole toUpdate = accountRoleDto;
                toUpdate.CreatedDate = entity.CreatedDate;
                _accountRoleRepository.Update(toUpdate);

                return Ok(new ResponseOKHandler<AccountRoleDto>("Data updated successfully")); // Returns a success message if the update is successful
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

        // Endpoint to delete AccountRole data based on GUID
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var existingAccountRole = _accountRoleRepository.GetByGuid(guid);
                if (existingAccountRole is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Id Not Found"
                    });
                }
                _accountRoleRepository.Delete(existingAccountRole);

                return Ok(new ResponseOKHandler<AccountRoleDto>("Data deleted successfully"));  // Returns a success message if deletion is successful
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