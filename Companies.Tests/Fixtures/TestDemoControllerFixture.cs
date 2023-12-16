using AutoMapper;
using Companies.API.Controllers;
using Companies.API.Mappings;
using Companies.API.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Companies.Tests.Fixtures
{
    public class TestDemoControllerFixture : IDisposable
    {
        public Mock<ICompanyRepository> MockRepo { get; }
        public TestDemoController Controller { get; }
        public TestDemoControllerFixture()
        {
            MockRepo = new Mock<ICompanyRepository>();
            var mockUow = new Mock<IUnitOfWork>();

            mockUow.Setup(x => x.CompanyRepository).Returns(MockRepo.Object);

            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CompanyMappings>();
            }));

            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            var userManager = new UserManager<IdentityUser>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            Controller = new TestDemoController(mockUow.Object, mapper, userManager);
        }

        public void Dispose()
        {
            //Not used here
        }
    }
}
