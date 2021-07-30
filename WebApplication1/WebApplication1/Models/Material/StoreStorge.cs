using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Material
{
    public class StoreStorge
    {
        [Display(Name = "門市編號")]
        [StringLength(5, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 1)]
        public string StoreID { get; set; }

        [Display(Name = "庫存")]
        public decimal Storage { get; set; }

        public StoreStorge()
        {

        }

        public List<Models.Material.StoreStorge> Get_Storage(string materialID)
        {

            List<Models.Material.StoreStorge> result = new List<Models.Material.StoreStorge>();
            string connectionString = GlobalFunction.GlobalConnString;

            using (var conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT StoreID,  Storage FROM StoreMaterial WHERE MaterialID = @MaterialID";
                    command.Parameters.AddWithValue("@MaterialID", materialID);


                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new Models.Material.StoreStorge()
                                {

                                    StoreID = (reader.IsDBNull(reader.GetOrdinal("StoreID"))) ? "" : (string)reader["StoreID"],
                                    Storage = (reader.IsDBNull(reader.GetOrdinal("Storage"))) ? 0 : (decimal)reader["Storage"]

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