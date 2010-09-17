using System.Collections.Generic;
using System.ServiceModel;

namespace CodeValue.CodeLight.Shared
{
    [ServiceContract]
    public interface IServiceRepository
    {
        [OperationContract]
        List<object> All(string typeName);

    }
}
