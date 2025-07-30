using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;
using System;
using System.Data;
using System.Drawing.Printing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_FinSupplierPlantWiseReport : System.Web.UI.Page
{
    DataSet ds, ds1;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                FillDropdown();
                btnPrint.Visible = false;
                btnExcel.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillDropdown()
    {
        try
        {

            ds = objdb.ByProcedure("SpFinSupplierItem",
                        new string[] { "flag" },
                        new string[] { "8" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlPlant.DataSource = ds;
                ddlPlant.DataTextField = "Office_Name";
                ddlPlant.DataValueField = "Office_ID";
                ddlPlant.DataBind();
                // ddlPlant.Items.Insert(0, "Select");
                ddlPlant.Items.Insert(0, new ListItem("All", "0"));
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
            lblMsg.Text = "";
            btnPrint.Visible = false;
            btnExcel.Visible = false;
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                ds = objdb.ByProcedure("SpFinSupplierOrder",
                    new string[] { "flag", "FromDate", "ToDate", "PlantID" },
                    new string[] { "13", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ddlPlant.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    btnPrint.Visible = true;
                    btnExcel.Visible = true;
                    //GridView1.DataSource = ds;
                    //GridView1.DataBind();
                    string HTMLTABLE = " <h4 class='box-title'> Supply Details By Order Date(" + txtFromDate.Text + "-" + txtToDate.Text + ")</h4>";
                    HTMLTABLE += "<table class='table table-hover table-bordered' border='1'>";
                    HTMLTABLE += "<thead><tr>";
                    HTMLTABLE += "<th>Plant</th>";
                    HTMLTABLE += "<th>Supplier</th>";
                    HTMLTABLE += "<th>Quantity</th>";
                    HTMLTABLE += "<th>UNIT</th>";
                    HTMLTABLE += "<th>Basic</th>";
                    HTMLTABLE += "<th>GST</th>";
                    HTMLTABLE += "<th>Cess</th>";
                    HTMLTABLE += "<th>Total</th>";
                    HTMLTABLE += "<th>Bill No /Date</th>";
                    HTMLTABLE += "<th>Qty</th>";
                    HTMLTABLE += "<th>Basic</th>";
                    HTMLTABLE += "<th>GST</th>";
                    HTMLTABLE += "<th>Cess</th>";
                    HTMLTABLE += "<th>Total</th>";
                    HTMLTABLE += "<th>Extra Supply Deduction</th>";
                    HTMLTABLE += "<th>Deduction if any</th>";
                    HTMLTABLE += "<th>TDS</th>";
                    HTMLTABLE += "<th>TCS</th>";
                    HTMLTABLE += "<th>Round</th>";
                    HTMLTABLE += "<th>Net Payble Amount</th>";
                    HTMLTABLE += "<th>Plant Test Report</th>";
                    HTMLTABLE += "<th>NABL Report</th>";
                    HTMLTABLE += "<th>Supplier In House Report</th>";
                    HTMLTABLE += "<th>Toll Kanta Slip</th>";
                    HTMLTABLE += "<th>E-way bill</th>";


                    HTMLTABLE += "</tr></thead>";

                    HTMLTABLE += "<tbody>";
                    int OrderRowCount = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < OrderRowCount; i++)
                    {
                        string OrderNo = ds.Tables[0].Rows[i]["OrderNo"].ToString();
                        string OrderDate = ds.Tables[0].Rows[i]["OrderDate"].ToString();

                        int BillRowCount = 0, RowSpan = 0;
                        string PreviousCategory = "", PreviousItem = "";
                        ds1 = objdb.ByProcedure("SpFinSupplierOrder",
                             new string[] { "flag", "OrderDate", "PlantID", "OrderNo", "SupplierID" },
                                new string[] { "14", Convert.ToDateTime(OrderDate, cult).ToString("yyyy/MM/dd"), ds.Tables[0].Rows[i]["PlantID"].ToString(), OrderNo, ds.Tables[0].Rows[i]["SupplierID"].ToString() }, "dataset");
                        if (ds1.Tables.Count > 0)
                        {
                            BillRowCount = ds1.Tables[0].Rows.Count;
                            for (int h = 0; h < BillRowCount; h++)
                            {
                                if (PreviousCategory.Trim() == ds1.Tables[0].Rows[h]["CategoryName"].ToString().Trim())
                                {
                                    RowSpan = RowSpan + 1;
                                }
                                PreviousCategory = ds1.Tables[0].Rows[h]["CategoryName"].ToString();
                               // PreviousItem = ds1.Tables[0].Rows[h]["ItemName"].ToString();
                            }
                        }


                        if (BillRowCount > 0)
                        {
                            HTMLTABLE += "<tr>";
                            HTMLTABLE += "<td><b>" + ds.Tables[0].Rows[i]["PlantName"].ToString() + "</b></td><td><b>" + ds.Tables[0].Rows[i]["SupplierName"].ToString() + "</b></td><td colspan='2'>" + ds.Tables[0].Rows[i]["OrderNo"].ToString() + "/" + ds.Tables[0].Rows[i]["OrderDateView"].ToString() + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
                            HTMLTABLE += "</tr>";

                            decimal OrderQty = 0; //decimal.Parse(ds1.Tables[1].Rows[0]["Quantity"].ToString());
                            decimal ReceiveQty = decimal.Parse(ds1.Tables[1].Rows[0]["QuantityReceive"].ToString());

                            string PreCategory = "";
                            string itemname = "";
                            decimal qty = 0;
                            for (int k = 0; k < BillRowCount; k++)
                            {
                                string Br = "";
                                for (int b = 0; b < ((BillRowCount) / 2); b++)
                                {
                                    Br += "<br/>";
                                }
                                HTMLTABLE += "<tr>";
                                if (ds1.Tables[0].Rows[k]["CategoryName"].ToString() != ds1.Tables[0].Rows[k]["ItemName"].ToString())
                                {
                                    if (ds1.Tables[0].Rows[k]["CategoryName"].ToString() != PreCategory)
                                    {
                                        HTMLTABLE += "<td style='text-align : center;' rowspan='" + (RowSpan+1) + "'>" + Br + "<span>" + ds1.Tables[0].Rows[k]["CategoryName"].ToString() + "</span></td>";//New Column for Category
                                    }
                                    else if (ds1.Tables[0].Rows[k]["CategoryName"].ToString() != PreCategory)
                                    {
                                        HTMLTABLE += "<td style='text-align : center;' rowspan='" + (RowSpan+1) + "'>" + Br + "<span>" + ds1.Tables[0].Rows[0]["CategoryName"].ToString() + "</span></td>";//New Column for Category
                                    }
                                    PreCategory = ds1.Tables[0].Rows[k]["CategoryName"].ToString();
                                }
                                else if (ds1.Tables[0].Rows[k]["CategoryName"].ToString() == ds1.Tables[0].Rows[k]["ItemName"].ToString())
                                {
                                    HTMLTABLE += "<td></td>";
                                }
                                HTMLTABLE += "<td>" + ds1.Tables[0].Rows[k]["ItemName"].ToString() + "</td>";
                                if (itemname == "")
                                {
                                    HTMLTABLE += "<td style='text-align: right;'>" + Convert.ToDecimal(ds1.Tables[0].Rows[k]["Quantity"].ToString()).ToString("0.00") + "</td>";
                                    itemname = ds1.Tables[0].Rows[k]["ItemName"].ToString();
                                    OrderQty = OrderQty + decimal.Parse(ds1.Tables[0].Rows[k]["Quantity"].ToString());
                                    qty = decimal.Parse(ds1.Tables[0].Rows[k]["Quantity"].ToString());
                                }
                                else if (itemname != ds1.Tables[0].Rows[k]["ItemName"].ToString())
                                {
                                    HTMLTABLE += "<td style='text-align: right;'>" + Convert.ToDecimal(ds1.Tables[0].Rows[k]["Quantity"].ToString()).ToString("0.00") + "</td>";
                                    itemname = ds1.Tables[0].Rows[k]["ItemName"].ToString();
                                    OrderQty = OrderQty + decimal.Parse(ds1.Tables[0].Rows[k]["Quantity"].ToString());
                                    qty = decimal.Parse(ds1.Tables[0].Rows[k]["Quantity"].ToString());
                                }
                                else
                                {
                                    if (qty == decimal.Parse(ds1.Tables[0].Rows[k]["Quantity"].ToString()))
                                    {
                                        HTMLTABLE += "<td style='text-align: right;'></td>";
                                    }
                                    else
                                    {
                                        HTMLTABLE += "<td style='text-align: right;'>" + Convert.ToDecimal(ds1.Tables[0].Rows[k]["Quantity"].ToString()).ToString("0.00") + "</td>";

                                        OrderQty = OrderQty + decimal.Parse(ds1.Tables[0].Rows[k]["Quantity"].ToString());
                                        qty = decimal.Parse(ds1.Tables[0].Rows[k]["Quantity"].ToString());

                                    }


                                }

                                HTMLTABLE += "<td>" + ds1.Tables[0].Rows[k]["Unit"].ToString() + "</td>";
                                HTMLTABLE += "<td style='text-align: right;'>" + ds1.Tables[0].Rows[k]["BasicRate"].ToString() + "</td>";
                                HTMLTABLE += "<td style='text-align: right;'>" + ds1.Tables[0].Rows[k]["GSTRateAmount"].ToString() + "</td>";
                                HTMLTABLE += "<td style='text-align: right;'>" + ds1.Tables[0].Rows[k]["CessRate"].ToString() + "</td>";
                                HTMLTABLE += "<td style='text-align: right;'>" + ds1.Tables[0].Rows[k]["TotalRate"].ToString() + "</td>";

                                HTMLTABLE += "<td>" + ds1.Tables[0].Rows[k]["BillNo"].ToString() + "/" + ds1.Tables[0].Rows[k]["BillDateView"].ToString() + "</td>";
                                HTMLTABLE += "<td style='text-align: right;'>" + ds1.Tables[0].Rows[k]["QuantityReceive"].ToString() + "</td>";
                                HTMLTABLE += "<td style='text-align: right;'>" + ds1.Tables[0].Rows[k]["BasicAmount"].ToString() + "</td>";
                                HTMLTABLE += "<td style='text-align: right;'>" + ds1.Tables[0].Rows[k]["GSTAmount"].ToString() + "</td>";
                                HTMLTABLE += "<td style='text-align: right;'>" + ds1.Tables[0].Rows[k]["Cess"].ToString() + "</td>";
                                HTMLTABLE += "<td style='text-align: right;'>" + ds1.Tables[0].Rows[k]["TotalAmount"].ToString() + "</td>";
                                HTMLTABLE += "<td style='text-align: right;'>" + ds1.Tables[0].Rows[k]["ExtraSupDed"].ToString() + "</td>";
                                HTMLTABLE += "<td style='text-align: right;'>" + ds1.Tables[0].Rows[k]["DeductionAmount"].ToString() + "</td>";
                                HTMLTABLE += "<td style='text-align: right;'>" + ds1.Tables[0].Rows[k]["TDS"].ToString() + "</td>";
                                HTMLTABLE += "<td style='text-align: right;'>" + ds1.Tables[0].Rows[k]["TCS"].ToString() + "</td>";

                                HTMLTABLE += "<td style='text-align: right;'>" + ds1.Tables[0].Rows[k]["RoundOff"].ToString() + "</td>";
                                HTMLTABLE += "<td style='text-align: right;'>" + ds1.Tables[0].Rows[k]["NetPayment"].ToString() + "</td>";
                                HTMLTABLE += "<td>" + ds1.Tables[0].Rows[k]["QualityCleanReport"].ToString() + "</td>";
                                HTMLTABLE += "<td>" + ds1.Tables[0].Rows[k]["NABLReport"].ToString() + "</td>";
                                HTMLTABLE += "<td>" + ds1.Tables[0].Rows[k]["SupplierInHouseReport"].ToString() + "</td>";
                                HTMLTABLE += "<td>" + ds1.Tables[0].Rows[k]["TollKantaSlip"].ToString() + "</td>";
                                HTMLTABLE += "<td>" + ds1.Tables[0].Rows[k]["EWayBill"].ToString() + "</td>";
                                HTMLTABLE += "</tr>";
                            }

                            // Total Quantity
                            HTMLTABLE += "<tr>";
                            //if (ds1.Tables[0].Rows[BillRowCount - 1]["CategoryName"].ToString() == ds1.Tables[0].Rows[BillRowCount - 1]["ItemName"].ToString())
                            //{
                                HTMLTABLE += "<td></td>";
                            //}
                            HTMLTABLE += "<td><b>Order Qty</b></td>";
                            HTMLTABLE += "<td style='text-align: right;'>" + Convert.ToDecimal((OrderQty).ToString()).ToString("0.00") + "</td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";

                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "</tr>";
                            // Receive Quantity


                            HTMLTABLE += "<tr>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td><b>Bill Qty</b></td>";
                            HTMLTABLE += "<td style='text-align: right;'>" + Convert.ToDecimal((ReceiveQty).ToString()).ToString("0.00") + "</td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";

                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "</tr>";

                            // Balance Quantity
                            HTMLTABLE += "<tr>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td><b>Balance</b></td>";
                            HTMLTABLE += "<td style='text-align: right;'>" + Convert.ToDecimal((OrderQty - ReceiveQty).ToString()).ToString("0.00") + "</td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";

                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "</tr>";


                            // Total
                            HTMLTABLE += "<tr>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td style='background-color: #eded07a8; font-weight: 700; '>Total</td>";
                            HTMLTABLE += "<td style='background-color: #eded07a8; font-weight: 700;  text-align: right;'>" + Convert.ToDecimal(ds1.Tables[1].Rows[0]["QuantityReceive"].ToString()).ToString("0.00") + "</td>";
                            HTMLTABLE += "<td style='background-color: #eded07a8; font-weight: 700;  text-align: right;'>" + Convert.ToDecimal(ds1.Tables[1].Rows[0]["BasicAmount"].ToString()).ToString("0.00") + "</td>";
                            HTMLTABLE += "<td style='background-color: #eded07a8; font-weight: 700;  text-align: right;'>" + Convert.ToDecimal(ds1.Tables[1].Rows[0]["GSTAmount"].ToString()).ToString("0.00") + "</td>";
                            HTMLTABLE += "<td style='background-color: #eded07a8; font-weight: 700;  text-align: right;'>" + Convert.ToDecimal(ds1.Tables[1].Rows[0]["Cess"].ToString()).ToString("0.00") + "</td>";
                            HTMLTABLE += "<td style='background-color: #eded07a8; font-weight: 700;  text-align: right;'>" + Convert.ToDecimal(ds1.Tables[1].Rows[0]["TotalAmount"].ToString()).ToString("0.00") + "</td>";
                            HTMLTABLE += "<td style='background-color: #eded07a8; font-weight: 700;  text-align: right;'>" + Convert.ToDecimal(ds1.Tables[1].Rows[0]["ExtraSupDed"].ToString()).ToString("0.00") + "</td>";
                            HTMLTABLE += "<td style='background-color: #eded07a8; font-weight: 700;  text-align: right;'>" + Convert.ToDecimal(ds1.Tables[1].Rows[0]["DeductionAmount"].ToString()).ToString("0.00") + "</td>";
                            HTMLTABLE += "<td style='background-color: #eded07a8; font-weight: 700;  text-align: right;'>" + Convert.ToDecimal(ds1.Tables[1].Rows[0]["TDS"].ToString()).ToString("0.00") + "</td>";
                            HTMLTABLE += "<td style='background-color: #eded07a8; font-weight: 700;  text-align: right;'>" + Convert.ToDecimal(ds1.Tables[1].Rows[0]["TCS"].ToString()).ToString("0.00") + "</td>";
                            HTMLTABLE += "<td style='background-color: #eded07a8; font-weight: 700;  text-align: right;'>" + Convert.ToDecimal(ds1.Tables[1].Rows[0]["RoundOff"].ToString()).ToString("0.00") + "</td>";
                            HTMLTABLE += "<td style='background-color: #eded07a8; font-weight: 700;  text-align: right;'>" + Convert.ToDecimal(ds1.Tables[1].Rows[0]["NetPayment"].ToString()).ToString("0.00") + "</td>";
                            HTMLTABLE += "<td></td>";

                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";
                            HTMLTABLE += "<td></td>";

                            HTMLTABLE += "</tr>";

                        }


                    }
                    HTMLTABLE += "</tbody>";

                    HTMLTABLE += "</table>";



                    DivSupplyData.InnerHtml = HTMLTABLE;
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment; filename=SupplierPlantWiseReport" + (System.DateTime.Now.ToString("dd-MM-yyyy_HH_mm_ss_fff")) + ".xls");
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite =
        new HtmlTextWriter(stringWrite);
        DivSupplyData.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        PrinterSettings ps = new PrinterSettings();
        PrintDocument printDoc = new PrintDocument();
        printDoc.PrinterSettings = ps;
        printDoc.DefaultPageSettings.PaperSize = new PaperSize("Custom", 420, 297);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "window.print()", true);
    }
}