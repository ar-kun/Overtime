using API.Contracts;
using API.DTOs.Roles;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // Endpoint to retrieve all Role data
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roleRepository.GetAll();
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

            var data = result.Select(x => (RoleDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<RoleDto>>(data));  // Mengembalikan data Role jika ada
        }

        // Endpoint to retrieve Role data based on GUID
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _roleRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "ID Not Found"
                });  // Returns a message if the ID is not found
            }
            return Ok(new ResponseOKHandler<RoleDto>((RoleDto)result));  // Mengembalikan data Role jika ditemukan
        }

        // Endpoint for creating new Role data
        [HttpPost]
        public IActionResult Create(CreateRoleDto roleDto)
        {
            try
            {
                var result = _roleRepository.Create(roleDto);

                return Ok(new ResponseOKHandler<RoleDto>((RoleDto)result)); // Mengembalikan data Role yang baru saja dibuat
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

        // Endpoint to update Role data based on GUID
        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult Update(RoleDto roleDto)
        {
            try
            {
                var entity = _roleRepository.GetByGuid(roleDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Id Not Found"
                    });
                }

                Role toUpdate = roleDto;
                toUpdate.CreatedDate = entity.CreatedDate;

                _roleRepository.Update(toUpdate);

                return Ok(new ResponseOKHandler<RoleDto>("Data updated successfully")); // Mengembalikan pesan sukses jika pembaruan berhasil
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

        // Endpoint to delete Role data based on GUID
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var existingRole = _roleRepository.GetByGuid(guid);
                if (existingRole is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Id Not Found"
                    });
                }

                _roleRepository.Delete(existingRole);

                return Ok(new ResponseOKHandler<RoleDto>("Data deleted successfully"));  // Mengembalikan pesan sukses jika penghapusan berhasil
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