using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Linq;

public partial class mis_Payroll_PayrollArrearDetail : System.Web.UI.Page
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
    protected void TextReadonly()
    {
        txtTotalEarning.Attributes.Add("readonly", "readonly");
        txtTotalDeduction.Attributes.Add("readonly", "readonly");
        txtNetPayment.Attributes.Add("readonly", "readonly");

        //txtEarnPaidSalary_Basic.Attributes.Add("readonly", "readonly");
        txtEarnRemainingSalary_Basic.Attributes.Add("readonly", "readonly");
        txtETotalBePaidAmount.Attributes.Add("readonly", "readonly");
        txtETotalSalary_Basic.Attributes.Add("readonly", "readonly");
        txtETotalRemaining.Attributes.Add("readonly", "readonly");

        //txtDeductionPaidSalary.Attributes.Add("readonly", "readonly");
        txtDeductionRemainingSalary.Attributes.Add("readonly", "readonly");
        //txtPolicyPaidAmount.Attributes.Add("readonly", "readonly");
        txtPolicyRemaining.Attributes.Add("readonly", "readonly");
        //   txtDTotalRemaining.Attributes.Add("readonly", "readonly");
        txtDTotalBePaidAmount.Attributes.Add("readonly", "readonly");
        txtDTotalSalary_Basic.Attributes.Add("readonly", "readonly");
        txtDTotalRemaining.Attributes.Add("readonly", "readonly");
    }
    protected void FillDropdown()
    {
        try
        {
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            ddlFYear.Items.Insert(0, new ListItem("Select", "0"));
            ddlCurrentYear.Items.Insert(0, new ListItem("Select", "0"));

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
                // ==== From Year ====
                ddlFYear.DataSource = ds;
                ddlFYear.DataTextField = "Year";
                ddlFYear.DataValueField = "Year";
                ddlFYear.DataBind();
                ddlFYear.Items.Insert(0, new ListItem("Select", "0"));
                // ==== To Year ====
                ddlCurrentYear.DataSource = ds;
                ddlCurrentYear.DataTextField = "Year";
                ddlCurrentYear.DataValueField = "Year";
                ddlCurrentYear.DataBind();
                ddlCurrentYear.Items.Insert(0, new ListItem("Select", "0"));
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
            ds = objdb.ByProcedure("SpPayrollArrear", new string[] { "flag", "Emp_ID", "FYear", "FMonthNo", "TYear", "TMonthNo" },
                new string[] { "1", ddlEmployee.SelectedValue.ToString(), ddlFYear.SelectedValue.ToString(), ddlFMonth.SelectedValue.ToString(), ddlFYear.SelectedValue.ToString(), ddlFMonth.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                DivDetail.Visible = true;

                RepeaterEarning.DataSource = ds.Tables[0];
                RepeaterEarning.DataBind();

                RepeaterDeduction.DataSource = ds.Tables[1];
                RepeaterDeduction.DataBind();

                //=========== Earning =========
                decimal Earningtotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                decimal TotalSalary_Basic = Convert.ToDecimal(ds.Tables[2].Rows[0]["Salary_Basic"].ToString());
                txtEarnBePaidSalary_Basic.Text = TotalSalary_Basic.ToString();
                txtEarnPaidSalary_Basic.Text = TotalSalary_Basic.ToString();

                txtETotalBePaidAmount.Text = Math.Round((Earningtotal + TotalSalary_Basic), 2).ToString();
                txtETotalSalary_Basic.Text = Math.Round((Earningtotal + TotalSalary_Basic), 2).ToString();

                // txtTotalEarning.Text = Math.Round((Earningtotal + TotalSalary_Basic), 2).ToString();


                //=========== Deduction =========


                decimal Deductiontotal = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                decimal TotalSalary_NoDayDedu = Convert.ToDecimal(ds.Tables[2].Rows[0]["Salary_NoDayDeduAmt"].ToString());
                decimal PolicyDed_PolicyAmt = Convert.ToDecimal(ds.Tables[3].Rows[0]["PolicyDed_PolicyAmt"].ToString());

                txtDeductionBePaidSalary.Text = TotalSalary_NoDayDedu.ToString();
                txtDeductionPaidSalary.Text = TotalSalary_NoDayDedu.ToString();

                txtPolicyBePaidAmount.Text = PolicyDed_PolicyAmt.ToString();
                txtPolicyPaidAmount.Text = PolicyDed_PolicyAmt.ToString();


                txtDTotalBePaidAmount.Text = Math.Round((Deductiontotal + TotalSalary_NoDayDedu + PolicyDed_PolicyAmt), 2).ToString();
                txtDTotalSalary_Basic.Text = Math.Round((Deductiontotal + TotalSalary_NoDayDedu + PolicyDed_PolicyAmt), 2).ToString();

                // txtTotalDeduction.Text = Math.Round((Deductiontotal + TotalSalary_NoDayDedu + PolicyDed_PolicyAmt), 2).ToString();


                //decimal NetPayment = (Earningtotal + TotalSalary_Basic) - (Deductiontotal + TotalSalary_NoDayDedu + PolicyDed_PolicyAmt);
                //txtNetPayment.Text = Math.Round(NetPayment, 2).ToString();
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
            string msg = "";
            lblMsg.Text = "";
            DivDetail.Visible = false;
            ClearText();

            if (ddlEmployee.SelectedIndex == 0)
            {
                msg += "Select Employee Name. \\n";
            }
            if (ddlFYear.SelectedIndex == 0)
            {
                msg += "Select From Year. \\n";
            }
            if (ddlFMonth.SelectedIndex == 0)
            {
                msg += "Select From Month. \\n";
            }
            if (ddlCurrentYear.SelectedIndex == 0)
            {
                msg += "Select Current Year. \\n";
            }
            if (ddlCurrentMonth.SelectedIndex == 0)
            {
                msg += "Select Current Month. \\n";
            }
            if (msg.Trim() == "")
            {
                int FYear = int.Parse(ddlFYear.SelectedValue.ToString());
                int FMonth = int.Parse(ddlFMonth.SelectedValue.ToString());
                int TYear = int.Parse(ddlCurrentYear.SelectedValue.ToString());
                int TMonth = int.Parse(ddlCurrentMonth.SelectedValue.ToString());
                if (FYear > TYear)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select correct Year and Month.');", true);
                }
                else if (FYear == TYear && FMonth > TMonth)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select correct Year and Month');", true);
                }
                else
                {
                    FillGrid();
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
    protected void ClearText()
    {
        try
        {
            DivDetail.Visible = false;
            txtTotalEarning.Text = "0";
            txtTotalDeduction.Text = "0";
            txtNetPayment.Text = "0";
            txtEarnPaidSalary_Basic.Text = "0";
            txtEarnRemainingSalary_Basic.Text = "0";
            txtETotalBePaidAmount.Text = "0";
            txtETotalSalary_Basic.Text = "0";
            txtETotalRemaining.Text = "0";
            txtDeductionPaidSalary.Text = "0";
            txtDeductionRemainingSalary.Text = "0";
            txtPolicyPaidAmount.Text = "0";
            txtPolicyRemaining.Text = "0";
            // txtDTotalRemaining.Text = "";
            txtDTotalBePaidAmount.Text = "0";
            txtDTotalSalary_Basic.Text = "0";
            txtDTotalRemaining.Text = "0";
            txtEarnBePaidSalary_Basic.Text = "0";
            txtDeductionBePaidSalary.Text = "0";
            txtPolicyBePaidAmount.Text = "0";

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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            string OrderDate = "";
            lblMsg.Text = "";

            if (ddlEmployee.SelectedIndex == 0)
            {
                msg += "Select Employee Name. \\n";
            }
            if (ddlFYear.SelectedIndex == 0)
            {
                msg += "Select From Year. \\n";
            }
            if (ddlFMonth.SelectedIndex == 0)
            {
                msg += "Select From Month. \\n";
            }
            if (ddlCurrentYear.SelectedIndex == 0)
            {
                msg += "Select Current Year. \\n";
            }
            if (ddlCurrentMonth.SelectedIndex == 0)
            {
                msg += "Select Current Month. \\n";
            }

            if (msg.Trim() == "")
            {
                string EMP_ID = ddlEmployee.SelectedValue.ToString();
                // ===Earning===
                if (txtEarnBePaidSalary_Basic.Text == "")
                    txtEarnBePaidSalary_Basic.Text = "0";
                if (txtEarnPaidSalary_Basic.Text == "")
                    txtEarnPaidSalary_Basic.Text = "0";
                if (txtEarnRemainingSalary_Basic.Text == "")
                    txtEarnRemainingSalary_Basic.Text = "0";
                if (txtETotalBePaidAmount.Text == "")
                    txtETotalBePaidAmount.Text = "0";
                if (txtETotalSalary_Basic.Text == "")
                    txtETotalSalary_Basic.Text = "0";
                if (txtETotalRemaining.Text == "")
                    txtETotalRemaining.Text = "0";
                if (txtTotalEarning.Text == "")
                    txtTotalEarning.Text = "0";
                // ===Deduction ===
                if (txtDeductionBePaidSalary.Text == "")
                    txtDeductionBePaidSalary.Text = "0";
                if (txtDeductionPaidSalary.Text == "")
                    txtDeductionPaidSalary.Text = "0";
                if (txtDeductionRemainingSalary.Text == "")
                    txtDeductionRemainingSalary.Text = "0";
                if (txtPolicyBePaidAmount.Text == "")
                    txtPolicyBePaidAmount.Text = "0";
                if (txtPolicyPaidAmount.Text == "")
                    txtPolicyPaidAmount.Text = "0";
                if (txtPolicyRemaining.Text == "")
                    txtPolicyRemaining.Text = "0";
                if (txtDTotalBePaidAmount.Text == "")
                    txtDTotalBePaidAmount.Text = "0";
                if (txtDTotalSalary_Basic.Text == "")
                    txtDTotalSalary_Basic.Text = "0";
                if (txtDTotalRemaining.Text == "")
                    txtDTotalRemaining.Text = "0";
                if (txtTotalDeduction.Text == "")
                    txtTotalDeduction.Text = "0";
                if (txtNetPayment.Text == "")
                    txtNetPayment.Text = "0";
                if (txtOrderDate.Text != "")
                {
                    OrderDate = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy/MM/dd");
                }
                ds = objdb.ByProcedure("SpPayrollEmpArrear",
                    new string[] { "flag", "Office_ID", "Emp_ID", "CurrentYear", "CurrentMonth", "OrderNo", "OrderDate", "FromYear", "FromMonth", "FromMonthName", "ToYear", "ToMonth","ToMonthName","BasicSalary", "TotalEarning", "Policy", "TotalDeduction", "NetPaymentAmount", "Arrear_UpdatedBy", "Arrear_Type", "BasicSalaryPaid", "ArrearTitle" },
                    new string[] { "0", ViewState["Office_ID"].ToString(), EMP_ID, ddlCurrentYear.SelectedValue.ToString(), ddlCurrentMonth.SelectedValue.ToString(), txtOrderNo.Text, OrderDate, ddlFYear.SelectedValue.ToString(), ddlFMonth.SelectedValue.ToString(), ddlFMonth.SelectedItem.ToString(), ddlFYear.SelectedValue.ToString(), ddlFMonth.SelectedValue.ToString(), ddlFMonth.SelectedItem.ToString(), txtEarnBePaidSalary_Basic.Text, txtETotalRemaining.Text, txtPolicyRemaining.Text, txtDTotalRemaining.Text, txtNetPayment.Text, ViewState["Emp_ID"].ToString(), ddlArrearType.SelectedItem.Text, txtEarnPaidSalary_Basic.Text, txtRemark.Text }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    string EmpArrearID = ds.Tables[0].Rows[0]["EmpArrearID"].ToString();
                    string LoginUserID = ViewState["Emp_ID"].ToString();

                    foreach (RepeaterItem item in RepeaterEarning.Items)
                    {
                        Label lblEarnDeduction_ID = (Label)item.FindControl("lblEarnDeduction_ID");
                        Label lblEarnDeduction_Name = (Label)item.FindControl("lblEarnDeduction_Name");
                        TextBox txtToBePaidAmount = (TextBox)item.FindControl("txtToBePaidAmount");
                        TextBox txtPaidAmount = (TextBox)item.FindControl("txtPaidEarning");
                        TextBox txtRemainingAmount = (TextBox)item.FindControl("txtRemainingEarning");

                        if (txtToBePaidAmount.Text == "")
                            txtToBePaidAmount.Text = "0";
                        if (txtPaidAmount.Text == "")
                            txtPaidAmount.Text = "0";
                        if (txtRemainingAmount.Text == "")
                            txtRemainingAmount.Text = "0";

                        objdb.ByProcedure("SpPayrollEmpArrear",
                        new string[] { "flag", "EmpArrear_ID", "Emp_ID", "EarnDedution_ID", "EarnDeductionName", "RemainingAmount", "ToBePaidAmount", "PaidAmount", "EarnDeductionType", "ArrearEarnDeduction_UpdatedBy" },
                        new string[] { "1", EmpArrearID, EMP_ID, lblEarnDeduction_ID.Text, lblEarnDeduction_Name.Text, txtRemainingAmount.Text, txtToBePaidAmount.Text, txtPaidAmount.Text, "Earning", LoginUserID }, "dataset");

                    }
                    foreach (RepeaterItem item in RepeaterDeduction.Items)
                    {
                        Label lblEarnDeduction_ID = (Label)item.FindControl("lblEarnDeduction_ID");
                        Label lblEarnDeduction_Name = (Label)item.FindControl("lblEarnDeduction_Name");
                        TextBox txtToBePaidAmount = (TextBox)item.FindControl("txtToBePaidAmount");
                        TextBox txtPaidAmount = (TextBox)item.FindControl("txtPaidDeduction");
                        TextBox txtRemainingAmount = (TextBox)item.FindControl("txtRemainingDeduction");

                        if (txtToBePaidAmount.Text == "")
                            txtToBePaidAmount.Text = "0";
                        if (txtPaidAmount.Text == "")
                            txtPaidAmount.Text = "0";
                        if (txtRemainingAmount.Text == "")
                            txtRemainingAmount.Text = "0";

                        objdb.ByProcedure("SpPayrollEmpArrear",
                        new string[] { "flag", "EmpArrear_ID", "Emp_ID", "EarnDedution_ID", "EarnDeductionName", "RemainingAmount", "ToBePaidAmount", "PaidAmount", "EarnDeductionType", "ArrearEarnDeduction_UpdatedBy" },
                        new string[] { "1", EmpArrearID, EMP_ID, lblEarnDeduction_ID.Text, lblEarnDeduction_Name.Text, txtRemainingAmount.Text, txtToBePaidAmount.Text, txtPaidAmount.Text, "Deduction", LoginUserID }, "dataset");

                    }
                }
                ddlFYear.ClearSelection();
                ddlFMonth.ClearSelection();
                ClearText();
                FillArrear();
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
        // GridView1
        try
        {
            lblMsg.Text = "";
            ddlFYear.ClearSelection();
            ddlFMonth.ClearSelection();
            ddlCurrentYear.ClearSelection();
            ddlCurrentMonth.ClearSelection();
            ClearText();
            FillArrear();
            lblEmpName.Text = ddlEmployee.SelectedItem.Text;
            

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillArrear()
    {
        try
        {
            lblMsg.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ddlEmployee.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpPayrollEmpArrear", new string[] { "flag", "Emp_ID", }, new string[] { "2", ddlEmployee.SelectedValue.ToString(), }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();

                        GridView1.UseAccessibleHeader = true;
                        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                        GridView1.FooterRow.TableSection = TableRowSection.TableFooter; 
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}