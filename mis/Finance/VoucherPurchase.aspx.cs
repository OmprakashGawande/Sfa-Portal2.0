using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_PurchaseVoucher : System.Web.UI.Page
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
                    if (Request.QueryString["Type"] != null)
                    {
                        string Type = Request.QueryString["Type"].ToString();
                        ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                        ViewState["Office_ID"] = Session["Office_ID"].ToString();
                        ViewState["RowNo"] = "0";
                        ViewState["VoucherTx_ID"] = "0";
                        lblGrandTotal.Attributes.Add("readonly", "readonly");
                        if (Type == "Service")
                            lblVoucherType.Text = "Service";
                        else if (Type == "Goods")
                            lblVoucherType.Text = "Goods";
                        //else
                        //    Response.Redirect("~/mis/Login.aspx");
                        




                        CreateParticularDataset();
                        FillDropDown();

                        AddItem("NA");
                        txtUnitName.Attributes.Add("readonly", "readonly");
                        txtAmount.Attributes.Add("readonly", "readonly");

                    }
                    if(Request.QueryString["Type"] != null && Request.QueryString["Action"] != null)
                    {
                        string str = (Request.QueryString["Type"].ToString());
                        
                        string[] tokens = str.Split('?');

                        string Type = tokens[0].ToString();
                        if (Type == "Service")
                            lblVoucherType.Text = "Service";
                        else if (Type == "Goods")
                            lblVoucherType.Text = "Goods";
                        string[] Voucher = tokens[1].Split('=');
                        ViewState["VoucherTx_ID"] = objdb.Decrypt(Voucher[1].ToString());
                        string Action = objdb.Decrypt(Request.QueryString["Action"].ToString());
                        ViewState["Action"] = Action;
                        ViewState["VoucherTx_ID"] = objdb.Decrypt(Request.QueryString["VoucherTx_ID"].ToString());
                        if (Action == "2")
                        {
                            FillDetail();
                        }
                        else if (Action == "1")
                        {
                            FillDetail();
                            //ViewVoucher();
                        }
                    }
                    

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
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID" }, new string[] { "5", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlPartyName.DataTextField = "Ledger_Name";
                    ddlPartyName.DataValueField = "Ledger_ID";
                    ddlPartyName.DataSource = ds.Tables[0];
                    ddlPartyName.DataBind();
                    ddlPartyName.Items.Insert(0, new ListItem("Select", "0"));
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    ddlLedger.DataTextField = "Ledger_Name";
                    ddlLedger.DataValueField = "Ledger_ID";
                    ddlLedger.DataSource = ds.Tables[1];
                    ddlLedger.DataBind();
                    ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

                    ddlParticulars.DataTextField = "Ledger_Name";
                    ddlParticulars.DataValueField = "Ledger_ID";
                    ddlParticulars.DataSource = ds.Tables[1];
                    ddlParticulars.DataBind();
                    ddlParticulars.Items.Insert(0, new ListItem("Select", "0"));
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    ddlWarehouse.DataTextField = "WarehouseName";
                    ddlWarehouse.DataValueField = "Warehouse_id";
                    ddlWarehouse.DataSource = ds.Tables[2];
                    ddlWarehouse.DataBind();
                    ddlWarehouse.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            ds = null;
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
                ViewState["Voucher_FY"] = ds.Tables[0].Rows[0]["Voucher_FY"].ToString();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void CreateParticularDataset()
    {
        DataSet DS_GridViewParticulars = new DataSet();
        ViewState["DS_GridViewParticulars"] = DS_GridViewParticulars;
    }
    protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlItem.Items.Clear();
            if (ddlPartyName.SelectedIndex > 0)
            {
                string Vendor_id = ddlPartyName.SelectedValue.ToString();
                Vendor_id = "1";
                ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Vendor_id" }, new string[] { "3", Vendor_id }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlItem.DataTextField = "ItemName";
                    ddlItem.DataValueField = "Item_id";
                    ddlItem.DataSource = ds;
                    ddlItem.DataBind();
                    ddlItem.Items.Insert(0, new ListItem("Select", "0"));
                }
                DataSet ds1 = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "LedgerTx_FY" }, new string[] { "1", ddlPartyName.SelectedValue.ToString(), ViewState["Voucher_FY"].ToString() }, "dataset");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    txtCurrentBalance.Text = ds1.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
                }
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlPartyName.SelectedIndex > 0)
            {
                ddlItem.ClearSelection();
                txtQuantity.Text = "";
                txtRate.Text = "";
                txtAmount.Text = "";
                ddlParticulars.ClearSelection();
                txtParticularAmt.Text = "";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal();", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearItem();
            if (ddlItem.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Item_id", "Vendor_id" },
                   new string[] { "4", ddlItem.SelectedValue.ToString(), "1" }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    // txtLedger.Text = ds.Tables[0].Rows[0]["Ledger_Name"].ToString();
                    txtQuantity.Text = "1";
                    txtRate.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                    txtAmount.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                    txtUnitName.Text = ds.Tables[0].Rows[0]["UnitName"].ToString();
                    lblUnitName.Text = ds.Tables[0].Rows[0]["Unit_id"].ToString();

                }
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal();", true);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearItem()
    {
        try
        {
            lblMsg.Text = "";
            // txtLedger.Text = "";
            txtQuantity.Text = "";
            txtRate.Text = "";
            txtAmount.Text = "";
            txtUnitName.Text = "";
            lblUnitName.Text = "";
            ddlWarehouse.ClearSelection();
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

            DataTable dt_GridViewItem = new DataTable();
            dt_GridViewItem.Columns.Add(new DataColumn("ID", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("ItemID", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Unit_id", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Warehouse_id", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Item", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Quantity", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Rate", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("CGST", typeof(decimal)));
            dt_GridViewItem.Columns.Add(new DataColumn("SGST", typeof(decimal)));
            dt_GridViewItem.Columns.Add(new DataColumn("TotalAmount", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Unit", typeof(string)));
            int rowIndex = 0;
            int gridRows = GridViewItem.Rows.Count;
            int RID = 0;
            for (rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {
                Label lblID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblID");
                Label lblItemID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
                Label lblUnit_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblUnit_id");
                Label lblWarehouse_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_id");
                Label lblItem = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItem");
                Label lblQuantity = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblQuantity");
                Label lblRate = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblRate");
                Label lblAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblAmount");
                Label lblCGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblCGST");
                Label lblSGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblSGST");
                Label lblTotalAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblTotalAmount");
                Label lblUnit = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblUnit");


                if (lblItemID.Text != ID && ViewState["RowNo"].ToString() == "0")
                {
                    RID++;
                    dt_GridViewItem.Rows.Add(RID.ToString(), lblItemID.Text, lblUnit_id.Text, lblWarehouse_id.Text, lblItem.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, lblCGST.Text, lblSGST.Text, lblTotalAmount.Text, lblUnit.Text);
                }
                else if (ViewState["RowNo"].ToString() != "0")
                {
                    ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Item_id", "Vendor_id" },
                       new string[] { "4", ddlItem.SelectedValue.ToString(), "1" }, "dataset");
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        gridRows = gridRows + 1;
                        string Item = ddlItem.SelectedItem.ToString();

                        double CGST = double.Parse(ds.Tables[0].Rows[0]["CGST"].ToString());
                        double SGST = double.Parse(ds.Tables[0].Rows[0]["SGST"].ToString());

                        double Amount = double.Parse(txtAmount.Text);
                        CGST = Math.Round((Amount * CGST) / 100, 2);
                        SGST = Math.Round((Amount * SGST) / 100, 2);
                        double TotalAmount = Amount + CGST + SGST;

                        dt_GridViewItem.Rows.Add(ID.ToString(), ddlItem.SelectedValue.ToString(), lblUnitName.Text, ddlWarehouse.SelectedValue.ToString(), Item.ToString(), txtQuantity.Text, txtRate.Text, Amount.ToString(), CGST.ToString(), SGST.ToString(), TotalAmount.ToString(), txtUnitName.Text);
                    }
                }

                // dt_GridViewItem.Rows.Add(rowIndex.ToString(), lblItem.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text);

            }
            if (ID == "0" && ViewState["RowNo"].ToString() == "0")
            {
                ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Item_id", "Vendor_id" },
                   new string[] { "4", ddlItem.SelectedValue.ToString(), "1" }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    gridRows = gridRows + 1;
                    string Item = ddlItem.SelectedItem.ToString();

                    double CGST = double.Parse(ds.Tables[0].Rows[0]["CGST"].ToString());
                    double SGST = double.Parse(ds.Tables[0].Rows[0]["SGST"].ToString());

                    double Amount = double.Parse(txtAmount.Text);
                    CGST = Math.Round((Amount * CGST) / 100, 2);
                    SGST = Math.Round((Amount * SGST) / 100, 2);
                    double TotalAmount = Amount + CGST + SGST;
                    RID++;
                    dt_GridViewItem.Rows.Add(RID.ToString(), ddlItem.SelectedValue.ToString(), lblUnitName.Text, ddlWarehouse.SelectedValue.ToString(), Item.ToString(), txtQuantity.Text, txtRate.Text, Amount.ToString(), CGST.ToString(), SGST.ToString(), TotalAmount.ToString(), txtUnitName.Text);
                }

            }
            GridViewItem.DataSource = dt_GridViewItem;
            GridViewItem.DataBind();

            if (ID != "0")
            {
                DataSet ds1 = (DataSet)ViewState["DS_GridViewParticulars"];
                DataSet DS_GridViewParticulars = (DataSet)ViewState["DS_GridViewParticulars"];
                int TNo = GridViewItem.Rows.Count;
                DS_GridViewParticulars = new DataSet();
                for (rowIndex = 0; rowIndex < TNo; rowIndex++)
                {
                    Label lblID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
                    int TabName = int.Parse(lblID.Text);
                    DataTable dt = new DataTable(lblID.Text);
                    dt = ds1.Tables[TabName.ToString()];
                    DS_GridViewParticulars.Merge(dt);

                    //if (TabName >= int.Parse(ID))
                    //{
                    //    TabName++;
                    //    DataTable dt = new DataTable(lblID.Text);
                    //    dt = ds1.Tables[TabName.ToString()];

                    //    DS_GridViewParticulars.Merge(dt);

                    //    DS_GridViewParticulars.Tables[rowIndex].TableName = lblID.Text;
                    //}
                    //else
                    //{
                    //    DataTable dt = new DataTable(lblID.Text);
                    //    dt = ds1.Tables[TabName.ToString()];
                    //    DS_GridViewParticulars.Merge(dt);
                    //}

                    // GET DATA   

                }
                ViewState["DS_GridViewParticulars"] = DS_GridViewParticulars;
            }

            string status = "0";
            DataTable dt_GridViewLedger = new DataTable();
            dt_GridViewLedger.Columns.Add(new DataColumn("LedgerID", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("LedgerName", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("Status", typeof(string)));

            if (dt_GridViewItem.Rows.Count > 0)
            {

                decimal CGST = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("CGST"));
                decimal SGST = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("SGST"));
                dt_GridViewLedger.Rows.Add("15", "CGST", CGST.ToString(), status);
                dt_GridViewLedger.Rows.Add("16", "SGST", SGST.ToString(), status);
            }
            else
            {
                dt_GridViewLedger.Rows.Add("15", "CGST", "0", status);
                dt_GridViewLedger.Rows.Add("16", "SGST", "0", status);
            }
            gridRows = GridViewLedger.Rows.Count;
            if (gridRows > 2)
            {
                for (rowIndex = 2; rowIndex < gridRows; rowIndex++)
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
                dt_GridViewLedger.Rows.Add("17", "Round Off", "0", status);
            }

            GridViewLedger.DataSource = dt_GridViewLedger;
            GridViewLedger.DataBind();


            ViewState["RowNo"] = "0";
            // GridViewLedger
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
                if (rowIndex > 2)
                {
                    status = "1";
                }
                Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                Label lblLedgerName = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblLedgerName");
                TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
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
    protected void GridViewItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string ID = GridViewItem.DataKeys[e.RowIndex].Value.ToString();
            AddItem(ID);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlItem.SelectedIndex == 0)
            {
                msg += "Select Item Name. \\n";
            }
            if (txtQuantity.Text == "")
            {
                msg += "Enter Quantity. \\n";
            }
            if (txtRate.Text == "")
            {
                msg += "Enter Rate. \\n";
            }
            if (txtAmount.Text == "")
            {
                msg += "Enter Amount. \\n";
            }
            if (msg.Trim() == "")
            {
                AddItem("0");
                ClearItem();

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
    protected void btnAddLedgerAmt_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlLedger.SelectedIndex > 0 && txtLedgerAmt.Text != "")
            {

                FillLedgerAmount("0");
                ddlLedger.ClearSelection();
                txtLedgerAmt.Text = "";
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


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected string FillParticularAmount(string ID)
    {
        string Particular = "0";
        try
        {


            DataTable dt_GridViewParticulars = new DataTable();
            dt_GridViewParticulars.Columns.Add(new DataColumn("RID", typeof(string)));
            dt_GridViewParticulars.Columns.Add(new DataColumn("Item_ID", typeof(string)));
            dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularID", typeof(string)));
            dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularName", typeof(string)));
            dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularAmt", typeof(decimal)));
            int gridRows = GridViewParticulars.Rows.Count;
            int i = 0;
            for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {

                Label lblID = (Label)GridViewParticulars.Rows[rowIndex].Cells[0].FindControl("lblID");
                Label Item_ID = (Label)GridViewParticulars.Rows[rowIndex].Cells[0].FindControl("Item_ID");
                Label lblParticularID = (Label)GridViewParticulars.Rows[rowIndex].Cells[0].FindControl("lblParticularID");
                Label lblLedgerName = (Label)GridViewParticulars.Rows[rowIndex].Cells[0].FindControl("lblParticularsName");
                Label lblAmount = (Label)GridViewParticulars.Rows[rowIndex].Cells[1].FindControl("lblAmount");
                if (lblID.Text != ID)
                {
                    i++;
                    dt_GridViewParticulars.Rows.Add(i.ToString(), Item_ID.Text, lblParticularID.Text, lblLedgerName.Text, lblAmount.Text);
                }
            }
            if (ID == "0")
            {
                i++;
                dt_GridViewParticulars.Rows.Add(i.ToString(), ddlItem.SelectedValue.ToString(), ddlParticulars.SelectedValue.ToString(), ddlParticulars.SelectedItem.ToString(), txtParticularAmt.Text);
            }

            GridViewParticulars.DataSource = dt_GridViewParticulars;
            GridViewParticulars.DataBind();


            if (dt_GridViewParticulars.Rows.Count > 0)
            {

                decimal ParticularAmt = dt_GridViewParticulars.AsEnumerable().Sum(row => row.Field<decimal>("ParticularAmt"));
                Particular = ParticularAmt.ToString();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        return Particular.ToString();
    }
    protected void CreateBillByBillTable()
    {
        DataTable dt_BillByBillTable = new DataTable();
        dt_BillByBillTable.Columns.Add(new DataColumn("RID", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
        dt_BillByBillTable.Columns.Add(new DataColumn("Type", typeof(string)));

        ViewState["BillByBillTable"] = dt_BillByBillTable;

        //GridViewBillByBillDetail.DataSource = dt_BillByBillTable;
        //GridViewBillByBillDetail.DataBind();
    }
    protected void btnAddValue_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet DS_GridViewParticulars = (DataSet)ViewState["DS_GridViewParticulars"];
            lblMsg.Text = "";
            string msg = "";
            if (ddlItem.SelectedIndex == 0)
            {
                msg += "Select Item Name. \\n";
            }
            if (txtQuantity.Text == "")
            {
                msg += "Enter Quantity. \\n";
            }
            if (txtRate.Text == "")
            {
                msg += "Enter Rate. \\n";
            }
            if (txtAmount.Text == "")
            {
                msg += "Enter Amount. \\n";
            }
            if (ddlParticulars.SelectedIndex == 0)
            {
                msg += "Select Particulars. \\n";
            }
            if (txtParticularAmt.Text == "")
            {
                msg += "Enter Particular Amount. \\n";
            }
            if (msg.Trim() == "")
            {

                //AddItem("0");
                //ClearItem();
                int rowIndex = 0;
                int gridRows = GridViewItem.Rows.Count;
                if (gridRows > 0)
                {
                    for (rowIndex = 0; rowIndex < gridRows; rowIndex++)
                    {
                        Label lblItem = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItem");
                        if (lblItem.Text == ddlItem.SelectedItem.Text)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Item already exists');", true);
                        }
                        else
                        {
                            string Particular = FillParticularAmount("0");

                            if (float.Parse(txtAmount.Text) != float.Parse(Particular))
                            {
                                ddlParticulars.ClearSelection();
                                txtParticularAmt.Text = "";
                                ViewState["RowNo"] = "0";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal();", true);
                            }
                            else
                            {
                                AddItem(ViewState["RowNo"].ToString());
                                ClearItem();

                                string TNo = ddlItem.SelectedValue.ToString();

                                //if (TNo == "0")
                                //{
                                //    TNo = GridViewItem.Rows.Count.ToString();
                                //}
                                //else
                                //{
                                //    DS_GridViewParticulars.Tables.Remove(TNo);
                                //}
                                DataTable dt_GridViewParticulars = new DataTable(TNo);
                                dt_GridViewParticulars.Columns.Add(new DataColumn("RID", typeof(string)));
                                dt_GridViewParticulars.Columns.Add(new DataColumn("Item_ID", typeof(string)));
                                dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularID", typeof(string)));
                                dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularName", typeof(string)));
                                dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularAmt", typeof(decimal)));

                                int RID = 0;
                                int gridParticularRows = GridViewParticulars.Rows.Count;
                                for (int row = 0; rowIndex < gridParticularRows; rowIndex++)
                                {
                                    RID++;
                                    Label lblID = (Label)GridViewParticulars.Rows[row].Cells[0].FindControl("lblID");
                                    Label lblParticularID = (Label)GridViewParticulars.Rows[row].Cells[0].FindControl("lblParticularID");
                                    Label Item_ID = (Label)GridViewParticulars.Rows[rowIndex].Cells[0].FindControl("Item_ID");
                                    Label lblLedgerName = (Label)GridViewParticulars.Rows[row].Cells[0].FindControl("lblParticularsName");
                                    Label lblAmount = (Label)GridViewParticulars.Rows[row].Cells[1].FindControl("lblAmount");

                                    dt_GridViewParticulars.Rows.Add(RID.ToString(), Item_ID.Text, lblParticularID.Text, lblLedgerName.Text, lblAmount.Text);
                                }

                                DS_GridViewParticulars.Merge(dt_GridViewParticulars);
                                //ViewState["DS_GridViewParticulars"] = DS_GridViewParticulars;

                                ddlParticulars.ClearSelection();
                                txtParticularAmt.Text = "";
                                GridViewParticulars.DataSource = null;
                                GridViewParticulars.DataBind();
                                ViewState["RowNo"] = "0";
                                //  DS_GridViewParticulars.Merge(dt_GridViewParticulars);
                                // GET DATA     DataTable td = DS_GridViewParticulars.Tables["TableName"];
                            }

                        }
                    }
                }
                else
                {
                    string Particular = FillParticularAmount("0");

                    if (float.Parse(txtAmount.Text) != float.Parse(Particular))
                    {
                        ddlParticulars.ClearSelection();
                        txtParticularAmt.Text = "";
                        ViewState["RowNo"] = "0";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal();", true);
                    }
                    else
                    {
                        AddItem(ViewState["RowNo"].ToString());
                        ClearItem();

                        string TNo = ddlItem.SelectedValue.ToString();

                        //if (TNo == "0")
                        //{
                        //    TNo = GridViewItem.Rows.Count.ToString();
                        //}
                        //else
                        //{
                        //    DS_GridViewParticulars.Tables.Remove(TNo);
                        //}
                        DataTable dt_GridViewParticulars = new DataTable(TNo);
                        dt_GridViewParticulars.Columns.Add(new DataColumn("RID", typeof(string)));
                        dt_GridViewParticulars.Columns.Add(new DataColumn("Item_ID", typeof(string)));
                        dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularID", typeof(string)));
                        dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularName", typeof(string)));
                        dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularAmt", typeof(decimal)));

                        int RID = 0;
                        int gridParticularRows = GridViewParticulars.Rows.Count;
                        for (int rows = 0; rows < gridParticularRows; rows++)
                        {
                            RID++;
                            Label lblID = (Label)GridViewParticulars.Rows[rows].Cells[0].FindControl("lblID");
                            Label Item_ID = (Label)GridViewParticulars.Rows[rows].Cells[0].FindControl("Item_ID");
                            Label lblParticularID = (Label)GridViewParticulars.Rows[rows].Cells[0].FindControl("lblParticularID");
                            Label lblLedgerName = (Label)GridViewParticulars.Rows[rows].Cells[0].FindControl("lblParticularsName");
                            Label lblAmount = (Label)GridViewParticulars.Rows[rows].Cells[1].FindControl("lblAmount");

                            dt_GridViewParticulars.Rows.Add(RID.ToString(), Item_ID.Text, lblParticularID.Text, lblLedgerName.Text, lblAmount.Text);
                        }

                        DS_GridViewParticulars.Merge(dt_GridViewParticulars);
                        // ViewState["DS_GridViewParticulars"] = DS_GridViewParticulars;

                        ddlParticulars.ClearSelection();
                        txtParticularAmt.Text = "";
                        GridViewParticulars.DataSource = null;
                        GridViewParticulars.DataBind();
                        ViewState["RowNo"] = "0";

                        //  DS_GridViewParticulars.Merge(dt_GridViewParticulars);
                        // GET DATA     DataTable td = DS_GridViewParticulars.Tables["TableName"];
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
    protected void GridViewParticulars_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string ID = GridViewParticulars.DataKeys[e.RowIndex].Value.ToString();
            FillParticularAmount(ID);
            ddlParticulars.ClearSelection();
            txtParticularAmt.Text = "";
            ViewState["RowNo"] = "0";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal();", true);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
            ViewState["Amount"] = lblGrandTotal.Text;
            FillRefAmount("0");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }


    }
    protected void FillRefAmount(string ID)
    {
        try
        {
            DataTable dt_BillByBillTable = new DataTable();
            dt_BillByBillTable.Columns.Add(new DataColumn("RID", typeof(string)));
            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
            dt_BillByBillTable.Columns.Add(new DataColumn("Type", typeof(string)));
            string Type = "";

            if (ddlBillByBillTx_crdr.SelectedValue == "Cr")
            {
                Type = "Cr";
            }
            else
            {
                Type = "Dr";
            }
            int gridRows = GridViewRef.Rows.Count;
            for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {
                Label lblID = (Label)GridViewRef.Rows[rowIndex].Cells[0].FindControl("lblID");
                Label lblTypeOfRef = (Label)GridViewRef.Rows[rowIndex].Cells[0].FindControl("lblTypeOfRef");
                Label lblRefNo = (Label)GridViewRef.Rows[rowIndex].Cells[1].FindControl("lblRefNo");
                Label lblAmount = (Label)GridViewRef.Rows[rowIndex].Cells[2].FindControl("lblAmount");
                Label lblType = (Label)GridViewRef.Rows[rowIndex].Cells[2].FindControl("lblType");
                if (lblID.Text != ID)
                {
                    dt_BillByBillTable.Rows.Add(lblID.Text, lblTypeOfRef.Text, lblRefNo.Text, lblAmount.Text, lblType.Text);
                    if (lblType.Text == "Dr")
                    {
                        Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(lblAmount.Text));
                        ViewState["Amount"] = Amount.ToString();
                        lblAmount.Text = ViewState["Amount"].ToString();
                    }
                    else
                    {
                        Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) + Convert.ToDecimal(lblAmount.Text));
                        ViewState["Amount"] = Amount.ToString();
                        lblAmount.Text = ViewState["Amount"].ToString();
                    }
                }

            }
            if (ID == "0")
            {
                int RefType = ddlRefType.SelectedIndex;
                string RefNo = "";
                if (RefType == 0 || RefType == 2)
                {
                    RefNo = txtBillByBillTx_Ref.Text;
                }
                else if (RefType == 3)
                {
                    RefNo = "OnAccount";
                }
                else
                {
                    RefNo = ddlBillByBillTx_Ref.SelectedValue;
                }
                dt_BillByBillTable.Rows.Add((gridRows + 1).ToString(), ddlRefType.SelectedItem.Text, RefNo, txtBillByBillTx_Amount.Text, Type);
                if (ddlBillByBillTx_crdr.SelectedValue == "Dr")
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
            }


            DataTable dt_BillByBillData = (DataTable)ViewState["dt_BillByBillData"];
            //foreach (DataRow rows in dt_BillByBillData.Rows)
            //{
            //    if (rows["BillByBillTx_Ref"].ToString().Equals(ddlBillByBillTx_Ref.SelectedValue))
            //    {
            //        dt_BillByBillData.Rows.Remove(rows);
            //        dt_BillByBillData.AcceptChanges();
            //        break;
            //    }
            //}
            ViewState["dt_BillByBillData"] = dt_BillByBillData;
            ddlBillByBillTx_Ref.DataSource = dt_BillByBillData;
            ddlBillByBillTx_Ref.DataTextField = "AgnstRef";
            ddlBillByBillTx_Ref.DataValueField = "BillByBillTx_Ref";
            ddlBillByBillTx_Ref.DataBind();
            ddlBillByBillTx_Ref.Items.Insert(0, "Select");




            GridViewRef.DataSource = dt_BillByBillTable;
            GridViewRef.DataBind();

            //decimal RefTotal = 0;
            //RefTotal = dt_BillByBillTable.AsEnumerable().Sum(row => row.Field<decimal>("BillByBillTx_Amount"));

            //GridViewBillByBillDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
            //GridViewBillByBillDetail.FooterRow.Cells[3].Text = "<b>" + RefTotal.ToString() + "</b>";

            //txtBillByBillTx_Amount.Text = (Convert.ToDecimal(txtLedgerTx_Amount.Text) - RefTotal).ToString();
            txtBillByBillTx_Ref.Text = "";
            ddlRefType.ClearSelection();
            if (ViewState["Amount"].ToString() == "0" || ViewState["Amount"].ToString() == "0.00")
            {
                btnAddBillByBill.Enabled = false;
                btnSubmit.Enabled = true;
            }
            else
            {
                btnAddBillByBill.Enabled = true;
                btnSubmit.Enabled = false;
            }
            //ViewState["BillByBillTable"] = dt_BillByBillTable;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);

            txtBillByBillTx_Ref.Visible = true;
            txtBillByBillTx_Ref.Text = txtVoucherTx_No.Text;
            //BindBillByBillData();
            ddlRefType.ClearSelection();
            ddlBillByBillTx_crdr.SelectedValue = "Dr";
            txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();
            txtBillByBillTx_Ref.Enabled = true;
            ddlBillByBillTx_Ref.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewRef_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string ID = GridViewRef.DataKeys[e.RowIndex].Value.ToString();
            ViewState["Amount"] = lblGrandTotal.Text;
            FillRefAmount(ID);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);
    }
    protected void BindBillByBillData()
    {

        DataTable dt_BillByBillData = (DataTable)ViewState["dt_BillByBillData"];

        ddlBillByBillTx_Ref.Items.Clear();
        string LedgerID = ddlLedger.SelectedValue.ToString();
        ds = objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "Ledger_ID" }, new string[] { "2", LedgerID }, "dataset");
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

        }
        else
        {
            ddlBillByBillTx_Ref.Items.Clear();
            ddlBillByBillTx_Ref.Items.Insert(0, "Select");
        }


    }
    protected void ddlRefType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBillByBillData();
        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);
            if (ddlRefType.SelectedValue.ToString() == "1")
            {
                txtBillByBillTx_Ref.Visible = false;
                ddlBillByBillTx_Ref.Visible = true;
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
                txtBillByBillTx_Ref.Text = txtVoucherTx_No.Text;
                txtBillByBillTx_Ref.Visible = true;
                ddlBillByBillTx_Ref.Visible = false;
                txtBillByBillTx_Ref.Enabled = true;

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
            lblMsg.Text = "";
            string msg = "";
            if (txtVoucherTx_No.Text == "")
            {
                msg += "Enter Voucher No. \\n";
            }
            if (txtVoucherTx_Date.Text == "")
            {
                msg += "Enter Date. \\n";
            }
            if (ddlPartyName.SelectedIndex == 0)
            {
                msg += "Select Party A/c Name. \\n";
            }
            if (GridViewItem.Rows.Count == 0)
            {
                msg += "Add item Detail. \\n";
            }
            if (msg.Trim() == "")
            {
                ViewState["Amount"] = lblGrandTotal.Text;
                txtBillByBillTx_Ref.Text = txtVoucherTx_No.Text;
                txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();
                ddlBillByBillTx_crdr.SelectedValue = "Dr";
                //    txtRefAmount.Text = lblGrandTotal.Text;
                // CreateBillByBillTable();
                GridViewRef.DataSource = new string[] { };
                GridViewRef.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);
                btnAddBillByBill.Enabled = true;
                btnSubmit.Enabled = false;

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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (txtVoucherTx_No.Text == "")
            {
                msg += "Enter Voucher No. \\n";
            }
            if (txtVoucherTx_Date.Text == "")
            {
                msg += "Enter Date. \\n";
            }
            if (ddlPartyName.SelectedIndex == 0)
            {
                msg += "Select Party A/c Name. \\n";
            }
            if (GridViewItem.Rows.Count == 0)
            {
                msg += "Add item Detail. \\n";
            }
            //float GrandTotal = float.Parse(lblGrandTotal.Text);
            //float RefTotal = float.Parse(lblRefTotal.Text);
            //if (GrandTotal != RefTotal)
            //{
            //    msg += "Amount Not Clear. \\n";
            //}
            if (msg.Trim() == "")
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


                string VoucherTx_Type = "GST" + lblVoucherType.Text + " Purchase";
                string VoucherTx_Name = "Purchase Voucher";
                string VoucherTx_Ref = txtInvoice.Text;
                string VoucherTx_Amount = lblGrandTotal.Text;
                string ItemTx_IsActive = "1";
                string LedgerTx_IsActive = "1";
                string VoucherTx_IsActive = "1";
                int Status = 0;
                DataSet ds11 = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "VoucherTx_No", "VoucherTx_ID" },
                    new string[] { "9", txtVoucherTx_No.Text, ViewState["VoucherTx_ID"].ToString() }, "dataset");
                if (ds11.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds11.Tables[0].Rows[0]["Status"].ToString());

                }
                if (btnAccept.Text == "Accept" && ViewState["VoucherTx_ID"].ToString() == "0" && Status == 0)
                {
                    DataSet ds2 = objdb.ByProcedure("SpFinVoucherTx",
                  new string[] { "flag","VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type"
                    , "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY"
                    , "VoucherTx_IsActive", "VoucherTx_InsertedBy" },
                  new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), VoucherTx_Name, VoucherTx_Type,txtVoucherTx_No.Text
                   ,VoucherTx_Ref,txtVoucherTx_Narration.Text,lblGrandTotal.Text,Month.ToString(),Year.ToString(), ViewState["Office_ID"].ToString(),ViewState["Voucher_FY"].ToString()
                    ,VoucherTx_IsActive,ViewState["Emp_ID"].ToString()}, "dataset");


                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        string VoucherTx_ID = ds2.Tables[0].Rows[0]["VoucherTx_ID"].ToString();

                        objdb.ByProcedure("SpFinLedgerTx",
                        new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                        , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy" },
                        new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
                        ,ViewState["Voucher_FY"].ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1"}, "dataset");

                        int gridRows = GridViewItem.Rows.Count;
                        for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                        {
                            Label lblID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblID");
                            Label lblItemID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
                            Label lblUnit_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblUnit_id");
                            Label lblWarehouse_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_id");
                            Label lblItem = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItem");
                            Label lblQuantity = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblQuantity");
                            Label lblRate = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblRate");
                            Label lblAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblAmount");
                            Label lblCGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblCGST");
                            Label lblSGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblSGST");
                            Label lblTotalAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblTotalAmount");
                            objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Name", "VoucherTx_Type", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "Warehouse_id", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy" }, new string[] { "0", VoucherTx_ID, VoucherTx_Name, VoucherTx_Type, lblItemID.Text, lblUnit_id.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, lblWarehouse_id.Text, ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 1).ToString() }, "dataset");



                            //DataTable dt = DS_GridViewParticulars.Tables[lblID.Text];
                            //int dtRowCount = dt.Rows.Count;
                            //for (int i = 0; i < dtRowCount; i++)
                            //{
                            //    Label lblLedger_ID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblParticularID");
                            //    Label lblLedgerAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblAmount");

                            //    objdb.ByProcedure("SpFinLedgerTx",
                            //    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            //        , "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy" },
                            //    new string[] { "0", lblLedger_ID.Text,VoucherTx_ID,VoucherTx_Type,lblLedgerAmount.Text ,Month.ToString(),Year.ToString()
                            //        ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() ,"1",ViewState["Emp_ID"].ToString()}, "dataset");
                            //}



                            //int dtRowCount = GridViewLedger.Rows.Count;    
                            //for (int i = 0; i < dtRowCount; i++)
                            //{
                            //    Label lblLedger_ID = (Label)GridViewLedger.Rows[i].Cells[0].FindControl("lblID");
                            //    TextBox lblLedgerAmount = (TextBox)GridViewLedger.Rows[i].Cells[1].FindControl("txtAmount");

                            //    objdb.ByProcedure("SpFinLedgerTx",
                            //    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            //        , "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy" },
                            //    new string[] { "0", lblLedger_ID.Text,VoucherTx_ID,VoucherTx_Type,lblLedgerAmount.Text ,Month.ToString(),Year.ToString()
                            //        ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() ,"1",ViewState["Emp_ID"].ToString()}, "dataset");
                            //}

                        }
                        int gridLedgerRows = GridViewLedger.Rows.Count;
                        for (int rowIndex = 0; rowIndex < gridLedgerRows; rowIndex++)
                        {

                            Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                            TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                            objdb.ByProcedure("SpFinLedgerTx",
                            new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
                            new string[] { "0", lblID.Text, "Sub Ledger", VoucherTx_ID, VoucherTx_Type, txtAmount.Text, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 1).ToString() }, "dataset");

                        }
                        DataSet DS_GridViewParticulars = (DataSet)ViewState["DS_GridViewParticulars"];
                        for (int i = 0; i < DS_GridViewParticulars.Tables.Count; i++)
                        {
                            for (int j = 0; j < DS_GridViewParticulars.Tables[i].Rows.Count; j++)
                            {
                                string ParticularID = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularID"].ToString();
                                string Amount = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularAmt"].ToString();
                                string Item_id = DS_GridViewParticulars.Tables[i].Rows[j]["Item_ID"].ToString();
                                objdb.ByProcedure("SpFinLedgerTx",
                                new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
                                new string[] { "0", ParticularID, "Item Ledger", VoucherTx_ID, Item_id, VoucherTx_Type, Amount, Month.ToString(), Year.ToString(), ViewState["Voucher_FY"].ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (j + 1).ToString() }, "dataset");
                            }
                        }
                        int gridBillbyBillRows = GridViewRef.Rows.Count;
                        for (int k = 0; k < gridBillbyBillRows; k++)
                        {


                            Label BillByBillTx_RefType = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblTypeOfRef");
                            Label BillByBillTx_Ref = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblRefNo");
                            Label BillByBillTx_Amount = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblAmount");
                            Label Type = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblType");
                            if (Type.Text == "Dr")
                            {
                                BillByBillTx_Amount.Text = "-" + BillByBillTx_Amount.Text;
                            }

                            objdb.ByProcedure("SpFinBillByBillTx",
                                        new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy" },
                                        new string[] { "3", VoucherTx_ID, ddlPartyName.SelectedValue.ToString(), BillByBillTx_RefType.Text, BillByBillTx_Ref.Text, BillByBillTx_Amount.Text, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), ViewState["Voucher_FY"].ToString(), "1", (k + 1).ToString() }, "dataset");

                        }
                    }

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success","Thank you!", "Operation Completed Successfully.");
                    ClearData();
                }

            }
            else
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);
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
            txtInvoice.Text = "";
            ddlPartyName.ClearSelection();
            txtCurrentBalance.Text = "";
            GridViewItem.DataSource = new string[] { };
            GridViewItem.DataBind();
            ddlLedger.ClearSelection();
            txtLedgerAmt.Text = "";
            GridViewParticulars.DataSource = new string[] { };
            GridViewParticulars.DataBind();
            GridViewRef.DataSource = new string[] { };
            GridViewRef.DataBind();
            GridViewLedger.DataSource = new string[] { };
            GridViewLedger.DataBind();
            CreateParticularDataset();
            AddItem("NA");
            txtVoucherTx_Narration.Text = "";
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
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "8", ViewState["VoucherTx_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtVoucherTx_No.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    lblGrandTotal.Text = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    GridViewItem.DataSource = ds.Tables[1];
                    GridViewItem.DataBind();
                }
                if (ds.Tables[3].Rows.Count > 0)
                {

                    GridViewLedger.DataSource = ds.Tables[3];
                    GridViewLedger.DataBind();



                }
                if (ds.Tables[4].Rows.Count > 0)
                {
                    DataSet DS_GridViewParticulars = (DataSet)ViewState["DS_GridViewParticulars"];
                    int rowcount = ds.Tables[4].Rows.Count;
                    for (int i = 0; i < rowcount; i++)
                    {

                        string RID = ds.Tables[4].Rows[i]["RID"].ToString();
                        string Item_ID = ds.Tables[4].Rows[i]["Item_ID"].ToString();
                        string ParticularID = ds.Tables[4].Rows[i]["ParticularID"].ToString();
                        string ParticularName = ds.Tables[4].Rows[i]["ParticularName"].ToString();
                        string ParticularAmt = ds.Tables[4].Rows[i]["ParticularAmt"].ToString();
                        string TNo = ds.Tables[4].Rows[i]["Item_ID"].ToString();

                        DataTable dt_GridViewParticulars = new DataTable(TNo);
                        dt_GridViewParticulars.Columns.Add(new DataColumn("RID", typeof(string)));
                        dt_GridViewParticulars.Columns.Add(new DataColumn("Item_ID", typeof(string)));
                        dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularID", typeof(string)));
                        dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularName", typeof(string)));
                        dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularAmt", typeof(decimal)));

                        dt_GridViewParticulars.Rows.Add(RID, Item_ID, ParticularID, ParticularName, ParticularAmt);
                        DS_GridViewParticulars.Merge(dt_GridViewParticulars);
                        ViewState["DS_GridViewParticulars"] = DS_GridViewParticulars;

                    }
                }
                if (ds.Tables[5].Rows.Count > 0)
                {
                    GridViewRef.DataSource = ds.Tables[5];
                    GridViewRef.DataBind();

                }
                btnAccept.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}