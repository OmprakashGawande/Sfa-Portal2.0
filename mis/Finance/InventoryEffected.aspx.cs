using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Finance_InventoryEffected : System.Web.UI.Page
{
    DataSet ds, ds2;
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
                Session["Page"] = Server.UrlEncode(System.DateTime.Now.ToString());
                DivDetail.Visible = false;
                FillDropdown();
                FillLedgerDropdown();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

        ViewState["UPage"] = Session["Page"];
    }
    protected void FillDropdown()
    {
        try
        {
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));

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
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillLedgerDropdown()
    {
        try
        {
            ddlLedgerName.Items.Insert(0, new ListItem("Select", "0"));

            ds = objdb.ByProcedure("SpFinInventoryEffected", new string[] { "flag", "Office_ID" }, new string[] { "0", ddlOfficeName.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlLedgerName.DataSource = ds;
                ddlLedgerName.DataTextField = "LedgerName";
                ddlLedgerName.DataValueField = "Ledger_ID";
                ddlLedgerName.DataBind();
                ddlLedgerName.Items.Insert(0, new ListItem("Select", "0"));
            }
            //ddlLedgerName.SelectedValue = ViewState["Ledger_ID"].ToString();

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
            DivDetail.Visible = false;
            ds = objdb.ByProcedure("SpFinInventoryEffected", new string[] { "flag", "Office_ID", "Ledger_ID" }, new string[] { "1", ddlOfficeName.SelectedValue.ToString(), ddlLedgerName.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                DivDetail.Visible = true;
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
            if (ddlLedgerName.SelectedIndex > 0 && ddlOfficeName.SelectedIndex > 0)
            {
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnGenerated_Click(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            string msg = "";
            if (ddlLedgerName.SelectedIndex == 0)
            {
                msg += "Select Ledger.\\n";
            }

            if (msg == "")
            {
                //StringBuilder sbSet_Attendance = new StringBuilder();
                string Year = ddlLedgerName.SelectedValue.ToString();
                string LoginUserID = ViewState["Emp_ID"].ToString();
                string Office_ID = ViewState["Office_ID"].ToString();
                if (ViewState["UPage"].ToString() == Session["Page"].ToString())
                {
                    foreach (GridViewRow gr in GridView1.Rows)
                    {

                        CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                        Label lblGenStatus = (Label)gr.FindControl("lblGenStatus");
                        Label lblRowNumber = (Label)gr.FindControl("lblRowNumber");
                        Label lblItemTx_ID = (Label)gr.FindControl("lblItemTx_ID");
                        Label lblVoucherTx_ID = (Label)gr.FindControl("lblVoucherTx_ID");
                        Label lblOffice_ID = (Label)gr.FindControl("lblOffice_ID");
                        
                        if (chkSelect.Checked == true && lblGenStatus.Text != "StockUpdated")
                        {
                            ds2 = objdb.ByProcedure("SpFinInventoryEffected",
                            new string[] { "flag", "ItemTx_ID", "Office_ID", "VoucherTx_ID"},
                            new string[] { "2", lblRowNumber.ToolTip.ToString(), lblOffice_ID.Text, lblVoucherTx_ID.Text }, "dataset");



                            /****************************/
                            if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                            {


                                string S_TrType = ds2.Tables[0].Rows[0]["TrType"].ToString();
                                string S_ItemID = ds2.Tables[0].Rows[0]["Item_id"].ToString();
                                string S_Quantity = ds2.Tables[0].Rows[0]["Quantity"].ToString();
                                string S_Rate = ds2.Tables[0].Rows[0]["Rate"].ToString();
                                string S_VoucherTx_ID = ds2.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
                                string S_VoucherTx_No = ds2.Tables[0].Rows[0]["VoucherNo"].ToString();
                                string S_Office_ID = ds2.Tables[0].Rows[0]["Office_ID"].ToString();
                                string S_WarehouseID = ds2.Tables[0].Rows[0]["Warehouse_id"].ToString();
                                string S_Emp_ID = ds2.Tables[0].Rows[0]["Emp_ID"].ToString();
                                string S_VoucherDate = ds2.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                                string S_Form = ds2.Tables[0].Rows[0]["VoucherTx_Type"].ToString();

    
                                if (ds2.Tables[0].Rows[0]["TrType"].ToString() == "Dr")
                                {
                                 objdb.ByProcedure("SpFinInventoryEffected",
                                      new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "TransactionFrom", "InvoiceNo", "Office_Id", "Warehouse_id", "CreatedBy", "TranDt" }
                                     , new string[] { "3", S_ItemID, "0", S_Quantity, S_Rate, S_VoucherTx_ID, S_Form.ToString(), S_VoucherTx_No, S_Office_ID, S_WarehouseID, S_Emp_ID, Convert.ToDateTime(S_VoucherDate, cult).ToString("yyyy/MM/dd") }, "dataset");
                                }
                                else
                                {
                                 objdb.ByProcedure("SpFinInventoryEffected",
                                      new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "TransactionFrom", "InvoiceNo", "Office_Id", "Warehouse_id", "CreatedBy", "TranDt" }
                                     , new string[] { "3", S_ItemID, S_Quantity, "0", S_Rate, S_VoucherTx_ID, S_Form.ToString(), S_VoucherTx_No, S_Office_ID, S_WarehouseID, S_Emp_ID, Convert.ToDateTime(S_VoucherDate, cult).ToString("yyyy/MM/dd") }, "dataset");

                                }
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Somthing went wrong please contact to admin.');", true);
                            }

                            /****************************/

                        }
                    }

                    Session["Page"] = Server.UrlEncode(System.DateTime.Now.ToString());

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
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
    /*******New PP**********/
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {

        ViewState["Office_ID"] = ddlOfficeName.SelectedItem.Value;
        DivDetail.Visible = false;
        FillLedgerDropdown();
    }
    protected void ddlLedgerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
}