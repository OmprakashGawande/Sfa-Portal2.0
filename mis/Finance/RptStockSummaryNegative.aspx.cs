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

public partial class mis_Finance_RptStockSummaryNegative : System.Web.UI.Page
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
                    ViewState["ReportLevelName"] = "";
                    ddlOffice.Enabled = false;
                    //txtFromDate.Attributes.Add("readonly", "readonly");
                    //txtFromDate.Enabled = false;
                    FillFromDate();
                    FillDropdown();
                    SelectDropdown();
                    GetCommonSearch();
                    ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
                    ViewState["ToDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");

                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        ddlOffice.Enabled = false;
                    }
                    //fill_details();
                    //
                    OnLoadFillGrid();
                    DivGrid3.Visible = false;
                    DivGrid4.Visible = false;
                    DivGrid2.Visible = false;
                    //
                }
                lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt");
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
                //txtToDate.Text = "01/04/" + (yy - 1).ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();

            }
            else
            {
                txtFromDate.Text = "01/04/" + (yy).ToString();
                //txtToDate.Text = "01/04/" + (yy).ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void OnLoadFillGrid()
    {
        DivGrid3.Visible = false;
        DivGrid4.Visible = false;
        DivGrid2.Visible = false;
        GridView3.DataSource = new string[] { };
        GridView3.DataBind();
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");            
                fill_details2();

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
            /*******Groups***********/
            ds = objdb.ByProcedure("SpFinRptRptStockSummary",
                   new string[] { "flag" },
                   new string[] { "7" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlGroup.DataSource = ds;
                ddlGroup.DataTextField = "ItemTypeName";
                ddlGroup.DataValueField = "ItemType_id";
                ddlGroup.DataBind();
                //ddlGroup.Items.Insert(0, new ListItem("All", "0"));

                foreach (ListItem item in ddlGroup.Items)
                {
                    item.Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        DivGrid3.Visible = false;
        DivGrid4.Visible = false;
        DivGrid2.Visible = false;
        GridView3.DataSource = new string[] { };
        GridView3.DataBind();
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            /*****************SET Search Option Start********************/
            #region SetSearchOption
            Session["CommonFromDate"] = txtFromDate.Text;
            Session["CommonToDate"] = txtToDate.Text;
            #endregion
            /*****************SET Search Option End********************/

            ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");


            if (ViewState["ReportLevelName"].ToString() == "LastVoucherWise")
            {
                ViewState["FromDateNew"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
                ViewState["ToDateNew"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
                ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
                ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");

                FillGridNextLedger3(Convert.ToInt32(ViewState["ReportLevelitem_id"]), ViewState["ReportLevelitem_name"].ToString(), ViewState["ReportLevelDisplayType"].ToString());
            }
            else
            {
                fill_details2();
            }

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

            SerialNo = 0;
            string itemgroup = "";
            string itemgroupname = "";
            foreach (ListItem item in ddlGroup.Items)
            {

                if (item.Selected)
                {
                    SerialNo++;
                    itemgroup += item.Value + ",";
                    itemgroupname += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
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
            string headingSecond = "<p class='text-center' style='font-weight:600'>" + item_name + " <br /> M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd,  <br/> [ " + OfficeName + " ]  <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
            lblheadingSecond.Text = headingSecond;

            lblprinttext.ToolTip = "<p class='text-center' style='font-size:18px'>" + item_name + "<br /> M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd, <br/> [Offices:  " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";

            lblexceltext.ToolTip = "" + item_name + "-M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd-" + OfficeName + "-" + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + " To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy");


            lblItemId.Text = item_id.ToString();
            lblItemId.ToolTip = item_name.ToString();
            /********************************/
            int yearFY = GetFinancialYearByDate();
            /********************************/



            ds = objdb.ByProcedure("SpFinRptRptStockSummary", new string[] { "flag", "Office_ID_Mlt", "item_id", "FromDate", "ToDate", "FYear", "Group_ID_Mlt" }, new string[] { "1", Office, item_id.ToString(), ViewState["FromDate"].ToString(), ViewState["ToDate"].ToString(), yearFY.ToString(), itemgroupname }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                GridView3.DataSource = ds;
                GridView3.DataBind();
                GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView3.UseAccessibleHeader = true;

                GridView3.FooterRow.Cells[0].Text = "Grand Total";

                GridView3.FooterRow.Cells[6].Text = "<b>" + ds.Tables[1].Rows[0]["InwardValue"].ToString() + "</b>";
                GridView3.FooterRow.Cells[9].Text = "<b>" + ds.Tables[1].Rows[0]["OutwardValue"].ToString() + "</b>";

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

            SerialNo = 0;
            string itemgroup = "";
            string itemgroupname = "";
            foreach (ListItem item in ddlGroup.Items)
            {

                if (item.Selected)
                {
                    SerialNo++;
                    itemgroup += item.Value + ",";
                    itemgroupname += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
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
                headingSecond = "<p class='text-center' style='font-weight:600'>" + item_name + " (Daily Break-Up Of Item) <br /> M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd, <br/> [ " + OfficeName + " ]  <br /> " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
                lblheadingSecond.Text = headingSecond;
                ds = objdb.ByProcedure("SpFinRptRptStockSummary", new string[] { "flag", "Office_ID_Mlt", "item_id", "FromDate", "ToDate", "FYear", "Group_ID_Mlt" }, new string[] { "2", Office, item_id.ToString(), ViewState["FromDate"].ToString(), ViewState["ToDate"].ToString(), yearFY.ToString(), itemgroupname }, "dataset");

            }
            else if (DisplayType == "Monthly")
            {
                headingSecond = "<p class='text-center' style='font-weight:600'>" + item_name + " (Stock Item Monthly Summary) <br /> M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd, <br/> [ " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
                lblheadingSecond.Text = headingSecond;
                ds = objdb.ByProcedure("SpFinRptRptStockSummary", new string[] { "flag", "Office_ID_Mlt", "item_id", "FromDate", "ToDate", "FYear", "Group_ID_Mlt" }, new string[] { "1", Office, item_id.ToString(), ViewState["FromDate"].ToString(), ViewState["ToDate"].ToString(), yearFY.ToString(), itemgroupname }, "dataset");
            }
            else if (DisplayType == "Quarterly")
            {
                headingSecond = "<p class='text-center' style='font-weight:600'>" + item_name + " (Stock Item Quarterly Summary) <br /> M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd, <br/> [ " + OfficeName + " ]  <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
                lblheadingSecond.Text = headingSecond;
                ds = objdb.ByProcedure("SpFinRptRptStockSummary", new string[] { "flag", "Office_ID_Mlt", "item_id", "FromDate", "ToDate", "FYear", "Group_ID_Mlt" }, new string[] { "3", Office, item_id.ToString(), ViewState["FromDate"].ToString(), ViewState["ToDate"].ToString(), yearFY.ToString(), itemgroupname }, "dataset");
            }

            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                GridView3.DataSource = ds;
                GridView3.DataBind();
                GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView3.UseAccessibleHeader = true;

                GridView3.FooterRow.Cells[0].Text = "Grand Total";
                GridView3.FooterRow.Cells[4].Text = "<b>" + ds.Tables[1].Rows[0]["InwardValue"].ToString() + "</b>";
                GridView3.FooterRow.Cells[6].Text = "<b>" + ds.Tables[1].Rows[0]["InwardValue"].ToString() + "</b>";
                GridView3.FooterRow.Cells[7].Text = "<b>" + ds.Tables[1].Rows[0]["OutwardValue"].ToString() + "</b>";
                GridView3.FooterRow.Cells[9].Text = "<b>" + ds.Tables[1].Rows[0]["OutwardValue"].ToString() + "</b>";

                GridView3.FooterRow.TableSection = TableRowSection.TableFooter;


            }
            else
            {
                GridView3.DataSource = new string[] { };
                GridView3.DataBind();
            }
            /************/
            /************/
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


        ViewState["ReportLevelName"] = "LastVoucherWise";
        ViewState["ReportLevelitem_id"] = item_id;
        ViewState["ReportLevelitem_name"] = item_name;
        ViewState["ReportLevelDisplayType"] = DisplayType;


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
            SerialNo = 0;
            string itemgroup = "";
            string itemgroupname = "";
            foreach (ListItem item in ddlGroup.Items)
            {

                if (item.Selected)
                {
                    SerialNo++;
                    itemgroup += item.Value + ",";
                    itemgroupname += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
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
            headingThird = "<p class='text-center' style='font-weight:600'>" + item_name + ", ( Stock Vouchers ) <br /> M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd, <br/> [ " + OfficeName + " ]  <br /> " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + " && "
                     + Convert.ToDateTime(ViewState["FromDateNew"], cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(ViewState["ToDateNew"], cult).ToString("dd-MM-yyyy") + " </p>";
            lblheadingThird.Text = headingThird;

            lblprinttext.ToolTip = "<p class='text-center' style='font-size:18px'>" + item_name + "<br /> M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd, <br/> [Offices:  " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + " && "
                     + Convert.ToDateTime(ViewState["FromDateNew"], cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(ViewState["ToDateNew"], cult).ToString("dd-MM-yyyy") + " </p>";

            lblexceltext.ToolTip = "" + item_name + "-M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd-" + OfficeName + "-" + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + " To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + " AND "
                     + Convert.ToDateTime(ViewState["FromDateNew"], cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(ViewState["ToDateNew"], cult).ToString("dd-MM-yyyy");


            ds = objdb.ByProcedure("SpFinRptRptStockSummary",
                new string[] { "flag", "Office_ID_Mlt", "item_id", "FromDate", "ToDate", "FYear", "FromDateN", "ToDateN", "Group_ID_Mlt" },
                new string[] { "5", Office, item_id.ToString(), ViewState["FromDate"].ToString(), ViewState["ToDate"].ToString(), yearFY.ToString(), ViewState["FromDateNew"].ToString(), ViewState["ToDateNew"].ToString(), itemgroupname }, "dataset");



            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                GridView4.DataSource = ds;
                GridView4.DataBind();
                GridView4.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView4.UseAccessibleHeader = true;
                /**************************/
               

                /**************************/
                GridView4.FooterRow.Cells[0].Text = "<b>Grand Total</b>";
                GridView4.FooterRow.Cells[5].Text = "<b>" + ds.Tables[1].Rows[0]["InwardQuantity"].ToString() + "</b>";
                GridView4.FooterRow.Cells[7].Text = "<b>" + ds.Tables[1].Rows[0]["InwardValue"].ToString() + "</b>";
                GridView4.FooterRow.Cells[8].Text = "<b>" + ds.Tables[1].Rows[0]["OutwardQuantity"].ToString() + "</b>";
                GridView4.FooterRow.Cells[10].Text = "<b>" + ds.Tables[1].Rows[0]["OutwardValue"].ToString() + "</b>";
                GridView4.FooterRow.TableSection = TableRowSection.TableFooter;

                /******************************/               

                /******************************/

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
    protected int GetFinancialYearByDate()
    {
        String sDate = DateTime.Now.ToString();
        DateTime datevalue = (Convert.ToDateTime(Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd")));
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
            FillGridNextLedger2(item_id, item_name, DisplayType);
        }

    }

    private void fill_details2()
    {

        try
        {
            DivGrid3.Visible = false;
            DivGrid4.Visible = false;
            DivGrid2.Visible = true;

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

            SerialNo = 0;
            string itemgroup = "";
            string itemgroupname = "";
            foreach (ListItem item in ddlGroup.Items)
            {

                if (item.Selected)
                {
                    SerialNo++;
                    itemgroup += item.Value + ",";
                    itemgroupname += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
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

            string headingFirst = "<p class='text-center' style='font-weight:600'>Stock Summary <br /> M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd, <br/> [ " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
            lblheadingFirst.Text = headingFirst;
            //GridView1.DataSource = null;
            GridView2.DataSource = null;

            ds = objdb.ByProcedure("SpFinRptRptStockSummary",
                   new string[] { "flag", "Office_ID_Mlt", "ToDate", "FromDate", "Group_ID_Mlt","ReportType" },
                   new string[] { "21", Office, ViewState["ToDate"].ToString(), ViewState["FromDate"].ToString(), itemgroup, ddlReportType.SelectedValue.ToString() }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                GridView2.DataSource = ds.Tables[0];
                GridView2.DataBind();
                GridView2.UseAccessibleHeader = true;
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView2.FooterRow.Cells[0].Text = "Grand Total";
                GridView2.FooterRow.Cells[1].Text = "<b>" + ds.Tables[1].Rows[0]["OpeningQuantity"].ToString() + "</b>";
                GridView2.FooterRow.Cells[2].Text = "<b>" + ds.Tables[1].Rows[0]["InwardQuantity"].ToString() + "</b>";
                GridView2.FooterRow.Cells[3].Text = "<b>" + ds.Tables[1].Rows[0]["OutwardQuantity"].ToString() + "</b>";
                GridView2.FooterRow.Cells[4].Text = "<b>" + ds.Tables[1].Rows[0]["ClosingQuantity"].ToString() + "</b>";
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
    protected void GetCommonSearch()
    {
        try
        {
           
            if (Session["CommonFromDate"] != null)
            {
                string FromDate = Session["CommonFromDate"].ToString();
                txtFromDate.Text = FromDate.ToString();
            }
            if (Session["CommonToDate"] != null)
            {
                string ToDate = Session["CommonToDate"].ToString();
                txtToDate.Text = ToDate.ToString();
            }
        }
        catch (Exception ex)
        {
           // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
}