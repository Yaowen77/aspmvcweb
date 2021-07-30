using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using static WebApplication1.Controllers.MemberController;

namespace WebApplication1.Models.Member
{

    public class Member
    {
        [Required]
        [Display(Name = "會員編號")]
        [StringLength(20, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 1)]
        public string MemberID { get; set; }
        [Display(Name = "會員姓名")]
        [StringLength(40, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 1)]
        public string MemberName { get; set; }
        [Display(Name = "會員手機")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(20, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 1)]
        public string MobilPhone { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 1)]
        public string EMail { get; set; }


        [Display(Name = "會員圖片")]
        public string ImageName { get; set; }

        public byte[] MemberImage { get; set; }


        public Member()
        {
            
        }

        public static List<Member>  Get_Member()
        {

            List<Member> result = new List<Member>();
            string connectionString = GlobalFunction.GlobalConnString;

            using (var conn = new MySqlConnection(connectionString))
            {
                
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT MemberID,MemberName,MobilPhone,EMail  FROM Member";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {                           
                            while (reader.Read())
                            {
                                result.Add(new Member()
                                {
                                    MemberID = (string)reader["MemberID"],
                                    MemberName = (reader.IsDBNull(reader.GetOrdinal("MemberName"))) ? "" : (string)reader["MemberName"],
                                    MobilPhone = (reader.IsDBNull(reader.GetOrdinal("MobilPhone"))) ? "" : (string)reader["MobilPhone"],
                                    EMail = (reader.IsDBNull(reader.GetOrdinal("EMail"))) ? "" : (string)reader["EMail"],
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

        public static List<Member> Get_Member(string id)
        {
            List<Member> result = new List<Member>();

            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT MemberID,MemberName,MobilPhone,EMail  FROM Member WHERE MemberID = @ID";
                    command.Parameters.AddWithValue("@ID", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                result.Add(new Member()
                                {
                                    MemberID = (string)reader["MemberID"],
                                    MemberName = (reader.IsDBNull(reader.GetOrdinal("MemberName"))) ? "" : (string)reader["MemberName"],
                                    MobilPhone = (reader.IsDBNull(reader.GetOrdinal("MobilPhone"))) ? "" : (string)reader["MobilPhone"],
                                    EMail = (reader.IsDBNull(reader.GetOrdinal("EMail"))) ? "" : (string)reader["EMail"],
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



        public Member Get_Edit_Member(string id)
        {
            var result = new Member();

            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT MemberID,MemberName,MobilPhone,EMail,ImageName,ImageData  FROM Member WHERE MemberID = @ID";
                    command.Parameters.AddWithValue("@ID", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {

                                if (!reader.IsDBNull(reader.GetOrdinal("ImageData")))
                                {
                                    byte[] buffer = (byte[])reader["ImageData"];


                                    result = new Member()
                                    {
                                        MemberID = (string)reader["MemberID"],
                                        MemberName = (reader.IsDBNull(reader.GetOrdinal("MemberName"))) ? "" : (string)reader["MemberName"],
                                        MobilPhone = (reader.IsDBNull(reader.GetOrdinal("MobilPhone"))) ? "" : (string)reader["MobilPhone"],
                                        EMail = (reader.IsDBNull(reader.GetOrdinal("EMail"))) ? "" : (string)reader["EMail"],
                                        ImageName = (reader.IsDBNull(reader.GetOrdinal("ImageName"))) ? "" : (string)reader["ImageName"],
                                        MemberImage = buffer

                                    };
                                }
                                else
                                {

                                    result = new Member()
                                    {
                                        MemberID = (string)reader["MemberID"],
                                        MemberName = (reader.IsDBNull(reader.GetOrdinal("MemberName"))) ? "" : (string)reader["MemberName"],
                                        MobilPhone = (reader.IsDBNull(reader.GetOrdinal("MobilPhone"))) ? "" : (string)reader["MobilPhone"],
                                        EMail = (reader.IsDBNull(reader.GetOrdinal("EMail"))) ? "" : (string)reader["EMail"],
                                        ImageName = (reader.IsDBNull(reader.GetOrdinal("ImageName"))) ? "" : (string)reader["ImageName"]
                                    };
                                }
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
        
        public bool Patch_Member(Member member, HttpPostedFileBase file, byte[] FileBytes, string InputUserID)
        {
            var result = false;
            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "Update Member Set MemberName = @MemberName , MobilPhone = @MobilPhone ,EMail = @EMail,ModifyEID =@ModifyEID,ModifyDate = Now() Where MemberID = @MemberID";
                    command.Parameters.AddWithValue("@MemberID", member.MemberID);
                    command.Parameters.AddWithValue("@MemberName", member.MemberName);
                    command.Parameters.AddWithValue("@MobilPhone", member.MobilPhone);
                    command.Parameters.AddWithValue("@EMail", member.EMail);
                    command.Parameters.AddWithValue("@ModifyEID", InputUserID);
                    command.ExecuteNonQuery();

                    if (FileBytes != null)
                    {

                        command.CommandText = "Update Member Set ImageName = @ImageName , ImageData = @ImageData  Where MemberID = @MemberID2";
                        command.Parameters.AddWithValue("@MemberID2", member.MemberID);
                        command.Parameters.AddWithValue("@ImageName", file.FileName);
                        command.Parameters.AddWithValue("@ImageData", FileBytes);
                        command.ExecuteNonQuery();
                    }

                    result = true;
                    return result;
                }
            }
        }

        public bool Delete_Member(string id)
        {
            var result = false;
            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "Delete From Member  Where MemberID = @MemberID";
                    command.Parameters.AddWithValue("@MemberID", id);
                    command.ExecuteNonQuery();
                    result = true;
                    return result;
                }
            }
        }

        public bool Post_Member(Member member, HttpPostedFileBase file, byte[] FileBytes,string InputUserID)
        {
            var result = false;
            string storeId = Get_StoreID();
            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "Insert Into Member (StoreID,MemberID,AccountID,MemberName,MobilPhone,EMail,InputDate,InputUser) VALUES(@StoreID,@MemberID,@AccountID,@MemberName,@MobilPhone,@EMail,CURDATE(),@InputUser)";
                    command.Parameters.AddWithValue("@StoreID", storeId);
                    command.Parameters.AddWithValue("@MemberID", member.MemberID);
                    command.Parameters.AddWithValue("@AccountID", member.MemberID);
                    command.Parameters.AddWithValue("@MemberName", member.MemberName);
                    command.Parameters.AddWithValue("@MobilPhone", member.MobilPhone);
                    command.Parameters.AddWithValue("@EMail", member.EMail);
                    command.Parameters.AddWithValue("@InputUser", InputUserID);
                    command.ExecuteNonQuery();


                    if (FileBytes != null)
                    {
                        command.CommandText = "Update Member Set ImageName = @ImageName , ImageData = @ImageData  Where MemberID = @MemberID2";
                        command.Parameters.AddWithValue("@MemberID2", member.MemberID);
                        command.Parameters.AddWithValue("@ImageName", file.FileName);
                        command.Parameters.AddWithValue("@ImageData", FileBytes);
                        command.ExecuteNonQuery();
                    }
                    result = true;           
                }
            }
            return result;
        }

        //預設總部門市
        public string Get_StoreID()
        {
            string storeId = "00";


            string connectionString = GlobalFunction.GlobalConnString;

            using (var conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT StoreID  FROM STORE WHERE HOFFICE = 1";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                storeId = (reader.IsDBNull(reader.GetOrdinal("StoreID"))) ? "00" : (string)reader["StoreID"];

                            }
                            return storeId;
                        }
                        else
                        {
                            return storeId;
                        }
                    }
                }
            }

        }



        public bool IsMemberidExist(string memberid)
        {
           
            string connectionString = GlobalFunction.GlobalConnString;

            using (var conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT MemberID  FROM Member WHERE MemberID = @MemberID";
                    command.Parameters.AddWithValue("@MemberID", memberid);

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




        //圖片結尾是否為gif|png|jpg|bmp
        public static bool IsPicture(string fileName)
        {
            string extensionName = fileName.Substring(fileName.LastIndexOf('.') + 1);
            var reg = new Regex("^(gif|png|jpg|bmp)$", RegexOptions.IgnoreCase);
            return reg.IsMatch(extensionName);
        }

        //檔案是否為圖片
        public static Image IsImage(HttpPostedFileBase photofile)
        {
            try
            {
                Image img = Image.FromStream(photofile.InputStream);
                return img;
            }
            catch
            {
                return null;
            }
        }

    }


 

}