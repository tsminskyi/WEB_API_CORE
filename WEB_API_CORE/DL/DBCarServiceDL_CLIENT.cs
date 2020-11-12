using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WEB_API_CORE.ConnectionDB;
using WEB_API_CORE.Models;

namespace WEB_API_CORE.DL
{
    public class DBCarServiceDL_CLIENT : ConnectionString
    {
        public static int ClientADD(ClientModel temp)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string comdText = "dbo.ClientADD";
                SqlCommand cmd = new SqlCommand(comdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd);

                cmd.Parameters[1].Value = temp.name;
                cmd.Parameters[2].Value = temp.lastName;
                cmd.Parameters[3].Value = temp.phone;
                cmd.Parameters[4].Value = temp.email;
                cmd.Parameters[5].Value = temp.birthDate;
                cmd.Parameters[6].Value = DBNull.Value;

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(cmd.Parameters[6].Value);
            }
        }

        public static List<ClientModel> ClientOutAll()
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string comdText = "dbo.ClientOutAll";
                SqlCommand cmd = new SqlCommand(comdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd);

                SqlDataReader reader = cmd.ExecuteReader();

                List<ClientModel> result = new List<ClientModel>();

                while (reader.Read())
                {
                    int clientID = int.Parse(reader[0].ToString());
                    string first_name = reader[1].ToString();
                    string last_name = reader[2].ToString();
                    string clientPhone = reader[3].ToString();
                    string clientEmail = reader[4].ToString();
                    DateTime bday = DateTime.Parse(reader[5].ToString());

                    result.Add(new ClientModel()
                    {
                        ID = clientID,
                        name = first_name,
                        lastName = last_name,
                        phone = clientPhone,
                        email = clientEmail,
                        birthDate = bday

                    });

                }

                reader.Close();

                return result;
            }
        }

        public static ClientModel ClientByID(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string cmdText = "dbo.ClientByID";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClientID", ID);

                SqlDataReader reader = cmd.ExecuteReader();

                ClientModel client = null;

                while (reader.Read())
                {
                    int clientID = int.Parse(reader[0].ToString());
                    string first_name = reader[1].ToString();
                    string last_name = reader[2].ToString();
                    string clientPhone = reader[3].ToString();
                    string clientEmail = reader[4].ToString();
                    DateTime bday = DateTime.Parse(reader[5].ToString());

                    client = new ClientModel()
                    {
                        ID = clientID,
                        name = first_name,
                        lastName = last_name,
                        phone = clientPhone,
                        email = clientEmail,
                        birthDate = bday
                    };
                }

                reader.Close();

                return client;
            }
        }

        public static List<ClientModel> ClientByPhone(string phone)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string cmdText = "dbo.ClientByPhone";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Phone", phone);

                SqlDataReader reader = cmd.ExecuteReader();

                List<ClientModel> clientList = new List<ClientModel>();

                while (reader.Read())
                {
                    int clientID = int.Parse(reader[0].ToString());
                    string first_name = reader[1].ToString();
                    string last_name = reader[2].ToString();
                    string clientPhone = reader[3].ToString();
                    string clientEmail = reader[4].ToString();
                    DateTime bday = DateTime.Parse(reader[5].ToString());

                    clientList.Add( new ClientModel()
                    {
                        ID = clientID,
                        name = first_name,
                        lastName = last_name,
                        phone = clientPhone,
                        email = clientEmail,
                        birthDate = bday

                    });
                }

                reader.Close();

                return clientList;
            }
        }

        public static bool ClientDeleteByID(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string comdText = "dbo.ClientDeleteByID";
                SqlCommand cmd = new SqlCommand(comdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd);

                cmd.Parameters[1].Value = ID;
                cmd.Parameters[2].Value = DBNull.Value;

                cmd.ExecuteNonQuery();

                return Convert.ToBoolean(cmd.Parameters[2].Value);
            }
        }

        public static bool ClientUpdate_SearchByID(ClientModel temp)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string cmdText = "dbo.ClientUpdateByID";
                SqlCommand cmd = new SqlCommand(cmdText, conn);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlCommandBuilder.DeriveParameters(cmd);

                cmd.Parameters[1].Value = temp.ID;
                cmd.Parameters[2].Value = temp.name;
                cmd.Parameters[3].Value = temp.lastName;
                cmd.Parameters[4].Value = temp.phone;
                cmd.Parameters[5].Value = temp.email;
                cmd.Parameters[6].Value = temp.birthDate;
                cmd.Parameters[7].Value = DBNull.Value;

                cmd.ExecuteReader();

                return Convert.ToBoolean(cmd.Parameters[7].Value);
            }
        }

    }


}

