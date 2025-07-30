using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_Payroll_PayrollPendingSalarySlip : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                DivDetail.Visible = false;
                TextReadonly();
                FillDropdown();
                //FillGrid();
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
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));

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
            ds = null;
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
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
            lblMsg.Text = "";
            RepeaterEarning.DataSource = null;
            RepeaterEarning.DataBind();
            RepeaterDeduction.DataSource = null;
            RepeaterDeduction.DataBind();
            ds = objdb.ByProcedure("SpPayrollPendingSalaryDetail", new string[] { "flag", "EarnDeduction_Year" },
                new string[] { "1", ddlYear.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                DivDetail.Visible = true;

                RepeaterEarning.DataSource = ds.Tables[0];
                RepeaterEarning.DataBind();

                RepeaterDeduction.DataSource = ds.Tables[1];
                RepeaterDeduction.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        try
        {
            DivDetail.Visible = false;
            txtTotalEarning.Text = "0";
            txtTotalDeduction.Text = "0";
            txtNetPayment.Text = "0";
            txtSalary_Basic.Text = "0";
            txtETotalRemaining.Text = "0";
            txtPolicyRemaining.Text = "0";
            txtDTotalRemaining.Text = "0";

            RepeaterEarning.DataSource = null;
            RepeaterEarning.DataBind();
            RepeaterDeduction.DataSource = null;
            RepeaterDeduction.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void TextReadonly()
    {
        txtTotalEarning.Attributes.Add("readonly", "readonly");
        txtTotalDeduction.Attributes.Add("readonly", "readonly");
        txtNetPayment.Attributes.Add("readonly", "readonly");
        txtETotalRemaining.Attributes.Add("readonly", "readonly");
        txtDTotalRemaining.Attributes.Add("readonly", "readonly");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            lblMsg.Text = "";
            if (ddlEmployee.SelectedIndex == 0)
            {
                msg += "Select Employee Name. \\n";
            }
            if (ddlMonth.SelectedIndex == 0)
            {
                msg += "Select Month. \\n";
            }
            if (msg.Trim() == "")
            {
                // ===Earning===
                if (txtSalary_Basic.Text == "")
                    txtSalary_Basic.Text = "0";
                if (txtETotalRemaining.Text == "")
                    txtETotalRemaining.Text = "0";
                if (txtTotalEarning.Text == "")
                    txtTotalEarning.Text = "0";


                // ===Deduction ===
                if (txtPolicyRemaining.Text == "")
                    txtPolicyRemaining.Text = "0";
                if (txtDTotalRemaining.Text == "")
                    txtDTotalRemaining.Text = "0";
                if (txtTotalDeduction.Text == "")
                    txtTotalDeduction.Text = "0";
                if (txtNetPayment.Text == "")
                    txtNetPayment.Text = "0";

                DateTime CurrentDate = Convert.ToDateTime(DateTime.Now.ToString());
                int CurrentMonth = int.Parse(CurrentDate.Month.ToString());
                int CurrentYear = int.Parse(CurrentDate.Year.ToString());


                ds = objdb.ByProcedure("SpPayrollPendingSalaryDetail",
                new string[] { "flag", "Office_ID", "Emp_ID", "CurrentYear", "CurrentMonth", "Year", "Month", "MonthName", "NetPaymentAmount", "PendingSalaryUpdatedBy" },
                new string[] { "0", ViewState["Office_ID"].ToString(), ddlEmployee.SelectedValue.ToString(), CurrentYear.ToString(),CurrentMonth.ToString(),  ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(),ddlMonth.SelectedItem.ToString(),
                         txtNetPayment.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    string PendingSalaryID = ds.Tables[0].Rows[0]["PendingSalaryID"].ToString();
                    string EMP_ID = ddlEmployee.SelectedValue.ToString();
                    foreach (RepeaterItem item in RepeaterEarning.Items)
                    {
                        Label lblEarnDeduction_ID = (Label)item.FindControl("lblEarnDeduction_ID");
                        Label lblEarnDeduction_Name = (Label)item.FindControl("lblEarnDeduction_Name");
                        TextBox txtRemainingAmount = (TextBox)item.FindControl("txtRemainingEarning");

                        objdb.ByProcedure("SpPayrollPendingSalaryDetail",
                   new string[] { "flag", "PendingSalaryID", "Emp_ID", "EarnDeduction_Name", "EarningType", "RemainingAmount", "SalaryUpdatedBy" },
                   new string[] { "3", PendingSalaryID, EMP_ID, lblEarnDeduction_Name.Text, "Earning", txtRemainingAmount.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    }

                    foreach (RepeaterItem item in RepeaterDeduction.Items)
                    {
                        Label lblEarnDeduction_ID = (Label)item.FindControl("lblEarnDeduction_ID");
                        Label lblEarnDeduction_Name = (Label)item.FindControl("lblEarnDeduction_Name");
                        TextBox txtRemainingAmount = (TextBox)item.FindControl("txtRemainingDeduction");
                        if (txtRemainingAmount.Text == "")
                            txtRemainingAmount.Text = "0";


                        objdb.ByProcedure("SpPayrollPendingSalaryDetail",
                    new string[] { "flag", "PendingSalaryID", "Emp_ID", "EarnDeduction_Name", "EarningType", "RemainingAmount", "SalaryUpdatedBy" },
                    new string[] { "3", PendingSalaryID, EMP_ID, lblEarnDeduction_Name.Text, "Deduction", txtRemainingAmount.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    }
                }

                ddlYear.ClearSelection();
                ddlMonth.ClearSelection();
                ClearText();
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
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByProcedure("SpPayrollPendingSalaryDetail", new string[] { "flag", "Emp_ID" }, new string[] { "2", ddlEmployee.SelectedValue.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                txtSalary_Basic.Text = ds.Tables[0].Rows[0]["Emp_BasicSalery"].ToString();
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
            FillGrid();
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
            ds = objdb.ByProcedure("SpPayrollPendingSalaryDetail", new string[] { "flag", "Year", "Emp_ID", "Month" }, new string[] { "4", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                RepeaterEarning.DataSource = ds.Tables[0];
                RepeaterEarning.DataBind();
                RepeaterDeduction.DataSource = ds.Tables[1];
                RepeaterDeduction.DataBind();
                foreach (RepeaterItem item in RepeaterEarning.Items)
                {
                    TextBox RemainingAmount = (TextBox)item.FindControl("txtRemainingEarning");
                    if (RemainingAmount != null)
                    {
                        RemainingAmount.Text = ds.Tables[0].Rows[0]["RemainingAmount"].ToString();

                    }
                }
            }
            else
            {
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}