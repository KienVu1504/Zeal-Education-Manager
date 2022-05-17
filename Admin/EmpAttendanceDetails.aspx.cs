using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ZealEducationManager.Models.CommonFn;

namespace ZealEducationManager.Admin
{
    public partial class EmpAttendanceDetails : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTeacher();
            }
        }

        private void GetTeacher()
        {
            DataTable dt = fn.Fletch("Select * from Teacher");
            ddlTeacher.DataSource = dt;
            ddlTeacher.DataTextField = "Name";
            ddlTeacher.DataValueField = "TeacherId";
            ddlTeacher.DataBind();
            ddlTeacher.Items.Insert(0, "Select Teacher");
        }

        protected void btnCheckAttendance_Click(object sender, EventArgs e)
        {
            DateTime date = Convert.ToDateTime(txtMonth.Text);
            DataTable dt = fn.Fletch(@"select ROW_NUMBER() over(order by (select 1)) as [Sr.No], t.Name, ta.Status, ta.Date from TeacherAttendance ta inner join Teacher t on t.TeacherId = ta.TeacherId 
                                    where DATEPART(YY, Date) = '" + date.Year + "' and DATEPART(M, Date) = '" + date.Month + "' and ta.TeacherId = '" + ddlTeacher.SelectedValue + "'");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        } 
    }
}