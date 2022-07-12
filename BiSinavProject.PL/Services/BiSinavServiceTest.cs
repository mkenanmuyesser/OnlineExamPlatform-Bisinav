using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiSinavProject.PL.Services
{
    public class BiSinavServiceTest
    {
        public void Test()
        {
            BiSinavWcfService client = new BiSinavWcfService();
            string kod = client.SifreUret("j2hbu0i4");
        }
    }
}