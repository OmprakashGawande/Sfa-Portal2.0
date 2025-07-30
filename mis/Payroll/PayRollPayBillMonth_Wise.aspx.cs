using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Linq;
using System.Text;

public partial class mis_Payroll_PayRollPayBillMonth_Wise : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
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

                    ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                    //if (ddlOfficeName.SelectedIndex > 0)
                    //{
                    //    FillEmployee();
                    //}

                }
                FillDropdown();
                FillBank();
                FillMonth();

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
            StringBuilder sb = new StringBuilder();
            lblMsg.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            btnPrint.Visible = false;
            double SubTotal = 0.00;
            // Create DataTable  
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[8] 
            { new DataColumn("Salary_NetSalary", typeof(string)),
              new DataColumn("SubTotal",typeof(string)),
              new DataColumn("Bank_IfscCode",typeof(string)),
              new DataColumn("Bank_AccountNo",typeof(string)),
              new DataColumn("Bank_AccountNo1",typeof(string)),
              new DataColumn("Emp_Name",typeof(string)),
              new DataColumn("Bank_Name",typeof(string)),
              //new DataColumn("Address",typeof(string)),
              new DataColumn("SNo",typeof(string))
              
            });
            ds = null;
            ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "Salary_Month", "Emp_TypeOfPost", "Bank" }, new string[] { "4", ddlOffice.SelectedValue.ToString(), ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedItem.Text.ToString(), ddlEmp_TypeOfPost.SelectedValue.ToString(), ddlBank_Name.SelectedValue }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //GridView1.DataSource = ds.Tables[0];
                //GridView1.DataBind();
                btnPrint.Visible = true;
                // Insert Row in DataTable
                int dsRowcount = ds.Tables[0].Rows.Count;
                double val123 = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string SubTotalVal = "";
                    SubTotal = SubTotal + double.Parse(ds.Tables[0].Rows[i]["Salary_NetSalary"].ToString());
                    val123 = val123 + double.Parse(ds.Tables[0].Rows[i]["Salary_NetSalary"].ToString());
                    if (i + 1 < dsRowcount)
                    {
                        if (ds.Tables[0].Rows[i]["Bank_IfscCode"].ToString() != ds.Tables[0].Rows[i + 1]["Bank_IfscCode"].ToString())
                        {
                            SubTotalVal = Math.Round(SubTotal, 2).ToString();
                            SubTotal = 0.00;
                        }
                    }
                    else
                    {
                        SubTotalVal = Math.Round(SubTotal, 2).ToString();
                        SubTotal = 0.00;
                    }
                    //if (SubTotalVal.ToString() =="")
                    //{
                    //    SubTotalVal = null;
                    //}

                    dt.Rows.Add(Math.Round(double.Parse(ds.Tables[0].Rows[i]["Salary_NetSalary"].ToString())).ToString()
                        , SubTotalVal.ToString().Trim()
                        , ds.Tables[0].Rows[i]["Bank_IfscCode"].ToString()
                        , "" + ds.Tables[0].Rows[i]["Bank_AccountNo"].ToString()
                        , "'" + ds.Tables[0].Rows[i]["Bank_AccountNo1"].ToString() + "'"
                        , ds.Tables[0].Rows[i]["Emp_Name"].ToString()
                        , ds.Tables[0].Rows[i]["Bank_Name"].ToString()
                        //,"BHOPAL"
                        , (i + 1).ToString()
                        );

                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.Columns[2].Visible = true;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    decimal NetSalary = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Salary_NetSalary"));
                    ViewState["NetSalary"] = NetSalary.ToString();
                    GridView1.FooterRow.Cells[0].Text = "| TOTAL |";
                    GridView1.FooterRow.Cells[1].Text = "" + NetSalary.ToString() + "";
                    GridView1.FooterRow.Cells[2].Text = "" + val123.ToString() + "";
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    GridView1.Columns[2].Visible = true;
                    if (ddlEmp_TypeOfPost.SelectedValue.ToString() != "Permanent")
                    {
                        //GridView1.Columns[2].Visible = false;
                    }
                    dt = null;
                }

                sb.Append("<table class='table' style='width=100%'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='text-align:center;'><h3>SFA Technologies Pvt. Ltd.    SFA Tower 28-Sector A, Kasturba Nagar, Chetak Bridge, Bhopal - 462023, India</h3></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'>NO./MFP/ACCOUNT/</td>");
                sb.Append("<td>Bhopal, Dt.</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4'>To,.</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='padding-left:30px;'><b>Branch Manager,</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='padding-left:30px;'>" + ddlBank_Name.SelectedItem.Text + ",</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='padding-left:30px;'>Bhopal.</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4'>Sub :- Regarding deposit of Salary " + ddlMonth.SelectedItem.Text + "," + ddlFinancialYear.SelectedValue + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4'>Sir,</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='padding-left:40px;'><div style='overflow-wrap: break-word; inline-size: 150px;'>The enclosed Cheque No.<span style='padding-left:70px;'></span>           Dt<span style='padding-left:30px;'></span>  /<span style='padding-left:30px;'></span>   /<span style='padding-left:50px;'></span>for Rs " + ViewState["NetSalary"] + " may kindly be credited inthe Saving Account of the following</br> Officers/Officials of this Federation as per amount shown againts their Account Numbers.</div></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table' style='width=100%'>");
                sb.Append("<thead class='header'>");
                sb.Append("<tr>");
                sb.Append("<th style='border-top:1px solid black; border-bottom:1px solid black;' width='10%'>Sr.No.</th>");
                sb.Append("<th style='border-top:1px solid black; border-bottom:1px solid black;' >NAME OF OFFICERS/OFFICIALS</th>");
                sb.Append("<th style='border-top:1px solid black; border-bottom:1px solid black;' >A/C No.</th>");
                sb.Append("<th style='border-top:1px solid black; border-bottom:1px solid black;' >AMOUNT</th>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                int count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='border-top:1px solid black; border-bottom:1px solid black;'  width='10%'>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td style='border-top:1px solid black; border-bottom:1px solid black;' >" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + "</td>");
                    sb.Append("<td style='border-top:1px solid black; border-bottom:1px solid black;' >" + ds.Tables[0].Rows[i]["Bank_AccountNo"].ToString() + "</td>");
                    sb.Append("<td style='border-top:1px solid black; border-bottom:1px solid black;' >" + ds.Tables[0].Rows[i]["Salary_NetSalary"].ToString() + "</td>");
                    sb.Append("</tr>");
                }
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='border-top:1px solid black; border-bottom:1px solid black;'><b>TOTAL</b></td>");
                sb.Append("<td style='border-top:1px solid black; border-bottom:1px solid black;'><b>" + ViewState["NetSalary"].ToString() + "</b></td>");
                sb.Append("</tr>");

                string Amount = GenerateWordsinRs(ViewState["NetSalary"].ToString());
                sb.Append("<tr>");
                sb.Append("<td colspan='4'><b>(" + Amount + ")</b></td>");

                sb.Append("</tr>");

                sb.Append("</table>");

                //GridView1.DataSource = dt;
                //GridView1.DataBind();
                //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                //GridView1.UseAccessibleHeader = true;

                //GridView1.Columns[2].Visible = true;
                //if (ddlEmp_TypeOfPost.SelectedValue.ToString() != "Permanent")
                //{
                //    GridView1.Columns[2].Visible = false;
                //}

            }
            //else if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
            //{
            //    GridView1.DataSource = ds.Tables[0];
            //    GridView1.DataBind();
            //}

            printsection.InnerHtml = sb.ToString();




        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillBank()
    {
        try
        {
            ddlBank_Name.Items.Clear();
            ds = objdb.ByProcedure("SpHRBankDetail", new string[] { "flag" }, new string[] { "4" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlBank_Name.DataSource = ds;
                ddlBank_Name.DataTextField = "BankName";
                ddlBank_Name.DataValueField = "Bank_id";
                ddlBank_Name.DataBind();


            }
            ddlBank_Name.Items.Insert(0, new ListItem("All", "0"));
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
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ddlFinancialYear.SelectedIndex > 0 && ddlOffice.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
            {
                FillGrid();
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillMonth()
    {
        try
        {
            ddlMonth.Items.Clear();
            for (int i = 12; i >= 1; i--)
            {
                DateTime date = new DateTime(DateTime.Now.Year, i, 1);
                ddlMonth.Items.Insert(0, new ListItem(date.ToString("MMMM"), i.ToString()));
            }
            ddlMonth.DataBind();
            ddlMonth.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            // lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    private string GenerateWordsinRs(string value)
    {
        decimal numberrs = Convert.ToDecimal(value);
        CultureInfo ci = new CultureInfo("en-IN");
        string aaa = String.Format("{0:#,##0.##}", numberrs);
        aaa = aaa + " " + ci.NumberFormat.CurrencySymbol.ToString();
        // label6.Text = aaa;


        string input = value;
        string a = "";
        string b = "";

        // take decimal part of input. convert it to word. add it at the end of method.
        string decimals = "";

        if (input.Contains("."))
        {
            decimals = input.Substring(input.IndexOf(".") + 1);
            // remove decimal part from input
            input = input.Remove(input.IndexOf("."));

        }
        string strWords = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(input));

        if (!value.Contains("."))
        {
            a = strWords + " Rupees Only";
        }
        else
        {
            a = strWords + " Rupees";
        }

        if (decimals.Length > 0)
        {
            // if there is any decimal part convert it to words and add it to strWords.
            string strwords2 = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(decimals));
            b = " and " + strwords2 + " Paisa Only ";
        }

        return a + b;
    }
}
