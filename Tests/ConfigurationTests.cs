using System;
using System.Collections.Generic;
using System.Text;

using Xunit;
using Xunit.Abstractions;
using FluentAssertions;

namespace Tests
{
    public class ConfigurationTests
    {
        /// <summary>
        /// Output
        /// </summary>
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// C'tor
        /// </summary>
        public ConfigurationTests( ITestOutputHelper output )
        {
            _output = output;
        }

        [Fact]
        public void Can_Get_Configuration_Connection()
        {
            ConfigurationHelper helper = new ConfigurationHelper();
            var configuration = helper.GetConfiguration( AppContext.BaseDirectory );

            configuration
                .Should().NotBeNull( "Configuration object cannot be null" );

            configuration.Connection
                .Should().NotBeNull( "Connection string cannot be null." )
                .And.Should().NotBeSameAs( String.Empty, "Connection cannot be an empty string" );

            _output.WriteLine( $"Connection string is: {configuration.Connection}" );
        }

    }
}
