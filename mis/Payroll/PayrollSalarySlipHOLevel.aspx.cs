using System;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
public partial class mis_Payroll_PayrollSalarySlipHOLevel : System.Web.UI.Page
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

                DivSlip.Visible = false;
                lblNotGenerated.Visible = false;
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
                ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlYear.DataSource = ds;
                    ddlYear.DataTextField = "Year";
                    ddlYear.DataValueField = "Year";
                    ddlYear.DataBind();
                    ddlYear.Items.Insert(0, new ListItem("Select", "0"));
                }


                ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
                ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
                ds = null;
                ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    ddlOfficeName.DataSource = ds;
                    ddlOfficeName.DataTextField = "Office_Name";
                    ddlOfficeName.DataValueField = "Office_ID";
                    ddlOfficeName.DataBind();
                    ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
                }
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
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlEmployee.Items.Clear();
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            FillEmployee();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEmployee()
    {
        try
        {
            if (ddlOfficeName.SelectedIndex > 0)
            {
                DataSet ds1 = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ddlOfficeName.SelectedValue.ToString() }, "dataset");
                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    ddlEmployee.DataSource = ds1;
                    ddlEmployee.DataTextField = "Emp_Name";
                    ddlEmployee.DataValueField = "Emp_ID";
                    ddlEmployee.DataBind();
                    ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DivSlip.Visible = false;
            lblNotGenerated.Visible = false;
            if (ddlYear.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Year", "MonthNo" }, new string[] { "3", ddlEmployee.SelectedValue.ToString(),ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    FillDetail(ddlEmployee.SelectedValue.ToString(), ddlOfficeName.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString());
                }
                else
                {
                    lblNotGenerated.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDetail(string Emp_ID, string Office_ID, string Year, string MonthNo)
    {
        try
        {
            ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo" }, new string[] { "2", Emp_ID, Office_ID, Year, MonthNo }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblEmp_Name.Text = ds.Tables[0].Rows[0]["Emp_Name"].ToString();
                    lblDesignation_Name.Text = ds.Tables[0].Rows[0]["Designation_Name"].ToString();
                    lblUserName.Text = ds.Tables[0].Rows[0]["UserName"].ToString();
                    lblBank_AccountNo.Text = ds.Tables[0].Rows[0]["Bank_AccountNo"].ToString();
                    //lblEmp_GpfType.Text = ds.Tables[0].Rows[0]["Emp_GpfType"].ToString();
                    //lblEmp_GpfNo.Text = ds.Tables[0].Rows[0]["Emp_GpfNo"].ToString();
                    lblFinancialYear.InnerHtml = ds.Tables[0].Rows[0]["FinancialYear"].ToString();
                    lblMonth.InnerHtml = ds.Tables[0].Rows[0]["Month"].ToString();
                    lblSalary_Basic.Text = ds.Tables[0].Rows[0]["Salary_Basic"].ToString();
                    // lblSalary_NoDayEarnAmt.Text = ds.Tables[0].Rows[0]["Salary_NoDayEarnAmt"].ToString();
                    lblSalary_NoDayDeduAmt.Text = ds.Tables[0].Rows[0]["Salary_NoDayDeduAmt"].ToString();
                    lblSalary_NetSalary.Text = ds.Tables[0].Rows[0]["Salary_NetSalary"].ToString();
                    lblSalary_EarningTotal.Text = ds.Tables[0].Rows[0]["Salary_EarningTotal"].ToString();
                    lblPolicyDeduction.Text = ds.Tables[0].Rows[0]["PolicyDeduction"].ToString();
                    lblSalary_DeductionTotal.Text = ds.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString();
                    lblBank_Name.Text = ds.Tables[0].Rows[0]["Bank_Name"].ToString();
                    lblIFSCCode.Text = ds.Tables[0].Rows[0]["Bank_IfscCode"].ToString();
                    lblGroupInsurance_No.Text = ds.Tables[0].Rows[0]["GroupInsurance_No"].ToString();
                    lblEPF_No.Text = ds.Tables[0].Rows[0]["EPF_No"].ToString() + " / " + ds.Tables[0].Rows[0]["UAN_No"].ToString();
                    totlleavedays.InnerHtml = ds.Tables[0].Rows[0]["LeaveDays"].ToString();
                    DivSlip.Visible = true;
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    RepeaterEarning.DataSource = ds.Tables[1];
                    RepeaterEarning.DataBind();
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    RepeaterDeduction.DataSource = ds.Tables[2];
                    RepeaterDeduction.DataBind();
                }
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}