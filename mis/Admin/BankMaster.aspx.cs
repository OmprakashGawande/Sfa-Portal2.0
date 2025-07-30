using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_Masters_BankMaster : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Emp_ID"] = "1";
        Session["Office_ID"] = "2";
        lblMsg.Text = "";
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                GetBankDetails();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            //ClearText();
            string Bank_ID = Convert.ToString(e.CommandArgument.ToString());
            ViewState["Bank_ID"] = Bank_ID;
            ds = objdb.ByProcedure("sp_tblPUBankMaster", new string[] { "flag", "Bank_ID" }, new string[] { "8", Bank_ID }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtBankName.Text = ds.Tables[0].Rows[0]["BankName"].ToString();
                txtOrderNo.Text = ds.Tables[0].Rows[0]["OrderNo"].ToString();
                txtGroupNo.Text = ds.Tables[0].Rows[0]["GroupNo"].ToString();
                if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "True" || ds.Tables[0].Rows[0]["IsActive"].ToString() == "1")
                {
                    chkIsActive.Checked = true;
                }
                else
                {
                    chkIsActive.Checked = false;
                }
                btnSubmit.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetBankDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("sp_tblPUBankMaster",
                            new string[] { "flag" },
                            new string[] { "7" }, "dataset");
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void Clear()
    {
        txtBankName.Text = string.Empty;
        txtOrderNo.Text = string.Empty;
        txtGroupNo.Text = string.Empty;
        btnSubmit.Text = "Submit";
        ds = null;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string CreatedBy = "1";
            string Office_ID = "2";
            string CreatedBy_IP = "3";
            lblMsg.Text = "";
            string Isactive = "1";
            if (chkIsActive.Checked)
            {
                Isactive = "1";
            }
            else
            {
                Isactive = "0";
            }
            if (btnSubmit.Text == "Submit")
            {
                // To Activate when Session Comes
                //ds = objdb.ByProcedure("sp_tblPUBankMaster",
                //    new string[] { "flag", "BankName", "CreatedBy", "Office_ID", "CreatedBy_IP" },
                //    new string[] { "2", txtBankName.Text.Trim(), objdb.createdBy(), objdb.Office_ID(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "TableSave");
                ds = objdb.ByProcedure("sp_tblPUBankMaster",
                   new string[] { "flag", "BankName", "OrderNo", "GroupNo", "CreatedBy", "Office_ID", "CreatedBy_IP", "IsActive" },
                   new string[] { "2", txtBankName.Text.Trim(),txtOrderNo.Text,txtGroupNo.Text, CreatedBy, Office_ID, CreatedBy_IP, Isactive }, "TableSave");
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    //string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-success", "Thank You!", "Bank Saved Successfully");
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning!", "Bank Already Exist.");
                }
                ds.Clear();
            }
            if (btnSubmit.Text == "Update")
            {
                lblMsg.Text = "";
                ds = objdb.ByProcedure("sp_tblPUBankMaster",
                    new string[] { "flag", "Bank_id", "BankName", "OrderNo", "GroupNo", "CreatedBy", "IsActive" },
                    new string[] { "3", ViewState["Bank_ID"].ToString(), txtBankName.Text.Trim(), txtOrderNo.Text, txtGroupNo.Text, CreatedBy, Isactive }, "TableSave");
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    //string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-success", "Thank You!", "Record Updated Successfully");
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning!", "Bank Already Exist.");
                }
                ds.Clear();
            }
            Clear();
            GetBankDetails();
            ds = null;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableCell statusCell = e.Row.Cells[2];
            if (statusCell.Text == "1" || statusCell.Text == "True")
            {
                statusCell.Text = "Yes";
            }
            if (statusCell.Text == "0" || statusCell.Text == "False")
            {
                statusCell.Text = "No";
            }
        }
    }
}