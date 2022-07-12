using DevExpress.Web.ASPxEditors;
using BiSinavProject.PL.Class.Enum;
using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace BiSinavProject.PL.Class.Business
{
    public class BankaBs
    {
        public static BankaGaranti GarantiAyarGetir()
        {
            using (var service = new DBEntities())
            {
                BankaGaranti bankaGaranti = service.BankaGarantis.SingleOrDefault();

                return bankaGaranti;
            }
        }
    }
}