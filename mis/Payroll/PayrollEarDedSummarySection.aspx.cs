using System;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollEarDedSummarySection : System.Web.UI.Page
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
                ddlOffice.Enabled = false;
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    ddlOffice.Enabled = true;
                }
                ds = objdb.ByProcedure("SpAdminOffice",
                       new string[] { "flag" },
                       new string[] { "1" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOffice.DataSource = ds;
                    ddlOffice.DataTextField = "Office_Name";
                    ddlOffice.DataValueField = "Office_ID";
                    ddlOffice.DataBind();
                    ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                    ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                    ddlOffice.Enabled = false;
                }
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

            ddlSection.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpPayrollSalaryFinalSummary", new string[] { "flag", "Office_ID" }, new string[] { "4", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                ddlSection.DataSource = ds;
                ddlSection.DataTextField = "SalarySec_No";
                ddlSection.DataValueField = "SalarySec_No";
                ddlSection.DataBind();
                //ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
            }


        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void FillDetail()
    {
        try
        {

            string Office = "";
            //string OfficeName = "";
            int SerialNoOffice = 0;
            int totalListItem = ddlSection.Items.Count;
            foreach (ListItem item in ddlSection.Items)
            {

                if (item.Selected)
                {
                    SerialNoOffice++;
                    Office += item.Value + ",";
                    //OfficeName += " <span style='color:tomato;'>" + SerialNoOffice + ".</span>" + item.Text + " ,";
                }
            }



            ReportSection.Visible = true;
            ds = objdb.ByProcedure("SpPayrollSalaryFinalSummary",
                new string[] { "flag", "Office_ID", "Year", "MonthNo", "SalarySection_No", "Emp_TypeOfPost" },
                new string[] { "3", ddlOffice.SelectedValue.ToString(), 
                    ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), 
                    Office.ToString(),ddlEmp_TypeOfPost.SelectedValue }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblOfficeName.Text = ds.Tables[0].Rows[0]["OfficeName"].ToString();
                    lblFinancialYear.InnerHtml = ds.Tables[0].Rows[0]["FinancialYear"].ToString();
                    lblMonth.InnerHtml = ds.Tables[0].Rows[0]["Month"].ToString();
                    lblSalary_Basic.Text = ds.Tables[0].Rows[0]["BasicSalary"].ToString();
                    //lblPolicyDeduction.Text = ds.Tables[0].Rows[0]["PolicyDeduction"].ToString();
                    lblSalary_GrossSalary.Text = ds.Tables[0].Rows[0]["GrossSalary"].ToString();
                    lblSalary_DeductionTotal.Text = ds.Tables[0].Rows[0]["TotalDeduction"].ToString();
                    lblSalary_NetAmount.Text = ds.Tables[0].Rows[0]["NetSalary"].ToString();
                    lblGross.Text = GenerateWordsinRs(ds.Tables[0].Rows[0]["GrossSalary"].ToString());
                    lblNet.Text = GenerateWordsinRs(ds.Tables[0].Rows[0]["NetSalary"].ToString());
                    lblDeduction.Text = GenerateWordsinRs(ds.Tables[0].Rows[0]["TotalDeduction"].ToString());
                    //GenerateWordsinRs
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    RepeaterEarning.DataSource = ds.Tables[1];
                    RepeaterEarning.DataBind();
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    RepeaterDeduction.DataSource = ds.Tables[2];
                    RepeaterDeduction.DataBind();
                }
            }


            /********************/
            //GridMismatchDetail.DataSource = null;
            //GridMismatchDetail.DataBind();

            //ds = objdb.ByProcedure("SpPayrollSalaryFinalSummary",
            //    new string[] { "flag", "Office_ID", "Year", "MonthNo" },
            //    new string[] { "2", ddlOffice.SelectedValue.ToString(), ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString() }, "dataset");
            //int TotalMismatch = 0;
            //if (ds.Tables.Count > 1)
            //{
                
            //    TotalMismatch = int.Parse(ds.Tables[1].Rows[0]["TotalMismatch"].ToString());
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        GridMismatchDetail.DataSource = ds;
            //        GridMismatchDetail.DataBind();
            //    }

            //}

            //if (TotalMismatch > 0)
            //{
            //    lblWarningMessage.Text = "<button type='button' class='btn btn-danger btn-flat btn-block' data-toggle='modal' data-target='#MisMatchDetailModal'>Issue In Salary ( " + TotalMismatch.ToString() + " )</button>";
            //    lblSalary_DeductionTotal.Text = "****************";
            //    lblSalary_GrossSalary.Text = "****************";
            //    lblSalary_NetAmount.Text = "****************";

            //}
            //else
            //{
            //    lblWarningMessage.Text = "";
            //}
            
            /********************/


        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        FillDetail();
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