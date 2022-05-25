using System;
using System.Data;
using System.Web.UI.WebControls;
using static ZealEducationManager.Models.CommonFn;

namespace ZealEducationManager.Admin
{
    public partial class EmployeeAttendance : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                Attendance();
            }
        }

        private void Attendance()
        {
            DataTable dt = fn.Fletch("select TeacherId, Name, Mobile, Email from Teacher");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnMarkAttendance_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                int teacherId = Convert.ToInt32(row.Cells[1].Text);
                RadioButton rb1 = (row.Cells[0].FindControl("RadioButton1") as RadioButton);
                RadioButton rb2 = (row.Cells[0].FindControl("RadioButton2") as RadioButton);
                int status = 0;
                if (rb1.Checked)
                {
                    status = 1;
                }
                else if (rb2.Checked)
                {
                    status = 0;
                }
                DataTable dataCheck = fn.Fletch("select * from TeacherAttendance where TeacherId = '" + teacherId + "' and Date = CAST(GETDATE() as date);");
                if (dataCheck.Rows.Count != 0)
                {
                    lblMsg.Text = "You've already taken today's attendance!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    fn.Query("insert into TeacherAttendance values ('" + teacherId + "', '" + status + "', '" + DateTime.Now.ToString("yyyy/MM/dd") + "')");
                    lblMsg.Text = " Inserted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                }
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString();
        }
    }
}