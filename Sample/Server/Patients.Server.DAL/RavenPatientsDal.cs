using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patients.Server.CommonTypes;
using Raven.Client.Document;

namespace Patients.Server.DAL
{
    public class RavenPatientsDal
    {
        private static DocumentStore _documentStore;

        private RavenPatientsDal()
        {
            _documentStore = new DocumentStore { Url = "http://localhost:8080" };
            _documentStore.Initialize();
        }

        private static RavenPatientsDal _instance = new RavenPatientsDal();

        public static RavenPatientsDal Instance
        {
            get { return _instance; }
        }

        public List<Patient> AllPatients()
        {
            using (var session = _documentStore.OpenSession())
            {
                return session.LuceneQuery<Patient>("Patients").ToList();
            }
        }
    }
}
