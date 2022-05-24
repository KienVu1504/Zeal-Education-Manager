using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ZealEducationManager.Models.CommonFn;

namespace ZealEducationManager.Admin
{
    public partial class Student : Page
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
                GetStudents();
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
                if (txtName.Text.Trim() == null || txtName.Text.Trim() == "")
                {
                    lblMsg.Text = "Please enter name!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    DateTime dateVal = DateTime.Parse(txtDoB.Text.Trim());
                    DateTime now = DateTime.Now;
                    if (dateVal.Date < now.Date)
                    {
                        if (ddlGender.SelectedValue != "0")
                        {
                            string rollNo = txtRoll.Text.Trim();
                            if (rollNo == null || rollNo == "")
                            {
                                lblMsg.Text = "Please enter roll number!";
                                lblMsg.CssClass = "alert alert-danger";
                            }
                            else
                            {
                                if (txtAddress.Text.Trim() == null || txtAddress.Text.Trim() == "")
                                {
                                    lblMsg.Text = "Please enter address!";
                                    lblMsg.CssClass = "alert alert-danger";
                                }
                                else
                                {
                                    if (Convert.ToString(Convert.ToInt64(txtMobile.Text.Trim())).Length != 10)
                                    {
                                        lblMsg.Text = "Please enter a valid phone number!";
                                        lblMsg.CssClass = "alert alert-danger";
                                    }
                                    else
                                    {
                                        DataTable dt = fn.Fletch("Select * from Student where RollNo = '" + rollNo + "'");
                                        if (dt.Rows.Count == 0)
                                        {
                                            string query = "Insert into Student values ('" + txtName.Text.Trim() + "', '" + txtDoB.Text.Trim() + "', '" + ddlGender.Text.Trim() + "', '" + txtMobile.Text.Trim() + "'," +
                                                " '" + txtRoll.Text.Trim() + "', '" + txtAddress.Text.Trim() + "', '" + ddlClass.SelectedValue + "')";
                                            fn.Query(query);
                                            lblMsg.Text = "Inserted successfully!";
                                            lblMsg.CssClass = "alert alert-success";
                                            ddlGender.SelectedIndex = 0;
                                            txtName.Text = string.Empty;
                                            txtDoB.Text = string.Empty;
                                            txtMobile.Text = string.Empty;
                                            txtRoll.Text = string.Empty;
                                            txtAddress.Text = string.Empty;
                                            ddlClass.SelectedIndex = 0;
                                            GetStudents();
                                        }
                                        else
                                        {
                                            lblMsg.Text = "Entered roll no. <b>'" + rollNo + "'</b> already exist!";
                                            lblMsg.CssClass = "alert alert-danger";
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Gender is required!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Please enter a valid date of birth!";
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

        private void GetStudents()
        {
            DataTable dt = fn.Fletch("select ROW_NUMBER() over(order by (select 1)) as [Sr.No], s.StudentId, s.[Name], s.DOB, s.Gender, s.Mobile, s.RollNo, s.[Address], c.ClassId, c.ClassName from Student s " +
                                    "inner join Class c on c.ClassId = s.ClassId");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetStudents();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetStudents();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetStudents();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int studentId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string name = (row.FindControl("txtName") as TextBox).Text;
                string mobile = (row.FindControl("txtMobile") as TextBox).Text;
                string rollNo = (row.FindControl("txtRollNo") as TextBox).Text;
                string address = (row.FindControl("txtAddress") as TextBox).Text;
                string classId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[4].FindControl("ddlClass")).SelectedValue;
                if (name.Trim() == null || name.Trim() == "")
                {
                    lblMsg.Text = "Please enter a valid name!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    if (Convert.ToString(Convert.ToInt64(mobile.Trim())).Length != 10)
                    {
                        lblMsg.Text = "Please enter a valid phone number!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                    else
                    {
                        if (rollNo.Trim() == null || rollNo.Trim() == "")
                        {
                            lblMsg.Text = "Please enter a valid roll number!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                        else
                        {
                            if (address.Trim() == null || address.Trim() == "")
                            {
                                lblMsg.Text = "Please enter a valid address!";
                                lblMsg.CssClass = "alert alert-danger";
                            }
                            else
                            {
                                DataTable dataCheck = fn.Fletch("select * from Student where RollNo = '" + rollNo.Trim() + "'");
                                if (dataCheck.Rows.Count != 0)
                                {
                                    lblMsg.Text = "Entered data already exist!";
                                    lblMsg.CssClass = "alert alert-danger";
                                }
                                else
                                {
                                    fn.Query("Update Student set Name = '" + name.Trim() + "', Mobile = '" + mobile.Trim() + "', Address = '" + address.Trim() + "', RollNo = '" + rollNo.Trim() + "', ClassId = '" + classId + "' where StudentId = '" + studentId + "'");
                                    lblMsg.Text = "Student updated successfully!";
                                    lblMsg.CssClass = "alert alert-success";
                                    GridView1.EditIndex = -1;
                                    GetStudents();
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
            if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlClass = (DropDownList)e.Row.FindControl("ddlClass");
                DataTable dt = fn.Fletch("select * from Class");
                ddlClass.DataSource = dt;
                ddlClass.DataTextField = "ClassName";
                ddlClass.DataValueField = "ClassId";
                ddlClass.DataBind();
                ddlClass.Items.Insert(0, "select class");
                string selectedClass = DataBinder.Eval(e.Row.DataItem, "ClassName").ToString();
                ddlClass.Items.FindByText(selectedClass).Selected = true;            
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int studentId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                fn.Query("Delete from Student where StudentId = '" + studentId + "'");
                lblMsg.Text = "Student deleted successfully!";
                lblMsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetStudents();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message + "!";
                lblMsg.CssClass = "alert alert-danger";
            }
        }
    }
}