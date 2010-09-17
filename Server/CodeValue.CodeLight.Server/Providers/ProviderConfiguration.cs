using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeValue.CodeLight.Server.Providers
{
    static public class ProviderConfiguration
    {
        static Dictionary<string, IProvider> providers = new Dictionary<string, IProvider>();
        public static void Register(string typeName, IProvider provider)
        {
            providers.Add(typeName, provider);
        }

        public static IProvider GetProvider(string typeName)
        {
          return 
            providers[typeName];
        }

    }
}
