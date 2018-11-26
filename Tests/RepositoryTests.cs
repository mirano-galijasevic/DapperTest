using System;
using Xunit;
using Xunit.Abstractions;

using FluentAssertions;
using System.Threading.Tasks;
using DataLayer;

namespace Tests
{
    [TestCaseOrderer( "Tests.PriorityOrderer", "Tests" )]
    public class RepositoryTests : IClassFixture<TestFixture>
    {
        /// <summary>
        /// Fixture
        /// </summary>
        private TestFixture _fixture;

        /// <summary>
        /// C'tor
        /// </summary>
        public RepositoryTests( TestFixture fixture, ITestOutputHelper output )
        {
            if ( null == fixture )
                throw new ArgumentNullException( "fixture" );

            _fixture = fixture;

            if ( null == output )
                throw new ArgumentNullException( "output" );

            _fixture.Output = output;

            ConfigurationHelper helper = new ConfigurationHelper();
            _fixture.Configuration = helper.GetConfiguration( AppContext.BaseDirectory );
        }

        [Fact, TestPriority( 1 )]
        public async Task Get_all_should_return_6_results()
        {
            // Arrange
            IContactRepository repo = CreateRepository();

            // Act
            var contacts = await repo.GetAll();

            // Assert
            contacts.Should().NotBeNull();
            contacts.Count.Should().Be( 6 );
        }

        [Fact, TestPriority( 2 )]
        public async Task Find_should_retrieve_existing_entity()
        {
            IContactRepository repo = CreateRepository();

            var contact = await repo.Find( 1 );

            contact.Should().NotBeNull();
            contact.Id.Should().Be( 1 );
        }

        [Fact, TestPriority( 3 )]
        public async Task Insert_should_assign_identity_to_new_entity()
        {
            IContactRepository repo = CreateRepository();

            var newContact = new Contact()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@microsoft.com",
                Company = "Microsoft"
            };

            var contact = await repo.Add( newContact );

            contact.Id.Should().NotBe( 0, "Identity should have been assigned by DB" );
            _fixture.Output.WriteLine( $"New ID: { contact.Id}" );
            _fixture.ContactId = contact.Id;
        }

        [Fact, TestPriority( 4 )]
        public async Task Find_just_added_entity()
        {
            // Arrange
            IContactRepository repo = CreateRepository();

            // Act
            var contact = await repo.Find( _fixture.ContactId );

            // Assert
            contact.Should().NotBeNull();
            contact.Id.Should().Be( _fixture.ContactId );
            contact.FirstName.Should().Be( "John" );
            contact.LastName.Should().Be( "Doe" );
            contact.Email.Should().Be( "john.doe@microsoft.com" );
            contact.Company.Should().Be( "Microsoft" );
        }

        [Fact, TestPriority( 5 )]
        public async Task Modify_should_update_existing_entity()
        {
            // Arrange
            IContactRepository repo = CreateRepository();

            // Act...let's just get the new contact and change his name
            var contact = await repo.Find( _fixture.ContactId );
            contact.FirstName = "Jane";

            // Now update this contact
            var updatedContact = await repo.Update( contact );

            // Assert that the name has really changed
            updatedContact.FirstName.Should().Be( "Jane" );
        }

        [Fact, TestPriority( 6 )]
        public async Task Delete_should_remove_entity()
        {
            // Arrange
            IContactRepository repo = CreateRepository();

            // Act
            await repo.Remove( _fixture.ContactId );

            // Assert
            var deletedContact = await repo.Find( _fixture.ContactId );
            deletedContact.Should().BeNull();
        }

        [Fact, TestPriority( 7 )]
        public async Task Get_all_from_addresses()
        {
            // Arrange
            IAddressRepository repo = new AddressRepository( _fixture.Configuration.Connection );

            // Act
            var addresses = await repo.GetAll();

            // Assert
            Assert.NotNull( addresses );

            foreach ( var address in addresses )
            {
                _fixture.Output.WriteLine( $"{address.StreetAddress}, {address.City}, {address.PostalCode}" );
            }
        }

        [Fact, TestPriority( 9 )]
        public async Task Get_multiple_results()
        {
            // Arrange
            IAddressRepository repo = new AddressRepository( _fixture.Configuration.Connection );

            // Act
            var addresses = await repo.GetAddressesAndStates();

            // Assert
            Assert.NotNull( addresses );
        }

        /// <summary>
        /// Create repository
        /// </summary>
        /// <returns></returns>
        private IContactRepository CreateRepository()
        {
            if ( null == _fixture.ContactRepository )
                _fixture.ContactRepository = new ContactRepository( _fixture.Configuration.Connection );

            return _fixture.ContactRepository;
        }
    }
}
