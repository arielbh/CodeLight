using System;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Text;
using CodeValue.CodeLight.Server;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

namespace CodeValue.CodeLight.Client
{
    public class ClientRepository<T> : IRepository<T>
    {
        private readonly string _baseUri;
        public ClientRepository(string baseUri)
        {
            _baseUri = baseUri;
        }

        IServiceRepository CreateProxy()
        {
            CustomBinding binding = new CustomBinding(
                new WebMessageEncodingBindingElement(),
                new HttpTransportBindingElement { ManualAddressing = true });

            ChannelFactory<IServiceRepository> factory = new ChannelFactory<IServiceRepository>(binding, new EndpointAddress(_baseUri));
            factory.Endpoint.Behaviors.Add(new WebHttpBehaviorWithJson());
            IServiceRepository serviceRepository = factory.CreateChannel();
            ((IClientChannel)serviceRepository).Closed += delegate { factory.Close(); };
            return serviceRepository;
        }

        public void All(Action<IEnumerable<T>> callback)
        {
            IServiceRepository proxy = CreateProxy();
            proxy.BeginAll(typeof(T).Name, Callback, new StateToken { Repository = proxy, Callback = callback});
            
        }

        private void Callback(IAsyncResult asyncResult)
        {
            StateToken token = (StateToken)asyncResult.AsyncState;
            IServiceRepository proxy = token.Repository;
            IEnumerable<string> result = proxy.EndAll(asyncResult);


            ((IClientChannel)proxy).BeginClose(new AsyncCallback(CloseCallback), proxy);

            Action<IEnumerable<T>> callback = token.Callback as Action<IEnumerable<T>>;
            var list = new List<T>();

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            foreach (string entity in result)
            {
                using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(entity)))
                {
                    T obj = (T) serializer.ReadObject(stream);
                    list.Add(obj);

                }
            }
            Deployment.Current.Dispatcher.BeginInvoke(() => callback(list));


        }

        private void CloseCallback(IAsyncResult asyncResult)
        {
            IServiceRepository proxy = (IServiceRepository)asyncResult.AsyncState;
            ((IClientChannel)proxy).EndClose(asyncResult);
        }

        public void FindAll(System.Linq.Expressions.Expression<Func<T, bool>> filter, Action<System.Collections.Generic.IEnumerable<T>> callback)
        {
            throw new NotImplementedException();
        }
    }

    public class StateToken
    {
        public IServiceRepository Repository { get; set; }
        public object Callback { get; set; }
        
    }
}
