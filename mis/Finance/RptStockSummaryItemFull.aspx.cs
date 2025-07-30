using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.Drawing;
using System.IO;

public partial class mis_Finance_RptStockSummaryItemFull : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ddlOffice.Enabled = false;
                    FillFromDate();
                    FillDropdown();
                    SelectDropdown();

                    ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
                    ViewState["ToDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");

                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        ddlOffice.Enabled = false;
                    }
                    fill_details();
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void SelectDropdown()
    {
        try
        {
            if (Request.QueryString["FromDate"] != null && Request.QueryString["ToDate"] != null && Request.QueryString["MltOfficeID"] != null)
            {
                string FromDate = objdb.Decrypt(Request.QueryString["FromDate"].ToString());
                string ToDate = objdb.Decrypt(Request.QueryString["ToDate"].ToString());
                string MltOfficeID = objdb.Decrypt(Request.QueryString["MltOfficeID"].ToString());

                var OfficeString = MltOfficeID.Split(',', ' ');

                int count = OfficeString.Length;
                ddlOffice.ClearSelection();
                for (int i = 0; i <= count; i++)
                {
                    string Value = OfficeString[i].ToString();
                    foreach (ListItem item in ddlOffice.Items)
                    {
                        if (item.Value == Value)
                        {
                            item.Selected = true;
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {

            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
   
    protected void FillFromDate()
    {
        try
        {
            ds = null;
            string firstDateOfYear = "";
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                firstDateOfYear = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }

            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(Convert.ToDateTime(firstDateOfYear, cult).ToString("yyyy/MM/dd")));
            int mn = datevalue.Month;
            int yy = datevalue.Year;
            if (mn < 4)
            {
                txtFromDate.Text = "01/04/" + (yy - 1).ToString();
                txtToDate.Text = "01/04/" + (yy - 1).ToString();

            }
            else
            {
                txtFromDate.Text = "01/04/" + (yy).ToString();
                txtToDate.Text = "01/04/" + (yy).ToString();
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillVoucherDate()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillDropdown()
    {
        try
        {
            if (ViewState["Office_ID"].ToString() == "1")
            {
                ddlOffice.Enabled = true;
            }
            ds = objdb.ByProcedure("SpAdminOffice",
                   new string[] { "flag" },
                   new string[] { "10" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }

            ds = objdb.ByProcedure("SpFinRptRptStockSummaryItemFull",
                   new string[] { "flag" },
                   new string[] { "101" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlStockGroups.DataSource = ds;
                ddlStockGroups.DataTextField = "ItemTypeName";
                ddlStockGroups.DataValueField = "ItemType_id";
                ddlStockGroups.DataBind();
                ddlStockGroups.Items.Insert(0, new ListItem("All", "0"));
                ddlStockGroups.SelectedValue = "0";
            }

            /***********/
            ds = objdb.ByProcedure("SpFinRptRptStockSummaryItemFull",
                   new string[] { "flag", "Office_ID_Mlt", "ItemGroup" },
                   new string[] { "102", ViewState["Office_ID"].ToString(), "0" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlStockItems.DataSource = ds;
                ddlStockItems.DataTextField = "ItemName";
                ddlStockItems.DataValueField = "Item_id";
                ddlStockItems.DataBind();
            }
            /***********/
        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        //GridView1.DataSource = new string[]{};
        //GridView1.DataBind();
        DivGrid3.Visible = false;
        DivGrid4.Visible = false;
        DivGrid2.Visible = true;
        GridView3.DataSource = new string[] { };
        GridView3.DataBind();
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            fill_details();
        }

    }

    private void fill_details()
    {
        try
        {
            string Office = "";
            
            string OfficeName = "";
            int SerialNo = 0;
            int totalListItem = ddlOffice.Items.Count;
            foreach (ListItem item in ddlOffice.Items)
            {

                if (item.Selected)
                {
                    SerialNo++;
                    Office += item.Value + ",";
                    OfficeName += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
                }
            }
            if (totalListItem == SerialNo)
            {
                OfficeName = "All Offices";
            }
            else if (SerialNo == 0)
            {
                OfficeName = "---Office Not Selected---";
            }
            else
            {
                OfficeName = OfficeName.Remove(OfficeName.Length - 1, 1);
            }

            string headingFirst = "<p class='text-center' style='font-weight:600'>Stock Summary <br /> MP State Agro Industries Development Corporation, <br/> [ " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
            lblheadingFirst.Text = headingFirst;
            /*************/
            string itemlist = "";
            foreach (ListItem item in ddlStockItems.Items)
            {
                if (item.Selected)
                {
                    itemlist += item.Value + ",";
                }
            }
            /*************/

            //GridView1.DataSource = null;
            GridView2.DataSource = null;
            ds = objdb.ByProcedure("SpFinRptRptStockSummaryItemFull",
                   new string[] { "flag", "Office_ID_Mlt", "ToDate", "FromDate", "ItemGroup", "item_id_Mlt", "Order_By" },
                   new string[] { "0", Office, ViewState["ToDate"].ToString(), ViewState["FromDate"].ToString(),ddlStockGroups.SelectedValue.ToString(),itemlist, "Office" }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                GridView2.DataSource = ds.Tables[0];
                GridView2.DataBind();

                //foreach (GridViewRow row in GridView2.Rows)
                //{

                //    Label lblstock = (Label)row.FindControl("lblStockType");
                //    if (lblstock.Text == "ItemTypes")
                //    {
                //        row.CssClass = "GroupHeading";
                //    }
                //    else
                //    {
                //        row.CssClass = "GroupChildren";
                //    }

                //}

                GridView2.UseAccessibleHeader = true;
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView2.FooterRow.Cells[0].Text = "Grand Total";
                //decimal OpeningQauntity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OpeningQuantity"));
                //GridView2.FooterRow.Cells[1].Text = "<b>" + OpeningQauntity + "</b>";
                //decimal OpeningRate = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OpeningRate"));
                //GridView2.FooterRow.Cells[2].Text = "<b>" + OpeningRate + "</b>";
                //decimal OpeningValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OpeningValue"));
                //GridView2.FooterRow.Cells[3].Text = "<b>" + OpeningValue + "</b>";

                decimal InwardQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("InwardQuantity"));
                GridView2.FooterRow.Cells[4].Text = "<b>" + InwardQuantity + "</b>";
                decimal InwardRate = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("InwardRate"));
                GridView2.FooterRow.Cells[5].Text = "<b>" + InwardRate + "</b>";
                decimal InwardValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("InwardValue"));
                GridView2.FooterRow.Cells[6].Text = "<b>" + InwardValue + "</b>";

                decimal OutwardQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OutwardQuantity"));
                GridView2.FooterRow.Cells[7].Text = "<b>" + OutwardQuantity + "</b>";
                decimal OutwardRate = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OutwardRate"));
                GridView2.FooterRow.Cells[8].Text = "<b>" + OutwardRate + "</b>";
                decimal OutwardValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OutwardValue"));
                GridView2.FooterRow.Cells[9].Text = "<b>" + OutwardValue + "</b>";

                //decimal ClosingQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ClosingQuantity"));
                //GridView2.FooterRow.Cells[10].Text = "<b>" + ClosingQuantity + "</b>";
                //decimal ClosingRate = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ClosingRate"));
                //GridView2.FooterRow.Cells[11].Text = "<b>" + ClosingRate + "</b>";
                //decimal ClosingValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ClosingValue"));
                //GridView2.FooterRow.Cells[12].Text = "<b>" + ClosingValue + "</b>";
                GridView2.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                GridView2.DataSource = new string[] { };
                GridView2.DataBind();
            }
        }
        catch (Exception ex)
        {

            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //GridView HeaderGrid = (GridView)sender;
            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //TableCell HeaderCell = new TableCell();
            //HeaderCell.Text = "";
            //HeaderCell.ColumnSpan = 1;
            //HeaderGridRow.Cells.Add(HeaderCell);


            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Opening";
            //HeaderCell.ColumnSpan = 3;
            //HeaderCell.Font.Bold = true;
            //HeaderGridRow.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Inward";
            //HeaderCell.ColumnSpan = 3;
            //HeaderCell.Font.Bold = true;
            //HeaderGridRow.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Outward";
            //HeaderCell.ColumnSpan = 3;
            //HeaderCell.Font.Bold = true;
            //HeaderGridRow.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Closing";
            //HeaderCell.ColumnSpan = 3;
            //HeaderCell.Font.Bold = true;
            //HeaderGridRow.Cells.Add(HeaderCell);

            //GridView2.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }



    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = GridView2.SelectedRow.RowIndex;
        GridViewRow row = GridView2.Rows[index];
        LinkButton Particular = (LinkButton)row.Cells[0].FindControl("lnkbtnParticular");
        int item_id = int.Parse(Particular.ToolTip.ToString());
        string item_name = Particular.Text;
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            //string VoucherTx_Type = Voucher_Name.Text;
            FillGridNextLedger(item_id, item_name);
        }
    }

    protected void FillGridNextLedger(int item_id, string item_name)
    {
        try
        {
            //GridView1.DataSource = new string[] { };
            //GridView1.DataBind();

            DivGrid3.Visible = true;
            DivGrid4.Visible = false;
            DivGrid2.Visible = false;

            string Office = "";
            string OfficeName = "";
            int SerialNo = 0;
            int totalListItem = ddlOffice.Items.Count;
            foreach (ListItem item in ddlOffice.Items)
            {

                if (item.Selected)
                {
                    SerialNo++;
                    Office += item.Value + ",";
                    OfficeName += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
                }
            }
            if (totalListItem == SerialNo)
            {
                OfficeName = "All Offices";
            }
            else if (SerialNo == 0)
            {
                OfficeName = "---Office Not Selected---";
            }
            else
            {
                OfficeName = OfficeName.Remove(OfficeName.Length - 1, 1);
            }
            string headingSecond = "<p class='text-center' style='font-weight:600'>" + item_name + " <br /> MP State Agro Industries Development Corporation,  <br/> [ " + OfficeName + " ]  <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
            lblheadingSecond.Text = headingSecond;
            lblItemId.Text = item_id.ToString();
            lblItemId.ToolTip = item_name.ToString();
            //lblItemId.Visible = false;
            int yearFY = FillFromYear();



            ds = objdb.ByProcedure("SpFinRptRptStockSummaryItemFull", new string[] { "flag", "Office_ID_Mlt", "item_id", "FromDate", "ToDate", "FYear" }, new string[] { "1", Office, item_id.ToString(), ViewState["FromDate"].ToString(), ViewState["ToDate"].ToString(), yearFY.ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                GridView3.DataSource = ds;
                GridView3.DataBind();
                GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView3.UseAccessibleHeader = true;

                GridView3.FooterRow.Cells[0].Text = "Grand Total";
                //decimal OpeningQauntity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OpeningQuantity"));
                //GridView3.FooterRow.Cells[1].Text = "<b>" + OpeningQauntity + "</b>";
                //decimal OpeningRate = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OpeningQuantity"));
                //GridView3.FooterRow.Cells[2].Text = "<b>" + OpeningRate + "</b>";
                //decimal OpeningValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OpeningQuantity"));
                //GridView3.FooterRow.Cells[3].Text = "<b>" + OpeningValue + "</b>";

                decimal InwardQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("InwardQuantity"));
                GridView3.FooterRow.Cells[4].Text = "<b>" + InwardQuantity + "</b>";
                decimal InwardRate = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("InwardRate"));
                GridView3.FooterRow.Cells[5].Text = "<b>" + InwardRate + "</b>";
                decimal InwardValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("InwardValue"));
                GridView3.FooterRow.Cells[6].Text = "<b>" + InwardValue + "</b>";

                decimal OutwardQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OutwardQuantity"));
                GridView3.FooterRow.Cells[7].Text = "<b>" + OutwardQuantity + "</b>";
                decimal OutwardRate = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OutwardRate"));
                GridView3.FooterRow.Cells[8].Text = "<b>" + OutwardRate + "</b>";
                decimal OutwardValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OutwardValue"));
                GridView3.FooterRow.Cells[9].Text = "<b>" + OutwardValue + "</b>";

                //decimal ClosingQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ClosingQuantity"));
                //GridView3.FooterRow.Cells[10].Text = "<b>" + ClosingQuantity + "</b>";
                //decimal ClosingRate = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ClosingRate"));
                //GridView3.FooterRow.Cells[11].Text = "<b>" + ClosingRate + "</b>";
                //decimal ClosingValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ClosingValue"));
                //GridView3.FooterRow.Cells[12].Text = "<b>" + ClosingValue + "</b>";

                GridView3.FooterRow.TableSection = TableRowSection.TableFooter;

            }
            else
            {
                GridView3.DataSource = new string[] { };
                GridView3.DataBind();
            }
            /************/
            /************/



            //GridView2.DataSource = new string[] { };
            //GridView2.DataBind();
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    //protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int index = GridView2.SelectedRow.RowIndex;
    //    GridViewRow row = GridView2.Rows[index];
    //    LinkButton Particular = (LinkButton)row.Cells[0].FindControl("lnkbtnParticular");
    //    int item_id = int.Parse(Particular.ToolTip.ToString());
    //    string DisplayType = "Daily";
    //    string item_name = Particular.Text;
    //    if (txtFromDate.Text != "" && txtToDate.Text != "")
    //    {
    //        ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
    //        ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
    //        //string VoucherTx_Type = Voucher_Name.Text;
    //        FillGridNextLedger3(item_id, item_name, DisplayType);
    //    }
    //}

    protected void FillGridNextLedger2(int item_id, string item_name, string DisplayType)
    {
        try
        {
            //GridView1.DataSource = new string[] { };
            //GridView1.DataBind();
            string headingSecond = "";
            int yearFY = FillFromYear();

            string Office = "";
            string OfficeName = "";
            int SerialNo = 0;
            int totalListItem = ddlOffice.Items.Count;
            foreach (ListItem item in ddlOffice.Items)
            {

                if (item.Selected)
                {
                    SerialNo++;
                    Office += item.Value + ",";
                    OfficeName += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
                }
            }
            if (totalListItem == SerialNo)
            {
                OfficeName = "All Offices";
            }
            else if (SerialNo == 0)
            {
                OfficeName = "---Office Not Selected---";
            }
            else
            {
                OfficeName = OfficeName.Remove(OfficeName.Length - 1, 1);
            }

            if (DisplayType == "Daily")
            {
                headingSecond = "<p class='text-center' style='font-weight:600'>" + item_name + " (Daily Break-Up Of Item) <br /> MP State Agro Industries Development Corporation, <br/> [ " + OfficeName + " ]  <br /> " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
                lblheadingSecond.Text = headingSecond;
                ds = objdb.ByProcedure("SpFinRptRptStockSummaryItemFull", new string[] { "flag", "Office_ID_Mlt", "item_id", "FromDate", "ToDate", "FYear" }, new string[] { "2", Office, item_id.ToString(), ViewState["FromDate"].ToString(), ViewState["ToDate"].ToString(), yearFY.ToString() }, "dataset");

            }
            else if (DisplayType == "Monthly")
            {
                headingSecond = "<p class='text-center' style='font-weight:600'>" + item_name + " (Stock Item Monthly Summary) <br /> MP State Agro Industries Development Corporation, <br/> [ " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
                lblheadingSecond.Text = headingSecond;
                ds = objdb.ByProcedure("SpFinRptRptStockSummaryItemFull", new string[] { "flag", "Office_ID_Mlt", "item_id", "FromDate", "ToDate", "FYear" }, new string[] { "1", Office, item_id.ToString(), ViewState["FromDate"].ToString(), ViewState["ToDate"].ToString(), yearFY.ToString() }, "dataset");
            }
            else if (DisplayType == "Quarterly")
            {
                headingSecond = "<p class='text-center' style='font-weight:600'>" + item_name + " (Stock Item Quarterly Summary) <br /> MP State Agro Industries Development Corporation, <br/> [ " + OfficeName + " ]  <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
                lblheadingSecond.Text = headingSecond;
                ds = objdb.ByProcedure("SpFinRptRptStockSummaryItemFull", new string[] { "flag", "Office_ID_Mlt", "item_id", "FromDate", "ToDate", "FYear" }, new string[] { "3", Office, item_id.ToString(), ViewState["FromDate"].ToString(), ViewState["ToDate"].ToString(), yearFY.ToString() }, "dataset");
            }

            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                GridView3.DataSource = ds;
                GridView3.DataBind();
                GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView3.UseAccessibleHeader = true;

                //GridView3.FooterRow.Cells[0].Text = "Grand Total";
                //decimal OpeningQauntity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OpeningQuantity"));
                //GridView3.FooterRow.Cells[1].Text = "<b>" + OpeningQauntity + "</b>";
                //decimal OpeningRate = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OpeningQuantity"));
                //GridView3.FooterRow.Cells[2].Text = "<b>" + OpeningRate + "</b>";
                //decimal OpeningValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OpeningQuantity"));
                //GridView3.FooterRow.Cells[3].Text = "<b>" + OpeningValue + "</b>";

                decimal InwardQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("InwardQuantity"));
                GridView3.FooterRow.Cells[4].Text = "<b>" + InwardQuantity + "</b>";
                decimal InwardRate = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("InwardRate"));
                GridView3.FooterRow.Cells[5].Text = "<b>" + InwardRate + "</b>";
                decimal InwardValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("InwardValue"));
                GridView3.FooterRow.Cells[6].Text = "<b>" + InwardValue + "</b>";

                decimal OutwardQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OutwardQuantity"));
                GridView3.FooterRow.Cells[7].Text = "<b>" + OutwardQuantity + "</b>";
                decimal OutwardRate = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OutwardRate"));
                GridView3.FooterRow.Cells[8].Text = "<b>" + OutwardRate + "</b>";
                decimal OutwardValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OutwardValue"));
                GridView3.FooterRow.Cells[9].Text = "<b>" + OutwardValue + "</b>";

                //decimal ClosingQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ClosingQuantity"));
                //GridView3.FooterRow.Cells[10].Text = "<b>" + ClosingQuantity + "</b>";
                //decimal ClosingRate = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ClosingRate"));
                //GridView3.FooterRow.Cells[11].Text = "<b>" + ClosingRate + "</b>";
                //decimal ClosingValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ClosingValue"));
                //GridView3.FooterRow.Cells[12].Text = "<b>" + ClosingValue + "</b>";

                GridView3.FooterRow.TableSection = TableRowSection.TableFooter;

            }
            else
            {
                GridView3.DataSource = new string[] { };
                GridView3.DataBind();
            }
            /************/
            /************/



            //GridView2.DataSource = new string[] { };
            //GridView2.DataBind();
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    /**********Vouchers List************/
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = GridView3.SelectedRow.RowIndex;
        GridViewRow row = GridView3.Rows[index];
        LinkButton Particular = (LinkButton)row.Cells[0].FindControl("lnkbtnParticular");
        string FromToDate = Particular.ToolTip.ToString();
        string[] FromToDateArr = FromToDate.Split(',');

        ViewState["FromDateNew"] = Convert.ToDateTime(FromToDateArr[0], cult).ToString("yyyy/MM/dd");
        ViewState["ToDateNew"] = Convert.ToDateTime(FromToDateArr[1], cult).ToString("yyyy/MM/dd");

        int item_id = int.Parse(lblItemId.Text.ToString());
        string DisplayType = "VoucherList";
        string item_name = lblItemId.ToolTip.ToString();
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            FillGridNextLedger3(item_id, item_name, DisplayType);
        }
    }

    protected void FillGridNextLedger3(int item_id, string item_name, string DisplayType)
    {
        try
        {
            DivGrid3.Visible = false;
            DivGrid2.Visible = false;
            DivGrid4.Visible = true;
            string headingThird = "";
            int yearFY = FillFromYear();

            string Office = "";
            string OfficeName = "";
            int SerialNo = 0;
            int totalListItem = ddlOffice.Items.Count;
            foreach (ListItem item in ddlOffice.Items)
            {

                if (item.Selected)
                {
                    SerialNo++;
                    Office += item.Value + ",";
                    OfficeName += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
                }
            }
            if (totalListItem == SerialNo)
            {
                OfficeName = "All Offices";
            }
            else if (SerialNo == 0)
            {
                OfficeName = "---Office Not Selected---";
            }
            else
            {
                OfficeName = OfficeName.Remove(OfficeName.Length - 1, 1);
            }
            headingThird = "<p class='text-center' style='font-weight:600'>" + item_name + ", ( Stock Vouchers ) <br /> MP State Agro Industries Development Corporation, <br/> [ " + OfficeName + " ]  <br /> " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + " && "
                     + ViewState["FromDateNew"].ToString() + "  To " + ViewState["ToDateNew"].ToString() + " </p>";
            lblheadingThird.Text = headingThird;


            ds = objdb.ByProcedure("SpFinRptRptStockSummaryItemFull",
                new string[] { "flag", "Office_ID_Mlt", "item_id", "FromDate", "ToDate", "FYear", "FromDateN", "ToDateN", },
                new string[] { "5", Office, item_id.ToString(), ViewState["FromDate"].ToString(), ViewState["ToDate"].ToString(), yearFY.ToString(), ViewState["FromDateNew"].ToString(), ViewState["ToDateNew"].ToString() }, "dataset");


            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                GridView4.DataSource = ds;
                GridView4.DataBind();
                GridView4.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView4.UseAccessibleHeader = true;
                GridView4.FooterRow.Cells[0].Text = "<b>Grand Total</b>";
                //decimal OpeningQauntity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OpeningQuantity"));
                //GridView3.FooterRow.Cells[1].Text = "<b>" + OpeningQauntity + "</b>";
                //decimal OpeningRate = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OpeningQuantity"));
                //GridView3.FooterRow.Cells[2].Text = "<b>" + OpeningRate + "</b>";
                //decimal OpeningValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OpeningQuantity"));
                //GridView3.FooterRow.Cells[3].Text = "<b>" + OpeningValue + "</b>";

                decimal InwardQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("InwardQuantity"));
                GridView4.FooterRow.Cells[5].Text = "<b>" + InwardQuantity + "</b>";
                decimal InwardRate = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("InwardRate"));
                GridView4.FooterRow.Cells[6].Text = "<b>" + InwardRate + "</b>";
                decimal InwardValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("InwardValue"));
                GridView4.FooterRow.Cells[7].Text = "<b>" + InwardValue + "</b>";

                decimal OutwardQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OutwardQuantity"));
                GridView4.FooterRow.Cells[8].Text = "<b>" + OutwardQuantity + "</b>";
                decimal OutwardRate = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OutwardRate"));
                GridView4.FooterRow.Cells[9].Text = "<b>" + OutwardRate + "</b>";
                decimal OutwardValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OutwardValue"));
                GridView4.FooterRow.Cells[10].Text = "<b>" + OutwardValue + "</b>";

                //decimal ClosingQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ClosingQuantity"));
                //GridView3.FooterRow.Cells[10].Text = "<b>" + ClosingQuantity + "</b>";
                //decimal ClosingRate = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ClosingRate"));
                //GridView3.FooterRow.Cells[11].Text = "<b>" + ClosingRate + "</b>";
                //decimal ClosingValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ClosingValue"));
                //GridView3.FooterRow.Cells[12].Text = "<b>" + ClosingValue + "</b>";

                GridView4.FooterRow.TableSection = TableRowSection.TableFooter;

            }
            else
            {
                GridView4.DataSource = new string[] { };
                GridView4.DataBind();
            }
            /************/
            /************/



            //GridView2.DataSource = new string[] { };
            //GridView2.DataBind();
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected int FillFromYear()
    {
        try
        {
            ds = null;
            string firstDateOfYear = "";
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                firstDateOfYear = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }

            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(Convert.ToDateTime(firstDateOfYear, cult).ToString("yyyy/MM/dd")));
            int mn = datevalue.Month;
            int yy = datevalue.Year;
            int yearFY;
            if (mn < 4)
            {
                yearFY = yy - 1;

            }
            else
            {
                yearFY = yy;
            }
            return yearFY;
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            return 2019;
        }
    }
    protected void btnQuarterly_Click(object sender, EventArgs e)
    {
        ViewState["DisplayType"] = "Quarterly";
        int item_id = int.Parse(lblItemId.Text.ToString());
        string DisplayType = "Quarterly";
        string item_name = lblItemId.ToolTip;

        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            //ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            //ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            FillGridNextLedger2(item_id, item_name, DisplayType);
        }


    }
    protected void btnMonthly_Click(object sender, EventArgs e)
    {
        ViewState["DisplayType"] = "Monthly";
        int item_id = int.Parse(lblItemId.Text.ToString());
        string DisplayType = "Monthly";
        string item_name = lblItemId.ToolTip;
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            //ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            //ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            FillGridNextLedger2(item_id, item_name, DisplayType);
        }


    }
    protected void btnDaily_Click(object sender, EventArgs e)
    {
        ViewState["DisplayType"] = "Daily";
        int item_id = int.Parse(lblItemId.Text.ToString());
        string DisplayType = "Daily";
        string item_name = lblItemId.ToolTip;

        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            //ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            //ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            FillGridNextLedger2(item_id, item_name, DisplayType);
        }

    }


    protected void ddlStockGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Office = "";
        foreach (ListItem item in ddlOffice.Items)
        {

            if (item.Selected)
            {
                Office += item.Value + ",";
            }
        }

        ds = objdb.ByProcedure("SpFinRptRptStockSummaryItemFull",
                   new string[] { "flag", "Office_ID_Mlt", "ItemGroup" },
                   new string[] { "102", Office.ToString(),ddlStockGroups.SelectedValue.ToString() }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlStockItems.DataSource = ds;
            ddlStockItems.DataTextField = "ItemName";
            ddlStockItems.DataValueField = "Item_id";
            ddlStockItems.DataBind();
            //ddlStockItems.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    public void FillitemsInddl(){

        string Office = "";
        foreach (ListItem item in ddlOffice.Items)
        {

            if (item.Selected)
            {
                Office += item.Value + ",";
            }
        }

        ds = objdb.ByProcedure("SpFinRptRptStockSummaryItemFull",
                   new string[] { "flag", "Office_ID_Mlt", "ItemGroup" },
                   new string[] { "102", Office.ToString(), ddlStockGroups.SelectedValue.ToString() }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlStockItems.DataSource = ds;
            ddlStockItems.DataTextField = "ItemName";
            ddlStockItems.DataValueField = "Item_id";
            ddlStockItems.DataBind();
        }
    }
}