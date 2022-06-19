namespace Demo.Application.Jobs.Commands.CompleteJob
{
    public class CompleteJobCommand : ICommand
    {
        public CompleteJobCommand(Guid jobId)
        {
            JobId = jobId;
        }

        public Guid JobId { get; }
    }
}
