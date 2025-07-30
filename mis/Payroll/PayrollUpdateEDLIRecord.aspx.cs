using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_Payroll_PayrollUpdateEDLIRecord : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    btnSave.Visible = false;
                    FillGrid();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag" }, new string[] { "7" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                btnSave.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
			lblMsg.Text = "";
            foreach (GridViewRow row in GridView1.Rows)
            {
                Label Emp_Name = (Label)row.FindControl("Emp_Name");
                string Emp_Id = Emp_Name.ToolTip.ToString();
                TextBox EDLIID = (TextBox)row.FindControl("txtEDLIID");
                TextBox EDLINO = (TextBox)row.FindControl("txtEDLINO");
                TextBox EDLIFreq = (TextBox)row.FindControl("txtEDLIFreq");
                TextBox DOB = (TextBox)row.FindControl("txtDOB");
                TextBox DOJ = (TextBox)row.FindControl("txtDOJ");
                TextBox DOR = (TextBox)row.FindControl("txtDOR");
                objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "Emp_ID", "EDLI_ID", "EDLI_EmpNo", "EDLIFrequency", "Emp_Dob", "Emp_JoiningDate", "Emp_RetirementDate" },
                                    new string[] { "8", Emp_Id, EDLIID.Text, EDLINO.Text, EDLIFreq.Text, Convert.ToDateTime(DOB.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(DOJ.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(DOR.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

            }
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}