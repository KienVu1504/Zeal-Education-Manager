using System;
using System.Data;
using static ZealEducationManager.Models.CommonFn;

namespace ZealEducationManager
{
    public partial class Login : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = inputEmail.Value.Trim();
            string password = inputPassword.Value.Trim();
            DataTable dt = fn.Fletch("select * from Teacher where Email = '" + email + "' and password = '" + password + "'");
            if (email == "admin.123@gmail.com" && password == "57ce757a5e1ed1ba18f88340d2d42192")
            {
                Session["admin"] = email;
                Response.Redirect("Admin/AdminHome.aspx");
            }
            else if (dt.Rows.Count > 0)
            {
                Session["staff"] = email;
                Response.Redirect("Teacher/TeacherHome.aspx");
            }
            else
            {
                lblMsg.Text = "Login Failed! <br> Check your email & password";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}