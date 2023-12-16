﻿
using Companies.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Companies.Tests.Controllers
{
    public class TestDemoControllerTests
    {
        private TestDemoController sut;

        public TestDemoControllerTests()
        {
            sut = new TestDemoController();
        }

        [Fact]
        public async Task GetEmployees_ShouldReturnOkResult()
        {
            var output = await sut.GetEmployee();
            var okResult = output.Result as OkResult;

            Assert.IsType<OkResult>(okResult);
        }

    }
}
