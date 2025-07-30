using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mis_Payroll_Rpt_PayrollSetMonthOrHeadwiseChallanDetails : System.Web.UI.Page
{
    DataSet ds1, ds2, ds3, ds4, ds5 = new DataSet();
    APIProcedure objdb = new APIProcedure();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null)
        {
            if (!IsPostBack)
            {
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
    protected void FillDropdown()
    {
        try
        {
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
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
                            ddlEmployee.DataSource = ds1.Tables[0];
                            ddlEmployee.DataTextField = "Emp_Name";
                            ddlEmployee.DataValueField = "Emp_ID";
                            ddlEmployee.DataBind();
                            ddlEmployee.Items.Insert(0, new ListItem("All", "0"));
                        }
                    }
                }
                else
                {
                    ddlEmployee.Items.Insert(0, new ListItem("No Record Found", "-1"));
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

    protected void GetHeadWiseDetails()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Payroll_Trn_SetHeadOrEmpWiseChallanDetails_Get",
                    new string[] { "EarnDeduction_ID", "Office_ID", "SYear"
                                    , "SMonth", "Emp_ID" },
                    new string[] { ddlHead.SelectedValue, ddlOfficeName.SelectedValue.ToString(),ddlFinancialYear.SelectedValue
                                   ,ddlMonth.SelectedValue,ddlEmployee.SelectedValue}, "dataset");

            GridView1.DataSource = null;
            GridView1.DataBind();

            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        GridView1.Visible = true;
                        GridView1.DataSource = ds1.Tables[0];
                        GridView1.DataBind();

                    }
                }
            }
            else
            {
                ddlEmployee.Items.Insert(0, new ListItem("No Record Found", "-1"));
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

    protected void btnSearch_Click(object sender, EventArgs e)
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
                GetHeadWiseDetails();
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Loan Creation", ex.Message.ToString());
            }
        }
    }
    //private void GetDatatableHeaderDesign()
    //{
    //    try
    //    {
    //        if (GridView1.Rows.Count > 0)
    //        {
    //            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
    //            GridView1.UseAccessibleHeader = true;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
    //    }
    //}
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
    
    protected void lnkClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        GridView1.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();

    }
}