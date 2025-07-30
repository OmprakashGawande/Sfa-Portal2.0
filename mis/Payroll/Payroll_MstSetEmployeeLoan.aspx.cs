using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mis_Payroll_Payroll_MstSetEmployeeLoan : System.Web.UI.Page
{
    DataSet ds1,ds2,ds3,ds4,ds5 =new DataSet();
    APIProcedure objdb = new APIProcedure();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy()!=null)
        {
            if (!IsPostBack)
            {
                GetYear();
                GetMonth();
                FillDropdown();
                FillDropdownSearch();
                GetLoanHead();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void FillDropdown()
    {
        try
        {
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            ds2 = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds2!=null)
            {
                if (ds2.Tables.Count>0)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {

                        ddlOfficeName.DataSource = ds2.Tables[0];
                        ddlOfficeName.DataTextField = "Office_Name";
                        ddlOfficeName.DataValueField = "Office_ID";
                        ddlOfficeName.DataBind();
                        ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));

                        ddlOfficeName.SelectedValue = objdb.Office_ID();
                        if (ddlOfficeName.SelectedIndex > 0)
                        {
                            FillEmployee();
                        }
                    }
                }
            }
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
            if (ds2 != null) { ds2.Dispose(); }
            if (ddlOfficeName.SelectedIndex > 0)
            {
                ds1 = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ddlOfficeName.SelectedValue.ToString() }, "dataset");
                if (ds1!=null)
                {
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            ddlEmployee.DataSource = ds1.Tables[0];
                            ddlEmployee.DataTextField = "Emp_Name";
                            ddlEmployee.DataValueField = "Emp_ID";
                            ddlEmployee.DataBind();
                            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1!= null) { ds1.Dispose(); }
        }
    }

    protected void FillDropdownSearch()
    {
        try
        {
            ddlSearchOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlSearchEmployeeName.Items.Insert(0, new ListItem("Select", "0"));
            ds2 = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds2 != null)
            {
                if (ds2.Tables.Count > 0)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {

                        ddlSearchOfficeName.DataSource = ds2.Tables[0];
                        ddlSearchOfficeName.DataTextField = "Office_Name";
                        ddlSearchOfficeName.DataValueField = "Office_ID";
                        ddlSearchOfficeName.DataBind();
                        ddlSearchOfficeName.Items.Insert(0, new ListItem("Select", "0"));

                        ddlSearchOfficeName.SelectedValue = objdb.Office_ID();
                        if (ddlSearchOfficeName.SelectedIndex > 0)
                        {
                            FillEmployeeSearch();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEmployeeSearch()
    {
        try
        {
            if (ds2 != null) { ds2.Dispose(); }
            if (ddlSearchOfficeName.SelectedIndex > 0)
            {
                ds1 = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ddlSearchOfficeName.SelectedValue.ToString() }, "dataset");
                if (ds1 != null)
                {
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            ddlSearchEmployeeName.DataSource = ds1.Tables[0];
                            ddlSearchEmployeeName.DataTextField = "Emp_Name";
                            ddlSearchEmployeeName.DataValueField = "Emp_ID";
                            ddlSearchEmployeeName.DataBind();
                            ddlSearchEmployeeName.Items.Insert(0, new ListItem("All", "0"));
                        }
                    }
                }
                else
                {
                    ddlSearchEmployeeName.Items.Insert(0, new ListItem("No Record Fount", "-1"));
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }

    private void GetLoanHead()
    {
        try
        {
            ds3 = objdb.ByProcedure("SpHRPayrollEarnDedMaster", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds3 != null)
            {
                if (ds3.Tables.Count > 0)
                {
                    if (ds3.Tables[0].Rows.Count > 0)
                    {

                        ddlLoanHead.DataSource = ds3.Tables[0];
                        ddlLoanHead.DataTextField = "EarnDeduction_Name";
                        ddlLoanHead.DataValueField = "EarnDeduction_ID";
                        ddlLoanHead.DataBind();
                        ddlLoanHead.Items.Insert(0, new ListItem("Select", "0"));

                     
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }

    private void GetYear()
    {
        try
        {
            ds3 = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "8" }, "dataset");
            {
                if (ds3.Tables.Count > 0)
                {
                    if (ds3.Tables[0].Rows.Count > 0)
                    {

                        ddlFinancialYear.DataSource = ds3.Tables[0];
                        ddlFinancialYear.DataTextField = "Year";
                        ddlFinancialYear.DataValueField = "Year";
                        ddlFinancialYear.DataBind();
                        ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));


                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }
    private void Clear()
    {
        txtInterestAmount.Text = string.Empty;
        txtInstallmentNo.Text = string.Empty;
        txtInstallmentAmount.Text = string.Empty;
        txtLoanAmount.Text = string.Empty;
        ddlFinancialYear.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        ddlFinancialYear.SelectedIndex = 0;
        ddlEmployee.SelectedIndex = 0;
        txtInstallmentAmount.Attributes.Remove("readonly");
        txtInstallmentNo.Attributes.Remove("readonly");
        txtLoanAmount.Attributes.Remove("readonly");
        ddlLoanHead.Enabled = true;
        ddlMonth.Enabled = true;
        ddlOfficeName.Enabled = true;
        ddlFinancialYear.Enabled = true;
        ddlEmployee.Enabled = true;
        GridView1.SelectedIndex = -1;
        btnSave.Text = "Save";
    }

    private void GetMonth()
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_tblMonthMaster_SetEmployeeLoan", new string[] { }, new string[] { }, "dataset");
            if (ds3 != null)
            {
                if (ds3.Tables.Count > 0)
                {
                    if (ds3.Tables[0].Rows.Count > 0)
                    {

                        ddlMonth.DataSource = ds3.Tables[0];
                        ddlMonth.DataTextField = "MonthName";
                        ddlMonth.DataValueField = "MonthID";
                        ddlMonth.DataBind();
                        ddlMonth.Items.Insert(0, new ListItem("Select", "0"));


                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }

    private void InsertOrUpdateRecord()
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                if (btnSave.Text == "Save")
                {
                    lblMsg.Text = "";
                    string interestamount = "";
                    if(txtInterestAmount.Text.Trim()=="" || txtInterestAmount.Text.Trim()==string.Empty)
                    {
                        interestamount = "0";
                    }
                    else
                    {
                        interestamount = txtInterestAmount.Text.Trim();
                    }
                  
                    ds4 = objdb.ByProcedure("USP_Payroll_EmployeeLoan_Insert",
                        new string[] { "Emp_ID","EarnDeduction_ID","LoanAmount","IntallmentAmount", "IntallmentNo"
                                , "InterestAmount","LoanDeductionFromYear", "LoanDeductionFromMonth", "LoanStatus",
                                "CreatedBy", "CreatedByIP" },
                        new string[] { ddlEmployee.SelectedValue,ddlLoanHead.SelectedValue,txtLoanAmount.Text.Trim()
                            ,txtInstallmentAmount.Text.Trim(),txtInstallmentNo.Text.Trim(),interestamount,
                            ddlFinancialYear.SelectedValue,ddlMonth.SelectedValue,"1"
                            ,objdb.createdBy(),IPAddress
                        }, "TableSave");

                    if (ds4.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {

                        string success = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        Clear();
                    }
                    else
                    {
                        string error = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        if (ds4.Tables[0].Rows[0]["Msg"].ToString() == "already")
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                    }
                    ds4.Dispose();
                }
                else if (btnSave.Text == "Update" && rowid.Value!="")
                {
                    lblMsg.Text = "";
                    string interestamount = "";
                    if (txtInterestAmount.Text.Trim() == "" || txtInterestAmount.Text.Trim() == string.Empty)
                    {
                        interestamount = "0";
                    }
                    else
                    {
                        interestamount = txtInterestAmount.Text.Trim();
                    }
                    ds4 = objdb.ByProcedure("USP_Payroll_EmployeeLoan_UpdateInterest",
                        new string[] { "EmployeeLoanId","InterestAmount","CreatedBy", "CreatedByIP" },
                        new string[] { rowid.Value,interestamount,objdb.createdBy(),IPAddress
                        }, "TableSave");

                    if (ds4.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        Clear();
                        SearchEmployeeDetails();
                    }
                    else
                    {
                        string error = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        if (ds4.Tables[0].Rows[0]["Msg"].ToString() == "No")
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                    }
                    ds4.Dispose();
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
              
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Enter EmployeeName");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }

    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillEmployee();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlSearchOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillEmployeeSearch();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Page.Validate("a");

        if (!Page.IsValid)
        {
            return;
        }
        else
        {
            try
            {
                InsertOrUpdateRecord();
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Loan Creation", ex.Message.ToString());
            }
        }
    }
    protected void lnkClear_Click(object sender, EventArgs e)
    {
        Clear();
        lblMsg.Text = "";
        ddlLoanHead.SelectedIndex = 0;
        
    }
    private void SearchEmployeeDetails()
    {
        try
        {
            ds5 = objdb.ByProcedure("USP_Payroll_EmployeeLoan_GetOfficeOrEmpWise",
                new string[] { "Office_ID", "Emp_ID" }, 
                new string[] { ddlSearchOfficeName.SelectedValue,ddlSearchEmployeeName.SelectedValue }, "dataset");
            GridView1.DataSource = null;
            GridView1.DataBind();
            if(ds5!=null)
            {
                if(ds5.Tables.Count>0)
                {
                    if(ds5.Tables[0].Rows.Count>0)
                    {
                        GridView1.DataSource = ds5.Tables[0];
                        GridView1.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Loan Creation", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null)
            { ds5.Dispose(); }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Page.Validate("b");

        if (!Page.IsValid)
        {
            return;
        }
        else
        {
            try
            {
                SearchEmployeeDetails();
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Loan Search", ex.Message.ToString());
            }
        }
      
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = string.Empty;
        if (e.CommandName == "RecordEdit")
        {
            Control ctrl = e.CommandSource as Control;
            if (ctrl != null)
            {
                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                Label lblOffice_ID = (Label)row.FindControl("lblOffice_ID");
                Label lblEmp_ID = (Label)row.FindControl("lblEmp_ID");
                Label lblEarnDeduction_ID = (Label)row.FindControl("lblEarnDeduction_ID");
                Label lblLoanAmount = (Label)row.FindControl("lblLoanAmount");
                Label lblLoanDeductionFromYear = (Label)row.FindControl("lblLoanDeductionFromYear");
                Label lblLoanDeductionFromMonth = (Label)row.FindControl("lblLoanDeductionFromMonth");
                Label lblIntallmentAmount = (Label)row.FindControl("lblIntallmentAmount");
                Label lblIntallmentNo = (Label)row.FindControl("lblIntallmentNo");
                Label lblInterestAmount = (Label)row.FindControl("lblInterestAmount");
                ddlOfficeName.SelectedValue = lblOffice_ID.Text;
                FillEmployee();
                ddlEmployee.SelectedValue = lblEmp_ID.Text;
                ddlLoanHead.SelectedValue = lblEarnDeduction_ID.Text;
                txtLoanAmount.Text = lblLoanAmount.Text;
                txtInstallmentAmount.Text = lblIntallmentAmount.Text;
                txtInstallmentNo.Text = lblIntallmentNo.Text;
                txtInterestAmount.Text = lblInterestAmount.Text;
                ddlFinancialYear.SelectedValue = lblLoanDeductionFromYear.Text;
                ddlMonth.SelectedValue = lblLoanDeductionFromMonth.Text;

                txtInstallmentAmount.Attributes.Add("readonly","true");
                txtInstallmentNo.Attributes.Add("readonly","true");
                txtLoanAmount.Attributes.Add("readonly","true");
                ddlLoanHead.Enabled = false;
                ddlMonth.Enabled = false;
                ddlOfficeName.Enabled = false;
                ddlFinancialYear.Enabled = false;
                ddlEmployee.Enabled = false;
                btnSave.Text = "Update";
                txtInterestAmount.Focus();
                rowid.Value = e.CommandArgument.ToString();
                foreach (GridViewRow gvRow in GridView1.Rows)
                {
                    if (GridView1.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                    {
                        GridView1.SelectedIndex = gvRow.DataItemIndex;
                        GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                        break;
                    }
                }
               
            }
        }
        if (e.CommandName == "RecordDelete")
        {
            Control ctrl = e.CommandSource as Control;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            if (ctrl != null)
            {
                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
              
                Label lblEmp_ID = (Label)row.FindControl("lblEmp_ID");
                Label lblEarnDeduction_ID = (Label)row.FindControl("lblEarnDeduction_ID");
                Label lblLoanDeductionFromYear = (Label)row.FindControl("lblLoanDeductionFromYear");

                ds2 = objdb.ByProcedure("USP_Payroll_EmployeeLoan_Delete",
                       new string[] { "EmployeeLoanId","Emp_ID"
                           ,"DeductionYear", "EarnDeduction_ID","CreatedBy", "CreatedByIP" },
                       new string[] { e.CommandArgument.ToString(), lblEmp_ID.Text,lblLoanDeductionFromYear.Text
                           , lblEarnDeduction_ID.Text, objdb.createdBy(), IPAddress }, "TableSave");

                if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Success !", success);
                    if (ds2 != null) { ds2.Dispose(); }
                    SearchEmployeeDetails();
                    txtInterestAmount.Focus();
                }
                else
                {
                    string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "already")
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                        txtInterestAmount.Focus();
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                }

            }
        }
    }
}