// -----------------------------------------------------------------------
//  <copyright file="AppBootstrapper.cs" company="">
//      Author: Sameer Omar
//      Copyright (c) . All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        protected override void BuildUp(object instance)
        {
            Container.BuildUp(instance);
        }

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

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return Container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return Container.GetInstance(service, key);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewForAsync<MainViewModel>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] { Assembly.GetExecutingAssembly() };
        }
    }
}