using Demo.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Demo.Application.Jobs.Commands.CompleteJob
{
    public class CompleteJobCommandHandler : ICommandHandler<CompleteJobCommand>
    {
        private readonly IDemoDbContext _context;

        public CompleteJobCommandHandler(IDemoDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CompleteJobCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var job = await _context.Jobs.FirstAsync(j => j.Id == request.JobId, cancellationToken);

            job.Status = "Complete";
            job.StatusNum = 1;

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
