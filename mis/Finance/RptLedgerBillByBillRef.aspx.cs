using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Finance_RptLedgerBillByBillRef : System.Web.UI.Page
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
                    divExcel.Visible = false;
                    FillDropdown();
                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        ddlOffice.Enabled = false;
                    }
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
            if (ViewState["Office_ID"].ToString() == "1")
            {
                ddlOffice.Enabled = true;
            }
            ds = objdb.ByProcedure("SpFinRptLedgerBillByBillRef",
                   new string[] { "flag" },
                   new string[] { "0" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
            if (ddlOffice.SelectedIndex > 0)
            {
                DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerBillByBillRef", new string[] { "flag", "Office_ID" }, new string[] { "2", ddlOffice.SelectedValue.ToString() }, "dataset");
                if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                {
                    ddlLedger.DataSource = ds1;
                    ddlLedger.DataTextField = "Ledger_Name";
                    ddlLedger.DataValueField = "Ledger_ID";
                    ddlLedger.DataBind();
                    ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblGrid.Text = "";
            GridViewRefDetail.DataSource = null;
            GridViewRefDetail.DataBind();
            ddlLedger.DataSource = null;
            ddlLedger.DataBind();

            if (ddlOffice.SelectedIndex > 0)
            {
                DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerBillByBillRef", new string[] { "flag", "Office_ID" }, new string[] { "2", ddlOffice.SelectedValue.ToString() }, "dataset");
                if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                {
                    ddlLedger.DataSource = ds1;
                    ddlLedger.DataTextField = "Ledger_Name";
                    ddlLedger.DataValueField = "Ledger_ID";
                    ddlLedger.DataBind();
                    ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlLedger_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblGrid.Text = "";
            GridViewRefDetail.DataSource = null;
            GridViewRefDetail.DataBind();

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
            var watch = System.Diagnostics.Stopwatch.StartNew();

            lblMsg.Text = "";
            lblGrid.Text = "";
            GridViewRefDetail.DataSource = null;
            GridViewRefDetail.DataBind();
            if (ddlOffice.SelectedIndex > 0 && ddlLedger.SelectedIndex > 0)
            {
                string headingFirst = "<p class='text-center' style='font-weight:600'>Ledger Wise Pending References <br /> MP State Agro Industries Development Corporation, <br/> [ " + ddlOffice.SelectedItem.Text + " ] <br /></p>";
                lblheadingFirst.Text = headingFirst;
                lblGrid.Text = "<p class='text-center' style='font-weight:600'>Ledger Name : " + ddlLedger.SelectedItem.ToString() + " [ " + ddlOffice.SelectedItem.ToString() + " ] </p>";
                ds = objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "2", ddlLedger.SelectedValue.ToString(), ddlOffice.SelectedValue.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    divExcel.Visible = true;
                    GridViewRefDetail.DataSource = ds.Tables[0];
                    GridViewRefDetail.DataBind();
                    GridViewRefDetail.UseAccessibleHeader = true;
                    GridViewRefDetail.HeaderRow.TableSection = TableRowSection.TableHeader;

                    decimal BillByBillTx_Amount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BillByBillTx_Amount"));

                    GridViewRefDetail.FooterRow.Cells[2].Text = "<b>Grand Total</b>";
                    if (BillByBillTx_Amount < 0)
                    {
                        GridViewRefDetail.FooterRow.Cells[3].Text = "<b>" + Math.Abs(BillByBillTx_Amount).ToString() + " Dr" + "</b>";
                    }
                    else
                    {
                        GridViewRefDetail.FooterRow.Cells[3].Text = "<b>" + Math.Abs(BillByBillTx_Amount).ToString() + " Cr" + "</b>";
                    }
                    GridViewRefDetail.FooterRow.Cells[3].CssClass = "align-right";

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;

                    lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
                }
                else
                {
                    lblGrid.Text = "Ledger Name : " + ddlLedger.SelectedItem.ToString() + " (<span style='color:red;'>No Record Found.</span>)";
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

}
