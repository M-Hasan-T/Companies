using Companies.EmployeeService;
using Moq;

namespace EmpServicesDemo.Tests
{
    public class EmpServiceTests
    {
        [Fact]
        public void RegisterUser_WhenIncorrectName_ShouldReturnFalse()
        {
            const string incorrectEmployeeName = "This is an incorrect name for an Employee";

            var mockValidator = new Mock<IValidator>(MockBehavior.Strict);

            var employee = new Employee
            {
                Name = incorrectEmployeeName,
            };

            mockValidator.Setup(x => x.ValidateName(employee)).Returns(false);
            mockValidator.Setup(x => x.ValidateSalaryLevel(employee)).Returns(SalaryLevel.Default);

            var sut = new EmpService(mockValidator.Object);
            var actual = sut.RegisterUser(employee);

            Assert.False(actual);

        }
    }
}