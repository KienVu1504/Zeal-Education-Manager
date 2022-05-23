using System;
using System.Data;
using System.Web.UI.WebControls;
using static ZealEducationManager.Models.CommonFn;

namespace ZealEducationManager.Admin
{
    public partial class Subject : System.Web.UI.Page
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
                GetClass();
                GetSubject();
            }
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
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string classVal = ddlClass.SelectedItem.Text;
                DataTable dt = fn.Fletch("Select * from Subject where ClassId = '" + ddlClass.SelectedItem.Value + "' and SubjectName = '" + txtSubject.Text.Trim() + "'");
                if (dt.Rows.Count == 0)
                {
                    if (txtSubject.Text.Trim() == null || txtSubject.Text.Trim() == "")
                    {
                        lblMsg.Text = "Please enter a valid subject name!";
                        lblMsg.CssClass = "alert alert-danger";
                    } 
                    else
                    {
                        string query = "Insert into Subject values('" + ddlClass.SelectedItem.Value + "','" + txtSubject.Text.Trim() + "')";
                        fn.Query(query);
                        lblMsg.Text = "Inserted successfully!";
                        lblMsg.CssClass = "alert alert-success";
                        ddlClass.SelectedIndex = 0;
                        txtSubject.Text = string.Empty;
                        GetSubject();
                    }
                }
                else
                {
                    lblMsg.Text = "Entered subject already exists for <b>'" + classVal + "'</b>!";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message + "!";
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        private void GetSubject()
        {
            DataTable dt = fn.Fletch(@"Select Row_NUMBER() over(Order by (Select 1)) as [Sr.No], s.SubjectId, c.ClassId, c.ClassName, s.SubjectName from Subject s inner join Class c on c.ClassId = s.ClassId");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetSubject();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetSubject();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetSubject();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int subjId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string classId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("DropDownlist1")).SelectedValue;
                string ddlClass = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("DropDownlist1")).SelectedItem.Text;
                string subjName = (row.FindControl("TextBox1") as TextBox).Text.Trim();
                DataTable classCheck = fn.Fletch("select  * from Subject where ClassId = '" + classId + "' and SubjectName = '" + subjName + "'");
                if (classCheck.Rows.Count != 0)
                {
                    lblMsg.Text = "Entered subject already exists for <b>'" + ddlClass + "'</b>!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    if (subjName == null || subjName == "")
                    {
                        lblMsg.Text = "Please enter a valid subject name!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                    else
                    {
                        fn.Query("Update Subject set ClassId = '" + classId + "', SubjectName = '" + subjName + "' where SubjectId = '" + subjId + "'");
                        lblMsg.Text = "Subject updated successfully!";
                        lblMsg.CssClass = "alert alert-success";
                        GridView1.EditIndex = -1;
                        GetSubject();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message + "!";
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int subsId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                DataTable examCheck = fn.Fletch("select * from Exam where SubjectId = '" + subsId + "'");
                DataTable teacherSubjectCheck = fn.Fletch("select * from TeacherSubject where SubjectId = '" + subsId + "'");
                DataTable expenseCheck = fn.Fletch("select * from Expense where SubjectId = '" + subsId + "'");
                DataTable studentAttendanceCheck = fn.Fletch("select * from StudentAttendance where SubjectId = '" + subsId + "'");
                if (examCheck.Rows.Count != 0 || teacherSubjectCheck.Rows.Count != 0 || expenseCheck.Rows.Count != 0 || studentAttendanceCheck.Rows.Count != 0)
                {
                    lblMsg.Text = "You can only delete subject that contain no data!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    fn.Query("Delete from Subject where SubjectId = '" + subsId + "'");
                    lblMsg.Text = "Subject deleted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    GridView1.EditIndex = -1;
                    GetSubject();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message + "!";
                lblMsg.CssClass = "alert alert-danger";
            }
        }
    }
}