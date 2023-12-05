using AutoMapper;
using Companies.API.Entities;
using Companies.Shared.Dtos.EmployeesDtos;

namespace Companies.API.Mappings
{
    public class EmployeeMappings : Profile
    {
        public EmployeeMappings()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(
                dest => dest.Position,
                from => from.MapFrom(e => e.Department.Name));

            CreateMap<EmployeesForUpdateDto, Employee>().ReverseMap();
        }
    }
}
