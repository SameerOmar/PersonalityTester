// -----------------------------------------------------------------------
//  <copyright file="DesignTimeDbContextFactory.cs" company="">
//      Author: Sameer Omar
//      Copyright (c) . All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Design;
using PersonalityTester.Helpers;

namespace PersonalityTester.Data
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        /// <summary>
        /// Creates a new instance of a DB context.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>
        /// An instance of <typeparamref name="TContext" />.
        /// </returns>
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = ConfigurationHelper.GetConfiguration();
            var contextOption = DatabaseHelper.CreateDbContextOption(configuration);

            return new AppDbContext(contextOption);
        }
    }
}