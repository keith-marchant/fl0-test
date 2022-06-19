using Demo.Application.Jobs.Commands.CompleteJob;

namespace Demo.Application.Tests.Jobs
{
    public class CompleteJobCommandHandlerTests : UnitTestBase
    {
        [Fact]
        public async Task GivenIncompleteJob_WhenHandled_ThenJobIsComplete()
        {
            var jobId = Guid.Parse("00cc80fc-060a-4814-975f-001963b6503a");

            Context.Jobs.Add(new Entities.Job
            {
                Id = jobId,
                ContractorId = null,
                Name = "Job1_0_0",
                Status = "Delayed",
                Floor = 1,
                Room = 0,
                DelayReason = null,
                DateCreated = DateTime.Now,
                DateCompleted = null,
                DateDelayed = null,
                StatusNum = 4,
                RJobId = null,
                RoomType = new Entities.RoomType
                {
                    Id = Guid.NewGuid(),
                    Name = "QQ",
                    Description = "Common Area",
                },
            });
            await Context.SaveChangesAsync();

            var command = new CompleteJobCommand(jobId);

            var handler = new CompleteJobCommandHandler(Context);

            await handler.Handle(command, CancellationToken.None);

            var job = Context.Jobs.First(j => j.Id == jobId);

            job.Status.Should().Be("Complete");
            job.StatusNum.Should().Be(1);
        }
    }
}
