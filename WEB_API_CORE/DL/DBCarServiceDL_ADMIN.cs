using System;
using System.Data;
using System.Data.SqlClient;
using WEB_API_CORE.Models;
using WEB_API_CORE.ConnectionDB;

namespace WEB_API_CORE.DL
{
    public class DBCarServiceDL_ADMIN : ConnectionString
    {
        public static int AdminADD(AdminModel temp)
        {

            var searchAdmin = SearchAdmin(temp.login);
            if (searchAdmin != null)
            {
                return -1;
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                       
                        string comdText = "AdminADD";
                        SqlCommand cmd = new SqlCommand(comdText, conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlCommandBuilder.DeriveParameters(cmd);

                        cmd.Parameters[1].Value = temp.login;
                        cmd.Parameters[2].Value = temp.password;
                        cmd.Parameters[3].Value = temp.role;
                        cmd.Parameters[4].Value = DBNull.Value;

                        cmd.ExecuteNonQuery();

                        return Convert.ToInt32(cmd.Parameters[4].Value);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
               
            }

        }

        public static AdminModel SearchAdmin(string login)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string comdText = "OutputAdmin";

                SqlCommand cmd = new SqlCommand(comdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd);

                cmd.Parameters[1].Value = login;

                SqlDataReader reader = cmd.ExecuteReader();

                AdminModel result = null;
                while (reader.Read())
                {
                    int readerID = int.Parse(reader[0].ToString());
                    string readerLogin = reader[1].ToString();
                    string readerPassword = reader[2].ToString();
                    string readerRole = reader[3].ToString();

                    result = new AdminModel()
                    {
                        ID = readerID,
                        login = readerLogin,
                        password = readerPassword,
                        role = readerRole

                    };

                }

                reader.Close();

                return result;
            }

        }
    }
}

