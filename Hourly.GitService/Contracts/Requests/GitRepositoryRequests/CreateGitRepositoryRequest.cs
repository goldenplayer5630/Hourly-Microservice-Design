using System.ComponentModel.DataAnnotations;

namespace Hourly.GitService.Contracts.Requests.GitRepositoryRequests
{
    public class CreateGitRepositoryRequest
    {
        [Required]
        public string ExtRepositoryId { get; init; } = string.Empty;

        [Required]
        public string Name { get; init; } = string.Empty;

        [Required]
        public string Namespace { get; init; } = string.Empty;

        [Required]
        public string WebUrl { get; init; } = string.Empty;
    }
}
