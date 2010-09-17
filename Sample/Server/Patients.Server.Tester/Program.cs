using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patients.Server.CommonTypes;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace Patients.Server.Tester
{
    class Program
    {
        private static DocumentStore _documentStore;
        static void Main(string[] args)
        {
            _documentStore = new DocumentStore { Url = "http://localhost:8080" };
            _documentStore.Initialize();

            AddPatients();
        //    GetPatients();
            CreateIndex();
            GetPatients();


        }

        private static void CreateIndex()
        {
            _documentStore.DatabaseCommands.PutIndex("Patients", new IndexDefinition<Patient>
            {
                Map = patients => from patient in patients
                                  select new { patient.Id }
            });
        }

        private static void GetPatients()
        {
            using (var session = _documentStore.OpenSession())
            {
                var list = session.LuceneQuery<Patient>("Patients").ToList();
                foreach (var patient in list)
                {
                    //dynamic patient = session.Load<object>("patients/1");
                    Console.WriteLine(patient.Name);
                }
            }
        }

        private static void AddPatients()
        {
            using (var session = _documentStore.OpenSession())
            {
                var product = new Patient
                                  {
                                      Name = "Dina",
                                  };
                session.Store(product);
                session.SaveChanges();
            }
        }
    }
}
