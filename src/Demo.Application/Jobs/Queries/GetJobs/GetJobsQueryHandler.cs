using AutoMapper;
using Demo.Application.Common.Interfaces;
using Demo.Application.Jobs.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Demo.Application.Jobs.Queries.GetJobs
{
    public class GetJobsQueryHandler : IQueryHandler<GetJobsQuery, IList<JobDto>>
    {
        private readonly IDemoDbContext _context;
        private readonly IMapper _mapper;

        public GetJobsQueryHandler(IDemoDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IList<JobDto>> Handle(GetJobsQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _context.Jobs.Include(jobs => jobs.RoomType).ToListAsync();

            return _mapper.Map<List<JobDto>>(jobs);
        }
    }
}
