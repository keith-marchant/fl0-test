using AutoMapper;
using Demo.Application.Common.Interfaces;
using Demo.Application.Common.Mapping;
using Demo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Demo.Application.Tests
{
    public abstract class UnitTestBase
    {
        protected readonly IConfigurationProvider Configuration;
        protected readonly IMapper Mapper;
        protected readonly IDemoDbContext Context;

        public UnitTestBase()
        {
            Configuration = new MapperConfiguration(config =>
                config.AddProfile<MappingProfile>());

            Mapper = Configuration.CreateMapper();

            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Context = new DemoDbContext(options);
            Context.Database.EnsureCreated();
        }
    }
}
