using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patients.Server.CommonTypes;

namespace Patients.Server.DAL
{
    class MemoryPatientsDal
    {
        public static List<Patient> AllPatients()
        {
            return new List<Patient>
                       {
                           new Patient {Name = "Joe Doe"},
                           new Patient {Name = "Jacks S"},
                           new Patient {Name = "Margol Foo"}
                       };

        }
    }
}
