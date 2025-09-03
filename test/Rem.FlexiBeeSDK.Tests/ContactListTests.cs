using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Rem.FlexiBeeSDK.Client.Clients.Contacts;
using Rem.FlexiBeeSDK.Model.Contacts;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class ContactListTests
    {
        private IFixture _fixture;

        public ContactListTests()
        {
            _fixture = FlexiFixture.Setup();
        }

        [Fact]
        public async Task GetSupplierContacts()
        {
            var client = _fixture.Create<ContactListClient>();
            
            var contacts = await client.GetAsync([ContactType.Supplier], limit: 10);

            contacts.Should().NotBeEmpty();
            contacts.Should().OnlyContain(c => !string.IsNullOrEmpty(c.Code));
        }
        
        [Fact]
        public async Task GetSupplierContactsWithPaging()
        {
            var client = _fixture.Create<ContactListClient>();
            
            var firstPage = await client.GetAsync([ContactType.Supplier], limit: 5);
            var secondPage = await client.GetAsync([ContactType.Supplier], limit: 5, skip: 5);

            firstPage.Should().NotBeEmpty();
            secondPage.Should().NotBeEmpty();
            
            firstPage.Select(x => x.Code).Should().NotIntersectWith(secondPage.Select(y => y.Code));
        }
        
        [Fact]
        public async Task GetSupplierContactsWithoutLimit()
        {
            var client = _fixture.Create<ContactListClient>();
            
            var contacts = await client.GetAsync([ContactType.Supplier]);

            contacts.Should().NotBeEmpty();
            contacts.Should().OnlyContain(c => !string.IsNullOrEmpty(c.Code));
        }
        
        [Fact]
        public async Task GetSupplierContactsMultipleTypes()
        {
            var client = _fixture.Create<ContactListClient>();
            
            var contacts = await client.GetAsync([ContactType.SupplierAndCustomer]);

            contacts.Should().NotBeEmpty();
            contacts.Should().Contain(c => c.Code == "HERUFEK");
        }
    }
}