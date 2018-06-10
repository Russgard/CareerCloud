using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantWorkHistoryRepository : BaseADO, IDataRepository<ApplicantWorkHistoryPoco>
    {
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            using (_conn)
            {
                SqlCommand cmd = new SqlCommand { Connection = _conn };

                _conn.Open();
                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO dbo.Applicant_Work_History
                    (Id, Applicant, Company_Name, Country_Code, Location, Job_Title, Job_Description, Start_Month, Start_Year, End_Month, End_Year) 
                    VALUES 
                    (@Id, @Applicant, @Company_Name, @Country_Code, @Location, @Job_Title, @Job_Description, @Start_Month, @Start_Year, @End_Month, @End_Year);";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", item.Location);
                    cmd.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", item.EndYear);

                    int rowsEffected = cmd.ExecuteNonQuery();
                }
                _conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            ApplicantWorkHistoryPoco[] pocos = new ApplicantWorkHistoryPoco[1000];
            using (_conn)
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = _conn,
                    CommandText = @"SELECT * FROM dbo.Applicant_Work_History;"
                };
                _conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int step = 0;
                while (reader.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco
                    {
                        Id = (Guid)reader[0],
                        Applicant = (Guid)reader[1],
                        CompanyName = (string)reader[2],
                        CountryCode = (string)reader[3],
                        Location = (string)reader[4],
                        JobTitle = (string)reader[5],
                        JobDescription = (string)reader[6],
                        StartMonth = (Int16)reader[7],
                        StartYear = (int)reader[8],
                        EndMonth = (Int16)reader[9],
                        EndYear = (int)reader[10],
                        TimeStamp = (byte[])reader[11]
                    };

                    pocos[step] = poco;
                    step++;
                }
                _conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            using (_conn)
            {

                SqlCommand cmd = new SqlCommand { Connection = _conn };

                _conn.Open();
                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM dbo.Applicant_Work_History 
                                    WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    int rowsEffected = cmd.ExecuteNonQuery();
                }
                _conn.Close();
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            using (_conn)
            {
                SqlCommand cmd = new SqlCommand { Connection = _conn };

                _conn.Open();
                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    cmd.CommandText = @"UPDATE dbo.Applicant_Work_History
                                    SET Applicant = @Applicant,
                                        Company_Name = @Company_Name, 
                                        Country_Code = @Country_Code, 
                                        Location = @Location,
                                        Job_Title = @Job_Title, 
                                        Job_Description = @Job_Description,
                                        Start_Month = @Start_Month, 
                                        Start_Year = @Start_Year,
                                        End_Month = @End_Month, 
                                        End_Year = @End_Year                                        
                                    WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", item.Location);
                    cmd.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", item.EndYear);

                    int rowsEffected = cmd.ExecuteNonQuery();
                }
                _conn.Close();
            }
        }
    }
}
