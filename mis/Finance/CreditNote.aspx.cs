using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_CreditNote : System.Web.UI.Page
{
    DataSet ds;
    //static DataSet DS_GridViewParticulars = new DataSet();
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {

                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["RowNo"] = "0";
                    //ViewState["CGST"] = "1";
                    //ViewState["SGST"] = "2";
                    ViewState["RoundOff"] = "3";
                    ViewState["VoucherTx_ID"] = "0";
                    lblGrandTotal.Attributes.Add("readonly", "readonly");
                    txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
                    //txtVoucherTx_Date.Enabled = false;
                    txtVoucherTxDate.Attributes.Add("readonly", "readonly");
                    txtVoucherTxDate.Enabled = false;
                    ViewState["LedgerTotal"] = "0";
                    CreateLedgerTable();
                    txDebtorAmt.Attributes.Add("readonly", "readonly");
                    CreateBillByBillDataSet();
                    FillDropDown();

                    AddItem("NA");

                    GridViewItem.DataSource = new string[] { };
                    GridViewItem.DataBind();

                    GridViewDebtor.DataSource = new string[] { };
                    GridViewDebtor.DataBind();
                    DataTable dt_BillByBillData = new DataTable();
                    dt_BillByBillData.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
                    dt_BillByBillData.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
                    dt_BillByBillData.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
                    ViewState["dt_BillByBillData"] = dt_BillByBillData;

                    if (Request.QueryString["VoucherTx_ID"] != null && Request.QueryString["Action"] != null)
                    {
                        ViewState["VoucherTx_ID"] = objdb.Decrypt(Request.QueryString["VoucherTx_ID"].ToString());
                        ViewVoucher();
                    }
                    else
                    {
                        GetPreviousVoucherNo();
                    }
                    //if (Request.QueryString["VoucherTx_ID"] != null && Request.QueryString["Action"] != null)
                    //{
                    //    string Action = objdb.Decrypt(Request.QueryString["Action"].ToString());
                    //    ViewState["Action"] = Action;
                    //    ViewState["VoucherTx_ID"] = objdb.Decrypt(Request.QueryString["VoucherTx_ID"].ToString());
                    //    if (Action == "2")
                    //    {
                    //        FillDetail();
                    //    }
                    //    else if (Action == "1")
                    //    {
                    //        FillDetail();
                    //        //ViewVoucher();
                    //    }
                    //}


                }
                else
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillDropDown()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster",
                new string[] { "flag", "Office_ID", "MultipleHeadIDs" },
                new string[] { "22", ViewState["Office_ID"].ToString(), "1,2,3,4" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDebitLedger.DataTextField = "Ledger_Name";
                    ddlDebitLedger.DataValueField = "Ledger_ID";
                    ddlDebitLedger.DataSource = ds.Tables[0];
                    ddlDebitLedger.DataBind();
                    ddlDebitLedger.Items.Insert(0, new ListItem("Select", "0"));


                    ddlLedger.DataTextField = "Ledger_Name";
                    ddlLedger.DataValueField = "Ledger_ID";
                    ddlLedger.DataSource = ds.Tables[0];
                    ddlLedger.DataBind();
                    ddlLedger.Items.Insert(0, new ListItem("Select", "0"));


                }

            }
            ds = null;
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
                ViewState["Voucher_FY"] = ds.Tables[0].Rows[0]["Voucher_FY"].ToString();
                FillVoucherNo();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void AddItem(string ID)
    {
        try
        {
            string status = "0";
            DataTable dt_GridViewLedger = new DataTable();
            dt_GridViewLedger.Columns.Add(new DataColumn("LedgerID", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("LedgerName", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("Status", typeof(string)));
            if (ViewState["CGST"].ToString() == "46721")
            {
                dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST OUTPUT", "0", status);
            }
            else
            {
                dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST", "0", status);
            }
            if (ViewState["SGST"].ToString() == "46722")
            {
                dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST OUTPUT", "0", status);
            }
            else
            {
                dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST", "0", status);
            }
            

            int gridRows = GridViewLedger.Rows.Count;
            if (gridRows > 2)
            {
                for (int rowIndex = 2; rowIndex < gridRows; rowIndex++)
                {
                    if (rowIndex > 2)
                    {
                        status = "1";
                    }
                    Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                    Label lblLedgerName = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblLedgerName");
                    TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                    dt_GridViewLedger.Rows.Add(lblID.Text, lblLedgerName.Text, txtAmount.Text, status);
                }
            }
            else
            {
                dt_GridViewLedger.Rows.Add(ViewState["RoundOff"].ToString(), "Round off", "0", status);
            }

            GridViewLedger.DataSource = dt_GridViewLedger;
            GridViewLedger.DataBind();
            ViewState["RowNo"] = "0";

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillLedgerAmount(string ID)
    {
        try
        {
            string status = "0";
            DataTable dt_GridViewLedger = new DataTable();
            dt_GridViewLedger.Columns.Add(new DataColumn("LedgerID", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("LedgerName", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("Status", typeof(string)));
            int gridRows = GridViewLedger.Rows.Count;
            for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {
                //if (rowIndex > 2)
                //{
                //    status = "1";
                //}
                Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                Label lblLedgerName = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblLedgerName");
                TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                if (lblID.Text == "1" || lblID.Text == "2" || lblID.Text == "3" || lblID.Text == "46721" || lblID.Text == "46722" || lblID.Text == "46723")
                {
                    status = "0";
                }
                else
                {
                    status = "1";
                }
                if (lblID.Text != ID)
                {
                    dt_GridViewLedger.Rows.Add(lblID.Text, lblLedgerName.Text, txtAmount.Text, status);
                }
            }
            if (ID == "0")
            {
                dt_GridViewLedger.Rows.Add(ddlLedger.SelectedValue.ToString(), ddlLedger.SelectedItem.ToString(), txtLedgerAmt.Text, "1");
            }

            GridViewLedger.DataSource = dt_GridViewLedger;
            GridViewLedger.DataBind();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnAddLedgerAmt_Click(object sender, EventArgs e)
    {
        try
        {
            int LedgerStatus = 0;
            lblMsg.Text = "";
            if (ddlLedger.SelectedIndex > 0 && txtLedgerAmt.Text != "")
            {
                int rowIndex = 0;
                int gridRows = GridViewLedger.Rows.Count;
                if (gridRows > 0)
                {
                    for (rowIndex = 0; rowIndex < gridRows; rowIndex++)
                    {
                        Label lblLedgerID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                        if (ddlLedger.SelectedIndex > 0)
                        {
                            if (lblLedgerID.Text == ddlLedger.SelectedValue.ToString())
                            {
                                LedgerStatus = 1;
                            }
                            else
                            {


                            }
                        }

                    }
                }
                if (LedgerStatus == 0)
                {
                    FillLedgerAmount("0");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                    ViewState["GrandTotal"] = lblGrandTotal.Text;
                    //btnAcceptEnable();
                    ddlLedger.ClearSelection();
                    txtLedgerAmt.Text = "";
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Ledger already exists');", true);
                }
                //FillLedgerAmount("0");
                //ddlLedger.ClearSelection();
                //txtLedgerAmt.Text = "";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnAddDebtor_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string LedgerId = ddlDebitLedger.SelectedValue.ToString();
            txtBillByBillTx_Ref.Visible = true;
            ddlBillByBillTx_Ref.Visible = false;
            txtBillByBillTx_Ref.Text = txtVoucherTx_Ref.Text;
            txtChequeTx_Amount.Text = txDebtorAmt.Text;
            BindBillByBillData();
            ViewState["Amount"] = txDebtorAmt.Text;
            txtBillByBillTx_Amount.Text = txDebtorAmt.Text;
            ddlRefType.ClearSelection();
            ddlBillByBillTx_crdr.ClearSelection();
            //ddlBillByBillTx_Ref.Items.Clear();
            lnkView.Visible = false;
            txtBillByBillTx_Ref.Enabled = true;
            string msg = "";
            if (ddlDebitLedger.SelectedIndex <= 0)
            {
                msg = "Select Debtor.\\n";
            }
            if (txDebtorAmt.Text == "")
            {
                msg += "Enter Amount.\\n";
            }
            if (GridViewItem.Rows.Count == 0)
            {
                msg += "Enter Item Detail. \\n";
            }
            if (txtVoucherTx_No.Text == "")
            {
                msg += "Enter Voucher/Bill No. \\n";
            }
            if (msg == "")
            {
                int rowIndex = 0;
                int gridRows = GridViewDebtor.Rows.Count;
                if (gridRows > 0)
                {
                    for (rowIndex = 0; rowIndex < gridRows; rowIndex++)
                    {
                        Label lblLedgerID = (Label)GridViewDebtor.Rows[rowIndex].Cells[0].FindControl("Ledger_ID");
                        if (lblLedgerID.Text == ddlDebitLedger.SelectedValue.ToString())
                        {
                            status = 1;
                        }
                        else
                        {


                        }
                    }
                }
                if (status == 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Ledger already exists');", true);
                }
                else
                {
                    ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", LedgerId, ViewState["Office_ID"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "Yes")
                        {
                            CreateBillByBillTable();
                            if (lblGrandTotal.Text.Contains("-"))
                            {
                                ddlBillByBillTx_crdr.SelectedValue = "Dr";
                            }
                            else
                            {
                                ddlBillByBillTx_crdr.SelectedValue = "Cr";
                            }
                            //txtBillByBillTx_Amount.Text = txtLedgerTx_Amount.Text;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);

                        }
                        else if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "BankLedger")
                        {
                            btnAddCheque.Enabled = true;
                            //btnAddChequeDetail.Enabled = false;
                            CreatTableFinChequeTx();
                            //txtChequeTx_Amount.Text = txtLedgerTx_Amount.Text;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
                        }

                        else
                        {
                            //CreateBillByBillTable();
                            //ViewState["GrandTotal"] = lblGrandTotal.Text;
                            //DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                            //dsBillByBill.Merge((DataTable)ViewState["BillByBillTable"]);
                            //ViewState["dsBillByBill"] = dsBillByBill;
                            DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                            dt_LedgerTable.Rows.Add(ddlDebitLedger.SelectedValue.ToString(), ddlDebitLedger.SelectedItem.Text, txDebtorAmt.Text, "None");

                            GridViewDebtor.DataSource = dt_LedgerTable;
                            GridViewDebtor.DataBind();

                            if (dt_LedgerTable.Rows.Count > 0)
                            {
                                decimal Amount = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));

                                GridViewDebtor.FooterRow.Cells[2].Text = "<b>| TOTAL |</b>";
                                GridViewDebtor.FooterRow.Cells[3].Text = "<b>" + Amount.ToString() + "</b>";
                                GridViewDebtor.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                            }


                            decimal LedgerTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));
                            ViewState["LedgerTotal"] = LedgerTotal;
                            hfvalue.Value = ViewState["LedgerTotal"].ToString();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                            //btnAcceptEnable();
                            //if (ViewState["GrandTotal"].ToString() == ViewState["LedgerTotal"].ToString())
                            //{
                            //    btnAccept.Enabled = true;
                            //    btnAddDebtor.Enabled = false;
                            //}
                            ClearBillByBillModal();
                        }
                    }

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
    protected void GridViewLedger_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        try
        {
            string ID = GridViewLedger.DataKeys[e.RowIndex].Value.ToString();
            FillLedgerAmount(ID);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void CreateLedgerTable()
    {
        DataTable dt_LedgerTable = new DataTable();
        dt_LedgerTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_Amount", typeof(decimal)));
        dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_MaintainType", typeof(string)));
        //dt_LedgerTable.Columns.Add(new DataColumn("Ledger_TableID", typeof(decimal)));

        ViewState["LedgerTable"] = dt_LedgerTable;


        GridViewDebtor.DataSource = dt_LedgerTable;
        GridViewDebtor.DataBind();

        if (dt_LedgerTable.Rows.Count > 0)
        {
            decimal Amount = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));

            GridViewDebtor.FooterRow.Cells[2].Text = "<b>| TOTAL |</b> ";
            GridViewDebtor.FooterRow.Cells[3].Text = "<b>" + Amount.ToString() + "</b>";
        }
    }
    protected void CreateBillByBillDataSet()
    {
        DataSet dsBillByBill = new DataSet();
        ViewState["dsBillByBill"] = dsBillByBill;

    }
    protected void CreateBillByBillTable()
    {

        string TNO = ddlDebitLedger.SelectedValue.ToString();
        DataTable dt_BillByBillTable = new DataTable(TNO);
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
        dt_BillByBillTable.Columns.Add(new DataColumn("Type", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
        ViewState["BillByBillTable"] = dt_BillByBillTable;

        GridViewBillByBillDetail.DataSource = dt_BillByBillTable;
        GridViewBillByBillDetail.DataBind();
    }
    protected void CreatTableFinChequeTx()
    {
        string TNO = ddlDebitLedger.SelectedValue.ToString();
        DataTable dt_FinChequeTx = new DataTable(TNO);
        dt_FinChequeTx.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_No", typeof(string)));
        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Date", typeof(string)));
        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Amount", typeof(decimal)));

        ViewState["FinChequeTx"] = dt_FinChequeTx;

        GVFinChequeTx.DataSource = dt_FinChequeTx;
        GVFinChequeTx.DataBind();
    }
    protected void btnAddCheque_Click(object sender, EventArgs e)
    {
        string msg = "";
        //if (txtChequeTx_No.Text == "")
        //{
        //    msg += "Enter Cheque/ DD No \\n";
        //}
        //if (txtChequeTx_Date.Text == "")
        //{
        //    msg += "Enter Cheque/ DD Date \\n";
        //}
        if (txtChequeTx_Amount.Text == "")
        {
            msg += "Enter Amount \\n";
        }
        if (msg == "")
        {
            string CheqAmount = ChequeAmount("0");
            if (decimal.Parse(txDebtorAmt.Text) != decimal.Parse(CheqAmount))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);

            }
            else
            {
                ViewState["GrandTotal"] = lblGrandTotal.Text;
                DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                dsBillByBill.Merge((DataTable)ViewState["FinChequeTx"]);
                ViewState["dsBillByBill"] = dsBillByBill;
                DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                dt_LedgerTable.Rows.Add(ddlDebitLedger.SelectedValue.ToString(), ddlDebitLedger.SelectedItem.Text, txDebtorAmt.Text, "Cheque");

                GridViewDebtor.DataSource = dt_LedgerTable;
                GridViewDebtor.DataBind();

                if (dt_LedgerTable.Rows.Count > 0)
                {
                    decimal Amount = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));

                    GridViewDebtor.FooterRow.Cells[2].Text = "<b>| TOTAL |</b>";
                    GridViewDebtor.FooterRow.Cells[3].Text = "<b>" + Amount.ToString() + "</b>";
                    GridViewDebtor.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                }


                decimal LedgerTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));
                ViewState["LedgerTotal"] = LedgerTotal;
                hfvalue.Value = ViewState["LedgerTotal"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                ClearFinChequeTxModal();
            }

        }
        else
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }

    }

    protected void BindBillByBillData()
    {

        DataTable dt_BillByBillData = (DataTable)ViewState["dt_BillByBillData"];

        ddlBillByBillTx_Ref.Items.Clear();
        string LedgerID = ddlDebitLedger.SelectedValue.ToString();
        ds = objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "2", LedgerID, ViewState["Office_ID"].ToString() }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["AgnstRef"] = ds.Tables[0].Rows[0]["AgnstRef"].ToString();
            dt_BillByBillData = ds.Tables[0];
            ViewState["dt_BillByBillData"] = dt_BillByBillData;
            ddlBillByBillTx_Ref.DataSource = dt_BillByBillData;
            ddlBillByBillTx_Ref.DataTextField = "AgnstRef";
            ddlBillByBillTx_Ref.DataValueField = "BillByBillTx_Ref";
            ddlBillByBillTx_Ref.DataBind();
            ddlBillByBillTx_Ref.Items.Insert(0, "Select");
            GridViewRefDetail.DataSource = ds.Tables[0];
            GridViewRefDetail.DataBind();

        }
        else
        {
            ddlBillByBillTx_Ref.Items.Clear();
            ddlBillByBillTx_Ref.Items.Insert(0, "Select");
            GridViewRefDetail.DataSource = new string[] { };
            GridViewRefDetail.DataBind();
        }


    }
    protected void btnAddBillByBill_Click(object sender, EventArgs e)
    {
        string msg = "";
        if (txtBillByBillTx_Ref.Text == "")
        {
            msg += "Enter Name.\n";
        }
        if (txtBillByBillTx_Amount.Text == "")
        {
            msg += "Enter Amount.\n";
        }
        if (ddlBillByBillTx_crdr.SelectedIndex == 0)
        {
            msg += "Select Cr/Dr.\n";
        }
        if (msg == "")
        {
            string LedgerAmount = BillAmount("0");
            if (decimal.Parse(LedgerAmount) != 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);
                txtBillByBillTx_Ref.Visible = true;
                txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;

                //ddlBillByBillTx_crdr.ClearSelection();
                ddlBillByBillTx_Ref.ClearSelection();

            }
            else
            {
                ViewState["GrandTotal"] = lblGrandTotal.Text;
                DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                dsBillByBill.Merge((DataTable)ViewState["BillByBillTable"]);
                ViewState["dsBillByBill"] = dsBillByBill;
                DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                dt_LedgerTable.Rows.Add(ddlDebitLedger.SelectedValue.ToString(), ddlDebitLedger.SelectedItem.Text, txDebtorAmt.Text, "BillByBill");

                GridViewDebtor.DataSource = dt_LedgerTable;
                GridViewDebtor.DataBind();

                if (dt_LedgerTable.Rows.Count > 0)
                {
                    decimal Amount = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));

                    GridViewDebtor.FooterRow.Cells[2].Text = "<b>| TOTAL |</b>";
                    GridViewDebtor.FooterRow.Cells[3].Text = "<b>" + Amount.ToString() + "</b>";
                    GridViewDebtor.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                }


                decimal LedgerTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));
                ViewState["LedgerTotal"] = LedgerTotal;
                hfvalue.Value = ViewState["LedgerTotal"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                //btnAcceptEnable();
                //if (ViewState["GrandTotal"].ToString() == ViewState["LedgerTotal"].ToString())
                //{
                //    btnAccept.Enabled = true;
                //    btnAddDebtor.Enabled = false;
                //}
                ClearBillByBillModal();
            }

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);
            txtBillByBillTx_Ref.Visible = true;
            txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
            //BindBillByBillData();
            ddlRefType.ClearSelection();
            ddlBillByBillTx_crdr.SelectedValue = "Cr";
            txtBillByBillTx_Ref.Enabled = true;
            ddlBillByBillTx_Ref.Visible = false;
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }


    }
    //protected void btnBillByBillSave_Click(object sender, EventArgs e)
    //{
    //    ViewState["GrandTotal"] = lblGrandTotal.Text;
    //    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
    //    dsBillByBill.Merge((DataTable)ViewState["BillByBillTable"]);
    //    ViewState["dsBillByBill"] = dsBillByBill;
    //    DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
    //    dt_LedgerTable.Rows.Add(ddlDebitLedger.SelectedValue.ToString(), ddlDebitLedger.SelectedItem.Text, txDebtorAmt.Text);

    //    GridViewDebtor.DataSource = dt_LedgerTable;
    //    GridViewDebtor.DataBind();
    //    decimal LedgerTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));
    //    ViewState["LedgerTotal"] = LedgerTotal;
    //    hfvalue.Value = ViewState["LedgerTotal"].ToString();
    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
    //    //btnAcceptEnable();
    //    //if (ViewState["GrandTotal"].ToString() == ViewState["LedgerTotal"].ToString())
    //    //{
    //    //    btnAccept.Enabled = true;
    //    //    btnAddDebtor.Enabled = false;
    //    //}
    //    ClearBillByBillModal();
    //}
    //protected void GridViewLedgerDetail_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];

    //    //int TableId = int.Parse(GridViewLedgerDetail.SelectedDataKey.Value.ToString());
    //    int TableId = int.Parse(GridViewLedgerDetail.SelectedDataKey.Value.ToString());
    //    int rowindex = int.Parse(GridViewLedgerDetail.SelectedRow.RowIndex.ToString());
    //    Label lbl = (Label)GridViewLedgerDetail.Rows[rowindex].FindControl("Type");
    //    if (lbl.Text == "Cr")
    //    {
    //        GridViewBillByBillViewDetail.DataSource = dsBillByBill.Tables[TableId.ToString()];
    //        GridViewBillByBillViewDetail.DataBind();
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillByBillViewModal();", true);
    //    }

    //    else
    //    {
    //        GVViewFinChequeTx.DataSource = dsBillByBill.Tables[TableId.ToString()];
    //        GVViewFinChequeTx.DataBind();
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetailView();", true);
    //    }






    //}
    //protected void GridViewLedgerDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    int TableId = int.Parse(GridViewLedgerDetail.DataKeys[e.RowIndex].Value.ToString());
    //    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
    //    DataSet dsBillByBillTemp = new DataSet();
    //    dsBillByBillTemp = dsBillByBill;
    //    for (int i = 0; i < dsBillByBillTemp.Tables.Count;


    //        i++)
    //    {
    //        if (dsBillByBillTemp.Tables[i].TableName == TableId.ToString())
    //        {
    //            dsBillByBill.Tables.Remove(dsBillByBillTemp.Tables[i].TableName);
    //            //dsBillByBill.Tables[i].Merge(dsBillByBillTemp.Tables[i]);
    //        }
    //    }



    //    DataTable dt_LedgerTableTemp = new DataTable(ViewState["TableId"].ToString());
    //    dt_LedgerTableTemp.Columns.Add(new DataColumn("Type", typeof(string)));
    //    dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
    //    dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
    //    dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_Credit", typeof(decimal)));
    //    dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_Debit", typeof(decimal)));
    //    dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_TableID", typeof(decimal)));


    //    int gridRows = GridViewLedgerDetail.Rows.Count;
    //    for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
    //    {
    //        Label Ledger_ID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_ID");
    //        Label Type = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Type");
    //        Label Ledger_Name = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_Name");
    //        Label LedgerTx_Credit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Credit");
    //        Label LedgerTx_Debit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Debit");
    //        Label Ledger_TableID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_TableID");
    //        if (Ledger_TableID.Text != TableId.ToString())
    //        {

    //            dt_LedgerTableTemp.Rows.Add(Type.Text, Ledger_ID.Text, Ledger_Name.Text, LedgerTx_Credit.Text, LedgerTx_Debit.Text, Ledger_TableID.Text);
    //        }
    //    }
    //    GridViewLedgerDetail.DataSource = null;
    //    GridViewLedgerDetail.DataBind();
    //    GridViewLedgerDetail.DataSource = dt_LedgerTableTemp;
    //    GridViewLedgerDetail.DataBind();
    //    decimal LedgerCreditTotal = 0;
    //    decimal LedgerDebitTotal = 0;

    //    LedgerCreditTotal = dt_LedgerTableTemp.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
    //    LedgerDebitTotal = dt_LedgerTableTemp.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));

    //    GridViewLedgerDetail.FooterRow.Cells[3].Text = "<b>Total : </b>";
    //    GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
    //    GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";



    //    ViewState["LedgerCreditTotal"] = LedgerCreditTotal;
    //    ViewState["LedgerDebitTotal"] = LedgerDebitTotal;
    //    if (ViewState["LedgerCreditTotal"] == "0")
    //    {
    //        ddlcreditdebit.Enabled = false;
    //    }
    //    else
    //    {
    //        ddlcreditdebit.Enabled = true;
    //    }
    //    if (LedgerCreditTotal == LedgerDebitTotal && LedgerDebitTotal != 0)
    //    {
    //        btnAccept.Enabled = true;
    //    }
    //    else
    //    {
    //        btnAccept.Enabled = false;
    //    }

    //    ViewState["LedgerTable"] = dt_LedgerTableTemp;
    //    if (dt_LedgerTableTemp.Rows.Count == 0)
    //    {
    //        ddlcreditdebit.Enabled = false;
    //        ddlcreditdebit.SelectedValue = "Cr";
    //        lblname.InnerHtml = "Credit Amount";
    //        FillParticularsDropDown();

    //    }
    //}
    protected void ClearBillByBillModal()
    {
        ddlRefType.ClearSelection();
        ddlBillByBillTx_Ref.ClearSelection();
        txtBillByBillTx_Ref.Text = "";
        txDebtorAmt.Text = "";
        ViewState["BillByBillTable"] = "";

    }
    protected void ddlRefType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBillByBillData();
        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);
            if (ddlRefType.SelectedValue.ToString() == "1")
            {
                txtBillByBillTx_Ref.Visible = false;
                ddlBillByBillTx_Ref.Visible = true;
                lnkView.Visible = true;
                //txtBillByBillTx_Ref.Enabled = false;

            }
            else if (ddlRefType.SelectedValue.ToString() == "3")
            {
                txtBillByBillTx_Ref.Visible = true;
                ddlBillByBillTx_Ref.Visible = false;
                txtBillByBillTx_Ref.Enabled = false;
                txtBillByBillTx_Ref.Text = "On Account";
            }
            else
            {
                txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
                txtBillByBillTx_Ref.Visible = true;
                ddlBillByBillTx_Ref.Visible = false;
                lnkView.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewDebtor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
            int LedgerId = int.Parse(GridViewDebtor.SelectedDataKey.Value.ToString());
            int rowindex = int.Parse(GridViewDebtor.SelectedRow.RowIndex.ToString());
            Label lbl = (Label)GridViewDebtor.Rows[rowindex].FindControl("lblMaintainType");
            if (lbl.Text == "BillByBill")
            {
                GridViewBillByBillViewDetail.DataSource = dsBillByBill.Tables[LedgerId.ToString()];
                GridViewBillByBillViewDetail.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillByBillViewModal();", true);
            }

            else if (lbl.Text == "Cheque")
            {
                GVViewFinChequeTx.DataSource = dsBillByBill.Tables[LedgerId.ToString()];
                GVViewFinChequeTx.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetailView();", true);
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
    protected void GridViewDebtor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int LedgerID = int.Parse(GridViewDebtor.DataKeys[e.RowIndex].Value.ToString());
            DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
            DataSet dsBillByBillTemp = new DataSet();
            dsBillByBillTemp = dsBillByBill;
            for (int i = 0; i < dsBillByBillTemp.Tables.Count;


                i++)
            {
                if (dsBillByBillTemp.Tables[i].TableName == LedgerID.ToString())
                {
                    dsBillByBill.Tables.Remove(dsBillByBillTemp.Tables[i].TableName);
                    //dsBillByBill.Tables[i].Merge(dsBillByBillTemp.Tables[i]);
                }
            }



            DataTable dt_LedgerTableTemp = new DataTable();
            dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_Amount", typeof(decimal)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_MaintainType", typeof(string)));
            //dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_TableID", typeof(decimal)));


            int gridRows = GridViewDebtor.Rows.Count;
            for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {
                Label Ledger_ID = (Label)GridViewDebtor.Rows[rowIndex].Cells[0].FindControl("Ledger_ID");
                Label Ledger_Name = (Label)GridViewDebtor.Rows[rowIndex].Cells[0].FindControl("Ledger_Name");
                Label LedgerTx_Amount = (Label)GridViewDebtor.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Amount");
                Label lblMaintainType = (Label)GridViewDebtor.Rows[rowIndex].Cells[0].FindControl("lblMaintainType");
                //Label Ledger_TableID = (Label)GridViewDebtor.Rows[rowIndex].Cells[0].FindControl("Ledger_TableID");
                if (Ledger_ID.Text != LedgerID.ToString())
                {

                    dt_LedgerTableTemp.Rows.Add(Ledger_ID.Text, Ledger_Name.Text, LedgerTx_Amount.Text, lblMaintainType.Text);
                }
            }
            GridViewDebtor.DataSource = null;
            GridViewDebtor.DataBind();
            GridViewDebtor.DataSource = dt_LedgerTableTemp;
            GridViewDebtor.DataBind();

            if (dt_LedgerTableTemp.Rows.Count > 0)
            {
                decimal Amount = dt_LedgerTableTemp.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));
                ViewState["Amount"] = Amount;
                hfvalue.Value = ViewState["Amount"].ToString();
                GridViewDebtor.FooterRow.Cells[2].Text = "<b>| TOTAL |</b> ";
                GridViewDebtor.FooterRow.Cells[3].Text = "<b>" + Amount.ToString() + "</b>";
                GridViewDebtor.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            }

            ViewState["LedgerTable"] = dt_LedgerTableTemp;
            if (dt_LedgerTableTemp.Rows.Count == 0)
            {
                ViewState["Amount"] = "0";
                hfvalue.Value = ViewState["Amount"].ToString();

            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected string BillAmount(string ID)
    {
        string LedgerAmount = "0";
        try
        {


            DataTable dt_BillByBillTable = (DataTable)ViewState["BillByBillTable"];
            string Type = "";

            if (ddlBillByBillTx_crdr.SelectedValue == "Cr")
            {
                Type = "Cr";
            }
            else
            {
                Type = "Dr";
            }

            if (ddlRefType.SelectedItem.Text == "Agst Ref")
            {
                dt_BillByBillTable.Rows.Add(ddlRefType.SelectedItem.Text, ddlBillByBillTx_Ref.SelectedValue, txtBillByBillTx_Amount.Text, Type, ddlDebitLedger.SelectedValue.ToString());
                //DataTable dt_BillByBillData = (DataTable)ViewState["dt_BillByBillData"];
                //foreach (DataRow rows in dt_BillByBillData.Rows)
                //{
                //    if (rows["BillByBillTx_Ref"].ToString().Equals(ddlBillByBillTx_Ref.SelectedValue))
                //    {
                //        dt_BillByBillData.Rows.Remove(rows);
                //        dt_BillByBillData.AcceptChanges();
                //        break;
                //    }
                //}
                //ViewState["dt_BillByBillData"] = dt_BillByBillData;
                //ddlBillByBillTx_Ref.DataSource = dt_BillByBillData;
                //ddlBillByBillTx_Ref.DataTextField = "AgnstRef";
                //ddlBillByBillTx_Ref.DataValueField = "BillByBillTx_Ref";
                //ddlBillByBillTx_Ref.DataBind();
                //ddlBillByBillTx_Ref.Items.Insert(0, "Select");


            }
            else
            {
                dt_BillByBillTable.Rows.Add(ddlRefType.SelectedItem.Text, txtBillByBillTx_Ref.Text, txtBillByBillTx_Amount.Text, Type, ddlDebitLedger.SelectedValue.ToString());
            }

            if (ddlBillByBillTx_crdr.SelectedValue == "Cr")
            {

                Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(txtBillByBillTx_Amount.Text));
                ViewState["Amount"] = Amount.ToString();
                txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();


            }
            else
            {

                Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) + Convert.ToDecimal(txtBillByBillTx_Amount.Text));
                ViewState["Amount"] = Amount.ToString();
                txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();



            }


            GridViewBillByBillDetail.DataSource = dt_BillByBillTable;
            GridViewBillByBillDetail.DataBind();

            //decimal RefTotal = 0;
            //RefTotal = dt_BillByBillTable.AsEnumerable().Sum(row => row.Field<decimal>("BillByBillTx_Amount"));

            //GridViewBillByBillDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
            //GridViewBillByBillDetail.FooterRow.Cells[3].Text = "<b>" + RefTotal.ToString() + "</b>";

            //txtBillByBillTx_Amount.Text = (Convert.ToDecimal(txtLedgerTx_Amount.Text) - RefTotal).ToString();
            txtBillByBillTx_Ref.Text = "";
            ddlRefType.ClearSelection();
            if (dt_BillByBillTable.Rows.Count > 0)
            {

                decimal Amt = dt_BillByBillTable.AsEnumerable().Sum(row => row.Field<decimal>("BillByBillTx_Amount"));
                LedgerAmount = Amt.ToString();
            }

            ViewState["BillByBillTable"] = dt_BillByBillTable;



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        return ViewState["Amount"].ToString();
    }
    protected string ChequeAmount(string ID)
    {

        string CheqAmount = "0";
        try
        {
            DataTable dt_FinChequeTx = (DataTable)ViewState["FinChequeTx"];

            dt_FinChequeTx.Rows.Add(ddlDebitLedger.SelectedValue.ToString(), txtChequeTx_No.Text, txtChequeTx_Date.Text, txtChequeTx_Amount.Text);


            GVFinChequeTx.DataSource = dt_FinChequeTx;
            GVFinChequeTx.DataBind();

            decimal ChequeTx_AmountTotal = 0;

            ChequeTx_AmountTotal = dt_FinChequeTx.AsEnumerable().Sum(row => row.Field<decimal>("ChequeTx_Amount"));

            //GVFinChequeTx.FooterRow.Cells[2].Text = "<b>Total : </b>";
            //GVFinChequeTx.FooterRow.Cells[3].Text = "<b>" + ChequeTx_AmountTotal.ToString() + "</b>";

            txtChequeTx_Amount.Text = (Convert.ToDecimal(txDebtorAmt.Text) - ChequeTx_AmountTotal).ToString();

            txtChequeTx_No.Text = "";
            txtChequeTx_Date.Text = "";
            if (dt_FinChequeTx.Rows.Count > 0)
            {

                decimal Amt = dt_FinChequeTx.AsEnumerable().Sum(row => row.Field<decimal>("ChequeTx_Amount"));
                CheqAmount = Amt.ToString();
            }

            ViewState["FinChequeTx"] = dt_FinChequeTx;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        return CheqAmount.ToString();
    }
    protected void ClearFinChequeTxModal()
    {
        txtChequeTx_No.Text = "";
        txtChequeTx_Date.Text = "";
        txtChequeTx_Amount.Text = "";

        ViewState["FinChequeTx"] = "";

        GVFinChequeTx.DataSource = new string[] { };
        GVFinChequeTx.DataBind();


        ddlDebitLedger.ClearSelection();
        txDebtorAmt.Text = "";




    }
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        try
        {

            string msg = "";
            string Scheme = "0";
            if (txtVoucherTx_No.Text == "")
            {
                msg += "Enter Voucher/Bill No. \\n";
            }
            //if (txtVoucherTx_No.Text != "")
            //{
            //    string str = txtVoucherTx_No.Text;
            //    string substr = str.Substring(str.Length - 1);
            //    int res = 0;

            //    if (int.TryParse(substr, out res))
            //    {

            //    }
            //    else
            //    {
            //        msg += "Enter VALID Voucher/Bill No. \\n";
            //    }
            //}
            //if (txtVoucherTx_Ref.Text == "")
            //{
            //    msg += "Enter Reference No. \\n";
            //}
            if (txtVoucherTx_Date.Text == "")
            {
                msg += "Enter Date. \\n";
            }
            else
            {
                string ValidStatus = ValidDate();
                if (ValidStatus == "No")
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
            }
            //if (ddlDebitLedger.SelectedIndex == 0)
            //{
            //    msg += "Select Debit To Ledger . \\n";
            //}
            //if (txtNameofConsignee.Text == "")
            //{
            //    msg += "Enter Sold To. \\n";
            //}
            //if (ddlSalesCenter.SelectedIndex == 0)
            //{
            //    msg += "Select Sales Center . \\n";
            //}

            //if (txtVoucherTx_OrderNo.Text == "")
            //{
            //    msg += "Enter Order No. \\n";
            //}
            //if (txtVoucherTx_RegNo.Text == "")
            //{
            //    msg += "Enter Registration No. \\n";
            //}
            if (txtVoucherTx_Narration.Text == "")
            {
                msg += "Enter Narration. \\n";
            }
            if (msg == "")
            {
                string VoucherTxRef_No = txtVoucherTx_Ref.Text;
                string VoucherTx_No = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
                string sDate = (Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd")).ToString();
                DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
                int Month = int.Parse(datevalue.Month.ToString());
                int Year = int.Parse(datevalue.Year.ToString());
                string VoucherTx_IsActive = "1";
                string LedgerTx_IsActive = "1";
                string ItemTx_IsActive = "1";
                int Status = 0;
                DataSet ds11 = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "VoucherTx_No", "VoucherTx_ID" },
                    new string[] { "9", VoucherTx_No, ViewState["VoucherTx_ID"].ToString() }, "dataset");
                if (ds11.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds11.Tables[0].Rows[0]["Status"].ToString());

                }
                if (btnAccept.Text == "Accept" && ViewState["VoucherTx_ID"].ToString() == "0" && Status == 0)
                {
                    VoucherTx_IsActive = "0";
                    LedgerTx_IsActive = "0";
                    ItemTx_IsActive = "0";
                    //ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy", "VoucherTx_SalesCenterID", "SchemeTx_ID", "VoucherTx_SoldTo", "VoucherTx_OrderNo", "VoucherTx_RegNo", "VoucherTx_SNo" }, new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Sales Voucher", "CreditSale Voucher", txtVoucherTx_No.Text, txtVoucherTx_Ref.Text, txtVoucherTx_Narration.Text, lblGrandTotal.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), VoucherTx_IsActive, ViewState["Emp_ID"].ToString(), ddlSalesCenter.SelectedValue.ToString(), ddlScheme.SelectedValue.ToString(), txtNameofConsignee.Text, txtVoucherTx_OrderNo.Text, txtVoucherTx_RegNo.Text, ViewState["VoucherTx_SNo"].ToString() }, "dataset");
                    ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy", "VoucherTx_SNo", "GSTVoucher" }, new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Sales Return Voucher", "CreditNote Voucher", VoucherTx_No, VoucherTxRef_No, txtVoucherTx_Narration.Text, lblGrandTotal.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), VoucherTx_IsActive, ViewState["Emp_ID"].ToString(), txtVoucherTx_No.Text, "Yes" }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string VoucherTx_ID = ds.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
                        int rowItemIndex = 0;
                        int gridItemRows = GridViewItem.Rows.Count;
                        for (rowItemIndex = 0; rowItemIndex < gridItemRows; rowItemIndex++)
                        {
                            CheckBox chkboxid = (CheckBox)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("chkboxid");
                            Label lblItemID = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblItemID");
                            Label lblUnit_id = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblUnit_id");
                            Label lblWarehouse_id = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblWarehouse_id");
                            Label lblItem = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblItem");
                            Label lblQuantity = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblQuantity");
                            TextBox txtQuantity = (TextBox)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("txtQuantity");
                            TextBox lblRate = (TextBox)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblRate");
                            //Label lblRate = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblRate");
                            TextBox txtRate = (TextBox)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("txtRate");

                            txtRate.Text = lblRate.Text;

                            Label lblAmount = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblAmount");
                            TextBox txtAmountH = (TextBox)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("txtAmountH");
                            TextBox txtCGST = (TextBox)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("txtCGST");
                            TextBox txtSGST = (TextBox)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("txtSGST");
                            Label IGST = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("IGST");
                            Label lblCGST_Per = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblCGST_Per");
                            Label lblSGST_Per = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblSGST_Per");
                            Label lblIGSTPer = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblIGSTPer");
                            Label lblLedgerID = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblLedgerID");
                            Label lblHSNCode = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblHSNCode");
                            if (chkboxid.Checked == true)
                            {
                                string Amount = "-" + txtAmountH.Text;
                                objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Name", "VoucherTx_Type", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "HSN_Code", "IGST_Per", "CGST_Per", "SGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Warehouse_id", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy" },
                            new string[] { "0", VoucherTx_ID, lblLedgerID.Text, "Sales Return", "CreditNote Voucher", lblItemID.Text, lblUnit_id.Text, txtQuantity.Text, txtRate.Text, txtAmountH.Text, lblHSNCode.Text, lblIGSTPer.Text, lblCGST_Per.Text, lblSGST_Per.Text, txtCGST.Text, txtSGST.Text, IGST.Text, lblWarehouse_id.Text, ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), (rowItemIndex + 1).ToString() }, "dataset");
                                ds = objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "Ledger_ID" }, new string[] { "3", lblLedgerID.Text }, "dataset");
                                if (ds != null && ds.Tables[0].Rows.Count > 0)
                                {
                                    if (ds.Tables[0].Rows[0]["InventoryAffected"].ToString() == "Yes")
                                    {
                                        string iAmount = "-" + txtAmountH.Text;
                                        string iQuantity = "-" + txtQuantity.Text;
                                        objdb.ByProcedure("SpFinItemTx",
                                            new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "TransactionFrom", "InvoiceNo", "Office_Id", "Warehouse_id", "CreatedBy", "TranDt", "Amount" }
                                           , new string[] { "4", lblItemID.Text, "0", iQuantity, txtRate.Text, VoucherTx_ID, "CreditNote Voucher", VoucherTx_No, ViewState["Office_ID"].ToString(), lblWarehouse_id.Text, ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), iAmount }, "dataset");
                                    }
                                }
                                objdb.ByProcedure("SpFinLedgerTx",
                                new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
                                new string[] { "0", lblLedgerID.Text, "Item Ledger", VoucherTx_ID, lblItemID.Text, "CreditNote Voucher", Amount, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (rowItemIndex + 1).ToString() }, "dataset");
                            }

                        }

                        int gridRows = GridViewLedger.Rows.Count;
                        for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                        {

                            Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                            TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                            if (txtAmount.Text == "")
                            {
                                txtAmount.Text = "0";
                            }
                            string Amount = "0";

                            if (decimal.Parse(txtAmount.Text) >= 0)
                                Amount = "-" + txtAmount.Text;
                            else
                                Amount = Math.Abs(decimal.Parse(txtAmount.Text)).ToString();

                            objdb.ByProcedure("SpFinLedgerTx",
                            new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
                            new string[] { "0", lblID.Text, "Sub Ledger", VoucherTx_ID, "CreditNote Voucher", Amount.ToString(), Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 1).ToString() }, "dataset");

                        }

                        //
                        int LedgerTable = GridViewDebtor.Rows.Count;
                        for (int i = 0; i < LedgerTable; i++)
                        {
                            Label lbltype = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("lblMaintainType");
                            if (lbltype.Text == "BillByBill")
                            {
                                Label Ledger_ID = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("Ledger_ID");

                                Label LedgerTx_Amount = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("LedgerTx_Amount");
                                string LedgerTxAmount = LedgerTx_Amount.Text;
                                int TableId = int.Parse(Ledger_ID.Text);

                                objdb.ByProcedure("SpFinLedgerTx",
                                new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                                new string[] { "0", Ledger_ID.Text, "Main Ledger", VoucherTx_ID, "CreditNote Voucher", LedgerTxAmount, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (i + 1).ToString(), "BillByBill" }, "dataset");
                                DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                                DataSet dsBillByBillTemp = new DataSet();
                                dsBillByBillTemp = dsBillByBill;
                                for (int j = 0; j < dsBillByBillTemp.Tables.Count; j++)
                                {
                                    if (dsBillByBillTemp.Tables[j].TableName == TableId.ToString())
                                    {
                                        for (int k = 0; k < dsBillByBillTemp.Tables[j].Rows.Count; k++)
                                        {
                                            string Type = dsBillByBillTemp.Tables[j].Rows[k]["Type"].ToString();
                                            string BillByBillTx_RefType = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_RefType"].ToString();
                                            string BillByBillTx_Ref = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_Ref"].ToString();
                                            string BillByBillTx_Amount = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_Amount"].ToString();
                                            if (Type == "Dr")
                                            {
                                                BillByBillTx_Amount = "-" + BillByBillTx_Amount;
                                            }

                                            objdb.ByProcedure("SpFinBillByBillTx",
                                            new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy" },
                                            new string[] { "3", VoucherTx_ID, Ledger_ID.Text, BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), "0", (k + 1).ToString() }, "dataset");
                                        }
                                    }
                                }
                            }
                            else if (lbltype.Text == "Cheque")
                            {
                                Label Ledger_ID = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("Ledger_ID");

                                Label LedgerTx_Amount = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("LedgerTx_Amount");
                                string LedgerTxAmount = LedgerTx_Amount.Text;


                                int TableId = int.Parse(Ledger_ID.Text);

                                objdb.ByProcedure("SpFinLedgerTx",
                                new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                                new string[] { "0", Ledger_ID.Text, "Main Ledger", VoucherTx_ID, "CreditNote Voucher", LedgerTxAmount, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (i + 1).ToString(), "Cheque" }, "dataset");
                                DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                                DataSet dsBillByBillTemp = new DataSet();
                                dsBillByBillTemp = dsBillByBill;
                                for (int j = 0; j < dsBillByBillTemp.Tables.Count; j++)
                                {
                                    if (dsBillByBillTemp.Tables[j].TableName == TableId.ToString())
                                    {
                                        for (int k = 0; k < dsBillByBillTemp.Tables[j].Rows.Count; k++)
                                        {
                                            string ChequeTx_No = dsBillByBill.Tables[j].Rows[k]["ChequeTx_No"].ToString();
                                            if (ChequeTx_No == "")
                                            {
                                                ChequeTx_No = null;
                                            }
                                            else
                                            {

                                            }
                                            string ChequeTx_Date = dsBillByBill.Tables[j].Rows[k]["ChequeTx_Date"].ToString();
                                            if (ChequeTx_Date == "")
                                            {
                                                ChequeTx_Date = null;
                                            }
                                            else
                                            {
                                                ChequeTx_Date = Convert.ToDateTime(ChequeTx_Date, cult).ToString("yyyy/MM/dd");
                                            }
                                            string ChequeTx_Amount = dsBillByBill.Tables[j].Rows[k]["ChequeTx_Amount"].ToString();
                                            objdb.ByProcedure("SpFinChequeTx",
                                            new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy" },
                                            new string[] { "1", VoucherTx_ID, Ledger_ID.Text, "CreditNote Voucher", ChequeTx_No, ChequeTx_Date, ChequeTx_Amount, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), "0", ViewState["Emp_ID"].ToString(), (k + 1).ToString() }, "dataset");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Label Ledger_ID = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("Ledger_ID");

                                Label LedgerTx_Amount = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("LedgerTx_Amount");

                                string LedgerTxAmount = LedgerTx_Amount.Text;


                                // int TableId = int.Parse(Ledger_ID.Text);

                                objdb.ByProcedure("SpFinLedgerTx",
                                new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                                new string[] { "0", Ledger_ID.Text, "Main Ledger", VoucherTx_ID, "CreditNote Voucher", LedgerTxAmount, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (i + 1).ToString(), "None" }, "dataset");
                            }

                        }
                        objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "40", VoucherTx_ID }, "dataset");

                    }
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                    ClearText();

                }
                else if (btnAccept.Text == "Update" && ViewState["VoucherTx_ID"].ToString() != "0" && Status == 0)
                {
                    // objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_InsertedBy", "VoucherTx_SalesCenterID", "SchemeTx_ID", "VoucherTx_SoldTo", "VoucherTx_OrderNo", "VoucherTx_RegNo" }, new string[] { "7", ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Sales Voucher", "CreditSale Voucher", txtVoucherTx_No.Text, txtVoucherTx_Ref.Text, txtVoucherTx_Narration.Text, lblGrandTotal.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Emp_ID"].ToString(), ddlSalesCenter.SelectedValue.ToString(), ddlScheme.SelectedValue.ToString(), txtNameofConsignee.Text, txtVoucherTx_OrderNo.Text, txtVoucherTx_RegNo.Text }, "dataset");
                    // objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_InsertedBy", "VoucherTx_SalesCenterID", "SchemeTx_ID", "VoucherTx_SoldTo", "VoucherTx_OrderNo", "VoucherTx_RegNo" }, new string[] { "7", ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Sales Voucher", "CreditSale Voucher", VoucherTx_No, txtVoucherTx_Ref.Text, txtVoucherTx_Narration.Text, lblGrandTotal.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Emp_ID"].ToString(), "0", Scheme, txtNameofConsignee.Text, txtVoucherTx_OrderNo.Text, txtVoucherTx_RegNo.Text }, "dataset");
                    //objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "1", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                    //int rowItemIndex = 0;
                    //int gridItemRows = GridViewItem.Rows.Count;
                    //for (rowItemIndex = 0; rowItemIndex < gridItemRows; rowItemIndex++)
                    //{

                    //    Label lblItemID = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblItemID");
                    //    Label lblUnit_id = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblUnit_id");
                    //    Label lblWarehouse_id = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblWarehouse_id");
                    //    Label lblItem = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblItem");
                    //    Label lblQuantity = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblQuantity");
                    //    Label lblRate = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblRate");
                    //    Label lblAmount = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblAmount");

                    //    //objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Name", "VoucherTx_Type", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "Warehouse_id", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy" }, new string[] { "0", ViewState["VoucherTx_ID"].ToString(), "Sales Voucher", "CreditSale Voucher", lblItemID.Text, lblUnit_id.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, ddlWarehouse.SelectedValue.ToString(), ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), (rowItemIndex + 1).ToString() }, "dataset");
                    //}
                    //objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");

                    //int gridRows = GridViewLedger.Rows.Count;
                    //for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                    //{
                    //    string LedgerTx_Amount = "";
                    //    Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                    //    TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                    //    //if (txtAmount.Text.Contains("-"))
                    //    //{
                    //    //    LedgerTx_Amount = txtAmount.Text.Replace(@"-", string.Empty);
                    //    //}
                    //    //else
                    //    //{
                    //    //    LedgerTx_Amount = "-" + txtAmount.Text;
                    //    //}
                    //    objdb.ByProcedure("SpFinLedgerTx",
                    //    new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
                    //    new string[] { "0", lblID.Text, "Sub Ledger", ViewState["VoucherTx_ID"].ToString(), "CreditNote Voucher", LedgerTx_Amount.ToString(), Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 2).ToString() }, "dataset");

                    //}
                    //DataSet DS_GridViewParticulars = (DataSet)ViewState["DS_GridViewParticulars"];
                    //for (int i = 0; i < DS_GridViewParticulars.Tables.Count; i++)
                    //{
                    //    for (int j = 0; j < DS_GridViewParticulars.Tables[i].Rows.Count; j++)
                    //    {
                    //        string ParticularID = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularID"].ToString();
                    //        string Amount = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularAmt"].ToString();
                    //        string Item_id = DS_GridViewParticulars.Tables[i].Rows[j]["Item_ID"].ToString();
                    //        Amount = "-" + Amount.ToString();
                    //        objdb.ByProcedure("SpFinLedgerTx",
                    //        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
                    //        new string[] { "0", ParticularID, "Item Ledger", ViewState["VoucherTx_ID"].ToString(), Item_id, "CreditSale Voucher", Amount, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (j + 1).ToString() }, "dataset");
                    //    }
                    //}
                    //objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "4", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                    //objdb.ByProcedure("SpFinChequeTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                    //int LedgerTable = GridViewDebtor.Rows.Count;
                    //for (int i = 0; i < LedgerTable; i++)
                    //{
                    //    Label lbltype = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("lblMaintainType");
                    //    if (lbltype.Text == "BillByBill")
                    //    {
                    //        Label Ledger_ID = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("Ledger_ID");

                    //        Label LedgerTx_Amount = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("LedgerTx_Amount");
                    //        string LedgerTxAmount = LedgerTx_Amount.Text;                           
                    //        int TableId = int.Parse(Ledger_ID.Text);

                    //        objdb.ByProcedure("SpFinLedgerTx",
                    //        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                    //        new string[] { "0", Ledger_ID.Text, "Main Ledger", ViewState["VoucherTx_ID"].ToString(), "CreditSale Voucher", LedgerTxAmount, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (i + 1).ToString(), "BillByBill" }, "dataset");
                    //        DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                    //        DataSet dsBillByBillTemp = new DataSet();
                    //        dsBillByBillTemp = dsBillByBill;
                    //        for (int j = 0; j < dsBillByBillTemp.Tables.Count; j++)
                    //        {
                    //            if (dsBillByBillTemp.Tables[j].TableName == TableId.ToString())
                    //            {
                    //                for (int k = 0; k < dsBillByBillTemp.Tables[j].Rows.Count; k++)
                    //                {
                    //                    string Type = dsBillByBillTemp.Tables[j].Rows[k]["Type"].ToString();
                    //                    string BillByBillTx_RefType = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_RefType"].ToString();
                    //                    string BillByBillTx_Ref = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_Ref"].ToString();
                    //                    string BillByBillTx_Amount = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_Amount"].ToString();
                    //                    if (Type == "Dr")
                    //                    {
                    //                        BillByBillTx_Amount = "-" + BillByBillTx_Amount;
                    //                    }

                    //                    objdb.ByProcedure("SpFinBillByBillTx",
                    //                    new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy" },
                    //                    new string[] { "3", ViewState["VoucherTx_ID"].ToString(), Ledger_ID.Text, BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), "1", (k + 1).ToString() }, "dataset");
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else if (lbltype.Text == "Cheque")
                    //    {
                    //        Label Ledger_ID = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("Ledger_ID");

                    //        Label LedgerTx_Amount = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("LedgerTx_Amount");
                    //        string LedgerTxAmount = LedgerTx_Amount.Text;
                    //        //if (lblGrandTotal.Text.Contains("-"))
                    //        //{
                    //        //    LedgerTxAmount = LedgerTxAmount.Replace(@"-", string.Empty);
                    //        //}
                    //        //else
                    //        //{
                    //        //    LedgerTxAmount = "-" + LedgerTxAmount;
                    //        //}


                    //        int TableId = int.Parse(Ledger_ID.Text);

                    //        objdb.ByProcedure("SpFinLedgerTx",
                    //        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                    //        new string[] { "0", Ledger_ID.Text, "Main Ledger", ViewState["VoucherTx_ID"].ToString(), "CreditSale Voucher", LedgerTxAmount, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (i + 1).ToString(), "Cheque" }, "dataset");
                    //        DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                    //        DataSet dsBillByBillTemp = new DataSet();
                    //        dsBillByBillTemp = dsBillByBill;
                    //        for (int j = 0; j < dsBillByBillTemp.Tables.Count; j++)
                    //        {
                    //            if (dsBillByBillTemp.Tables[j].TableName == TableId.ToString())
                    //            {
                    //                for (int k = 0; k < dsBillByBillTemp.Tables[j].Rows.Count; k++)
                    //                {
                    //                    string ChequeTx_No = dsBillByBill.Tables[j].Rows[k]["ChequeTx_No"].ToString();
                    //                    if (ChequeTx_No == "")
                    //                    {
                    //                        ChequeTx_No = null;
                    //                    }
                    //                    else
                    //                    {

                    //                    }
                    //                    string ChequeTx_Date = dsBillByBill.Tables[j].Rows[k]["ChequeTx_Date"].ToString();
                    //                    if (ChequeTx_Date == "")
                    //                    {
                    //                        ChequeTx_Date = null;
                    //                    }
                    //                    else
                    //                    {
                    //                        ChequeTx_Date = Convert.ToDateTime(ChequeTx_Date, cult).ToString("yyyy/MM/dd");
                    //                    }
                    //                    string ChequeTx_Amount = dsBillByBill.Tables[j].Rows[k]["ChequeTx_Amount"].ToString();
                    //                    objdb.ByProcedure("SpFinChequeTx",
                    //                    new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy" },
                    //                    new string[] { "1", ViewState["VoucherTx_ID"].ToString(), Ledger_ID.Text, "CreditSale Voucher", ChequeTx_No, ChequeTx_Date, ChequeTx_Amount, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), (k + 1).ToString() }, "dataset");
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Label Ledger_ID = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("Ledger_ID");

                    //        Label LedgerTx_Amount = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("LedgerTx_Amount");
                    //        string LedgerTxAmount = LedgerTx_Amount.Text;                           
                    //        objdb.ByProcedure("SpFinLedgerTx",
                    //        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                    //        new string[] { "0", Ledger_ID.Text, "Main Ledger", ViewState["VoucherTx_ID"].ToString(), "CreditSale Voucher", LedgerTxAmount, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (i + 1).ToString(), "None" }, "dataset");
                    //    }

                    //}

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Voucher No is already exist.');", true);
                    //ClearData();
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
    //protected void btnAccept_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
    //        txtBillByBillTx_Ref.Visible = true;
    //        ddlBillByBillTx_Ref.Visible = false;
    //        txtBillByBillTx_Ref.Enabled = true;
    //        lnkView.Visible = false;

    //        string VoucherTx_No = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
    //        lblMsg.Text = "";
    //        string msg = "";
    //        if (txtVoucherTx_No.Text == "")
    //        {
    //            msg += "Enter Voucher No. \\n";
    //        }

    //        if (txtVoucherTx_Date.Text == "")
    //        {
    //            msg += "Enter Date. \\n";
    //        }
    //        if (ddlPartyName.SelectedIndex == 0)
    //        {
    //            msg += "Select Party A/c Name. \\n";
    //        }
    //        if (GridViewItem.Rows.Count == 0)
    //        {
    //            msg += "Enter item Detail. \\n";
    //        }
    //        if (txtVoucherTx_Narration.Text == "")
    //        {
    //            msg += "Enter Narration. \\n";
    //        }
    //        if (msg.Trim() == "")
    //        {

    //          string LedgerId = ddlPartyName.SelectedValue.ToString();
    //          int Status = 0;
    //          DataSet ds11 = objdb.ByProcedure("SpFinVoucherTx",
    //              new string[] { "flag", "VoucherTx_No", "VoucherTx_ID" },
    //              new string[] { "9", VoucherTx_No, ViewState["VoucherTx_ID"].ToString() }, "dataset");
    //          if (ds11.Tables[0].Rows.Count > 0)
    //          {
    //              Status = Convert.ToInt32(ds11.Tables[0].Rows[0]["Status"].ToString());

    //          }
    //          if (btnAccept.Text == "Accept" && ViewState["VoucherTx_ID"].ToString() == "0" && Status == 0)
    //          {
    //              ViewState["Amount"] = lblGrandTotal.Text;
    //              if (ViewState["VoucherTx_Type"].ToString() == "CreditSale Voucher")
    //              {
    //                  ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", LedgerId, ViewState["Office_ID"].ToString() }, "dataset");
    //                  if (ds.Tables[0].Rows.Count > 0)
    //                  {
    //                      if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "Yes")
    //                      {
    //                          txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
    //                          txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();
    //                          ddlBillByBillTx_crdr.SelectedValue = "Cr";
    //                          ddlRefType.SelectedValue = "2";
    //                          //    txtRefAmount.Text = lblGrandTotal.Text;
    //                          // CreateBillByBillTable();
    //                          GridViewRef.DataSource = new string[] { };
    //                          GridViewRef.DataBind();
    //                          Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);


    //                      }
    //                      else if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "BankLedger")
    //                      {
    //                          btnAddCheque.Enabled = true;
    //                          //btnAddChequeDetail.Enabled = false;
    //                          CreatTableFinChequeTx();
    //                          txtChequeTx_Amount.Text = ViewState["Amount"].ToString();
    //                          Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
    //                      }
    //                      else
    //                      {
    //                          Save("None");
    //                      }

    //                  }

    //              }
    //              else
    //              {
    //                  Save("None");
    //              }




    //          }
    //          else if (btnAccept.Text == "Update")
    //          {
    //              if (ViewState["Amount"].ToString() == lblGrandTotal.Text)
    //              {
    //                  txtBillByBillTx_Amount.Text = "0.00";
    //                  btnAddBillByBill.Enabled = false;
    //                  btnSubmit.Enabled = true;
    //              }
    //              else
    //              {
    //                  ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", LedgerId, ViewState["Office_ID"].ToString() }, "dataset");
    //                  if (ds.Tables[0].Rows.Count > 0)
    //                  {
    //                      if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "Yes")
    //                      {
    //                          decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(lblGrandTotal.Text));
    //                          txtBillByBillTx_Amount.Text = Amount.ToString();
    //                          btnAddBillByBill.Enabled = true;
    //                          btnSubmit.Enabled = false;
    //                          txtBillByBillTx_Ref.Text = txtVoucherTx_No.Text;
    //                          ddlBillByBillTx_crdr.SelectedValue = "Cr";
    //                          //    txtRefAmount.Text = lblGrandTotal.Text;
    //                          // CreateBillByBillTable();

    //                          Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);


    //                      }
    //                      else if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "BankLedger")
    //                      {
    //                          btnAddCheque.Enabled = true;
    //                          //btnAddChequeDetail.Enabled = false;
    //                          CreatTableFinChequeTx();
    //                          txtChequeTx_Amount.Text = ViewState["Amount"].ToString();
    //                          Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
    //                      }
    //                      else
    //                      {
    //                          Save("None");
    //                      }



    //                  }

    //              }

    //          }
    //          else
    //          {
    //              Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Voucher No is already exist.');", true);
    //          }
    //        }
    //        else
    //        {
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }

    //}
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    //try
    //    //{
    //    //    lblMsg.Text = "";
    //    //    string msg = "";
    //    //    if (txtVoucherTx_No.Text == "")
    //    //    {
    //    //        msg += "Enter Voucher No. \\n";
    //    //    }
    //    //    if (txtVoucherTx_Date.Text == "")
    //    //    {
    //    //        msg += "Enter Date. \\n";
    //    //    }
    //    //    if (ddlPartyName.SelectedIndex == 0)
    //    //    {
    //    //        msg += "Select Party A/c Name. \\n";
    //    //    }
    //    //    if (GridViewItem.Rows.Count == 0)
    //    //    {
    //    //        msg += "Add item Detail. \\n";
    //    //    }
    //    //    //float GrandTotal = float.Parse(lblGrandTotal.Text);
    //    //    //float RefTotal = float.Parse(lblRefTotal.Text);
    //    //    //if (GrandTotal != RefTotal)
    //    //    //{
    //    //    //    msg += "Amount Not Clear. \\n";
    //    //    //}
    //    //    if (msg.Trim() == "")
    //    //    {
    //    //        string VoucherTx_No = txtVoucherTx_No.Text;
    //    //        string sDate = (Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd")).ToString();
    //    //        DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
    //    //        int Month = int.Parse(datevalue.Month.ToString());
    //    //        int Year = int.Parse(datevalue.Year.ToString());
    //    //        int FY = Year;
    //    //        string FinancialYear = Year.ToString();
    //    //        string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
    //    //        FinancialYear = "";
    //    //        if (Month <= 3)
    //    //        {
    //    //            FY = Year - 1;
    //    //            FinancialYear = FY.ToString() + "-" + LFY.ToString();
    //    //        }
    //    //        else
    //    //        {

    //    //            FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
    //    //        }


    //    //        string VoucherTx_Type = "GSTGoods Purchase";
    //    //        string VoucherTx_Name = "Purchase Voucher";
    //    //        string VoucherTx_Ref = txtInvoice.Text;
    //    //        string VoucherTx_Amount = lblGrandTotal.Text;
    //    //        string ItemTx_IsActive = "1";
    //    //        string LedgerTx_IsActive = "1";
    //    //        string VoucherTx_IsActive = "1";
    //    //        int Status = 0;
    //    //        DataSet ds11 = objdb.ByProcedure("SpFinVoucherTx",
    //    //            new string[] { "flag", "VoucherTx_No", "VoucherTx_ID" },
    //    //            new string[] { "9", txtVoucherTx_No.Text, ViewState["VoucherTx_ID"].ToString() }, "dataset");
    //    //        if (ds11.Tables[0].Rows.Count > 0)
    //    //        {
    //    //            Status = Convert.ToInt32(ds11.Tables[0].Rows[0]["Status"].ToString());

    //    //        }
    //    //        if (btnAccept.Text == "Accept" && ViewState["VoucherTx_ID"].ToString() == "0" && Status == 0)
    //    //        {
    //    //            DataSet ds2 = objdb.ByProcedure("SpFinVoucherTx",
    //    //          new string[] { "flag","VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type"
    //    //            , "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY"
    //    //            , "VoucherTx_IsActive", "VoucherTx_InsertedBy","VoucherTx_SNo" },
    //    //          new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), VoucherTx_Name, VoucherTx_Type,VoucherTx_No
    //    //           ,VoucherTx_Ref,txtVoucherTx_Narration.Text,lblGrandTotal.Text,Month.ToString(),Year.ToString(), ViewState["Office_ID"].ToString(),ViewState["Voucher_FY"].ToString()
    //    //            ,VoucherTx_IsActive,ViewState["Emp_ID"].ToString(),txtVoucherTx_No.Text}, "dataset");


    //    //            if (ds2.Tables[0].Rows.Count > 0)
    //    //            {
    //    //                string VoucherTx_ID = ds2.Tables[0].Rows[0]["VoucherTx_ID"].ToString();

    //    //                objdb.ByProcedure("SpFinLedgerTx",
    //    //                new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
    //    //                , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy" },
    //    //                new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
    //    //                ,ViewState["Voucher_FY"].ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1"}, "dataset");

    //    //                int gridRows = GridViewItem.Rows.Count;
    //    //                for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
    //    //                {
    //    //                    Label lblID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblID");
    //    //                    Label lblItemID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
    //    //                    Label lblUnit_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblUnit_id");
    //    //                    Label lblWarehouse_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_id");
    //    //                    Label lblItem = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItem");
    //    //                    Label lblQuantity = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblQuantity");
    //    //                    Label lblRate = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblRate");
    //    //                    Label lblAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblAmount");
    //    //                    Label lblCGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblCGST");
    //    //                    Label lblSGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblSGST");
    //    //                    //Label lblTotalAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblTotalAmount");
    //    //                    objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Name", "VoucherTx_Type", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "Warehouse_id", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy" }, new string[] { "0", VoucherTx_ID, VoucherTx_Name, VoucherTx_Type, lblItemID.Text, lblUnit_id.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, lblWarehouse_id.Text, ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 1).ToString() }, "dataset");



    //    //                    //DataTable dt = DS_GridViewParticulars.Tables[lblID.Text];
    //    //                    //int dtRowCount = dt.Rows.Count;
    //    //                    //for (int i = 0; i < dtRowCount; i++)
    //    //                    //{
    //    //                    //    Label lblLedger_ID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblParticularID");
    //    //                    //    Label lblLedgerAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblAmount");

    //    //                    //    objdb.ByProcedure("SpFinLedgerTx",
    //    //                    //    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
    //    //                    //        , "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy" },
    //    //                    //    new string[] { "0", lblLedger_ID.Text,VoucherTx_ID,VoucherTx_Type,lblLedgerAmount.Text ,Month.ToString(),Year.ToString()
    //    //                    //        ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() ,"1",ViewState["Emp_ID"].ToString()}, "dataset");
    //    //                    //}



    //    //                    //int dtRowCount = GridViewLedger.Rows.Count;    
    //    //                    //for (int i = 0; i < dtRowCount; i++)
    //    //                    //{
    //    //                    //    Label lblLedger_ID = (Label)GridViewLedger.Rows[i].Cells[0].FindControl("lblID");
    //    //                    //    TextBox lblLedgerAmount = (TextBox)GridViewLedger.Rows[i].Cells[1].FindControl("txtAmount");

    //    //                    //    objdb.ByProcedure("SpFinLedgerTx",
    //    //                    //    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
    //    //                    //        , "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy" },
    //    //                    //    new string[] { "0", lblLedger_ID.Text,VoucherTx_ID,VoucherTx_Type,lblLedgerAmount.Text ,Month.ToString(),Year.ToString()
    //    //                    //        ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() ,"1",ViewState["Emp_ID"].ToString()}, "dataset");
    //    //                    //}

    //    //                }
    //    //                int gridLedgerRows = GridViewLedger.Rows.Count;
    //    //                for (int rowIndex = 0; rowIndex < gridLedgerRows; rowIndex++)
    //    //                {

    //    //                    Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
    //    //                    TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
    //    //                    objdb.ByProcedure("SpFinLedgerTx",
    //    //                    new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
    //    //                    new string[] { "0", lblID.Text, "Sub Ledger", VoucherTx_ID, VoucherTx_Type, txtAmount.Text, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 1).ToString() }, "dataset");

    //    //                }
    //    //                DataSet DS_GridViewParticulars = (DataSet)ViewState["DS_GridViewParticulars"];
    //    //                for (int i = 0; i < DS_GridViewParticulars.Tables.Count; i++)
    //    //                {
    //    //                    for (int j = 0; j < DS_GridViewParticulars.Tables[i].Rows.Count; j++)
    //    //                    {
    //    //                        string ParticularID = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularID"].ToString();
    //    //                        string Amount = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularAmt"].ToString();
    //    //                        string Item_id = DS_GridViewParticulars.Tables[i].Rows[j]["Item_ID"].ToString();
    //    //                        objdb.ByProcedure("SpFinLedgerTx",
    //    //                        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
    //    //                        new string[] { "0", ParticularID, "Item Ledger", VoucherTx_ID, Item_id, VoucherTx_Type, Amount, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (j + 1).ToString() }, "dataset");
    //    //                    }
    //    //                }
    //    //                int gridBillbyBillRows = GridViewRef.Rows.Count;
    //    //                for (int k = 0; k < gridBillbyBillRows; k++)
    //    //                {


    //    //                    Label BillByBillTx_RefType = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblTypeOfRef");
    //    //                    Label BillByBillTx_Ref = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblRefNo");
    //    //                    Label BillByBillTx_Amount = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblAmount");
    //    //                    Label Type = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblType");
    //    //                    if (Type.Text == "Dr")
    //    //                    {
    //    //                        BillByBillTx_Amount.Text = "-" + BillByBillTx_Amount.Text;
    //    //                    }

    //    //                    objdb.ByProcedure("SpFinBillByBillTx",
    //    //                                new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy" },
    //    //                                new string[] { "3", VoucherTx_ID, ddlPartyName.SelectedValue.ToString(), BillByBillTx_RefType.Text, BillByBillTx_Ref.Text, BillByBillTx_Amount.Text, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), "1", (k + 1).ToString() }, "dataset");

    //    //                }
    //    //            }

    //    //            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
    //    //            ClearData();
    //    //        }
    //    //        else if (btnAccept.Text == "Update" && ViewState["VoucherTx_ID"].ToString() != "0" && Status == 0)
    //    //        {
    //    //            DataSet ds2 = objdb.ByProcedure("SpFinVoucherTx",
    //    //        new string[] { "flag","VoucherTx_ID","VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type"
    //    //            , "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY"
    //    //            , "VoucherTx_IsActive", "VoucherTx_InsertedBy" },
    //    //        new string[] { "7", ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), VoucherTx_Name, VoucherTx_Type,txtVoucherTx_No.Text
    //    //           ,VoucherTx_Ref,txtVoucherTx_Narration.Text,lblGrandTotal.Text,Month.ToString(),Year.ToString(), ViewState["Office_ID"].ToString(),ViewState["Voucher_FY"].ToString()
    //    //            ,VoucherTx_IsActive,ViewState["Emp_ID"].ToString()}, "dataset");


    //    //            //delete previous record//
    //    //            objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "1", ViewState["VoucherTx_ID"].ToString() }, "dataset");
    //    //            objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
    //    //            objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "4", ViewState["VoucherTx_ID"].ToString() }, "dataset");
    //    //            /////////////////////////////////////////////////////////////////////////////////////////
    //    //            objdb.ByProcedure("SpFinLedgerTx",
    //    //              new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
    //    //                , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy" },
    //    //              new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",ViewState["VoucherTx_ID"].ToString(),VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
    //    //                ,ViewState["Voucher_FY"].ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1"}, "dataset");
    //    //            int gridRows = GridViewItem.Rows.Count;
    //    //            for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
    //    //            {
    //    //                Label lblID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblID");
    //    //                Label lblItemID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
    //    //                Label lblUnit_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblUnit_id");
    //    //                Label lblWarehouse_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_id");
    //    //                Label lblItem = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItem");
    //    //                Label lblQuantity = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblQuantity");
    //    //                Label lblRate = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblRate");
    //    //                Label lblAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblAmount");
    //    //                Label lblCGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblCGST");
    //    //                Label lblSGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblSGST");
    //    //                //Label lblTotalAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblTotalAmount");
    //    //                objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Name", "VoucherTx_Type", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "Warehouse_id", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy" }, new string[] { "0", ViewState["VoucherTx_ID"].ToString(), VoucherTx_Name, VoucherTx_Type, lblItemID.Text, lblUnit_id.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, lblWarehouse_id.Text, ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 1).ToString() }, "dataset");



    //    //                //DataTable dt = DS_GridViewParticulars.Tables[lblID.Text];
    //    //                //int dtRowCount = dt.Rows.Count;
    //    //                //for (int i = 0; i < dtRowCount; i++)
    //    //                //{
    //    //                //    Label lblLedger_ID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblParticularID");
    //    //                //    Label lblLedgerAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblAmount");

    //    //                //    objdb.ByProcedure("SpFinLedgerTx",
    //    //                //    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
    //    //                //        , "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy" },
    //    //                //    new string[] { "0", lblLedger_ID.Text,VoucherTx_ID,VoucherTx_Type,lblLedgerAmount.Text ,Month.ToString(),Year.ToString()
    //    //                //        ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() ,"1",ViewState["Emp_ID"].ToString()}, "dataset");
    //    //                //}



    //    //                //int dtRowCount = GridViewLedger.Rows.Count;    
    //    //                //for (int i = 0; i < dtRowCount; i++)
    //    //                //{
    //    //                //    Label lblLedger_ID = (Label)GridViewLedger.Rows[i].Cells[0].FindControl("lblID");
    //    //                //    TextBox lblLedgerAmount = (TextBox)GridViewLedger.Rows[i].Cells[1].FindControl("txtAmount");

    //    //                //    objdb.ByProcedure("SpFinLedgerTx",
    //    //                //    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
    //    //                //        , "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy" },
    //    //                //    new string[] { "0", lblLedger_ID.Text,VoucherTx_ID,VoucherTx_Type,lblLedgerAmount.Text ,Month.ToString(),Year.ToString()
    //    //                //        ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() ,"1",ViewState["Emp_ID"].ToString()}, "dataset");
    //    //                //}

    //    //            }
    //    //            int gridLedgerRows = GridViewLedger.Rows.Count;
    //    //            for (int rowIndex = 0; rowIndex < gridLedgerRows; rowIndex++)
    //    //            {

    //    //                Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
    //    //                TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
    //    //                objdb.ByProcedure("SpFinLedgerTx",
    //    //                new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
    //    //                new string[] { "0", lblID.Text, "Sub Ledger", ViewState["VoucherTx_ID"].ToString(), VoucherTx_Type, txtAmount.Text, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 1).ToString() }, "dataset");

    //    //            }
    //    //            DataSet DS_GridViewParticulars = (DataSet)ViewState["DS_GridViewParticulars"];
    //    //            for (int i = 0; i < DS_GridViewParticulars.Tables.Count; i++)
    //    //            {
    //    //                for (int j = 0; j < DS_GridViewParticulars.Tables[i].Rows.Count; j++)
    //    //                {
    //    //                    string ParticularID = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularID"].ToString();
    //    //                    string Amount = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularAmt"].ToString();
    //    //                    string Item_id = DS_GridViewParticulars.Tables[i].Rows[j]["Item_ID"].ToString();
    //    //                    objdb.ByProcedure("SpFinLedgerTx",
    //    //                    new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
    //    //                    new string[] { "0", ParticularID, "Item Ledger", ViewState["VoucherTx_ID"].ToString(), Item_id, VoucherTx_Type, Amount, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (j + 1).ToString() }, "dataset");
    //    //                }
    //    //            }
    //    //            int gridBillbyBillRows = GridViewRef.Rows.Count;
    //    //            for (int k = 0; k < gridBillbyBillRows; k++)
    //    //            {


    //    //                Label BillByBillTx_RefType = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblTypeOfRef");
    //    //                Label BillByBillTx_Ref = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblRefNo");
    //    //                Label BillByBillTx_Amount = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblAmount");
    //    //                Label Type = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblType");
    //    //                if (Type.Text == "Dr")
    //    //                {
    //    //                    BillByBillTx_Amount.Text = "-" + BillByBillTx_Amount.Text;
    //    //                }

    //    //                objdb.ByProcedure("SpFinBillByBillTx",
    //    //                            new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy" },
    //    //                            new string[] { "3", ViewState["VoucherTx_ID"].ToString(), ddlPartyName.SelectedValue.ToString(), BillByBillTx_RefType.Text, BillByBillTx_Ref.Text, BillByBillTx_Amount.Text, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), "1", (k + 1).ToString() }, "dataset");

    //    //            }


    //    //            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Record Updated Successfully.");
    //    //        }
    //    //        else
    //    //        {
    //    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Voucher No Already Exists');", true);
    //    //        }

    //    //    }
    //    //    else
    //    //    {

    //    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
    //    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);
    //    //    }
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    //}
    //}
    protected void ClearText()
    {
        try
        {
            txtVoucherTx_No.Text = "";
            txtVoucherTx_Ref.Text = "";
            GridViewLedger.DataSource = new string[] { };
            GridViewLedger.DataBind();
            GridViewItem.DataSource = new string[] { };
            GridViewItem.DataBind();
            lblGrandTotal.Text = "0";
            ddlLedger.ClearSelection();
            txtLedgerAmt.Text = "";
            ddlDebitLedger.ClearSelection();
            // ddlSalesCenter.ClearSelection();

            txtVoucherTx_Narration.Text = "";
            CreateLedgerTable();
            CreateBillByBillDataSet();
            FillVoucherNo();
            AddItem("NA");
            GetPreviousVoucherNo();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDetail()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "24", ViewState["VoucherTxRef_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    txtVoucherTxDate.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    txtPrevoiusAmount.Text = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                    //ddlSalesCenter.ClearSelection();
                    //ddlSalesCenter.Items.FindByValue(ds.Tables[0].Rows[0]["VoucherTx_SalesCenterID"].ToString()).Selected = true;

                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    GridViewItem.DataSource = ds.Tables[1];
                    GridViewItem.DataBind();

                }
                if (ds.Tables[2].Rows.Count > 0)
                {

                    //GridViewDebtor.DataSource = ds.Tables[2];
                    //GridViewDebtor.DataBind();

                    //if (ds.Tables[2].Rows.Count > 0)
                    //{
                    decimal Amount = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));
                    ViewState["Amount"] = Amount;
                    hfvalue.Value = ViewState["Amount"].ToString();

                    //    GridViewDebtor.FooterRow.Cells[2].Text = "<b>| TOTAL |</b> ";
                    //    GridViewDebtor.FooterRow.Cells[3].Text = "<b>" + Amount.ToString() + "</b>";
                    //    GridViewDebtor.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    //}

                    //DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                    //int rowcount = ds.Tables[2].Rows.Count;
                    //for (int i = 0; i < rowcount; i++)
                    //{
                    ddlDebitLedger.ClearSelection();
                    ddlDebitLedger.Items.FindByValue(ds.Tables[2].Rows[0]["Ledger_ID"].ToString()).Selected = true;
                    /****************Disable Debtor Ledger*************/
                    //ddlDebitLedger.Enabled = false;
                    /*****************************/
                    ddlDebitLedger.Enabled = true;
                    //    string Ledger_ID = ds.Tables[2].Rows[i]["Ledger_ID"].ToString();
                    //    string Ledger_Name = ds.Tables[2].Rows[i]["Ledger_Name"].ToString();
                    //    string LedgerTx_Amount = ds.Tables[2].Rows[i]["LedgerTx_Amount"].ToString();
                    //    string LedgerTx_MaintainType = ds.Tables[2].Rows[i]["LedgerTx_MaintainType"].ToString();


                    //    dt_LedgerTable.Rows.Add(Ledger_ID, Ledger_Name, LedgerTx_Amount, LedgerTx_MaintainType);
                    //    ViewState["LedgerTable"] = dt_LedgerTable;
                    //    decimal LedgerTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));
                    //    ViewState["LedgerTotal"] = LedgerTotal;
                    //    hfvalue.Value = ViewState["LedgerTotal"].ToString();
                    //}




                }
                if (ds.Tables[3].Rows.Count > 0)
                {

                    //GridViewLedger.DataSource = ds.Tables[3];
                    //GridViewLedger.DataBind();



                }

                //if (ds.Tables[5].Rows.Count > 0)
                //{


                //    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                //    int rowscount = ds.Tables[5].Rows.Count;
                //    for (int i = 0; i < rowscount; i++)
                //    {

                //        string Ledger_ID = ds.Tables[5].Rows[i]["Ledger_ID"].ToString();
                //        string BillByBillTx_RefType = ds.Tables[5].Rows[i]["BillByBillTx_RefType"].ToString();
                //        string BillByBillTx_Ref = ds.Tables[5].Rows[i]["BillByBillTx_Ref"].ToString();
                //        string BillByBillTx_Amount = ds.Tables[5].Rows[i]["BillByBillTx_Amount"].ToString();
                //        string Type = ds.Tables[5].Rows[i]["BillByBillTxType"].ToString();
                //        string TNO = Ledger_ID.ToString();
                //        DataTable dt_BillByBillTable = new DataTable(TNO);
                //        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
                //        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
                //        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
                //        dt_BillByBillTable.Columns.Add(new DataColumn("Type", typeof(string)));
                //        dt_BillByBillTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
                //        dt_BillByBillTable.Rows.Add(BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Type, Ledger_ID);
                //        dsBillByBill.Merge(dt_BillByBillTable);
                //        ViewState["BillByBillTable"] = dt_BillByBillTable;
                //        ViewState["dsBillByBill"] = dsBillByBill;


                //    }


                //}
                //if (ds.Tables[6].Rows.Count > 0)
                //{

                //    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                //    int rowscount = ds.Tables[6].Rows.Count;
                //    for (int i = 0; i < rowscount; i++)
                //    {

                //        string Ledger_ID = ds.Tables[6].Rows[i]["Ledger_ID"].ToString();
                //        string ChequeTx_No = ds.Tables[6].Rows[i]["ChequeTx_No"].ToString();
                //        string ChequeTx_Date = ds.Tables[6].Rows[i]["ChequeTx_Date"].ToString();
                //        string ChequeTx_Amount = ds.Tables[6].Rows[i]["ChequeTx_Amount"].ToString();
                //        string TNO = Ledger_ID.ToString();
                //        DataTable dt_FinChequeTx = new DataTable(TNO);
                //        dt_FinChequeTx.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
                //        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_No", typeof(string)));
                //        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Date", typeof(string)));
                //        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Amount", typeof(decimal)));
                //        dt_FinChequeTx.Rows.Add(Ledger_ID, ChequeTx_No, ChequeTx_Date, ChequeTx_Amount);
                //        dsBillByBill.Merge(dt_FinChequeTx);
                //        ViewState["FinChequeTx"] = dt_FinChequeTx;
                //        ViewState["dsBillByBill"] = dsBillByBill;
                //    }


                //}

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                //btnAccept.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowRefDetailModal();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //protected void Save(string Type)
    //{
    //    try
    //    {
    //        string VoucherTx_No = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
    //        string sDate = (Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd")).ToString();
    //        DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
    //        int Month = int.Parse(datevalue.Month.ToString());
    //        int Year = int.Parse(datevalue.Year.ToString());
    //        int FY = Year;
    //        string FinancialYear = Year.ToString();
    //        string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
    //        FinancialYear = "";
    //        if (Month <= 3)
    //        {
    //            FY = Year - 1;
    //            FinancialYear = FY.ToString() + "-" + LFY.ToString();
    //        }
    //        else
    //        {

    //            FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
    //        }


    //        string VoucherTx_Type = "Sales Return";
    //        string VoucherTx_Name = "Credit Note";
    //        string VoucherTx_Ref = txtVoucherTx_Ref.Text;
    //        string VoucherTx_Amount = lblGrandTotal.Text;
    //        string ItemTx_IsActive = "1";
    //        string LedgerTx_IsActive = "1";
    //        string VoucherTx_IsActive = "1";

    //        if (btnAccept.Text == "Accept")
    //        {
    //            DataSet ds2 = objdb.ByProcedure("SpFinVoucherTx",
    //                        new string[] { "flag","VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type"
    //            , "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY"
    //            , "VoucherTx_IsActive", "VoucherTx_InsertedBy","VoucherTx_SNo" },
    //                        new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), VoucherTx_Name, VoucherTx_Type,VoucherTx_No
    //           ,VoucherTx_Ref,txtVoucherTx_Narration.Text,lblGrandTotal.Text,Month.ToString(),Year.ToString(), ViewState["Office_ID"].ToString(),ViewState["Voucher_FY"].ToString()
    //            ,VoucherTx_IsActive,ViewState["Emp_ID"].ToString(),txtVoucherTx_No.Text}, "dataset");


    //            if (ds2.Tables[0].Rows.Count > 0)
    //            {
    //                string VoucherTx_ID = ds2.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
    //                int gridRows = GridViewItem.Rows.Count;
    //                for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
    //                {
    //                    Label lblID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblID");
    //                    Label lblItemID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
    //                    Label lblUnit_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblUnit_id");
    //                    Label lblWarehouse_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_id");
    //                    Label lblItem = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItem");
    //                    Label lblQuantity = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblQuantity");
    //                    Label lblRate = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblRate");
    //                    Label lblAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblAmount");
    //                    Label lblCGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblCGST");
    //                    Label lblSGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblSGST");
    //                    //Label lblTotalAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblTotalAmount");
    //                    objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Name", "VoucherTx_Type", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "Warehouse_id", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy" }, new string[] { "0", VoucherTx_ID, VoucherTx_Name, VoucherTx_Type, lblItemID.Text, lblUnit_id.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, lblWarehouse_id.Text, ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 1).ToString() }, "dataset");





    //                }
    //                int gridLedgerRows = GridViewLedger.Rows.Count;
    //                for (int rowIndex = 0; rowIndex < gridLedgerRows; rowIndex++)
    //                {
    //                    string LedgerTx_Amount = "0";
    //                    Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
    //                    TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
    //                    if (txtAmount.Text.Contains("-"))
    //                    {
    //                        LedgerTx_Amount = txtAmount.Text.Replace(@"-", string.Empty);
    //                    }
    //                    else
    //                    {
    //                        LedgerTx_Amount = "-" + txtAmount.Text;
    //                    }

    //                    // string LedgerTx_Amount = "-" + txtAmount.Text;
    //                    objdb.ByProcedure("SpFinLedgerTx",
    //                    new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
    //                    new string[] { "0", lblID.Text, "Sub Ledger", VoucherTx_ID, VoucherTx_Type, LedgerTx_Amount, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 1).ToString() }, "dataset");

    //                }
    //                DataSet DS_GridViewParticulars = (DataSet)ViewState["DS_GridViewParticulars"];
    //                for (int i = 0; i < DS_GridViewParticulars.Tables.Count; i++)
    //                {
    //                    for (int j = 0; j < DS_GridViewParticulars.Tables[i].Rows.Count; j++)
    //                    {
    //                        string ParticularID = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularID"].ToString();
    //                        string Amount = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularAmt"].ToString();
    //                        string Item_id = DS_GridViewParticulars.Tables[i].Rows[j]["Item_ID"].ToString();
    //                        Amount = "-" + Amount;
    //                        objdb.ByProcedure("SpFinLedgerTx",
    //                        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
    //                        new string[] { "0", ParticularID, "Item Ledger", VoucherTx_ID, Item_id, VoucherTx_Type, Amount, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (j + 1).ToString() }, "dataset");
    //                    }
    //                }
    //                if (Type == "BillByBill")
    //                {
    //                    objdb.ByProcedure("SpFinLedgerTx",
    //               new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
    //        , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
    //               new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
    //        ,ViewState["Voucher_FY"].ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","BillByBill"}, "dataset");


    //                    int gridBillbyBillRows = GridViewRef.Rows.Count;
    //                    for (int k = 0; k < gridBillbyBillRows; k++)
    //                    {


    //                        Label BillByBillTx_RefType = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblTypeOfRef");
    //                        Label BillByBillTx_Ref = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblRefNo");
    //                        Label BillByBillTx_Amount = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblAmount");
    //                        Label BillType = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblType");
    //                        if (BillType.Text == "Dr")
    //                        {
    //                            BillByBillTx_Amount.Text = "-" + BillByBillTx_Amount.Text;
    //                        }

    //                        objdb.ByProcedure("SpFinBillByBillTx",
    //                                    new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy" },
    //                                    new string[] { "3", VoucherTx_ID, ddlPartyName.SelectedValue.ToString(), BillByBillTx_RefType.Text, BillByBillTx_Ref.Text, BillByBillTx_Amount.Text, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), "1", (k + 1).ToString() }, "dataset");

    //                    }
    //                }
    //                else if (Type == "Cheque")
    //                {
    //                    objdb.ByProcedure("SpFinLedgerTx",
    //               new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
    //        , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
    //               new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
    //        ,ViewState["Voucher_FY"].ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","Cheque"}, "dataset");


    //                    int gridChequeRows = GVFinChequeTx.Rows.Count;
    //                    for (int k = 0; k < gridChequeRows; k++)
    //                    {


    //                        Label RID = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblID");
    //                        Label ChequeTx_No = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_No");
    //                        Label ChequeTx_Date = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_Date");
    //                        Label ChequeTx_Amount = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_Amount");

    //                        if (ChequeTx_No.Text == "")
    //                        {
    //                            ChequeTx_No.Text = null;
    //                        }
    //                        else
    //                        {

    //                        }

    //                        if (ChequeTx_Date.Text == "")
    //                        {
    //                            ChequeTx_Date.Text = null;
    //                        }
    //                        else
    //                        {
    //                            ChequeTx_Date.Text = Convert.ToDateTime(ChequeTx_Date.Text, cult).ToString("yyyy/MM/dd");
    //                        }

    //                        objdb.ByProcedure("SpFinChequeTx",
    //                                             new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy" },
    //                                             new string[] { "1", VoucherTx_ID, ddlPartyName.SelectedValue.ToString(), "CreditSale Voucher", ChequeTx_No.Text, ChequeTx_Date.Text, ChequeTx_Amount.Text, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), (k + 1).ToString() }, "dataset");

    //                    }

    //                }
    //                else
    //                {
    //                    objdb.ByProcedure("SpFinLedgerTx",
    //               new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
    //        , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
    //               new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
    //        ,ViewState["Voucher_FY"].ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","None"}, "dataset");
    //                }

    //            }

    //            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
    //            ClearData();
    //        }
    //        else if (btnAccept.Text == "Update")
    //        {
    //            DataSet ds2 = objdb.ByProcedure("SpFinVoucherTx",
    //        new string[] { "flag","VoucherTx_ID","VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type"
    //    , "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY"
    //    , "VoucherTx_IsActive", "VoucherTx_InsertedBy","VoucherTx_SNo" },
    //        new string[] { "7",ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), VoucherTx_Name, VoucherTx_Type,VoucherTx_No
    //   ,VoucherTx_Ref,txtVoucherTx_Narration.Text,lblGrandTotal.Text,Month.ToString(),Year.ToString(), ViewState["Office_ID"].ToString(),ViewState["Voucher_FY"].ToString()
    //    ,VoucherTx_IsActive,ViewState["Emp_ID"].ToString(),txtVoucherTx_No.Text}, "dataset");

    //            objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "1", ViewState["VoucherTx_ID"].ToString() }, "dataset");
    //            if (ds2.Tables[0].Rows.Count > 0)
    //            {
    //                string VoucherTx_ID = ds2.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
    //                int gridRows = GridViewItem.Rows.Count;
    //                for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
    //                {
    //                    Label lblID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblID");
    //                    Label lblItemID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
    //                    Label lblUnit_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblUnit_id");
    //                    Label lblWarehouse_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_id");
    //                    Label lblItem = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItem");
    //                    Label lblQuantity = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblQuantity");
    //                    Label lblRate = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblRate");
    //                    Label lblAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblAmount");
    //                    Label lblCGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblCGST");
    //                    Label lblSGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblSGST");
    //                    //Label lblTotalAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblTotalAmount");
    //                    objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Name", "VoucherTx_Type", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "Warehouse_id", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy" }, new string[] { "0", VoucherTx_ID, VoucherTx_Name, VoucherTx_Type, lblItemID.Text, lblUnit_id.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, lblWarehouse_id.Text, ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 1).ToString() }, "dataset");





    //                }
    //                objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
    //                int gridLedgerRows = GridViewLedger.Rows.Count;
    //                for (int rowIndex = 0; rowIndex < gridLedgerRows; rowIndex++)
    //                {
    //                    string LedgerTx_Amount = "0";
    //                    Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
    //                    TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
    //                    if (txtAmount.Text.Contains("-"))
    //                    {
    //                        LedgerTx_Amount = txtAmount.Text.Replace(@"-", string.Empty);
    //                    }
    //                    else
    //                    {
    //                        LedgerTx_Amount = "-" + txtAmount.Text;
    //                    }

    //                    // string LedgerTx_Amount = "-" + txtAmount.Text;
    //                    objdb.ByProcedure("SpFinLedgerTx",
    //                    new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
    //                    new string[] { "0", lblID.Text, "Sub Ledger", VoucherTx_ID, VoucherTx_Type, LedgerTx_Amount, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 1).ToString() }, "dataset");

    //                }

    //                DataSet DS_GridViewParticulars = (DataSet)ViewState["DS_GridViewParticulars"];
    //                for (int i = 0; i < DS_GridViewParticulars.Tables.Count; i++)
    //                {
    //                    for (int j = 0; j < DS_GridViewParticulars.Tables[i].Rows.Count; j++)
    //                    {
    //                        string ParticularID = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularID"].ToString();
    //                        string Amount = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularAmt"].ToString();
    //                        string Item_id = DS_GridViewParticulars.Tables[i].Rows[j]["Item_ID"].ToString();
    //                        Amount = "-" + Amount;
    //                        objdb.ByProcedure("SpFinLedgerTx",
    //                        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
    //                        new string[] { "0", ParticularID, "Item Ledger", VoucherTx_ID, Item_id, VoucherTx_Type, Amount, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (j + 1).ToString() }, "dataset");
    //                    }
    //                }
    //                objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "4", ViewState["VoucherTx_ID"].ToString() }, "dataset");
    //                objdb.ByProcedure("SpFinChequeTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
    //                if (Type == "BillByBill")
    //                {
    //                    objdb.ByProcedure("SpFinLedgerTx",
    //               new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
    //        , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
    //               new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
    //        ,ViewState["Voucher_FY"].ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","BillByBill"}, "dataset");


    //                    int gridBillbyBillRows = GridViewRef.Rows.Count;
    //                    for (int k = 0; k < gridBillbyBillRows; k++)
    //                    {


    //                        Label BillByBillTx_RefType = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblTypeOfRef");
    //                        Label BillByBillTx_Ref = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblRefNo");
    //                        Label BillByBillTx_Amount = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblAmount");
    //                        Label BillType = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblType");
    //                        if (BillType.Text == "Dr")
    //                        {
    //                            BillByBillTx_Amount.Text = "-" + BillByBillTx_Amount.Text;
    //                        }

    //                        objdb.ByProcedure("SpFinBillByBillTx",
    //                                    new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy" },
    //                                    new string[] { "3", VoucherTx_ID, ddlPartyName.SelectedValue.ToString(), BillByBillTx_RefType.Text, BillByBillTx_Ref.Text, BillByBillTx_Amount.Text, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), "1", (k + 1).ToString() }, "dataset");

    //                    }
    //                }
    //                else if (Type == "Cheque")
    //                {
    //                    objdb.ByProcedure("SpFinLedgerTx",
    //               new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
    //        , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
    //               new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
    //        ,ViewState["Voucher_FY"].ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","Cheque"}, "dataset");


    //                    int gridChequeRows = GVFinChequeTx.Rows.Count;
    //                    for (int k = 0; k < gridChequeRows; k++)
    //                    {


    //                        Label RID = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblID");
    //                        Label ChequeTx_No = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_No");
    //                        Label ChequeTx_Date = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_Date");
    //                        Label ChequeTx_Amount = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_Amount");

    //                        if (ChequeTx_No.Text == "")
    //                        {
    //                            ChequeTx_No.Text = null;
    //                        }
    //                        else
    //                        {

    //                        }

    //                        if (ChequeTx_Date.Text == "")
    //                        {
    //                            ChequeTx_Date.Text = null;
    //                        }
    //                        else
    //                        {
    //                            ChequeTx_Date.Text = Convert.ToDateTime(ChequeTx_Date.Text, cult).ToString("yyyy/MM/dd");
    //                        }

    //                        objdb.ByProcedure("SpFinChequeTx",
    //                                             new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy" },
    //                                             new string[] { "1", VoucherTx_ID, ddlPartyName.SelectedValue.ToString(), "CreditSale Voucher", ChequeTx_No.Text, ChequeTx_Date.Text, ChequeTx_Amount.Text, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), (k + 1).ToString() }, "dataset");

    //                    }

    //                }
    //                else
    //                {
    //                    objdb.ByProcedure("SpFinLedgerTx",
    //               new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
    //        , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
    //               new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
    //        ,ViewState["Voucher_FY"].ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","None"}, "dataset");
    //                }

    //            }

    //            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
    //            ClearData();
    //        }





    //    }


    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    //protected void GVFinChequeTx_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    try
    //    {
    //        string ID = GVFinChequeTx.DataKeys[e.RowIndex].Value.ToString();
    //        ViewState["Amount"] = lblGrandTotal.Text;
    //        FillChequeAmount(ID);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewDebtor.DataSource = new string[] { };
            GridViewDebtor.DataBind();
            GridViewItem.DataSource = new string[] { };
            GridViewItem.DataBind();
            //GridViewLedger.DataSource = new string[] { };
            //GridViewLedger.DataBind();
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_No", "Office_ID" }, new string[] { "18", txtVoucherTx_Ref.Text, ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Status"].ToString() == "true")
                {
                    ViewState["VoucherTxRef_ID"] = ds.Tables[1].Rows[0]["VoucherTx_ID"].ToString();
                    FillDetail();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please Enter Valid Voucher No.');", true);
                    //ClearData();
                }
                GridViewDebtor.DataSource = new string[] { };
                GridViewDebtor.DataBind();


            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillVoucherNo()
    {
        try
        {
            //start done changes by Mohini on 16Apr2022
            string VoucherTx_No = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
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
            string[] FinY = FinancialYear.Split('-');
            ViewState["CGST"] = "1";
            ViewState["SGST"] = "2";
            ViewState["IGST"] = "4";
            //if (int.Parse(FinY[0]) >= 2022)
            //{
            //    ViewState["CGST"] = "46721";
            //    ViewState["SGST"] = "46722";
            //    ViewState["IGST"] = "46723";

            //}
            //else
            //{
            //    ViewState["CGST"] = "1";
            //    ViewState["SGST"] = "2";
            //    ViewState["IGST"] = "737";
            //}
            //start done changes by Mohini on 16Apr2022
            //string VoucherTx_Names_ForSno = "'Payment,Journal,Contra'";
            //string VoucherTx_Names_ForSno = "'Receipt'";
            //string VoucherTx_Names_ForSno = "'Purchase Voucher'";
            string VoucherTx_Names_ForSno = "Sales Return Voucher";

            DataSet ds1 = objdb.ByProcedure("SpFinVoucherTx",
                new string[] { "flag", "Office_ID", "VoucherTx_FY", "VoucherTx_Names_ForSno" },
                new string[] { "13", ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_Names_ForSno }, "dataset");

            
            string Office_Code = "";
            if (ds1.Tables[1].Rows.Count != 0)
            {
                Office_Code = ds1.Tables[1].Rows[0]["Office_Code"].ToString();
            }
            int VoucherTx_SNo = 0;
            if (ds1.Tables[0].Rows.Count != 0)
            {
                //VoucherTx_SNo = Convert.ToInt32(ds1.Tables[0].Rows[0]["VoucherTx_SNo"].ToString());

            }
            //ViewState["PreVoucherNo"] = Office_Code + FinancialYear.ToString().Substring(2) + "PV" + VoucherTx_SNo.ToString();
            VoucherTx_SNo++;
            ViewState["VoucherTx_SNo"] = VoucherTx_SNo;
            lblVoucherTx_No.Text = Office_Code + FinancialYear.ToString().Substring(2) + "CN";
            txtVoucherTx_No.Text = VoucherTx_SNo.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CreateLedgerTable();
            TextBox txt = sender as TextBox;
            GridViewRow row = (GridViewRow)txt.Parent.Parent;
            int rowIndex = row.RowIndex;
            CheckBox chkboxid = (CheckBox)GridViewItem.Rows[rowIndex].Cells[0].FindControl("chkboxid");
            TextBox txtQuantity = (TextBox)GridViewItem.Rows[rowIndex].Cells[0].FindControl("txtQuantity");
            TextBox txtAmountH = (TextBox)GridViewItem.Rows[rowIndex].Cells[0].FindControl("txtAmountH");
            TextBox txtCGST = (TextBox)GridViewItem.Rows[rowIndex].Cells[0].FindControl("txtCGST");
            TextBox txtSGST = (TextBox)GridViewItem.Rows[rowIndex].Cells[0].FindControl("txtSGST");
            Label ItemID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
            Label lblQuantity = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblQuantity");
            Label lblAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblAmount");
            Label CGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("CGST");
            Label SGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("SGST");
            Label lblWarehouse_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_id");
            int Item_ID = int.Parse(ItemID.Text);
            int Warehouse_id = int.Parse(lblWarehouse_id.Text);
            //if (txtQuantity.Text != "" && float.Parse(txtQuantity.Text) != 0)
            if (txtQuantity.Text != "")
            {
                DataSet ds = objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "Item_id", "Warehouse_id", "VoucherTx_ID", "VoucherTx_No" }, new string[] { "5", Item_ID.ToString(), Warehouse_id.ToString(), ViewState["VoucherTxRef_ID"].ToString(), txtVoucherTx_Ref.Text }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    float Quantity = float.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString());
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        if (ds.Tables[1].Rows[0]["Quantity"].ToString() != "")
                        {
                            float Quantity1 = float.Parse(ds.Tables[1].Rows[0]["Quantity"].ToString());
                            float ValidQuantity = (Quantity - Quantity1);
                            if (float.Parse(txtQuantity.Text) > (Quantity - Quantity1))
                            {
                                if (ValidQuantity == 0)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Selected Item's quantity has been fully returned.');", true);
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Quantity Should not be greater than " + ValidQuantity + ".');", true);
                                }
                                txtQuantity.Text = lblQuantity.Text;
                                txtAmountH.Text = lblAmount.Text;
                                txtCGST.Text = CGST.Text;
                                txtSGST.Text = SGST.Text;
                                chkboxid.Checked = false;
                            }
                        }
                        else if (float.Parse(txtQuantity.Text) > Quantity)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Invalid Quantity.');", true);
                            txtQuantity.Text = lblQuantity.Text;
                            txtAmountH.Text = lblAmount.Text;
                            txtCGST.Text = CGST.Text;
                            txtSGST.Text = SGST.Text;
                            chkboxid.Checked = false;
                        }

                    }
                    else
                    {

                        if (float.Parse(txtQuantity.Text) > Quantity)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Invalid Quantity.');", true);
                            txtQuantity.Text = lblQuantity.Text;
                            txtAmountH.Text = lblAmount.Text;
                            txtCGST.Text = CGST.Text;
                            txtSGST.Text = SGST.Text;
                            chkboxid.Checked = false;


                        }
                    }
                }
            }

            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Quantity cannot be blank or Zero.');", true);
                txtQuantity.Text = lblQuantity.Text;
                txtAmountH.Text = lblAmount.Text;
                txtCGST.Text = CGST.Text;
                txtSGST.Text = SGST.Text;
                chkboxid.Checked = false;
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGridAmount();", true);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void chkboxid_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            CheckBox chk = sender as CheckBox;
            GridViewRow row = (GridViewRow)chk.Parent.Parent;
            int rowIndex = row.RowIndex;
            CheckBox chkboxid = (CheckBox)GridViewItem.Rows[rowIndex].Cells[0].FindControl("chkboxid");
            TextBox txtQuantity = (TextBox)GridViewItem.Rows[rowIndex].Cells[0].FindControl("txtQuantity");
            TextBox txtAmountH = (TextBox)GridViewItem.Rows[rowIndex].Cells[0].FindControl("txtAmountH");
            TextBox txtCGST = (TextBox)GridViewItem.Rows[rowIndex].Cells[0].FindControl("txtCGST");
            TextBox txtSGST = (TextBox)GridViewItem.Rows[rowIndex].Cells[0].FindControl("txtSGST");
            Label ItemID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
            Label lblQuantity = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblQuantity");
            Label lblAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblAmount");
            Label CGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("CGST");
            Label SGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("SGST");
            Label lblWarehouse_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_id");
            int Item_ID = int.Parse(ItemID.Text);
            int Warehouse_id = int.Parse(lblWarehouse_id.Text);
            if (chkboxid.Checked == true)
            {
                // if (txtQuantity.Text != "" && float.Parse(txtQuantity.Text) != 0)
                if (txtQuantity.Text != "")
                {
                    DataSet ds = objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "Item_id", "Warehouse_id", "VoucherTx_ID", "VoucherTx_No" }, new string[] { "5", Item_ID.ToString(), Warehouse_id.ToString(), ViewState["VoucherTxRef_ID"].ToString(), txtVoucherTx_Ref.Text }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        float Quantity = float.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString());
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            if (ds.Tables[1].Rows[0]["Quantity"].ToString() != "")
                            {
                                float Quantity1 = float.Parse(ds.Tables[1].Rows[0]["Quantity"].ToString());
                                float ValidQuantity = (Quantity - Quantity1);

                                if (float.Parse(txtQuantity.Text) > (Quantity - Quantity1))
                                {
                                    if (ValidQuantity == 0)
                                    {
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Selected Item's quantity has been fully returned.');", true);
                                    }
                                    else
                                    {
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Quantity Should not be greater than " + ValidQuantity + ".');", true);
                                    }
                                    txtQuantity.Text = lblQuantity.Text;
                                    txtAmountH.Text = lblAmount.Text;
                                    txtCGST.Text = CGST.Text;
                                    txtSGST.Text = SGST.Text;
                                    chkboxid.Checked = false;
                                }
                            }
                            else if (float.Parse(txtQuantity.Text) > Quantity)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Invalid Quantity.');", true);
                                txtQuantity.Text = lblQuantity.Text;
                                txtAmountH.Text = lblAmount.Text;
                                txtCGST.Text = CGST.Text;
                                txtSGST.Text = SGST.Text;
                                chkboxid.Checked = false;
                            }
                        }
                        else
                        {

                            if (float.Parse(txtQuantity.Text) > Quantity)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Invalid Quantity.');", true);
                                txtQuantity.Text = lblQuantity.Text;
                                txtAmountH.Text = lblAmount.Text;
                                txtCGST.Text = CGST.Text;
                                txtSGST.Text = SGST.Text;
                                chkboxid.Checked = false;


                            }
                        }
                    }

                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Quantity cannot be blank or Zero.');", true);
                    txtQuantity.Text = lblQuantity.Text;
                    txtAmountH.Text = lblAmount.Text;
                    txtCGST.Text = CGST.Text;
                    txtSGST.Text = SGST.Text;
                    chkboxid.Checked = false;
                }
            }
            else
            {
                CreateLedgerTable();
                txtQuantity.Text = lblQuantity.Text;
                txtAmountH.Text = lblAmount.Text;
                txtCGST.Text = CGST.Text;
                txtSGST.Text = SGST.Text;

            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGridAmount();", true);


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ViewVoucher()
    {
        try
        {
            //ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "25", ViewState["VoucherTx_ID"].ToString() }, "dataset");
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "42", ViewState["VoucherTx_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtVoucherTx_Ref.Text = ds.Tables[0].Rows[0]["VoucherTx_Ref"].ToString();
                    txtVoucherTxDate.Text = ds.Tables[0].Rows[0]["VoucherTx_DateRef"].ToString();
                    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
                    txtVoucherTx_No.Text = ds.Tables[0].Rows[0]["VoucherTx_SNo"].ToString();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    GridViewItem.DataSource = ds.Tables[1];
                    GridViewItem.DataBind();
                    foreach (GridViewRow rows in GridViewItem.Rows)
                    {
                        CheckBox chkboxid = (CheckBox)rows.FindControl("chkboxid");
                        chkboxid.Checked = true;
                    }

                }
                if (ds.Tables[2].Rows.Count > 0)
                {

                    GridViewDebtor.DataSource = ds.Tables[2];
                    GridViewDebtor.DataBind();

                    if (ds.Tables[2].Rows.Count > 0)
                    {

                        decimal Amount = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));
                        GridViewDebtor.FooterRow.Cells[2].Text = "<b>| TOTAL |</b> ";
                        GridViewDebtor.FooterRow.Cells[3].Text = "<b>" + Amount.ToString() + "</b>";
                        GridViewDebtor.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    }
                    ddlDebitLedger.ClearSelection();
                    if (ds.Tables[0].Rows[0]["Office_ID"].ToString() == ViewState["Office_ID"].ToString())
                        ddlDebitLedger.Items.FindByValue(ds.Tables[2].Rows[0]["Ledger_ID"].ToString()).Selected = true;
                    ddlDebitLedger.Enabled = false;

                }
                if (ds.Tables[3].Rows.Count > 0)
                {

                    GridViewLedger.DataSource = ds.Tables[3];
                    GridViewLedger.DataBind();



                }


                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                //btnAccept.Enabled = true;
            }
            btnAccept.Visible = false;
            btn_Clear.Visible = false;
            btnSearch.Visible = false;
            divamount.Visible = false;


            panel1.Enabled = false;
            btnAddDebtor.Visible = false;
            GridViewDebtor.Columns[4].Visible = false;
            GridViewDebtor.Columns[5].Visible = false;
            foreach (GridViewRow rows in GridViewLedger.Rows)
            {
                LinkButton delete = (LinkButton)rows.FindControl("Delete");
                delete.Visible = false;
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtVoucherTx_Date_TextChanged(object sender, EventArgs e)
    {
        string ValidStatus = ValidDate();
        if (ValidStatus == "No")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('You are not allowed to choose this date, please contact to head office.');", true);
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }
        }
        else
        {
            if (ViewState["VoucherTx_ID"].ToString() == "0")
            {
                FillVoucherNo();
                AddItem("NA");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGridAmount();", true);

            }
            else
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
                if (ViewState["FY"].ToString() == FinancialYear.ToString())
                {
                    FillVoucherNo();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Date selection should be according to Financial Year(" + ViewState["FY"].ToString() + ")');", true);
                    txtVoucherTx_Date.Text = ViewState["VoucherTx_Date"].ToString();
                }
            }
        }
    }

    //GetPreviousVoucherNo
    protected void GetPreviousVoucherNo()
    {
        try
        {
            //lblMsg.Text = "";
            lblPreviousVoucherNo.Text = "";
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
            string VoucherTx_Type = "CreditNote Voucher";
            DataSet ds = objdb.ByProcedure("SpFinVoucherTx",
                new string[] { "flag", "Office_ID", "VoucherTx_FY", "VoucherTx_Type" },
                new string[] { "39", ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_Type }, "dataset");
            //ds = objdb.ByProcedure("", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblPreviousVoucherNo.Text = "(Previous VoucherNo :" + " " + ds.Tables[0].Rows[0]["VoucherTx_No"].ToString() + ")";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected string ValidDate()
    {
        string validDays = "No";
        if (txtVoucherTx_Date.Text != "")
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

            DataSet dsValidDate = objdb.ByProcedure("SpFinEditRight", new string[] { "flag", "Office_ID", "VoucherDate", "FinancialYear" }, new string[] { "3", ViewState["Office_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), FinancialYear }, "dataset");
            if (dsValidDate.Tables.Count != 0 && dsValidDate.Tables[0].Rows.Count != 0)
            {
                validDays = dsValidDate.Tables[0].Rows[0]["ValidStatus"].ToString();
            }
        }
        return validDays;
    }



}