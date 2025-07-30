using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollAllEmpEarnDedDetailNewRM : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {

                //string SessionVariableName;
                //string SessionVariableValue; 
                //foreach (string SessionVariable in Session.Keys)
                //{
                //    SessionVariableName = SessionVariable;
                //    Response.Write(SessionVariableName+'\n');
                //    SessionVariableValue = Session[SessionVariableName].ToString();
                //    Response.Write(SessionVariableValue); 
                //}


                if (!IsPostBack)
                {

                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    //ddlOffice.Enabled = false;
                    DivHead.Visible = false;
                    //if (ViewState["Office_ID"].ToString() == "1")
                    //{
                    //    ddlOffice.Enabled = true;
                    //}
                    ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag", "Office_ID" },
                           new string[] { "20", ViewState["Office_ID"].ToString() }, "dataset");
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
            lblPosttype.Text = "";
            lblOffice.Text = "";
            DivHead.Visible = false;
            if (ddlOffice.SelectedIndex > 0 && ddlFinancialYear.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0 && ddlEmp_TypeOfPost.SelectedIndex > 0)
            {
                lblOffice.Text = ddlOffice.SelectedItem.ToString();
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

            lblSession.Text = ddlMonth.SelectedItem.ToString() + " " + ddlFinancialYear.SelectedValue.ToString();
            lblPosttype.Text = ddlEmp_TypeOfPost.SelectedValue.ToString();
            DivHead.Visible = true;
            ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "Salary_MonthNo", "Emp_TypeOfPost" }, new string[] { "10", Office_ID, Salary_Year, Salary_MonthNo, ddlEmp_TypeOfPost.SelectedValue.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                int Count = ds.Tables[0].Rows.Count;
                StringBuilder htmlStr = new StringBuilder();
                htmlStr.Append("<table class='main-heading-print' style='margin-bottom:5px; font-family: monospace; line-height: 14px;' id='SalaryTable'>");
                // htmlStr.Append("<thead  style='border-bottom: 0px solid white;'>");
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th class='text-left'>SNo.</th>");
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

                string repeadtCodePrintPage = htmlStr.ToString();

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
                string Islast_heading = "";
                for (int i = 0; i < Count; i++)
                {

                    string Emp_ID = ds.Tables[0].Rows[i]["Emp_ID"].ToString();

                    DataSet ds1 = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo" }, new string[] { "11", Emp_ID, Office_ID, Salary_Year, Salary_MonthNo }, "dataset");
                    if (ds1.Tables[0].Rows.Count != 0)
                    {
                        // Row 1
                        string pagebreak = "";
                        if(i==14||i==30 ||i==46||i==62){
                            pagebreak=".page-break";
                            Islast_heading ="Yes";
                            //htmlStr.Append(repeadtCodePrintPage);
                        }
                        htmlStr.Append("<tr class='" + pagebreak + "'>");
                        htmlStr.Append("<table  style='margin-top: 10px;background: beige;' class='" + pagebreak + "'>");
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<th class='text-left'>" + (i + 1).ToString() + "</th>");
                        htmlStr.Append("<th colspan='3'>" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + "</th>");
                        //  htmlStr.Append("<th colspan='2'>" + ds.Tables[0].Rows[i]["Designation_Name"].ToString() + "</th>");
                        htmlStr.Append("<th colspan='2'>" + ds.Tables[0].Rows[i]["Bank_IfscCode"].ToString() + "</th>");
                        htmlStr.Append("<th colspan='2'>'" + ds.Tables[0].Rows[i]["Bank_AccountNo"].ToString() + "'</th>");
                        htmlStr.Append("<th>" + ds.Tables[0].Rows[i]["EPF_No"].ToString() + "</th>");
                        htmlStr.Append("<th>" + ds.Tables[0].Rows[i]["GroupInsurance_No"].ToString() + "</th>");
                        htmlStr.Append("</tr>");

                        // Row 2
                        htmlStr.Append("<tr>");
                        Tables0Rows0Basic = Tables0Rows0Basic + float.Parse(ds1.Tables[0].Rows[0]["Salary_Basic"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_Basic"].ToString() + "</td>");
                        Tables1Rows4 = Tables1Rows4 + float.Parse(ds1.Tables[1].Rows[4]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[4]["Earning"].ToString() + "</td>");
                        Tables1Rows2 = Tables1Rows2 + float.Parse(ds1.Tables[1].Rows[2]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[2]["Earning"].ToString() + "</td>");
                        Tables2Rows0 = Tables2Rows0 + float.Parse(ds1.Tables[2].Rows[0]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[0]["Earning"].ToString() + "</td>");
                        Tables2Rows3 = Tables2Rows3 + float.Parse(ds1.Tables[2].Rows[3]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[3]["Earning"].ToString() + " </td>");
                        Tables2Rows7 = Tables2Rows7 + float.Parse(ds1.Tables[2].Rows[7]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[7]["Earning"].ToString() + "</td>");
                        Tables2Rows12 = Tables2Rows12 + float.Parse(ds1.Tables[2].Rows[12]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[12]["Earning"].ToString() + "</td>");
                        Tables2Rows8 = Tables2Rows8 + float.Parse(ds1.Tables[2].Rows[8]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[8]["Earning"].ToString() + "</td>");
                        Tables1Rows9 = Tables1Rows9 + float.Parse(ds1.Tables[1].Rows[9]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[9]["Earning"].ToString() + "</td>");

                        Tables0Rows0EarningTotal = Tables0Rows0EarningTotal + float.Parse(ds1.Tables[0].Rows[0]["Salary_EarningTotal"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_EarningTotal"].ToString() + "</td>");
                        htmlStr.Append("</tr>");

                        // Row 3
                        htmlStr.Append("<tr>");
                        Tables1Rows0 = Tables1Rows0 + float.Parse(ds1.Tables[1].Rows[0]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[0]["Earning"].ToString() + "</td>");
                        Tables1Rows5 = Tables1Rows5 + float.Parse(ds1.Tables[1].Rows[5]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[5]["Earning"].ToString() + "</td>");
                        Tables1Rows1 = Tables1Rows1 + float.Parse(ds1.Tables[1].Rows[1]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[1]["Earning"].ToString() + "</td>");
                        Tables2Rows1 = Tables2Rows1 + float.Parse(ds1.Tables[2].Rows[1]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[1]["Earning"].ToString() + "</td>");
                        Tables2Rows4 = Tables2Rows4 + float.Parse(ds1.Tables[2].Rows[4]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[4]["Earning"].ToString() + "</td>");
                        Tables2Rows10 = Tables2Rows10 + float.Parse(ds1.Tables[2].Rows[10]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[10]["Earning"].ToString() + "</td>");
                        Tables2Rows6 = Tables2Rows6 + float.Parse(ds1.Tables[2].Rows[6]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[6]["Earning"].ToString() + "</td>");
                        Tables2Rows13 = Tables2Rows13 + float.Parse(ds1.Tables[2].Rows[13]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[13]["Earning"].ToString() + "</td>");

                        Tables2Rows14 = Tables2Rows14 + float.Parse(ds1.Tables[2].Rows[14]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[14]["Earning"].ToString() + "</td>");

                        Tables2Rows13DeductionTotal = Tables2Rows13DeductionTotal + float.Parse(ds1.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString() + "</td>");
                        htmlStr.Append("</tr>");

                        // Row 4
                        htmlStr.Append("<tr>");
                        Tables1Rows6 = Tables1Rows6 + float.Parse(ds1.Tables[1].Rows[6]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[6]["Earning"].ToString() + "</td>");
                        Tables1Rows3 = Tables1Rows3 + float.Parse(ds1.Tables[1].Rows[3]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[3]["Earning"].ToString() + "</td>");
                        Tables1Rows7 = Tables1Rows7 + float.Parse(ds1.Tables[1].Rows[7]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[7]["Earning"].ToString() + "</td>");
                        Tables2Rows2 = Tables2Rows2 + float.Parse(ds1.Tables[2].Rows[2]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[2]["Earning"].ToString() + "</td>");
                        Tables0Rows0PolicyDeduction = Tables0Rows0PolicyDeduction + float.Parse(ds1.Tables[0].Rows[0]["PolicyDeduction"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[0].Rows[0]["PolicyDeduction"].ToString() + "</td>");
                        Tables2Rows9 = Tables2Rows9 + float.Parse(ds1.Tables[2].Rows[9]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[9]["Earning"].ToString() + "</td>");
                        Tables2Rows11 = Tables2Rows11 + float.Parse(ds1.Tables[2].Rows[11]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[11]["Earning"].ToString() + "</td>");
                        Tables2Rows5 = Tables2Rows5 + float.Parse(ds1.Tables[2].Rows[5]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[5]["Earning"].ToString() + "</td>");
                        Tables1Rows8 = Tables1Rows8 + float.Parse(ds1.Tables[1].Rows[8]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[8]["Earning"].ToString() + "</td>");
                        Tables0Rows0Salary_NetSalary = Tables0Rows0Salary_NetSalary + float.Parse(ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString() + "</td>");
                        htmlStr.Append("</tr>");

                        htmlStr.Append("</table>");
                        htmlStr.Append("</tr>");


                    }
                }
                if(Islast_heading=="Yes"){                    
                htmlStr.Append(repeadtCodePrintPage);
                }
                htmlStr.Append("<tr>");
                htmlStr.Append("<table style='margin-top: 10px;background: beige;'>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<th colspan='10' style='text-align: center;font-weight: 700;'>Report Summary</th>");
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='alignright'>" + Tables0Rows0Basic.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows4.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows2.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows0.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows3.ToString() + " </td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows7.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows12.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows8.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows9.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables0Rows0EarningTotal.ToString() + "</td>");
                htmlStr.Append("</tr>");



                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows0.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows5.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows1.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows1.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows4.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows10.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows6.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows13.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows14.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows13DeductionTotal.ToString() + "</td>");
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows6.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows3.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows7.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows2.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables0Rows0PolicyDeduction.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows9.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows11.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows5.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows8.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables0Rows0Salary_NetSalary.ToString() + "</td>");
                htmlStr.Append("</tr>");
                htmlStr.Append("</table>");
                htmlStr.Append("</tr>");

                htmlStr.Append("</tbody>");
                htmlStr.Append("</table>");

                DivDetail.InnerHtml = htmlStr.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}
