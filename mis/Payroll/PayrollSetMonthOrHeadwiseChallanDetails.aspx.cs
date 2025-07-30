using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Payroll_PayrollSetMonthOrHeadwiseChallanDetails : System.Web.UI.Page
{
    DataSet ds1, ds2, ds3, ds4, ds5 = new DataSet();
    APIProcedure objdb = new APIProcedure();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null)
        {
            if (!IsPostBack)
            {
                txtTransactionDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtTransactionDate.Attributes.Add("readonly", "true");
                GetYear();
                GetMonth();
                FillDropdown();
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
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds2 = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds2 != null)
            {
                if (ds2.Tables.Count > 0)
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
                if (ds1 != null)
                {
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            GridView1.DataSource = ds1.Tables[0];
                            GridView1.DataBind();
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
            if (ds1 != null) { ds1.Dispose(); }
        }
    }

    private void GetLoanHead()
    {
        try
        {
            ds3 = objdb.ByProcedure("SpHRPayrollEarnDedMaster", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds3 != null)
            {
                if (ds3.Tables.Count > 0)
                {
                    if (ds3.Tables[0].Rows.Count > 0)
                    {

                        ddlHead.DataSource = ds3.Tables[0];
                        ddlHead.DataTextField = "EarnDeduction_Name";
                        ddlHead.DataValueField = "EarnDeduction_ID";
                        ddlHead.DataBind();
                        ddlHead.Items.Insert(0, new ListItem("Select", "0"));


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

        ddlMonth.SelectedIndex = 0;
        txtBSRCONo.Text = string.Empty;
        txtChallanNo.Text = string.Empty;
        ddlFinancialYear.SelectedIndex = 0;
        btnSave.Text = "Save";
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox chkbox = (CheckBox)row.FindControl("chkSelect");
            if (chkbox.Checked == true)
            {
                chkbox.Checked =false;
            }
        }
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
                    DataTable dtInsertEmpDetails = new DataTable();
                    DataRow drIC;
                    dtInsertEmpDetails.Columns.Add("MilkOrProductDemandId", typeof(int));
                    drIC = dtInsertEmpDetails.NewRow();
                    foreach(GridViewRow row in GridView1.Rows)
                    {
                        CheckBox chkbox = (CheckBox)row.FindControl("chkSelect");
                        if(chkbox.Checked==true)
                        {
                            Label lblempid = (Label)row.FindControl("lblEmp_ID");
                             drIC[0] = lblempid.Text;
                             dtInsertEmpDetails.Rows.Add(drIC.ItemArray);
                        }
                    }
                    if (dtInsertEmpDetails!=null)
                    {
                        if(dtInsertEmpDetails.Rows.Count>0)
                        {
                            DateTime transdate = DateTime.ParseExact(txtTransactionDate.Text, "dd/MM/yyyy", culture);
                            string transdat = transdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                            lblMsg.Text = "";
                            ds4 = objdb.ByProcedure("USP_Payroll_Trn_SetHeadOrEmpWiseChallanDetails_Insert",
                                new string[] { "EarnDeduction_ID","SYear","SMonth","TransactionDate", "ChallanNo"
                                , "BSRCONo","CreatedBy", "CreatedByIP" },
                                new string[] { ddlHead.SelectedValue,ddlFinancialYear.SelectedValue,ddlMonth.SelectedValue,
                                   transdat,txtChallanNo.Text.Trim() ,txtBSRCONo.Text.Trim()
                            ,objdb.createdBy(),IPAddress },
                                new string[] { "Type_Payroll_Trn_SetHeadOrEmpWiseChallanDetails" },
                                new DataTable[] { dtInsertEmpDetails }
                               , "TableSave");

                            if (ds4.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                                Clear();
                                dtInsertEmpDetails.Dispose();
                            }
                            else
                            {
                                string error = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                    }                   

                    ds4.Dispose();

                    }
               
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Enter Head");
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
        ddlHead.SelectedIndex = 0;

    }
}