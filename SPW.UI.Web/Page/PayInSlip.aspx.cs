using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPW.UI.Web.Page
{
    public partial class PayInSlip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //InitialPage();
            }
            else
            {
                //ReloadPageEngine();
            }
            lblDateTime.Text = DateTime.Now.ToShortDateString();
        }
    }
}