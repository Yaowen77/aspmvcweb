using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class GlobalFunction
    {
        public static string GlobalConnString = ConfigurationManager.ConnectionStrings["mysqlconnection"].ConnectionString;                    //資料庫連接字串
    }
}