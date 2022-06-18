using Demo.Application.Jobs.Dtos;

namespace Demo.WebApp.Models
{
    public class JobListViewModel
    {
        public JobListViewModel(IList<JobDto> jobs)
        {
            Jobs = jobs;
        }

        public IList<JobDto> Jobs { get; }
    }
}
