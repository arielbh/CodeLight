using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace CodeValue.CodeLight.Server
{
    [ServiceContract]
    public interface IServiceRepository
    {
        //[OperationContract]
        //[WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        //List<object> All(string typeName);

        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#if !SILVERLIGHT
        [OperationContract]
        IEnumerable<string> All(string typeName);
#else
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginAll(string typeName, AsyncCallback callback, object state);
        IEnumerable<string> EndAll(IAsyncResult asyncResult);
#endif

    }
}
