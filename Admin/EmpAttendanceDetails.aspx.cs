using System;
using System.Data;
using static ZealEducationManager.Models.CommonFn;

namespace ZealEducationManager.Admin
{
    public partial class EmpAttendanceDetails : System.Web.UI.Page
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
            ddlTeacher.Items.Insert(0, "Select teacher");
        }

        protected void btnCheckAttendance_Click(object sender, EventArgs e)
        {
            DateTime date = Convert.ToDateTime(txtMonth.Text);
            if (ddlTeacher.SelectedValue == "Select teacher")
            {
                lblMsg.Text = "Please select teacher!";
                lblMsg.CssClass = "alert alert-danger";
            }
            else
            {
                DataTable dt = fn.Fletch(@"select ROW_NUMBER() over(order by (select 1)) as [Sr.No], t.Name, ta.Status, ta.Date from TeacherAttendance ta inner join Teacher t on t.TeacherId = ta.TeacherId 
                                    where DATEPART(YY, Date) = '" + date.Year + "' and DATEPART(M, Date) = '" + date.Month + "' and ta.TeacherId = '" + ddlTeacher.SelectedValue + "'");
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        } 
    }
}