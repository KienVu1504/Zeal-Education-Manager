using System;
using System.Data;
using System.Web.UI.WebControls;
using static ZealEducationManager.Models.CommonFn;

namespace ZealEducationManager
{
    public partial class MarksDetailUserControl : System.Web.UI.UserControl
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClass();
                GetMarks();
            }
        }

        private void GetMarks()
        {
            DataTable dt = fn.Fletch(@"select ROW_NUMBER() over(order by (select 1)) as [Sr.No], e.ExamId, e.ClassId, c.ClassName, e.SubjectId, s.SubjectName, e.RollNo, e.TotalMarks, e.OutOfMarks 
                                        from Exam e inner join Class c on c.ClassId = e.ClassId inner join Subject s on s.SubjectId = e.SubjectId");
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string classId = ddlClass.SelectedValue;
                string rollNo = txtRoll.Text.Trim();
                if (classId == null || classId == "" || classId == "Select class")
                {
                    lblMsg.Text = "Please select class!";
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
                        DataTable dt = fn.Fletch(@"select ROW_NUMBER() over(order by (select 1)) as [Sr.No], e.ExamId, e.ClassId, c.ClassName, e.SubjectId, s.SubjectName, e.RollNo, e.TotalMarks, e.OutOfMarks 
                                        from Exam e inner join Class c on c.ClassId = e.ClassId inner join Subject s on s.SubjectId = e.SubjectId where e.ClassId = '" + classId + "' and e.RollNo = '" + rollNo + "'");
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
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
        }
    }
}