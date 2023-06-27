using Caliburn.Micro;
using RMDesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMDesktopUI
{
    public class Bootstrapper : BootstrapperBase
    {
        //used for Dependency injection will replace most NEW() calls
        private SimpleContainer _container = new SimpleContainer();
       public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container.Instance(_container); // returns the container when requested

            _container
                .Singleton<IWindowManager, WindowManager>() //bring windows in and out
                .Singleton<IEventAggregator, EventAggregator>(); //pass event messaging thru application

            GetType().Assembly.GetTypes()
            .Where(type => type.IsClass)
            .Where(type => type.Name.EndsWith("ViewModel"))
            .ToList() //creates list of all viewModels
            .ForEach(viewModelType => _container.RegisterPerRequest(
                viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewForAsync<ShellViewModel>();
            
        }


        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        //Constructs the instance (I believe)
        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
