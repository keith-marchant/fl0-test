using Demo.Application.Common.Interfaces;
using Demo.Application.Jobs.Dtos;

namespace Demo.Application.Jobs.Queries.GetJobs
{
    public class GetJobsQueryHandler : IQueryHandler<GetJobsQuery, IList<JobDto>>
    {
        private readonly IDemoDbContext _context;

        public GetJobsQueryHandler(IDemoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<IList<JobDto>> Handle(GetJobsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
