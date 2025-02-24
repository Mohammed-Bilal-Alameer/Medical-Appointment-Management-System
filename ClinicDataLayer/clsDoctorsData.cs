using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicDataLayer
{
    public class clsDoctorsData
    {
        public static int? AddNewDoctor(int PersonID, string Specialization, string Username, string Password)
        {
            int? DoctorID = -1;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {


                    //string query = @"INSERT INTO Doctors( PersonID,
                    //                      Specialization,
                    //                        Username,
                    //                        Password

                    //       )

                    //            VALUES(                           
                    //         @PersonID,                         
                    //         @Specialization,
                    //         @Username,
                    //         @Password
                    //         );
                    //         SELECT SCOPE_IDENTITY()";

                    using (SqlCommand command = new SqlCommand("SP_AddNewDoctor", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);

                        command.Parameters.AddWithValue("@Specialization",(object)Specialization?? DBNull.Value);                
                        command.Parameters.AddWithValue("@Username", Username);
                        command.Parameters.AddWithValue("@Password", Password);

                        SqlParameter OutputParameter = new SqlParameter("DoctorID", SqlDbType.Int)
                        {

                            Direction = ParameterDirection.Output

                        };
                        command.Parameters.Add(OutputParameter);

                        connection.Open();
                        command.ExecuteNonQuery();
                        DoctorID = (int)OutputParameter.Value;


                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AddNewDoctor" + ex.Message);
            }


            return DoctorID;
        }


        public static bool UpdateDoctorInfo(int DoctorID, int PersonID, string Specialization)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    //string query = @"Update  Doctors  
                    //        set PersonID = @PersonID,                             
                    //            Specialization = @Specialization                            
                               
                    //            where DoctorID = @DoctorID";

                    using (SqlCommand command = new SqlCommand("SP_UpdateDoctorSpecialization", connection))
                    {
                        command.CommandType= CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DoctorID", DoctorID);

                        command.Parameters.AddWithValue("@PersonID", PersonID);
            
                        command.Parameters.AddWithValue("@Specialization", (object)Specialization?? DBNull.Value);
                    

                        


                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateDoctor: " + ex.Message);

            }



            return (rowsAffected > 0);




        }

        public static bool Find(int DoctorID, ref Nullable<int> PersonID, ref string UserName, ref string Password, ref string Specialization)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    //string query = "SELECT * FROM Doctors WHERE DoctorID = @DoctorID";

                    SqlCommand command = new SqlCommand("SP_FindDoctorByID", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DoctorID", DoctorID);


                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            // The record was found
                            isFound = true;

                            UserName = (string)reader["UserName"];
                            Password = (string)reader["Password"];
                            PersonID = (int)reader["PersonID"];
                            if (reader["Specialization"] == DBNull.Value)
                            {
                                Specialization = null;
                            }
                            else
                                Specialization = (string)reader["Specialization"];



                        }
                        else
                        {
                            // The record was not found
                            isFound = false;
                        }

                        reader.Close();


                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write("Find" + ex.ToString());
            }




            return isFound;
        }
        public static bool FindByUserName(ref Nullable<int> DoctorID, ref Nullable<int> PersonID, string UserName, ref string Password, ref string Specialization)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    //string query = "SELECT * FROM Doctors WHERE UserName = @UserName";

                    SqlCommand command = new SqlCommand("SP_FindDoctorByUserName", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserName", UserName);


                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            // The record was found
                            isFound = true;

                            DoctorID = (int)reader["DoctorID"];
                            Password = (string)reader["Password"];
                            PersonID = (int)reader["PersonID"];
                            if (reader["Specialization"] == DBNull.Value)
                            {
                                Specialization = null;
                            }
                            else
                                Specialization = (string)reader["Specialization"];



                        }
                        else
                        {
                            // The record was not found
                            isFound = false;
                        }

                        reader.Close();


                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write("FindByUserName" + ex.ToString());
            }




            return isFound;
        }

        public static bool IsUsernameExist(string Username)
        {

            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                bool IsUserNameExists = false;
                try
                {

                    //string Query = "select found=1 from Doctors where Doctors.Username @Username";
                    using (SqlCommand Command = new SqlCommand("SP_IsUsernameExist", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@Username", (object)Username ?? DBNull.Value);

                        SqlParameter returnParameter = new SqlParameter
                        {
                            Direction = ParameterDirection.ReturnValue
                        };

                        Command.Parameters.Add(returnParameter);
                        Connection.Open();

                        Command.ExecuteScalar();
                        int Resulte = (int)returnParameter.Value;
                        IsUserNameExists = (Resulte == 1);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("IsUsernameExist" + ex.Message);
                }



                return IsUserNameExists;



            }



        }

        public static bool ChangePassword (string NewPassword,int DoctorID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    //string query = @"Update  Doctors  
                    //        set Password = @NewPassword                            
                                                                                         
                    //            where DoctorID = @DoctorID";

                    using (SqlCommand command = new SqlCommand("SP_ChangePassword", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@DoctorID", DoctorID);

                        command.Parameters.AddWithValue("@Password", NewPassword);

                   


                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ChangePassword: " + ex.Message);

            }



            return (rowsAffected > 0);




        }



    }
}
