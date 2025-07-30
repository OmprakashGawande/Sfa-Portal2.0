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


public partial class mis_Finance_RptMis : System.Web.UI.Page
{
    DataSet ds;
    static DataSet ds1;
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
                    ViewState["OfficeType_Title"] = Session["OfficeType_Title"].ToString();
                    ViewState["Division_ID"] = Session["Division_ID"].ToString();
                    ddlOffice.Enabled = false;
                    divExcel.Visible = false;
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    FillFromDate();
                    FillDropdown();
                    SelectDropdown();
                    
                    ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
                    FillVoucherDate();
                    GetCommonSearch();
                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        ddlOffice.Enabled = false;
                    }
                    //fill_details();
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
    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    Response.Clear();
    //    Response.AddHeader("content-disposition", "attachment;filename=FileName.xls");
    //    Response.Charset = "";
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    Response.ContentType = "application/vnd.xls";
    //    System.IO.StringWriter stringWrite = new System.IO.StringWriter();
    //    System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
    //    DivGrid2.RenderControl(htmlWrite);
    //    Response.Write(stringWrite.ToString());
    //    Response.End();
    //}

    ////don’t forget to add this method , other wise we will get Control gv of type 
    ////'GridView' must be placed inside a form tag with runat=server. 

    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //} 
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
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtToDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }
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
            //if (ViewState["Office_ID"].ToString() == "1")
            //{
            //    ddlOffice.Enabled = true;
            //}
            //ds = objdb.ByProcedure("SpAdminOffice",
            //       new string[] { "flag" },
            //       new string[] { "10" }, "dataset");
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlOffice.DataSource = ds;
            //    ddlOffice.DataTextField = "Office_Name";
            //    ddlOffice.DataValueField = "Office_ID";
            //    ddlOffice.DataBind();
            //    //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
            //    ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            //}
            if (ViewState["OfficeType_Title"].ToString() != "District Office" && ViewState["OfficeType_Title"].ToString() != "Production Unit")
            {
                ddlOffice.Enabled = true;
                ddlRegionalOffice.Enabled = true;
               
                //ds = objdb.ByProcedure("SpAdminOffice",
                // new string[] { "flag" },
                // new string[] { "21" }, "dataset");
                ds = objdb.ByProcedure("SpAdminOffice",
                new string[] { "flag" },
                new string[] { "25" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlRegionalOffice.DataSource = ds;
                    ddlRegionalOffice.DataTextField = "Office_Name";
                    ddlRegionalOffice.DataValueField = "Office_ID";
                    ddlRegionalOffice.DataBind();
                    foreach (ListItem item in ddlRegionalOffice.Items)
                    {
                        item.Selected = true;
                    }
                    //ddlRegionalOffice.Items.Insert(0, new ListItem("All", "0"));
                    //ddlRegionalOffice.Items.Insert(ds.Tables[0].Rows.Count + 1, new ListItem("Production Unit", "010"));
                    if (ViewState["OfficeType_Title"].ToString() == "Regional Office")
                    {
                        ddlRegionalOffice.Enabled = false;
                        ddlRegionalOffice.SelectedValue = ViewState["Division_ID"].ToString();
                    }
                }
            }
            FillOffice();
            /*******Groups***********/
            ds = objdb.ByProcedure("SpFinRptRptStockSummaryItem",
                   new string[] { "flag" },
                   new string[] { "7" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlGroup.DataSource = ds;
                ddlGroup.DataTextField = "ItemTypeName";
                ddlGroup.DataValueField = "ItemType_id";
                ddlGroup.DataBind();

                foreach (ListItem item in ddlGroup.Items)
                {
                    item.Selected = true;
                }
                //ddlGroup.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    protected void FillGrid()
    {
        try
        {
            if (txtFromDate.Text != "" && txtToDate.Text != "")
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

                string headingFirst = "<p class='text-center' style='font-weight:600'>MIS Report <br /> MP State Agro Industries Development Corporation, <br/> [ " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";


                lblprinttext.ToolTip = "<p class='text-center' style='font-weight:600'>MIS Report <br /> MP State Agro Industries Development Corporation, <br/> [ " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";

                lblexceltext.ToolTip = "<p class='text-center' style='font-weight:600'>MIS Report <br /> MP State Agro Industries Development Corporation, <br/> [ " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";


                lblheadingFirst.Text = headingFirst;
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
                GridView1.Visible = true;
                GridView2.Visible = false;
                GridView3.Visible = false;

                /*****************SET Search Option Start********************/
                #region SetSearchOption
                Session["CommonOffice"] = Office;
                Session["CommonFromDate"] = txtFromDate.Text;
                Session["CommonToDate"] = txtToDate.Text;
                #endregion
                /*****************SET Search Option End********************/

                ds = objdb.ByProcedure("SpFinRptMis", new string[] { "flag", "Office_ID_Mlt", "Group_ID_Mlt", "FromDate", "ToDate" }, new string[] { "0", Office, itemgroup, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ds1 = ds;
                    if (ddlReportType.SelectedValue == "Group Wise")
                    {
                        divExcel.Visible = true;
                        GridView2.Visible = false;
                        GridView1.Visible = true;
                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();

                        decimal TotalPurchaseValueWithoutTax = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PurchaseValueWithoutTax"));
                        decimal TotalPurchaseTaxAmount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PurchaseTaxAmount"));
                        decimal TotalPurchaseValuewithTax = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PurchaseValuewithTax"));
                        decimal TotalSaleValueWithoutTax = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SaleValueWithoutTax"));
                        decimal TotalSaleTaxAmount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SaleTaxAmount"));
                        decimal TotalSaleValuewithTax = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SaleValuewithTax"));
                        GridView1.FooterRow.Cells[2].Text = "<b>Total : </b>";
                        GridView1.FooterRow.Cells[3].Text = "<b>" + TotalPurchaseValueWithoutTax.ToString() + "</b>";
                        GridView1.FooterRow.Cells[4].Text = "<b>" + TotalPurchaseTaxAmount.ToString() + "</b>";
                        GridView1.FooterRow.Cells[5].Text = "<b>" + TotalPurchaseValuewithTax.ToString() + "</b>";
                        GridView1.FooterRow.Cells[6].Text = "<b>Total : </b>";
                        GridView1.FooterRow.Cells[7].Text = "<b>" + TotalSaleValueWithoutTax.ToString() + "</b>";
                        GridView1.FooterRow.Cells[8].Text = "<b>" + TotalSaleTaxAmount.ToString() + "</b>";
                        GridView1.FooterRow.Cells[9].Text = "<b>" + TotalSaleValuewithTax.ToString() + "</b>";
                        GridView1.UseAccessibleHeader = true;
                        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        GridView1.Visible = false;
                        GridView2.Visible = true;
                        divExcel.Visible = true;
                        GridView2.DataSource = ds.Tables[1];
                        GridView2.DataBind();
                        decimal TotalPurchaseValueWithoutTax = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("PurchaseValueWithoutTax"));
                        decimal TotalPurchaseTaxAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("PurchaseTaxAmount"));
                        decimal TotalPurchaseValuewithTax = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("PurchaseValuewithTax"));
                        decimal TotalSaleValueWithoutTax = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SaleValueWithoutTax"));
                        decimal TotalSaleTaxAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SaleTaxAmount"));
                        decimal TotalSaleValuewithTax = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SaleValuewithTax"));
                        GridView2.FooterRow.Cells[3].Text = "<b>Total : </b>";
                        GridView2.FooterRow.Cells[4].Text = "<b>" + TotalPurchaseValueWithoutTax.ToString() + "</b>";
                        GridView2.FooterRow.Cells[5].Text = "<b>" + TotalPurchaseTaxAmount.ToString() + "</b>";
                        GridView2.FooterRow.Cells[6].Text = "<b>" + TotalPurchaseValuewithTax.ToString() + "</b>";
                        GridView2.FooterRow.Cells[7].Text = "<b>Total : </b>";
                        GridView2.FooterRow.Cells[8].Text = "<b>" + TotalSaleValueWithoutTax.ToString() + "</b>";
                        GridView2.FooterRow.Cells[9].Text = "<b>" + TotalSaleTaxAmount.ToString() + "</b>";
                        GridView2.FooterRow.Cells[10].Text = "<b>" + TotalSaleValuewithTax.ToString() + "</b>";
                        GridView2.UseAccessibleHeader = true;
                        GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    //GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    //GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                }
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridView3.DataSource = new string[] { };
            GridView3.DataBind();
            if(e.CommandName == "View")
            {
                string Item_id = e.CommandArgument.ToString();
                string Office = "";               
                int SerialNo = 0;
                int totalListItem = ddlOffice.Items.Count;
                foreach (ListItem item in ddlOffice.Items)
                {

                    if (item.Selected)
                    {
                        SerialNo++;
                        Office += item.Value + ",";
                        
                    }
                }
                ds = objdb.ByProcedure("SpFinRptMis", new string[] { "flag", "Item_id", "Office_ID_Mlt", "FromDate", "ToDate" }, new string[] { "1", Item_id, Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if(ds!= null && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.Visible = false;
                    GridView2.Visible = false;
                    GridView3.Visible = true;
                    divExcel.Visible = true;
                    GridView3.DataSource = ds.Tables[0];
                    GridView3.DataBind();
                    GridView3.UseAccessibleHeader = true;
                    GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "View")
            {
                string VoucherTx_ID = e.CommandArgument.ToString();
                DataSet dsPageURL = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "VoucherTx_ID" },
                    new string[] { "30", VoucherTx_ID },
                    "dataset");

                if (dsPageURL != null)
                {

                    string Url = dsPageURL.Tables[0].Rows[0]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                    Url = Url + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(dsPageURL.Tables[1].Rows[0]["Office_ID"].ToString());

                    Response.Redirect(Url);

                }

            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "View")
            {
                GridView1.Visible = false;
                GridView2.Visible = true;
                GridView3.Visible = false;
                string ItemType_id = e.CommandArgument.ToString();
                DataView dv = new DataView();
                dv = ds1.Tables[1].DefaultView;
                dv.RowFilter = "ItemType_id = '" + ItemType_id + "'";
                DataTable dt = dv.ToTable();
                GridView2.DataSource = dt;
                GridView2.DataBind();
                decimal TotalPurchaseValueWithoutTax = dt.AsEnumerable().Sum(row => row.Field<decimal>("PurchaseValueWithoutTax"));
                decimal TotalPurchaseTaxAmount = dt.AsEnumerable().Sum(row => row.Field<decimal>("PurchaseTaxAmount"));
                decimal TotalPurchaseValuewithTax = dt.AsEnumerable().Sum(row => row.Field<decimal>("PurchaseValuewithTax"));
                decimal TotalSaleValueWithoutTax = dt.AsEnumerable().Sum(row => row.Field<decimal>("SaleValueWithoutTax"));
                decimal TotalSaleTaxAmount = dt.AsEnumerable().Sum(row => row.Field<decimal>("SaleTaxAmount"));
                decimal TotalSaleValuewithTax = dt.AsEnumerable().Sum(row => row.Field<decimal>("SaleValuewithTax"));
                GridView2.FooterRow.Cells[3].Text = "<b>Total : </b>";
                GridView2.FooterRow.Cells[4].Text = "<b>" + TotalPurchaseValueWithoutTax.ToString() + "</b>";
                GridView2.FooterRow.Cells[5].Text = "<b>" + TotalPurchaseTaxAmount.ToString() + "</b>";
                GridView2.FooterRow.Cells[6].Text = "<b>" + TotalPurchaseValuewithTax.ToString() + "</b>";
                GridView2.FooterRow.Cells[7].Text = "<b>Total : </b>";
                GridView2.FooterRow.Cells[8].Text = "<b>" + TotalSaleValueWithoutTax.ToString() + "</b>";
                GridView2.FooterRow.Cells[9].Text = "<b>" + TotalSaleTaxAmount.ToString() + "</b>";
                GridView2.FooterRow.Cells[10].Text = "<b>" + TotalSaleValuewithTax.ToString() + "</b>";
                GridView2.UseAccessibleHeader = true;
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            if (Session["CommonOffice"] != null)
            {
                string Office = Session["CommonOffice"].ToString();
                string[] OfficeList = Office.Split(new Char[] { ',' });
                if (OfficeList.Count() > 0)
                {
                    ddlOffice.ClearSelection();
                    foreach (string OfficeID in OfficeList)
                    {
                        // ddlOffice.SelectedValue = OfficeID.ToString();
                        foreach (ListItem item in ddlOffice.Items)
                        {
                            if (item.Value == OfficeID)
                                item.Selected = true;
                        }
                    }
                }

            }
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
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void ddlRegionalOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillOffice();
    }
    protected void FillOffice()
    {
        try
        {
            
            ddlOffice.Items.Clear();
            string Office_ID_mlt = "";

            foreach (ListItem item in ddlRegionalOffice.Items)
            {
                if (item.Selected)
                {
                    Office_ID_mlt += item.Value + ",";
                }
            }
            //if (ddlRegionalOffice.SelectedIndex > 0)
            //{
            //    if (ddlRegionalOffice.SelectedValue.ToString() != "010" && ddlRegionalOffice.SelectedItem.ToString() != "Production Unit")
            //    {
            //        //ds = objdb.ByProcedure("SpAdminOffice",
            //        //       new string[] { "flag", "Office_ID" },
            //        //       new string[] { "23", ddlRegionalOffice.SelectedValue.ToString() }, "dataset");
            //        ds = objdb.ByProcedure("SpAdminOffice",
            //              new string[] { "flag", "Office_ID_mlt" },
            //              new string[] { "26", Office_ID_mlt }, "dataset");
            //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //        {
            //            ddlOffice.DataSource = ds;
            //            ddlOffice.DataTextField = "Office_Name";
            //            ddlOffice.DataValueField = "Office_ID";
            //            ddlOffice.DataBind();
            //            //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
            //            // ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            //        }
            //    }
            //    else
            //    {
            //        ds = objdb.ByProcedure("SpAdminOffice",
            //               new string[] { "flag" },
            //               new string[] { "24" }, "dataset");
            //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //        {
            //            ddlOffice.DataSource = ds;
            //            ddlOffice.DataTextField = "Office_Name";
            //            ddlOffice.DataValueField = "Office_ID";
            //            ddlOffice.DataBind();
            //            //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
            //            // ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            //        }

            //    }
            //}
            //else
            //{
            //    ds = objdb.ByProcedure("SpAdminOffice",
            //            new string[] { "flag" },
            //            new string[] { "22" }, "dataset");
            //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //    {
            //        ddlOffice.DataSource = ds;
            //        ddlOffice.DataTextField = "Office_Name";
            //        ddlOffice.DataValueField = "Office_ID";
            //        ddlOffice.DataBind();
            //        //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
            //        ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            //    }
            //}

            ds = objdb.ByProcedure("SpAdminOffice",
                         new string[] { "flag", "Office_ID_mlt" },
                         new string[] { "26", Office_ID_mlt }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                // ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
           

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}