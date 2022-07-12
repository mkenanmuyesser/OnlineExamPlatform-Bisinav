using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

/// <summary>
/// Summary description for GenelIslemler
/// </summary>
public static class GenisletmeMetotlari
{
    public static string ToURL(this string s)
    {
        //s = oncul + id + "-" + s;
        if (string.IsNullOrEmpty(s)) return "";
        if (s.Length > 80)
            s = s.Substring(0, 80);
        s = s.Replace("þ", "s");
        s = s.Replace("Þ", "S");
        s = s.Replace("ð", "g");
        s = s.Replace("Ð", "G");
        s = s.Replace("Ý", "I");
        s = s.Replace("ý", "i");
        s = s.Replace("ç", "c");
        s = s.Replace("Ç", "C");
        s = s.Replace("ö", "o");
        s = s.Replace("Ö", "O");
        s = s.Replace("ü", "u");
        s = s.Replace("Ü", "U");
        s = s.Replace("'", "");
        s = s.Replace("\"", "");
        Regex r = new Regex("[^a-zA-Z0-9_-]");
        //if (r.IsMatch(s))
        s = r.Replace(s, "-");
        if (!string.IsNullOrEmpty(s))
            while (s.IndexOf("--") > -1)
                s = s.Replace("--", "-");
        if (s.StartsWith("-")) s = s.Substring(1);
        if (s.EndsWith("-")) s = s.Substring(0, s.Length - 1);
        return s;
    }

    public static bool IsInteger(this object sayi)
    {
        try
        {
            if (sayi == null) throw new Exception();
            Convert.ToInt32(sayi);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static int ToInt32(this object sayi)
    {
        try
        {
            if (sayi == null) throw new Exception();
            int x = Convert.ToInt32(sayi);
            return x;
        }
        catch (Exception)
        {
            return 0;
        }
    }

}
