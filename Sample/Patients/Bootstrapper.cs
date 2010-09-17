using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.UnityExtensions;

namespace Patients
{
    public class Bootstrapper : UnityBootstrapper
    {

        protected override DependencyObject CreateShell()
        {
            MainPage shell = Container.Resolve<MainPage>();
            Application.Current.RootVisual = shell;
            return shell;
        }

        protected override Microsoft.Practices.Composite.Modularity.IModuleCatalog GetModuleCatalog()
        {
            return ModuleCatalog.CreateFromXaml(new Uri("ModulesCatalog.xaml", UriKind.Relative));
        }
    }
}
