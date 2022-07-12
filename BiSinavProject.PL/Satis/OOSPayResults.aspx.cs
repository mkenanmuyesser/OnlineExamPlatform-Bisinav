using System.Collections;
using System.Web.UI;

namespace BiSinavProject.PL.Satis
{
    public partial class OOSPayResults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                IEnumerator f = Request.Form.GetEnumerator();
                while (f.MoveNext())
                {
                    string xkey = (string)f.Current;
                    string xval = Request.Form.Get(xkey);
                    txtResults.Text = txtResults.Text + (xkey + " : " + xval);
                }
            }
        }
    }
}