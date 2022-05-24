using System;
using System.Data;
using System.Web.UI.WebControls;
using static ZealEducationManager.Models.CommonFn;

namespace ZealEducationManager.Admin
{
    public partial class Expense : System.Web.UI.Page
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
                GetExpense();
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

        private void GetExpense()
        {
            DataTable dt = fn.Fletch(@"select ROW_NUMBER() over(order by (select 1)) as [Sr.No], e.ExpenseId, e.ClassId, c.ClassName, e.SubjectId, s.SubjectName, e.ChargeAmount from Expense e 
                                      inner join Class c on e.ClassId = c.ClassId inner join Subject s on e.SubjectId = s.SubjectId");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string classId = ddlClass.SelectedValue;
                string subjectId = ddlSubject.SelectedValue;
                long chargeAmt = Convert.ToInt64(txtExpenseAmt.Text.Trim());
                if (classId == "Select class" || subjectId == "Select subject")
                {
                    lblMsg.Text = "Please select class and subject!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    if (chargeAmt > 999999999)
                    {
                        lblMsg.Text = "Fee must be <= 999999999!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                    else if (chargeAmt < 0)
                    {
                        lblMsg.Text = "Fee must be >= 0!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                    else
                    {
                        DataTable dt = fn.Fletch("Select * from Expense where ClassId = '" + classId + "' and SubjectId = '" + subjectId + "' or ChargeAmount = '" + chargeAmt + "'");
                        if (dt.Rows.Count == 0)
                        {
                            string query = "Insert into Expense values('" + classId + "','" + subjectId + "','" + chargeAmt + "')";
                            fn.Query(query);
                            lblMsg.Text = "Inserted successfully!";
                            lblMsg.CssClass = "alert alert-success";
                            ddlClass.SelectedIndex = 0;
                            ddlSubject.SelectedIndex = 0;
                            txtExpenseAmt.Text = String.Empty;
                            GetExpense();
                        }
                        else
                        {
                            lblMsg.Text = "Entered <b>Data</b> already exists!";
                            lblMsg.CssClass = "alert alert-danger";
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
            GetExpense();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetExpense();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int expenseId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                fn.Query("Delete from Expense where ExpenseId = '" + expenseId + "'");
                lblMsg.Text = "Expense Deleted Successfully!";
                lblMsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetExpense();
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
            GetExpense();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int expenseId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string classId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlClassGv")).SelectedValue;
                string subjectId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlSubjectGv")).SelectedValue;
                string chargeAmt = (row.FindControl("txtExpenseAmt") as TextBox).Text.Trim();
                if (subjectId == "Select subject" || subjectId == "" || subjectId == null)
                {
                    lblMsg.Text = "Please select subject!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    if (chargeAmt.Length >= 10)
                    {
                        lblMsg.Text = "Fee must be <= 999999999!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                    else
                    {
                        if (chargeAmt.Contains("-") || chargeAmt.Contains("+"))
                        {
                            lblMsg.Text = "Fee must be >= 0!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                        else
                        {
                            DataTable dataCheck = fn.Fletch("select * from Expense where ClassId = '" + classId + "' and SubjectId = '" + subjectId + "'");
                            if (dataCheck.Rows.Count != 0)
                            {
                                lblMsg.Text = "Entered data already exist!";
                                lblMsg.CssClass = "alert alert-danger";
                            }
                            else
                            {
                                fn.Query(@"Update Expense set ClassId = '" + classId + "', SubjectId = '" + subjectId + "', ChargeAmount = '" + chargeAmt + "' where ExpenseId = '" + expenseId + "'");
                                lblMsg.Text = "Record updated successfully!";
                                lblMsg.CssClass = "alert alert-success";
                                GridView1.EditIndex = -1;
                                GetExpense();
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
                    string expenseId = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                    DataTable dataTable = fn.Fletch(@"select e.ExpenseId, e.ClassId, e.SubjectId, s.SubjectName from Expense e inner join Subject s on e.SubjectId = s.SubjectId where e.ExpenseId = '" + expenseId + "'");
                    ddlSubject.SelectedValue = dataTable.Rows[0]["SubjectId"].ToString();
                }
            }
        }
    }
}