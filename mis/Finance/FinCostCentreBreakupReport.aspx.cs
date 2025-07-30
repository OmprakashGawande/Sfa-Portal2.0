using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_FinCostCentreBreakupReport : System.Web.UI.Page
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
                    ViewState["OfficeType_Title"] = Session["OfficeType_Title"].ToString();
                    ViewState["Division_ID"] = Session["Division_ID"].ToString();
                    //ViewState["Office_FinAddress"] = Session["Office_FinAddress"].ToString();
                    ddlOffice.Enabled = false;
                    divExcel.Visible = false;

                    ViewState["GridCount"] = "0";
                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    FillDropdown();
                    FillSubCategory();
                    spnAltB.Visible = false;
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDropdown()
    {
        try
        {

            ddlOffice.Enabled = false;
            ddlRegionalOffice.Enabled = false;
            divRegionalOffice.Visible = false;
            if (ViewState["OfficeType_Title"].ToString() != "Division Office (DFO)" && ViewState["OfficeType_Title"].ToString() != "Production Unit")
            {
                ddlOffice.Enabled = true;
                ddlRegionalOffice.Enabled = true;
                divRegionalOffice.Visible = true;
                ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "21" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlRegionalOffice.DataSource = ds;
                    ddlRegionalOffice.DataTextField = "Office_Name";
                    ddlRegionalOffice.DataValueField = "Office_ID";
                    ddlRegionalOffice.DataBind();
                    ddlRegionalOffice.Items.Insert(0, new ListItem("All", "0"));
                  //  ddlRegionalOffice.Items.Insert(ds.Tables[0].Rows.Count + 1, new ListItem("Production Unit", "010"));
                    if (ViewState["OfficeType_Title"].ToString() == "Circle Office (CCF)")
                    {
                        ddlRegionalOffice.Enabled = false;
                        ddlRegionalOffice.SelectedValue = ViewState["Division_ID"].ToString();
                    }
                }
            }

            FillOffice();



            //if (ViewState["Office_ID"].ToString() == "1")
            //{
            //    ddlOffice.Enabled = true;
            //}

            //ds = objdb.ByProcedure("SpFinRptTrialBalanceNewFF",
            //       new string[] { "flag" },
            //       new string[] { "0" }, "dataset");
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlOffice.DataSource = ds;
            //    ddlOffice.DataTextField = "Office_Name";
            //    ddlOffice.DataValueField = "Office_ID";
            //    ddlOffice.DataBind();
            //    //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
            //    ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillOffice()
    {
        try
        {

            //ddlOffice.Enabled = false;
            //ddlRegionalOffice.Enabled = false;

            ddlOffice.Items.Clear();
            if (ddlRegionalOffice.SelectedIndex > 0)
            {
                if (ddlRegionalOffice.SelectedValue.ToString() != "010" && ddlRegionalOffice.SelectedItem.ToString() != "Production Unit")
                {
                    ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag", "Office_ID" },
                           new string[] { "23", ddlRegionalOffice.SelectedValue.ToString() }, "dataset");
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
                else
                {
                    ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag" },
                           new string[] { "24" }, "dataset");
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
            }
            else
            {
                ds = objdb.ByProcedure("SpAdminOffice",
                        new string[] { "flag" },
                        new string[] { "22" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOffice.DataSource = ds;
                    ddlOffice.DataTextField = "Office_Name";
                    ddlOffice.DataValueField = "Office_ID";
                    ddlOffice.DataBind();
                    //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                    ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    //protected void FillDropdown()
    //{
    //    try
    //    {
    //        if (ViewState["Office_ID"].ToString() == "1")
    //        {
    //            ddlOffice.Enabled = true;
    //        }
    //        ds = objdb.ByProcedure("SpFinVoucherTx",
    //               new string[] { "flag" },
    //               new string[] { "26" }, "dataset");
    //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlOffice.DataSource = ds;
    //            ddlOffice.DataTextField = "Office_Name";
    //            ddlOffice.DataValueField = "Office_ID";
    //            ddlOffice.DataBind();
    //            ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

    //    }
    //}
    protected void FillSubCategory()
    {
        try
        {
            string Office = "";

            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            ddlSubCategory.Items.Clear();
            //ds = objdb.ByProcedure("Sp_Fin_CostCentreReport",
            //       new string[] { "flag", "OfficeID" },
            //       new string[] { "6",ddlOffice.SelectedValue }, "dataset");
            ds = objdb.ByProcedure("Sp_Fin_CostCentreReport",
                  new string[] { "flag", "OfficeID" },
                  new string[] { "7", Office }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlSubCategory.DataSource = ds;
                ddlSubCategory.DataTextField = "SubCategoryName";
                ddlSubCategory.DataValueField = "SubCategoryId";
                ddlSubCategory.DataBind();
                
            }
            ddlSubCategory.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void ShowColumn()
    {
        try
        {
            // Head Name
            GridView1.Columns[2].Visible = true;
            GridView1.Columns[3].Visible = true;
            GridView1.Columns[4].Visible = true;
            GridView1.Columns[5].Visible = true;
            GridView1.Columns[6].Visible = true;
            GridView1.Columns[7].Visible = true;

            // Ledger Name       
            GridView2.Columns[2].Visible = true;
            GridView2.Columns[3].Visible = true;
            GridView2.Columns[4].Visible = true;
            GridView2.Columns[5].Visible = true;
            GridView2.Columns[6].Visible = true;
            GridView2.Columns[7].Visible = true;


            //GridView3.Columns[4].Visible = true;


        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void ShowHideColumn()
    {
        try
        {
            // Head Name
            if (chkOpeningBal.Checked == false)
            {
                GridView1.Columns[2].Visible = false;
                GridView1.Columns[3].Visible = false;

                GridView2.Columns[2].Visible = false;
                GridView2.Columns[3].Visible = false;
            }
            if (chkTransactionAmt.Checked == false)
            {

                GridView1.Columns[4].Visible = false;
                GridView1.Columns[5].Visible = false;

                GridView2.Columns[4].Visible = false;
                GridView2.Columns[5].Visible = false;
            }
            if (chkClosingBal.Checked == false)
            {
                GridView1.Columns[6].Visible = false;
                GridView1.Columns[7].Visible = false;

                GridView2.Columns[6].Visible = false;
                GridView2.Columns[7].Visible = false;

                //GridView3.Columns[4].Visible = false;
            }
            if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
            {
                GridView1.Columns[3].Visible = false;
                GridView1.Columns[7].Visible = false;

                GridView2.Columns[3].Visible = false;
                GridView2.Columns[7].Visible = false;
            }
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
            lblMsg.Text = "";
            lblHeadingSecond.Text = "";
            lblHeadName.Text = "";
            lblOpening.Text = "";
            lblMonth.Text = "";
            DivTable.InnerHtml = "";


            decimal? DebitAmt = 0;
            decimal? CreditAmt = 0;
            GridView1.Visible = true;
            GridView2.Visible = false;
            GridView3.Visible = false;
            GridView4.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            //GridView2.DataSource = new string[] { };
            //GridView3.DataSource = new string[] { };
            //GridView4.DataSource = null;
            //GridView4.DataBind();

            ds = null;

            string Office = "";

            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            ShowColumn();

            //string headingFirst = "<p class='text-center' style='font-weight:600'>" + ViewState["Office_FinAddress"].ToString() + " <br />  Cost Centre BreakUp Report<br />" + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</br>Sub Category : " + ddlSubCategory.SelectedItem.Text + "</p>";
            string headingFirst = "<p class='text-center' style='font-weight:600'>Cost Centre BreakUp Report<br />" + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</br>Sub Category : " + ddlSubCategory.SelectedItem.Text + "</p>";
            lblheadingFirst.Text = headingFirst;

            ds = objdb.ByProcedure("Sp_Fin_CostCentreReport", new string[] { "flag", "OfficeID", "FromDate", "ToDate", "SubCategoryId" }, new string[] { "5", Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"),ddlSubCategory.SelectedValue }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {


                GridView1.DataSource = ds;
                GridView1.DataBind();
                divExcel.Visible = true;
                ViewState["GridCount"] = Convert.ToInt32(ViewState["GridCount"]) + 1;
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                decimal TxnDebitAmt = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TxnDebitAmt"));
                decimal TxnCreditAmt = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TxnCreditAmt"));

                GridView1.FooterRow.Cells[3].Text = "<b>" + TxnDebitAmt.ToString() + "</b>";
                GridView1.FooterRow.Cells[4].Text = "<b>" + TxnCreditAmt.ToString() + "</b>";
                if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                {
                    GridView1.HeaderRow.Cells[2].Text = "Opening Bal.";

                    GridView1.HeaderRow.Cells[6].Text = "Closing Bal.";
                }


                decimal TotalOpeningBalCr = 0;
                decimal TotalOpeningBalDr = 0;
                decimal TotalClosingBalCr = 0;
                decimal TotalClosingBalDr = 0;

                decimal TotalDebitAmt = TxnDebitAmt;
                decimal TotalCreditAmt = TxnCreditAmt;


                decimal OpenTotalDr = 0;
                decimal OpenTotalCr = 0;
                decimal CloseTotalDr = 0;
                decimal CloseTotalCr = 0;


                int rowcount = 0;//ds.Tables[0].Rows.Count - 1;
                rowcount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < rowcount; i++)
                {
                    decimal OpenDebitAmt = 0;
                    decimal OpenCreditAmt = 0;
                    decimal OpeningBalance = 0;

                    decimal ClosingDrBlnc = 0;
                    decimal ClosingCrBlnc = 0;
                    decimal ClosingBalance = 0;


                    OpenDebitAmt = -decimal.Parse(ds.Tables[0].Rows[i]["OpenDebitAmt"].ToString());

                    OpenCreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["OpenCreditAmt"].ToString());

                    ClosingDrBlnc = -decimal.Parse(ds.Tables[0].Rows[i]["ClosingDrBlnc"].ToString());

                    ClosingCrBlnc = decimal.Parse(ds.Tables[0].Rows[i]["ClosingCrBlnc"].ToString());


                    OpeningBalance = OpenDebitAmt + OpenCreditAmt;

                    ClosingBalance = ClosingDrBlnc + ClosingCrBlnc;


                    TotalOpeningBalCr = TotalOpeningBalCr + OpenCreditAmt;
                    TotalOpeningBalDr = TotalOpeningBalDr + OpenDebitAmt;
                    TotalClosingBalCr = TotalClosingBalCr + ClosingCrBlnc;
                    TotalClosingBalDr = TotalClosingBalDr + ClosingDrBlnc;


                    if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                    {
                        if (OpeningBalance >= 0)
                        {
                            GridView1.Rows[i].Cells[2].Text = OpeningBalance.ToString() + " Cr";
                        }
                        else
                        {
                            GridView1.Rows[i].Cells[2].Text = Math.Abs(OpeningBalance).ToString() + " Dr";
                        }

                        if (ClosingBalance >= 0)
                        {
                            GridView1.Rows[i].Cells[6].Text = ClosingBalance.ToString() + " Cr";
                        }
                        else
                        {
                            GridView1.Rows[i].Cells[6].Text = Math.Abs(ClosingBalance).ToString() + " Dr";
                        }
                    }
                    else
                    {

                        GridView1.Rows[i].Cells[2].Text = ds.Tables[0].Rows[i]["OpenDebitAmt"].ToString();

                        GridView1.Rows[i].Cells[3].Text = ds.Tables[0].Rows[i]["OpenCreditAmt"].ToString();

                        GridView1.Rows[i].Cells[6].Text = ds.Tables[0].Rows[i]["ClosingDrBlnc"].ToString();

                        GridView1.Rows[i].Cells[7].Text = ds.Tables[0].Rows[i]["ClosingCrBlnc"].ToString();

                    }
                }

                decimal opening = TotalOpeningBalCr + TotalOpeningBalDr;
                decimal Closing = TotalClosingBalCr + TotalClosingBalDr;

                if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                {
                    GridView1.FooterRow.Cells[1].Text = "<b>Grand Total</b>";

                    if (opening < 0)
                    {
                        GridView1.FooterRow.Cells[2].Text = Math.Abs(opening).ToString() + " Dr";//Dr
                    }
                    else
                    {
                        GridView1.FooterRow.Cells[2].Text = Math.Abs(opening).ToString() + " Cr";//Dr
                    }

                    GridView1.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
                    GridView1.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();


                    if (Closing < 0)
                    {
                        GridView1.FooterRow.Cells[6].Text = Math.Abs(Closing).ToString() + " Dr";//Dr
                    }
                    else
                    {
                        GridView1.FooterRow.Cells[6].Text = Math.Abs(Closing).ToString() + " Cr";//Dr
                    }


                }
                else
                {
                    GridView1.FooterRow.Cells[1].Text = "<b>Grand Total</b>";
                    GridView1.FooterRow.Cells[2].Text = Math.Abs(TotalOpeningBalDr).ToString();//Dr
                    GridView1.FooterRow.Cells[3].Text = TotalOpeningBalCr.ToString(); //Cr

                    GridView1.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
                    GridView1.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();

                    GridView1.FooterRow.Cells[6].Text = Math.Abs(TotalClosingBalDr).ToString(); //Dr
                    GridView1.FooterRow.Cells[7].Text = TotalClosingBalCr.ToString(); //Cr
                }
                GridView1.FooterRow.Cells[2].CssClass = "align-right";
                GridView1.FooterRow.Cells[3].CssClass = "align-right";
                GridView1.FooterRow.Cells[4].CssClass = "align-right";
                GridView1.FooterRow.Cells[5].CssClass = "align-right";
                GridView1.FooterRow.Cells[6].CssClass = "align-right";
                GridView1.FooterRow.Cells[7].CssClass = "align-right";
                GridView1.FooterRow.Style.Add("font-weight", "700");


                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            //GridView2.DataBind();
            //GridView3.DataBind();

            ShowHideColumn();

            //ViewState["CategoryId"] = ds.Tables[1].Rows[0]["CategoryId"].ToString();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillCategoryWiseRecord(string Category_ID, string CategoryName)
    {
        try
        {
            lblMsg.Text = "";
            decimal? DebitAmt = 0;
            decimal? CreditAmt = 0;
            GridView1.Visible = false;
            GridView2.Visible = true;
            GridView3.Visible = false;
            GridView4.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();


            lblHeadName.Text = "";
            lblOpening.Text = "";
            lblMonth.Text = "";
            DivTable.InnerHtml = "";
            ds = null;

            string Office = "";

            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            ShowColumn();
            string headingFirst = "Category : " + CategoryName + "";
            lblHeadingSecond.Text = headingFirst;

            ds = objdb.ByProcedure("Sp_Fin_CostCentreReport", new string[] { "flag", "OfficeID", "FromDate", "ToDate", "Category_ID" }, new string[] { "2", Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), Category_ID }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                //if (ViewState["Office_ID"].ToString() != "1")
                //{
                //    ds.Tables[0].Rows.Add("-1", "Profit & Loss A/C", "0", "0", "0", "0", "0", "0", "0", "0");
                //}

                GridView2.DataSource = ds;
                GridView2.DataBind();
                decimal TxnDebitAmt = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TxnDebitAmt"));
                decimal TxnCreditAmt = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TxnCreditAmt"));

                GridView2.FooterRow.Cells[4].Text = "<b>" + TxnDebitAmt.ToString() + "</b>";
                GridView2.FooterRow.Cells[5].Text = "<b>" + TxnCreditAmt.ToString() + "</b>";

                spnAltB.Visible = true;
                divExcel.Visible = true;
                ViewState["GridCount"] = Convert.ToInt32(ViewState["GridCount"]) + 1;
                GridView2.UseAccessibleHeader = true;
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;

                if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                {
                    GridView2.HeaderRow.Cells[2].Text = "Opening Bal.";

                    GridView2.HeaderRow.Cells[6].Text = "Closing Bal.";
                }



                decimal TotalOpeningBalCr = 0;
                decimal TotalOpeningBalDr = 0;
                decimal TotalClosingBalCr = 0;
                decimal TotalClosingBalDr = 0;


                decimal TotalDebitAmt = TxnDebitAmt;
                decimal TotalCreditAmt = TxnCreditAmt;


                decimal OpenTotalDr = 0;
                decimal OpenTotalCr = 0;
                decimal CloseTotalDr = 0;
                decimal CloseTotalCr = 0;


                int rowcount = 0;//ds.Tables[0].Rows.Count - 1;

                rowcount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < rowcount; i++)
                {
                    decimal OpenDebitAmt = 0;
                    decimal OpenCreditAmt = 0;
                    decimal OpeningBalance = 0;

                    decimal ClosingDrBlnc = 0;
                    decimal ClosingCrBlnc = 0;
                    decimal ClosingBalance = 0;
                    //if (ds.Tables[0].Rows[i]["ClosingDrBlnc"].ToString() != "")
                    //    ClosingDrBlnc = decimal.Parse(ds.Tables[0].Rows[i]["ClosingDrBlnc"].ToString());

                    //if (ds.Tables[0].Rows[i]["ClosingCrBlnc"].ToString() != "")
                    //    ClosingCrBlnc = decimal.Parse(ds.Tables[0].Rows[i]["ClosingCrBlnc"].ToString());


                    OpenDebitAmt = -decimal.Parse(ds.Tables[0].Rows[i]["OpenDebitAmt"].ToString());

                    OpenCreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["OpenCreditAmt"].ToString());

                    ClosingDrBlnc = -decimal.Parse(ds.Tables[0].Rows[i]["ClosingDrBlnc"].ToString());

                    ClosingCrBlnc = decimal.Parse(ds.Tables[0].Rows[i]["ClosingCrBlnc"].ToString());

                    OpeningBalance = OpenDebitAmt + OpenCreditAmt;

                    ClosingBalance = ClosingDrBlnc + ClosingCrBlnc;

                    TotalOpeningBalCr = TotalOpeningBalCr + OpenCreditAmt;
                    TotalOpeningBalDr = TotalOpeningBalDr + OpenDebitAmt;
                    TotalClosingBalCr = TotalClosingBalCr + ClosingCrBlnc;
                    TotalClosingBalDr = TotalClosingBalDr + ClosingDrBlnc;


                    if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                    {
                        if (OpeningBalance >= 0)
                        {
                            GridView2.Rows[i].Cells[2].Text = OpeningBalance.ToString() + " Cr";
                        }
                        else
                        {
                            GridView2.Rows[i].Cells[2].Text = Math.Abs(OpeningBalance).ToString() + " Dr";
                        }

                        if (ClosingBalance >= 0)
                        {
                            GridView2.Rows[i].Cells[6].Text = ClosingBalance.ToString() + " Cr";
                        }
                        else
                        {
                            GridView2.Rows[i].Cells[6].Text = Math.Abs(ClosingBalance).ToString() + " Dr";
                        }
                    }
                    else
                    {

                        GridView2.Rows[i].Cells[2].Text = ds.Tables[0].Rows[i]["OpenDebitAmt"].ToString();
                        // OpenTotalDr = OpenTotalDr + decimal.Parse(ds.Tables[0].Rows[i]["OpeningBalanceDR"].ToString());


                        GridView2.Rows[i].Cells[3].Text = ds.Tables[0].Rows[i]["OpenCreditAmt"].ToString();
                        //  OpenTotalCr = OpenTotalCr + decimal.Parse(ds.Tables[0].Rows[i]["OpeningBalanceCR"].ToString());


                        GridView2.Rows[i].Cells[6].Text = ds.Tables[0].Rows[i]["ClosingDrBlnc"].ToString();
                        // CloseTotalDr = CloseTotalDr + decimal.Parse(ds.Tables[0].Rows[i]["DebitClosingBalance"].ToString());

                        GridView2.Rows[i].Cells[7].Text = ds.Tables[0].Rows[i]["ClosingCrBlnc"].ToString();
                        // CloseTotalCr = CloseTotalCr + decimal.Parse(ds.Tables[0].Rows[i]["CreditClosingBalance"].ToString());

                    }
                }

                decimal opening = TotalOpeningBalCr + TotalOpeningBalDr;
                decimal Closing = TotalClosingBalCr + TotalClosingBalDr;


                if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                {
                    GridView2.FooterRow.Cells[1].Text = "<b>Grand Total</b>";
                    if (opening < 0)
                    {
                        GridView2.FooterRow.Cells[2].Text = Math.Abs(opening).ToString() + " Dr";//Dr
                    }
                    else
                    {
                        GridView2.FooterRow.Cells[2].Text = Math.Abs(opening).ToString() + " Cr";//Dr
                    }
                    GridView2.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
                    GridView2.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();
                    if (Closing < 0)
                    {
                        GridView2.FooterRow.Cells[6].Text = Math.Abs(Closing).ToString() + " Dr";//Dr
                    }
                    else
                    {
                        GridView2.FooterRow.Cells[6].Text = Math.Abs(Closing).ToString() + " Cr";//Dr
                    }
                }
                else
                {
                    GridView2.FooterRow.Cells[1].Text = "<b>Grand Total</b>";
                    GridView2.FooterRow.Cells[2].Text = Math.Abs(TotalOpeningBalDr).ToString();//Dr
                    GridView2.FooterRow.Cells[3].Text = TotalOpeningBalCr.ToString(); //Cr

                    GridView2.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
                    GridView2.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();

                    GridView2.FooterRow.Cells[6].Text = Math.Abs(TotalClosingBalDr).ToString(); //Dr
                    GridView2.FooterRow.Cells[7].Text = TotalClosingBalCr.ToString(); //Cr
                }





                GridView2.FooterRow.Cells[2].CssClass = "align-right";
                GridView2.FooterRow.Cells[3].CssClass = "align-right";
                GridView2.FooterRow.Cells[4].CssClass = "align-right";
                GridView2.FooterRow.Cells[5].CssClass = "align-right";
                GridView2.FooterRow.Cells[6].CssClass = "align-right";
                GridView2.FooterRow.Cells[7].CssClass = "align-right";
                GridView2.FooterRow.Style.Add("font-weight", "700");


                GridView2.UseAccessibleHeader = true;
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;

                GridView2.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            ShowHideColumn();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillCategorySubCateRecord(string Category_ID, string SubCategory_ID, string SubCategoryName)
    {
        try
        {
            lblMsg.Text = "";
            decimal? DebitAmt = 0;
            decimal? CreditAmt = 0;
            decimal DebitTotal = 0;
            decimal CreditTotal = 0;
            decimal TotalOpening = 0;
            GridView1.Visible = false;
            GridView2.Visible = false;
            GridView3.Visible = true;
            GridView4.Visible = false;
            GridView3.DataSource = null;
            GridView3.DataBind();

            lblHeadName.Text = "";
            lblOpening.Text = "";
            lblMonth.Text = "";
            DivTable.InnerHtml = "";
            ds = null;

            string Office = "";

            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            ShowColumn();

            string headingFirst = "Sub Category : " + SubCategoryName;
            lblHeadName.Text = headingFirst;

            ds = objdb.ByProcedure("Sp_Fin_CostCentreReport", new string[] { "flag", "OfficeID", "FromDate", "ToDate", "Category_ID", "SubCategoryId" },
                new string[] { "3", Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), Category_ID, SubCategory_ID }, "dataset");
            if (ds != null && ds.Tables.Count != 0)
            {
                decimal OpeningBalance = 0;
                if (ds.Tables[1].Rows.Count > 0)
                {
                    OpeningBalance = Convert.ToDecimal(ds.Tables[1].Rows[0]["OpeningBalance"].ToString());
                    if (OpeningBalance < 0)
                        lblOpening.Text = "Opening Balance : <span style='color:red;'>" + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                    else
                        lblOpening.Text = "Opening Balance :<span style='color:red;'>" + Math.Abs(OpeningBalance).ToString() + " Cr</span>";

                }
                GridView3.DataSource = ds;
                GridView3.DataBind();
                divExcel.Visible = true;
                spnAltB.Visible = true;
                ViewState["GridCount"] = Convert.ToInt32(ViewState["GridCount"]) + 1;
                GridView3.UseAccessibleHeader = true;
                GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
               
                ViewState["Bal"] = (decimal.Parse(ds.Tables[1].Rows[0]["OpeningBalance"].ToString())).ToString();
                //decimal OpeningBal = decimal.Parse(ViewState["Bal"].ToString());
                int rowcount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < rowcount; i++)
                {

                    string DebitMonthAmt = "0";
                    
                    decimal CreditMonthAmt = 0;
                    decimal CurrentOpening = 0;

                    //if (i != 0)
                    //{
                    if (ds.Tables[0].Rows[i]["TxnDrAmount"].ToString() != "")
                        DebitMonthAmt = "-" + ds.Tables[0].Rows[i]["TxnDrAmount"].ToString();
                    DebitTotal = DebitTotal + decimal.Parse(DebitMonthAmt);

                    if (ds.Tables[0].Rows[i]["TxnCrAmount"].ToString() != "")
                        CreditMonthAmt = decimal.Parse(ds.Tables[0].Rows[i]["TxnCrAmount"].ToString());
                    CreditTotal = CreditTotal + CreditMonthAmt;
                    //}

                    Label lblMonthOpening = (Label)GridView3.Rows[i].Cells[0].FindControl("lblMonthOpening");

                    lblMonthOpening.Text = OpeningBalance.ToString();
                    //if (OpeningBalance >= 0)
                    //{
                    //    lblMonthOpening.Text = OpeningBalance.ToString();
                    //}
                    //else
                    //{
                    //    lblMonthOpening.Text = Math.Abs(OpeningBalance).ToString();
                    //}


                    OpeningBalance = OpeningBalance + decimal.Parse(DebitMonthAmt) + CreditMonthAmt;
                    TotalOpening = TotalOpening + OpeningBalance;
                    if (OpeningBalance >= 0)
                    {
                        GridView3.Rows[i].Cells[4].Text = OpeningBalance.ToString() + " Cr";
                    }
                    else
                    {
                        GridView3.Rows[i].Cells[4].Text = Math.Abs(OpeningBalance).ToString() + " Dr";
                    }

                    
                    
                       


                }
                GridView3.FooterRow.Cells[1].Text = "<b>Grand Total</b>";
                GridView3.FooterRow.Cells[2].Text = Math.Abs(DebitTotal).ToString();//Dr
                GridView3.FooterRow.Cells[3].Text = CreditTotal.ToString(); //Cr
               






                GridView3.FooterRow.Cells[2].CssClass = "align-right";
                GridView3.FooterRow.Cells[3].CssClass = "align-right";
                GridView3.FooterRow.Cells[4].CssClass = "align-right";

                GridView3.FooterRow.Style.Add("font-weight", "700");


                GridView3.UseAccessibleHeader = true;
                GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;

                GridView3.FooterRow.TableSection = TableRowSection.TableFooter;
            }

            ShowHideColumn();
            //int status = 0;
            //if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
            //{
            //    GridView2.FooterRow.Cells[1].Text = "<b>Grand Total</b>";
            //    GridView2.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
            //    GridView2.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();
            //}
            //else
            //{
            //    GridView2.FooterRow.Cells[1].Text = "<b>Grand Total</b>";
            //    GridView2.FooterRow.Cells[2].Text = Math.Abs(OpenTotalDr).ToString();//Dr
            //    GridView2.FooterRow.Cells[3].Text = OpenTotalCr.ToString(); //Cr

            //    GridView2.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
            //    GridView2.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();

            //    GridView2.FooterRow.Cells[6].Text = Math.Abs(CloseTotalDr).ToString(); //Dr
            //    GridView2.FooterRow.Cells[7].Text = CloseTotalCr.ToString(); //Cr
            //}
            //GridView2.FooterRow.Cells[2].CssClass = "align-right";
            //GridView2.FooterRow.Cells[3].CssClass = "align-right";
            //GridView2.FooterRow.Cells[4].CssClass = "align-right";
            //GridView2.FooterRow.Cells[5].CssClass = "align-right";
            //GridView2.FooterRow.Cells[6].CssClass = "align-right";
            //GridView2.FooterRow.Cells[7].CssClass = "align-right";
            //GridView2.FooterRow.Style.Add("font-weight", "700");


            //GridView2.UseAccessibleHeader = true;
            //GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;

            //GridView2.FooterRow.TableSection = TableRowSection.TableFooter;
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
            lblExecTime.Text = "";
            lblHeadingSecond.Text = "";
            lblHeadName.Text = "";
            lblOpening.Text = "";
            lblMonth.Text = "";
            lblheadingFirst.Text = "";
            lblOpening.Visible = true;
            ViewState["GridCount"] = "0";
            var watch = System.Diagnostics.Stopwatch.StartNew();
            lblMsg.Text = "";
            if (chkClosingBal.Checked == false && chkOpeningBal.Checked == false && chkTransactionAmt.Checked == false)
            {
                chkClosingBal.Checked = true;
            }
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
            if (Office != "")
            {
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
               // string headingFirst = "<p class='text-center' style='font-weight:600'>" + ViewState["Office_FinAddress"].ToString() + " <br />  Cost Centre Report<br />" + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "SubCategory: "+ddlSubCategory.SelectedItem.Text+"</p>";
                string headingFirst = "<p class='text-center' style='font-weight:600'>Cost Centre Report<br />" + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "SubCategory: " + ddlSubCategory.SelectedItem.Text + "</p>";
                lblheadingFirst.Text = headingFirst;
                FillGrid();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select atleast one Office.');", true);
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView1.Rows[index];
            Label lblCategoryId = (Label)row.Cells[0].FindControl("lblCategoryId");
            Label lblCategoryName = (Label)row.Cells[0].FindControl("lblCategoryName");
            Label lblDebitAmt = (Label)row.Cells[0].FindControl("lblDebitAmt");
            Label lblCreditAmt = (Label)row.Cells[0].FindControl("lblCreditAmt");

            decimal debit = 0;
            decimal credit = 0;
            if (lblDebitAmt.Text == "")
                debit = 0;
            else
                debit = decimal.Parse(lblDebitAmt.Text);

            if (lblCreditAmt.Text == "")
                credit = 0;
            else
                credit = decimal.Parse(lblCreditAmt.Text);

            if (debit != 0 || credit != 0)
            {
                lblOpening.Visible = true;
                ViewState["CategoryID"] = lblCategoryId.Text;
                FillCategorySubCateRecord(lblCategoryId.Text, ddlSubCategory.SelectedValue, ddlSubCategory.SelectedItem.Text);
            }
        }
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            lblOpening.Visible = true;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView2.Rows[index];
            Label lblCategory_ID = (Label)row.Cells[0].FindControl("lblCategory_ID");
            Label lblSubCategoryId = (Label)row.Cells[0].FindControl("lblSubCategoryId");
            Label lblSubCategoryName = (Label)row.Cells[0].FindControl("lblSubCategoryName");

            Label lblDebitAmt = (Label)row.Cells[0].FindControl("lblDebitAmt");
            Label lblCreditAmt = (Label)row.Cells[0].FindControl("lblCreditAmt");

            decimal debit = 0;
            decimal credit = 0;
            if (lblDebitAmt.Text == "")
                debit = 0;
            else
                debit = decimal.Parse(lblDebitAmt.Text);

            if (lblCreditAmt.Text == "")
                credit = 0;
            else
                credit = decimal.Parse(lblCreditAmt.Text);

            if (debit != 0 || credit != 0)
            {

                ViewState["CategoryID"] = lblCategory_ID.Text;
                ViewState["SubCategoryId"] = lblSubCategoryId.Text;
                FillCategorySubCateRecord(lblCategory_ID.Text, lblSubCategoryId.Text, lblSubCategoryName.Text);
            }
        }
    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView3.Rows[index];
            Label lblMonthID = (Label)row.Cells[0].FindControl("lblMonthID");
            Label lblMonthName = (Label)row.Cells[0].FindControl("lblMonthName");
            Label lblMonthOpening = (Label)row.Cells[0].FindControl("lblMonthOpening");

            Label lblMCategory_ID = (Label)row.Cells[0].FindControl("lblMCategory_ID");
            Label lblMSubCategoryId = (Label)row.Cells[0].FindControl("lblMSubCategoryId");

            Label lblDebitAmt = (Label)row.Cells[0].FindControl("lblDebitAmt");
            Label lblCreditAmt = (Label)row.Cells[0].FindControl("lblCreditAmt");

            decimal debit = 0;
            decimal credit = 0;
            if (lblDebitAmt.Text == "")
                debit = 0;
            else
                debit = decimal.Parse(lblDebitAmt.Text);

            if (lblCreditAmt.Text == "")
                credit = 0;
            else
                credit = decimal.Parse(lblCreditAmt.Text);

            if (debit != 0 || credit != 0)
            {


                decimal OpeningBal = 0;
                if (lblMonthOpening.Text != "")
                {
                    OpeningBal = decimal.Parse(lblMonthOpening.Text);
                }




                string Office = "";

                foreach (ListItem item in ddlOffice.Items)
                {
                    if (item.Selected)
                    {
                        Office += item.Value + ",";
                    }
                }
                GridView1.Visible = false;
                GridView2.Visible = false;
                GridView3.Visible = false;
                GridView4.Visible = true;
                GridView4.DataSource = null;
                GridView4.DataBind();

                ds = objdb.ByProcedure("Sp_Fin_CostCentreReport", new string[] { "flag", "OfficeID", "FromDate", "ToDate", "Category_ID", "SubCategoryId", "MonthID" },
                  new string[] { "4", Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["CategoryID"].ToString(),ddlSubCategory.SelectedValue, lblMonthID.Text }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    lblOpening.Visible = false;
                    lblMonth.Text = "Month : " + lblMonthName.Text;

                    GridView4.DataSource = ds;
                    GridView4.DataBind();
                    divExcel.Visible = true;
                    spnAltB.Visible = true;
                    ViewState["GridCount"] = Convert.ToInt32(ViewState["GridCount"]) + 1;

                    decimal CurrentBal = 0;
                    decimal DebitTotal = 0;
                    decimal CreditTotal = 0;

                    StringBuilder htmlStr = new StringBuilder();

                    htmlStr.Append("<table  id='DetailGrid' class='lastdatatable table table-hover table-bordered' >");
                    htmlStr.Append("<thead>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<th style='width: 65px;'>Voucher Date</th>");
                    htmlStr.Append("<th>Particulars</th>");
                    htmlStr.Append("<th>Vch Type</th>");
                    // htmlStr.Append("<th>Office Name</th>");
                    htmlStr.Append("<th style='width:70px !important'>Vch No.</th>");
                    htmlStr.Append("<th>Debit Amt.</th>");
                    htmlStr.Append("<th>Credit Amt.</th>");
                    htmlStr.Append("<th class='hide_print'>Action</th>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("</thead>");

                    htmlStr.Append("<tbody>");
                    int Count = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");


                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");

                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                        // htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                        htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                        htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");

                        string DebitAmt = "0";
                        decimal CreditAmt = 0;


                        if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                        {
                            DebitAmt = "-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString();
                            DebitTotal = DebitTotal + decimal.Parse(ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                        }
                        if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                        {
                            CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                            CreditTotal = CreditTotal + decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                        }

                        CurrentBal = CurrentBal + decimal.Parse(DebitAmt) + CreditAmt;

                        htmlStr.Append("<td  class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a> ");
                        htmlStr.Append("<a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("2") + "' target='_blank'>Edit</a> </td>");
                        htmlStr.Append("</tr>");
                    }
                    htmlStr.Append("</tbody>");
                    htmlStr.Append("<tfoot>");

                    //OPENING BALANCE TOTAL
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td><b>OPENING BALANCE :</b></td>");
                    // htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    string TotalBAL = "";
                    if (OpeningBal < 0)
                    {
                        TotalBAL = "0.00";

                        if (Math.Abs(OpeningBal) != 0)
                            TotalBAL = Math.Abs(OpeningBal).ToString();

                        htmlStr.Append("<td class='align-right'><b>" + TotalBAL.ToString() + "</b></td>");
                        htmlStr.Append("<td></td>");

                    }
                    else
                    {
                        TotalBAL = "0.00";
                        htmlStr.Append("<td></td>");

                        if (Math.Abs(OpeningBal) != 0)
                            TotalBAL = Math.Abs(OpeningBal).ToString();

                        htmlStr.Append("<td class='align-right'><b>" + TotalBAL.ToString() + "</b></td>");
                    }
                    htmlStr.Append("<td  class='hide_print'></td>");
                    htmlStr.Append("</tr>");

                    //CURRENT TOTAL
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td><b>CURRENT TOTAL :</b></td>");
                    //htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");

                    //DebitTotal = 0;
                    //CreditTotal = 0;
                    htmlStr.Append("<td class='align-right'><b>" + Math.Abs(DebitTotal).ToString() + "</b></td>");
                    htmlStr.Append("<td class='align-right'><b>" + Math.Abs(CreditTotal).ToString() + "</b></td>");

                    htmlStr.Append("<td class='hide_print'></td>");
                    htmlStr.Append("</tr>");
                    //CLOSING BALANCE TOTAL
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td><b>CLOSING BALANCE : </b></td>");
                    // htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    if ((OpeningBal + CurrentBal) < 0)
                    {
                        htmlStr.Append("<td class='align-right'><b>" + Math.Abs((OpeningBal + CurrentBal)).ToString() + "</b></td>");
                        htmlStr.Append("<td></td>");

                    }
                    else
                    {
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td class='align-right'><b>" + Math.Abs((OpeningBal + CurrentBal)).ToString() + "</b></td>");
                    }
                    htmlStr.Append("<td class='hide_print'></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("</tfoot>");
                    htmlStr.Append("</table>");

                    DivTable.InnerHtml = htmlStr.ToString();




                }
            }
        }
    }
    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "Editing")
            {
                string VoucherTx_ID = e.CommandArgument.ToString();
                DataSet dsPageURL = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "VoucherTx_ID" },
                    new string[] { "30", VoucherTx_ID },
                    "dataset");

                if (dsPageURL != null)
                {

                    string Url = dsPageURL.Tables[0].Rows[0]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                    Url = Url + "&Action=" + objdb.Encrypt("2");
                    Response.Redirect(Url);

                }
            }
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
            if (e.CommandName == "Print")
            {

                string VoucherTx_ID = e.CommandArgument.ToString();
                ds = objdb.ByProcedure("SpFinVoucherTx",
                  new string[] { "flag", "VoucherTx_ID" },
                  new string[] { "31", VoucherTx_ID },
                  "dataset");

                if (ds != null)
                {
                    string VoucherTx_Type = ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString();
                    if (VoucherTx_Type == "Payment" || VoucherTx_Type == "Contra" || VoucherTx_Type == "GSTService Purchase" || VoucherTx_Type == "Cash Payment")
                    {

                        string Url = "VoucherContraInvoice.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                        Response.Redirect(Url);

                    }
                    else if (VoucherTx_Type == "Receipt" || VoucherTx_Type == "Journal" || VoucherTx_Type == "Bank Receipt" || VoucherTx_Type == "Journal HO" || VoucherTx_Type == "CreditNote Voucher" || VoucherTx_Type == "DebitNote Voucher")
                    {

                        string Url = "VoucherJournalInvoice.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                        Response.Redirect(Url);


                    }
                    else if (VoucherTx_Type == "CashSale Voucher" || VoucherTx_Type == "CreditSale Voucher" || VoucherTx_Type == "GSTGoods Purchase" || VoucherTx_Type == "Goods Purchase Tax Free" || VoucherTx_Type == "CC Sale Voucher" || VoucherTx_Type == "JV Sale Voucher" || VoucherTx_Type == "GST Sale Voucher" || VoucherTx_Type == "DCS Sale Voucher")
                    {
                        string Url = "VoucherSalepurchaseInvocie.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                        Response.Redirect(Url);
                    }
                    else
                    {

                    }
                }
            }
            if (e.CommandName == "ReceiptPrint")
            {
                string VoucherTx_ID = e.CommandArgument.ToString();
                string Url = "ReceivingInvoice.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                Response.Redirect(Url);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        ViewState["GridCount"] = Convert.ToInt32(ViewState["GridCount"]) - 1;
        if (ViewState["GridCount"].ToString() == "1")
        {
            GridView1.Visible = true;
            GridView2.Visible = false;
            GridView3.Visible = false;
            GridView4.Visible = false;

            lblOpening.Visible = true;
            lblHeadingSecond.Text = "";
            lblHeadName.Text = "";
            lblOpening.Text = "";
            lblMonth.Text = "";
            DivTable.InnerHtml = "";
        }
        //else if (ViewState["GridCount"].ToString() == "2")
        //{
        //    GridView1.Visible = false;
        //    GridView2.Visible = true;
        //    GridView3.Visible = false;
        //    GridView4.Visible = false;

        //    lblOpening.Visible = true;
        //    lblHeadName.Text = "";
        //    lblOpening.Text = "";
        //    lblMonth.Text = "";
        //    DivTable.InnerHtml = "";
        //}
        else if (ViewState["GridCount"].ToString() == "2")
        {
            GridView1.Visible = false;
            GridView2.Visible = false;
            GridView3.Visible = true;
            GridView4.Visible = false;

            lblOpening.Visible = true;
            DivTable.InnerHtml = "";
            lblMonth.Text = "";
        }
        else if (ViewState["GridCount"].ToString() == "3")
        {
            GridView1.Visible = false;
            GridView2.Visible = false;
            GridView3.Visible = false;
            GridView4.Visible = true;
            // lblMonth.Text = "";
            DivTable.InnerHtml = "";
        }
    }
    protected void ddlRegionalOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblHeadingSecond.Text = "";
        lblHeadName.Text = "";
        lblOpening.Text = "";
        lblMonth.Text = "";
        DivTable.InnerHtml = "";
     
        GridView1.Visible = true;
        GridView2.Visible = false;
        GridView3.Visible = false;
        GridView4.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        FillOffice();
    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblHeadingSecond.Text = "";
        lblHeadName.Text = "";
        lblOpening.Text = "";
        lblMonth.Text = "";
        DivTable.InnerHtml = "";

        GridView1.Visible = true;
        GridView2.Visible = false;
        GridView3.Visible = false;
        GridView4.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        FillSubCategory();
    }
}