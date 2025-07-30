using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayRollArrearEpfDedMonthWise : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ddlOffice.Enabled = false;
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    ddlOffice.Enabled = true;
                }
                DivTextFileExport.Visible = false;
                FillDropdown();
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
            ds = objdb.ByProcedure("SpAdminOffice",
                       new string[] { "flag" },
                       new string[] { "10" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                if (ViewState["Office_ID"].ToString() != "1")
                {
                    ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                }

            }

            ds.Reset();
            ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "8" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlFinancialYear.DataSource = ds;
                ddlFinancialYear.DataTextField = "Year";
                ddlFinancialYear.DataValueField = "Year";
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            DivTextFileExport.Visible = false;
            ds = objdb.ByProcedure("SpPayrollEmpArrear", new string[] { "flag", "CurrentYear", "CurrentMonth", "Office_ID" },
                new string[] { "14", ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOffice.SelectedValue.ToString() }, "dataset"); //"4"
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
                DivTextFileExport.Visible = true;

                decimal? TotalEarning = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("ARREAR_EPF_WAGES"));
                decimal? ARREAR_EPS_WAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("ARREAR_EPS_WAGES"));
                decimal? ARREAR_EDLI_WAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("ARREAR_EDLI_WAGES"));
                decimal? EPF_Deduction = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("ARREAR_EPF_EE_SHARE"));
                decimal? ARREAR_EPF_ER_SHARE = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("ARREAR_EPF_ER_SHARE"));
                decimal? ARREAR_EPS_SHARE = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("ARREAR_EPS_SHARE"));

                GridView1.FooterRow.Cells[1].Text = "| TOTAL |";
                GridView1.FooterRow.Cells[5].Text = TotalEarning.ToString();
                GridView1.FooterRow.Cells[6].Text = ARREAR_EPS_WAGES.ToString();
                GridView1.FooterRow.Cells[7].Text = ARREAR_EDLI_WAGES.ToString();
                GridView1.FooterRow.Cells[8].Text = EPF_Deduction.ToString();
                GridView1.FooterRow.Cells[9].Text = ARREAR_EPF_ER_SHARE.ToString();
                GridView1.FooterRow.Cells[10].Text = ARREAR_EPS_SHARE.ToString();
            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
                DivTextFileExport.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnTextFileExport_Click(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByProcedure("SpPayrollEmpArrear", new string[] { "flag", "CurrentYear", "CurrentMonth", "Office_ID" },
                 new string[] { "14", ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOffice.SelectedValue.ToString() }, "dataset"); //"4"
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Write(ds.Tables[1]);

                //GridView1.DataSource = ds.Tables[0];
                //GridView1.DataBind();
                //DivTextFileExport.Visible = true;
                //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                //GridView1.UseAccessibleHeader = true;
            }
            else
            {
                //GridView1.DataSource = ds.Tables[0];
                //GridView1.DataBind();
                //DivTextFileExport.Visible = true;
                //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                //GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Write(DataTable dt)
    {
        string txt = string.Empty;

        //foreach (DataColumn column in dt.Columns)
        //{
        //    //Add the Header row for Text file.
        //    txt += column.ColumnName + "\t\t";
        //}

        //Add new line.
        // txt += "\r\n";

        foreach (DataRow row in dt.Rows)
        {
            int i = 1;
            int ColCount = dt.Columns.Count;
            foreach (DataColumn column in dt.Columns)
            {
                //Add the Data rows.
                txt += row[column.ColumnName].ToString();
                if (i < ColCount)
                {
                    txt += "#~#";
                }
                i++;
            }

            //Add new line.
            txt += "\r\n";
        }

        //Download the Text file.
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=ARREAR_EPF-" + DateTime.Now.ToString("MM-dd-yyyy") + ".txt");
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(txt);
        Response.Flush();
        Response.End();

    }
}