using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Rem.FlexiBeeSDK.Client.Clients.Contacts;
using Rem.FlexiBeeSDK.Model.Contacts;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class ContactClientTests
    {
        private IFixture _fixture;

        public ContactClientTests()
        {
            _fixture = FlexiFixture.Setup();
        }

        [Fact]
        public async Task GetContactByCode()
        {
            var client = _fixture.Create<ContactClient>();
            var contactCode = "ABRA";

            var contact = await client.GetAsync(contactCode);

            contact.Should().NotBeNull();
            contact.Code.Should().Be(contactCode);
        }

        [Fact]
        public async Task GetContactByCode_ShouldThrowKeyNotFoundException_WhenContactNotFound()
        {
            var client = _fixture.Create<ContactClient>();
            var nonExistentCode = "NONEXISTENT999";

            Func<Task> act = async () => await client.GetAsync(nonExistentCode);

            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task GetContactById()
        {
            var client = _fixture.Create<ContactClient>();
            var contactIc = "28019920";

            var contact = await client.GetByIdAsync(contactIc);

            contact.Should().NotBeNull();
            contact.Should().BeOfType<ContactFlexiDto>();
        }

        [Fact]
        public async Task GetContactById_ShouldThrowKeyNotFoundException_WhenContactNotFound()
        {
            var client = _fixture.Create<ContactClient>();
            var nonExistentIc = "99999999";

            Func<Task> act = async () => await client.GetByIdAsync(nonExistentIc);

            await act.Should().ThrowAsync<KeyNotFoundException>();
        }


        [Fact]
        public async Task UpdateContact()
        {
            var client = _fixture.Create<ContactClient>();
            var contactToUpdate = new ContactFlexiDto
            {
                Id = 742,
                Code = "PAJGRT",
                Name = "Ondřej Pajgrt",
                Email = "updated@example.com",
                Phone = "+420123456789",
                Note = "Nejake podivin"
            };

            var result = await client.UpdateAsync(contactToUpdate);

            result.Should().NotBeNull();
            result.Result.Should().NotBeNull();
            result.Result.IsSuccess.Should().BeTrue();
            result.Result.Stats.Updated.Should().Be("1");
        }
    }
}