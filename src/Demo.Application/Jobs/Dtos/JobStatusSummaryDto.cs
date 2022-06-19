namespace Demo.Application.Jobs.Dtos
{
    public class JobStatusSummaryDto
    {
        public JobStatusSummaryDto()
        {
            Summary = new Dictionary<string, Dictionary<string, int>>();
        }

        public Dictionary<string, Dictionary<string, int>> Summary { get; }
    }
}
