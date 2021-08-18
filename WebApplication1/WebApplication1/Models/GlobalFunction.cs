using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebApplication1.Models
{
    public class GlobalFunction
    {
        public static string GlobalConnString = WebConfigurationManager.ConnectionStrings["mysqlconnection"].ConnectionString;                    //資料庫連接字串
    }
}