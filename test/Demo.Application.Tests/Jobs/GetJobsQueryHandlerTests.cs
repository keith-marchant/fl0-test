using Demo.Application.Jobs.Queries.GetJobs;
using FluentAssertions;

namespace Demo.Application.Tests.Jobs;

public class GetJobsQueryHandlerTests : UnitTestBase
{
    public GetJobsQueryHandlerTests() : base()
    {
    }

    [Fact]
    public async Task GivenJobsQuery_WhenNoResults_ThenEmptyListReturned()
    {
        var query = new GetJobsQuery();
        var handler = new GetJobsQueryHandler(Context, Mapper);

        var response = await handler.Handle(query, CancellationToken.None);

        response.Should().NotBeNull();
        response.Count.Should().Be(0);
    }

    [Fact]
    public async Task GivenJobsQuery_WhenResultsAvailable_ThenPopulatedListReturned()
    {
        Context.Jobs.Add(new Entities.Job
        {
            Id = Guid.NewGuid(),
            ContractorId = null,
            Name = "Job1_0_0",
            Status = "In Progress",
            Floor = 1,
            Room = 0,
            DelayReason = null,
            DateCreated = DateTime.Now,
            DateCompleted = null,
            DateDelayed = null,
            StatusNum = 3,
            RJobId = null,
            RoomType = new Entities.RoomType
            {
                Id = Guid.NewGuid(),
                Name = "QQ",
                Description = "Common Area",
            },
        });
        await Context.SaveChangesAsync();
        
        var query = new GetJobsQuery();
        var handler = new GetJobsQueryHandler(Context, Mapper);

        var response = await handler.Handle(query, CancellationToken.None);

        response.Should().NotBeNull();
        response.Count.Should().Be(1);
    }
}