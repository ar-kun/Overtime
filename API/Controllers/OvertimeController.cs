using API.Contracts;
using API.DTOs.Overtimes;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OvertimeController : ControllerBase
    {
        private readonly IOvertimeRepository _overtimeRepository;

        public OvertimeController(IOvertimeRepository overtimeRepository)
        {
            _overtimeRepository = overtimeRepository;
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
            // Mengembalikan data Employee jika ditemukan
            return Ok(new ResponseOKHandler<OvertimeDto>((OvertimeDto)result));
        }

        // Endpoint for creating new Overtime data
        [HttpPost]
        public IActionResult Create(StoreOvertimeDto createOvertimeDto)
        {
            try
            {
                var result = _overtimeRepository.Create(createOvertimeDto);

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
        public IActionResult Update(StoreOvertimeDto updateOvertimeDto)
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