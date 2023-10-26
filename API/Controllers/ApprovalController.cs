using API.Contracts;
using API.DTOs.Approvals;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApprovalController : ControllerBase
    {
        private readonly IApprovalRepository _approvalRepository;

        public ApprovalController(IApprovalRepository approvalRepository)
        {
            _approvalRepository = approvalRepository;
        }

        [HttpGet] // Endpoint HTTP GET requests for GetAll()
        public IActionResult GetAll()
        {
            // Check if there is any data approval
            var result = _approvalRepository.GetAll();
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
            // Converts each Approval object in the result to a ApprovalDto object
            var data = result.Select(x => (ApprovalDto)x);
            return Ok(new ResponseOKHandler<IEnumerable<ApprovalDto>>(data));
        }

        [HttpGet("{guid}")] // Endpoint HTTP GET requests with a GUID parameter in the URL.
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _approvalRepository.GetByGuid(guid);
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
            // Returns a 200 OK response with the ApprovalDto object created from the result object.
            return Ok(new ResponseOKHandler<ApprovalDto>((ApprovalDto)result));
        }

        [HttpPost] // Endpoint HTTP POST requests for create account
        public IActionResult Create(CreateApprovalDto createApprovalDto)
        {
            try
            {
                var result = _approvalRepository.Create(createApprovalDto);
                return Ok(new ResponseOKHandler<ApprovalDto>((ApprovalDto)result));
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

        [HttpPut] // Endpoint HTTP PUT requests for update approval
        public IActionResult Update(ApprovalDto approvalDto)
        {
            try
            {
                var entity = _approvalRepository.GetByGuid(approvalDto.Guid);
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
                Approval toUpdate = approvalDto;
                _approvalRepository.Update(toUpdate);
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

        [HttpDelete("{guid}")] // Endpoint HTTP DELETE requests for delete approval
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var entity = _approvalRepository.GetByGuid(guid);
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
                _approvalRepository.Delete(entity);
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
