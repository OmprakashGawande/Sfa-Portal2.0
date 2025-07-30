using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayRollPayBillMonth_Wise_HO : System.Web.UI.Page
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
                try
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();

                    //ds = objdb.ByProcedure("SpAdminOffice",
                    //       new string[] { "flag" },
                    //       new string[] { "10" }, "dataset");
                    //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    //{
                    //    ddlOffice.DataSource = ds;
                    //    ddlOffice.DataTextField = "Office_Name";
                    //    ddlOffice.DataValueField = "Office_ID";
                    //    ddlOffice.DataBind();
                    //    ddlOffice.Items.Insert(0, new ListItem("All", "0"));

                    //}

                    ddlOffice.SelectedValue = "1";
                    ddlOffice.Enabled = false;
                    btnbankPaybil.Visible = false;
                    btnBankwise.Visible = false;
                    FillDropdown();
                    FillBank();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
               
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
            lblMsg.Text = "";
            btnbankPaybil.Visible = false;
            btnBankwise.Visible=false;
            StringBuilder htmlStr = new StringBuilder("");
            ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "Salary_Month", "Emp_TypeOfPost","Bank" }, new string[] { "7", ddlOffice.SelectedValue.ToString(), ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlEmp_TypeOfPost.SelectedValue.ToString(),ddlBank_Name.SelectedValue }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                htmlStr.Append("<table class='table table-hover table-bordered pagination-ys' id='EpfTable'><thead>");
                htmlStr.Append("<tr> <th colspan='8' style='font-size:16px; text-align:center; font-weight:700;'>SFA Technologies Pvt. Ltd.    SFA Tower 28-Sector A, Kasturba Nagar, Chetak Bridge, Bhopal - 462023, India</th> </tr>");
                //htmlStr.Append("<tr><th colspan='8' style='font-size:16px; text-align:center; font-weight:700;'>PANCHANAN, 3rd FLOOR , MALVIYA NAGAR BHOPAL</th></tr>");
                htmlStr.Append("<tr> <th colspan='8' style='font-size:16px; text-align:center; font-weight:700;'>BANK WISE DETAIL OF "+ddlMonth.SelectedItem.ToString() + " "+ ddlFinancialYear.SelectedItem.ToString()+" </th></tr>");
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
                htmlStr.Append("<td style='font-weight: 700;'>TOTAL Rs.</td>");
                htmlStr.Append("<td style='font-weight: 700;'>" + ds.Tables[2].Rows[0]["GrandTotal"].ToString() + "</td>");
                htmlStr.Append("<td style='font-weight: 700;'>" + ds.Tables[2].Rows[0]["GrandTotal"].ToString() + "</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td colspan='3'></td>");
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td colspan='8'>&nbsp;</td>");
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td colspan='8' style='font-weight: 700;text-align: center;font-size: 17px;'>(" + GenerateWordsinRs(ds.Tables[2].Rows[0]["GrandTotal"].ToString()) + ")</td>");

                htmlStr.Append("</tr>");

                htmlStr.Append("<tr><td colspan='8' style='height:30px; border:none;'>&nbsp;</td></tr>");
                htmlStr.Append("<tr><td colspan='8' style='height:30px; border:none;'>&nbsp;</td></tr>");
                htmlStr.Append("<tr><td colspan='8' style='height:30px; border:none;'>&nbsp;</td></tr>");


                htmlStr.Append("<tr><td colspan='2' style='font-size:16px; text-align:center; font-weight:700;'>DY MANAGER (A/CS)</th>");
                htmlStr.Append("<td >&nbsp;</th>");
               // htmlStr.Append("<td colspan='2' style='font-size:16px; text-align:center; font-weight:700;'>DGM (ACCTS)</th>");
                htmlStr.Append("<td colspan='2' style='font-size:16px; text-align:center; font-weight:700;'></th>");
                htmlStr.Append("<td>&nbsp;</th>");
                htmlStr.Append("<td colspan='2' style='font-size:16px; text-align:center; font-weight:700;'>GM(ACCTS)</th></tr>");
                htmlStr.Append("</tbody>");
                htmlStr.Append("</table>");

                btnbankPaybil.Visible = true;
               // btnBankwise.Visible = true;

                string link = "PayrollCoverLetter.aspx?Office_ID=" + ddlOffice.SelectedValue.ToString() + "&Salary_Year=" + ddlFinancialYear.SelectedValue.ToString() + "&Salary_Month=" + ddlMonth.SelectedValue.ToString() + "&Emp_TypeOfPost=" + ddlEmp_TypeOfPost.SelectedValue.ToString();
                btnBankwise.NavigateUrl = link;

                string link1 = "PayrollBankWisePaybill.aspx?Office_ID=" + ddlOffice.SelectedValue.ToString() + "&Salary_Year=" + ddlFinancialYear.SelectedValue.ToString() + "&Salary_Month=" + ddlMonth.SelectedValue.ToString() + "&Emp_TypeOfPost=" + ddlEmp_TypeOfPost.SelectedValue.ToString() + "&Bank=" + ddlBank_Name.SelectedValue.ToString();
                btnbankPaybil.NavigateUrl = link1;
            }

            DivTable.InnerHtml = htmlStr.ToString();
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
            btnBankwise.NavigateUrl = "";
            btnbankPaybil.NavigateUrl = "";
            if (ddlFinancialYear.SelectedIndex > 0 && ddlOffice.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0 && ddlEmp_TypeOfPost.SelectedIndex > 0)
            {
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
    //protected void btnBankwise_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlFinancialYear.SelectedIndex > 0 && ddlOffice.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
    //        {
    //            Response.Redirect();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
}