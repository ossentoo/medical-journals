using AutoMapper;

namespace MedicalJournals.Models.Interfaces
{
	public interface IHaveCustomMappings
	{
		void CreateMappings(IMapperConfigurationExpression configurationProvider);
	}
}