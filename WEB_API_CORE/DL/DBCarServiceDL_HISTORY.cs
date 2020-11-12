using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WEB_API_CORE.ConnectionDB;
using WEB_API_CORE.Models;

namespace WEB_API_CORE.DL
{
    public class DBCarServiceDL_HISTORY : ConnectionString
    {
        public static int WorkHistoriesADD(HistoryModel temp)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string comdText = "dbo.WorkHistoriesADD";
                SqlCommand cmd = new SqlCommand(comdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd);

                cmd.Parameters[1].Value = temp.workData;
                cmd.Parameters[2].Value = temp.stateNumber;
                cmd.Parameters[3].Value = temp.brand;
                cmd.Parameters[4].Value = temp.model;
                cmd.Parameters[5].Value = temp.intermediateMileage;
                cmd.Parameters[6].Value = temp.carID;

                cmd.Parameters[7].Value = DBNull.Value;

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(cmd.Parameters[7].Value);
            }
        }

        public static List<HistoryModel> WorkHistoriesByClientID(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string cmdText = "dbo.WorkHistoriesByClientID";
                SqlCommand cmd = new SqlCommand(cmdText, conn);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlCommandBuilder.DeriveParameters(cmd);

                cmd.Parameters[1].Value = ID;

                SqlDataReader reader = cmd.ExecuteReader();

                List<HistoryModel> workHistores = null;

                while (reader.Read())
                {
                    int workID = Convert.ToInt32(reader[0].ToString()); ;
                    string workDataHistory = reader[1].ToString();
                    string stateNumberHistory = reader[2].ToString();
                    string brandHistory = reader[3].ToString();
                    string modelHistory = reader[4].ToString();
                    int intermediateMileageHistory = Convert.ToInt32(reader[5].ToString());
                    int carIDHistory = Convert.ToInt32(reader[6].ToString());

                    workHistores.Add(new HistoryModel()
                    {
                        ID = workID,
                        workData = workDataHistory,
                        stateNumber = stateNumberHistory,
                        brand = brandHistory,
                        model = modelHistory,
                        intermediateMileage = intermediateMileageHistory,
                        carID = carIDHistory
                    });
                }

                reader.Close();

                return workHistores;
            }
        }
    }
}

