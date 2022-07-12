using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using BiSinavProject.PL.Class.CustomType;
using System.Web.Security;
using BiSinavProject.PL.Data;

namespace BiSinavProject.PL.Kullanici
{
    public partial class GirisYonlendir : System.Web.UI.Page
    {
        #region Properties

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            //FormsAuthentication.SetAuthCookie("darkbybars", true);
            // Get the Facebook code from the querystring
            if (Request.QueryString["code"] != "" && Request.QueryString["code"] != null)
            {
                var userData = GetFacebookUserData(Request.QueryString["code"]);

                if (userData != null)
                {

                    //kullanıcı varmı kontrolü

                    if (Membership.ValidateUser(userData.email, userData.id))
                    {
                        FormsAuthentication.SetAuthCookie(userData.email, true);
                    }
                    else
                    {                        
                        Membership.CreateUser(userData.email, userData.id, userData.email);
                        FormsAuthentication.SetAuthCookie(userData.email, true);
                    }
                }

                Session["KullaniciBilgi"] = userData.name;
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            Response.Redirect("/Default.aspx", true);
        }

        #endregion

        #region Methods

        protected Class.CustomType.Facebook.User GetFacebookUserData(string code)
        {
            // Exchange the code for an access token
            Uri targetUri = new Uri("https://graph.facebook.com/oauth/access_token?client_id=" +
                ConfigurationManager.AppSettings["FacebookAppId"] +
                "&client_secret=" +
                ConfigurationManager.AppSettings["FacebookAppSecret"] +
                "&redirect_uri=" +
                //"http://localhost:14690" +
                "http://www.bisinav.net"+
                "/Kullanici/GirisYonlendir.aspx&code=" + code);
            HttpWebRequest at = (HttpWebRequest)HttpWebRequest.Create(targetUri);

            System.IO.StreamReader str = new System.IO.StreamReader(at.GetResponse().GetResponseStream());
            string token = str.ReadToEnd().ToString().Replace("access_token=", "");

            // Split the access token and expiration from the single string
            string[] combined = token.Split('&');
            string accessToken = combined[0];

            // Exchange the code for an extended access token
            Uri eatTargetUri = new Uri("https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&fb_exchange_token=" + accessToken);
            HttpWebRequest eat = (HttpWebRequest)HttpWebRequest.Create(eatTargetUri);

            StreamReader eatStr = new StreamReader(eat.GetResponse().GetResponseStream());
            string eatToken = eatStr.ReadToEnd().ToString().Replace("access_token=", "");

            // Split the access token and expiration from the single string
            string[] eatWords = eatToken.Split('&');
            string extendedAccessToken = eatWords[0];

            // Request the Facebook user information
            Uri targetUserUri = new Uri("https://graph.facebook.com/me?fields=email,name,first_name,last_name,gender,locale,link&access_token=" + accessToken);
            HttpWebRequest user = (HttpWebRequest)HttpWebRequest.Create(targetUserUri);

            // Read the returned JSON object response
            StreamReader userInfo = new StreamReader(user.GetResponse().GetResponseStream());
            string jsonResponse = string.Empty;
            jsonResponse = userInfo.ReadToEnd();

            // Deserialize and convert the JSON object to the Facebook.User object type
            JavaScriptSerializer sr = new JavaScriptSerializer();
            string jsondata = jsonResponse;
            Class.CustomType.Facebook.User converted = sr.Deserialize<Class.CustomType.Facebook.User>(jsondata);

            //// Write the user data to a List
            //List<Class.CustomType.Facebook.User> currentUser = new List<Class.CustomType.Facebook.User>();
            //currentUser.Add(converted);


            return converted;
        }

        #endregion
    }
}