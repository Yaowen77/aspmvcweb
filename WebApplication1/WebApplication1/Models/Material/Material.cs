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

        [Display(Name = "主供應商")]
        [StringLength(10, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 1)]
        public string SupplierID1 { get; set; }


        /* [Display(Name = "主供應商")]
         [StringLength(10, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 1)]
         public string SupplierID1 { get; set; }*/

        public Material()
        {

        }

        public List<Models.Material.Category> Get_Categpoy()
        {
            List<Models.Material.Category> result = new List<Models.Material.Category>();
            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT Category,CONCAT(Category,' ',Category_Name) As Category_Name  FROM Category Where CateType = 0  ";
 

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                result.Add(new Models.Material.Category()
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

        public List<Models.Material.Category> Get_Categpoy2()
        {
            List<Models.Material.Category> result = new List<Models.Material.Category>();
            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT Category,CONCAT(Category,' ',Category_Name) As Category_Name  FROM Category Where CateType = 1  ";


                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            result.Add(new Models.Material.Category()
                            {
                                CategoryID = "",
                                CategoryName = ""
                            });

                            while (reader.Read())
                            {
                                result.Add(new Models.Material.Category()
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


        public List<Models.Material.Supplier> Get_Supplier()
        {
            List<Models.Material.Supplier> result = new List<Models.Material.Supplier>();
            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT SupplierID,CONCAT(SupplierID,' ',FullName) As FullName  FROM Supplier Where AFlag = 0  ";


                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            result.Add(new Models.Material.Supplier()
                            {
                                SupplierID = "",
                                FullName = ""
                            });

                            while (reader.Read())
                            {
                                result.Add(new Models.Material.Supplier()
                                {

                                    SupplierID = (string)reader["SupplierID"],
                                    FullName = (reader.IsDBNull(reader.GetOrdinal("FullName"))) ? "" : (string)reader["FullName"]

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


        

        public List<Models.Material.Material> Get_StoreMaterial()
        {

            List<Models.Material.Material> result = new List<Models.Material.Material>();
            string connectionString = GlobalFunction.GlobalConnString;

            using (var conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT Category,Category2,StoreID,MaterialID,MaterialName,SalePrice,SupplierID1  FROM StoreMaterial Where Aflag = 0";


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
                                    SalePrice = (reader.IsDBNull(reader.GetOrdinal("SalePrice"))) ? 0 : (decimal)reader["SalePrice"],
                                    SupplierID1 = (reader.IsDBNull(reader.GetOrdinal("SupplierID1"))) ? "" : (string)reader["SupplierID1"]

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
                    command.CommandText = "SELECT Category,Category2,StoreID,MaterialID,MaterialName,SalePrice,SupplierID1  FROM StoreMaterial Where Aflag = 0  And  MaterialID = @MaterialID";
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
                                    SalePrice = (reader.IsDBNull(reader.GetOrdinal("SalePrice"))) ? 0 : (decimal)reader["SalePrice"],
                                    SupplierID1 = (reader.IsDBNull(reader.GetOrdinal("SupplierID1"))) ? "" : (string)reader["SupplierID1"]

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



        public Models.Material.Material Get_Edit_Material(string materialId)
        {
            var result = new Models.Material.Material();

            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT Category,Category2,MaterialID,MaterialName,SalePrice,SupplierID1  FROM Material Where Aflag = 0 And  MaterialID = @MaterialID";
                    command.Parameters.AddWithValue("@MaterialID", materialId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                result = new Models.Material.Material()
                                {
                                    StoreID = "",
                                    Category01 = (reader.IsDBNull(reader.GetOrdinal("Category"))) ? "" : (string)reader["Category"],
                                    Category02 = (reader.IsDBNull(reader.GetOrdinal("Category2"))) ? "" : (string)reader["Category2"],
                                    MaterialID = (string)reader["MaterialID"],
                                    MaterialNmae = (reader.IsDBNull(reader.GetOrdinal("MaterialName"))) ? "" : (string)reader["MaterialName"],
                                    SalePrice = (reader.IsDBNull(reader.GetOrdinal("SalePrice"))) ? 0 : (decimal)reader["SalePrice"],
                                    SupplierID1 = (reader.IsDBNull(reader.GetOrdinal("SupplierID1"))) ? "" : (string)reader["SupplierID1"]
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

        public Models.Material.Material Get_Edit_StoreMaterial(string materialId,string storeId)
        {
            var result = new Models.Material.Material();

            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT Category,Category2,StoreID,MaterialID,MaterialName,SalePrice,SupplierID1  FROM StoreMaterial Where Aflag = 0  And  MaterialID = @MaterialID And StoreID = @StoreID";
                    command.Parameters.AddWithValue("@MaterialID", materialId);
                    command.Parameters.AddWithValue("@StoreID", storeId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                result = new Models.Material.Material()
                                {
                                    StoreID = (reader.IsDBNull(reader.GetOrdinal("StoreID"))) ? "" : (string)reader["StoreID"],
                                    Category01 = (reader.IsDBNull(reader.GetOrdinal("Category"))) ? "" : (string)reader["Category"],
                                    Category02 = (reader.IsDBNull(reader.GetOrdinal("Category2"))) ? "" : (string)reader["Category2"],
                                    MaterialID = (string)reader["MaterialID"],
                                    MaterialNmae = (reader.IsDBNull(reader.GetOrdinal("MaterialName"))) ? "" : (string)reader["MaterialName"],
                                    SalePrice = (reader.IsDBNull(reader.GetOrdinal("SalePrice"))) ? 0 : (decimal)reader["SalePrice"],
                                    SupplierID1 = (reader.IsDBNull(reader.GetOrdinal("SupplierID1"))) ? "" : (string)reader["SupplierID1"]
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


        public List<Models.Material.Material> Get_Material(string materialId)
        {
            List<Models.Material.Material> result = new List<Models.Material.Material>();

            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {

                    command.CommandText = "SELECT Category,Category2,MaterialID,MaterialName,SalePrice,SupplierID1  FROM Material Where Aflag = 0 And  MaterialID = @MaterialID";
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
                                    SalePrice = (reader.IsDBNull(reader.GetOrdinal("SalePrice"))) ? 0 : (decimal)reader["SalePrice"],
                                    SupplierID1 = (reader.IsDBNull(reader.GetOrdinal("SupplierID1"))) ? "" : (string)reader["SupplierID1"]

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
                    command.CommandText = "SELECT Category,Category2,MaterialID,MaterialName,SalePrice,SupplierID1  FROM Material Where Aflag = 0";


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
                                    SalePrice = (reader.IsDBNull(reader.GetOrdinal("SalePrice"))) ? 0 : (decimal)reader["SalePrice"],
                                    SupplierID1 = (reader.IsDBNull(reader.GetOrdinal("SupplierID1"))) ? "" : (string)reader["SupplierID1"]
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

        public bool IsMaterialIdExist(Models.Material.Material material, int type)
        {

            string connectionString = GlobalFunction.GlobalConnString;

            using (var conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    if (type == 0)
                    {
                        command.CommandText = "SELECT MaterialID  FROM Material WHERE MaterialID = @MaterialID ";
                        command.Parameters.AddWithValue("@MaterialID", material.MaterialID);
                    }
                    else
                    {
                        command.CommandText = "SELECT MaterialID  FROM StoreMaterial WHERE MaterialID = @MaterialID And StoreID = @StoreID";
                        command.Parameters.AddWithValue("@MaterialID", material.MaterialID);
                        command.Parameters.AddWithValue("@StoreID", material.StoreID);
                    }


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



        public bool Post_Material(Models.Material.Material material, int type, string InputUserID)
        {
            var result = false;
            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    if (type == 0)
                    {
                        command.CommandText = "Insert Into Material (MaterialID,MaterialName,Category,Category2,SalePrice,SupplierID1,ModifyDate,ModifyEID) VALUES(@MaterialID,@MaterialName,@Category,@Category2,@SalePrice,@SupplierID1,Now(),@InputUser)";
                        command.Parameters.AddWithValue("@MaterialID", material.MaterialID);
                        command.Parameters.AddWithValue("@MaterialName", material.MaterialNmae);
                        command.Parameters.AddWithValue("@Category", material.Category01);
                        command.Parameters.AddWithValue("@Category2", material.Category02);
                        command.Parameters.AddWithValue("@SalePrice", material.SalePrice);
                        command.Parameters.AddWithValue("@SupplierID1", material.SupplierID1);
                        command.Parameters.AddWithValue("@InputUser", InputUserID);
                        command.ExecuteNonQuery();

                        var storeList = new Models.Material.Material().ListStoreQuery();
                        foreach (var st in storeList)
                        {
                            Post_StoreMaterial(st, InputUserID, material);
                        }
                    }
                    else
                    {
                        command.CommandText = "Insert Into StoreMaterial (StoreID,MaterialID,MaterialName,Category,Category2,SalePrice,SupplierID1,ModifyDate,ModifyEID) VALUES(@StoreID2,@MaterialID2,@MaterialName2,@Category02,@Category22,@SalePrice2,@SupplierID12,Now(),@InputUser2)";
                        command.Parameters.AddWithValue("@StoreID2", material.StoreID);
                        command.Parameters.AddWithValue("@MaterialID2", material.MaterialID);
                        command.Parameters.AddWithValue("@MaterialName2", material.MaterialNmae);
                        command.Parameters.AddWithValue("@Category02", material.Category01);
                        command.Parameters.AddWithValue("@Category22", material.Category02);
                        command.Parameters.AddWithValue("@SalePrice2", material.SalePrice);
                        command.Parameters.AddWithValue("@SupplierID12", material.SupplierID1);
                        command.Parameters.AddWithValue("@InputUser2", InputUserID);
                        command.ExecuteNonQuery();
                    }

                    result = true;
                }
            }
            return result;
        }

        public void Post_StoreMaterial(string storeId ,string InputUserID , Models.Material.Material material)
        {

            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                        command.CommandText = "Insert Into StoreMaterial (StoreID,MaterialID,MaterialName,Category,Category2,SalePrice,SupplierID1,ModifyDate,ModifyEID) VALUES(@StoreID2,@MaterialID2,@MaterialName2,@Category02,@Category22,@SalePrice2,@SupplierID12,Now(),@InputUser2)";
                        command.Parameters.AddWithValue("@StoreID2", storeId);
                        command.Parameters.AddWithValue("@MaterialID2", material.MaterialID);
                        command.Parameters.AddWithValue("@MaterialName2", material.MaterialNmae);
                        command.Parameters.AddWithValue("@Category02", material.Category01);
                        command.Parameters.AddWithValue("@Category22", material.Category02);
                        command.Parameters.AddWithValue("@SalePrice2", material.SalePrice);
                        command.Parameters.AddWithValue("@SupplierID12", material.SupplierID1);
                        command.Parameters.AddWithValue("@InputUser2", InputUserID);
                        command.ExecuteNonQuery();          
                }
            }   
        }

            public List<string> ListStoreQuery()
        {   
            string connectionString = GlobalFunction.GlobalConnString;

            using (var conn = new MySqlConnection(connectionString))
            {

                List<string> storeList = new List<string>();

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT StoreID  FROM Store Where Aflag = 1";


                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                storeList.Add((reader.IsDBNull(reader.GetOrdinal("StoreID"))) ? "" : (string)reader["StoreID"]);
                            }
                            return storeList;
                        }
                        else
                        {
                            return storeList;
                        }
                    }
                }
            }

        }


            public bool Patch_Material(Models.Material.Material material, int type, string InputUserID)
        {
            var result = false;
            using (var conn = new MySqlConnection(GlobalFunction.GlobalConnString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                   
                    if(type == 0)
                    {
                        command.CommandText = "Update Material Set Category = @Category,Category2 =@Category2 ,MaterialName =@MaterialName, SalePrice =@SalePrice,SupplierID1 =@SupplierID1 ,ModifyEID =@ModifyEID,ModifyDate = Now() Where MaterialID = @MaterialID ";
                        command.Parameters.AddWithValue("@MaterialID", material.MaterialID);
                        command.Parameters.AddWithValue("@Category", material.Category01);
                        command.Parameters.AddWithValue("@Category2", material.Category02);
                        command.Parameters.AddWithValue("@MaterialName", material.MaterialNmae);
                        command.Parameters.AddWithValue("@SalePrice", material.SalePrice);
                        command.Parameters.AddWithValue("@SupplierID1", material.SupplierID1);
                        command.Parameters.AddWithValue("@ModifyEID", InputUserID);
                        command.ExecuteNonQuery();
                    }
                    else {

                        command.CommandText = "Update StoreMaterial Set SalePrice =@SalePrice,SupplierID1 =@SupplierID1, ModifyEID =@ModifyEID,ModifyDate = Now() Where MaterialID = @MaterialID And StoreID = @StoreID";
                        command.Parameters.AddWithValue("@MaterialID", material.MaterialID);
                        command.Parameters.AddWithValue("@StoreID", material.StoreID);
                        command.Parameters.AddWithValue("@SalePrice", material.SalePrice);
                        command.Parameters.AddWithValue("@SupplierID1", material.SupplierID1);
                        command.Parameters.AddWithValue("@ModifyEID", InputUserID);
                        command.ExecuteNonQuery();

                    }
                    result = true;
                    return result;
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