using System;
using System.Data;
using System.Web.UI.WebControls;
using static ZealEducationManager.Models.CommonFn;

namespace ZealEducationManager.Teacher
{
    public partial class StudentAttendance : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["staff"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                GetClass();
                btnMarkAttendance.Visible = false;
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString();
        }

        private void GetClass()
        {
            DataTable dt = fn.Fletch("Select * from Class");
            ddlClass.DataSource = dt;
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "ClassId";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, "Select class");
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            string classId = ddlClass.SelectedValue;
            if (classId == "Select class")
            {
                ddlClass.SelectedIndex = 0;
            }
            else
            {
                DataTable dt = fn.Fletch("Select * from Subject where ClassId = '" + classId + "'");
                ddlSubject.DataSource = dt;
                ddlSubject.DataTextField = "SubjectName";
                ddlSubject.DataValueField = "SubjectId";
                ddlSubject.DataBind();
                ddlSubject.Items.Insert(0, "Select subject");
                btnMarkAttendance.Visible = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlClass.SelectedValue == "Select class" || ddlClass.SelectedValue == null || ddlClass.SelectedValue == "")
            {
                lblMsg.Text = " Please select class!";
                lblMsg.CssClass = "alert alert-warning";
            }
            else
            {
                if (ddlSubject.SelectedValue == "Select subject" || ddlSubject.SelectedValue == null || ddlSubject.SelectedValue == "")
                {
                    lblMsg.Text = " Please select subject!";
                    lblMsg.CssClass = "alert alert-warning";
                }
                else
                {
                    DataTable dt = fn.Fletch(@"select StudentId, RollNo, Name, Mobile from Student where ClassId = '" + ddlClass.SelectedValue + "'");
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                        btnMarkAttendance.Visible = true;
                    }
                    else
                    {
                        btnMarkAttendance.Visible = false;
                    }
                }
            }
        }

        protected void btnMarkAttendance_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                string rollNo = row.Cells[2].Text.Trim();
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
                DataTable dataCheck = fn.Fletch("select * from StudentAttendance where ClassId = '" + ddlClass.SelectedValue + "' and SubjectId = '" + ddlSubject.SelectedValue + "' and RollNo = '" + rollNo + "' " +
                                                "and Date = CAST(GETDATE() as date)");
                if (dataCheck.Rows.Count != 0)
                {
                    lblMsg.Text = "You've already taken today's attendance!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    fn.Query("insert into StudentAttendance values ('" + ddlClass.SelectedValue + "', '" + ddlSubject.SelectedValue + "', '" + rollNo + "', '" + status + "', '" + DateTime.Now.ToString("yyyy/MM/dd") + "')");
                    lblMsg.Text = " Inserted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                }
            }
        }
    }
}