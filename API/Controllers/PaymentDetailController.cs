﻿using API.Contracts;
using API.DTOs.PaymentDetails;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentDetailController : ControllerBase
    {
        private readonly IPaymentDetailRepository _paymentDetailRepository;

        public PaymentDetailController(IPaymentDetailRepository paymentDetailRepository)
        {
            _paymentDetailRepository = paymentDetailRepository;
        }

        [HttpGet] // Endpoint HTTP GET requests for GetAll()
        public IActionResult GetAll()
        {
            // Check if there is any data payment detail
            var result = _paymentDetailRepository.GetAll();
            if (!result.Any())
            {
                // Returns a 404 Not Found response with code, status and message if the result is empty
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            // Converts each PaymentDetail object in the result to a PaymentDetailDto object
            var data = result.Select(x => (PaymentDetailDto)x);
            return Ok(new ResponseOKHandler<IEnumerable<PaymentDetailDto>>(data));
        }

        [HttpGet("{guid}")] // Endpoint HTTP GET requests with a GUID parameter in the URL
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _paymentDetailRepository.GetByGuid(guid);
            if (result is null)
            {
                // Returns a 404 Not Found response with code, status and message if the result is empty
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            // Returns a 200 OK response with the PaymentDetailDto object created from the result object
            return Ok(new ResponseOKHandler<PaymentDetailDto>((PaymentDetailDto)result));
        }

        [HttpPost] // Endpoint HTTP POST requests for create payment detail
        public IActionResult Create(CreatePaymentDetailDto createPaymentDetailDto)
        {
            try
            {
                var result = _paymentDetailRepository.Create(createPaymentDetailDto);
                return Ok(new ResponseOKHandler<PaymentDetailDto>((PaymentDetailDto)result));
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

        [HttpPut] // Endpoint HTTP PUT requests for update payment detail
        public IActionResult Update(PaymentDetailDto paymentDetailDto)
        {
            try
            {
                var entity = _paymentDetailRepository.GetByGuid(paymentDetailDto.Guid);
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
                PaymentDetail toUpdate = paymentDetailDto;
                _paymentDetailRepository.Update(toUpdate);
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

        [HttpDelete("{guid}")] // Endpoint HTTP DELETE requests for delete payment detail
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var entity = _paymentDetailRepository.GetByGuid(guid);
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
                _paymentDetailRepository.Delete(entity);
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
