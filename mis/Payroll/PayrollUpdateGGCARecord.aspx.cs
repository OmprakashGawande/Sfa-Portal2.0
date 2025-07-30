using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_Payroll_PayrollUpdateGGCARecord : System.Web.UI.Page
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
            ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag" }, new string[] { "19" }, "dataset");
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
                TextBox GGCALicId = (TextBox)row.FindControl("txtLicID");
                TextBox GGCANewId = (TextBox)row.FindControl("txtNewId");
                TextBox GGCAEmpNO = (TextBox)row.FindControl("txtEmpNo");
                objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "Emp_ID", "GGCA_LICID", "GGCA_NEWID", "GGCA_EMPNO"},
                                    new string[] { "20", Emp_Id,GGCALicId.Text,GGCANewId.Text,GGCAEmpNO.Text}, "dataset");

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