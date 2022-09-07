// -----------------------------------------------------------------------
//  <copyright file="DatabaseHelper.cs" company="">
//      Author: Sameer Omar
//      Copyright (c) . All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonalityTester.Data;

namespace PersonalityTester.Helpers
{
    internal class DatabaseHelper
    {
        /// <summary>
        ///     Creates the database context option.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static DbContextOptions<AppDbContext> CreateDbContextOption(IConfigurationRoot configuration)
        {
            var connectionString = configuration.GetConnectionString("SqliteConnection");

            return CreateDbContextOption(connectionString);
        }

        /// <summary>
        ///     Creates the database context option.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        public static DbContextOptions<AppDbContext> CreateDbContextOption(string connectionString)
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connectionString);

            return contextOptionsBuilder.Options;
        }
    }
}