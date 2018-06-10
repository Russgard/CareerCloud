using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : BaseADO, IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand { Connection = conn };

                conn.Open();
                foreach (ApplicantEducationPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO dbo.Applicant_Educations 
                    (Id, Applicant, Major, Certificate_Diploma, Start_Date, Completion_Date, Completion_Percent) 
                    VALUES 
                    (@Id, @Applicant, @Major, @Certificate_Diploma, @Start_Date, @Completion_Date, @Completion_Percent);";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Major", item.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", item.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);

                    int rowsEffected = cmd.ExecuteNonQuery();
                }
                 conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[1000];
            SqlConnection conn = new SqlConnection(_connString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = @"SELECT * FROM dbo.Applicant_Educations;"
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int step = 0;
                while(reader.Read())
                {
                    ApplicantEducationPoco poco = new ApplicantEducationPoco
                    {
                        Id = (Guid)reader[0],
                        Applicant = (Guid)reader[1],
                        Major = (string)reader[2],
                        CertificateDiploma = (string)reader[3],
                        StartDate = (DateTime?)reader[4],
                        CompletionDate = (DateTime?)reader[5],
                        CompletionPercent = (byte?)reader[6],
                        TimeStamp = (byte[])reader[7]
                    };

                    pocos[step] = poco;
                    step++;
                }
                conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();

        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connString);
            using (conn) {

                SqlCommand cmd = new SqlCommand { Connection = conn };

                conn.Open();
                foreach (ApplicantEducationPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM dbo.Applicant_Educations 
                                    WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand { Connection = conn };

                conn.Open();
                foreach (ApplicantEducationPoco item in items)
                {
                    cmd.CommandText = @"UPDATE dbo.Applicant_Educations
                                    SET Applicant = @Applicant, Major = @Major, Certificate_Diploma = @Certificate_Diploma, Start_Date = @Start_Date, Completion_Date = @Completion_Date, Completion_Percent = @Completion_Percent
                                    WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Major", item.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", item.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);

                    int rowsEffected = cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
