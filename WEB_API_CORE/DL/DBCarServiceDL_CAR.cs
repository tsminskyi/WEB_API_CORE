using System;
using System.Data;
using System.Data.SqlClient;
using WEB_API_CORE.ConnectionDB;
using WEB_API_CORE.Models;

namespace WEB_API_CORE.DL
{
    public class DBCarServiceDL_CAR : ConnectionString
    {


        public static int AddCar(CarModel temp)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string comdText = "dbo.AddCar";
                SqlCommand cmd = new SqlCommand(comdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd);

                cmd.Parameters[1].Value = temp.VIN;
                cmd.Parameters[2].Value = temp.stateNumber;
                cmd.Parameters[3].Value = temp.brand;
                cmd.Parameters[4].Value = temp.model;
                cmd.Parameters[5].Value = temp.mileage;
                cmd.Parameters[6].Value = temp.yearOfIssue;
                cmd.Parameters[7].Value = temp.clientID;
                cmd.Parameters[8].Value = DBNull.Value;

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(cmd.Parameters[8].Value);
            }
        }

        public static CarModel CarByNumber(string stateNumber)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string comdText = "dbo.CarByNumber";
                SqlCommand cmd = new SqlCommand(comdText, conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Number", stateNumber);

                SqlDataReader reader = cmd.ExecuteReader();

                CarModel car = null;

                while (reader.Read())
                {
                    int carID = Convert.ToInt32(reader[0].ToString());
                    string VINCar = reader[1].ToString();
                    string stateNumberCar = reader[2].ToString();
                    string brandCar = reader[3].ToString();
                    string modelCar = reader[4].ToString();
                    int mileageCar = Convert.ToInt32(reader[5]);
                    DateTime yearOfIssueCar = DateTime.Parse(reader[6].ToString());
                    int clientCarID = Convert.ToInt32(reader[7]);

                    car = new CarModel()
                    {
                        ID = carID,
                        VIN = VINCar,
                        stateNumber = stateNumberCar,
                        brand = brandCar,
                        model = modelCar,
                        mileage = mileageCar,
                        yearOfIssue = yearOfIssueCar,
                        clientID = clientCarID
                    };
                }

                reader.Close();

                return car;
            }
        }

        public static bool CarDeleteByID(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string comdText = "dbo.CarDeleteByID";
                SqlCommand cmd = new SqlCommand(comdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd);

                cmd.Parameters[1].Value = ID;

                cmd.Parameters[2].Value = DBNull.Value;

                cmd.ExecuteNonQuery();

                return Convert.ToBoolean(cmd.Parameters[2].Value);
            }
        }

        public static bool CarUpdate_searchByID(CarModel temp)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string cmdText = "dbo.CarUpdateByID";
                SqlCommand cmd = new SqlCommand(cmdText, conn);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd);

                cmd.Parameters[1].Value = temp.VIN;
                cmd.Parameters[2].Value = temp.stateNumber;
                cmd.Parameters[3].Value = temp.brand;
                cmd.Parameters[4].Value = temp.model;
                cmd.Parameters[5].Value = temp.mileage;
                cmd.Parameters[6].Value = temp.yearOfIssue;
                cmd.Parameters[7].Value = temp.clientID;
                cmd.Parameters[7].Value = DBNull.Value;

                cmd.ExecuteNonQuery();

                return Convert.ToBoolean(cmd.Parameters[7].Value);
            }
        }
    }
}

