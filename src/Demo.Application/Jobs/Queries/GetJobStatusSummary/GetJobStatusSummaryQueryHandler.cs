using Demo.Application.Common.Interfaces;
using Demo.Application.Jobs.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Demo.Application.Jobs.Queries.GetJobStatusSummary
{
    public class GetJobStatusSummaryQueryHandler : IQueryHandler<GetJobStatusSummaryQuery, JobStatusSummaryDto>
    {
        private readonly IDemoDbContext _context;

        public GetJobStatusSummaryQueryHandler(IDemoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<JobStatusSummaryDto> Handle(GetJobStatusSummaryQuery request, CancellationToken cancellationToken)
        {
            //var query = from rt in _context.RoomTypes
            //            join j in _context.Jobs on rt.Id equals j.RoomTypeId
            //            group rt by rt.

            var resultList = await _context.Jobs.Include(j => j.RoomType)
                            .GroupBy(j => new { j.RoomType.Name, j.Status })
                            .Select(j => new RoomTypeStatusGroup
                            {
                                RoomType = j.Key.Name,
                                Status = j.Key.Status,
                                Count = j.Count(),
                            })
                            .ToListAsync(cancellationToken);

            var summary = new JobStatusSummaryDto();
            
            foreach(var result in resultList)
            {
                if (!summary.Summary.ContainsKey(result.RoomType))
                {
                    summary.Summary.Add(result.RoomType, new Dictionary<string, int>());
                }

                if (!summary.Summary[result.RoomType].ContainsKey(result.Status))
                {
                    summary.Summary[result.RoomType].Add(result.Status, result.Count);
                }
            }

            return summary;
        }

        private class RoomTypeStatusGroup
        {
            public string RoomType { get; set; }
            public string Status { get; set; }
            public int Count { get; set; }
        }
    }
}
