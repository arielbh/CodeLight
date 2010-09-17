using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeValue.CodeLight.Server.Providers;
using System.Collections;
using Patients.Server.CommonTypes;

namespace Patients.Server.DAL
{
    public class PatientProvider : IProvider
    {
        public IEnumerable All()
        {
            return MemoryPatientsDal.AllPatients();
        }

        public Type Type
        {
            get { return typeof (Patient); }
        }
    }
}
