// -----------------------------------------------------------------------
//  <copyright file="ConfigurationHelper.cs" company="">
//      Author: Sameer Omar
//      Copyright (c) . All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System.IO;
using Microsoft.Extensions.Configuration;

namespace PersonalityTester.Helpers
{
    internal class ConfigurationHelper
    {
        /// <summary>
        ///     Gets the configuration.
        /// </summary>
        /// <returns></returns>
        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);

            return builder.Build();
        }
    }
}