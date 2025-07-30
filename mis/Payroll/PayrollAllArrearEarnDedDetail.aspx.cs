using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollAllArrearEarnDedDetail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
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
                    DivHead.Visible = false;
                    if (ViewState["Office_ID"].ToString() == "1")
                    {
                        ddlOffice.Enabled = true;
                    }
                    ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag" },
                           new string[] { "10" }, "dataset");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlOffice.DataSource = ds;
                        ddlOffice.DataTextField = "Office_Name";
                        ddlOffice.DataValueField = "Office_ID";
                        ddlOffice.DataBind();
                        ddlOffice.Items.Insert(0, new ListItem("Select", "0"));

                        ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                    }
                    FillDropdown();
                    //FillEmpDropdown();
                }
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
    //protected void FillEmpDropdown()
    //{
    //    try
    //    {

    //        ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
    //        ds = null;
    //        ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ddlOffice.SelectedValue.ToString() }, "dataset");
    //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlEmployee.DataSource = ds;
    //            ddlEmployee.DataTextField = "Emp_Name";
    //            ddlEmployee.DataValueField = "Emp_ID";
    //            ddlEmployee.DataBind();
    //            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
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
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DivDetail.InnerHtml = "";
            lblMsg.Text = "";
            lblSession.Text = "";
            //lblPosttype.Text = "";
            DivHead.Visible = false;
            lblOfficeName.Text = ddlOffice.SelectedItem.Text;
            if (ddlOffice.SelectedIndex > 0 && ddlFinancialYear.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
            {
                FillSalary();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillSalary()
    {
        try
        {
            DivDetail.InnerHtml = "";
            string Office_ID = ddlOffice.SelectedValue.ToString();
            string Salary_Year = ddlFinancialYear.SelectedValue.ToString();
            string Salary_MonthNo = ddlMonth.SelectedValue.ToString();
            //string Salary_Employee = ddlEmployee.SelectedValue.ToString();

            lblSession.Text = ddlMonth.SelectedItem.ToString() + " " + ddlFinancialYear.SelectedValue.ToString();
            DivHead.Visible = true;
            ds = objdb.ByProcedure("SpPayrollEmpArrear", new string[] { "flag", "Office_ID", "CurrentYear", "CurrentMonth" }, new string[] { "5", Office_ID, Salary_Year, Salary_MonthNo}, "dataset");
            StringBuilder htmlStr = new StringBuilder();
            if (ds.Tables[0].Rows.Count != 0)
            {
                int Count = ds.Tables[0].Rows.Count;
                htmlStr.Append("<table class='' style='margin-bottom:5px; font-family: monospace; line-height: 14px;' id='SalaryTable'>");
                // htmlStr.Append("<thead  style='border-bottom: 0px solid white;'>");
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th>SNo.</th>");
                htmlStr.Append("<th colspan='3'>Name Of Employee</th>");
                //  htmlStr.Append("<th colspan='2'>Designation</th>");
                htmlStr.Append("<th colspan='2'>IFSC Code</th>");
                htmlStr.Append("<th colspan='2'>Bank Account No.</th>");
                htmlStr.Append("<th>EPF No.</th>");
                htmlStr.Append("<th>GI No.</th>");
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th>Basic Pay</th>");
                htmlStr.Append("<th>W.Allow </th>");
                htmlStr.Append("<th>Con.Allw</th>");
                htmlStr.Append("<th>EPF Ded</th>");
                htmlStr.Append("<th>I.Tax </th>");
                htmlStr.Append("<th>Staff Adv</th>");
                htmlStr.Append("<th>HB Loan</th>");
                htmlStr.Append("<th>Misc Ded</th>");
                htmlStr.Append("<th>CTC</th>");
                htmlStr.Append("<th>Total Pay</th>");
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th>D.A.</th>");
                htmlStr.Append("<th>Spl Pay</th>");
                htmlStr.Append("<th>H.R.A.</th>");
                htmlStr.Append("<th>A.D.A.Ded</th>");
                htmlStr.Append("<th>Prof.Tax</th>");
                htmlStr.Append("<th>Fest.Adv</th>");
                htmlStr.Append("<th>Veh.Adv</th>");
                htmlStr.Append("<th>Recovery</th>");
                htmlStr.Append("<th>EPF CONT.</th>");
                htmlStr.Append("<th>Total Ded</th>");
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th>Other Allow</th>");
                htmlStr.Append("<th>Ord.Allow </th>");
                htmlStr.Append("<th>Trib.Allw</th>");
                htmlStr.Append("<th>G.Ins</th>");
                htmlStr.Append("<th>LIC Ded</th>");
                htmlStr.Append("<th>Gr.Adv</th>");
                htmlStr.Append("<th>HRA Ded</th>");
                htmlStr.Append("<th>Run Chgs</th>");
                htmlStr.Append("<th>HandiAllow</th>");
                htmlStr.Append("<th>Net Pay</th>");
                htmlStr.Append("</tr>");
                //  htmlStr.Append("</thead>");
                htmlStr.Append("<tbody>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<table>");
                // Row 1
                float Tables0Rows0Basic = 0;
                float Tables1Rows4 = 0;
                float Tables1Rows2 = 0;
                float Tables2Rows0 = 0;
                float Tables2Rows3 = 0;
                float Tables2Rows7 = 0;
                float Tables2Rows12 = 0;
                float Tables2Rows8 = 0;
                float Tables0Rows0EarningTotal = 0;

                // Row 2
                float Tables1Rows0 = 0;
                float Tables1Rows5 = 0;
                float Tables1Rows1 = 0;
                float Tables2Rows1 = 0;
                float Tables2Rows4 = 0;
                float Tables2Rows10 = 0;
                float Tables2Rows6 = 0;
                float Tables2Rows13 = 0;
                float Tables1Rows9 = 0;
                float Tables2Rows13DeductionTotal = 0;

                // Row 3
                float Tables1Rows6 = 0;
                float Tables1Rows3 = 0;
                float Tables1Rows7 = 0;
                float Tables2Rows2 = 0;
                float Tables0Rows0PolicyDeduction = 0;
                float Tables2Rows9 = 0;
                float Tables2Rows11 = 0;
                float Tables2Rows5 = 0;
                float Tables1Rows8 = 0;
                float Tables2Rows14 = 0;
                float Tables0Rows0Salary_NetSalary = 0;

                for (int i = 0; i < Count; i++)
                {

                    string Emp_ID = ds.Tables[0].Rows[i]["Emp_ID"].ToString();

                    DataSet ds1 = objdb.ByProcedure("SpPayrollEmpArrear", new string[] { "flag", "Emp_ID", "Office_ID", "CurrentYear", "CurrentMonth" }, new string[] { "12", Emp_ID, Office_ID, Salary_Year, Salary_MonthNo }, "dataset"); //"6"
                    if (ds1.Tables[0].Rows.Count != 0)
                    {
                        // Row 1
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<table  style='margin-top:5px;'>");
                        htmlStr.Append("<tr'>");
                        htmlStr.Append("<th>" + (i + 1).ToString() + "</th>");
                        htmlStr.Append("<th colspan='3'>" + ds1.Tables[0].Rows[0]["Emp_Name"].ToString() + "</th>");
                        //  htmlStr.Append("<th colspan='2'>" + ds.Tables[0].Rows[i]["Designation_Name"].ToString() + "</th>");
                        htmlStr.Append("<th colspan='2'>" + ds1.Tables[0].Rows[0]["Bank_IfscCode"].ToString() + "</th>");
                        htmlStr.Append("<th colspan='2'>'" + ds1.Tables[0].Rows[0]["Bank_AccountNo"].ToString() + "'</th>");
                        htmlStr.Append("<th>" + ds1.Tables[0].Rows[0]["EPF_No"].ToString() + "</th>");
                        htmlStr.Append("<th>" + ds1.Tables[0].Rows[0]["GroupInsurance_No"].ToString() + "</th>");
                        htmlStr.Append("</tr>");

                        // Row 2
                        htmlStr.Append("<tr>");
                        // Tables0Rows0Basic = Tables0Rows0Basic + float.Parse(ds1.Tables[0].Rows[0]["Emp_BasicSalery"].ToString()); // OLD
                        Tables0Rows0Basic = Tables0Rows0Basic + float.Parse(ds1.Tables[0].Rows[0]["BasicSalary"].ToString()); //05-07-2019

                        // htmlStr.Append("<td>" + ds1.Tables[0].Rows[0]["Emp_BasicSalery"].ToString() + "</td>");  // OLD
                        htmlStr.Append("<td>" + ds1.Tables[0].Rows[0]["BasicSalary"].ToString() + "</td>"); //05-07-2019
                        Tables1Rows4 = Tables1Rows4 + float.Parse(ds1.Tables[1].Rows[4]["Earning"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[1].Rows[4]["Earning"].ToString() + "</td>");
                        Tables1Rows2 = Tables1Rows2 + float.Parse(ds1.Tables[1].Rows[2]["Earning"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[1].Rows[2]["Earning"].ToString() + "</td>");
                        Tables2Rows0 = Tables2Rows0 + float.Parse(ds1.Tables[2].Rows[0]["Deduction"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[2].Rows[0]["Deduction"].ToString() + "</td>");
                        Tables2Rows3 = Tables2Rows3 + float.Parse(ds1.Tables[2].Rows[3]["Deduction"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[2].Rows[3]["Deduction"].ToString() + " </td>");
                        Tables2Rows7 = Tables2Rows7 + float.Parse(ds1.Tables[2].Rows[7]["Deduction"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[2].Rows[7]["Deduction"].ToString() + "</td>");
                        Tables2Rows12 = Tables2Rows12 + float.Parse(ds1.Tables[2].Rows[12]["Deduction"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[2].Rows[12]["Deduction"].ToString() + "</td>");
                        Tables2Rows8 = Tables2Rows8 + float.Parse(ds1.Tables[2].Rows[8]["Deduction"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[2].Rows[8]["Deduction"].ToString() + "</td>");

                        float datavalue123 = 0;
                        if (ds1.Tables[1].Rows.Count == 10)
                        {
                            datavalue123 = float.Parse(ds1.Tables[1].Rows[9]["Earning"].ToString());
                        }
                        //htmlStr.Append("<td>" + datavalue123 + "</td>");

                        Tables1Rows9 = Tables1Rows9 + datavalue123;
                        // Tables1Rows9 = Tables1Rows9 + float.Parse(ds1.Tables[1].Rows[9]["Earning"].ToString());
                        //htmlStr.Append("<td>" + ds1.Tables[1].Rows[9]["Earning"].ToString() + "</td>");
                        htmlStr.Append("<td>" + datavalue123 + "</td>");

                        Tables0Rows0EarningTotal = Tables0Rows0EarningTotal + float.Parse(ds1.Tables[0].Rows[0]["TotalEarning"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[0].Rows[0]["TotalEarning"].ToString() + "</td>");
                        htmlStr.Append("</tr>");

                        // Row 3
                        htmlStr.Append("<tr>");
                        Tables1Rows0 = Tables1Rows0 + float.Parse(ds1.Tables[1].Rows[0]["Earning"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[1].Rows[0]["Earning"].ToString() + "</td>");
                        Tables1Rows5 = Tables1Rows5 + float.Parse(ds1.Tables[1].Rows[5]["Earning"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[1].Rows[5]["Earning"].ToString() + "</td>");
                        Tables1Rows1 = Tables1Rows1 + float.Parse(ds1.Tables[1].Rows[1]["Earning"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[1].Rows[1]["Earning"].ToString() + "</td>");
                        Tables2Rows1 = Tables2Rows1 + float.Parse(ds1.Tables[2].Rows[1]["Deduction"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[2].Rows[1]["Deduction"].ToString() + "</td>");
                        Tables2Rows4 = Tables2Rows4 + float.Parse(ds1.Tables[2].Rows[4]["Deduction"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[2].Rows[4]["Deduction"].ToString() + "</td>");
                        Tables2Rows10 = Tables2Rows10 + float.Parse(ds1.Tables[2].Rows[10]["Deduction"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[2].Rows[10]["Deduction"].ToString() + "</td>");
                        Tables2Rows6 = Tables2Rows6 + float.Parse(ds1.Tables[2].Rows[6]["Deduction"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[2].Rows[6]["Deduction"].ToString() + "</td>");
                        Tables2Rows13 = Tables2Rows13 + float.Parse(ds1.Tables[2].Rows[13]["Deduction"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[2].Rows[13]["Deduction"].ToString() + "</td>");
                        //Tables2Rows14 = Tables2Rows14 + float.Parse(ds1.Tables[2].Rows[14]["Deduction"].ToString());
                        //htmlStr.Append("<td>" + ds1.Tables[2].Rows[14]["Deduction"].ToString() + "</td>");


                        float datavalue123ded = 0;
                        if (ds1.Tables[2].Rows.Count == 15)
                        {
                            datavalue123ded = float.Parse(ds1.Tables[2].Rows[14]["Deduction"].ToString());
                        }
                        Tables2Rows14 = Tables2Rows14 + datavalue123ded;
                        htmlStr.Append("<td>" + datavalue123ded + "</td>");


                        Tables2Rows13DeductionTotal = Tables2Rows13DeductionTotal + float.Parse(ds1.Tables[0].Rows[0]["TotalDeduction"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[0].Rows[0]["TotalDeduction"].ToString() + "</td>");
                        htmlStr.Append("</tr>");

                        // Row 4
                        htmlStr.Append("<tr>");
                        Tables1Rows6 = Tables1Rows6 + float.Parse(ds1.Tables[1].Rows[6]["Earning"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[1].Rows[6]["Earning"].ToString() + "</td>");
                        Tables1Rows3 = Tables1Rows3 + float.Parse(ds1.Tables[1].Rows[3]["Earning"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[1].Rows[3]["Earning"].ToString() + "</td>");
                        Tables1Rows7 = Tables1Rows7 + float.Parse(ds1.Tables[1].Rows[7]["Earning"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[1].Rows[7]["Earning"].ToString() + "</td>");
                        Tables2Rows2 = Tables2Rows2 + float.Parse(ds1.Tables[2].Rows[2]["Deduction"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[2].Rows[2]["Deduction"].ToString() + "</td>");
                        Tables0Rows0PolicyDeduction = Tables0Rows0PolicyDeduction + float.Parse(ds1.Tables[0].Rows[0]["PolicyDeduction"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[0].Rows[0]["PolicyDeduction"].ToString() + "</td>");
                        Tables2Rows9 = Tables2Rows9 + float.Parse(ds1.Tables[2].Rows[9]["Deduction"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[2].Rows[9]["Deduction"].ToString() + "</td>");
                        Tables2Rows11 = Tables2Rows11 + float.Parse(ds1.Tables[2].Rows[11]["Deduction"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[2].Rows[11]["Deduction"].ToString() + "</td>");
                        Tables2Rows5 = Tables2Rows5 + float.Parse(ds1.Tables[2].Rows[5]["Deduction"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[2].Rows[5]["Deduction"].ToString() + "</td>");
                        Tables1Rows8 = Tables1Rows8 + float.Parse(ds1.Tables[1].Rows[8]["Earning"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[1].Rows[8]["Earning"].ToString() + "</td>");
                        Tables0Rows0Salary_NetSalary = Tables0Rows0Salary_NetSalary + float.Parse(ds1.Tables[0].Rows[0]["NetPaymentAmount"].ToString());
                        htmlStr.Append("<td>" + ds1.Tables[0].Rows[0]["NetPaymentAmount"].ToString() + "</td>");
                        htmlStr.Append("</tr>");

                        htmlStr.Append("</table>");
                        htmlStr.Append("</tr>");


                    }
                }
                htmlStr.Append("<tr>");
                htmlStr.Append("<table style='margin-top:5px;'>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<th colspan='10' style='text-align: center;font-weight: 700;'>Report Summary</th>");
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<td>" + Tables0Rows0Basic.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables1Rows4.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables1Rows2.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables2Rows0.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables2Rows3.ToString() + " </td>");
                htmlStr.Append("<td>" + Tables2Rows7.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables2Rows12.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables2Rows8.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables1Rows9.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables0Rows0EarningTotal.ToString() + "</td>");
                htmlStr.Append("</tr>");



                htmlStr.Append("<tr>");
                htmlStr.Append("<td>" + Tables1Rows0.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables1Rows5.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables1Rows1.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables2Rows1.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables2Rows4.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables2Rows10.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables2Rows6.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables2Rows13.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables2Rows14.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables2Rows13DeductionTotal.ToString() + "</td>");
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>" + Tables1Rows6.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables1Rows3.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables1Rows7.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables2Rows2.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables0Rows0PolicyDeduction.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables2Rows9.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables2Rows11.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables2Rows5.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables1Rows8.ToString() + "</td>");
                htmlStr.Append("<td>" + Tables0Rows0Salary_NetSalary.ToString() + "</td>");
                htmlStr.Append("</tr>");
                htmlStr.Append("</table>");
                htmlStr.Append("</tr>");

                htmlStr.Append("</tbody>");
                htmlStr.Append("</table>");

                //DivDetail.InnerHtml = htmlStr.ToString();
            }
            else
            {
                htmlStr.Append("<p style='color:red; text-align:center; font-size:16px; margin-top:20px;'>No Record Found</p>");
            }

            DivDetail.InnerHtml = htmlStr.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    FillEmpDropdown();

    //}
}