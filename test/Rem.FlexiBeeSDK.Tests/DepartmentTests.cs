using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Rem.FlexiBeeSDK.Client.Clients.Accounting.Departments;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class DepartmentTests
    {
        private IFixture _fixture;

        public DepartmentTests()
        {
            _fixture = FlexiFixture.Setup();
        }

        [Fact]
        public async Task GetDepartments_ShouldReturnDepartments()
        {
            var client = _fixture.Create<DepartmentClient>();

            var departments = await client.GetAsync();

            departments.Should().NotBeEmpty();
            departments.Should().OnlyContain(d => !string.IsNullOrEmpty(d.Code));
            departments.Should().OnlyContain(d => !string.IsNullOrEmpty(d.Name));
            departments.Should().OnlyContain(d => d.Id > 0);
        }

        [Fact]
        public async Task GetDepartments_ShouldContainExpectedProperties()
        {
            var client = _fixture.Create<DepartmentClient>();

            var departments = await client.GetAsync();

            departments.Should().NotBeEmpty();
            
            var firstDepartment = departments.First();
            firstDepartment.Id.Should().BeGreaterThan(0);
            firstDepartment.Code.Should().NotBeNullOrEmpty();
            firstDepartment.Name.Should().NotBeNullOrEmpty();
            firstDepartment.LastUpdate.Should().NotBe(default);
        }

        [Fact]
        public async Task GetDepartments_ShouldContainExpectedDepartments()
        {
            var client = _fixture.Create<DepartmentClient>();

            var departments = await client.GetAsync();

            departments.Should().NotBeEmpty();
            departments.Should().Contain(d => d.Code == "VYROBA");
            departments.Should().Contain(d => d.Code == "MARKETING");
        }
    }
}