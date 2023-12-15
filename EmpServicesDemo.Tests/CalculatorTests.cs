﻿using Companies.EmployeeService;

namespace EmpServicesDemo.Tests
{
    public class CalculatorTests
    {
        private Calculator sut;

        public CalculatorTests()
        {
            sut = new Calculator();
        }

        [Theory]
        [InlineData(1, 578)]
        [InlineData(6, 2)]
        [InlineData(9, 7)]
        public void Add(int val1, int val2)
        {
            var res = sut.Add(val1, val2);
            Assert.Equal(val1 + val2, res);
        }

        [Theory]
        [MemberData(nameof(GetNumbers), 2)]
        public void Add2(int val1, int val2)
        {
            var res = sut.Add(val1, val2);
            Assert.Equal(val1 + val2, res);
        }

        public static IEnumerable<object[]> GetNumbers(int nrOfDataSets)
        {
            var dataSets = new List<object[]>
            {
                new object[] { 1, 2},
                new object[] { 1, 2},
                new object[] { 1, 2},
                new object[] { 1, 2},
            };

            return dataSets.Count <= nrOfDataSets ? dataSets : dataSets.Take(nrOfDataSets);
        }
    }
}
