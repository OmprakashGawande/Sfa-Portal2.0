using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Finance_VoucherPayment : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    //static DataSet dsBillByBill;
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
                    CreateBillByBillDataSet();

                    GridViewBillByBillDetail.DataSource = new string[] { };
                    GridViewBillByBillDetail.DataBind();

                    GridViewLedgerDetail.DataSource = new string[] { };
                    GridViewLedgerDetail.DataBind();

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
    protected void btnAddBillByBill_Click(object sender, EventArgs e)
    {

        DataTable dt_BillByBillTable = (DataTable)ViewState["BillByBillTable"];


        if (ddlRefType.SelectedItem.Text == "Agst Ref")
        {
            dt_BillByBillTable.Rows.Add(ddlRefType.SelectedItem.Text, ddlBillByBillTx_Ref.SelectedItem.Text, txtBillByBillTx_Amount.Text);
        }
        else if (ddlRefType.SelectedItem.Text == "Advance" || ddlRefType.SelectedItem.Text == "New Ref")
        {
            dt_BillByBillTable.Rows.Add(ddlRefType.SelectedItem.Text, txtBillByBillTx_Ref.Text, txtBillByBillTx_Amount.Text);
        }
        else if (ddlRefType.SelectedItem.Text == "On Account")
        {
            dt_BillByBillTable.Rows.Add(ddlRefType.SelectedItem.Text, "", txtBillByBillTx_Amount.Text);
        }

        GridViewBillByBillDetail.DataSource = dt_BillByBillTable;
        GridViewBillByBillDetail.DataBind();

        decimal RefTotal = 0;
        RefTotal = dt_BillByBillTable.AsEnumerable().Sum(row => row.Field<decimal>("BillByBillTx_Amount"));

        GridViewBillByBillDetail.FooterRow.Cells[1].Text = "<b>Total : </b>";
        GridViewBillByBillDetail.FooterRow.Cells[2].Text = "<b>" + RefTotal.ToString() + "</b>";

        txtBillByBillTx_Amount.Text = (Convert.ToDecimal(txtLedgerTx_Amount.Text) - RefTotal).ToString();
        txtBillByBillTx_Ref.Text = "";
        ddlRefType.ClearSelection();
        if (txtBillByBillTx_Amount.Text == "0")
        {
            btnAddBillByBill.Enabled = false;
            btnBillByBillSave.Enabled = true;
        }

        ViewState["BillByBillTable"] = dt_BillByBillTable;



        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);

    }
    protected void btnAddLedger_Click(object sender, EventArgs e)
    {
        CreateBillByBillTable();
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
            txtBillByBillTx_Amount.Text = txtLedgerTx_Amount.Text;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);

            btnAddBillByBill.Enabled = true;
            btnBillByBillSave.Enabled = false;
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }

    }
    protected void CreateBillByBillDataSet()
    {
        DataSet dsBillByBill = new DataSet();
        ViewState["dsBillByBill"] = dsBillByBill;

    }
    protected void CreateBillByBillTable()
    {

        ViewState["TableId"] = Convert.ToInt32(ViewState["TableId"].ToString()) + 1;
        DataTable dt_BillByBillTable = new DataTable(ViewState["TableId"].ToString());
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
        ViewState["BillByBillTable"] = dt_BillByBillTable;

        GridViewBillByBillDetail.DataSource = dt_BillByBillTable;
        GridViewBillByBillDetail.DataBind();
    }
    protected void CreateLedgerTable()
    {

        DataTable dt_LedgerTable = new DataTable(ViewState["TableId"].ToString());
        dt_LedgerTable.Columns.Add(new DataColumn("subLedger_ID", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("subLedger_Name", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_Amount", typeof(decimal)));
        dt_LedgerTable.Columns.Add(new DataColumn("BillByBillTx_TableID", typeof(decimal)));

        ViewState["LedgerTable"] = dt_LedgerTable;
    }
    protected void btnBillByBillSave_Click(object sender, EventArgs e)
    {
        DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
        dsBillByBill.Merge((DataTable)ViewState["BillByBillTable"]);

        ViewState["dsBillByBill"] = dsBillByBill;

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

        ClearBillByBillModal();


    }
    protected void ClearBillByBillModal()
    {
        ddlRefType.ClearSelection();
        ddlBillByBillTx_Ref.ClearSelection();
        txtBillByBillTx_Ref.Text = "";
        ddlsubLedger_ID.ClearSelection();
        txtLedgerTx_Amount.Text = "";

    }
    protected void GridViewLedgerDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
        int TableId = int.Parse(GridViewLedgerDetail.SelectedDataKey.Value.ToString());

        GridViewBillByBillViewDetail.DataSource = dsBillByBill.Tables[TableId.ToString()];
        GridViewBillByBillViewDetail.DataBind();

        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillByBillViewModal();", true);



    }
    protected void GridViewLedgerDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int TableId = int.Parse(GridViewLedgerDetail.DataKeys[e.RowIndex].Value.ToString());
        DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
        DataSet dsBillByBillTemp = new DataSet();
        dsBillByBillTemp = dsBillByBill;
        for (int i = 0; i < dsBillByBillTemp.Tables.Count; i++)
        {
            if (dsBillByBillTemp.Tables[i].TableName == TableId.ToString())
            {
                dsBillByBill.Tables[i].Clear();
                //dsBillByBill.Tables[i].Merge(dsBillByBillTemp.Tables[i]);
            }
        }



        DataTable dt_LedgerTableTemp = new DataTable(ViewState["TableId"].ToString());
        dt_LedgerTableTemp.Columns.Add(new DataColumn("subLedger_ID", typeof(string)));
        dt_LedgerTableTemp.Columns.Add(new DataColumn("subLedger_Name", typeof(string)));
        dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_Amount", typeof(decimal)));
        dt_LedgerTableTemp.Columns.Add(new DataColumn("BillByBillTx_TableID", typeof(decimal)));


        int gridRows = GridViewLedgerDetail.Rows.Count;
        for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
        {

            Label subLedger_ID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("subLedger_ID");
            Label subLedger_Name = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("subLedger_Name");
            Label LedgerTx_Amount = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Amount");
            Label BillByBillTx_TableID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("BillByBillTx_TableID");
            if (BillByBillTx_TableID.Text != TableId.ToString())
            {

                dt_LedgerTableTemp.Rows.Add(subLedger_ID.Text, subLedger_Name.Text, LedgerTx_Amount.Text, BillByBillTx_TableID.Text);
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
            string Ledger_ID = ddlsubLedger_ID.SelectedValue.ToString();

            ds = objdb.ByProcedure("SpFinBillByBillTx",
                new string[] { "flag", "Ledger_ID" },
                new string[] { "2", Ledger_ID }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GVBillByBillDetailAtModal.DataSource = ds;
                GVBillByBillDetailAtModal.DataBind();

                ddlBillByBillTx_Ref.DataSource = ds;
                ddlBillByBillTx_Ref.DataTextField = "BillByBillTx_Ref";
                ddlBillByBillTx_Ref.DataValueField = "BillByBillTx_Amount";
                ddlBillByBillTx_Ref.DataBind();
                ddlBillByBillTx_Ref.Items.Insert(0, "Select");
            }
            else
            {
                ddlBillByBillTx_Ref.Items.Clear();
                GVBillByBillDetailAtModal.DataSource = null;
                GVBillByBillDetailAtModal.DataBind();
            }
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
                    new string[] { "0", ddlLedger_ID.SelectedValue.ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Payment", "Payment", txtVoucherTx_No.Text, "", txtVoucherTx_Narration.Text, ViewState["LedgerTotal"].ToString(), Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", ViewState["Emp_ID"].ToString() }, "dataset");

                string VoucherTx_ID = ds.Tables[0].Rows[0]["VoucherTx_ID"].ToString();

                objdb.ByProcedure("SpFinLedgerTx",
                    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy" },
                    new string[] { "0", ddlLedger_ID.SelectedValue.ToString(), VoucherTx_ID, "Payment", ViewState["LedgerTotal"].ToString(), Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), }, "dataset");
                
                DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                

                for (int i = 0; i < dt_LedgerTable.Rows.Count; i++)
                {

                    string Ledger_ID = dt_LedgerTable.Rows[i]["subLedger_ID"].ToString();
                    string LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Amount"].ToString();
                    int TableId = int.Parse(dt_LedgerTable.Rows[i]["BillByBillTx_TableID"].ToString());

                    objdb.ByProcedure("SpFinLedgerTx",
                    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy" },
                    new string[] { "0", Ledger_ID, VoucherTx_ID, "Payment", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), }, "dataset");
                    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                    DataSet dsBillByBillTemp = new DataSet();
                    dsBillByBillTemp = dsBillByBill;
                    for (int j = 0; j < dsBillByBillTemp.Tables.Count; j++)
                    {
                        if (dsBillByBillTemp.Tables[j].TableName == TableId.ToString())
                        {
                            for (int k = 0; k < dsBillByBillTemp.Tables[j].Rows.Count; k++)
                            {
                                string BillByBillTx_RefType = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_RefType"].ToString();
                                string BillByBillTx_Ref = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_Ref"].ToString();
                                string BillByBillTx_Amount = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_Amount"].ToString();
                                objdb.ByProcedure("SpFinBillByBillTx",
                                new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive" },
                                new string[] { "3", VoucherTx_ID, Ledger_ID, BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1" }, "dataset");
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
}