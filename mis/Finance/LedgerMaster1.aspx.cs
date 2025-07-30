using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_Finance_LedgerMaster : System.Web.UI.Page
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
                FillGrid();
                ds = objdb.ByProcedure("SpHrYear_Master",
                       new string[] { "flag" },
                       new string[] { "2" }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {
                    ddlFinancialYear.DataTextField = "Financial_Year";
                    ddlFinancialYear.DataValueField = "Financial_Year";
                    ddlFinancialYear.DataSource = ds;
                    ddlFinancialYear.DataBind();
                    ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
                }

                ds = objdb.ByProcedure("SpFinHeadMaster",
                       new string[] { "flag" },
                       new string[] { "1" }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {

                    ddlHeadName.DataTextField = "HeadName";
                    ddlHeadName.DataValueField = "Head_ID";
                    ddlHeadName.DataSource = ds;
                    ddlHeadName.DataBind();
                    ddlHeadName.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillGrid()
    {
        ds = objdb.ByProcedure("SpFinLedgerMaster",
              new string[] { "flag" },
              new string[] { "2" }, "dataset");

        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void ClearText()
    {
        ddlHeadName.ClearSelection();
        txtLedgerName.Text = "";
        ddlInventoryAffected.ClearSelection();
        txtMailing_Name.Text = "";
        txtMailing_Address.Text = "";
        txtMailing_PanNo.Text = "";
        ddlFinancialYear.ClearSelection();
        txtOpeningBalance.Text = "";
        ddlCRDRStatus.ClearSelection();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string Ledger_IsActive = "1";
            if (ddlHeadName.SelectedIndex == 0)
            {
                msg += "Select Head Name";
            }
            if (txtLedgerName.Text == "")
            {
                msg += "Enter Ledger Name";
            }
            if (ddlInventoryAffected.SelectedIndex == 0)
            {
                msg += "Select Inventory Value Are Affect";
            }
            if (txtMailing_Name.Text == "")
            {
                msg += "Enter Mailing Name";
            }
            if (txtMailing_Address.Text == "")
            {
                msg += "Enter Mailing Address";
            }
            if (txtMailing_PanNo.Text == "")
            {
                msg += "Enter PAN No";
            }
            if (ddlFinancialYear.SelectedIndex == 0)
            {
                msg += "Select Financial Year";
            }
            if (txtOpeningBalance.Text == "")
            {
                msg += "Enter Opening Balance";
            }
            if (ddlCRDRStatus.SelectedIndex == 0)
            {
                msg += "Select CR / DR Status";
            }
            if (msg.Trim() == "")
            {

                if (btnSave.Text == "Accept")
                {
                    objdb.ByProcedure("SpFinLedgerMaster",
                    new string[] { "flag", "Ledger_Name", "Ledger_HeadID", "InventoryAffected", "Mailing_Name", "Mailing_Address", "Pan_IT_No", "FinancialYear", "OpeningBalance", "CRDRStatus", "Ledger_IsActive", "Ledger_UpdatedBy" },
                    new string[] { "0", txtLedgerName.Text.Trim(), ddlHeadName.SelectedValue.ToString(), ddlInventoryAffected.SelectedItem.Text, txtMailing_Name.Text.Trim(), txtMailing_Address.Text.Trim(), txtMailing_PanNo.Text.Trim(), ddlFinancialYear.SelectedValue.ToString(), txtOpeningBalance.Text.Trim(), ddlCRDRStatus.SelectedItem.Text, Ledger_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillGrid();
                    ClearText();
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
    protected void ddlHeadName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlHeadName.SelectedIndex == 0)
            {
                FillGrid();
            }
            else
            {
                ds = objdb.ByProcedure("SpFinLedgerMaster",
              new string[] { "flag", "Ledger_HeadID" },
              new string[] { "3", ddlHeadName.SelectedValue.ToString() }, "dataset");

                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
}