using Hourly.Domain.Mappers;
using Hourly.Shared.Exceptions;
using Hourly.TimeTrackingService.Application.Services;
using Hourly.TimeTrackingService.Contracts.Requests.WorkSessionRequests;
using Microsoft.AspNetCore.Mvc;

namespace Hourly.TimeTrackingService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkSessionController : Controller
    {
        private readonly IWorkSessionService _workSessionService;
        private readonly ILogger<WorkSessionController> _logger;

        public WorkSessionController(IWorkSessionService workSessionService, ILogger<WorkSessionController> logger)
        {
            _workSessionService = workSessionService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkSessions()
        {
            try
            {
                var workSessions = await _workSessionService.GetAll();
                return Ok(workSessions.Select(ws => ws.ToSummaryResponse()).ToList());
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
                _logger.LogError(ex, "An unexpected error occurred while retrieving work sessions by month.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> FilterWorkSessions([FromQuery] Guid? userContractId, [FromQuery] int? year, [FromQuery] int? month, bool? wbso)
        {
            try
            {
                var results = await _workSessionService.Filter(userContractId, year, month, wbso);
                return Ok(results.Select(ws => ws.ToResponse()));
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
                _logger.LogError(ex, "An unexpected error occurred while retrieving work sessions by month.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{workSessionId}")]
        public async Task<IActionResult> GetWorkSessionById(Guid workSessionId)
        {
            try
            {
                var result = await _workSessionService.GetById(workSessionId);
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
                _logger.LogError(ex, "An unexpected error occurred while retrieving a workSession.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkSession([FromBody] CreateWorkSessionRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var workSession = request.ToWorkSession();

            try
            {
                var created = await _workSessionService.Create(workSession, request.GitCommitIds);

                return CreatedAtAction(
                    nameof(GetWorkSessionById),       // must match method name exactly
                    new { workSessionId = created.Id },          // must match [HttpGet("{id}")]
                    created.ToResponse());            // payload returned in body
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
                _logger.LogError(ex, "An unexpected error occurred while creating a workSession.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }


        [HttpPost("{workSessionId}/AddGitCommit/{gitCommitId}")]
        public async Task<IActionResult> AddGitCommit(Guid workSessionId, Guid gitCommitId)
        {
            try
            {
                var workSession = await _workSessionService.AddGitCommit(workSessionId, gitCommitId);
                return Ok(workSession.ToResponse());
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
                _logger.LogError(ex, "An unexpected error occurred while adding a git commit to a work session.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost("{workSessionId}/RemoveGitCommit/{gitCommitId}")]
        public async Task<IActionResult> RemoveGitCommit(Guid workSessionId, Guid gitCommitId)
        {
            try
            {
                var workSession = await _workSessionService.RemoveGitCommit(workSessionId, gitCommitId);
                return Ok(workSession.ToResponse());
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
                _logger.LogError(ex, "An unexpected error occurred while removing a git commit from a work session.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("{workSessionId}")]
        public async Task<IActionResult> UpdateWorkSession(Guid workSessionId, [FromBody] UpdateWorkSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workSession = request.ToWorkSession(workSessionId);

            try
            {
                var updated = await _workSessionService.Update(workSession, request.GitCommitIds);
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
                _logger.LogError(ex, "An unexpected error occurred while updating a workSession.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPatch("{workSessionId}")]
        public async Task<IActionResult> UpdateWorkSessionLock(Guid workSessionId, [FromQuery] bool locked)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updated = await _workSessionService.UpdateLock(workSessionId, locked);
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
                _logger.LogError(ex, "An unexpected error occurred while updating the lock status of a work session.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{workSessionId}")]
        public async Task<IActionResult> DeleteWorkSession(Guid workSessionId)
        {
            try
            {
                await _workSessionService.Delete(workSessionId);
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
                _logger.LogError(ex, "An unexpected error occurred while deleting a workSession.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
