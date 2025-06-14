using Hourly.GitService.Abstractions.Services;
using Hourly.GitService.Api.Contracts.Requests.GitCommitRequests;
using Hourly.GitService.Application.Mappers;
using Hourly.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Hourly.GitService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GitCommitController : Controller
    {
        private readonly IGitCommitService _gitCommitService;
        private readonly ILogger<GitCommitController> _logger;

        public GitCommitController(IGitCommitService gitCommitService, ILogger<GitCommitController> logger)
        {
            _gitCommitService = gitCommitService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGitCommits()
        {
            try
            {
                var gitCommits = await _gitCommitService.GetAll();
                return Ok(gitCommits.Select(gc => gc.ToResponse()).ToList());
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

        [HttpGet("{gitCommitId}")]
        public async Task<IActionResult> GetGitCommitById(Guid gitCommitId)
        {
            try
            {
                var result = await _gitCommitService.GetById(gitCommitId);
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
                _logger.LogError(ex, "An unexpected error occurred while retrieving a gitCommit.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> FilterGitCommits([FromQuery] Guid? repositoryId, [FromQuery] Guid? authorId, [FromQuery] DateTime? authoredDate)
        {
            try
            {
                var results = await _gitCommitService.Filter(repositoryId, authorId, authoredDate);
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

        [HttpPost]
        public async Task<IActionResult> CreateGitCommit([FromBody] CreateGitCommitRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gitCommit = request.ToGitCommit();

            try
            {
                var created = await _gitCommitService.Create(gitCommit);
                return CreatedAtAction(nameof(GetGitCommitById), new { gitCommitId = created.Id }, created.ToResponse());
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
                _logger.LogError(ex, "An unexpected error occurred while updating a gitCommit.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{gitCommitId}")]
        public async Task<IActionResult> DeleteGitCommit(Guid gitCommitId)
        {
            try
            {
                await _gitCommitService.Delete(gitCommitId);
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
                _logger.LogError(ex, "An unexpected error occurred while deleting a gitCommit.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
