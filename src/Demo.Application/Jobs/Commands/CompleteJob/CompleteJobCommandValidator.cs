using Demo.Application.Common.Interfaces;
using Demo.Application.Jobs.Dtos;
using FluentValidation;

namespace Demo.Application.Jobs.Commands.CompleteJob
{
    public class CompleteJobCommandValidator : AbstractValidator<CompleteJobCommand>
    {
        private readonly IDemoDbContext _dbContext;

        public CompleteJobCommandValidator(IDemoDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            RuleFor(c => c.JobId).NotEmpty().Custom(ValidateJobStatus);
        }

        private void ValidateJobStatus(Guid jobId, ValidationContext<CompleteJobCommand> context)
        {
            var job = _dbContext.Jobs.FirstOrDefault(j => j.Id == jobId);

            if (job is null)
            {
                context.AddFailure($"Job Id {jobId} does not exist.");
            }

            var jobStatus = (JobStatusEnum)(job?.StatusNum ?? 0);
            switch (jobStatus)
            {
                case JobStatusEnum.InProgress:
                case JobStatusEnum.Delayed:
                    return;
                default:
                    context.AddFailure($"Job Id {jobId} may not be completed.");
                    return;
            }
        }
    }
}
