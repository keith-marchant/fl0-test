using Demo.Application.Jobs.Dtos;
using Demo.Application.Jobs.Queries.GetJobStatusSummary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobStatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobStatusController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Get a summary of the jobs by status
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>'
        [ProducesResponseType(200, Type = typeof(JobStatusSummaryDto))]
        [HttpGet]
        public async Task<ActionResult<JobStatusSummaryDto>> GetJobStatusSummary(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetJobStatusSummaryQuery(), cancellationToken);
            return Ok(result);
        }
    }
}
