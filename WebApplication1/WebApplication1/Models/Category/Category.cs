using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;

namespace WebApplication1.Models.Category
{

    public class Category
    {

        [Display(Name = "類別種類")]
        public int CateType { get; set; }

        [Required]
        [Display(Name = "類別編號")]
        [StringLength(4, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 1)]
        public string CategoryID { get; set; }

        [Display(Name = "類別名稱")]
        [StringLength(20, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 1)]
        public string CategoryName { get; set; }

        public Category()
        {

        }

        public static List<Models.Category.Category> Get_Gategory(int id)
        {

            List<Models.Category.Category> result = new List<Models.Category.Category>();
            string connectionString = GlobalFunction.GlobalConnString;

            using (var conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT Category, Category_Name FROM Category WHERE CateType = @CateType";
                    command.Parameters.AddWithValue("@CateType", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new Models.Category.Category()
                                {
                                    CategoryID = (string)reader["Category"],
                                    CategoryName = (reader.IsDBNull(reader.GetOrdinal("Category_Name"))) ? "" : (string)reader["Category_Name"],
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
        public bool Delete_Category(int type, string categoryID)
        {
            var result = false;
            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "Delete From Category  Where Category = @Category And CateType = @CateType";
                    command.Parameters.AddWithValue("@Category", categoryID);
                    command.Parameters.AddWithValue("@CateType", type);
                    command.ExecuteNonQuery();
                    result = true;
                    return result;
                }
            }
        }

        public bool IsCategoryidExist(string categoryid,int type)
        {

            string connectionString = GlobalFunction.GlobalConnString;

            using (var conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT Category  FROM Category WHERE Category = @Category And CateType = @CateType";
                    command.Parameters.AddWithValue("@Category", categoryid);
                    command.Parameters.AddWithValue("@CateType", type);

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

        public bool Post_Category(Models.Category.Category category,int type, string InputUserID)
        {
            var result = false;
            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "Insert Into Category (CateType,Category,Category_Name,ModifyDate,ModifyEID) VALUES(@CateType,@Category,@Category_Name,Now(),@InputUser)";
                    command.Parameters.AddWithValue("@CateType", type);
                    command.Parameters.AddWithValue("@Category", category.CategoryID);
                    command.Parameters.AddWithValue("@Category_Name", category.CategoryName);
                    command.Parameters.AddWithValue("@InputUser", InputUserID);
                    command.ExecuteNonQuery();
                    result = true;
                }
            }
            return result;
        }


        public Models.Category.Category Get_Edit_Category(string categoryID, int type)
        {
            var result = new Models.Category.Category();

            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT Category, Category_Name FROM Category WHERE Category = @Category And CateType = @CateType";
                    command.Parameters.AddWithValue("@Category", categoryID);
                    command.Parameters.AddWithValue("@CateType", type);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                result = new Models.Category.Category()
                                {
                                    CategoryID = (string)reader["Category"],
                                    CategoryName = (reader.IsDBNull(reader.GetOrdinal("Category_Name"))) ? "" : (string)reader["Category_Name"]
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


        public bool Patch_Category(Models.Category.Category category, int type, string InputUserID)
        {
            var result = false;
            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "Update Category Set Category_Name = @Category_Name ,ModifyEID =@ModifyEID,ModifyDate = Now() Where Category = @Category And CateType = @CateType";
                    command.Parameters.AddWithValue("@CateType", type);
                    command.Parameters.AddWithValue("@Category", category.CategoryID);
                    command.Parameters.AddWithValue("@Category_Name", category.CategoryName);
                    command.Parameters.AddWithValue("@ModifyEID", InputUserID);
                    command.ExecuteNonQuery();

                    result = true;
                    return result;
                }
            }
        }

        public static List<Models.Category.Category> Get_Category(string categoryID, int type)
        {
            List<Models.Category.Category> result = new List<Models.Category.Category>();

            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT Category, Category_Name FROM Category WHERE Category = @Category And CateType = @CateType";
                    command.Parameters.AddWithValue("@Category", categoryID);
                    command.Parameters.AddWithValue("@CateType", type);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                result.Add(new Models.Category.Category()
                                {
                                    CategoryID = (string)reader["Category"],
                                    CategoryName = (reader.IsDBNull(reader.GetOrdinal("Category_Name"))) ? "" : (string)reader["Category_Name"]
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


    }
}