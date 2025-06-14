using Hourly.Shared.Exceptions;
using Hourly.UserService.Abstractions.Contracts.Requests.UserContractRequests;
using Hourly.UserService.Abstractions.Services;
using Hourly.UserService.Application.Mappers;
using Microsoft.AspNetCore.Mvc;


namespace Hourly.UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContractController : Controller
    {
        private readonly IUserContractService _userContractService;
        private readonly ILogger<UserContractController> _logger;

        public UserContractController(IUserContractService userContractService, ILogger<UserContractController> logger)
        {
            _userContractService = userContractService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserContracts()
        {
            try
            {
                var userContracts = await _userContractService.GetAll();
                var response = userContracts.Select(userContract => userContract.ToResponse()).ToList();
                return Ok(response);
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

        [HttpGet("Filter")]
        public async Task<IActionResult> FilterUserContracts([FromQuery] Guid? userId, [FromQuery] int? year, [FromQuery] int? month, bool? isActive)
        {
            try
            {
                var results = await _userContractService.FilterUserContracts(userId, year, month, isActive);
                return Ok(results.Select(uc => uc.ToResponse()));
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
                _logger.LogError(ex, "An unexpected error occurred while filtering user contracts.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{userContractId}")]
        public async Task<IActionResult> GetUserContractById(Guid userContractId)
        {
            try
            {
                var result = await _userContractService.GetById(userContractId);
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
            catch (DomainValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving a userContract.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserContract([FromBody] CreateUserContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userContract = request.ToUserContract();

            try
            {
                var created = await _userContractService.Create(userContract);
                return CreatedAtAction(nameof(GetUserContractById), new { userContractId = created.Id }, created.ToResponse());
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
                // Log the exception details for diagnostics
                _logger.LogError(ex, "An unexpected error occurred while updating a userContract.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("{userContractId}")]
        public async Task<IActionResult> UpdateUserContract(Guid userContractId, [FromBody] UpdateUserContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userContract = request.ToUserContract(userContractId);

            try
            {
                var updated = await _userContractService.Update(userContract);
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
            catch (DomainValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating a userContract.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{userContractId}")]
        public async Task<IActionResult> DeleteUserContract(Guid userContractId)
        {
            try
            {
                await _userContractService.Delete(userContractId);
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
            catch (DomainValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting a userContract.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
