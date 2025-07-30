using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;


public partial class mis_Finance_VoucherContra : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {
                if (!IsPostBack)
                {

                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["LedgerTotal"] = "0";

                    FillAccountDropDown();
                    FillParticularsDropDown();

                    ViewState["TableId"] = "-1";

                    CreateLedgerTable();
                    CreateDataSetFinChequeTx();
         
                    GridViewLedgerDetail.DataSource = new string[] { };
                    GridViewLedgerDetail.DataBind();

                    GVFinChequeTx.DataSource = new string[] { };
                    GVFinChequeTx.DataBind();

                    ds = null;
                    ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
                    if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
                    }

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
    protected void FillAccountDropDown()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag" }, new string[] { "11" }, "dataset");
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
    protected void FillParticularsDropDown()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag" }, new string[] { "11" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlsubLedger_ID.DataSource = ds;
                ddlsubLedger_ID.DataTextField = "Ledger_Name";
                ddlsubLedger_ID.DataValueField = "Ledger_ID";
                ddlsubLedger_ID.DataBind();
                ddlsubLedger_ID.Items.Insert(0, "Select");

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
    protected void btnAddLedger_Click(object sender, EventArgs e)
    {
        CreatTableFinChequeTx();
        string msg = "";
        if (ddlsubLedger_ID.SelectedIndex <= 0)
        {
            msg = "Select Particulars.\\n";
        }
        if (txtLedgerTx_Amount.Text == "")
        {
            msg += "Enter Amount.\\n";
        }

        if (msg == "")
        {

            txtChequeTx_Amount.Text = txtLedgerTx_Amount.Text;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);

            btnAddCheque.Enabled = true;
            btnAddChequeDetail.Enabled = false;
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }

    }
    protected void CreateDataSetFinChequeTx()
    {
        DataSet dsFinChequeTx = new DataSet();
        ViewState["dsFinChequeTx"] = dsFinChequeTx;

    }
    protected void CreatTableFinChequeTx()
    {
        ViewState["TableId"] = Convert.ToInt32(ViewState["TableId"].ToString()) + 1;
        
        DataTable dt_FinChequeTx = new DataTable(ViewState["TableId"].ToString());
        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_No", typeof(string)));
        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Date", typeof(string)));
        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Amount", typeof(decimal)));
        
        ViewState["FinChequeTx"] = dt_FinChequeTx;
        GVFinChequeTx.DataSource = dt_FinChequeTx;
        GVFinChequeTx.DataBind();
    }
    protected void CreateLedgerTable()
    {

        DataTable dt_LedgerTable = new DataTable(ViewState["TableId"].ToString());
        dt_LedgerTable.Columns.Add(new DataColumn("subLedger_ID", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("subLedger_Name", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_Amount", typeof(decimal)));
        dt_LedgerTable.Columns.Add(new DataColumn("Ledger_TableID", typeof(decimal)));

        ViewState["LedgerTable"] = dt_LedgerTable;
    }
    protected void ClearFinChequeTxModal()
    {
        txtChequeTx_No.Text = "";
        txtChequeTx_Date.Text = "";
        txtChequeTx_Amount.Text = "";

        ViewState["FinChequeTx"] = "";

        GVFinChequeTx.DataSource = new string[] { };
        GVFinChequeTx.DataBind();


        ddlsubLedger_ID.ClearSelection();
        txtLedgerTx_Amount.Text = "";
       

    }
    protected void GridViewLedgerDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsFinChequeTx = (DataSet)ViewState["dsFinChequeTx"];
        int TableId = int.Parse(GridViewLedgerDetail.SelectedDataKey.Value.ToString());

        GVViewFinChequeTx.DataSource = dsFinChequeTx.Tables[TableId.ToString()];
        GVViewFinChequeTx.DataBind();

        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetailView();", true);



    }
    protected void GridViewLedgerDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int TableId = int.Parse(GridViewLedgerDetail.DataKeys[e.RowIndex].Value.ToString());
        DataSet dsFinChequeTx = (DataSet)ViewState["dsFinChequeTx"];
        DataSet dsFinChequeTxTemp = new DataSet();
        dsFinChequeTxTemp = dsFinChequeTx;
        for (int i = 0; i < dsFinChequeTxTemp.Tables.Count; i++)
        {
            if (dsFinChequeTxTemp.Tables[i].TableName == TableId.ToString())
            {
                dsFinChequeTx.Tables[i].Clear();
                //dsBillByBill.Tables[i].Merge(dsBillByBillTemp.Tables[i]);
            }
        }



        DataTable dt_LedgerTableTemp = new DataTable(ViewState["TableId"].ToString());
        dt_LedgerTableTemp.Columns.Add(new DataColumn("subLedger_ID", typeof(string)));
        dt_LedgerTableTemp.Columns.Add(new DataColumn("subLedger_Name", typeof(string)));
        dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_Amount", typeof(decimal)));
        dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_TableID", typeof(decimal)));


        int gridRows = GridViewLedgerDetail.Rows.Count;
        for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
        {

            Label subLedger_ID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("subLedger_ID");
            Label subLedger_Name = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("subLedger_Name");
            Label LedgerTx_Amount = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Amount");
            Label Ledger_TableID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_TableID");
            if (Ledger_TableID.Text != TableId.ToString())
            {

                dt_LedgerTableTemp.Rows.Add(subLedger_ID.Text, subLedger_Name.Text, LedgerTx_Amount.Text, Ledger_TableID.Text);
            }
        }
        GridViewLedgerDetail.DataSource = null;
        GridViewLedgerDetail.DataBind();
        GridViewLedgerDetail.DataSource = dt_LedgerTableTemp;
        GridViewLedgerDetail.DataBind();
        decimal LedgerTotal = 0;
        LedgerTotal = dt_LedgerTableTemp.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));

        GridViewLedgerDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
        GridViewLedgerDetail.FooterRow.Cells[3].Text = "<b>" + LedgerTotal.ToString() + "</b>";

        ViewState["LedgerTotal"] = LedgerTotal;
        ViewState["LedgerTable"] = dt_LedgerTableTemp;
    }
    protected void ddlsubLedger_ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //string Ledger_ID = ddlsubLedger_ID.SelectedValue.ToString();

            //ds = objdb.ByProcedure("SpFinBillByBillTx",
            //    new string[] { "flag", "Ledger_ID" },
            //    new string[] { "2", Ledger_ID }, "dataset");

            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    GVBillByBillDetailAtModal.DataSource = ds;
            //    GVBillByBillDetailAtModal.DataBind();

            //    ddlBillByBillTx_Ref.DataSource = ds;
            //    ddlBillByBillTx_Ref.DataTextField = "BillByBillTx_Ref";
            //    ddlBillByBillTx_Ref.DataValueField = "BillByBillTx_Amount";
            //    ddlBillByBillTx_Ref.DataBind();
            //    ddlBillByBillTx_Ref.Items.Insert(0, "Select");
            //}
            //else
            //{
            //    ddlBillByBillTx_Ref.Items.Clear();
            //    GVBillByBillDetailAtModal.DataSource = null;
            //    GVBillByBillDetailAtModal.DataBind();
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            if (txtVoucherTx_No.Text == "")
            {
                msg += "Enter Voucher No. \\n";
            }
            if (txtVoucherTx_Date.Text == "")
            {
                msg += "Enter Date. \\n";
            }
            if (ddlLedger_ID.SelectedIndex == 0)
            {
                msg += "Select Account. \\n";
            }

            if (msg == "")
            {

                string sDate = (Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd")).ToString();
                DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
                int Month = int.Parse(datevalue.Month.ToString());
                int Year = int.Parse(datevalue.Year.ToString());
                int FY = Year;
                string FinancialYear = Year.ToString();
                string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
                FinancialYear = "";
                if (Month <= 3)
                {
                    FY = Year - 1;
                    FinancialYear = FY.ToString() + "-" + LFY.ToString();
                }
                else
                {

                    FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
                }


                ds = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "Ledger_ID", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy" },
                    new string[] { "0", ddlLedger_ID.SelectedValue.ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Contra", "Contra", txtVoucherTx_No.Text, "", txtVoucherTx_Narration.Text, ViewState["LedgerTotal"].ToString(), Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", ViewState["Emp_ID"].ToString() }, "dataset");

                string VoucherTx_ID = ds.Tables[0].Rows[0]["VoucherTx_ID"].ToString();

                objdb.ByProcedure("SpFinLedgerTx",
                    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy" },
                    new string[] { "0", ddlLedger_ID.SelectedValue.ToString(), VoucherTx_ID, "Contra", ViewState["LedgerTotal"].ToString(), Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString()}, "dataset");

                DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];


                for (int i = 0; i < dt_LedgerTable.Rows.Count; i++)
                {

                    string Ledger_ID = dt_LedgerTable.Rows[i]["subLedger_ID"].ToString();
                    string LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Amount"].ToString();
                    int TableId = int.Parse(dt_LedgerTable.Rows[i]["Ledger_TableID"].ToString());

                    objdb.ByProcedure("SpFinLedgerTx",
                    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy" },
                    new string[] { "0", Ledger_ID, VoucherTx_ID, "Contra", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString()}, "dataset");
                    DataSet dsFinChequeTx = (DataSet)ViewState["dsFinChequeTx"];
            
                    for (int j = 0; j < dsFinChequeTx.Tables.Count; j++)
                    {
                        if (dsFinChequeTx.Tables[j].TableName == TableId.ToString())
                        {
                            for (int k = 0; k < dsFinChequeTx.Tables[j].Rows.Count; k++)
                            {
                                string ChequeTx_No = dsFinChequeTx.Tables[j].Rows[k]["ChequeTx_No"].ToString();
                                string ChequeTx_Date = dsFinChequeTx.Tables[j].Rows[k]["ChequeTx_Date"].ToString();
                                string ChequeTx_Amount = dsFinChequeTx.Tables[j].Rows[k]["ChequeTx_Amount"].ToString();
                                objdb.ByProcedure("SpFinChequeTx",
                                new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy" },
                                new string[] { "1", VoucherTx_ID, Ledger_ID, "Contra", ChequeTx_No, Convert.ToDateTime(ChequeTx_Date, cult).ToString("yyyy/MM/dd"), ChequeTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString() }, "dataset");
                            }
                        }
                    }
                }

                lblMsg.Text = objdb.Alert("fa-check", "alert-success","Thank you!", "Operation Completed Successfully.");
                ClearData();
                
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
    protected void ClearData()
    {
        try
        {
            txtVoucherTx_No.Text = "";
            ddlLedger_ID.ClearSelection();
            ddlsubLedger_ID.ClearSelection();
            txtLedgerTx_Amount.Text = "";
            txtVoucherTx_Narration.Text = "";
            GridViewLedgerDetail.DataSource = new string[] { };
            GridViewLedgerDetail.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnAddChequeDetail_Click(object sender, EventArgs e)
    {
        DataSet dsFinChequeTx = (DataSet)ViewState["dsFinChequeTx"];

        dsFinChequeTx.Merge((DataTable)ViewState["FinChequeTx"]);

        ViewState["dsFinChequeTx"] = dsFinChequeTx;

        DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
        dt_LedgerTable.Rows.Add(ddlsubLedger_ID.SelectedValue.ToString(), ddlsubLedger_ID.SelectedItem.Text, txtLedgerTx_Amount.Text, ViewState["TableId"].ToString());

        GridViewLedgerDetail.DataSource = dt_LedgerTable;
        GridViewLedgerDetail.DataBind();

        decimal LedgerTotal = 0;
        LedgerTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));

        GridViewLedgerDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
        GridViewLedgerDetail.FooterRow.Cells[3].Text = "<b>" + LedgerTotal.ToString() + "</b>";

        ViewState["LedgerTotal"] = LedgerTotal;

        ViewState["LedgerTable"] = dt_LedgerTable;

        ClearFinChequeTxModal();
    }
    protected void btnAddCheque_Click(object sender, EventArgs e)
    {
        string msg = "";
        if(txtChequeTx_No.Text == "")
        {
            msg += "Enter Cheque/ DD No \\n";
        }
        if (txtChequeTx_Date.Text == "")
        {
            msg += "Enter Cheque/ DD Date \\n";
        }
        if (txtChequeTx_Amount.Text == "")
        {
            msg += "Enter Amount \\n";
        }
        if(msg == "")
        {
            DataTable dt_FinChequeTx = (DataTable)ViewState["FinChequeTx"];

            dt_FinChequeTx.Rows.Add(txtChequeTx_No.Text, txtChequeTx_Date.Text, txtChequeTx_Amount.Text);


            GVFinChequeTx.DataSource = dt_FinChequeTx;
            GVFinChequeTx.DataBind();

            decimal ChequeTx_AmountTotal = 0;
            ChequeTx_AmountTotal = dt_FinChequeTx.AsEnumerable().Sum(row => row.Field<decimal>("ChequeTx_Amount"));

            GVFinChequeTx.FooterRow.Cells[2].Text = "<b>Total : </b>";
            GVFinChequeTx.FooterRow.Cells[3].Text = "<b>" + ChequeTx_AmountTotal.ToString() + "</b>";

            txtChequeTx_Amount.Text = (Convert.ToDecimal(txtLedgerTx_Amount.Text) - ChequeTx_AmountTotal).ToString();
            txtChequeTx_No.Text = "";
            txtChequeTx_Date.Text = "";
            if (txtChequeTx_Amount.Text == "0")
            {
                btnAddCheque.Enabled = false;
                btnAddChequeDetail.Enabled = true;
            }
            ViewState["FinChequeTx"] = dt_FinChequeTx;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
        }
        else
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }
        
    }
    
}