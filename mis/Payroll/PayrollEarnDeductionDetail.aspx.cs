using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollEarnDeductionDetail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Emp_ID"] != null)
            {

                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                divDetail.Visible = false;
                FillDropdown();
                FillGrid();

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
                ddlYear.DataTextField = "Year";
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
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ddlOfficeName.SelectedValue.ToString() }, "dataset");
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
            ds = objdb.ByProcedure("SpPayrollEarnDeductionDetail", new string[] { "flag", "Year", "Emp_ID" }, new string[] { "1", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString() }, "dataset");
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
                if (Monthcount != "")
                {
                    foreach (GridViewRow gr in GridView1.Rows)
                    {
                        if (gr.RowType == DataControlRowType.DataRow)
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
                    }
                }
            }
            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                GridView2.DataSource = ds.Tables[1];
                GridView2.DataBind();
                if (Monthcount != "")
                {
                    foreach (GridViewRow gr in GridView2.Rows)
                    {
                        if (gr.RowType == DataControlRowType.DataRow)
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
                //if (Monthcount != "")
                //{

                //    string[] monthNo = Monthcount.Split(',');
                //    for (int i = 0; i < monthNo.Length - 1; i++)
                //    {
                //        if (monthNo[i].ToString() != "")
                //        {
                //            int aa = int.Parse(monthNo[i]);
                //            if (aa == 1)
                //                FJan_Amount.Enabled = false;
                //            else if (aa == 2)
                //                FFeb_Amount.Enabled = false;
                //            else if (aa == 3)
                //                FMar_Amount.Enabled = false;
                //            else if (aa == 4)
                //                FApr_Amount.Enabled = false;
                //            else if (aa == 5)
                //                FMay_Amount.Enabled = false;
                //            else if (aa == 6)
                //                FJun_Amount.Enabled = false;
                //            else if (aa == 7)
                //                FJul_Amount.Enabled = false;
                //            else if (aa == 8)
                //                FAug_Amount.Enabled = false;
                //            else if (aa == 9)
                //                FSep_Amount.Enabled = false;
                //            else if (aa == 10)
                //                FOct_Amount.Enabled = false;
                //            else if (aa == 11)
                //                FNov_Amount.Enabled = false;
                //            else if (aa == 12)
                //                FDec_Amount.Enabled = false;
                //        }
                //    }
                //}
                if (ds.Tables[4].Rows.Count != 0)
                {
                    lblBank_AccountNo.Text = ds.Tables[4].Rows[0]["Bank_AccountNo"].ToString();
                    lblSalary_NetSalary.Text = ds.Tables[4].Rows[0]["Emp_BasicSalery"].ToString();
                    lblBank_Name.Text = ds.Tables[4].Rows[0]["Bank_Name"].ToString();
                    lblIFSCCode.Text = ds.Tables[4].Rows[0]["Bank_IfscCode"].ToString();
                    lblGroupInsurance_No.Text = ds.Tables[4].Rows[0]["GroupInsurance_No"].ToString();
                    lblEPF_No.Text = ds.Tables[4].Rows[0]["EPF_No"].ToString();
                }

            }
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


                    objdb.ByProcedure("SpPayrollEarnDeductionDetail",
                       new string[] { "flag", "Emp_ID", "Year", "EarnDeduction_ID", "EarnDeduction_Type", "Calculation_Type", "Apr_Amount", "May_Amount", "Jun_Amount", "Jul_Amount", "Aug_Amount", "Sep_Amount", "Oct_Amount", "Nov_Amount", "Dec_Amount", "Jan_Amount", "Feb_Amount", "Mar_Amount", "UpdatedBy" },
                             new string[] { "0", Employee, Year, lblEarnDeduction_ID.Text, "Earning", lblCalculation_Type.Text, Apr_Amount.Text, May_Amount.Text, Jun_Amount.Text, Jul_Amount.Text, Aug_Amount.Text, Sep_Amount.Text, Oct_Amount.Text, Nov_Amount.Text, Dec_Amount.Text, Jan_Amount.Text, Feb_Amount.Text, Mar_Amount.Text, ViewState["Emp_ID"].ToString() }, "dataset");
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


                    objdb.ByProcedure("SpPayrollEarnDeductionDetail",
                       new string[] { "flag", "Emp_ID", "Year", "EarnDeduction_ID", "EarnDeduction_Type", "Calculation_Type", "Apr_Amount", "May_Amount", "Jun_Amount", "Jul_Amount", "Aug_Amount", "Sep_Amount", "Oct_Amount", "Nov_Amount", "Dec_Amount", "Jan_Amount", "Feb_Amount", "Mar_Amount", "UpdatedBy" },
                             new string[] { "0", Employee, Year, lblEarnDeduction_ID.Text, "Deduction", lblCalculation_Type.Text, Apr_Amount.Text, May_Amount.Text, Jun_Amount.Text, Jul_Amount.Text, Aug_Amount.Text, Sep_Amount.Text, Oct_Amount.Text, Nov_Amount.Text, Dec_Amount.Text, Jan_Amount.Text, Feb_Amount.Text, Mar_Amount.Text, ViewState["Emp_ID"].ToString() }, "dataset");
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
                    objdb.ByProcedure("SpPayrollPolicyDeduction",
                      new string[] { "flag", "Emp_ID", "Office_ID", "Salary_Year", "PolicyDed_UpdatedBy" },
                      new string[] { j.ToString(), Employee, ViewState["Office_ID"].ToString(), Year, ViewState["Emp_ID"].ToString() }, "dataset");
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