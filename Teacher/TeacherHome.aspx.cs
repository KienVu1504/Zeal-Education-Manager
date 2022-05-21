using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ZealEducationManager.Models.CommonFn;

namespace ZealEducationManager.Teacher
{
    public partial class TeacherHome : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["staff"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                StudentCount();
                TeacherCount();
                ClassCount();
                SubjectCount();
            }
        }
        void StudentCount()
        {
            DataTable dt = fn.Fletch("select count(*) from Student");
            Session["student"] = dt.Rows[0][0];
        }

        void TeacherCount()
        {
            DataTable dt = fn.Fletch("select count(*) from Teacher");
            Session["teacher"] = dt.Rows[0][0];
        }

        void ClassCount()
        {
            DataTable dt = fn.Fletch("select count(*) from Class");
            Session["class"] = dt.Rows[0][0];
        }

        void SubjectCount()
        {
            DataTable dt = fn.Fletch("select count(*) from Subject");
            Session["subject"] = dt.Rows[0][0];
        }
    }
}