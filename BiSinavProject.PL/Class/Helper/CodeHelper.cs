using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiSinavProject.PL.Class.Helper
{
    public static class CodeHelper
    {
        public static string GenerateNewCode(Random random,int size)
        {           
            char[] cr = "0123456789abcdefghijklmnopqrstuvwxyz".ToCharArray();
            string result = string.Empty;            
            for (int i = 0; i < size; i++)
            {
                result += cr[random.Next(0, cr.Length - 1)].ToString();
            }
            return result;
        }
    }
}