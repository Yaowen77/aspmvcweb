using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NKLibrary
{
    public class NKDLL
    {
        public bool LoginCheck()
        {
            if (HttpContext.Current.Session["UserID"] != null )
            {   
                return true;
            }
            else
            {   
                return false;
            }
        }

        public static string Encoded(string Code, int Length = 32)
        {
            string temp = "", PassWD = "", newcode1 = "", newcode2 = "", ReturnCode = "";
            for (int i = 1; i < Code.Length; i++)
            {
                if ((int)Code[i - 1] > 60)
                {
                    if (i % 2 == 0)
                        temp = Convert.ToString((((int)Code[i - 1] - 55) + 16) % 29 * 26);
                    else
                        temp = Convert.ToString((((int)Code[i - 1] - 55) + 9) % 26 * 29);
                }
                else
                {
                    if (i % 2 == 0)
                        temp = Convert.ToString((((int)Code[i - 1] - 48) + 7) % 17 * 9);
                    else
                        temp = Convert.ToString((((int)Code[i - 1] - 48) + 29) % 13 * 16);
                }
                PassWD += temp;
            }
            for (int i = 1; i < PassWD.Length; i++)
            {
                if (i % 2 == 0)
                    newcode1 += PassWD[i - 1];
                else
                    newcode2 += PassWD[i - 1];
            }
            PassWD = newcode1 + newcode2;

            if (PassWD != "")
            {
                while (PassWD.Length < 64)
                {
                    PassWD += PassWD;
                }
            }

            bool Out;
            for (int i = 1; i < PassWD.Length - 1; i = i + 2)
            {

                Out = int.TryParse(PassWD.Substring(i - 1, 2), out int CheckInt);
                if (!Out)
                    CheckInt = 0;

                CheckInt = CheckInt * i;
                if (CheckInt % 36 > 9)
                    CheckInt = CheckInt % 36 + 55;
                else
                    CheckInt = CheckInt % 36 + 48;
                ReturnCode += (char)CheckInt;
            }
            return ReturnCode.Substring(0, Length);
        }
    }
}
