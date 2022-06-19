using AutoMapper;
using Demo.Application.Common.Mapping;
using Demo.Application.Entities;

namespace Demo.Application.Jobs.Dtos
{
    public class JobDto : IMapFrom<Job>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int? Floor { get; set; }
        public string RoomType { get; set; }
        public JobStatusEnum StatusEnum { get; set; }

        public void Mapping(Profile profile)
        {
            if (profile is null)
            {
                throw new ArgumentNullException(nameof(profile));
            }

            profile.CreateMap<Entities.Job, JobDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(s => s.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(s => s.Name))
                .ForMember(x => x.Status, opts => opts.MapFrom(s => s.Status))
                .ForMember(x => x.Floor, opts => opts.MapFrom(s => s.Floor))
                .ForMember(x => x.RoomType, opts => opts.MapFrom(s => s.RoomType.Name))
                .ForMember(x => x.StatusEnum, opts => opts.MapFrom(s => (JobStatusEnum)(s.StatusNum ?? 0)));
        }
    }
}
