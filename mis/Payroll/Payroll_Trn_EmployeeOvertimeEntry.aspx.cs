using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Payroll_Payroll_Trn_EmployeeOvertimeEntry : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds2, ds3, ds1, ds5, dsdetail;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (objdb.createdBy() != null && objdb.Office_ID() != null)
            {
                if (!IsPostBack)
                {
                    FillOffice();
                    FillEmployee();
                    FillEmployeeSearch();
                    txtWorkingDate.Attributes.Add("readonly", "true");
                    txtWorkingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtSearchWorkingDate.Attributes.Add("readonly", "true");
                    txtSearchWorkingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error ", ex.Message.ToString());
        }
    }

    protected void FillOffice()
    {
        try
        {
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlSearchOfficeName.Items.Insert(0, new ListItem("Select", "0"));
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

                        ddlSearchOfficeName.DataSource = ds2.Tables[0];
                        ddlSearchOfficeName.DataTextField = "Office_Name";
                        ddlSearchOfficeName.DataValueField = "Office_ID";
                        ddlSearchOfficeName.DataBind();
                    }
                }
            }
            ddlOfficeName.SelectedValue = objdb.Office_ID();
            if (ddlOfficeName.SelectedValue.ToString() != "1")
            {
                ddlOfficeName.Enabled = false;
            }

            ddlSearchOfficeName.SelectedValue = objdb.Office_ID();
            if (ddlSearchOfficeName.SelectedValue.ToString() != "1")
            {
                ddlSearchOfficeName.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }


    protected void FillEmployee()
    {
        try
        {
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));

            ds1 = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "40", ddlOfficeName.SelectedValue.ToString() }, "dataset");
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
                        ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
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

    protected void FillEmployeeSearch()
    {
        try
        {

            ds1 = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "40", ddlSearchOfficeName.SelectedValue.ToString() }, "dataset");
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
                    else
                    {
                        ddlSearchEmployeeName.Items.Insert(0, new ListItem("No Record Found", "0"));
                    }
                }
                else
                {
                    ddlSearchEmployeeName.Items.Insert(0, new ListItem("No Record Found", "0"));
                }
            }
            else
            {
                ddlSearchEmployeeName.Items.Insert(0, new ListItem("No Record Found", "0"));
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
    public void GetOverTimeEntryDetails()
    {
        try
        {

            GridView1.DataSource = null;
            GridView1.DataBind();
            lblMsg.Text = string.Empty;
            DateTime workingdate = DateTime.ParseExact(txtSearchWorkingDate.Text, "dd/MM/yyyy", culture);
            string workingdat = workingdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds1 = objdb.ByProcedure("USP_Payroll_Trn_EmpOverTimePayChild_GetAllByDate",
                   new string[] { "Emp_ID", "WorkingDate" },
                   new string[] { ddlSearchEmployeeName.SelectedValue, workingdat }, "dataset");

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
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error BindData", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    private void Clear()
    {

        txtWorkingHour.Text = string.Empty;
        btnSave.Text = "Save";
        GridView1.SelectedIndex = -1;
    }
    private void InsertOrUpdateEmployeeOvertime()
    {
        try
        {


            string newefdate = txtWorkingDate.Text;
            DateTime workingdate = DateTime.ParseExact(newefdate, "dd/MM/yyyy", culture);
            string workingdat = workingdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string[] stringmonthandyear = workingdat.Split('/');
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            if (btnSave.Text == "Save")
            {
                ds5 = objdb.ByProcedure("USP_Payroll_Trn_EmpOverTimePayInsert",
                          new string[] { "Emp_ID", "OverTimePayMonth", "OverTimePayYear",
                              "WorkingHour","WorkingDate", "CreatedBy", "CreatedByIP" },
                         new string[] { ddlEmployee.SelectedValue, stringmonthandyear[1],stringmonthandyear[0],
                              txtWorkingHour.Text.Trim(), workingdat, objdb.createdBy(), IPAddress },
                            "dataset");


                if (ds5.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    string success = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    Clear();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                }
                else if (ds5.Tables[0].Rows[0]["Msg"].ToString() == "already")
                {
                    string warning = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning!", warning);
                }
                else
                {
                    string error = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                }
            }
            else if (btnSave.Text == "Update" && hfrowid.Value != "")
            {
                ds5 = objdb.ByProcedure("USP_Payroll_Mst_OverTimePayRate_Update",
                          new string[] { "OverTimePayRateId", "PayRate", "workingdate", "CreatedBy", "CreatedByIP" },
                         new string[] { hfrowid.Value, txtWorkingHour.Text.Trim(), workingdat, objdb.createdBy(), IPAddress },
                            "dataset");

                if (ds5.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    string success = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    Clear();
                    GetOverTimeEntryDetails();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                }
                else if (ds5.Tables[0].Rows[0]["Msg"].ToString() == "already")
                {
                    string warning = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning!", warning);
                }
                else
                {
                    string error = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                }
            }
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error " + ex.Message.ToString());
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
            InsertOrUpdateEmployeeOvertime();
        }
    }
    protected void lnkbtnClear_Click(object sender, EventArgs e)
    {
        Clear();
        lblMsg.Text = string.Empty;
        GridView1.SelectedIndex = -1;
        ddlEmployee.SelectedIndex = 0;
        ddlEmployee.Enabled = true;
        hfrowid.Value = "";
        txtWorkingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordDelete")
            {

                lblMsg.Text = string.Empty;
                Control ctrl = e.CommandSource as Control;
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                    Label lblEmpOverTimePayId = (Label)row.FindControl("lblEmpOverTimePayId");
                    ds3 = objdb.ByProcedure("USP_Payroll_Trn_EmpOverTimePayChild_Delete",
                                new string[] { "EmpOverTimePaychildId", "EmpOverTimePayId"
                                    , "CreatedBy", "CreatedByIP" },
                                new string[] { e.CommandArgument.ToString(),lblEmpOverTimePayId.Text
                                    , objdb.createdBy(),IPAddress  }, "TableSave");

                    if (ds3.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        ds3.Dispose();
                        GetOverTimeEntryDetails();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    ds3.Clear();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOfficeName.SelectedValue != "0")
        {
            FillEmployee();
        }

    }
    protected void ddlSearchOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchOfficeName.SelectedValue != "0")
        {
            FillEmployeeSearch();
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
            GetOverTimeEntryDetails();
        }
    }
}