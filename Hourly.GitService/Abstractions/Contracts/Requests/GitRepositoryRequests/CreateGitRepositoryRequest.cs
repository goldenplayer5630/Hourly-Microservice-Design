using System.ComponentModel.DataAnnotations;

namespace Hourly.GitService.Api.Contracts.Requests.GitRepositoryRequests
{
    public class CreateGitRepositoryRequest
    {
        [Required]
        public string ExtRepositoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Namespace { get; set; }

        [Required]
        public string WebUrl { get; set; }
    }
}
