using Demo.Application.Entities;
using Demo.Application.Jobs.Dtos;
using System.Runtime.Serialization;

namespace Demo.Application.Tests.Mappings
{
    public class MappingTests : UnitTestBase
    {
        [Theory]
        [InlineData(typeof(Job), typeof(JobDto))]
        public void GivenSourceAndDestination_WhenMapperInvoked_MappingProfileExists(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            Mapper.Map(instance, source, destination);
        }

        private static object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type)!;

            // Type without parameterless constructor
            return FormatterServices.GetUninitializedObject(type);
        }
    }
}
