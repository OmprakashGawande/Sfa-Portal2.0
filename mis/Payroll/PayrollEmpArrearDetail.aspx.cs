using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollEmpArrearDetail : System.Web.UI.Page
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
            ddlFYear.Items.Insert(0, new ListItem("Select", "0"));
            ddlTYear.Items.Insert(0, new ListItem("Select", "0"));

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
				 ddlFYear.Items.Insert(1, new ListItem("2006", "2006"));
                ddlFYear.Items.Insert(2, new ListItem("2007", "2007"));
                ddlFYear.Items.Insert(3, new ListItem("2008", "2008"));
                ddlFYear.Items.Insert(4, new ListItem("2009", "2009"));
                ddlFYear.Items.Insert(5, new ListItem("2010", "2010"));
                ddlFYear.Items.Insert(6, new ListItem("2011", "2011"));
                ddlFYear.Items.Insert(7, new ListItem("2012", "2012"));
                ddlFYear.Items.Insert(8, new ListItem("2013", "2013"));
                ddlFYear.Items.Insert(9, new ListItem("2014", "2014"));
                ddlFYear.Items.Insert(10, new ListItem("2015", "2015"));
                ddlFYear.Items.Insert(11, new ListItem("2016", "2016"));
                // ==== To Year ====
                ddlTYear.DataSource = ds;
                ddlTYear.DataTextField = "Year";
                ddlTYear.DataValueField = "Year";
                ddlTYear.DataBind();
                ddlTYear.Items.Insert(0, new ListItem("Select", "0"));
				  ddlTYear.Items.Insert(1, new ListItem("2006", "2006"));
                ddlTYear.Items.Insert(2, new ListItem("2007", "2007"));
                ddlTYear.Items.Insert(3, new ListItem("2008", "2008"));
                ddlTYear.Items.Insert(4, new ListItem("2009", "2009"));
                ddlTYear.Items.Insert(5, new ListItem("2010", "2010"));
                ddlTYear.Items.Insert(6, new ListItem("2011", "2011"));
                ddlTYear.Items.Insert(7, new ListItem("2012", "2012"));
                ddlTYear.Items.Insert(8, new ListItem("2013", "2013"));
                ddlTYear.Items.Insert(9, new ListItem("2014", "2014"));
                ddlTYear.Items.Insert(10, new ListItem("2015", "2015"));
                ddlTYear.Items.Insert(11, new ListItem("2016", "2016"));
                // ==== Month ====
                //ddlYear.DataSource = ds;
                //ddlYear.DataTextField = "Year";
                //ddlYear.DataValueField = "Year";
                //ddlYear.DataBind();
                //ddlYear.Items.Insert(0, new ListItem("Select", "0"));
                // ==== Search Year ====
                ddlSearchYear.DataSource = ds;
                ddlSearchYear.DataTextField = "Year";
                ddlSearchYear.DataValueField = "Year";
                ddlSearchYear.DataBind();
                ddlSearchYear.Items.Insert(0, new ListItem("Select", "0"));
            }

            ds = null;
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "10" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds.Tables[0];
                ddlYear.DataTextField = "Year_Name";
                ddlYear.DataValueField = "Year_ID";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));


                ddlMonth.DataSource = ds.Tables[1];
                ddlMonth.DataTextField = "Month_Name";
                ddlMonth.DataValueField = "Month_ID";
                ddlMonth.DataBind();
                ddlMonth.Items.Insert(0, new ListItem("Select", "0"));


                

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
            ds = objdb.ByProcedure("SpPayrollEmpArrear", new string[] { "flag", "EarnDeduction_Year" },
                new string[] { "8", ddlSearchYear.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                DivDetail.Visible = true;

                RepeaterEarning.DataSource = ds.Tables[0];
                RepeaterEarning.DataBind();

                RepeaterDeduction.DataSource = ds.Tables[1];
                RepeaterDeduction.DataBind();
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
    protected void ClearText()
    {
        try
        {
            DivDetail.Visible = false;
            txtTotalEarning.Text = "0";
            txtTotalDeduction.Text = "0";
            txtNetPayment.Text = "0";
            txtEarnRemainingSalary_Basic.Text = "0";
            txtETotalRemaining.Text = "0";
            //txtDeductionPaidSalary.Text = "0";
            //txtDeductionRemainingSalary.Text = "0";
            txtPolicyRemaining.Text = "0";
            // txtDTotalRemaining.Text = "";
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

        //txtEarnRemainingSalary_Basic.Attributes.Add("readonly", "readonly");
        txtETotalRemaining.Attributes.Add("readonly", "readonly");

        //txtDeductionPaidSalary.Attributes.Add("readonly", "readonly");
        //txtDeductionRemainingSalary.Attributes.Add("readonly", "readonly");
        //txtPolicyRemaining.Attributes.Add("readonly", "readonly");
        txtDTotalRemaining.Attributes.Add("readonly", "readonly");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            lblMsg.Text = "";
            string OrderDate = "";
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
            if (ddlTYear.SelectedIndex == 0)
            {
                msg += "Select To Year. \\n";
            }
            if (ddlTMonth.SelectedIndex == 0)
            {
                msg += "Select To Month. \\n";
            }

            if (msg.Trim() == "")
            {
                string EMP_ID = ddlEmployee.SelectedValue.ToString();
                // ===Earning===
                if (txtEarnRemainingSalary_Basic.Text == "")
                    txtEarnRemainingSalary_Basic.Text = "0";
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

                if (txtOrderDate.Text != "")
                {
                    OrderDate = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy/MM/dd");
                }
                else
                {
                    OrderDate = "";
                }

                string ArrearType = "";
                if(chkFullSalary.Checked==true){
                    ArrearType = "Salary";
                }

                ds = objdb.ByProcedure("SpPayrollEmpArrear",
                    new string[] { "flag", "Office_ID", "Emp_ID", "CurrentYear", "CurrentMonth", "OrderNo", "OrderDate", "FromYear", "FromMonth", "FromMonthName", "ToYear", "ToMonth", "ToMonthName", "BasicSalary", "TotalEarning", "Policy", "TotalDeduction", "NetPaymentAmount", "Arrear_UpdatedBy", "Arrear_Type" },
                    new string[] { "0", ddlOfficeName.SelectedValue.ToString(), EMP_ID, ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), txtOrderNo.Text,OrderDate, ddlFYear.SelectedValue.ToString(), ddlFMonth.SelectedValue.ToString(),ddlFMonth.SelectedItem.ToString(), ddlTYear.SelectedValue.ToString(), ddlTMonth.SelectedValue.ToString(),ddlTMonth.SelectedItem.ToString()
                        ,txtEarnRemainingSalary_Basic.Text, txtETotalRemaining.Text, txtPolicyRemaining.Text, txtDTotalRemaining.Text, txtNetPayment.Text, ViewState["Emp_ID"].ToString(),ArrearType.ToString() }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    string Arrear_ID = ds.Tables[0].Rows[0]["EmpArrearID"].ToString();
                    string LoginUserID = ViewState["Emp_ID"].ToString();

                    foreach (RepeaterItem item in RepeaterEarning.Items)
                    {
                        Label lblEarnDeduction_ID = (Label)item.FindControl("lblEarnDeduction_ID");
                        Label lblEarnDeduction_Name = (Label)item.FindControl("lblEarnDeduction_Name");
                        //TextBox txtPaidAmount = (TextBox)item.FindControl("txtPaidEarning");
                        TextBox txtRemainingAmount = (TextBox)item.FindControl("txtRemainingEarning");

                        //if (txtPaidAmount.Text == "")
                        //    txtPaidAmount.Text = "0";
                        if (txtRemainingAmount.Text == "")
                            txtRemainingAmount.Text = "0";

                        objdb.ByProcedure("SpPayrollEmpArrear",
                        new string[] { "flag", "EmpArrear_ID", "Emp_ID", "EarnDedution_ID", "EarnDeductionName", "RemainingAmount", "EarnDeductionType", "ArrearEarnDeduction_UpdatedBy" },
                        new string[] { "1", Arrear_ID, EMP_ID, lblEarnDeduction_ID.Text, lblEarnDeduction_Name.Text, txtRemainingAmount.Text, "Earning", LoginUserID }, "dataset");

                    }
                    foreach (RepeaterItem item in RepeaterDeduction.Items)
                    {
                        Label lblEarnDeduction_ID = (Label)item.FindControl("lblEarnDeduction_ID");
                        Label lblEarnDeduction_Name = (Label)item.FindControl("lblEarnDeduction_Name");
                        //TextBox txtPaidAmount = (TextBox)item.FindControl("txtPaidDeduction");
                        TextBox txtRemainingAmount = (TextBox)item.FindControl("txtRemainingDeduction");
                        //if (txtPaidAmount.Text == "")
                        //    txtPaidAmount.Text = "0";
                        if (txtRemainingAmount.Text == "")
                            txtRemainingAmount.Text = "0";

                        objdb.ByProcedure("SpPayrollEmpArrear",
                        new string[] { "flag", "EmpArrear_ID", "Emp_ID", "EarnDedution_ID", "EarnDeductionName", "RemainingAmount", "EarnDeductionType", "ArrearEarnDeduction_UpdatedBy" },
                        new string[] { "1", Arrear_ID, EMP_ID, lblEarnDeduction_ID.Text, lblEarnDeduction_Name.Text, txtRemainingAmount.Text, "Deduction", LoginUserID }, "dataset");

                    }
                }
                ddlFYear.ClearSelection();
                ddlFMonth.ClearSelection();
                ddlTYear.ClearSelection();
                ddlTMonth.ClearSelection();
                ddlYear.ClearSelection();
                ddlMonth.ClearSelection();
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
        try
        {
            lblMsg.Text = "";
            ddlFYear.ClearSelection();
            ddlFMonth.ClearSelection();
            ddlTYear.ClearSelection();
            ddlTMonth.ClearSelection();
            ClearText();
            FillArrear();
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
            if (ddlSearchYear.SelectedIndex == 0)
            {
                msg += "Select Year. \\n";
            }
            if (msg.Trim() == "")
            {
                FillGrid();
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
    protected void FillArrear()
    {
        try
        {

            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ddlEmployee.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpPayrollEmpArrear", new string[] { "flag", "Emp_ID", }, new string[] { "2", ddlEmployee.SelectedValue.ToString(), }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string EmpArrearID = GridView1.SelectedDataKey.Value.ToString();
    //        string EmpID = ddlEmployee.SelectedValue.ToString();
    //        string QueryString  = "EmployeeWiseEarnDedDetail.aspx?EmpArrearID=" + objdb.Encrypt(EmpArrearID) + "&EmpID=" + objdb.Encrypt(EmpID);
    //        Response.Write("<script>");
    //        Response.Write("window.open('"+ QueryString +"','_blank')");
    //        Response.Write("</script>");
    //      //  Response.Redirect("EmployeeWiseEarnDedDetail.aspx?EmpArrearID=" + objdb.Encrypt(EmpArrearID) + "&EmpID=" + objdb.Encrypt(EmpID));
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
}