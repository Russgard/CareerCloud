using CareerCloud.ADODataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestADO
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicantEducationRepository repo = new ApplicantEducationRepository();
            var pocos = repo.GetAll();


        }
    }
}
