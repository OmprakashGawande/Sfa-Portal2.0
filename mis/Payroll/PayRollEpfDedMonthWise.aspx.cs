using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Linq;

public partial class mis_Payroll_PayRollPayBillMonth_Wise : System.Web.UI.Page
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

                }
                //DivTextFileExport.Visible = false;
                btnTextFileExportNew.Visible = false;
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
    protected void FillGrid()
    {
        try
        {
            // by pawan on 13-01-2023
            string postid = "", multiTypeofPost = "";
            int postdata = 0;
            foreach (ListItem itemss in ddlEmp_TypeOfPost.Items)
            {
                if (itemss.Selected)
                {

                    postid = itemss.Value;

                    ++postdata;
                    if (postdata == 1)
                    {
                        multiTypeofPost = postid;

                    }
                    else
                    {
                        multiTypeofPost += "," + postid;

                    }
                }
            }

            if (postdata > 0)
            {
                // end of by pawan on  13-01-2023
                GridView1.DataSource = null;
                GridView1.DataBind();
                //DivTextFileExport.Visible = false;
                btnTextFileExportNew.Visible = false;
                //ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "SalaryYear", "SalaryMonth", "Office_ID", "Emp_TypeOfPost" }, new string[] { "1", ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOffice.SelectedValue.ToString(), ddlEmp_TypeOfPost.SelectedValue }, "dataset"); by pawan on 
                ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "SalaryYear", "SalaryMonth", "Office_ID", "multiTypeofPost" }, new string[] { "1", ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOffice.SelectedValue.ToString(), multiTypeofPost }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    //DivTextFileExport.Visible = true;
                    btnTextFileExportNew.Visible = true;
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;

                    decimal Salary_EarningTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Salary_EarningTotal"));
                    decimal EPFWAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPFWAGES"));
                    decimal EPS_WAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPS_WAGES"));
                    decimal EDLI_WAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EDLI_WAGES"));
                    decimal EPF = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPF"));
                    decimal EPS_CONTRI_REMITTED = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPS_CONTRI_REMITTED"));
                    decimal EPF_EPS_DIFF_REMITTED = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPF_EPS_DIFF_REMITTED"));
                    decimal NCP_DAYS = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("NCP_DAYS"));
                    decimal REFUND_OF_ADVANCES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("REFUND_OF_ADVANCES"));


                    GridView1.FooterRow.Cells[1].Text = "| TOTAL | ";
                    GridView1.FooterRow.Cells[5].Text = Salary_EarningTotal.ToString();
                    GridView1.FooterRow.Cells[6].Text = EPFWAGES.ToString();
                    GridView1.FooterRow.Cells[7].Text = EPS_WAGES.ToString();
                    GridView1.FooterRow.Cells[8].Text = EDLI_WAGES.ToString();
                    GridView1.FooterRow.Cells[9].Text = EPF.ToString();
                    GridView1.FooterRow.Cells[10].Text = EPS_CONTRI_REMITTED.ToString();
                    GridView1.FooterRow.Cells[11].Text = EPF_EPS_DIFF_REMITTED.ToString();
                    GridView1.FooterRow.Cells[12].Text = NCP_DAYS.ToString();
                    GridView1.FooterRow.Cells[13].Text = REFUND_OF_ADVANCES.ToString();

                }
                else if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    //DivTextFileExport.Visible = true;
                    btnTextFileExportNew.Visible = true;
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }


            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning!", "Type of Post (पद प्रकार)");
                return;
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
            //if (ddlFinancialYear.SelectedIndex > 0 && ddlOffice.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
            //{
                FillGrid();
            //}
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
            ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "SalaryYear", "SalaryMonth", "Office_ID" }, new string[] { "1", ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOffice.SelectedValue.ToString() }, "dataset");
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
                if(i<ColCount)
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
        Response.AddHeader("content-disposition", "attachment;filename=EpfReport-"+DateTime.Now.ToString("MM-dd-yyyy")+".txt");
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(txt);
        Response.Flush();
        Response.End();

    }
    protected void btnTextFileExportNew_Click(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "SalaryYear", "SalaryMonth", "Office_ID", "Emp_TypeOfPost" }, new string[] { "1", ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOffice.SelectedValue.ToString(), ddlEmp_TypeOfPost.SelectedValue }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                WriteNew(ds.Tables[3]);
            }
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void WriteNew(DataTable dt)
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
        Response.AddHeader("content-disposition", "attachment;filename=EpfReport-" + DateTime.Now.ToString("MM-dd-yyyy") + ".txt");
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(txt);
        Response.Flush();
        Response.End();

    }
}