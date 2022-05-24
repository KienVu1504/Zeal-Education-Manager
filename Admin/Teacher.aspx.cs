using System;
using System.Data;
using System.Web.UI.WebControls;
using static ZealEducationManager.Models.CommonFn;

namespace ZealEducationManager.Admin
{
    public partial class Teacher : System.Web.UI.Page
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
                GetTeachers();
            }
        }

        private void GetTeachers()
        {
            DataTable dt = fn.Fletch("Select ROW_Number() OVER(ORDER BY (SELECT 1)) as [Sr.No], TeacherId, [Name], DOB, Gender, Mobile, Email, [Address], [Password] from Teacher");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlGender.SelectedValue != "0")
                {
                    string email = txtEmail.Text.Trim();
                    DataTable dt = fn.Fletch("Select * from Teacher where Email = '" + email + "'");
                    DateTime dateVal = DateTime.Parse(txtDoB.Text.Trim());
                    DateTime now = DateTime.Now;
                    if (dt.Rows.Count == 0)
                    {
                        if (txtName.Text.Trim() == "" || txtName.Text.Trim() == null)
                        {
                            lblMsg.Text = "Please enter a valid name!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                        else
                        {
                            if (dateVal.Date < now.Date)
                            {
                                if (txtEmail.Text.Trim() == "" || txtEmail.Text.Trim() == null)
                                {
                                    lblMsg.Text = "Please enter a valid email!";
                                    lblMsg.CssClass = "alert alert-danger";
                                }
                                else
                                {
                                    if (txtPassword.Text.Trim() == "" || txtPassword.Text.Trim() == null)
                                    {
                                        lblMsg.Text = "Please enter a valid password!";
                                        lblMsg.CssClass = "alert alert-danger";
                                    }
                                    else
                                    {
                                        if (txtAddress.Text.Trim() == "" || txtAddress.Text.Trim() == null)
                                        {
                                            lblMsg.Text = "Please enter a valid address!";
                                            lblMsg.CssClass = "alert alert-danger";
                                        }
                                        else
                                        {
                                            string query = "Insert into Teacher values ('" + txtName.Text.Trim() + "', '" + txtDoB.Text.Trim() + "', '" + ddlGender.Text.Trim() + "', '" + txtMobile.Text.Trim() + "'," +
                                                                                      " '" + txtEmail.Text.Trim() + "', '" + txtAddress.Text.Trim() + "', '" + txtPassword.Text.Trim() + "')";
                                            fn.Query(query);
                                            lblMsg.Text = "Inserted successfully!";
                                            lblMsg.CssClass = "alert alert-success";
                                            ddlGender.SelectedIndex = 0;
                                            txtName.Text = string.Empty;
                                            txtDoB.Text = string.Empty;
                                            txtMobile.Text = string.Empty;
                                            txtEmail.Text = string.Empty;
                                            txtAddress.Text = string.Empty;
                                            txtPassword.Text = string.Empty;
                                            GetTeachers();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                lblMsg.Text = "Please enter a valid date of birth!";
                                lblMsg.CssClass = "alert alert-danger";
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Entered <b>'" + email + "'</b> already exist!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
                else
                {
                    lblMsg.Text = "Gender is required!";
                    lblMsg.CssClass = "alert alert-danger";
                }
            } catch (Exception ex)
            {
                lblMsg.Text = ex.Message + "!";
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetTeachers();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetTeachers();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int teacherId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                DataTable teacherSubjectCheck = fn.Fletch("select * from TeacherSubject where TeacherId = '" + teacherId + "'");
                DataTable teacherAttendanceCheck = fn.Fletch("select * from TeacherAttendance where TeacherId = '" + teacherId + "'");
                if (teacherSubjectCheck.Rows.Count != 0 || teacherAttendanceCheck.Rows.Count != 0)
                {
                    lblMsg.Text = "You can only delete teachers that contain no data!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    fn.Query("Delete from Teacher where TeacherId = '" + teacherId + "'");
                    lblMsg.Text = "Teacher deleted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    GridView1.EditIndex = -1;
                    GetTeachers();
                }
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
            GetTeachers();
        }

        static bool ValidatePassword(string passWord)
        {
            int validConditions = 0;
            foreach (char c in passWord)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in passWord)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 0) return false;
            foreach (char c in passWord)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 1) return false;
            if (validConditions == 2)
            {
                char[] special = { '@', '#', '$', '%', '^', '&', '+', '=' };
                if (passWord.IndexOfAny(special) == -1) return false;
            }
            return true;
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int teacherId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string name = (row.FindControl("txtName") as TextBox).Text;
                string mobile = (row.FindControl("txtMobile") as TextBox).Text;
                string password = (row.FindControl("txtPassword") as TextBox).Text;
                string address = (row.FindControl("txtAddress") as TextBox).Text;
                if (name.Trim() == "" || name.Trim() == null)
                {
                    lblMsg.Text = "Please enter a valid name!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    if (mobile.Trim().Length != 10)
                    {
                        lblMsg.Text = "Please enter a valid phone number!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                    else
                    {
                        if (password.Trim() == "" || password.Trim() == null)
                        {
                            lblMsg.Text = "Please enter a valid password!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                        else
                        {
                            if (ValidatePassword(password.Trim()))
                            {
                                if (address.Trim() == null || address.Trim() == "")
                                {
                                    lblMsg.Text = "Please enter a valid address!";
                                    lblMsg.CssClass = "alert alert-danger";
                                }
                                else
                                {
                                    fn.Query("Update Teacher set Name = '" + name.Trim() + "', Mobile = '" + mobile.Trim() + "', Address = '" + address.Trim() + "', Password = '" + password.Trim() + "' where TeacherId = '" + teacherId + "'");
                                    lblMsg.Text = "Teacher updated successfully!";
                                    lblMsg.CssClass = "alert alert-success";
                                    GridView1.EditIndex = -1;
                                    GetTeachers();
                                }
                            }
                            else
                            {
                                lblMsg.Text = "Password must have at least one lower case letter, upper case letter, special character, number & 8 characters length!";
                                lblMsg.CssClass = "alert alert-danger";
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
    }
}