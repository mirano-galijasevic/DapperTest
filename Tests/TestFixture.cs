using System;
using DataLayer;
using Xunit.Abstractions;

namespace Tests
{
    public class TestFixture : IDisposable
    {
        /// <summary>
        /// Output
        /// </summary>
        public ITestOutputHelper Output { get; set; }

        /// <summary>
        /// Configuration
        /// </summary>
        public TestConfiguration Configuration { get; set; }

        /// <summary>
        /// Contact repository
        /// </summary>
        public IContactRepository ContactRepository { get; set; }

        /// <summary>
        /// Contact Id
        /// </summary>
        public int ContactId { get; set; }

        /// <summary>
        /// C'tor
        /// </summary>
        public TestFixture()
        { }

        /// <summary>
        /// IDisposable implementation
        /// </summary>
        public void Dispose()
        {
            // Clean up
        }
    }
}
