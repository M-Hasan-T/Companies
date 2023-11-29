
using System.ComponentModel.DataAnnotations;

namespace Companies.API.Dtos.EmployeesDtos
{
    public class EmployeeDto
    {
        public Guid Id { get; init; }

        public string? Name { get; init; }

        public int Age { get; init; }

        public string? Position { get; init; }
    }
}
