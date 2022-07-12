using System.Web;
using BiSinavProject.PL.Class.CustomType;

namespace BiSinavProject.PL.Class.Helper
{
    public static class SessionHelper
    {

        public static BesteBesType BesteBesData
        {
            get
            {
                var besteBesData = HttpContext.Current.Session["BesteBesData"];
                if (besteBesData != null)
                    return (BesteBesType)besteBesData;

                return null;
            }
            set
            {
                HttpContext.Current.Session["BesteBesData"] = value;
            }
        }       
     
        public static DenemeType DenemeData
        {
            get
            {
                var denemeData = HttpContext.Current.Session["DenemeData"];
                if (denemeData != null)
                    return (DenemeType)denemeData;

                return null;
            }
            set
            {
                HttpContext.Current.Session["DenemeData"] = value;
            }
        }
    }
}