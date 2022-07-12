using BiSinavProject.PL.Data;
using System;
using System.Web;
using System.Web.Security;

namespace BiSinavProject.PL
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        private void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            using (var entity = new DBEntities())
            {
                Exception ex = Server.GetLastError();

                DateTime Tarih = DateTime.Now;
                string Message = ex.Message;
                string Source = ex.Source;
                string StackTrace = ex.StackTrace;
                string ExceptionType = ex.GetType().FullName;

                Guid? userId = null;
                var kullaniciData = Membership.GetUser(true);
                if (kullaniciData == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    userId = Guid.Parse(kullaniciData.ProviderUserKey.ToString());
                }

                string Url = "";
                if (HttpContext.Current != null)
                {
                    Url = HttpContext.Current.Request.Url.AbsoluteUri;
                }

                var programLog = new Log
                {
                    Tarih = Tarih,
                    Message = Message,
                    Source = Source,
                    StackTrace = StackTrace,
                    ExceptionType = ExceptionType,
                    UserId = userId,
                    Url = Url,
                };
                entity.Logs.Add(programLog);
                entity.SaveChanges();
            }

            Server.ClearError();
            Response.Clear();
            Response.Redirect("~/Hata.aspx");
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}