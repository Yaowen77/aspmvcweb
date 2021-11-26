using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace WebApplication1.Models.Login
{

    public class Login
    {
        [Required]
        [Display(Name = "帳號")]
        [StringLength(10, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 4)]
        public string UserID { get; set; }

        [Required]
        [Display(Name = "密碼")]
        [StringLength(10, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string UserPwd { get; set; }

        public static async System.Threading.Tasks.Task<bool> LoginAccountAsync(string userID, string receivePassword)
        {
          
            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {

                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT *  FROM employee WHERE EmployeeID = @ID and Passwd = @Password ;";
                    command.Parameters.AddWithValue("@ID", userID);
                    command.Parameters.AddWithValue("@Password", receivePassword);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {


                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

        }
    }
}