﻿using System.IO;
using System.Reflection;
using System.Windows.Threading;
using Application_Pour_Sibilia.Services;
using Application_Pour_Sibilia.ViewModels.Pages;
using Application_Pour_Sibilia.ViewModels.Windows;
using Application_Pour_Sibilia.Views.Pages;
using Application_Pour_Sibilia.Views.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wpf.Ui;
using Wpf.Ui.DependencyInjection;
using static Application_Pour_Sibilia.ViewModels.Windows.MainWindowViewModel;

namespace Application_Pour_Sibilia
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
        // https://docs.microsoft.com/dotnet/core/extensions/generic-host
        // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
        // https://docs.microsoft.com/dotnet/core/extensions/configuration
        // https://docs.microsoft.com/dotnet/core/extensions/logging
        private static readonly IHost _host = Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(AppContext.BaseDirectory)); })
            .ConfigureServices((context, services) =>
            {
                services.AddNavigationViewPageProvider();

                services.AddHostedService<ApplicationHostService>();
                services.AddSingleton<SessionService>();

                // Theme manipulation
                services.AddSingleton<IThemeService, ThemeService>();

                // TaskBar manipulation
                services.AddSingleton<ITaskBarService, TaskBarService>();

                // Service containing navigation, same as INavigationWindow... but without window
                services.AddSingleton<INavigationService, NavigationService>();

                // Main window with navigation
                services.AddSingleton<INavigationWindow, MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<ConnexionWindowViewModel>();

                
                
                services.AddSingleton<GestionDesPlatsPage>();
                services.AddSingleton<GestionDesPlatsViewModel>();
                services.AddSingleton<CreationCommandePage>();
                services.AddSingleton<CreationCommandeViewModel>();
                services.AddSingleton<SettingsPage>();
                services.AddSingleton<SettingsViewModel>();
                services.AddSingleton<GestionClientPage>();
                services.AddSingleton<GestionClientViewModel>();
                services.AddSingleton<ConsulterCommandePage>();
                services.AddSingleton<ConsulterCommandeViewModel>();
                services.AddSingleton<PlatWindow>();
                services.AddSingleton<ConnexionWindow>();
                services.AddSingleton<ToutesLesCommandes>();
                services.AddSingleton<CommandeDuJour>();

                //services.AddSingleton<WindowCLient>();
            }).Build();

        /// <summary>
        /// Gets services.
        /// </summary>
        public static IServiceProvider Services
        {
            get { return _host.Services; }
        }

        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        private async void OnStartup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();
        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();

            _host.Dispose();
        }

        /// <summary>
        /// Occurs when an exception is thrown by an application but not handled.
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
        }
    }
}
