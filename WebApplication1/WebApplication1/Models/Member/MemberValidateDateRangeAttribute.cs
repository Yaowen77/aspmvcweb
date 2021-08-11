using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Member.Validation
{
    public class MemberValidateDateRangeAttribute : ValidationAttribute
    {
        public string MyStartDate { get; set; }
        public string MyEndDate { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
   
            DateTime dt = (DateTime)value;

            if (value != null && dt >= Convert.ToDateTime(MyStartDate) && dt <= Convert.ToDateTime(MyEndDate))
            {
                return ValidationResult.Success;   // 驗證成功
            }
            else
            {   // 第一種作法，驗證失敗會出現這一句錯誤訊息。
                //return new ValidationResult("[自訂驗證 的 錯誤訊息] 抱歉～日期區間，不符合或超出範圍");

                //第二種作法，這裡使用空字串（""）。驗證失敗就會使用 UserTable.cs裡面的 [ ErrorMessage=""] 錯誤訊息
                return new ValidationResult("");
            }
             
        }
    }
}