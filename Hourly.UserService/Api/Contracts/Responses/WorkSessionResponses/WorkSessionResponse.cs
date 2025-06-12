using Hourly.Domain.Contracts.Responses.GitCommitResponses;
using Hourly.UserService.Api.Contracts.Responses.UserContractResponses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Domain.Contracts.Responses.WorkSessionResponses
{
    public class WorkSessionResponse : WorkSessionSummaryResponse
    {

        [ForeignKey("UserContractId")]
        public UserContractSummaryResponse? UserContract { get; set; }
    }
}
