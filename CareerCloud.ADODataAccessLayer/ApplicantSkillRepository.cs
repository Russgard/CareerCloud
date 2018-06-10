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
    public class ApplicantSkillRepository : BaseADO, IDataRepository<ApplicantSkillPoco>
    {
        public void Add(params ApplicantSkillPoco[] items)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            using (_conn)
            {
                SqlCommand cmd = new SqlCommand { Connection = _conn };

                _conn.Open();
                foreach (ApplicantSkillPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO dbo.Applicant_Skills 
                    (Id, Applicant, Skill, Skill_Level, Start_Month, Start_Year, End_Month, End_Year) 
                    VALUES 
                    (@Id, @Applicant, @Skill, @Skill_Level, @Start_Month, @Start_Year, @End_Month, @End_Year);";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            ApplicantSkillPoco[] pocos = new ApplicantSkillPoco[1000];
            using (_conn)
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = _conn,
                    CommandText = @"SELECT * FROM dbo.Applicant_Skills;"
                };
                _conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int step = 0;
                while (reader.Read())
                {
                    ApplicantSkillPoco poco = new ApplicantSkillPoco
                    {
                        Id = (Guid)reader[0],
                        Applicant = (Guid)reader[1],
                        Skill = (string)reader[2],
                        SkillLevel = (string)reader[3],
                        StartMonth = (byte)reader[4],
                        StartYear = (int)reader[5],
                        EndMonth = (byte)reader[6],
                        EndYear = (int)reader[7],
                        TimeStamp = (byte[])reader[8]
                    };
                    pocos[step] = poco;
                    step++;
                }
                _conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            using (_conn)
            {

                SqlCommand cmd = new SqlCommand { Connection = _conn };

                _conn.Open();
                foreach (ApplicantSkillPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM dbo.Applicant_Skills 
                                    WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    int rowsEffected = cmd.ExecuteNonQuery();
                }
                _conn.Close();
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            using (_conn)
            {
                SqlCommand cmd = new SqlCommand { Connection = _conn };

                _conn.Open();
                foreach (ApplicantSkillPoco item in items)
                {
                    cmd.CommandText = @"UPDATE dbo.Applicant_Skills
                                    SET Applicant = @Applicant, 
                                        Skill = @Skill,
                                        Skill_Level = @Skill_Level, 
                                        Start_Month = @Start_Month, 
                                        Start_Year = @Start_Year,
                                        End_Month = @End_Month,
                                        End_Year = @End_Year
                                    WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
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
