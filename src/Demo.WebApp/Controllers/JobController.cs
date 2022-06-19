using Demo.Application.Jobs.Queries.GetJobs;
using Demo.WebApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApp.Controllers
{
    public class JobController : Controller
    {
        private readonly IMediator _mediator;

        public JobController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetJobsQuery(), cancellationToken);
            return View(new JobListViewModel(result));
        }

        [HttpPost("Complete/{id}")]
        public async Task<IActionResult> Complete([FromRoute]Guid id, CancellationToken cancellationToken)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
