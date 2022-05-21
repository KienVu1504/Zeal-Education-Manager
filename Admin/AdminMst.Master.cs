using System;

namespace ZealEducationManager.Admin
{
    public partial class AdminMst : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("../Login.aspx");
        }
    }
}