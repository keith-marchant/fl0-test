using System.ComponentModel.DataAnnotations;

namespace Demo.Application.Jobs.Dtos
{
    public enum JobStatusEnum
    {
        Unknown = 0,
        Complete = 1,
        [Display(Name = "Not Started")]
        NotStarted = 2,
        [Display(Name = "In Progress")]
        InProgress = 3,
        Delayed = 4
    }
}
