using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VietSearch.Utility
{
    public class StringHelper
    {
        //remove space, lower, toASCII
        public static string StandardizeString(String s)
        {
            string result = ConvertToASCII(s);
            return result.Replace(" ", "").ToLower();
        }

        private static string ConvertToASCII(string text)
        {
            string s = text;
            string[] VietnameseSigns = new string[]
            {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "ĐÐ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ" };


            for (int i = 1; i < VietnameseSigns.Length; i++)
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    s = s.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            return s.ToUpper();
        }

        public static string Erase(string strInput, string strErase)
        {
            string strOutput = strInput;
            if (null != strErase && strErase.Length > 0)
            {
                int idx = strErase.IndexOf(strInput);
                if (idx >= 0)
                {
                    strOutput = String.Empty;
                }
                else
                {
                    strOutput = strInput.Replace(strErase, @"");
                    strOutput = strOutput.Trim();
                }
            }

            return strOutput;
        }
        
    }
}
