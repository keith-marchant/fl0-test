using Demo.Application.Jobs.Commands.CompleteJob;
using FluentValidation.TestHelper;

namespace Demo.Application.Tests.Jobs
{
    public class CompleteJobCommandValidatorTests : UnitTestBase
    {
        private readonly CompleteJobCommandValidator _validator;

        public CompleteJobCommandValidatorTests() : base()
        {
            _validator = new CompleteJobCommandValidator(Context);
        }

        [Fact]
        public async Task GivenJobToValidate_WhenComplete_ThenValidationFails()
        {
            var jobId = Guid.Parse("00cc80fc-060a-4814-975f-001963b6503a");

            Context.Jobs.Add(new Entities.Job
            {
                Id = jobId,
                ContractorId = null,
                Name = "Job1_0_0",
                Status = "Complete",
                Floor = 1,
                Room = 0,
                DelayReason = null,
                DateCreated = DateTime.Now,
                DateCompleted = null,
                DateDelayed = null,
                StatusNum = 1,
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

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.JobId);
        }

        [Fact]
        public async Task GivenJobToValidate_WhenNotStarted_ThenValidationFails()
        {
            var jobId = Guid.Parse("00cc80fc-060a-4814-975f-001963b6503a");

            Context.Jobs.Add(new Entities.Job
            {
                Id = jobId,
                ContractorId = null,
                Name = "Job1_0_0",
                Status = "Not Started",
                Floor = 1,
                Room = 0,
                DelayReason = null,
                DateCreated = DateTime.Now,
                DateCompleted = null,
                DateDelayed = null,
                StatusNum = 2,
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

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.JobId);
        }

        [Fact]
        public async Task GivenJobToValidate_WhenInProgress_ThenValidationSucceeds()
        {
            var jobId = Guid.Parse("00cc80fc-060a-4814-975f-001963b6503a");

            Context.Jobs.Add(new Entities.Job
            {
                Id = jobId,
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

            var command = new CompleteJobCommand(jobId);

            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task GivenJobToValidate_WhenDelayed_ThenValidationSucceeds()
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

            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task GivenJobToValidate_WhenJobDoesNotExist_ThenValidationFails()
        {
            var jobId1 = Guid.Parse("00cc80fc-060a-4814-975f-001963b6503a");
            var jobId2 = Guid.Parse("97f31d3f-dd8b-4350-98ec-002e1bca71dc");

            Context.Jobs.Add(new Entities.Job
            {
                Id = jobId1,
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

            var command = new CompleteJobCommand(jobId2);

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.JobId);
        }
    }
}
