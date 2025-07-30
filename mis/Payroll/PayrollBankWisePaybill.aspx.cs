using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;


public partial class mis_Payroll_PayrollBankWisePaybill : System.Web.UI.Page
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
                if (Request.QueryString["Office_ID"] == null && Request.QueryString["Salary_Year"] == null && Request.QueryString["Salary_Month"] == null && Request.QueryString["Emp_TypeOfPost"] == null && Request.QueryString["Bank"].ToString() == null)
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
                StringBuilder htmlStr = new StringBuilder("");

                lblyear.Text = (Request.QueryString["Salary_Month"].ToString() + " " + Request.QueryString["Salary_Year"].ToString());
                ds = objdb.ByProcedure("SpPayrollSalaryDetail",
                    new string[] { "flag", "Office_ID", "Salary_Year", "Salary_Month", "Emp_TypeOfPost","Bank"},
                    new string[] { "7", Request.QueryString["Office_ID"].ToString(), Request.QueryString["Salary_Year"].ToString(), Request.QueryString["Salary_Month"].ToString(), Request.QueryString["Emp_TypeOfPost"].ToString(), Request.QueryString["Bank"].ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    htmlStr.Append("<table style='width:1200px;' class='table table-hover table-bordered pagination-ys' id='EpfTable'><thead>");
                    //htmlStr.Append("<tr> <th colspan='8' style='font-size:16px; text-align:center; font-weight:700;'>BANK WISE DETAIL OF " + ddlMonth.SelectedItem.ToString() + " " + ddlFinancialYear.SelectedItem.ToString() + " </th></tr>");
                    htmlStr.Append(" <tr><th style='width: 5%;'>SNo.</th><th>NAME OF BANK </th><th>AMOUNT</th><th style='width: 10%;'></th><th>I F S C CODE</th><th style='width: 10%;'>CHQ No.</th><th style='width: 10%;'>DATE </th><th style='width: 10%;'>AMOUNT </th></tr></thead>");
                    htmlStr.Append("<tbody>");

                    int GroupNo = 0;
                    int k = 0;
                    int dsrowcount = ds.Tables[0].Rows.Count;
                    int dsrowcount1 = ds.Tables[1].Rows.Count;
                    for (int i = 0; i < dsrowcount; i++)
                    {
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td>" + (i + 1).ToString() + "</td>");
                        htmlStr.Append(" <td>" + ds.Tables[0].Rows[i]["Bank_Name"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Salary_NetSalary"].ToString() + "</td>");

                        if (GroupNo < dsrowcount1)
                        {
                            if (ds.Tables[0].Rows[i]["GroupNo"].ToString() == ds.Tables[1].Rows[GroupNo]["GroupNo"].ToString())
                            {
                                htmlStr.Append("<td style='font-weight: 700; vertical-align: middle;' rowspan='" + ds.Tables[1].Rows[GroupNo]["CountBank"].ToString() + "'>" + ds.Tables[1].Rows[GroupNo]["SubTotal"].ToString() + "</td>");
                                GroupNo = GroupNo + 1;
                            }
                        }

                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Bank_IfscCode"].ToString() + "</td>");

                        if (k < dsrowcount1)
                        {
                            if (ds.Tables[0].Rows[i]["GroupNo"].ToString() == ds.Tables[1].Rows[k]["GroupNo"].ToString())
                            {
                                htmlStr.Append("<td colspan='3' rowspan='" + ds.Tables[1].Rows[k]["CountBank"].ToString() + "'></td>");
                                k = k + 1;
                            }
                        }
                        htmlStr.Append("</tr>");
                    }

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append(" <td style='font-weight: 700;'>TOTAL Rs.</td>");
                    htmlStr.Append("<td style='font-weight: 700;'>" + ds.Tables[2].Rows[0]["GrandTotal"].ToString() + "</td>");
                    htmlStr.Append("<td style='font-weight: 700;'>" + ds.Tables[2].Rows[0]["GrandTotal"].ToString() + "</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td colspan='3'></td>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    //htmlStr.Append("<td colspan='8'>&nbsp;</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("</tbody>");
                    htmlStr.Append("</table>");
                    htmlStr.Append("<div style='font-weight: 700;text-align: center;font-size: 17px; width:100%; margin-Top:30px; margin-bottom:90px;'>(" + GenerateWordsinRs(ds.Tables[2].Rows[0]["GrandTotal"].ToString()) + ")</div>");
                }

                DivTable.InnerHtml = htmlStr.ToString();
            }
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
    //}
    //else
    //{
    // //   Response.Redirect("~/mis/Login.aspx");
    //}

}