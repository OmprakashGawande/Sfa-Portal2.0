using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Payroll_AddSalaryDetail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                // DivDetail.Visible = false;
                FillDropdown();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillDropdown()
    {
        try
        {
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            }
            ds = null;
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "12" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            }
            //ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();

        ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
        ds = null;
        ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ddlOfficeName.SelectedValue.ToString() }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlEmployee.DataSource = ds;
            ddlEmployee.DataTextField = "Emp_Name";
            ddlEmployee.DataValueField = "Emp_ID";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlOfficeName.SelectedIndex <= 0)
            {
                msg += "Select Office Name \\n";
            }
            if (ddlEmployee.SelectedIndex <= 0)
            {
                msg += "Select Emp Name \\n";
            }
            if (ddlYear.SelectedIndex <= 0)
            {
                msg += "Select Year\\n";
            }
            //if (ddlMonth.SelectedIndex <= 0)
            //{
            //    msg += "Select Month\\n";
            //}
            if (msg == "")
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Emp_ID", "Year", "MonthNo" }, new string[] { "14", ddlOfficeName.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    FillData();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillData()
    {
        try
        {
            lblMsg.Text = "";
            foreach (GridViewRow row in GridView1.Rows)
            {
                TextBox txtBasicSalary = (TextBox)row.FindControl("txtBasicSalary");
                TextBox txtPayableDays = (TextBox)row.FindControl("txtPayableDays");
                TextBox txtEarningTotal = (TextBox)row.FindControl("txtEarningTotal");
                TextBox txtDeductionTotal = (TextBox)row.FindControl("txtDeductionTotal");
                TextBox txtNetSalary = (TextBox)row.FindControl("txtNetSalary");
                TextBox txtNoDayEarnAmt = (TextBox)row.FindControl("txtNoDayEarnAmt");
                HiddenField hdnSalaryID = (HiddenField)row.FindControl("hdnSalaryID");
                Button BtnSave = (Button)row.FindControl("BtnSave");
                string Emp_ID = BtnSave.ToolTip.ToString();
                ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Emp_ID", "Year", "MonthNo" }, new string[] { "14", ddlOfficeName.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtBasicSalary.Text = ds.Tables[0].Rows[0]["Salary_Basic"].ToString();
                    txtPayableDays.Text = ds.Tables[0].Rows[0]["Salary_PayableDays"].ToString();
                    txtEarningTotal.Text = ds.Tables[0].Rows[0]["Salary_EarningTotal"].ToString();
                    txtDeductionTotal.Text = ds.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString();
                    txtNetSalary.Text = ds.Tables[0].Rows[0]["Salary_NetSalary"].ToString();
                    txtNoDayEarnAmt.Text = ds.Tables[0].Rows[0]["Salary_NoDayEarnAmt"].ToString();
                    hdnSalaryID.Value = ds.Tables[0].Rows[0]["Salary_ID"].ToString();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }
                else { }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int selRowIndex = ((GridViewRow)(((Button)sender).Parent.Parent)).RowIndex;
            TextBox chk = (TextBox)GridView1.Rows[selRowIndex].FindControl("txtBasicSalary");
            string Emp_ID = chk.ToolTip.ToString();
            lblMsg.Text = "";
            string msg = "";

            TextBox txtBasicSalary = (TextBox)GridView1.Rows[selRowIndex].FindControl("txtBasicSalary");
            TextBox txtPayableDays = (TextBox)GridView1.Rows[selRowIndex].FindControl("txtPayableDays");
            TextBox txtEarningTotal = (TextBox)GridView1.Rows[selRowIndex].FindControl("txtEarningTotal");
            TextBox txtDeductionTotal = (TextBox)GridView1.Rows[selRowIndex].FindControl("txtDeductionTotal");
            TextBox txtNetSalary = (TextBox)GridView1.Rows[selRowIndex].FindControl("txtNetSalary");
            TextBox txtNoDayEarnAmt = (TextBox)GridView1.Rows[selRowIndex].FindControl("txtNoDayEarnAmt");
            HiddenField hdnSalaryID = (HiddenField)GridView1.Rows[selRowIndex].FindControl("hdnSalaryID");
            if (txtBasicSalary.Text == "")
            {
                msg += "Please Enter Basic Salary. \\n";
            }
            if (txtPayableDays.Text == "")
            {
                msg += "Please Enter Payable Days. \\n";
            }
            if (txtEarningTotal.Text == "")
            {
                msg += "Please Enter Total Earning. \\n";
            }
            if (txtDeductionTotal.Text == "")
            {
                msg += "Please Enter Total Deduction. \\n";
            }
            if (txtNetSalary.Text == "")
            {
                msg += "Please Enter Net Salary. \\n";
            }
            if (txtNoDayEarnAmt.Text == "")
            {
                msg += "Please Enter NoDayEarnAmt. \\n";
            }
            if (msg.Trim() == "")
            {
                string BasicSalary = txtBasicSalary.Text;
                string PayableDays = txtPayableDays.Text;
                string EarningTotal = txtEarningTotal.Text;
                string DeductionTotal = txtDeductionTotal.Text;
                string NetSalary = txtNetSalary.Text;
                string NoDayEarnAmt = txtNoDayEarnAmt.Text;
                string SalaryID = hdnSalaryID.Value;

                objdb.ByProcedure("SpPayrollSalaryDetail",
                         new string[] { "flag", "Salary_ID", "Office_ID", "Year", "MonthNo", "Emp_ID", "Salary_Basic", "Salary_PayableDays", "Salary_EarningTotal", "Salary_DeductionTotal", "Salary_NetSalary", "Salary_NoDayEarnAmt", "Salary_UpdatedBy" },
                         new string[] { "15", SalaryID, ViewState["Office_ID"].ToString(), ddlYear.SelectedValue.ToString(),ddlMonth.SelectedValue.ToString(), Emp_ID, BasicSalary, PayableDays, EarningTotal, DeductionTotal, NetSalary, NoDayEarnAmt, ViewState["Emp_ID"].ToString()}, "dataset");
                
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}