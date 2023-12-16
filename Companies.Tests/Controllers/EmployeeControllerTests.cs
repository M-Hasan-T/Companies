﻿using Companies.API.Controllers;
using Companies.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Tests.Controllers
{
    public class EmployeeControllerTests : IClassFixture<TestDataBaseFixture>
    {
        private readonly TestDataBaseFixture fixture;
        private readonly EmployeesController sut;

        public EmployeeControllerTests(TestDataBaseFixture fixture)
        {
            this.fixture = fixture;
            sut = new EmployeesController(fixture.Context, fixture.Mapper);
        }

        [Fact]
        public async Task GetEmployee_CompanyDoesNotExist_ShouldReturnNotFound()
        {
            var output = await sut.GetEmployee(Guid.NewGuid());
            Assert.NotNull(output);
            Assert.IsType<NotFoundResult>(output.Result);
        }

    }
}
