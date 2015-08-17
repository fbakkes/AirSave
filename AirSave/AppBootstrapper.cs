using AirSave.Models;
using AirSave.ViewModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace AirSave
{
    public class AppBootstrapper : BootstrapperBase
    {
        private SimpleContainer _container;
        public static SimpleContainer Container = null;

        /// <summary>
        /// Sync. object for getting/setting from the container. Any number of reads are allowed but writes are synced
        /// </summary>
        private static readonly ReaderWriterLockSlim InstanceLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container = new SimpleContainer();

            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.PerRequest<IShell, ShellViewModel>();
            _container.RegisterSingleton(typeof(GMapViewModel), "GMap", typeof(GMapViewModel));

            _container.RegisterSingleton(typeof(Settings), "Settings", typeof(Settings));

            Container = _container;
        }
        /// <summary>
        /// Retrieves an instance of type T from the IoC container
        /// </summary>
        /// <typeparam name="T">Type of object to retrieve</typeparam>
        /// <param name="key">Key to use. Supply null to use the type name</param>
        /// <returns>Object instance or null</returns>
        public static T GetInstance<T>(string key = null)
        {
            object instance;
            if (Container == null) return default(T);
            //if (key == null) key = typeof (T).Name;

            InstanceLock.EnterReadLock();
            try
            {
                instance = Container.GetInstance(typeof(T), key);
            }
            finally
            {
                InstanceLock.ExitReadLock();
            }
            return (T)instance;
        }

        /// <summary>
        /// Updates an instance in the container
        /// </summary>
        public static void SetInstance<T>(T instance, string key = null)
        {
            InstanceLock.EnterWriteLock();
            try
            {
                Container.UnregisterHandler(typeof(T), key);
                Container.RegisterInstance(typeof(T), key, instance);
            }
            finally
            {
                InstanceLock.ExitWriteLock();
            }
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] { Assembly.GetExecutingAssembly() };
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            var shellSettings = new Dictionary<string, object>
                {
                    { "Title", "AirSave"},
                    { "WindowStyle", WindowStyle.ToolWindow },
                    { "ResizeMode", ResizeMode.CanResize},
                    { "WindowState", WindowState.Maximized},
                    { "WindowStartupLocation", WindowStartupLocation.CenterScreen }
                };
            DisplayRootViewFor<IShell>(shellSettings);
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            AppBootstrapper.GetInstance<Settings>().SaveSettings();
            base.OnExit(sender, e);
        }
    }
}
