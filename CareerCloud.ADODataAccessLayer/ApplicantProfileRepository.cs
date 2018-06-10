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
    public class ApplicantProfileRepository : BaseADO,IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            using (_conn)
            {
                SqlCommand cmd = new SqlCommand { Connection = _conn };

                _conn.Open();
                foreach (ApplicantProfilePoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO dbo.Applicant_Profiles 
                    (Id, Login, Current_Salary, Current_Rate, Currency, Country_Code, State_Province_Code, Street_Address, City_Town, Zip_Postal_Code) 
                    VALUES 
                    (@Id, @Login, @Current_Salary, @Current_Rate, @Currency, @Country_Code, @State_Province_Code, @Street_Address, @City_Town, @Zip_Postal_Code);";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", item.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", item.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                    cmd.Parameters.AddWithValue("@City_Town", item.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

                    int rowsEffected = cmd.ExecuteNonQuery();
                }
                _conn.Close();
            }            
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[500];
            using (_conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandText = @"SELECT * FROM dbo.Applicant_Profiles;";
                _conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int step = 0;
                while (reader.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco
                    {
                        Id = (Guid)reader[0],
                        Login = (Guid)reader[1],
                        CurrentSalary = (decimal?)reader[2],
                        CurrentRate = (decimal?)reader[3],
                        Currency = (string)reader[4],
                        Country = (string)reader[5],
                        Province = (string)reader[6],
                        Street = (string)reader[7],
                        City = (string)reader[8],
                        PostalCode = (string)reader[9],
                        TimeStamp = (byte[])reader[10]
                    };
                    pocos[step] = poco;
                    step++;
                }
                _conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            using (_conn)
            {               
                SqlCommand cmd = new SqlCommand { Connection = _conn };

                _conn.Open();
                foreach (ApplicantProfilePoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM dbo.Applicant_Profiles 
                                    WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    int rowsEffected = cmd.ExecuteNonQuery();
                }
                _conn.Close();
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            using (_conn)
            {

                SqlCommand cmd = new SqlCommand { Connection = _conn };

                _conn.Open();
                foreach (ApplicantProfilePoco item in items)
                {
                    cmd.CommandText = @"UPDATE dbo.Applicant_Profiles
                                    SET Login = @Login,
                                        Current_Salary = @Current_Salary, 
                                        Current_Rate = @Current_Rate, 
                                        Currency = @Currency,
                                        Country_Code = @Country_Code, 
                                        State_Province_Code = @State_Province_Code,
                                        Street_Address = @Street_Address, 
                                        City_Town = @City_Town,
                                        Zip_Postal_Code = @Zip_Postal_Code
                                    WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", item.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", item.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                    cmd.Parameters.AddWithValue("@City_Town", item.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

                    int rowsEffected = cmd.ExecuteNonQuery();
                }
                _conn.Close();
            }
        }
    }
}
