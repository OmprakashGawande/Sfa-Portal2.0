using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class mis_Payroll_PayrollMonthlyPayBillReportNew : System.Web.UI.Page
{

    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                GetYear();
                GetOffice();
                GetSection();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    private void GetYear()
    {
        DataSet ds3 = new DataSet();
        try
        {

            ds3 = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "8" }, "dataset");
            if (ds3 != null)
            {
                if (ds3.Tables.Count > 0)
                {
                    if (ds3.Tables[0].Rows.Count > 0)
                    {
                        ddlFinancialYear.DataSource = ds3.Tables[0];
                        ddlFinancialYear.DataTextField = "Year";
                        ddlFinancialYear.DataValueField = "Year";
                        ddlFinancialYear.DataBind();
                        ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Section Bind!", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }
    private void GetOffice()
    {
        DataSet ds2 = new DataSet();
        try
        {

            ds2 = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "10", }, "dataset");
            if (ds2 != null)
            {
                if (ds2.Tables.Count > 0)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        ddlOffice.DataSource = ds2.Tables[0];
                        ddlOffice.DataTextField = "Office_Name";
                        ddlOffice.DataValueField = "Office_ID";
                        ddlOffice.DataBind();
                        ddlOffice.Enabled = false;
                        if (objdb.createdBy() == "1" || objdb.createdBy() == "1586")
                        {
                            ddlOffice.Enabled = true;
                        }
                        ddlOffice.SelectedValue = objdb.Office_ID();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Section Bind!", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }

    private void GetSection()
    {
        DataSet ds1 = new DataSet();
        try
        {

            ds1 = objdb.ByProcedure("SpPayrollSalaryFinalSummary", new string[] { "flag", "Office_ID" }, new string[] { "4", ddlOffice.SelectedValue }, "dataset");
            ddlSection.Items.Clear();
            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlSection.DataSource = ds1.Tables[0];
                        ddlSection.DataTextField = "SalarySec_No";
                        ddlSection.DataValueField = "SalarySec_No";
                        ddlSection.DataBind();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Section Bind!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSection();
    }
    private void GetMonthlyDetails()
    {
        lblMsg.Text = string.Empty;
        string subsectionid = "", multisubsection = "";
        int subsectindata = 0;
        foreach (ListItem itemss in ddlSection.Items)
        {
            if (itemss.Selected)
            {

                subsectionid = itemss.Value;

                ++subsectindata;
                if (subsectindata == 1)
                {
                    multisubsection = subsectionid;

                }
                else
                {
                    multisubsection += "," + subsectionid;

                }
            }
        }
        // end of by pawan on 13-01-2023
        if (subsectindata > 0)
        {
            DivDetail.InnerHtml = "";
            pnlprint.Visible = false;
            DivHead.Visible = false;

            DataSet ds4 = objdb.ByProcedure("USP_PayrollMonthlyPaybillReport",
                new string[] { "Office_ID", "Salary_MonthNo", "Salary_Year"
                          , "Emp_TypeOfPost", "MultiSalarySec_No", "SalaryType" },
                new string[] { ddlOffice.SelectedValue,ddlMonth.SelectedValue,ddlFinancialYear.SelectedValue
                           ,ddlEmp_TypeOfPost.SelectedValue,multisubsection,rbnlist.SelectedValue }, "dataset");
            if (ds4 != null)
            {
                if (ds4.Tables.Count > 0)
                {
                    if (ds4.Tables[0].Rows.Count > 0)
                    {
                        string Office_ID = ddlOffice.SelectedValue.ToString();
                        string Salary_Year = ddlFinancialYear.SelectedValue.ToString();
                        string Salary_MonthNo = ddlMonth.SelectedValue.ToString();

                        lblSession.Text = ddlMonth.SelectedItem.ToString() + " " + ddlFinancialYear.SelectedValue.ToString();
                        lblPosttype.Text = ddlEmp_TypeOfPost.SelectedValue;
                        // earning heads name
                        DataTable dtEarning = ds4.Tables[0].AsEnumerable()
                       .Where(r =>r.Field<string>("EarnDeduction_Type") == "Earning")   
                       .GroupBy(a => new
                       {
                           EarnDeduction_Name = a.Field<string>("EarnDeduction_Name"),
                           SequenceNo = a.Field<int>("SequenceNo"),
                           EarnDeduction_Type = a.Field<string>("EarnDeduction_Type")
                       }).OrderBy(g => g.Key.SequenceNo)
                       .Select(b =>
                       {
                           DataRow row = ds4.Tables[0].NewRow();
                           row["EarnDeduction_Name"] = b.Key.EarnDeduction_Name;
                           row["EarnDeduction_Type"] = b.Key.EarnDeduction_Type;
                           row["SequenceNo"] = b.Key.SequenceNo;
                           return row;
                       }).CopyToDataTable();

                        // deduction heads name
                        DataTable dtDeduction = ds4.Tables[0].AsEnumerable()
                       .Where(r => r.Field<string>("EarnDeduction_Type") == "Deduction")
                       .GroupBy(a => new
                       {
                           EarnDeduction_Name = a.Field<string>("EarnDeduction_Name"),
                           SequenceNo = a.Field<int>("SequenceNo"),
                           EarnDeduction_Type = a.Field<string>("EarnDeduction_Type")
                       }).OrderBy(g => g.Key.SequenceNo)
                       .Select(b =>
                       {
                           DataRow row = ds4.Tables[0].NewRow();
                           row["EarnDeduction_Name"] = b.Key.EarnDeduction_Name;
                           row["EarnDeduction_Type"] = b.Key.EarnDeduction_Type;
                           row["SequenceNo"] = b.Key.SequenceNo;
                           return row;
                       }).CopyToDataTable();

                      

                        StringBuilder htmlStr = new StringBuilder();
                        htmlStr.Append("<table class='main-heading-print' style='margin-bottom:5px; font-family: monospace; line-height: 14px;' id='SalaryTable'>");
                        htmlStr.Append("<tr class='tblheadingslip'>");
                        htmlStr.Append("<th class='text-left'>SNo.</th>");
                        htmlStr.Append("<th>Name Of Employee</th>");
                        htmlStr.Append("<th>Employee Code</th>");
                        htmlStr.Append("<th>Designation</th>");
                        htmlStr.Append("<th>PAN No.</th>");
                        htmlStr.Append("<th>IFSC Code</th>");
                        htmlStr.Append("<th>Bank Account No.</th>");
                        htmlStr.Append("<th>EPF No.</th>");
                        htmlStr.Append("<th>UAN No.</th>");
                        htmlStr.Append("<th>GI No.</th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("</tr>");
                        htmlStr.Append("<tr class='tblheadingslip'>");
                        htmlStr.Append("<th colspan='5' class='text-center'>Earning</th>");
                        htmlStr.Append("<th colspan='5' class='text-center'>Deduction</th>");
                        htmlStr.Append("<th class='text-center'>Total (Earning ,Deduction & Net Pay)</th>");
                        htmlStr.Append("</tr>");
                      
                      
                       
                        DataRow dr = dtEarning.NewRow(); //Create New Row
                        dr["EarnDeduction_Name"] = "BASIC PAY";
                        dr["EarnDeduction_Type"] = "Earning";
                        dr["SequenceNo"] = "0"; // Set Column Value
                        dtEarning.Rows.InsertAt(dr, 0);
                        dtEarning.AcceptChanges();

                        const int dcs = 5;
                         int ECount = dtEarning.Rows.Count;
                         int ECountdefault = (dtEarning.Rows.Count / dcs);
                        int DCount = dtDeduction.Rows.Count;
                        int DCountdefault = (dtDeduction.Rows.Count / dcs);
                        
                        int mainrowcount = ECountdefault > DCountdefault ? ECountdefault : DCountdefault;
                        for (int i = 0; i < mainrowcount; i++)
                        {
                            htmlStr.Append("<tr class='tblheadingslip'>");

                            int colcount = ((dcs * i));
                            for (int j = 0; j < 10;j++ )
                            {
                                if (j < dcs)
                                {
                                    if ((colcount + j)<ECount)
                                    {  
                                            htmlStr.Append("<th>" + dtEarning.Rows[colcount + j]["EarnDeduction_Name"].ToString() + "</th>");
                                    }
                                    else
                                    {
                                        htmlStr.Append("<th></th>");
                                    }
                                   
                                }
                                else
                                {
                                    if (((colcount + j) - dcs) < DCount)
                                    {
                                        htmlStr.Append("<th>" + dtDeduction.Rows[(colcount + j) - dcs]["EarnDeduction_Name"].ToString() + "</th>");
                                    }
                                    else
                                    {
                                        htmlStr.Append("<th></th>");
                                    }
                                }
                            }
                            if (i == 0)
                            {
                                htmlStr.Append("<th>Total Pay</th>");
                            }
                            else if (i == 1)
                            {
                                htmlStr.Append("<th>Total Ded</th>");
                            }
                            else if (i == 2)
                            {
                                htmlStr.Append("<th>Net Pay</th>");
                            }
                            else 
                            {
                                htmlStr.Append("<th></th>");
                            }
                            htmlStr.Append("</tr>");
                                
                        }

                        // employeelist data
                        DataTable dtTotalEmployee = ds4.Tables[0].AsEnumerable()
                       .GroupBy(a => new
                       {
                           Emp_ID = a.Field<int>("Emp_ID").ToString(),
                           Emp_Name = a.Field<string>("Emp_Name"),                          
                           Bank_AccountNo = a.Field<string>("Bank_AccountNo"),
                           Bank_IfscCode = a.Field<string>("Bank_IfscCode"),
                           EPF_No = a.Field<string>("EPF_No"),
                           UAN_No = a.Field<string>("UAN_No"),
                           GroupInsurance_No = a.Field<string>("GroupInsurance_No"),
                           Emp_PanCardNo = a.Field<string>("Emp_PanCardNo"),
                           SalaryEmp_No = a.Field<string>("SalaryEmp_No"),
                           Designation_Name = a.Field<string>("Designation_Name"),
                           OrderNo = a.Field<int>("OrderNo"),
                           Salary_Basic = a.Field<decimal>("Salary_Basic"),
                           Salary_EarningTotal = a.Field<decimal>("Salary_EarningTotal"),
                           Salary_DeductionTotal = a.Field<decimal>("Salary_DeductionTotal"),
                           Salary_NetSalary = a.Field<decimal>("Salary_NetSalary"),
                       }).OrderBy(g => g.Key.OrderNo)
                       .Select(b =>
                       {
                           DataRow row = ds4.Tables[0].NewRow();
                           row["Emp_ID"] = b.Key.Emp_ID;
                           row["Emp_Name"] = b.Key.Emp_Name;
                           row["Bank_AccountNo"] = b.Key.Bank_AccountNo;
                           row["Bank_IfscCode"] = b.Key.Bank_IfscCode;
                           row["EPF_No"] = b.Key.EPF_No;
                           row["UAN_No"] = b.Key.UAN_No;
                           row["GroupInsurance_No"] = b.Key.GroupInsurance_No;
                           row["Emp_PanCardNo"] = b.Key.Emp_PanCardNo;
                           row["SalaryEmp_No"] = b.Key.SalaryEmp_No;
                           row["Emp_ID"] = b.Key.Emp_ID;
                           row["Emp_Name"] = b.Key.Emp_Name;
                           row["Designation_Name"] = b.Key.Designation_Name;
                           row["OrderNo"] = b.Key.OrderNo;
                           row["Salary_Basic"] = b.Key.Salary_Basic;
                           row["Designation_Name"] = b.Key.Designation_Name;
                           row["Salary_EarningTotal"] = b.Key.Salary_EarningTotal;
                           row["Salary_DeductionTotal"] = b.Key.Salary_DeductionTotal;
                           row["Salary_NetSalary"] = b.Key.Salary_NetSalary;
                           return row;
                       }).CopyToDataTable();

                        htmlStr.Append("<tbody>");
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<table>");

                        string repeadtCodePrintPage = htmlStr.ToString();
                       

                      
                        string Islast_heading = "";
                        string pagebreak = "";
                       

                        int empcount = dtTotalEmployee.Rows.Count;
                        decimal EmpSalaryBasic = dtTotalEmployee.AsEnumerable().Sum(row => row.Field<decimal?>("Salary_Basic") ?? 0);
                        decimal EmpTotalPay = dtTotalEmployee.AsEnumerable().Sum(row => row.Field<decimal?>("Salary_EarningTotal") ?? 0);
                        decimal EmpTotalDeduction = dtTotalEmployee.AsEnumerable().Sum(row => row.Field<decimal?>("Salary_DeductionTotal") ?? 0);
                        decimal EmpNetPay = dtTotalEmployee.AsEnumerable().Sum(row => row.Field<decimal?>("Salary_NetSalary") ?? 0);

                        for (int k = 0; k < empcount;k++)
                        {
                            // earning employee wise heads name
                            DataTable dtEmpEarning = ds4.Tables[0].AsEnumerable()
                           .Where(r => r.Field<string>("EarnDeduction_Type") == "Earning"
                                    && r.Field<int>("Emp_ID") == Convert.ToInt32(dtTotalEmployee.Rows[k]["Emp_ID"]))
                           .GroupBy(a => new
                           {
                               EarnDeduction_Name = a.Field<string>("EarnDeduction_Name"),
                               SequenceNo = a.Field<int>("SequenceNo"),
                               EarnDeduction_Type = a.Field<string>("EarnDeduction_Type"),
                               Amount = a.Field<decimal>("Amount")

                           }).OrderBy(g => g.Key.SequenceNo)
                           .Select(b =>
                           {
                               DataRow row = ds4.Tables[0].NewRow();
                               row["EarnDeduction_Name"] = b.Key.EarnDeduction_Name;
                               row["EarnDeduction_Type"] = b.Key.EarnDeduction_Type;
                               row["SequenceNo"] = b.Key.SequenceNo;
                               row["Amount"] = b.Key.Amount;
                               return row;
                           }).CopyToDataTable();

                            DataRow drempearning = dtEmpEarning.NewRow(); //Create New Row
                            drempearning["EarnDeduction_Name"] = "BASIC PAY";
                            drempearning["EarnDeduction_Type"] = "Earning";
                            drempearning["SequenceNo"] = "0"; // Set Column Value
                            drempearning["Amount"] = dtTotalEmployee.Rows[k]["Salary_Basic"]; // Set Column Value
                            dtEmpEarning.Rows.InsertAt(drempearning, 0);
                            dtEmpEarning.AcceptChanges();

                            // deduction employee eise heads name
                            DataTable dtEmpDeduction = ds4.Tables[0].AsEnumerable()
                           .Where(r => r.Field<string>("EarnDeduction_Type") == "Deduction"
                                    && r.Field<int>("Emp_ID") == Convert.ToInt32(dtTotalEmployee.Rows[k]["Emp_ID"]))
                           .GroupBy(a => new
                           {
                               EarnDeduction_Name = a.Field<string>("EarnDeduction_Name"),
                               SequenceNo = a.Field<int>("SequenceNo"),
                               EarnDeduction_Type = a.Field<string>("EarnDeduction_Type"),
                               Amount = a.Field<decimal>("Amount"),
                               IntallmentNo = a.Field<string>("IntallmentNo"),
                               EarnDeduction_ID = a.Field<int>("EarnDeduction_ID")

                           }).OrderBy(g => g.Key.SequenceNo)
                           .Select(b =>
                           {
                               DataRow row = ds4.Tables[0].NewRow();
                               row["EarnDeduction_Name"] = b.Key.EarnDeduction_Name;
                               row["EarnDeduction_Type"] = b.Key.EarnDeduction_Type;
                               row["SequenceNo"] = b.Key.SequenceNo;
                               row["Amount"] = b.Key.Amount;
                               row["IntallmentNo"] = b.Key.IntallmentNo;
                               row["EarnDeduction_ID"] = b.Key.EarnDeduction_ID;
                               return row;
                           }).CopyToDataTable();


                            if (k == 14 || k == 30 || k == 46 || k == 62)
                            {
                                pagebreak = ".page-break";
                            }

                            htmlStr.Append("<tr class='" + pagebreak + "'>");
                            htmlStr.Append("<table  style='margin-top: 10px;background: beige;' class='" + pagebreak + "'>");
                            htmlStr.Append("<tr>");
                            htmlStr.Append("<th class='text-left'>" + (k + 1) + "</th>");
                            htmlStr.Append("<th>" + Convert.ToString(dtTotalEmployee.Rows[k]["Emp_Name"]) + "</th>");
                            htmlStr.Append("<th>" + Convert.ToString(dtTotalEmployee.Rows[k]["SalaryEmp_No"]) + "</th>");
                            htmlStr.Append("<th>" + Convert.ToString(dtTotalEmployee.Rows[k]["Designation_Name"]) + "</th>");
                            htmlStr.Append("<th>" + Convert.ToString(dtTotalEmployee.Rows[k]["Emp_PanCardNo"]) + "</th>");
                            htmlStr.Append("<th>" + Convert.ToString(dtTotalEmployee.Rows[k]["Bank_IfscCode"]) + "</th>");
                            htmlStr.Append("<th>'" +Convert.ToString(dtTotalEmployee.Rows[k]["Bank_AccountNo"]) + "'</th>");
                            htmlStr.Append("<th>" + Convert.ToString(dtTotalEmployee.Rows[k]["EPF_No"].ToString()) + "</th>");
                            htmlStr.Append("<th>" + Convert.ToString(dtTotalEmployee.Rows[k]["UAN_No"].ToString()) + "</th>");
                            htmlStr.Append("<th>" + Convert.ToString(dtTotalEmployee.Rows[k]["GroupInsurance_No"]) + "</th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("</tr>");
                            htmlStr.Append("<tr class='tblheadingslip'>");
                            htmlStr.Append("<th colspan='4' class='text-center'>Earning</th>");
                            htmlStr.Append("<th colspan='6' class='text-center'>Deduction</th>");
                            htmlStr.Append("<th class='text-center'>Total (Earning & Deduction)</th>");
                            htmlStr.Append("</tr>");
                            for (int i = 0; i < mainrowcount; i++)
                            {
                                htmlStr.Append("<tr>");

                                int colcount = ((dcs * i));
                                for (int j = 0; j < 10; j++)
                                {
                                    if (j < dcs)
                                    {
                                        if ((colcount + j) < ECount)
                                        {
                                            htmlStr.Append("<td class='alignright'>" + dtEmpEarning.Rows[colcount + j]["Amount"].ToString() + "</td>");
                                        }
                                        else
                                        {
                                            htmlStr.Append("<td class='alignright'></td>");
                                        }

                                    }
                                    else
                                    {
                                        if (((colcount + j) - dcs) < DCount)
                                        {
                                            htmlStr.Append("<td class='alignright'>" 
                                                + 
                                                (Convert.ToString(dtEmpDeduction.Rows[(colcount + j) - dcs]["IntallmentNo"]).ToString() == "" ?
                                                Convert.ToString(dtEmpDeduction.Rows[(colcount + j) - dcs]["Amount"]) :
                                                (Convert.ToString(dtEmpDeduction.Rows[(colcount + j) - dcs]["Amount"]) + "(" + 
                                                Convert.ToString(dtEmpDeduction.Rows[(colcount + j) - dcs]["IntallmentNo"]) + ")" + "</td>")));
                                        }
                                        else
                                        {
                                            htmlStr.Append("<td class='alignright'></td>");
                                        }
                                    }
                                }
                                if (i == 0)
                                {
                                    htmlStr.Append("<td class='alignright'>" + Convert.ToString(dtTotalEmployee.Rows[k]["Salary_EarningTotal"]) + "</td>");
                                }
                                else if (i == 1)
                                {
                                    htmlStr.Append("<td class='alignright'>" + Convert.ToString(dtTotalEmployee.Rows[k]["Salary_DeductionTotal"]) + "</td>");
                                }
                                else if (i == 2)
                                {
                                    htmlStr.Append("<td class='alignright'>" + Convert.ToString(dtTotalEmployee.Rows[k]["Salary_NetSalary"]) + "</td>");
                                }
                                else
                                {
                                    htmlStr.Append("<td></td>");
                                }
                                htmlStr.Append("</tr>");

                            }

                            htmlStr.Append("</table>");
                            htmlStr.Append("</tr>");

                           

                        }

                        
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<table style='margin-top: 10px;background: beige;'>");
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<th colspan='11' style='text-align: center;font-weight: 700;'>Report Summary</th>");
                        htmlStr.Append("</tr>");
                        htmlStr.Append("<tr class='tblheadingslip'>");
                        htmlStr.Append("<th colspan='5' class='text-center'>Earning</th>");
                        htmlStr.Append("<th colspan='5' class='text-center'>Deduction</th>");
                        htmlStr.Append("<th class='text-center'>Total (Earning ,Deduction & Net Pay)</th>");
                        htmlStr.Append("</tr>");

                        for (int i = 0; i < mainrowcount; i++)
                        {
                            htmlStr.Append("<tr class='tblheadingslip'>");

                            int colcount = ((dcs * i));
                            for (int j = 0; j < 10; j++)
                            {
                                if (j < dcs)
                                {
                                    if ((colcount + j) < ECount)
                                    {
                                        htmlStr.Append("<th>" + dtEarning.Rows[colcount + j]["EarnDeduction_Name"].ToString() + "</th>");
                                    }
                                    else
                                    {
                                        htmlStr.Append("<th></th>");
                                    }

                                }
                                else
                                {
                                    if (((colcount + j) - dcs) < DCount)
                                    {
                                        htmlStr.Append("<th>" + dtDeduction.Rows[(colcount + j) - dcs]["EarnDeduction_Name"].ToString() + "</th>");
                                    }
                                    else
                                    {
                                        htmlStr.Append("<th></th>");
                                    }
                                }
                            }
                            if (i == 0)
                            {
                                htmlStr.Append("<th>Total Pay</th>");
                            }
                            else if (i == 1)
                            {
                                htmlStr.Append("<th>Total Ded</th>");
                            }
                            else if (i == 2)
                            {
                                htmlStr.Append("<th>Net Pay</th>");
                            }
                            else
                            {
                                htmlStr.Append("<th></th>");
                            }
                            htmlStr.Append("</tr>");

                        }

                        // summar to earning heads name with amount
                        DataTable dtEmpTotalSummarEarning = ds4.Tables[0].AsEnumerable()
                       .Where(r => r.Field<string>("EarnDeduction_Type") == "Earning")
                       .GroupBy(a => new
                       {
                           EarnDeduction_Name = a.Field<string>("EarnDeduction_Name"),
                           SequenceNo = a.Field<int>("SequenceNo"),
                           EarnDeduction_Type = a.Field<string>("EarnDeduction_Type")
                         


                       }).OrderBy(g => g.Key.SequenceNo)
                       .Select(b =>
                       {
                           DataRow row = ds4.Tables[0].NewRow();
                           row["EarnDeduction_Name"] = b.Key.EarnDeduction_Name;
                           row["EarnDeduction_Type"] = b.Key.EarnDeduction_Type;
                           row["SequenceNo"] = b.Key.SequenceNo;
                           row["Amount"] = b.Sum(r=> r.Field<decimal>("Amount"));
                           return row;
                       }).CopyToDataTable();

                        DataRow drdtempTotalSummarEarning = dtEmpTotalSummarEarning.NewRow(); //Create New Row
                        drdtempTotalSummarEarning["EarnDeduction_Name"] = "BASIC PAY";
                        drdtempTotalSummarEarning["EarnDeduction_Type"] = "Earning";
                        drdtempTotalSummarEarning["SequenceNo"] = "0"; // Set Column Value
                        drdtempTotalSummarEarning["Amount"] = EmpSalaryBasic; // Set Column Value
                        dtEmpTotalSummarEarning.Rows.InsertAt(drdtempTotalSummarEarning, 0);
                        drdtempTotalSummarEarning.AcceptChanges();

                        // summar to deduction heads name with amount
                        DataTable dtEmpTotalSummarDeduction = ds4.Tables[0].AsEnumerable()
                       .Where(r => r.Field<string>("EarnDeduction_Type") == "Deduction")
                       .GroupBy(a => new
                       {
                           EarnDeduction_Name = a.Field<string>("EarnDeduction_Name"),
                           SequenceNo = a.Field<int>("SequenceNo"),
                           EarnDeduction_Type = a.Field<string>("EarnDeduction_Type")



                       }).OrderBy(g => g.Key.SequenceNo)
                       .Select(b =>
                       {
                           DataRow row = ds4.Tables[0].NewRow();
                           row["EarnDeduction_Name"] = b.Key.EarnDeduction_Name;
                           row["EarnDeduction_Type"] = b.Key.EarnDeduction_Type;
                           row["SequenceNo"] = b.Key.SequenceNo;
                           row["Amount"] = b.Sum(r => r.Field<decimal>("Amount"));
                           return row;
                       }).CopyToDataTable();

                        for (int i = 0; i < mainrowcount; i++)
                        {
                            htmlStr.Append("<tr>");

                            int colcount = ((dcs * i));
                            for (int j = 0; j < 10; j++)
                            {
                                if (j < dcs)
                                {
                                    if ((colcount + j) < ECount)
                                    {
                                        htmlStr.Append("<td class='alignright'>" + dtEmpTotalSummarEarning.Rows[colcount + j]["Amount"].ToString() + "</td>");
                                    }
                                    else
                                    {
                                        htmlStr.Append("<td class='alignright'></td>");
                                    }

                                }
                                else
                                {
                                    if (((colcount + j) - dcs) < DCount)
                                    {
                                        htmlStr.Append("<td class='alignright'>" + dtEmpTotalSummarDeduction.Rows[(colcount + j) - dcs]["Amount"].ToString() + "</td>");
                                    }
                                    else
                                    {
                                        htmlStr.Append("<td class='alignright'></td>");
                                    }
                                }
                            }
                            if (i == 0)
                            {
                                htmlStr.Append("<td class='alignright'>" + Convert.ToString(EmpTotalPay) + "</td>");
                            }
                            else if (i == 1)
                            {
                                htmlStr.Append("<td class='alignright'>" + Convert.ToString(EmpTotalDeduction) + "</td>");
                            }
                            else if (i == 2)
                            {
                                htmlStr.Append("<td class='alignright'>" + Convert.ToString(EmpNetPay) + "</td>");
                            }
                            else
                            {
                                htmlStr.Append("<td></td>");
                            }
                            htmlStr.Append("</tr>");

                        }

                        htmlStr.Append("</table>");
                        DivDetail.InnerHtml = htmlStr.ToString();
                        pnlprint.Visible = true;
                        DivHead.Visible = true;
                           
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning!", "No record found.");
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning!", "No record found.");
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning!", "No record found.");
            }
        }
        else
        {
            lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning!", "Select Section");
        }

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        Page.Validate("a");

        if (!Page.IsValid)
        {
            return;
        }
        else
        {
            try
            {
                GetMonthlyDetails();
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 5 : ", ex.Message.ToString());
            }
        }
    }
}