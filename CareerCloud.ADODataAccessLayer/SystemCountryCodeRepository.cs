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
    public class SystemCountryCodeRepository : BaseADO, IDataRepository<SystemCountryCodePoco>
    {
        public void Add(params SystemCountryCodePoco[] items)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            using (_conn)
            {
                SqlCommand cmd = new SqlCommand { Connection = _conn };

                _conn.Open();
                foreach (SystemCountryCodePoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO dbo.System_Country_Codes 
                    (Code, Name) 
                    VALUES 
                    (@Code, @Name);";

                    cmd.Parameters.AddWithValue("@Code", item.Code);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                    
                    int rowsEffected = cmd.ExecuteNonQuery();
                }
                _conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            SystemCountryCodePoco[] pocos = new SystemCountryCodePoco[1000];
            using (_conn)
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = _conn,
                    CommandText = @"SELECT * FROM dbo.System_Country_Codes;"
                };
                _conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int step = 0;
                while (reader.Read())
                {
                    SystemCountryCodePoco poco = new SystemCountryCodePoco
                    {
                        Code = (string)reader[0],
                        Name = (string)reader[1]
                    };

                    pocos[step] = poco;
                    step++;
                }
                _conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            using (_conn)
            {

                SqlCommand cmd = new SqlCommand { Connection = _conn };

                _conn.Open();
                foreach (SystemCountryCodePoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM dbo.System_Country_Codes 
                                    WHERE Code = @Id;";
                    cmd.Parameters.AddWithValue("@Id", item.Code);

                    int rowsEffected = cmd.ExecuteNonQuery();
                }
                _conn.Close();
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            SqlConnection _conn = new SqlConnection(_connString);
            using (_conn)
            {
                SqlCommand cmd = new SqlCommand { Connection = _conn };

                _conn.Open();
                foreach (SystemCountryCodePoco item in items)
                {
                    cmd.CommandText = @"UPDATE dbo.System_Country_Codes
                                    SET Name = @Name
                                    WHERE Code = @Code;";
                    cmd.Parameters.AddWithValue("@Code", item.Code);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                    int rowsEffected = cmd.ExecuteNonQuery();
                }
                _conn.Close();
            }
        }
    }
}
