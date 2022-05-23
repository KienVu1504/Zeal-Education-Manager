using System;
using System.Data;
using System.Web.UI.WebControls;
using static ZealEducationManager.Models.CommonFn;

namespace ZealEducationManager.Admin
{
    public partial class AddClass : System.Web.UI.Page
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
            }
        }

        private void GetClass()
        {
            DataTable dt = fn.Fletch("Select Row_NUMBER() over(Order by (Select 1)) as [Sr.No], ClassId, ClassName from Class");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = fn.Fletch("Select * from Class where ClassName = '" + txtClass.Text.Trim() + "'");
                if (txtClass.Text.Trim() == "" || txtClass.Text.Trim() == null)
                {
                    lblMsg.Text = "Please enter a valid class name!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    if (dt.Rows.Count == 0)
                    {
                        string query = "Insert into Class values('" + txtClass.Text.Trim() + "')";
                        fn.Query(query);
                        lblMsg.Text = "Inserted successfully!";
                        lblMsg.CssClass = "alert alert-success";
                        txtClass.Text = string.Empty;
                        GetClass();
                    }
                    else
                    {
                        lblMsg.Text = "Entered class already exists!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message + "!";
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetClass();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetClass();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.PageIndex = e.NewEditIndex;
            GetClass();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int cId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string ClassName = (row.FindControl("txtClassEdit") as TextBox).Text;
                string ClassNameEdit = (row.FindControl("txtClassEdit") as TextBox).Text.Trim();
                DataTable dtCheck = fn.Fletch("Select * from Class where ClassName = '" + ClassNameEdit + "'");
                if (ClassName.Trim() == "")
                {
                    lblMsg.Text = "Please enter a valid class name!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else if (ClassName.Trim() == null)
                {
                    lblMsg.Text = "Please enter a valid class name!";
                    lblMsg.CssClass = "alert alert-danger";
                } 
                else if (dtCheck.Rows.Count != 0)
                {
                    lblMsg.Text = "Entered class already exists!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    fn.Query("Update Class set ClassName = '" + ClassName + "' where ClassId = '" + cId + "'");
                    lblMsg.Text = "Class updated successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    GridView1.EditIndex = -1;
                    GetClass();
                }
            } catch(Exception ex)
            {
                lblMsg.Text = ex.Message + "!";
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int classId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                DataTable examCheck = fn.Fletch("select * from Exam where ClassId = '" + classId + "'");
                DataTable expenseCheck = fn.Fletch("select * from Expense where ClassId = '" + classId + "'");
                DataTable feesCheck = fn.Fletch("select * from Fees where ClassId = '" + classId + "'");
                DataTable studentCheck = fn.Fletch("select * from Student where ClassId = '" + classId + "'");
                DataTable studentAttendanceCheck = fn.Fletch("select * from StudentAttendance where ClassId = '" + classId + "'");
                DataTable subjectCheck = fn.Fletch("select * from Subject where ClassId = '" + classId + "'");
                DataTable teacherSubjectCheck = fn.Fletch("select * from Expense where ClassId = '" + classId + "'");
                if (examCheck.Rows.Count != 0 || expenseCheck.Rows.Count != 0 || feesCheck.Rows.Count != 0 || studentCheck.Rows.Count != 0 || studentAttendanceCheck.Rows.Count != 0 || subjectCheck.Rows.Count != 0
                    || expenseCheck.Rows.Count != 0)
                {
                    lblMsg.Text = "You can only delete classes that contain no data!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    fn.Query("Delete from Class where ClassId = '" + classId + "'");
                    lblMsg.Text = "Class deleted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    GridView1.EditIndex = -1;
                    GetClass();
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