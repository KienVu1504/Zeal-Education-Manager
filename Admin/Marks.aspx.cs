using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ZealEducationManager.Models.CommonFn;

namespace ZealEducationManager.Admin
{
    public partial class Marks : Page
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
                GetMarks();
            }
        }

        private void GetMarks()
        {
            DataTable dt = fn.Fletch(@"select ROW_NUMBER() over(order by (select 1)) as [Sr.No], e.ExamId, e.ClassId, c.ClassName, e.SubjectId, s.SubjectName, e.RollNo, e.TotalMarks, e.OutOfMarks from Exam e 
                                    inner join Class c on e.ClassId = c.ClassId inner join Subject s on e.SubjectId = s.SubjectId");
            GridView1.DataSource = dt;
            GridView1.DataBind();
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
                ddlSubject.SelectedIndex = 0;
            }
            else
            {
                DataTable dt = fn.Fletch("Select * from Subject where ClassId = '" + classId + "'");
                ddlSubject.DataSource = dt;
                ddlSubject.DataTextField = "SubjectName";
                ddlSubject.DataValueField = "SubjectId";
                ddlSubject.DataBind();
                ddlSubject.Items.Insert(0, "Select subject");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string classId = ddlClass.SelectedValue;
                string subjectId = ddlSubject.SelectedValue;
                string rollNo = txtRoll.Text.Trim();
                string studMarks = txtStudentMarks.Text.Trim();
                string outOfMarks = txtOutOfMarks.Text.Trim();
                if (subjectId == "" || subjectId == null || subjectId == "Select subject")
                {
                    lblMsg.Text = "Please select subject!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    if (rollNo == null || rollNo == "")
                    {
                        lblMsg.Text = "Please enter a valid roll number!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                    else
                    {
                        if (studMarks.Length > 4)
                        {
                            lblMsg.Text = "Mark must be <= 1000!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                        else
                        {
                            if (outOfMarks.Length > 4)
                            {
                                lblMsg.Text = "Mark must be <= 1000!";
                                lblMsg.CssClass = "alert alert-danger";
                            }
                            else
                            {
                                if (Convert.ToInt16(studMarks) < 0)
                                {
                                    lblMsg.Text = "Please enter a valid mark!";
                                    lblMsg.CssClass = "alert alert-danger";
                                }
                                else
                                {
                                    if (Convert.ToInt16(outOfMarks) < 0)
                                    {
                                        lblMsg.Text = "Please enter a valid mark!";
                                        lblMsg.CssClass = "alert alert-danger";
                                    }
                                    else
                                    {
                                        if (Convert.ToInt16(outOfMarks) > 1000)
                                        {
                                            lblMsg.Text = "Mark must be <= 1000!";
                                            lblMsg.CssClass = "alert alert-danger";
                                        }
                                        else
                                        {
                                            if (Convert.ToInt16(studMarks) > 1000)
                                            {
                                                lblMsg.Text = "Mark must be <= 1000!";
                                                lblMsg.CssClass = "alert alert-danger";
                                            }
                                            else
                                            {
                                                if (Convert.ToInt16(studMarks) > Convert.ToInt16(outOfMarks))
                                                {
                                                    lblMsg.Text = "Total mark must be <= Out of mark!";
                                                    lblMsg.CssClass = "alert alert-danger";
                                                }
                                                else
                                                {
                                                    DataTable dttbl = fn.Fletch("Select StudentId from Student where ClassId = '" + classId + "' and RollNo = '" + rollNo + "'");
                                                    if (dttbl.Rows.Count > 0)
                                                    {
                                                        DataTable dt = fn.Fletch("Select * from Exam where ClassId = '" + classId + "' and SubjectId = '" + subjectId + "' and RollNo = '" + rollNo + "'");
                                                        if (dt.Rows.Count == 0)
                                                        {
                                                            string query = "Insert into Exam values('" + classId + "','" + subjectId + "','" + rollNo + "', '" + studMarks + "', '" + outOfMarks + "')";
                                                            fn.Query(query);
                                                            lblMsg.Text = "Inserted successfully!";
                                                            lblMsg.CssClass = "alert alert-success";
                                                            ddlClass.SelectedIndex = 0;
                                                            ddlSubject.SelectedIndex = 0;
                                                            txtRoll.Text = String.Empty;
                                                            txtStudentMarks.Text = String.Empty;
                                                            txtOutOfMarks.Text = String.Empty;
                                                            GetMarks();
                                                        }
                                                        else
                                                        {
                                                            lblMsg.Text = "Entered <b>Data</b> already exists!";
                                                            lblMsg.CssClass = "alert alert-danger";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        lblMsg.Text = "Entered RollNo <b>" + rollNo + "</b> does not exist for selected class!";
                                                        lblMsg.CssClass = "alert alert-danger";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
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
            GetMarks();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetMarks();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int examId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                fn.Query("Delete from Exam where ExamId = '" + examId + "'");
                lblMsg.Text = "Exam mark deleted successfully!";
                lblMsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetMarks();
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
            GetMarks();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int examId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string classId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlClassGv")).SelectedValue;
                string subjectId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlSubjectGv")).SelectedValue;
                string rollNo = (row.FindControl("txtRollNoGv") as TextBox).Text.Trim();
                string studMarks = (row.FindControl("txtStudMarksGv") as TextBox).Text.Trim();
                string outOfMarks = (row.FindControl("txtOutOfMarksGv") as TextBox).Text.Trim();
                if (subjectId == null || subjectId == "" || subjectId == "Select subject")
                {
                    lblMsg.Text = "Please select subject!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    if (rollNo == null || rollNo == "")
                    {
                        lblMsg.Text = "Please enter a valid roll number!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                    else
                    {
                        if (studMarks.Length > 4)
                        {
                            lblMsg.Text = "Mark must be <= 1000!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                        else
                        {
                            if (outOfMarks.Length > 4)
                            {
                                lblMsg.Text = "Mark must be <= 1000!";
                                lblMsg.CssClass = "alert alert-danger";
                            }
                            else
                            {
                                if (Convert.ToInt16(studMarks) < 0)
                                {
                                    lblMsg.Text = "Please enter a valid mark!";
                                    lblMsg.CssClass = "alert alert-danger";
                                }
                                else
                                {
                                    if (Convert.ToInt16(outOfMarks) < 0)
                                    {
                                        lblMsg.Text = "Please enter a valid mark!";
                                        lblMsg.CssClass = "alert alert-danger";
                                    }
                                    else
                                    {
                                        if (Convert.ToInt16(outOfMarks) > 1000)
                                        {
                                            lblMsg.Text = "Mark must be <= 1000!";
                                            lblMsg.CssClass = "alert alert-danger";
                                        }
                                        else
                                        {
                                            if (Convert.ToInt16(studMarks) > 1000)
                                            {
                                                lblMsg.Text = "Mark must be <= 1000!";
                                                lblMsg.CssClass = "alert alert-danger";
                                            }
                                            else
                                            {
                                                if (Convert.ToInt16(studMarks) > Convert.ToInt16(outOfMarks))
                                                {
                                                    lblMsg.Text = "Total mark must be <= Out of mark!";
                                                    lblMsg.CssClass = "alert alert-danger";
                                                }
                                                else
                                                {
                                                    DataTable classCheck = fn.Fletch("select * from Student where ClassId = '" + classId + "' and RollNo = '" + rollNo + "'");
                                                    DataTable examCheck = fn.Fletch("select * from Exam e inner join Class c on e.ClassId = c.ClassId inner join Subject s on e.SubjectId = s.SubjectId where e.ClassId = "
                                                                                        + classId + " and e.SubjectId = " + subjectId + " and e.RollNo = '" + rollNo + "'");
                                                    if (classCheck.Rows.Count == 0)
                                                    {
                                                        lblMsg.Text = "Entered RollNo <b>" + rollNo + "</b> does not exist for selected class!";
                                                        lblMsg.CssClass = "alert alert-danger";
                                                    }
                                                    else
                                                    {
                                                        if (classCheck.Rows.Count == 1 && examCheck.Rows.Count == 1)
                                                        {
                                                            lblMsg.Text = "Entered RollNo <b>" + rollNo + "</b> does not exist for selected class!";
                                                            lblMsg.CssClass = "alert alert-danger";
                                                        }
                                                        else
                                                        {
                                                            fn.Query(@"Update Exam set ClassId = '" + classId + "', SubjectId = '" + subjectId + "', RollNo = '" + rollNo + "', TotalMarks = '" + studMarks + "', OutOfMarks = '"
                                                                + outOfMarks + "' " + "where ExamId = '" + examId + "'");
                                                            lblMsg.Text = "Record updated successfully!";
                                                            lblMsg.CssClass = "alert alert-success";
                                                            GridView1.EditIndex = -1;
                                                            GetMarks();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message + "!";
                lblMsg.CssClass = "alert alert-danger";
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
                    string selectedSubject = DataBinder.Eval(e.Row.DataItem, "SubjectName").ToString();
                    ddlSubject.Items.FindByText(selectedSubject).Selected = true;
                }
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
    }
}