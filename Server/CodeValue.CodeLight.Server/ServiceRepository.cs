using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using CodeValue.CodeLight.Server.Providers;
using System.Runtime.Serialization.Json;

namespace CodeValue.CodeLight.Server
{
    public class ServiceRepository : IServiceRepository
    {
        //TODO: now it is assuming that the "generated" type name is the same, can we assume that?
        public IEnumerable<string> All(string typeName)
        {
            IProvider provider = ProviderConfiguration.GetProvider(typeName);
            Type type = provider.Type;
            IEnumerable objects = provider.All();
            List<string> result = new List<string>();
            foreach (var obj in objects)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(type);


                    serializer.WriteObject(stream, obj);
                    stream.Position = 0;
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        result.Add(reader.ReadToEnd());
                    }

                }
            }
            return result;
            //return
            //    "{ \"<Id>k__BackingField\": \"patients/1\", \"<Name>k__BackingField\": \"Margol Foo\",\"Name\": \"Margol Foo\"}";
        }
    }
}