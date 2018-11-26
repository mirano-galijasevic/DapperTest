using System;
using Microsoft.Extensions.Configuration;

namespace Tests
{
    public class ConfigurationHelper
    {
        /// <summary>
        /// C'tor
        /// </summary>
        public ConfigurationHelper()
        { }

        /// <summary>
        /// Get configuration root
        /// </summary>
        /// <param name="outputPath"></param>
        /// <returns></returns>
        private IConfigurationRoot GetConfigurationRoot( string basePath )
        {
            return new ConfigurationBuilder()
                .SetBasePath( basePath )
                .AddJsonFile( "settings.json", optional: true )
                .AddEnvironmentVariables()
                .Build();
        }

        /// <summary>
        /// Get TestConfiguration
        /// </summary>
        public TestConfiguration GetConfiguration( string basePath )
        {
            var configuration = new TestConfiguration();
            var iConfigRoot = GetConfigurationRoot( basePath );

            iConfigRoot
                .GetSection( "DapperTesting" )
                .Bind( configuration );

            return configuration;
        }
    }
}
