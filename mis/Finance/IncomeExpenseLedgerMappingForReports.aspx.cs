using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Collections.Generic;
using System.Web.UI;

public partial class mis_Finance_IncomeExpenseLedgerMappingForReports : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    APIProcedure api = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {

                if (!IsPostBack)
                {
                    ViewState["OfficeType_Title"] = Session["OfficeType_Title"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
					ViewState["Mapping_ID"] = "0";
                    FillHead();
                    FillLedger();
                    FillGrid();
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillHead()
    {
        try
        {

            ds = objdb.ByProcedure("Usp_FinLedgerMappedForIncExpRpt",
                         new string[] { "flag"},
                         new string[] { "5" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlHeadName.DataTextField = "Head_Name";
                ddlHeadName.DataValueField = "Head_ID";
                ddlHeadName.DataSource = ds;
                ddlHeadName.DataBind();
                ddlHeadName.Items.Insert(0, new ListItem("Select", "0"));

                ddlHead_flt.DataTextField = "Head_Name";
                ddlHead_flt.DataValueField = "Head_ID";
                ddlHead_flt.DataSource = ds;
                ddlHead_flt.DataBind();
                ddlHead_flt.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillLedger()
    {
        try
        {

            ds = objdb.ByProcedure("Usp_FinLedgerMappedForIncExpRpt",
              new string[] { "flag" },
              new string[] { "6"}, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlLedger_ID.DataSource = ds;
                ddlLedger_ID.DataTextField = "Ledger_Name";
                ddlLedger_ID.DataValueField = "Ledger_ID";
                ddlLedger_ID.DataBind();
                ddlLedger_ID.Items.Insert(0, "Select");

            }
            else
            {

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
            gvMappedLedgerData.DataSource = new string[]{};
            gvMappedLedgerData.DataBind();
            ds = objdb.ByProcedure("Usp_FinLedgerMappedForIncExpRpt", new string[] { "flag", "Head_ID", "Office_ID" }, new string[] { "2", ddlHead_flt.SelectedValue, ViewState["Office_ID"].ToString() }, "dataset");
            if(ds != null)
            {
                if(ds.Tables.Count > 0)
                {
                    if(ds.Tables[0].Rows.Count > 0)
                    {
                        gvMappedLedgerData.DataSource = ds;
                        gvMappedLedgerData.DataBind();
                    }
                }
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
            if(btnSave.Text == "Save")
            {
                ds = objdb.ByProcedure("Usp_FinLedgerMappedForIncExpRpt",
                                 new string[] { "flag", 
                                                  "ReportName", 
                                                  "Head_ID", 
                                                  "Ledger_ID", 
                                                  "Office_ID", 
                                                  "CreatedBy", 
                                                  "CreatedByIP" },
                                 new string[] { "1", 
                                                 ddlReportName.SelectedItem.Text, 
                                                 ddlHeadName.SelectedValue, 
                                                 ddlLedger_ID.SelectedValue, 
                                                 ViewState["Office_ID"].ToString(),
                                                 ViewState["Emp_ID"].ToString(),
                                                 api.GetLocalIPAddress()
                                                }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                        }
                    }
                }
            }
            else if(btnSave.Text == "Update")
            {
                ds = objdb.ByProcedure("Usp_FinLedgerMappedForIncExpRpt",
                                    new string[] { "flag", 
                                                  "Mapping_ID",
                                                  "ReportName", 
                                                  "Head_ID", 
                                                  "Ledger_ID", 
                                                  "Office_ID", 
                                                  "CreatedBy", 
                                                  "CreatedByIP" },
                                    new string[] { "3", 
                                                 ViewState["Mapping_ID"].ToString(),
                                                 ddlReportName.SelectedItem.Text, 
                                                 ddlHeadName.SelectedValue, 
                                                 ddlLedger_ID.SelectedValue, 
                                                 ViewState["Office_ID"].ToString(),
                                                 ViewState["Emp_ID"].ToString(),
                                                 api.GetLocalIPAddress()
                                                }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                        }
                    }
                }

            }
            btnSave.Text = "Save";
			ViewState["Mapping_ID"] = "0";
            //ddlReportName.ClearSelection();
            ddlLedger_ID.ClearSelection();
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvMappedLedgerData_RowCommand(object sender, GridViewCommandEventArgs e) 
    {
        try
        {
            string Mapping_ID = e.CommandArgument.ToString();
            ViewState["Mapping_ID"] = Mapping_ID.ToString();
            if(e.CommandName =="EditRecord")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

                Label lblReportName = (Label)row.FindControl("lblReportName");
                Label lblHead_ID = (Label)row.FindControl("lblHead_ID");
                Label lblLedger_ID = (Label)row.FindControl("lblLedger_ID");

                ddlReportName.ClearSelection();
                ddlReportName.Items.FindByText(lblReportName.Text).Selected = true;

                ddlHeadName.ClearSelection();
                ddlHeadName.Items.FindByValue(lblHead_ID.Text).Selected = true;

                ddlLedger_ID.ClearSelection();
                ddlLedger_ID.Items.FindByValue(lblLedger_ID.Text).Selected = true;
                btnSave.Text = "Update";
            }
            if(e.CommandName =="DeleteRecord")
            {
                objdb.ByProcedure("Usp_FinLedgerMappedForIncExpRpt", 
                                  new string[] { "flag",
                                                 "Mapping_ID",
                                                 "CreatedBy", 
                                                 "CreatedByIP" },
                                 new string[] {"4",
                                               Mapping_ID.ToString(),
                                               ViewState["Emp_ID"].ToString(), 
                                               api.GetLocalIPAddress()                             
                                              }, "dataset");

                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", "Record Deleted Successfully");
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlHead_flt_SelectedIndexChanged(object sender, EventArgs e)
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
}