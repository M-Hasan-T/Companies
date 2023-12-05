using Companies.Shared.Dtos.EmployeesDtos;
using System.ComponentModel.DataAnnotations;

namespace Companies.Shared.Dtos.CompaniesDtos
{
    public record CompanyDto
    {
        public Guid Id { get; init; }

        public string? Name { get; init; }
        public string? Address { get; init; }

        public IEnumerable<EmployeeDto> Employees { get; init; }
    }
}
