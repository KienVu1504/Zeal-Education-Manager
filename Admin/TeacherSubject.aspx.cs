using System;
using System.Data;
using System.Web.UI.WebControls;
using static ZealEducationManager.Models.CommonFn;

namespace ZealEducationManager.Admin
{
    public partial class TeacherSubject : System.Web.UI.Page
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
                GetTeacher();
                GetTeacherSubject();
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

        private void GetTeacher()
        {
            DataTable dt = fn.Fletch("Select * from Teacher");
            ddlTeacher.DataSource = dt;
            ddlTeacher.DataTextField = "Name";
            ddlTeacher.DataValueField = "TeacherId";
            ddlTeacher.DataBind();
            ddlTeacher.Items.Insert(0, "Select teacher");
        }

        private void GetTeacherSubject()
        {
            DataTable dt = fn.Fletch(@"select ROW_NUMBER() over(order by (select 1)) as [Sr.No], ts.Id, ts.ClassId, c.ClassName, ts.SubjectId, s.SubjectName, ts.TeacherId, t.Name from TeacherSubject ts inner join Class 
                                    c on ts.ClassId = c.ClassId inner join Subject s on ts.SubjectId = s.SubjectId inner join Teacher t on ts.TeacherId = t.TeacherId");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            string classId = ddlClass.SelectedValue;
            if (classId != "Select class")
            {
                DataTable dt = fn.Fletch("Select * from Subject where ClassId = '" + classId + "'");
                ddlSubject.DataSource = dt;
                ddlSubject.DataTextField = "SubjectName";
                ddlSubject.DataValueField = "SubjectId";
                ddlSubject.DataBind();
                ddlSubject.Items.Insert(0, "Select subject");
            }
            else
            {
                ddlTeacher.SelectedIndex = 0;
                ddlSubject.SelectedIndex = 0;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string classId = ddlClass.SelectedValue;
                string subjectId = ddlSubject.SelectedValue;
                string teacherId = ddlTeacher.SelectedValue;
                if (subjectId == "Select subject" || teacherId == "Select teacher")
                {
                    lblMsg.Text = "Please select teacher and subject!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    DataTable dt = fn.Fletch("Select * from TeacherSubject where ClassId = '" + classId + "' and SubjectId = '" + subjectId + "' and TeacherId = '" + teacherId + "'");
                    if (dt.Rows.Count == 0)
                    {
                        string query = "Insert into TeacherSubject values('" + classId + "','" + subjectId + "','" + teacherId + "')";
                        fn.Query(query);
                        lblMsg.Text = "Inserted successfully!";
                        lblMsg.CssClass = "alert alert-success";
                        ddlClass.SelectedIndex = 0;
                        ddlSubject.SelectedIndex = 0;
                        ddlTeacher.SelectedIndex = 0;
                        GetTeacherSubject();
                    }
                    else
                    {
                        lblMsg.Text = "Entered data already exists!";
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
            GetTeacherSubject();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetTeacherSubject();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int teacherSubjectId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                fn.Query("Delete from TeacherSubject where Id = '" + teacherSubjectId + "'");
                lblMsg.Text = "Teacher subject deleted successfully!";
                lblMsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetTeacherSubject();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message + "!";
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetTeacherSubject();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int teacherSubjectId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string classId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlClassGv")).SelectedValue;
                string subjectId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlSubjectGv")).SelectedValue;
                string teacherId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlTeacherGv")).SelectedValue;
                if (subjectId == "Select subject" || subjectId == "" || subjectId == null)
                {
                    lblMsg.Text = "Please select subject!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    DataTable dataCheck = fn.Fletch("select * from TeacherSubject where ClassId = '" + classId + "' and SubjectId = '" + subjectId + "' and TeacherId = '" + teacherId + "'");
                    if (dataCheck.Rows.Count != 0)
                    {
                        lblMsg.Text = "Entered data already exists!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                    else
                    {
                        fn.Query(@"Update TeacherSubject set ClassId = '" + classId + "', SubjectId = '" + subjectId + "', TeacherId = '" + teacherId + "' where Id = '" + teacherSubjectId + "'");
                        lblMsg.Text = "Record updated successfully!";
                        lblMsg.CssClass = "alert alert-success";
                        GridView1.EditIndex = -1;
                        GetTeacherSubject();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message + "!";
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        protected void ddlClassGv_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlClassSelected = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlClassSelected.NamingContainer;
            if (row != null)
            {
                if ((row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlSubjectGV = (DropDownList)row.FindControl("ddlSubjectGv");
                    DataTable dt = fn.Fletch("select * from Subject where ClassId = '" + ddlClassSelected.SelectedValue + "'");
                    ddlSubjectGV.DataSource = dt;
                    ddlSubjectGV.DataTextField = "SubjectName";
                    ddlSubjectGV.DataValueField = "SubjectId";
                    ddlSubjectGV.DataBind();
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlClass = (DropDownList)e.Row.FindControl("ddlClassGv");
                    DropDownList ddlSubject = (DropDownList)e.Row.FindControl("ddlSubjectGv");
                    DataTable dt = fn.Fletch("select * from Subject where ClassId = '" + ddlClass.SelectedValue + "'");
                    ddlSubject.DataSource = dt;
                    ddlSubject.DataTextField = "SubjectName";
                    ddlSubject.DataValueField = "SubjectId";
                    ddlSubject.DataBind();
                    ddlSubject.Items.Insert(0, "Select subject");
                    string teacherSubjectId = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                    DataTable dataTable = fn.Fletch(@"select ts.Id, ts.ClassId, ts.SubjectId, s.SubjectName from TeacherSubject ts inner join Subject s on ts.SubjectId = s.SubjectId where ts.Id = '" + teacherSubjectId + "'");
                    ddlSubject.SelectedValue = dataTable.Rows[0]["SubjectId"].ToString();
                }
            }
        }
    }
}