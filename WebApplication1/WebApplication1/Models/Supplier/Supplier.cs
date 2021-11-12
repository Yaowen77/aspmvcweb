using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Supplier
{
    public class Supplier
    {
        [Required]
        [Display(Name = "廠商編號")]
        [StringLength(10, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 1)]
        public string SupplierID { get; set; }

        [Display(Name = "廠商名稱")]
        [StringLength(50, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 1)]
        public string FullName { get; set; }




        public static List<Models.Supplier.Supplier> Get_Supplier(string id)
        {
            List<Models.Supplier.Supplier> result = new List<Models.Supplier.Supplier>();

            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT SupplierID,FullName  FROM Supplier WHERE SupplierID = @ID And AFlag = 0";
                    command.Parameters.AddWithValue("@ID", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                result.Add(new Models.Supplier.Supplier()
                                {
                                    SupplierID = (string)reader["SupplierID"],
                                    FullName = (reader.IsDBNull(reader.GetOrdinal("FullName"))) ? "" : (string)reader["FullName"],
                                });
                            }
                            return result;
                        }
                        else
                        {
                            return result;
                        }
                    }
                }
            }
        }

        public static List<Models.Supplier.Supplier> Get_Supplier()
        {

            List<Models.Supplier.Supplier> result = new List<Models.Supplier.Supplier>();
            string connectionString = GlobalFunction.GlobalConnString;

            using (var conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT SupplierID,FullName  FROM Supplier WHERE  AFlag = 0";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new Models.Supplier.Supplier()
                                {
                                    SupplierID = (string)reader["SupplierID"],
                                    FullName = (reader.IsDBNull(reader.GetOrdinal("FullName"))) ? "" : (string)reader["FullName"],
                                });
                            }
                            return result;
                        }
                        else
                        {
                            return result;
                        }
                    }
                }
            }
        }

        public Models.Supplier.Supplier Get_Edit_Supplier(string id)
        {
            var result = new Models.Supplier.Supplier();

            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT SupplierID,FullName  FROM Supplier WHERE SupplierID = @ID ";
                    command.Parameters.AddWithValue("@ID", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {

                                result = new Models.Supplier.Supplier()
                                {
                                    SupplierID = (string)reader["SupplierID"],
                                    FullName = (reader.IsDBNull(reader.GetOrdinal("FullName"))) ? "" : (string)reader["FullName"],
                                };
                            }

                            return result;
                        }
                        else
                        {
                            return result;
                        }
                    }
                }
            }
        }


        public bool Patch_Supplier(Models.Supplier.Supplier supplier, string InputUserID)
        {
            var result = false;
            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "Update Supplier Set FullName = @FullName,EmployeeID =@EmployeeID,ModifyDate = Now() Where SupplierID = @SupplierID";
                    command.Parameters.AddWithValue("@SupplierID", supplier.SupplierID);
                    command.Parameters.AddWithValue("@FullName", supplier.FullName);
                    command.Parameters.AddWithValue("@EmployeeID", InputUserID);
                    command.ExecuteNonQuery();

                    result = true;
                    return result;
                }
            }
        }


        public bool Delete_Supplier(string id)
        {
            var result = false;
            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "Update Supplier Set AFlag = -1 Where SupplierID = @SupplierID";
                    command.Parameters.AddWithValue("@SupplierID", id);
                    command.ExecuteNonQuery();
                    result = true;
                    return result;
                }
            }
        }

        public bool IsSupplieridExist(string supplierId)
        {

            string connectionString = GlobalFunction.GlobalConnString;

            using (var conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT SupplierID  FROM Supplier WHERE SupplierID = @SupplierID";
                    command.Parameters.AddWithValue("@SupplierID", supplierId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }

        }

        public bool Post_Supplier(Models.Supplier.Supplier supplier,string InputUserID)
        {
            var result = false;
            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "Insert Into Supplier (SupplierID,AccountID,FullName,InputDate,EmployeeID) VALUES(@SupplierID,@AccountID,@FullName,CURDATE(),@InputUser)";
                    command.Parameters.AddWithValue("@SupplierID", supplier.SupplierID);
                    command.Parameters.AddWithValue("@AccountID", supplier.SupplierID);
                    command.Parameters.AddWithValue("@FullName", supplier.FullName);
                    command.Parameters.AddWithValue("@InputUser", InputUserID);
                    command.ExecuteNonQuery();
   
                    result = true;
                }
            }
            return result;
        }

    }
}