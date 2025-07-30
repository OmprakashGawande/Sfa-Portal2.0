using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;


public partial class mis_Finance_RptBillWiseDetail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    string currentFY = "";
    decimal subTotal = 0;
    decimal total = 0;
    int subTotalRowIndex = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ddlOffice.Enabled = false;
                    divExcel.Visible = false;
                    FillDropdown();
                    FillGrid();
                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        ddlOffice.Enabled = false;
                    }

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;

                    lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
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
            DataSet dsYear = objdb.ByProcedure("SpFinRptBillByBillRefDetail",
                   new string[] { "flag" },
                   new string[] { "3" }, "dataset");
            if (dsYear.Tables.Count > 0 && dsYear.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = dsYear;
                ddlYear.DataTextField = "BillByBillTx_FY";
                ddlYear.DataValueField = "BillByBillTx_FY";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("All", "All"));
               
            }
            DataSet dsScheme = objdb.ByProcedure("SpFinRptBillByBillRefDetail",
                   new string[] { "flag" },
                   new string[] { "4" }, "dataset");
            if (dsScheme.Tables.Count > 0 && dsScheme.Tables[0].Rows.Count > 0)
            {
                ddlYojna.DataSource = dsScheme;
                ddlYojna.DataTextField = "SchemeTx_Name";
                ddlYojna.DataValueField = "SchemeTx_ID";
                ddlYojna.DataBind();
                ddlYojna.Items.Insert(0, new ListItem("All", "0"));

            }
            DataSet dsGroup = objdb.ByProcedure("SpFinRptBillByBillRefDetail",
                   new string[] { "flag" },
                   new string[] { "5" }, "dataset");
            if (dsGroup.Tables.Count > 0 && dsGroup.Tables[0].Rows.Count > 0)
            {
                ddlGroup.DataSource = dsGroup;
                ddlGroup.DataTextField = "BillByBillTx_ItemGroup";
                ddlGroup.DataValueField = "BillByBillTx_ItemGroup";
                ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, new ListItem("All", "All"));

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
            var watch = System.Diagnostics.Stopwatch.StartNew();

            FillGrid();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewRefDetail_DataBound(object sender, EventArgs e)
    {
        try
        {
            subTotal = 0;
            for (int i = subTotalRowIndex; i < GridViewRefDetail.Rows.Count; i++)
            {
                subTotal += Convert.ToDecimal(GridViewRefDetail.Rows[i].Cells[8].Text);
            }
            if (GridViewRefDetail.Rows.Count > 0)
            {
                this.AddTotalRow("<span style='font-weight:700;font-size:16px;'>योग -</span>", subTotal.ToString("N2"));
                this.AddTotalRow("<span style='font-weight:700;font-size:16px;'>महायोग -</span>", total.ToString("N2"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewRefDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                subTotal = 0;
                DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
                string FY = dt.Rows[e.Row.RowIndex]["BillByBillTx_FY"].ToString();
                total += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["BillByBillTx_Amount"]);
                if (FY.ToString() != currentFY.ToString())
                {
                    if (e.Row.RowIndex > 0)
                    {
                        for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                        {
                            subTotal += Convert.ToDecimal(GridViewRefDetail.Rows[i].Cells[8].Text);
                        }
                        this.AddTotalRow("<span style='font-weight:700;font-size:16px;'>योग -</span>", subTotal.ToString("N2"));
                        subTotalRowIndex = e.Row.RowIndex;
                    }
                    currentFY = FY;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    private void AddTotalRow(string labelText, string value)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        //row.BackColor = ColorTranslator.FromHtml("#F9F9F9");
        row.Cells.AddRange(new TableCell[10] { new TableCell (), //Empty Cell
                                        new TableCell { },
                                        new TableCell { },
                                        new TableCell { },
                                        new TableCell { },
                                        new TableCell { },
                                        new TableCell { },
                                                                               
                                        new TableCell { Text = labelText},
                                        new TableCell { Text = value,CssClass="aa" },
                                        new TableCell { }});


        GridViewRefDetail.Controls[0].Controls.Add(row);

    }
    protected void FillGrid()
    {
        try
        {
            lblheadingFirst.Text = "";
            lblMsg.Text = "";
            lblGrid.Text = "";
            GridViewRefDetail.DataSource = null;
            GridViewRefDetail.DataBind();
            if (ddlOffice.SelectedIndex > 0)
            {

                ds = objdb.ByProcedure("SpFinRptBillByBillRefDetail", new string[] { "flag", "Office_ID", "BillByBillTx_FY", "BillByBillTx_ItemGroup", "SchemeTx_ID" }, new string[] { "6", ddlOffice.SelectedValue.ToString(),ddlYear.SelectedValue.ToString(),ddlGroup.SelectedValue.ToString(),ddlYojna.SelectedValue.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    divExcel.Visible = true;
                    GridViewRefDetail.DataSource = ds.Tables[0];
                    GridViewRefDetail.DataBind();
                    string headingFirst = "<p class='text-center' style='font-weight:600'> MP State Agro Industries Development Corporation, <br/> [District -" + ddlOffice.SelectedItem.Text + " ] <br />  </p>";
                    lblheadingFirst.Text = headingFirst;
                }
                else
                {
                    GridViewRefDetail.DataSource = new string[] { };
                    GridViewRefDetail.DataBind();
                }
                GridViewRefDetail.UseAccessibleHeader = true;
                GridViewRefDetail.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

}