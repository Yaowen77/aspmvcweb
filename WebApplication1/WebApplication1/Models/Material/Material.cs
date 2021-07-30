using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Material
{
    public class Material
    {

        [Display(Name = "門市編號")]
        [StringLength(5, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 1)]
        public string StoreID { get; set; }

        [Display(Name = "類別一")]
        [StringLength(4, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 1)]
        public string Category01 { get; set; }

        [Display(Name = "類別二")]
        [StringLength(4, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 1)]
        public string Category02 { get; set; }

        [Required]
        [Display(Name = "商品編號")]
        [StringLength(30, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 1)]
        public string MaterialID { get; set; }

        [Display(Name = "商品名稱")]
        [StringLength(50, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 1)]
        public string MaterialNmae { get; set; }

        [Display(Name = "零售價")]
        public decimal SalePrice { get; set; }


        /* [Display(Name = "主供應商")]
         [StringLength(10, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 1)]
         public string SupplierID1 { get; set; }*/

        public Material()
        {

        }

        public List<Models.Material.Material> Get_StoreMaterial()
        {

            List<Models.Material.Material> result = new List<Models.Material.Material>();
            string connectionString = GlobalFunction.GlobalConnString;

            using (var conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT Category,Category2,StoreID,MaterialID,MaterialName,SalePrice  FROM StoreMaterial Where Aflag = 0";


                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new Models.Material.Material()
                                {

                                    StoreID = (reader.IsDBNull(reader.GetOrdinal("StoreID"))) ? "" : (string)reader["StoreID"],
                                    Category01 = (reader.IsDBNull(reader.GetOrdinal("Category"))) ? "" : (string)reader["Category"],
                                    Category02 = (reader.IsDBNull(reader.GetOrdinal("Category2"))) ? "" : (string)reader["Category2"],
                                    MaterialID = (string)reader["MaterialID"],
                                    MaterialNmae = (reader.IsDBNull(reader.GetOrdinal("MaterialName"))) ? "" : (string)reader["MaterialName"],
                                    SalePrice = (reader.IsDBNull(reader.GetOrdinal("SalePrice"))) ? 0 : (decimal)reader["SalePrice"]


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



        public List<Models.Material.Material> Get_StoreMaterial(string materialId)
        {

            List<Models.Material.Material> result = new List<Models.Material.Material>();
            string connectionString = GlobalFunction.GlobalConnString;

            using (var conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT Category,Category2,StoreID,MaterialID,MaterialName,SalePrice  FROM StoreMaterial Where Aflag = 0  And  MaterialID = @MaterialID";
                    command.Parameters.AddWithValue("@MaterialID", materialId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new Models.Material.Material()
                                {

                                    StoreID = (reader.IsDBNull(reader.GetOrdinal("StoreID"))) ? "" : (string)reader["StoreID"],
                                    Category01 = (reader.IsDBNull(reader.GetOrdinal("Category"))) ? "" : (string)reader["Category"],
                                    Category02 = (reader.IsDBNull(reader.GetOrdinal("Category2"))) ? "" : (string)reader["Category2"],
                                    MaterialID = (string)reader["MaterialID"],
                                    MaterialNmae = (reader.IsDBNull(reader.GetOrdinal("MaterialName"))) ? "" : (string)reader["MaterialName"],
                                    SalePrice = (reader.IsDBNull(reader.GetOrdinal("SalePrice"))) ? 0 : (decimal)reader["SalePrice"]

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






        public List<Models.Material.Material> Get_Material(string materialId)
        {
            List<Models.Material.Material> result = new List<Models.Material.Material>();

            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {

                    command.CommandText = "SELECT Category,Category2,MaterialID,MaterialName,SalePrice  FROM Material Where Aflag = 0 And  MaterialID = @MaterialID";
                    command.Parameters.AddWithValue("@MaterialID", materialId);


                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                result.Add(new Models.Material.Material()
                                {
                                    StoreID = "",
                                    Category01 = (reader.IsDBNull(reader.GetOrdinal("Category"))) ? "" : (string)reader["Category"],
                                    Category02 = (reader.IsDBNull(reader.GetOrdinal("Category2"))) ? "" : (string)reader["Category2"],
                                    MaterialID = (string)reader["MaterialID"],
                                    MaterialNmae = (reader.IsDBNull(reader.GetOrdinal("MaterialName"))) ? "" : (string)reader["MaterialName"],
                                    SalePrice = (reader.IsDBNull(reader.GetOrdinal("SalePrice"))) ? 0 : (decimal)reader["SalePrice"]

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





        public List<Models.Material.Material> Get_Material()
        {

            List<Models.Material.Material> result = new List<Models.Material.Material>();
            string connectionString = GlobalFunction.GlobalConnString;

            using (var conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT Category,Category2,MaterialID,MaterialName,SalePrice  FROM Material Where Aflag = 0";


                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new Models.Material.Material()
                                {
                                    StoreID =   "",
                                    Category01 = (reader.IsDBNull(reader.GetOrdinal("Category"))) ? "" : (string)reader["Category"],
                                    Category02 = (reader.IsDBNull(reader.GetOrdinal("Category2"))) ? "" : (string)reader["Category2"],
                                    MaterialID = (string)reader["MaterialID"],
                                    MaterialNmae = (reader.IsDBNull(reader.GetOrdinal("MaterialName"))) ? "" : (string)reader["MaterialName"],
                                    SalePrice = (reader.IsDBNull(reader.GetOrdinal("SalePrice"))) ? 0 : (decimal)reader["SalePrice"]
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

        public bool Delete_Material(int type, string materialId, string storeId)
        {
            var result = false;
            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    if(type == 0)
                    {
                        command.CommandText = "Update Material Set AFlag = -1 Where MaterialID = @MaterialID";
                        command.Parameters.AddWithValue("@MaterialID", materialId);
                        command.ExecuteNonQuery();


                        command.CommandText = "Update StoreMaterial Set AFlag = -1 Where MaterialID = @MaterialID2";
                        command.Parameters.AddWithValue("@MaterialID2", materialId);
                        command.ExecuteNonQuery();
                    }
                    else {
                       command.CommandText = "Update StoreMaterial Set AFlag = -1 Where StoreID = @StoreID And MaterialID = @MaterialID";
                       command.Parameters.AddWithValue("@StoreID", storeId);
                       command.Parameters.AddWithValue("@MaterialID", materialId);
                       command.ExecuteNonQuery();
                    }
                     
                    result = true;
                    return result;
                }
            }
        }

    }

}