using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Policy;
using System.Data;

namespace ClinicDataLayer
{
    public class clsPersonData
    {


        public static int? AddNewPerson(string FirstName, string LastName, Nullable<DateTime> DateOfBirth, bool Gender, string PhoneNumber, string Email, string Address)
        {
            int? PersonID = null;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {



                    using (SqlCommand command = new SqlCommand("SP_AddNewPerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@FirstName", FirstName);
                        command.Parameters.AddWithValue("@LastName", LastName);
                        command.Parameters.AddWithValue("@Gender", Gender);
                        command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                        command.Parameters.AddWithValue("@Email", (object)Email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Address", (object)Address ?? DBNull.Value);
                        command.Parameters.AddWithValue("@DateOfBirth", (object)DateOfBirth ?? DBNull.Value);


                        //if (DateOfBirth == null)
                        //    command.Parameters.AddWithValue("@DateOfBirth", DBNull.Value);
                        //else
                        //    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

              

                        //if (string.IsNullOrEmpty(Email))
                        //    command.Parameters.AddWithValue("@Email", DBNull.Value);
                        //else
                        //    command.Parameters.AddWithValue("@Email", Email);

                        //if (string.IsNullOrEmpty(Address))
                        //    command.Parameters.AddWithValue("@Address", DBNull.Value);
                        //else
                        //    command.Parameters.AddWithValue("@Address", Address);

                        SqlParameter outputIdParam = new SqlParameter("@PersonID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);


                        connection.Open();

                        command.ExecuteNonQuery();

                        PersonID = (int)outputIdParam.Value;

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AddNewPerson: " + ex.Message);
            }


            return PersonID;
        }

        public static bool UpdatePerson(int PersonID, string FirstName, string LastName, Nullable<DateTime> DateOfBirth, bool Gender, string PhoneNumber, string Email, string Address)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    //string query = @"Update  People  
                    //        set FirstName = @FirstName,                             
                    //            LastName = @LastName,                            
                    //            DateOfBirth = @DateOfBirth,
                    //            Gender=@Gender,
                    //            Address = @Address,  
                    //            PhoneNumber = @PhoneNumber,
                    //            Email = @Email                     
                    //            where PersonID = @PersonID";

                    using (SqlCommand command = new SqlCommand("SP_UpdatePerson", connection))
                    {
                        command.CommandType= CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@FirstName", FirstName);
                        command.Parameters.AddWithValue("@LastName", LastName);
                        command.Parameters.AddWithValue("@Gender", Gender);
                        command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                        command.Parameters.AddWithValue("@Email", (object)Email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Address", (object)Address ?? DBNull.Value);
                        command.Parameters.AddWithValue("@DateOfBirth", (object)DateOfBirth ?? DBNull.Value);

                        //if (DateOfBirth == null)
                        //    command.Parameters.AddWithValue("@DateOfBirth", DBNull.Value);
                        //else
                        //    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);



                        //if (string.IsNullOrEmpty(Email))
                        //    command.Parameters.AddWithValue("@Email", DBNull.Value);
                        //else
                        //    command.Parameters.AddWithValue("@Email", Email);

                        //if (string.IsNullOrEmpty(Address))
                        //    command.Parameters.AddWithValue("@Address", DBNull.Value);
                        //else
                        //    command.Parameters.AddWithValue("@Address", Address);



                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdatePerson: " + ex.Message);

            }



            return (rowsAffected > 0);




        }


        public static bool Find(int PersonID, ref string FirstName, ref string LastName, ref Nullable<DateTime> DateOfBirth, ref bool Gender, ref string PhoneNumber, ref string Email, ref string Address)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    //string query = "SELECT * FROM People WHERE PersonID = @PersonID";

                    SqlCommand command = new SqlCommand("SP_FindPerson", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", PersonID);


                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            // The record was found
                            isFound = true;

                            FirstName = (string)reader["FirstName"];
                            LastName = (string)reader["LastName"];
                            PersonID = (int)reader["PersonID"];
                            if (reader["DateOfBirth"] == DBNull.Value)
                            {
                                DateOfBirth = null;
                            }
                            else
                                DateOfBirth = (DateTime)reader["DateOfBirth"];

                            Gender = (bool)reader["Gender"];
                            PhoneNumber = (string)reader["PhoneNumber"];
                            if (reader["Email"] == DBNull.Value)
                            {
                                Email = null;
                            }
                            else
                                Email = (string)reader["Email"];

                            if (reader["Address"] == DBNull.Value)
                            {
                                Address = null;
                            }
                            else
                                Address = (string)reader["Address"];



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


        public static bool IsEmailExist(string Email)
        {
            bool EmailExists = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    Connection.Open();

                    //string Query = "select found=1 from People where People.Email= @Email";
                    using (SqlCommand Command = new SqlCommand("SP_IsEmailExists", Connection))
                    {

                        Command.Parameters.AddWithValue("@Email", (object)Email ?? DBNull.Value);

                        SqlParameter returnParameter = new SqlParameter
                        {
                            Direction = ParameterDirection.ReturnValue
                        };

                        Command.Parameters.Add(returnParameter);
                        Command.ExecuteScalar();
                        int Resulte = (int)returnParameter.Value;
                        EmailExists = (Resulte == 1);






                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("IsEmailExist" + ex.Message);
            }



                return EmailExists;



            }



        }

    
    }

