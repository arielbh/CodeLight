using System.ServiceModel;
using CodeValue.CodeLight.Server;

namespace Patients.Server.Facade
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPatientsService" in both code and config file together.
    [ServiceContract]
    public interface IPatientsService : IServiceRepository
    {
        [OperationContract]
        void DoWork();
    }
}
