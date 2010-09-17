using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CodeValue.CodeLight.Client;
using CodeValue.CodeLight.Client.Foundation;
using Microsoft.Practices.Composite.Presentation.Commands;
using Patients.Client.Common.CommonTypes;
using Patients.Client.Common.Services.Services;
using System.Windows.Threading;

namespace Patients.Client.Modules.Patients.ViewModels
{
    public class PatientsViewModel : ViewModelBase<IEnumerable<Patient>>
    {
        public PatientsViewModel()
        {
            CallCommand = new DelegateCommand<object>(CallAction);
        }

        private void CallAction(object obj)
        {

            //PatientsServiceClient client = new PatientsServiceClient();
            //client.DoWorkCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_DoWorkCompleted);
            //TODO: check out why other web service methods fails
            //client.DoWorkAsync();
            //ClientRepository<Patient> repository = new ClientRepository<Patient>(client.Endpoint.Address.Uri.ToString());
            ClientRepository<Patient> repository = new ClientRepository<Patient>("http://localhost/Patients.Web/PatientsService.svc");
            repository.All(Callback);
        }

        void client_DoWorkCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
        }

        private void Callback(IEnumerable<Patient> obj)
        {
            Model = obj;
        }

        public DelegateCommand<object> CallCommand { get; set; }
    }
}
