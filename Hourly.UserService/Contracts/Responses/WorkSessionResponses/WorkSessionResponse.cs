using Hourly.UserService.Contracts.Responses.UserContractResponses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.UserService.Contracts.Responses.WorkSessionResponses
{
    public class WorkSessionResponse : WorkSessionSummaryResponse
    {

        [ForeignKey("UserContractId")]
        public UserContractSummaryResponse? UserContract { get; init; }
    }
}
