using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Resources;
using System.Windows.Shapes;
using Microsoft.Practices.Composite.Modularity;
using System.Reflection;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Regions;
using Patients.Client.Common;
using Patients.Client.Modules.Patients.ViewModels;

namespace Patients.Client.Modules.Patients
{
    public class PatientsModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public PatientsModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            RegisterResources();
            _regionManager.AddToRegion(RegionNames.MainRegion, _container.Resolve<PatientsViewModel>());
        }

        private static void RegisterResources()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string assemblyName = assembly.FullName;
            string[] nameParts = assemblyName.Split(',');
            var dictionary = new ResourceDictionary();
            StreamResourceInfo resourceInfo = Application.GetResourceStream(new Uri(nameParts[0] + ";component/Resources/ModuleResources.xaml", UriKind.Relative));
            var resourceReader = new StreamReader(resourceInfo.Stream);
            string xaml = resourceReader.ReadToEnd();
            var resourceTheme = XamlReader.Load(xaml) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Add(resourceTheme);
        }
    }
}
