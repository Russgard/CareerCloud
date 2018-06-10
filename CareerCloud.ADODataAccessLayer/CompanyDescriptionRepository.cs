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
    public class CompanyDescriptionRepository : BaseADO, IDataRepository<CompanyDescriptionPoco>
    {
        public void Add(params CompanyDescriptionPoco[] items)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            using (_conn)
            {
                SqlCommand cmd = new SqlCommand { Connection = _conn };

                _conn.Open();

                foreach (CompanyDescriptionPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO dbo.Company_Descriptions 
                    (Id, Company, LanguageID, Company_Name, Company_Description) 
                    VALUES 
                    (@Id, @Company, @LanguageID, @Company_Name, @Company_Description);";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);

                    int rowsEffected = cmd.ExecuteNonQuery();
                }
                _conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            CompanyDescriptionPoco[] pocos = new CompanyDescriptionPoco[1000];
            using (_conn)
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = _conn,
                    CommandText = @"SELECT * FROM dbo.Company_Descriptions;"
                };
                _conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int step = 0;
                while (reader.Read())
                {
                    CompanyDescriptionPoco poco = new CompanyDescriptionPoco
                    {
                        Id = (Guid)reader[0],
                        Company = (Guid)reader[1],
                        LanguageId = (string)reader[2],
                        CompanyName = (string)reader[3],
                        CompanyDescription = (string)reader[4],
                        TimeStamp = (byte[])reader[5]
                    };

                    pocos[step] = poco;
                    step++;
                }
                _conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            using (_conn)
            {

                SqlCommand cmd = new SqlCommand { Connection = _conn };

                _conn.Open();
                foreach (CompanyDescriptionPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM dbo.Company_Descriptions 
                                    WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    int rowsEffected = cmd.ExecuteNonQuery();
                }
                _conn.Close();
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            using (_conn)
            {
                SqlCommand cmd = new SqlCommand { Connection = _conn };

                _conn.Open();
                foreach (CompanyDescriptionPoco item in items)
                {
                    cmd.CommandText = @"UPDATE dbo.Company_Descriptions
                                    SET Company = @Company,
                                        LanguageID = @LanguageID,
                                        Company_Name = @Company_Name, 
                                        Company_Description = @Company_Description
                                    WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);

                    int rowsEffected = cmd.ExecuteNonQuery();
                }
                _conn.Close();
            }
        }
    }
}
