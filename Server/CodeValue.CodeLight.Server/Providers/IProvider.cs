using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeValue.CodeLight.Server.Providers
{
    public interface IProvider
    {
        IEnumerable All();
        Type Type { get; }
    }
}
