// -----------------------------------------------------------------------
//  <copyright file="AppBootstrapper.cs" company="">
//      Author: Sameer Omar
//      Copyright (c) . All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using PersonalityTester.Data;
using PersonalityTester.Helpers;
using PersonalityTester.Services;
using PersonalityTester.ViewModels;

namespace PersonalityTester
{
    internal class AppBootstrapper : BootstrapperBase
    {
        public AppBootstrapper()
        {
            Initialize();
        }

        public SimpleContainer Container { get; set; } = new();

        /// <summary>
        ///     Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="instance">The instance to perform injection on.</param>
        protected override void BuildUp(object instance)
        {
            Container.BuildUp(instance);
        }

        /// <summary>
        ///     Override to configure the framework and setup your IoC container.
        /// </summary>
        protected override void Configure()
        {
            var configuration = ConfigurationHelper.GetConfiguration();
            var contextOption = DatabaseHelper.CreateDbContextOption(configuration);
            var appDbContext = new AppDbContext(contextOption);

            // Register DB and Configuration instances
            Container.Instance(appDbContext);
            Container.Instance(configuration);

            // Register services:
            Container.Singleton<IWindowManager, WindowManager>();
            Container.PerRequest<IDatabaseService, DatabaseService>();

            // Register View Models
            Container.PerRequest<MainViewModel>();
        }

        /// <summary>
        ///     Override this to provide an IoC specific implementation
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <returns>
        ///     The located services.
        /// </returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return Container.GetAllInstances(service);
        }

        /// <summary>
        ///     Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        ///     The located service.
        /// </returns>
        protected override object GetInstance(Type service, string key)
        {
            return Container.GetInstance(service, key);
        }

        /// <summary>
        ///     Override this to add custom behavior to execute after the application starts.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewForAsync<MainViewModel>();
        }

        /// <summary>
        ///     Override to tell the framework where to find assemblies to inspect for views, etc.
        /// </summary>
        /// <returns>
        ///     A list of assemblies to inspect.
        /// </returns>
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] { Assembly.GetExecutingAssembly() };
        }
    }
}