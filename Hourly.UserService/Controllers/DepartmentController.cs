using Hourly.Shared.Exceptions;
using Hourly.UserService.Application.Mappers;
using Hourly.UserService.Application.Services;
using Hourly.UserService.Contracts.Requests.DepartmentRequests;
using Microsoft.AspNetCore.Mvc;



namespace Hourly.UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var departments = await _departmentService.GetAll();
                return Ok(departments.Select(d => d.ToResponse()).ToList());
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving a gitRepository.");
                return StatusCode(500, "An unexpected error occurred.");
            }

        }

        [HttpGet("{departmentId}")]
        public async Task<IActionResult> GetDepartmentById(Guid departmentId)
        {
            try
            {
                var result = await _departmentService.GetById(departmentId);
                return Ok(result.ToResponse());
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving a department.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var department = request.ToDepartment();

            try
            {
                var created = await _departmentService.Create(department);
                return CreatedAtAction(nameof(GetDepartmentById), new { departmentId = created.Id }, created.ToResponse());
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception details for diagnostics
                _logger.LogError(ex, "An unexpected error occurred while updating a department.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("{departmentId}")]
        public async Task<IActionResult> UpdateDepartment(Guid departmentId, [FromBody] UpdateDepartmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var department = request.ToDepartment(departmentId);

            try
            {
                var updated = await _departmentService.Update(department);
                return Ok(updated.ToResponse());
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating a department.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{departmentId}")]
        public async Task<IActionResult> DeleteDepartment(Guid departmentId)
        {
            try
            {
                await _departmentService.Delete(departmentId);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting a department.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
