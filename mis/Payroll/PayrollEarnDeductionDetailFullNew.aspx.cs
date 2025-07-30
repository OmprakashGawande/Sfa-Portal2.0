using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollEarnDeductionDetailFullNew : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
   //static decimal Permanent_DARate = 9;
   //static decimal Fixed_DARate = 148;
	//static decimal Permanent_DARate = 12;  // 23Sep2019
	 //static decimal Permanent_DARate = 20;  // 25Oct2021
    // static decimal Permanent_DARate = 31;  // 25Oct2021
    // static decimal Permanent_DARate = 34;  // 06Sept2022
   // static decimal Fixed_DARate = 154; // 23Sep2019
      //static decimal Fixed_DARate = 171; // 24Feb2022 by pawan
     // static decimal Fixed_DARate = 196; // 24Feb2022 by pawan
    static decimal Fixed_NPFRate = 10;// by pawan on 19-01-2023
    static decimal Actual_ESI_Amount = 21000;// by pawan on 19-01-2023
    static double Fixed_ESIRate = 0.75;// by pawan on 19-01-2023

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Emp_ID"] != null)
            {

                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                //lblPermanent_DARate.Text = Permanent_DARate.ToString();
                //lblFixed_DARate.Text = Fixed_DARate.ToString();
                divDetail.Visible = false;
                FillDropdown();
                // FillGrid();

            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
    }
    protected void FillDropdown()
    {
        try
        {
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Financial_Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            }
            ds = null;
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            }
            ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
            ds = null;
           // ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ddlOfficeName.SelectedValue.ToString() }, "dataset");
	    ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "40", ddlOfficeName.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = ds;
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
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


            DataSet dsNew;
            decimal Amount = 0;
            decimal Jan_AmountDA = 0;
            decimal Feb_AmountDA = 0;
            decimal Mar_AmountDA = 0;
            decimal Apr_AmountDA = 0;
            decimal May_AmountDA = 0;
            decimal Jun_AmountDA = 0;
            decimal Jul_AmountDA = 0;
            decimal Aug_AmountDA = 0;
            decimal Sep_AmountDA = 0;
            decimal Oct_AmountDA = 0;
            decimal Nov_AmountDA = 0;
            decimal Dec_AmountDA = 0;

            decimal Jan_AmountEPF = 0;
            decimal Feb_AmountEPF = 0;
            decimal Mar_AmountEPF = 0;
            decimal Apr_AmountEPF = 0;
            decimal May_AmountEPF = 0;
            decimal Jun_AmountEPF = 0;
            decimal Jul_AmountEPF = 0;
            decimal Aug_AmountEPF = 0;
            decimal Sep_AmountEPF = 0;
            decimal Oct_AmountEPF = 0;
            decimal Nov_AmountEPF = 0;
            decimal Dec_AmountEPF = 0;

            decimal Jan_AmountNPF = 0;
            decimal Feb_AmountNPF = 0;
            decimal Mar_AmountNPF = 0;
            decimal Apr_AmountNPF = 0;
            decimal May_AmountNPF = 0;
            decimal Jun_AmountNPF = 0;
            decimal Jul_AmountNPF = 0;
            decimal Aug_AmountNPF = 0;
            decimal Sep_AmountNPF = 0;
            decimal Oct_AmountNPF = 0;
            decimal Nov_AmountNPF = 0;
            decimal Dec_AmountNPF = 0;

            decimal Jan_AmountESI = 0;
            decimal Feb_AmountESI = 0;
            decimal Mar_AmountESI = 0;
            decimal Apr_AmountESI = 0;
            decimal May_AmountESI = 0;
            decimal Jun_AmountESI = 0;
            decimal Jul_AmountESI = 0;
            decimal Aug_AmountESI = 0;
            decimal Sep_AmountESI = 0;
            decimal Oct_AmountESI = 0;
            decimal Nov_AmountESI = 0;
            decimal Dec_AmountESI = 0;

            decimal Jan_AmountBASIC = 0;
            decimal Feb_AmountBASIC = 0;
            decimal Mar_AmountBASIC = 0;
            decimal Apr_AmountBASIC = 0;
            decimal May_AmountBASIC = 0;
            decimal Jun_AmountBASIC = 0;
            decimal Jul_AmountBASIC = 0;
            decimal Aug_AmountBASIC = 0;
            decimal Sep_AmountBASIC = 0;
            decimal Oct_AmountBASIC = 0;
            decimal Nov_AmountBASIC = 0;
            decimal Dec_AmountBASIC = 0;

            decimal Jan_AmountLeave = 0;
            decimal Feb_AmountLeave = 0;
            decimal Mar_AmountLeave = 0;
            decimal Apr_AmountLeave = 0;
            decimal May_AmountLeave = 0;
            decimal Jun_AmountLeave = 0;
            decimal Jul_AmountLeave = 0;
            decimal Aug_AmountLeave = 0;
            decimal Sep_AmountLeave = 0;
            decimal Oct_AmountLeave = 0;
            decimal Nov_AmountLeave = 0;
            decimal Dec_AmountLeave = 0;



            decimal Deduction_PTax = 0;
            /***********************************/
            decimal Deduction_MiscDed = 0;
            long Emp_BasicSalery;
            /***********************************/

            string Year_2 = (Int32.Parse(ddlYear.SelectedValue.ToString()) + 1).ToString();


            // dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", Year_2, ddlEmployee.SelectedValue.ToString(), "1" }, "dataset");
            // if (dsNew.Tables.Count > 0)
            // {
                // if (dsNew.Tables[0].Rows.Count > 0)
                // {
                    // Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    // if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        // Jan_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    // else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        // Jan_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    // Jan_AmountEPF = Math.Round(((Amount + Jan_AmountDA) * 12) / 100);
                // }
            // }
            // dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", Year_2, ddlEmployee.SelectedValue.ToString(), "2" }, "dataset");
            // if (dsNew.Tables.Count > 0)
            // {
                // if (dsNew.Tables[0].Rows.Count > 0)
                // {
                    // Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    // if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        // Feb_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    // else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        // Feb_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    // Feb_AmountEPF = Math.Round(((Amount + Feb_AmountDA) * 12) / 100);
                // }
            // }
            // dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", Year_2, ddlEmployee.SelectedValue.ToString(), "3" }, "dataset");
            // if (dsNew.Tables.Count > 0)
            // {
                // if (dsNew.Tables[0].Rows.Count > 0)
                // {
                    // Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    // if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        // Mar_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    // else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        // Mar_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    // Mar_AmountEPF = Math.Round(((Amount + Mar_AmountDA) * 12) / 100);

                // }

            // }
            // dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "4" }, "dataset");
            // if (dsNew.Tables.Count > 0)
            // {
                // if (dsNew.Tables[0].Rows.Count > 0)
                // {
                    // Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());


                    // if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        // Apr_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    // else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        // Apr_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    // Apr_AmountEPF = Math.Round(((Amount + Apr_AmountDA) * 12) / 100);
                // }
            // }
            // dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "5" }, "dataset");
            // if (dsNew.Tables.Count > 0)
            // {
                // if (dsNew.Tables[0].Rows.Count > 0)
                // {
                    // Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    // if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        // May_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    // else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        // May_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    // May_AmountEPF = Math.Round(((Amount + May_AmountDA) * 12) / 100);
                // }
            // }
            // dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "6" }, "dataset");
            // if (dsNew.Tables.Count > 0)
            // {
                // if (dsNew.Tables[0].Rows.Count > 0)
                // {
                    // Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    // if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        // Jun_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    // else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        // Jun_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    // Jun_AmountEPF = Math.Round(((Amount + Jun_AmountDA) * 12) / 100);
                // }
            // }
            // dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "7" }, "dataset");
            // if (dsNew.Tables.Count > 0)
            // {
                // if (dsNew.Tables[0].Rows.Count > 0)
                // {
                    // Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    // if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        // Jul_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    // else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        // Jul_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    // Jul_AmountEPF = Math.Round(((Amount + Jul_AmountDA) * 12) / 100);
                // }
            // }
            // dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "8" }, "dataset");
            // if (dsNew.Tables.Count > 0)
            // {
                // if (dsNew.Tables[0].Rows.Count > 0)
                // {
                    // Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    // if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        // Aug_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    // else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        // Aug_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    // Aug_AmountEPF = Math.Round(((Amount + Aug_AmountDA) * 12) / 100);
                // }
            // }
            // dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "9" }, "dataset");
            // if (dsNew.Tables.Count > 0)
            // {
                // if (dsNew.Tables[0].Rows.Count > 0)
                // {
                    // Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    // if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        // Sep_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    // else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        // Sep_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    // Sep_AmountEPF = Math.Round(((Amount + Sep_AmountDA) * 12) / 100);
                // }
            // }
            // dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "10" }, "dataset");
            // if (dsNew.Tables.Count > 0)
            // {
                // if (dsNew.Tables[0].Rows.Count > 0)
                // {
                    // Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    // if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        // Oct_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    // else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        // Oct_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    // Oct_AmountEPF = Math.Round(((Amount + Oct_AmountDA) * 12) / 100);
                // }
            // }
            // dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "11" }, "dataset");
            // if (dsNew.Tables.Count > 0)
            // {
                // if (dsNew.Tables[0].Rows.Count > 0)
                // {
                    // Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    // if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        // Nov_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    // else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        // Nov_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    // Nov_AmountEPF = Math.Round(((Amount + Nov_AmountDA) * 12) / 100);
                // }
            // }
            // dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "12" }, "dataset");
            // if (dsNew.Tables.Count > 0)
            // {
                // if (dsNew.Tables[0].Rows.Count > 0)
                // {
                    // Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    // if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        // Dec_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    // else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        // Dec_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    // Dec_AmountEPF = Math.Round(((Amount + Dec_AmountDA) * 12) / 100);
                // }
            // }
            decimal Permanent_DARate = 0;
            decimal Fixed_DARate = 0;
			 dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo","MonthName" }, new string[] { "2", Year_2, ddlEmployee.SelectedValue.ToString(), "1","January"}, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Permanent_DARate = 0;
                    Fixed_DARate = 0;
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        Permanent_DARate = decimal.Parse(dsNew.Tables[1].Rows[0]["Permanent_DARate"].ToString());
                    }
                    if (dsNew.Tables[2].Rows.Count > 0)
                    {
                        Fixed_DARate = decimal.Parse(dsNew.Tables[2].Rows[0]["Fixed_DARate"].ToString());
                    }
                   
                    lblPermanent_DARate.Text = Permanent_DARate.ToString();
                    lblFixed_DARate.Text = Fixed_DARate.ToString();
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        Jan_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Jan_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);
                    
                        
                    if(dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() != "Deputation Employee")
                    { 
                         Jan_AmountEPF = Math.Round(((Amount + Jan_AmountDA) * 12) / 100);
                    }
                    //if (dsNew.Tables[0].Rows[0]["Emp_NPF"].ToString() == "True")
                    //{
                    //    Jan_AmountNPF = Math.Round(((Amount + Jan_AmountDA) * Fixed_NPFRate) / 100);
                    //}

                    if ((Amount + Jan_AmountDA) <= Actual_ESI_Amount)
                    {
                        Jan_AmountESI = Math.Round(((Amount + Jan_AmountDA) * Convert.ToDecimal(Fixed_ESIRate)) / 100);
                    }

                    Jan_AmountBASIC = Amount;
                    if (dsNew.Tables[4].Rows.Count > 0)
                    {
                        Jan_AmountLeave = decimal.Parse(dsNew.Tables[4].Rows[0]["LeaveAmount"].ToString());
                    }
                   
                   
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo", "MonthName" }, new string[] { "2", Year_2, ddlEmployee.SelectedValue.ToString(), "2", "February" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Permanent_DARate = 0;
                    Fixed_DARate = 0;
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        Permanent_DARate = decimal.Parse(dsNew.Tables[1].Rows[0]["Permanent_DARate"].ToString());
                    }
                    if (dsNew.Tables[2].Rows.Count > 0)
                    {
                        Fixed_DARate = decimal.Parse(dsNew.Tables[2].Rows[0]["Fixed_DARate"].ToString());
                    }
                    lblPermanent_DARate.Text = Permanent_DARate.ToString();
                    lblFixed_DARate.Text = Fixed_DARate.ToString();
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        Feb_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Feb_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);
                    if(dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() != "Deputation Employee")
                    {
                        Feb_AmountEPF = Math.Round(((Amount + Feb_AmountDA) * 12) / 100);
                     }
                    //if (dsNew.Tables[0].Rows[0]["Emp_NPF"].ToString() == "True")
                    //{
                    //    Feb_AmountNPF = Math.Round(((Amount + Feb_AmountDA) * Fixed_NPFRate) / 100);
                    //}

                    if ((Amount + Feb_AmountDA) <= Actual_ESI_Amount)
                    {
                        Feb_AmountESI = Math.Round(((Amount + Feb_AmountDA) * Convert.ToDecimal(Fixed_ESIRate)) / 100);
                    }

                    Feb_AmountBASIC = Amount;
                    if (dsNew.Tables[4].Rows.Count > 0)
                    {
                        Feb_AmountLeave = decimal.Parse(dsNew.Tables[4].Rows[0]["LeaveAmount"].ToString());
                    }
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo", "MonthName" }, new string[] { "2", Year_2, ddlEmployee.SelectedValue.ToString(), "3", "March" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Permanent_DARate = 0;
                    Fixed_DARate = 0;
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        Permanent_DARate = decimal.Parse(dsNew.Tables[1].Rows[0]["Permanent_DARate"].ToString());
                    }
                    if (dsNew.Tables[2].Rows.Count > 0)
                    {
                        Fixed_DARate = decimal.Parse(dsNew.Tables[2].Rows[0]["Fixed_DARate"].ToString());
                    }
                    lblPermanent_DARate.Text = Permanent_DARate.ToString();
                    lblFixed_DARate.Text = Fixed_DARate.ToString();
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        Mar_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Mar_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() != "Deputation Employee")
                    {
                        Mar_AmountEPF = Math.Round(((Amount + Mar_AmountDA) * 12) / 100);
                    }
                    //if (dsNew.Tables[0].Rows[0]["Emp_NPF"].ToString() == "True")
                    //{
                    //    Mar_AmountNPF = Math.Round(((Amount + Mar_AmountDA) * Fixed_NPFRate) / 100);
                    //}

                    if ((Amount + Mar_AmountDA) <= Actual_ESI_Amount)
                    {
                        Mar_AmountESI = Math.Round(((Amount + Mar_AmountDA) * Convert.ToDecimal(Fixed_ESIRate)) / 100);
                    }

                    Mar_AmountBASIC = Amount;
                    if (dsNew.Tables[4].Rows.Count > 0)
                    {
                        Mar_AmountLeave = decimal.Parse(dsNew.Tables[4].Rows[0]["LeaveAmount"].ToString());
                    }
                }

            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo", "MonthName" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "4", "April" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Permanent_DARate = 0;
                    Fixed_DARate = 0;
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        Permanent_DARate = decimal.Parse(dsNew.Tables[1].Rows[0]["Permanent_DARate"].ToString());
                    }
                    if (dsNew.Tables[2].Rows.Count > 0)
                    {
                        Fixed_DARate = decimal.Parse(dsNew.Tables[2].Rows[0]["Fixed_DARate"].ToString());
                    }
                    lblPermanent_DARate.Text = Permanent_DARate.ToString();
                    lblFixed_DARate.Text = Fixed_DARate.ToString();
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        Apr_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Apr_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() != "Deputation Employee")
                    {
                        Apr_AmountEPF = Math.Round(((Amount + Apr_AmountDA) * 12) / 100);
                    }
                    //if (dsNew.Tables[0].Rows[0]["Emp_NPF"].ToString() == "True")
                    //{
                    //    Apr_AmountNPF = Math.Round(((Amount + Apr_AmountDA) * Fixed_NPFRate) / 100);
                    //}

                    if ((Amount + Apr_AmountDA) <= Actual_ESI_Amount)
                    {
                        Apr_AmountESI = Math.Round(((Amount + Apr_AmountDA) * Convert.ToDecimal(Fixed_ESIRate)) / 100);
                    }

                    Apr_AmountBASIC = Amount;
                    if (dsNew.Tables[4].Rows.Count > 0)
                    {
                        Apr_AmountLeave = decimal.Parse(dsNew.Tables[4].Rows[0]["LeaveAmount"].ToString());
                    }
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo", "MonthName" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "5", "May" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Permanent_DARate = 0;
                    Fixed_DARate = 0;
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        Permanent_DARate = decimal.Parse(dsNew.Tables[1].Rows[0]["Permanent_DARate"].ToString());
                    }
                    if (dsNew.Tables[2].Rows.Count > 0)
                    {
                        Fixed_DARate = decimal.Parse(dsNew.Tables[2].Rows[0]["Fixed_DARate"].ToString());
                    }
                    lblPermanent_DARate.Text = Permanent_DARate.ToString();
                    lblFixed_DARate.Text = Fixed_DARate.ToString();
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        May_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        May_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() != "Deputation Employee")
                    {
                        May_AmountEPF = Math.Round(((Amount + May_AmountDA) * 12) / 100);
                    }
                    //if (dsNew.Tables[0].Rows[0]["Emp_NPF"].ToString() == "True")
                    //{
                    //    May_AmountNPF = Math.Round(((Amount + May_AmountDA) * Fixed_NPFRate) / 100);
                    //}
                    if ((Amount + May_AmountDA) <= Actual_ESI_Amount)
                    {
                        May_AmountESI = Math.Round(((Amount + May_AmountDA) * Convert.ToDecimal(Fixed_ESIRate)) / 100);
                    }

                    May_AmountBASIC = Amount;
                    if (dsNew.Tables[4].Rows.Count > 0)
                    {
                        May_AmountLeave = decimal.Parse(dsNew.Tables[4].Rows[0]["LeaveAmount"].ToString());
                    }
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo", "MonthName" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "6", "June" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Permanent_DARate = 0;
                    Fixed_DARate = 0;
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        Permanent_DARate = decimal.Parse(dsNew.Tables[1].Rows[0]["Permanent_DARate"].ToString());
                    }
                    if (dsNew.Tables[2].Rows.Count > 0)
                    {
                        Fixed_DARate = decimal.Parse(dsNew.Tables[2].Rows[0]["Fixed_DARate"].ToString());
                    }
                    lblPermanent_DARate.Text = Permanent_DARate.ToString();
                    lblFixed_DARate.Text = Fixed_DARate.ToString();
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        Jun_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Jun_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() != "Deputation Employee")
                    {
                        Jun_AmountEPF = Math.Round(((Amount + Jun_AmountDA) * 12) / 100);
                    }
                    //if (dsNew.Tables[0].Rows[0]["Emp_NPF"].ToString() == "True")
                    //{
                    //    Jun_AmountNPF = Math.Round(((Amount + Jun_AmountDA) * Fixed_NPFRate) / 100);
                    //}
                    if ((Amount + Jun_AmountDA) <= Actual_ESI_Amount)
                    {
                        Jun_AmountESI = Math.Round(((Amount + Jun_AmountDA) * Convert.ToDecimal(Fixed_ESIRate)) / 100);
                    }

                    Jun_AmountBASIC = Amount;
                    if (dsNew.Tables[4].Rows.Count > 0)
                    {
                        Jun_AmountLeave = decimal.Parse(dsNew.Tables[4].Rows[0]["LeaveAmount"].ToString());
                    }
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo", "MonthName" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "7", "July" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Permanent_DARate = 0;
                    Fixed_DARate = 0;
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        Permanent_DARate = decimal.Parse(dsNew.Tables[1].Rows[0]["Permanent_DARate"].ToString());
                    }
                    if (dsNew.Tables[2].Rows.Count > 0)
                    {
                        Fixed_DARate = decimal.Parse(dsNew.Tables[2].Rows[0]["Fixed_DARate"].ToString());
                    }
                    lblPermanent_DARate.Text = Permanent_DARate.ToString();
                    lblFixed_DARate.Text = Fixed_DARate.ToString();
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        Jul_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Jul_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() != "Deputation Employee")
                    {
                        Jul_AmountEPF = Math.Round(((Amount + Jul_AmountDA) * 12) / 100);
                    }
                    //if (dsNew.Tables[0].Rows[0]["Emp_NPF"].ToString() == "True")
                    //{
                    //    Jul_AmountNPF = Math.Round(((Amount + Jul_AmountDA) * Fixed_NPFRate) / 100);
                    //}

                    if ((Amount + Jul_AmountDA) <= Actual_ESI_Amount)
                    {
                        Jul_AmountESI = Math.Round(((Amount + Jul_AmountDA) * Convert.ToDecimal(Fixed_ESIRate)) / 100);
                    }

                    Jul_AmountBASIC = Amount;
                    if (dsNew.Tables[4].Rows.Count > 0)
                    {
                        Jul_AmountLeave = decimal.Parse(dsNew.Tables[4].Rows[0]["LeaveAmount"].ToString());
                    }
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo", "MonthName" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "8","August" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Permanent_DARate = 0;
                    Fixed_DARate = 0;
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        Permanent_DARate = decimal.Parse(dsNew.Tables[1].Rows[0]["Permanent_DARate"].ToString());
                    }
                    if (dsNew.Tables[2].Rows.Count > 0)
                    {
                        Fixed_DARate = decimal.Parse(dsNew.Tables[2].Rows[0]["Fixed_DARate"].ToString());
                    }
                    lblPermanent_DARate.Text = Permanent_DARate.ToString();
                    lblFixed_DARate.Text = Fixed_DARate.ToString();
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        Aug_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Aug_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() != "Deputation Employee")
                    {
                        Aug_AmountEPF = Math.Round(((Amount + Aug_AmountDA) * 12) / 100);
                    }
                    //if (dsNew.Tables[0].Rows[0]["Emp_NPF"].ToString() == "True")
                    //{
                    //    Aug_AmountNPF = Math.Round(((Amount + Aug_AmountDA) * Fixed_NPFRate) / 100);
                    //}

                    if ((Amount + Aug_AmountDA) <= Actual_ESI_Amount)
                    {
                        Aug_AmountESI = Math.Round(((Amount + Aug_AmountDA) * Convert.ToDecimal(Fixed_ESIRate)) / 100);
                    }

                    Aug_AmountBASIC = Amount;
                    if (dsNew.Tables[4].Rows.Count > 0)
                    {
                        Aug_AmountLeave = decimal.Parse(dsNew.Tables[4].Rows[0]["LeaveAmount"].ToString());
                    }
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo", "MonthName" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "9", "September" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Permanent_DARate = 0;
                    Fixed_DARate = 0;
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        Permanent_DARate = decimal.Parse(dsNew.Tables[1].Rows[0]["Permanent_DARate"].ToString());
                    }
                    if (dsNew.Tables[2].Rows.Count > 0)
                    {
                        Fixed_DARate = decimal.Parse(dsNew.Tables[2].Rows[0]["Fixed_DARate"].ToString());
                    }
                    lblPermanent_DARate.Text = Permanent_DARate.ToString();
                    lblFixed_DARate.Text = Fixed_DARate.ToString();
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        Sep_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Sep_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() != "Deputation Employee")
                    {
                        Sep_AmountEPF = Math.Round(((Amount + Sep_AmountDA) * 12) / 100);
                    }
                    //if (dsNew.Tables[0].Rows[0]["Emp_NPF"].ToString() == "True")
                    //{
                    //    Sep_AmountNPF = Math.Round(((Amount + Sep_AmountDA) * Fixed_NPFRate) / 100);
                    //}

                    if ((Amount + Sep_AmountDA) <= Actual_ESI_Amount)
                    {
                        Sep_AmountESI = Math.Round(((Amount + Sep_AmountDA) * Convert.ToDecimal(Fixed_ESIRate)) / 100);
                    }

                    Sep_AmountBASIC = Amount;
                    if (dsNew.Tables[4].Rows.Count > 0)
                    {
                        Sep_AmountLeave = decimal.Parse(dsNew.Tables[4].Rows[0]["LeaveAmount"].ToString());
                    }
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo", "MonthName" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "10","October" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Permanent_DARate = 0;
                    Fixed_DARate = 0;
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        Permanent_DARate = decimal.Parse(dsNew.Tables[1].Rows[0]["Permanent_DARate"].ToString());
                    }
                    if (dsNew.Tables[2].Rows.Count > 0)
                    {
                        Fixed_DARate = decimal.Parse(dsNew.Tables[2].Rows[0]["Fixed_DARate"].ToString());
                    }
                    lblPermanent_DARate.Text = Permanent_DARate.ToString();
                    lblFixed_DARate.Text = Fixed_DARate.ToString();
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        Oct_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Oct_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() != "Deputation Employee")
                    {
                        Oct_AmountEPF = Math.Round(((Amount + Oct_AmountDA) * 12) / 100);
                    }
                    //if (dsNew.Tables[0].Rows[0]["Emp_NPF"].ToString() == "True")
                    //{
                    //    Oct_AmountNPF = Math.Round(((Amount + Oct_AmountDA) * Fixed_NPFRate) / 100);
                    //}

                    if ((Amount + Oct_AmountDA) <= Actual_ESI_Amount)
                    {
                        Oct_AmountESI = Math.Round(((Amount + Oct_AmountDA) * Convert.ToDecimal(Fixed_ESIRate)) / 100);
                    }

                    Oct_AmountBASIC = Amount;
                    if (dsNew.Tables[4].Rows.Count > 0)
                    {
                        Oct_AmountLeave = decimal.Parse(dsNew.Tables[4].Rows[0]["LeaveAmount"].ToString());
                    }
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo", "MonthName" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "11", "November" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Permanent_DARate = 0;
                    Fixed_DARate = 0;
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        Permanent_DARate = decimal.Parse(dsNew.Tables[1].Rows[0]["Permanent_DARate"].ToString());
                    }
                    if (dsNew.Tables[2].Rows.Count > 0)
                    {
                        Fixed_DARate = decimal.Parse(dsNew.Tables[2].Rows[0]["Fixed_DARate"].ToString());
                    }
                    lblPermanent_DARate.Text = Permanent_DARate.ToString();
                    lblFixed_DARate.Text = Fixed_DARate.ToString();
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        Nov_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Nov_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() != "Deputation Employee")
                    {
                        Nov_AmountEPF = Math.Round(((Amount + Nov_AmountDA) * 12) / 100);
                    }
                    //if (dsNew.Tables[0].Rows[0]["Emp_NPF"].ToString() == "True")
                    //{
                    //    Nov_AmountNPF = Math.Round(((Amount + Nov_AmountDA) * Fixed_NPFRate) / 100);
                    //}

                    if ((Amount + Nov_AmountDA) <= Actual_ESI_Amount)
                    {
                        Nov_AmountESI = Math.Round(((Amount + Nov_AmountDA) * Convert.ToDecimal(Fixed_ESIRate)) / 100);
                    }

                    Nov_AmountBASIC = Amount;
                    if (dsNew.Tables[4].Rows.Count > 0)
                    {
                       Nov_AmountLeave = decimal.Parse(dsNew.Tables[4].Rows[0]["LeaveAmount"].ToString());
                    }
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo", "MonthName" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "12", "December" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Permanent_DARate = 0;
                    Fixed_DARate = 0;
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        Permanent_DARate = decimal.Parse(dsNew.Tables[1].Rows[0]["Permanent_DARate"].ToString());
                    }
                    if (dsNew.Tables[2].Rows.Count > 0)
                    {
                        Fixed_DARate = decimal.Parse(dsNew.Tables[2].Rows[0]["Fixed_DARate"].ToString());
                    }
                    lblPermanent_DARate.Text = Permanent_DARate.ToString();
                    lblFixed_DARate.Text = Fixed_DARate.ToString();
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent" || dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Deputation Employee")
                        Dec_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Dec_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() != "Deputation Employee")
                    {
                        Dec_AmountEPF = Math.Round(((Amount + Dec_AmountDA) * 12) / 100);
                    }
                    //if (dsNew.Tables[0].Rows[0]["Emp_NPF"].ToString() == "True")
                    //{
                    //    Dec_AmountNPF = Math.Round(((Amount + Dec_AmountDA) * Fixed_NPFRate) / 100);
                    //}

                    if ((Amount + Dec_AmountDA) <= Actual_ESI_Amount)
                    {
                        Dec_AmountESI = Math.Round(((Amount + Dec_AmountDA) * Convert.ToDecimal(Fixed_ESIRate)) / 100);
                    }

                    Dec_AmountBASIC = Amount;
                    if (dsNew.Tables[4].Rows.Count > 0)
                    {
                        Dec_AmountLeave = decimal.Parse(dsNew.Tables[4].Rows[0]["LeaveAmount"].ToString());
                    }
                }
            }




            lblMsg.Text = "";
            Clear();

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            divDetail.Visible = false;
            string Monthcount = "";
            ViewState["Monthcount"] = "";
            ViewState["EmployeeID"] = ddlEmployee.SelectedValue.ToString();
            ds = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID" }, new string[] { "1", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString() }, "dataset");
            if (ds.Tables[2].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    Monthcount += ds.Tables[2].Rows[i]["Salary_MonthNo"].ToString() + ", ";
                    ViewState["Monthcount"] = Monthcount;
                }
            }
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                divDetail.Visible = true;

                foreach (GridViewRow gr in GridView1.Rows)
                {
                    if (gr.RowType == DataControlRowType.DataRow)
                    {
                        Label lblEarnDeduction_ID = (Label)gr.FindControl("lblEarnDeduction_ID");

                        TextBox Apr_Amount = (TextBox)gr.FindControl("txtApr_Amount");
                        TextBox May_Amount = (TextBox)gr.FindControl("txtMay_Amount");
                        TextBox Jun_Amount = (TextBox)gr.FindControl("txtJun_Amount");
                        TextBox Jul_Amount = (TextBox)gr.FindControl("txtJul_Amount");
                        TextBox Aug_Amount = (TextBox)gr.FindControl("txtAug_Amount");
                        TextBox Sep_Amount = (TextBox)gr.FindControl("txtSep_Amount");
                        TextBox Oct_Amount = (TextBox)gr.FindControl("txtOct_Amount");
                        TextBox Nov_Amount = (TextBox)gr.FindControl("txtNov_Amount");
                        TextBox Dec_Amount = (TextBox)gr.FindControl("txtDec_Amount");
                        TextBox Jan_Amount = (TextBox)gr.FindControl("txtJan_Amount");
                        TextBox Feb_Amount = (TextBox)gr.FindControl("txtFeb_Amount");
                        TextBox Mar_Amount = (TextBox)gr.FindControl("txtMar_Amount");
                        if (Monthcount != "")
                        {
                            string[] monthNo = Monthcount.Split(',');
                            for (int i = 0; i < monthNo.Length - 1; i++)
                            {
                                if (monthNo[i].ToString() != "")
                                {
                                    int aa = int.Parse(monthNo[i]);
                                    if (aa == 1)
                                        Jan_Amount.Enabled = false;
                                    else if (aa == 2)
                                        Feb_Amount.Enabled = false;
                                    else if (aa == 3)
                                        Mar_Amount.Enabled = false;
                                    else if (aa == 4)
                                        Apr_Amount.Enabled = false;
                                    else if (aa == 5)
                                        May_Amount.Enabled = false;
                                    else if (aa == 6)
                                        Jun_Amount.Enabled = false;
                                    else if (aa == 7)
                                        Jul_Amount.Enabled = false;
                                    else if (aa == 8)
                                        Aug_Amount.Enabled = false;
                                    else if (aa == 9)
                                        Sep_Amount.Enabled = false;
                                    else if (aa == 10)
                                        Oct_Amount.Enabled = false;
                                    else if (aa == 11)
                                        Nov_Amount.Enabled = false;
                                    else if (aa == 12)
                                        Dec_Amount.Enabled = false;
                                }

                            }
                        }
                        // New code
                        if (lblEarnDeduction_ID.Text == "1")
                        {
                            if (Jan_Amount.Enabled != false)
                                Jan_Amount.Text = Jan_AmountDA.ToString();

                            if (Feb_Amount.Enabled != false)
                                Feb_Amount.Text = Feb_AmountDA.ToString();

                            if (Mar_Amount.Enabled != false)
                                Mar_Amount.Text = Mar_AmountDA.ToString();

                            if (Apr_Amount.Enabled != false)
                                Apr_Amount.Text = Apr_AmountDA.ToString();

                            if (May_Amount.Enabled != false)
                                May_Amount.Text = May_AmountDA.ToString();

                            if (Jun_Amount.Enabled != false)
                                Jun_Amount.Text = Jun_AmountDA.ToString();

                            if (Jul_Amount.Enabled != false)
                                Jul_Amount.Text = Jul_AmountDA.ToString();

                            if (Aug_Amount.Enabled != false)
                                Aug_Amount.Text = Aug_AmountDA.ToString();

                            if (Sep_Amount.Enabled != false)
                                Sep_Amount.Text = Sep_AmountDA.ToString();

                            if (Oct_Amount.Enabled != false)
                                Oct_Amount.Text = Oct_AmountDA.ToString();

                            if (Nov_Amount.Enabled != false)
                                Nov_Amount.Text = Nov_AmountDA.ToString();

                            if (Dec_Amount.Enabled != false)
                                Dec_Amount.Text = Dec_AmountDA.ToString();
                        }
                    }
                }

                txtEarTotApr.Text = Apr_AmountBASIC.ToString();
                txtEarTotApr.Enabled = false;
                txtEarTotMay.Text = May_AmountBASIC.ToString();
                txtEarTotMay.Enabled = false;
                txtEarTotJun.Text = Jun_AmountBASIC.ToString();
                txtEarTotJun.Enabled = false;
                txtEarTotJul.Text = Jul_AmountBASIC.ToString();
                txtEarTotJul.Enabled = false;
                txtEarTotAug.Text = Aug_AmountBASIC.ToString();
                txtEarTotAug.Enabled = false;
                txtEarTotSep.Text = Sep_AmountBASIC.ToString();
                txtEarTotSep.Enabled = false;
                txtEarTotOct.Text = Oct_AmountBASIC.ToString();
                txtEarTotOct.Enabled = false;
                txtEarTotNov.Text = Nov_AmountBASIC.ToString();
                txtEarTotNov.Enabled = false;
                txtEarTotDec.Text = Dec_AmountBASIC.ToString();
                txtEarTotDec.Enabled = false;
                txtEarTotJan.Text = Jan_AmountBASIC.ToString();
                txtEarTotJan.Enabled = false;
                txtEarTotFeb.Text = Feb_AmountBASIC.ToString();
                txtEarTotFeb.Enabled = false;
                txtEarTotMar.Text = Mar_AmountBASIC.ToString();
                txtEarTotMar.Enabled = false;

            }
            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                GridView2.DataSource = ds.Tables[1];
                GridView2.DataBind();

                foreach (GridViewRow gr in GridView2.Rows)
                {
                    if (gr.RowType == DataControlRowType.DataRow)
                    {
                        Label lblEarnDeduction_ID = (Label)gr.FindControl("lblEarnDeduction_ID");
                        TextBox Apr_Amount = (TextBox)gr.FindControl("txtApr_Amount");
                        TextBox May_Amount = (TextBox)gr.FindControl("txtMay_Amount");
                        TextBox Jun_Amount = (TextBox)gr.FindControl("txtJun_Amount");
                        TextBox Jul_Amount = (TextBox)gr.FindControl("txtJul_Amount");
                        TextBox Aug_Amount = (TextBox)gr.FindControl("txtAug_Amount");
                        TextBox Sep_Amount = (TextBox)gr.FindControl("txtSep_Amount");
                        TextBox Oct_Amount = (TextBox)gr.FindControl("txtOct_Amount");
                        TextBox Nov_Amount = (TextBox)gr.FindControl("txtNov_Amount");
                        TextBox Dec_Amount = (TextBox)gr.FindControl("txtDec_Amount");
                        TextBox Jan_Amount = (TextBox)gr.FindControl("txtJan_Amount");
                        TextBox Feb_Amount = (TextBox)gr.FindControl("txtFeb_Amount");
                        TextBox Mar_Amount = (TextBox)gr.FindControl("txtMar_Amount");
                        string[] monthNo = Monthcount.Split(',');
                        for (int i = 0; i < monthNo.Length - 1; i++)
                        {

                            if (monthNo[i] != "")
                            {
                                int aa = int.Parse(monthNo[i]);
                                if (aa == 1)
                                    Jan_Amount.Enabled = false;
                                else if (aa == 2)
                                    Feb_Amount.Enabled = false;
                                else if (aa == 3)
                                    Mar_Amount.Enabled = false;
                                else if (aa == 4)
                                    Apr_Amount.Enabled = false;
                                else if (aa == 5)
                                    May_Amount.Enabled = false;
                                else if (aa == 6)
                                    Jun_Amount.Enabled = false;
                                else if (aa == 7)
                                    Jul_Amount.Enabled = false;
                                else if (aa == 8)
                                    Aug_Amount.Enabled = false;
                                else if (aa == 9)
                                    Sep_Amount.Enabled = false;
                                else if (aa == 10)
                                    Oct_Amount.Enabled = false;
                                else if (aa == 11)
                                    Nov_Amount.Enabled = false;
                                else if (aa == 12)
                                    Dec_Amount.Enabled = false;
                            }
                        }

                        // New code
                        if (lblEarnDeduction_ID.Text == "8")
                        {
                            if (Jan_Amount.Enabled != false)
                                Jan_Amount.Text = Jan_AmountEPF.ToString();

                            if (Feb_Amount.Enabled != false)
                                Feb_Amount.Text = Feb_AmountEPF.ToString();

                            if (Mar_Amount.Enabled != false)
                                Mar_Amount.Text = Mar_AmountEPF.ToString();

                            if (Apr_Amount.Enabled != false)
                                Apr_Amount.Text = Apr_AmountEPF.ToString();

                            if (May_Amount.Enabled != false)
                                May_Amount.Text = May_AmountEPF.ToString();

                            if (Jun_Amount.Enabled != false)
                                Jun_Amount.Text = Jun_AmountEPF.ToString();

                            if (Jul_Amount.Enabled != false)
                                Jul_Amount.Text = Jul_AmountEPF.ToString();

                            if (Aug_Amount.Enabled != false)
                                Aug_Amount.Text = Aug_AmountEPF.ToString();

                            if (Sep_Amount.Enabled != false)
                                Sep_Amount.Text = Sep_AmountEPF.ToString();

                            if (Oct_Amount.Enabled != false)
                                Oct_Amount.Text = Oct_AmountEPF.ToString();

                            if (Nov_Amount.Enabled != false)
                                Nov_Amount.Text = Nov_AmountEPF.ToString();

                            if (Dec_Amount.Enabled != false)
                                Dec_Amount.Text = Dec_AmountEPF.ToString();
                        }

                        // New code
                        if (lblEarnDeduction_ID.Text == "37")  // by pawan on 19-jan-2023
                        {
                            if (Jan_Amount.Enabled != false)
                                Jan_Amount.Text = Jan_AmountNPF.ToString();

                            if (Feb_Amount.Enabled != false)
                                Feb_Amount.Text = Feb_AmountNPF.ToString();

                            if (Mar_Amount.Enabled != false)
                                Mar_Amount.Text = Mar_AmountNPF.ToString();

                            if (Apr_Amount.Enabled != false)
                                Apr_Amount.Text = Apr_AmountNPF.ToString();

                            if (May_Amount.Enabled != false)
                                May_Amount.Text = May_AmountNPF.ToString();

                            if (Jun_Amount.Enabled != false)
                                Jun_Amount.Text = Jun_AmountNPF.ToString();

                            if (Jul_Amount.Enabled != false)
                                Jul_Amount.Text = Jul_AmountNPF.ToString();

                            if (Aug_Amount.Enabled != false)
                                Aug_Amount.Text = Aug_AmountNPF.ToString();

                            if (Sep_Amount.Enabled != false)
                                Sep_Amount.Text = Sep_AmountNPF.ToString();

                            if (Oct_Amount.Enabled != false)
                                Oct_Amount.Text = Oct_AmountNPF.ToString();

                            if (Nov_Amount.Enabled != false)
                                Nov_Amount.Text = Nov_AmountNPF.ToString();

                            if (Dec_Amount.Enabled != false)
                                Dec_Amount.Text = Dec_AmountNPF.ToString();
                        }
                        if (lblEarnDeduction_ID.Text == "38") // by pawan on 19-jan-2023
                        {
                            if (Jan_Amount.Enabled != false)
                                Jan_Amount.Text = Jan_AmountESI.ToString();

                            if (Feb_Amount.Enabled != false)
                                Feb_Amount.Text = Feb_AmountESI.ToString();

                            if (Mar_Amount.Enabled != false)
                                Mar_Amount.Text = Mar_AmountESI.ToString();

                            if (Apr_Amount.Enabled != false)
                                Apr_Amount.Text = Apr_AmountESI.ToString();

                            if (May_Amount.Enabled != false)
                                May_Amount.Text = May_AmountESI.ToString();

                            if (Jun_Amount.Enabled != false)
                                Jun_Amount.Text = Jun_AmountESI.ToString();

                            if (Jul_Amount.Enabled != false)
                                Jul_Amount.Text = Jul_AmountESI.ToString();

                            if (Aug_Amount.Enabled != false)
                                Aug_Amount.Text = Aug_AmountESI.ToString();

                            if (Sep_Amount.Enabled != false)
                                Sep_Amount.Text = Sep_AmountESI.ToString();

                            if (Oct_Amount.Enabled != false)
                                Oct_Amount.Text = Oct_AmountESI.ToString();

                            if (Nov_Amount.Enabled != false)
                                Nov_Amount.Text = Nov_AmountESI.ToString();

                            if (Dec_Amount.Enabled != false)
                                Dec_Amount.Text = Dec_AmountESI.ToString();
                        }
                        if (lblEarnDeduction_ID.Text == "9") // by pawan on 19-jan-2023
                        {
                            
                                Jan_Amount.Text = Jan_AmountLeave.ToString();


                                Feb_Amount.Text = Feb_AmountLeave.ToString();


                                Mar_Amount.Text = Mar_AmountLeave.ToString();


                                Apr_Amount.Text = Apr_AmountLeave.ToString();


                                May_Amount.Text = May_AmountLeave.ToString();


                                Jun_Amount.Text = Jun_AmountLeave.ToString();


                                Jul_Amount.Text = Jul_AmountLeave.ToString();


                                Aug_Amount.Text = Aug_AmountLeave.ToString();


                                Sep_Amount.Text = Sep_AmountLeave.ToString();


                                Oct_Amount.Text = Oct_AmountLeave.ToString();


                                Nov_Amount.Text = Nov_AmountLeave.ToString();


                                Dec_Amount.Text = Dec_AmountLeave.ToString();
                        }
                        //if (lblEarnDeduction_ID.Text == "12")
                        //{
                        //    if (Jan_Amount.Enabled != false)
                        //        Jan_Amount.Text = "250";

                        //    if (Feb_Amount.Enabled != false)
                        //        Feb_Amount.Text = "0";

                        //    if (Mar_Amount.Enabled != false)
                        //        Mar_Amount.Text = "0";

                        //    if (Apr_Amount.Enabled != false)
                        //        Apr_Amount.Text = "250";

                        //    if (May_Amount.Enabled != false)
                        //        May_Amount.Text = "250";

                        //    if (Jun_Amount.Enabled != false)
                        //        Jun_Amount.Text = "250";

                        //    if (Jul_Amount.Enabled != false)
                        //        Jul_Amount.Text = "250";

                        //    if (Aug_Amount.Enabled != false)
                        //        Aug_Amount.Text = "250";

                        //    if (Sep_Amount.Enabled != false)
                        //        Sep_Amount.Text = "250";

                        //    if (Oct_Amount.Enabled != false)
                        //        Oct_Amount.Text = "250";

                        //    if (Nov_Amount.Enabled != false)
                        //        Nov_Amount.Text = "250";

                        //    if (Dec_Amount.Enabled != false)
                        //        Dec_Amount.Text = "250";
                        //}

                        if (lblEarnDeduction_ID.Text == "18")
                        {
                            if (Apr_Amount.Enabled != false)  
   
                            Apr_Amount.Text = (Convert.ToInt64(Convert.ToDouble(Convert.ToInt64(Convert.ToDouble((ds.Tables[4].Rows[0]["Emp_BasicSalery"].ToString()))) / 30))).ToString();

                        }
                    }

                }
                Label lblRowNumber = (Label)GridView2.FooterRow.FindControl("lblRowNumber");
                lblRowNumber.Text = (ds.Tables[1].Rows.Count + 1).ToString();

                TextBox FApr_Amount = (TextBox)GridView2.FooterRow.FindControl("txtApr_Amount");
                TextBox FMay_Amount = (TextBox)GridView2.FooterRow.FindControl("txtMay_Amount");
                TextBox FJun_Amount = (TextBox)GridView2.FooterRow.FindControl("txtJun_Amount");
                TextBox FJul_Amount = (TextBox)GridView2.FooterRow.FindControl("txtJul_Amount");
                TextBox FAug_Amount = (TextBox)GridView2.FooterRow.FindControl("txtAug_Amount");
                TextBox FSep_Amount = (TextBox)GridView2.FooterRow.FindControl("txtSep_Amount");
                TextBox FOct_Amount = (TextBox)GridView2.FooterRow.FindControl("txtOct_Amount");
                TextBox FNov_Amount = (TextBox)GridView2.FooterRow.FindControl("txtNov_Amount");
                TextBox FDec_Amount = (TextBox)GridView2.FooterRow.FindControl("txtDec_Amount");
                TextBox FJan_Amount = (TextBox)GridView2.FooterRow.FindControl("txtJan_Amount");
                TextBox FFeb_Amount = (TextBox)GridView2.FooterRow.FindControl("txtFeb_Amount");
                TextBox FMar_Amount = (TextBox)GridView2.FooterRow.FindControl("txtMar_Amount");
                if (ds.Tables[3].Rows.Count > 0)
                {
                    FApr_Amount.Text = ds.Tables[3].Rows[0]["Apr_Amount"].ToString();
                    FMay_Amount.Text = ds.Tables[3].Rows[0]["May_Amount"].ToString();
                    FJun_Amount.Text = ds.Tables[3].Rows[0]["Jun_Amount"].ToString();
                    FJul_Amount.Text = ds.Tables[3].Rows[0]["Jul_Amount"].ToString();
                    FAug_Amount.Text = ds.Tables[3].Rows[0]["Aug_Amount"].ToString();
                    FSep_Amount.Text = ds.Tables[3].Rows[0]["Sep_Amount"].ToString();
                    FOct_Amount.Text = ds.Tables[3].Rows[0]["Oct_Amount"].ToString();
                    FNov_Amount.Text = ds.Tables[3].Rows[0]["Nov_Amount"].ToString();
                    FDec_Amount.Text = ds.Tables[3].Rows[0]["Dec_Amount"].ToString();
                    FJan_Amount.Text = ds.Tables[3].Rows[0]["Jan_Amount"].ToString();
                    FFeb_Amount.Text = ds.Tables[3].Rows[0]["Feb_Amount"].ToString();
                    FMar_Amount.Text = ds.Tables[3].Rows[0]["Mar_Amount"].ToString();
                }
                else
                {
                    FApr_Amount.Text = "0";
                    FMay_Amount.Text = "0";
                    FJun_Amount.Text = "0";
                    FJul_Amount.Text = "0";
                    FAug_Amount.Text = "0";
                    FSep_Amount.Text = "0";
                    FOct_Amount.Text = "0";
                    FNov_Amount.Text = "0";
                    FDec_Amount.Text = "0";
                    FJan_Amount.Text = "0";
                    FFeb_Amount.Text = "0";
                    FMar_Amount.Text = "0";
                }

                if (ds.Tables[4].Rows.Count != 0)
                {
                    lblBank_AccountNo.Text = ds.Tables[4].Rows[0]["Bank_AccountNo"].ToString();
                    lblSalary_NetSalary.Text = ds.Tables[4].Rows[0]["Emp_BasicSalery"].ToString();
                    lblBank_Name.Text = ds.Tables[4].Rows[0]["Bank_Name"].ToString();
                    lblIFSCCode.Text = ds.Tables[4].Rows[0]["Bank_IfscCode"].ToString();
                    lblGroupInsurance_No.Text = ds.Tables[4].Rows[0]["GroupInsurance_No"].ToString();
                    lblEPF_No.Text = ds.Tables[4].Rows[0]["EPF_No"].ToString();
                    Emp_BasicSalery = Convert.ToInt64(Convert.ToDouble((ds.Tables[4].Rows[0]["Emp_BasicSalery"].ToString())));
                    //lblarilbasic.Text = "<i class='fa fa-inr'></i>&nbsp;&nbsp;" + (Convert.ToInt64(Convert.ToDouble(Emp_BasicSalery / 30))).ToString() + "  /-";
                    //lbl28daysBasic.Text = "<i class='fa fa-inr'></i>&nbsp;&nbsp;&nbsp;&nbsp;" +  (Convert.ToInt64(Convert.ToDouble(Emp_BasicSalery / 28))).ToString() + "  /-";
                    //lbl29daysBasic.Text = "<i class='fa fa-inr'></i>&nbsp;&nbsp;&nbsp;&nbsp;" + (Convert.ToInt64(Convert.ToDouble(Emp_BasicSalery / 29))).ToString() + "  /-";
                    //lbl30daysBasic.Text = "<i class='fa fa-inr'></i>&nbsp;&nbsp;&nbsp;&nbsp;" + (Convert.ToInt64(Convert.ToDouble(Emp_BasicSalery / 30))).ToString() + "  /-";
                    //lbl31daysBasic.Text = "<i class='fa fa-inr'></i>&nbsp;&nbsp;&nbsp;&nbsp;" + (Convert.ToInt64(Convert.ToDouble(Emp_BasicSalery / 31))).ToString() + "  /-";
                }

            }
            FillGridTotal();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGridTotal()
    {
        try
        {
            // Earning Detail Total
            #region Earning Total

            decimal TotApr_Amount = 0;
            decimal TotMay_Amount = 0;
            decimal TotJun_Amount = 0;
            decimal TotJul_Amount = 0;
            decimal TotAug_Amount = 0;
            decimal TotSep_Amount = 0;
            decimal TotOct_Amount = 0;
            decimal TotNov_Amount = 0;
            decimal TotDec_Amount = 0;
            decimal TotJan_Amount = 0;
            decimal TotFeb_Amount = 0;
            decimal TotMar_Amount = 0;
            foreach (GridViewRow gr in GridView1.Rows)
            {
                TextBox Apr_Amount = (TextBox)gr.FindControl("txtApr_Amount");
                TextBox May_Amount = (TextBox)gr.FindControl("txtMay_Amount");
                TextBox Jun_Amount = (TextBox)gr.FindControl("txtJun_Amount");
                TextBox Jul_Amount = (TextBox)gr.FindControl("txtJul_Amount");
                TextBox Aug_Amount = (TextBox)gr.FindControl("txtAug_Amount");
                TextBox Sep_Amount = (TextBox)gr.FindControl("txtSep_Amount");
                TextBox Oct_Amount = (TextBox)gr.FindControl("txtOct_Amount");
                TextBox Nov_Amount = (TextBox)gr.FindControl("txtNov_Amount");
                TextBox Dec_Amount = (TextBox)gr.FindControl("txtDec_Amount");
                TextBox Jan_Amount = (TextBox)gr.FindControl("txtJan_Amount");
                TextBox Feb_Amount = (TextBox)gr.FindControl("txtFeb_Amount");
                TextBox Mar_Amount = (TextBox)gr.FindControl("txtMar_Amount");

                if (Apr_Amount.Text == "" || !objdb.isDecimal(Apr_Amount.Text))
                {
                    Apr_Amount.Text = "0.00";
                }

                if (May_Amount.Text == "" || !objdb.isDecimal(May_Amount.Text))
                {
                    May_Amount.Text = "0.00";
                }
                if (Jun_Amount.Text == "" || !objdb.isDecimal(Jun_Amount.Text))
                {
                    Jun_Amount.Text = "0.00";
                }
                if (Jul_Amount.Text == "" || !objdb.isDecimal(Jul_Amount.Text))
                {
                    Jul_Amount.Text = "0.00";
                }
                if (Aug_Amount.Text == "" || !objdb.isDecimal(Aug_Amount.Text))
                {
                    Aug_Amount.Text = "0.00";
                }
                if (Sep_Amount.Text == "" || !objdb.isDecimal(Sep_Amount.Text))
                {
                    Sep_Amount.Text = "0.00";
                }
                if (Oct_Amount.Text == "" || !objdb.isDecimal(Oct_Amount.Text))
                {
                    Oct_Amount.Text = "0.00";
                }
                if (Nov_Amount.Text == "" || !objdb.isDecimal(Nov_Amount.Text))
                {
                    Nov_Amount.Text = "0.00";
                }
                if (Dec_Amount.Text == "" || !objdb.isDecimal(Dec_Amount.Text))
                {
                    Dec_Amount.Text = "0.00";
                }
                if (Jan_Amount.Text == "" || !objdb.isDecimal(Jan_Amount.Text))
                {
                    Jan_Amount.Text = "0.00";
                }
                if (Feb_Amount.Text == "" || !objdb.isDecimal(Feb_Amount.Text))
                {
                    Feb_Amount.Text = "0.00";
                }
                if (Mar_Amount.Text == "" || !objdb.isDecimal(Mar_Amount.Text))
                {
                    Mar_Amount.Text = "0.00";
                }

                TotApr_Amount = TotApr_Amount + decimal.Parse(Apr_Amount.Text);
                TotMay_Amount = TotMay_Amount + decimal.Parse(May_Amount.Text);
                TotJun_Amount = TotJun_Amount + decimal.Parse(Jun_Amount.Text);
                TotJul_Amount = TotJul_Amount + decimal.Parse(Jul_Amount.Text);
                TotAug_Amount = TotAug_Amount + decimal.Parse(Aug_Amount.Text);
                TotSep_Amount = TotSep_Amount + decimal.Parse(Sep_Amount.Text);
                TotOct_Amount = TotOct_Amount + decimal.Parse(Oct_Amount.Text);
                TotNov_Amount = TotNov_Amount + decimal.Parse(Nov_Amount.Text);
                TotDec_Amount = TotDec_Amount + decimal.Parse(Dec_Amount.Text);
                TotJan_Amount = TotJan_Amount + decimal.Parse(Jan_Amount.Text);
                TotFeb_Amount = TotFeb_Amount + decimal.Parse(Feb_Amount.Text);
                TotMar_Amount = TotMar_Amount + decimal.Parse(Mar_Amount.Text);
            }
            //txtEarTotApr.Text = TotApr_Amount.ToString();
            //txtEarTotMay.Text = TotMay_Amount.ToString();
            //txtEarTotJun.Text = TotJun_Amount.ToString();
            //txtEarTotJul.Text = TotJul_Amount.ToString();
            //txtEarTotAug.Text = TotAug_Amount.ToString();
            //txtEarTotSep.Text = TotSep_Amount.ToString();
            //txtEarTotOct.Text = TotOct_Amount.ToString();
            //txtEarTotNov.Text = TotNov_Amount.ToString();
            //txtEarTotDec.Text = TotDec_Amount.ToString();
            //txtEarTotJan.Text = TotJan_Amount.ToString();
            //txtEarTotFeb.Text = TotFeb_Amount.ToString();
            //txtEarTotMar.Text = TotMar_Amount.ToString();

            TotApr_Amount = TotApr_Amount + decimal.Parse(txtEarTotApr.Text);
            TotMay_Amount = TotMay_Amount + decimal.Parse(txtEarTotMay.Text);
            TotJun_Amount = TotJun_Amount + decimal.Parse(txtEarTotJun.Text);
            TotJul_Amount = TotJul_Amount + decimal.Parse(txtEarTotJul.Text);
            TotAug_Amount = TotAug_Amount + decimal.Parse(txtEarTotAug.Text);
            TotSep_Amount = TotSep_Amount + decimal.Parse(txtEarTotSep.Text);
            TotOct_Amount = TotOct_Amount + decimal.Parse(txtEarTotOct.Text);
            TotNov_Amount = TotNov_Amount + decimal.Parse(txtEarTotNov.Text);
            TotDec_Amount = TotDec_Amount + decimal.Parse(txtEarTotDec.Text);
            TotJan_Amount = TotJan_Amount + decimal.Parse(txtEarTotJan.Text);
            TotFeb_Amount = TotFeb_Amount + decimal.Parse(txtEarTotFeb.Text);
            TotMar_Amount = TotMar_Amount + decimal.Parse(txtEarTotMar.Text);

            txtGrossToApr.Text = TotApr_Amount.ToString();
            txtGrossToMay.Text = TotMay_Amount.ToString();
            txtGrossToJun.Text = TotJun_Amount.ToString();
            txtGrossToJul.Text = TotJul_Amount.ToString();
            txtGrossToAug.Text = TotAug_Amount.ToString();
            txtGrossToSep.Text = TotSep_Amount.ToString();
            txtGrossToOct.Text = TotOct_Amount.ToString();
            txtGrossToNov.Text = TotNov_Amount.ToString();
            txtGrossToDec.Text = TotDec_Amount.ToString();
            txtGrossToJan.Text = TotJan_Amount.ToString();
            txtGrossToFeb.Text = TotFeb_Amount.ToString();
            txtGrossToMar.Text = TotMar_Amount.ToString();
            #endregion
            // Deduction Detail Total
            #region Deduction Total
            TotApr_Amount = 0;
            TotMay_Amount = 0;
            TotJun_Amount = 0;
            TotJul_Amount = 0;
            TotAug_Amount = 0;
            TotSep_Amount = 0;
            TotOct_Amount = 0;
            TotNov_Amount = 0;
            TotDec_Amount = 0;
            TotJan_Amount = 0;
            TotFeb_Amount = 0;
            TotMar_Amount = 0;

            foreach (GridViewRow gr in GridView2.Rows)
            {
                TextBox Apr_Amount = (TextBox)gr.FindControl("txtApr_Amount");
                TextBox May_Amount = (TextBox)gr.FindControl("txtMay_Amount");
                TextBox Jun_Amount = (TextBox)gr.FindControl("txtJun_Amount");
                TextBox Jul_Amount = (TextBox)gr.FindControl("txtJul_Amount");
                TextBox Aug_Amount = (TextBox)gr.FindControl("txtAug_Amount");
                TextBox Sep_Amount = (TextBox)gr.FindControl("txtSep_Amount");
                TextBox Oct_Amount = (TextBox)gr.FindControl("txtOct_Amount");
                TextBox Nov_Amount = (TextBox)gr.FindControl("txtNov_Amount");
                TextBox Dec_Amount = (TextBox)gr.FindControl("txtDec_Amount");
                TextBox Jan_Amount = (TextBox)gr.FindControl("txtJan_Amount");
                TextBox Feb_Amount = (TextBox)gr.FindControl("txtFeb_Amount");
                TextBox Mar_Amount = (TextBox)gr.FindControl("txtMar_Amount");

                if (Apr_Amount.Text == "" || !objdb.isDecimal(Apr_Amount.Text))
                {
                    Apr_Amount.Text = "0.00";
                }

                if (May_Amount.Text == "" || !objdb.isDecimal(May_Amount.Text))
                {
                    May_Amount.Text = "0.00";
                }
                if (Jun_Amount.Text == "" || !objdb.isDecimal(Jun_Amount.Text))
                {
                    Jun_Amount.Text = "0.00";
                }
                if (Jul_Amount.Text == "" || !objdb.isDecimal(Jul_Amount.Text))
                {
                    Jul_Amount.Text = "0.00";
                }
                if (Aug_Amount.Text == "" || !objdb.isDecimal(Aug_Amount.Text))
                {
                    Aug_Amount.Text = "0.00";
                }
                if (Sep_Amount.Text == "" || !objdb.isDecimal(Sep_Amount.Text))
                {
                    Sep_Amount.Text = "0.00";
                }
                if (Oct_Amount.Text == "" || !objdb.isDecimal(Oct_Amount.Text))
                {
                    Oct_Amount.Text = "0.00";
                }
                if (Nov_Amount.Text == "" || !objdb.isDecimal(Nov_Amount.Text))
                {
                    Nov_Amount.Text = "0.00";
                }
                if (Dec_Amount.Text == "" || !objdb.isDecimal(Dec_Amount.Text))
                {
                    Dec_Amount.Text = "0.00";
                }
                if (Jan_Amount.Text == "" || !objdb.isDecimal(Jan_Amount.Text))
                {
                    Jan_Amount.Text = "0.00";
                }
                if (Feb_Amount.Text == "" || !objdb.isDecimal(Feb_Amount.Text))
                {
                    Feb_Amount.Text = "0.00";
                }
                if (Mar_Amount.Text == "" || !objdb.isDecimal(Mar_Amount.Text))
                {
                    Mar_Amount.Text = "0.00";
                }

                TotApr_Amount = TotApr_Amount + decimal.Parse(Apr_Amount.Text);
                TotMay_Amount = TotMay_Amount + decimal.Parse(May_Amount.Text);
                TotJun_Amount = TotJun_Amount + decimal.Parse(Jun_Amount.Text);
                TotJul_Amount = TotJul_Amount + decimal.Parse(Jul_Amount.Text);
                TotAug_Amount = TotAug_Amount + decimal.Parse(Aug_Amount.Text);
                TotSep_Amount = TotSep_Amount + decimal.Parse(Sep_Amount.Text);
                TotOct_Amount = TotOct_Amount + decimal.Parse(Oct_Amount.Text);
                TotNov_Amount = TotNov_Amount + decimal.Parse(Nov_Amount.Text);
                TotDec_Amount = TotDec_Amount + decimal.Parse(Dec_Amount.Text);
                TotJan_Amount = TotJan_Amount + decimal.Parse(Jan_Amount.Text);
                TotFeb_Amount = TotFeb_Amount + decimal.Parse(Feb_Amount.Text);
                TotMar_Amount = TotMar_Amount + decimal.Parse(Mar_Amount.Text);
            }



            if(GridView2.Rows.Count  >0)
            {
                TextBox FApr_Amount = (TextBox)GridView2.FooterRow.FindControl("txtApr_Amount");
                TextBox FMay_Amount = (TextBox)GridView2.FooterRow.FindControl("txtMay_Amount");
                TextBox FJun_Amount = (TextBox)GridView2.FooterRow.FindControl("txtJun_Amount");
                TextBox FJul_Amount = (TextBox)GridView2.FooterRow.FindControl("txtJul_Amount");
                TextBox FAug_Amount = (TextBox)GridView2.FooterRow.FindControl("txtAug_Amount");
                TextBox FSep_Amount = (TextBox)GridView2.FooterRow.FindControl("txtSep_Amount");
                TextBox FOct_Amount = (TextBox)GridView2.FooterRow.FindControl("txtOct_Amount");
                TextBox FNov_Amount = (TextBox)GridView2.FooterRow.FindControl("txtNov_Amount");
                TextBox FDec_Amount = (TextBox)GridView2.FooterRow.FindControl("txtDec_Amount");
                TextBox FJan_Amount = (TextBox)GridView2.FooterRow.FindControl("txtJan_Amount");
                TextBox FFeb_Amount = (TextBox)GridView2.FooterRow.FindControl("txtFeb_Amount");
                TextBox FMar_Amount = (TextBox)GridView2.FooterRow.FindControl("txtMar_Amount");

                if (FApr_Amount.Text == "" || !objdb.isDecimal(FApr_Amount.Text))
                {
                    FApr_Amount.Text = "0.00";
                }

                if (FMay_Amount.Text == "" || !objdb.isDecimal(FMay_Amount.Text))
                {
                    FMay_Amount.Text = "0.00";
                }
                if (FJun_Amount.Text == "" || !objdb.isDecimal(FJun_Amount.Text))
                {
                    FJun_Amount.Text = "0.00";
                }
                if (FJul_Amount.Text == "" || !objdb.isDecimal(FJul_Amount.Text))
                {
                    FJul_Amount.Text = "0.00";
                }
                if (FAug_Amount.Text == "" || !objdb.isDecimal(FAug_Amount.Text))
                {
                    FAug_Amount.Text = "0.00";
                }
                if (FSep_Amount.Text == "" || !objdb.isDecimal(FSep_Amount.Text))
                {
                    FSep_Amount.Text = "0.00";
                }
                if (FOct_Amount.Text == "" || !objdb.isDecimal(FOct_Amount.Text))
                {
                    FOct_Amount.Text = "0.00";
                }
                if (FNov_Amount.Text == "" || !objdb.isDecimal(FNov_Amount.Text))
                {
                    FNov_Amount.Text = "0.00";
                }
                if (FDec_Amount.Text == "" || !objdb.isDecimal(FDec_Amount.Text))
                {
                    FDec_Amount.Text = "0.00";
                }
                if (FJan_Amount.Text == "" || !objdb.isDecimal(FJan_Amount.Text))
                {
                    FJan_Amount.Text = "0.00";
                }
                if (FFeb_Amount.Text == "" || !objdb.isDecimal(FFeb_Amount.Text))
                {
                    FFeb_Amount.Text = "0.00";
                }
                if (FMar_Amount.Text == "" || !objdb.isDecimal(FMar_Amount.Text))
                {
                    FMar_Amount.Text = "0.00";
                }

                TotApr_Amount = TotApr_Amount + decimal.Parse(FApr_Amount.Text);
                TotMay_Amount = TotMay_Amount + decimal.Parse(FMay_Amount.Text);
                TotJun_Amount = TotJun_Amount + decimal.Parse(FJun_Amount.Text);
                TotJul_Amount = TotJul_Amount + decimal.Parse(FJul_Amount.Text);
                TotAug_Amount = TotAug_Amount + decimal.Parse(FAug_Amount.Text);
                TotSep_Amount = TotSep_Amount + decimal.Parse(FSep_Amount.Text);
                TotOct_Amount = TotOct_Amount + decimal.Parse(FOct_Amount.Text);
                TotNov_Amount = TotNov_Amount + decimal.Parse(FNov_Amount.Text);
                TotDec_Amount = TotDec_Amount + decimal.Parse(FDec_Amount.Text);
                TotJan_Amount = TotJan_Amount + decimal.Parse(FJan_Amount.Text);
                TotFeb_Amount = TotFeb_Amount + decimal.Parse(FFeb_Amount.Text);
                TotMar_Amount = TotMar_Amount + decimal.Parse(FMar_Amount.Text);





                txtDedTotApr.Text = TotApr_Amount.ToString();
                txtDedTotMay.Text = TotMay_Amount.ToString();
                txtDedTotJun.Text = TotJun_Amount.ToString();
                txtDedTotJul.Text = TotJul_Amount.ToString();
                txtDedTotAug.Text = TotAug_Amount.ToString();
                txtDedTotSep.Text = TotSep_Amount.ToString();
                txtDedTotOct.Text = TotOct_Amount.ToString();
                txtDedTotNov.Text = TotNov_Amount.ToString();
                txtDedTotDec.Text = TotDec_Amount.ToString();
                txtDedTotJan.Text = TotJan_Amount.ToString();
                txtDedTotFeb.Text = TotFeb_Amount.ToString();
                txtDedTotMar.Text = TotMar_Amount.ToString();
            }
           
            #endregion
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Clear()
    {
        lblBank_AccountNo.Text = "";
        lblSalary_NetSalary.Text = "";
        lblBank_Name.Text = "";
        lblIFSCCode.Text = "";
        lblGroupInsurance_No.Text = "";
        lblEPF_No.Text = "";

        // Earning Detail Total
        txtEarTotApr.Text = "";
        txtEarTotMay.Text = "";
        txtEarTotJun.Text = "";
        txtEarTotJul.Text = "";
        txtEarTotAug.Text = "";
        txtEarTotSep.Text = "";
        txtEarTotOct.Text = "";
        txtEarTotNov.Text = "";
        txtEarTotDec.Text = "";
        txtEarTotJan.Text = "";
        txtEarTotFeb.Text = "";
        txtEarTotMar.Text = "";
        // Deduction Detail Total
        txtDedTotApr.Text = "";
        txtDedTotMay.Text = "";
        txtDedTotJun.Text = "";
        txtDedTotJul.Text = "";
        txtDedTotAug.Text = "";
        txtDedTotSep.Text = "";
        txtDedTotOct.Text = "";
        txtDedTotNov.Text = "";
        txtDedTotDec.Text = "";
        txtDedTotJan.Text = "";
        txtDedTotFeb.Text = "";
        txtDedTotMar.Text = "";


    }
    protected void MonthWiseReport(string MonthNo)
    {
        try
        {
            GridView3.DataSource = null;
            GridView3.DataBind();
            if (ddlYear.SelectedIndex > 0 && ddlOfficeName.SelectedIndex > 0 && ddlEmployee.SelectedIndex > 0)
            {

                ds = objdb.ByProcedure("SpPayrollPolicyMonthWsDetail", new string[] { "flag", "Emp_ID", "Year", "MonthNo" }, new string[] { "0", ddlEmployee.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), MonthNo }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    GridView3.DataSource = ds;
                    GridView3.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MonthDetail();", true);
                }
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
            if (ddlYear.SelectedIndex > 0 && ddlOfficeName.SelectedIndex > 0 && ddlEmployee.SelectedIndex > 0)
            {
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnApr_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("4");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnMay_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("5");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnJun_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("6");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnJul_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("7");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnAug_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("8");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSep_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("9");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnOct_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("10");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnNov_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("11");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnDec_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("12");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnJan_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("1");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnFeb_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("2");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnMar_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("3");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            divDetail.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlEmployee.SelectedIndex == 0)
            {
                msg += "Select Employee. \\n";
            }
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year. \\n";
            }
            if (msg.Trim() == "")
            {
                string Year = ddlYear.SelectedValue.ToString();

                string Employee = ddlEmployee.SelectedValue.ToString();
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    TextBox Apr_Amount = (TextBox)gr.FindControl("txtApr_Amount");
                    TextBox May_Amount = (TextBox)gr.FindControl("txtMay_Amount");
                    TextBox Jun_Amount = (TextBox)gr.FindControl("txtJun_Amount");
                    TextBox Jul_Amount = (TextBox)gr.FindControl("txtJul_Amount");
                    TextBox Aug_Amount = (TextBox)gr.FindControl("txtAug_Amount");
                    TextBox Sep_Amount = (TextBox)gr.FindControl("txtSep_Amount");
                    TextBox Oct_Amount = (TextBox)gr.FindControl("txtOct_Amount");
                    TextBox Nov_Amount = (TextBox)gr.FindControl("txtNov_Amount");
                    TextBox Dec_Amount = (TextBox)gr.FindControl("txtDec_Amount");
                    TextBox Jan_Amount = (TextBox)gr.FindControl("txtJan_Amount");
                    TextBox Feb_Amount = (TextBox)gr.FindControl("txtFeb_Amount");
                    TextBox Mar_Amount = (TextBox)gr.FindControl("txtMar_Amount");
                    Label lblEarnDeduction_ID = (Label)gr.FindControl("lblEarnDeduction_ID");
                    Label lblCalculation_Type = (Label)gr.FindControl("lblCalculation_Type");
                    if (Apr_Amount.Text == "")
                    {
                        Apr_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Apr_Amount.Text))
                        {
                            Apr_Amount.Text = "0.00";
                        }
                    }

                    if (May_Amount.Text == "")
                    {
                        May_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(May_Amount.Text))
                        {
                            May_Amount.Text = "0.00";
                        }
                    }
                    if (Jun_Amount.Text == "")
                    {
                        Jun_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Jun_Amount.Text))
                        {
                            Jun_Amount.Text = "0.00";
                        }
                    }
                    if (Jul_Amount.Text == "")
                    {
                        Jul_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Jul_Amount.Text))
                        {
                            Jul_Amount.Text = "0.00";
                        }
                    }
                    if (Aug_Amount.Text == "")
                    {
                        Aug_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Aug_Amount.Text))
                        {
                            Aug_Amount.Text = "0.00";
                        }
                    }
                    if (Sep_Amount.Text == "")
                    {
                        Sep_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Sep_Amount.Text))
                        {
                            Sep_Amount.Text = "0.00";
                        }
                    }
                    if (Oct_Amount.Text == "")
                    {
                        Oct_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Oct_Amount.Text))
                        {
                            Oct_Amount.Text = "0.00";
                        }
                    }
                    if (Nov_Amount.Text == "")
                    {
                        Nov_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Nov_Amount.Text))
                        {
                            Nov_Amount.Text = "0.00";
                        }
                    }
                    if (Dec_Amount.Text == "")
                    {
                        Dec_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Dec_Amount.Text))
                        {
                            Dec_Amount.Text = "0.00";
                        }
                    }
                    if (Jan_Amount.Text == "")
                    {
                        Jan_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Jan_Amount.Text))
                        {
                            Jan_Amount.Text = "0.00";
                        }
                    }
                    if (Feb_Amount.Text == "")
                    {
                        Feb_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Feb_Amount.Text))
                        {
                            Feb_Amount.Text = "0.00";
                        }
                    }
                    if (Mar_Amount.Text == "")
                    {
                        Mar_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Mar_Amount.Text))
                        {
                            Mar_Amount.Text = "0.00";
                        }
                    }


                    //objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY",
                    //   new string[] { "flag", "Emp_ID", "Year", "EarnDeduction_ID", "EarnDeduction_Type", "Calculation_Type", "Apr_Amount", "May_Amount", "Jun_Amount", "Jul_Amount", "Aug_Amount", "Sep_Amount", "Oct_Amount", "Nov_Amount", "Dec_Amount", "Jan_Amount", "Feb_Amount", "Mar_Amount", "UpdatedBy" },
                    //         new string[] { "0", Employee, Year, lblEarnDeduction_ID.Text, "Earning", lblCalculation_Type.Text, Apr_Amount.Text, May_Amount.Text, Jun_Amount.Text, Jul_Amount.Text, Aug_Amount.Text, Sep_Amount.Text, Oct_Amount.Text, Nov_Amount.Text, Dec_Amount.Text, Jan_Amount.Text, Feb_Amount.Text, Mar_Amount.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    string Year_2 = (Int32.Parse(Year) + 1).ToString();
                    objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY",
                       new string[] { "flag", "Emp_ID", "Year", "EarnDeduction_ID", "EarnDeduction_Type", "Calculation_Type", "Apr_Amount", "May_Amount", "Jun_Amount", "Jul_Amount", "Aug_Amount", "Sep_Amount", "Oct_Amount", "Nov_Amount", "Dec_Amount", "UpdatedBy" },
                       new string[] { "0", Employee, Year, lblEarnDeduction_ID.Text, "Earning", lblCalculation_Type.Text, Apr_Amount.Text, May_Amount.Text, Jun_Amount.Text, Jul_Amount.Text, Aug_Amount.Text, Sep_Amount.Text, Oct_Amount.Text, Nov_Amount.Text, Dec_Amount.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY",
                      new string[] { "flag", "Emp_ID", "Year", "EarnDeduction_ID", "EarnDeduction_Type", "Calculation_Type", "Jan_Amount", "Feb_Amount", "Mar_Amount", "UpdatedBy" },
                      new string[] { "3", Employee, Year_2, lblEarnDeduction_ID.Text, "Earning", lblCalculation_Type.Text, Jan_Amount.Text, Feb_Amount.Text, Mar_Amount.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                }
                foreach (GridViewRow gr in GridView2.Rows)
                {
                    TextBox Apr_Amount = (TextBox)gr.FindControl("txtApr_Amount");
                    TextBox May_Amount = (TextBox)gr.FindControl("txtMay_Amount");
                    TextBox Jun_Amount = (TextBox)gr.FindControl("txtJun_Amount");
                    TextBox Jul_Amount = (TextBox)gr.FindControl("txtJul_Amount");
                    TextBox Aug_Amount = (TextBox)gr.FindControl("txtAug_Amount");
                    TextBox Sep_Amount = (TextBox)gr.FindControl("txtSep_Amount");
                    TextBox Oct_Amount = (TextBox)gr.FindControl("txtOct_Amount");
                    TextBox Nov_Amount = (TextBox)gr.FindControl("txtNov_Amount");
                    TextBox Dec_Amount = (TextBox)gr.FindControl("txtDec_Amount");
                    TextBox Jan_Amount = (TextBox)gr.FindControl("txtJan_Amount");
                    TextBox Feb_Amount = (TextBox)gr.FindControl("txtFeb_Amount");
                    TextBox Mar_Amount = (TextBox)gr.FindControl("txtMar_Amount");
                    Label lblEarnDeduction_ID = (Label)gr.FindControl("lblEarnDeduction_ID");
                    Label lblCalculation_Type = (Label)gr.FindControl("lblCalculation_Type");
                    if (Apr_Amount.Text == "")
                    {
                        Apr_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Apr_Amount.Text))
                        {
                            Apr_Amount.Text = "0.00";
                        }
                    }

                    if (May_Amount.Text == "")
                    {
                        May_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(May_Amount.Text))
                        {
                            May_Amount.Text = "0.00";
                        }
                    }
                    if (Jun_Amount.Text == "")
                    {
                        Jun_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Jun_Amount.Text))
                        {
                            Jun_Amount.Text = "0.00";
                        }
                    }
                    if (Jul_Amount.Text == "")
                    {
                        Jul_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Jul_Amount.Text))
                        {
                            Jul_Amount.Text = "0.00";
                        }
                    }
                    if (Aug_Amount.Text == "")
                    {
                        Aug_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Aug_Amount.Text))
                        {
                            Aug_Amount.Text = "0.00";
                        }
                    }
                    if (Sep_Amount.Text == "")
                    {
                        Sep_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Sep_Amount.Text))
                        {
                            Sep_Amount.Text = "0.00";
                        }
                    }
                    if (Oct_Amount.Text == "")
                    {
                        Oct_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Oct_Amount.Text))
                        {
                            Oct_Amount.Text = "0.00";
                        }
                    }
                    if (Nov_Amount.Text == "")
                    {
                        Nov_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Nov_Amount.Text))
                        {
                            Nov_Amount.Text = "0.00";
                        }
                    }
                    if (Dec_Amount.Text == "")
                    {
                        Dec_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Dec_Amount.Text))
                        {
                            Dec_Amount.Text = "0.00";
                        }
                    }
                    if (Jan_Amount.Text == "")
                    {
                        Jan_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Jan_Amount.Text))
                        {
                            Jan_Amount.Text = "0.00";
                        }
                    }
                    if (Feb_Amount.Text == "")
                    {
                        Feb_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Feb_Amount.Text))
                        {
                            Feb_Amount.Text = "0.00";
                        }
                    }
                    if (Mar_Amount.Text == "")
                    {
                        Mar_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Mar_Amount.Text))
                        {
                            Mar_Amount.Text = "0.00";
                        }
                    }


                    //objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY",
                    //   new string[] { "flag", "Emp_ID", "Year", "EarnDeduction_ID", "EarnDeduction_Type", "Calculation_Type", "Apr_Amount", "May_Amount", "Jun_Amount", "Jul_Amount", "Aug_Amount", "Sep_Amount", "Oct_Amount", "Nov_Amount", "Dec_Amount", "Jan_Amount", "Feb_Amount", "Mar_Amount", "UpdatedBy" },
                    //         new string[] { "0", Employee, Year, lblEarnDeduction_ID.Text, "Deduction", lblCalculation_Type.Text, Apr_Amount.Text, May_Amount.Text, Jun_Amount.Text, Jul_Amount.Text, Aug_Amount.Text, Sep_Amount.Text, Oct_Amount.Text, Nov_Amount.Text, Dec_Amount.Text, Jan_Amount.Text, Feb_Amount.Text, Mar_Amount.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    string Year_2 = (Int32.Parse(Year) + 1).ToString();
                    objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY",
                       new string[] { "flag", "Emp_ID", "Year", "EarnDeduction_ID", "EarnDeduction_Type", "Calculation_Type", "Apr_Amount", "May_Amount", "Jun_Amount", "Jul_Amount", "Aug_Amount", "Sep_Amount", "Oct_Amount", "Nov_Amount", "Dec_Amount", "UpdatedBy" },
                             new string[] { "0", Employee, Year, lblEarnDeduction_ID.Text, "Deduction", lblCalculation_Type.Text, Apr_Amount.Text, May_Amount.Text, Jun_Amount.Text, Jul_Amount.Text, Aug_Amount.Text, Sep_Amount.Text, Oct_Amount.Text, Nov_Amount.Text, Dec_Amount.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY",
                      new string[] { "flag", "Emp_ID", "Year", "EarnDeduction_ID", "EarnDeduction_Type", "Calculation_Type", "Jan_Amount", "Feb_Amount", "Mar_Amount", "UpdatedBy" },
                            new string[] { "3", Employee, Year_2, lblEarnDeduction_ID.Text, "Deduction", lblCalculation_Type.Text, Jan_Amount.Text, Feb_Amount.Text, Mar_Amount.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                }
                SavePolicyDetail();

                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void SavePolicyDetail()
    {
        try
        {

            string Year = ddlYear.SelectedValue.ToString();
            string Year_2 = (Int32.Parse(Year) + 1).ToString();
            string Employee = ddlEmployee.SelectedValue.ToString();

            string Monthcount = ViewState["Monthcount"].ToString();
            string[] monthNo = Monthcount.Split(',');
            for (int j = 1; j <= 12; j++)
            {
                string k = "0";
                for (int i = 0; i < monthNo.Length - 1; i++)
                {
                    int aa = int.Parse(monthNo[i]);
                    if (aa == j)
                    {
                        k = "1";
                    }
                }
                if (k == "0")
                {
                    //objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY_FY",
                    //  new string[] { "flag", "Emp_ID", "Office_ID", "Salary_Year", "PolicyDed_UpdatedBy" },
                    //  new string[] { j.ToString(), Employee, ViewState["Office_ID"].ToString(), Year, ViewState["Emp_ID"].ToString() }, "dataset");
                    string Year_FY = "";
                    if (j < 4)
                        Year_FY = Year_2.ToString();
                    else
                        Year_FY = Year.ToString();

                    objdb.ByProcedure("SpPayrollPolicyDeduction",
                      new string[] { "flag", "Emp_ID", "Office_ID", "Salary_Year", "PolicyDed_UpdatedBy" },
                      new string[] { j.ToString(), Employee, ViewState["Office_ID"].ToString(), Year_FY, ViewState["Emp_ID"].ToString() }, "dataset");
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    /*******New PP**********/
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {

        ViewState["Office_ID"] = ddlOfficeName.SelectedItem.Value;
        divDetail.Visible = false;
        FillDropdown();
        FillGrid();

    }
}