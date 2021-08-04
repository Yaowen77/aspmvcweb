using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace WebApplication1.Models.Login
{
    public class apis
    {

        #region 呼叫API
        /// <summary>
        /// 呼叫API
        /// </summary>
        /// <typeparam name="T">回傳之型別</typeparam>
        /// <typeparam name="T1">傳入之型別</typeparam>
        /// <param name="t1">傳入之型別</param>
        /// <param name="strParaUri">API URI</param>
        /// <returns>API回傳訊息</returns>
        public static T CallAPI <T,T1>(T1 t1, string strParaUri)
        {
            //Login
            T RetuenValue = default(T);
           // Result RetuenValue = new Result();
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("http://localhost:999");
                HttpResponseMessage response = null;
                // 將 data 轉為 json
                string json = JsonConvert.SerializeObject(t1);
                // 將轉為 string 的 json 依編碼並指定 content type 存為 httpcontent
                HttpContent contentPost = new StringContent(json, Encoding.UTF8, "application/json");
                response = httpClient.PostAsync(strParaUri, contentPost).Result;
                if (response.IsSuccessStatusCode)
                {
                    //RetuenValue = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result);
                    RetuenValue = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
               // PubFunc.GenErrorLog("(GetAuthorized_Code) " + ex.ToString(), null);
            }
            return RetuenValue;
        }
        #endregion
    }
}