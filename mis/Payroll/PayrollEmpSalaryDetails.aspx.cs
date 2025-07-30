using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollEmpSalaryDetails : System.Web.UI.Page
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
                Session["Page"] = Server.UrlEncode(System.DateTime.Now.ToString());
                DivDetail.Visible = false;
                FillDropdown();
                FillMonth();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

        ViewState["UPage"] = Session["Page"];
    }
    protected void FillDropdown()
    {
        try
        {
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
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
            // ddlOfficeName.Attributes.Add("readonly", "readonly");

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
                if (i <= DateTime.Now.Month)
                    ddlMonth.Items.Insert(0, new ListItem(date.ToString("MMMM"), i.ToString()));

            }
            if (DateTime.Now.Month == 1 && ddlYear.SelectedValue == (DateTime.Now.Year - 1).ToString())
            {
                ddlMonth.Items.Clear();
                DateTime date = new DateTime(DateTime.Now.Year, 12, 1);
                ddlMonth.Items.Insert(0, new ListItem(date.ToString("MMMM"), "12"));
            }
            ddlMonth.DataBind();
            ddlMonth.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            // lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            DivDetail.Visible = false;
            ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Year", "MonthNo", "Office_ID", "Emp_TypeOfPost" }, new string[] { "1", ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOfficeName.SelectedValue.ToString(), ddlEmp_TypeOfPost.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                DivDetail.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void InsertLoanInstallmentOfEmployee()
    {
        DataSet ds1 = new DataSet();
        try
        {
            string LoginUserID = ViewState["Emp_ID"].ToString();
            ds1 = objdb.ByProcedure("USP_PayrollEmpLoanUpdate",
                new string[] { "SalaryYear", "SalaryMonth", "Office_ID", "UpdatedBy" },
                new string[] { ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOfficeName.SelectedValue.ToString(), LoginUserID }, "dataset");
            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        }
                        else
                        {
                            string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();

                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);

                        }
                       
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlYear.SelectedIndex > 0 && ddlOfficeName.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0 && ddlEmp_TypeOfPost.SelectedIndex > 0)
            {
                InsertLoanInstallmentOfEmployee();
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnGenerated_Click(object sender, EventArgs e)
    {
        try
        {
            
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            lblMsg.Text = "";
            string msg = "";
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year.\\n";
            }
            if (ddlMonth.SelectedIndex == 0)
            {
                msg += "Select Month.\\n";
            }
            if (msg == "")
            {
                //StringBuilder sbSet_Attendance = new StringBuilder();
                string Year = ddlYear.SelectedValue.ToString();
                string MonthNo = ddlMonth.SelectedValue.ToString();
                string Month = ddlMonth.SelectedItem.ToString();
                string flag = "0";
                string LoginUserID = ViewState["Emp_ID"].ToString();
                string Office_ID = ViewState["Office_ID"].ToString();
                if (ViewState["UPage"].ToString() == Session["Page"].ToString())
                {
                    foreach (GridViewRow gr in GridView1.Rows)
                    {

                        CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                        Label lblRowNumber = (Label)gr.FindControl("lblRowNumber");
                        Label lblSalary_Basic = (Label)gr.FindControl("lblSalary_Basic");
                        Label lblSalary_BasicPayScale = (Label)gr.FindControl("lblSalary_BasicPayScale");
                        Label lblSalary_PayableDays = (Label)gr.FindControl("lblSalary_PayableDays");
                        Label lblSalary_EarningTotal = (Label)gr.FindControl("lblSalary_EarningTotal");
                        Label lblSalary_DeductionTotal = (Label)gr.FindControl("lblSalary_DeductionTotal");
                        Label lblSalary_NetSalary = (Label)gr.FindControl("lblSalary_NetSalary");
                        Label lblSalary_NoDayDeduAmt = (Label)gr.FindControl("lblSalary_NoDayDeduAmt");
                        Label lblSalary_NoDayEarnAmt = (Label)gr.FindControl("lblSalary_NoDayEarnAmt");
                        Label lblGenStatus = (Label)gr.FindControl("lblGenStatus");
                        if (chkSelect.Checked == true && lblGenStatus.Text == "Not Generated")
                        //if (chkSelect.Checked == true && lblGenStatus.Text != "Generated")
                        {
                            objdb.ByProcedure("SpPayrollSalaryDetail",
                            new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo", "Salary_Month", "Salary_Basic", "Salary_BasicPayScale", "Salary_PayableDays", "Salary_EarningTotal", "Salary_DeductionTotal", "Salary_NetSalary", "Salary_NoDayDeduAmt", "Salary_NoDayEarnAmt", "Salary_UpdatedBy", "SalaryStatus", "CreatedByIP" },
                            new string[] { "0", lblRowNumber.ToolTip.ToString(), Office_ID, Year, MonthNo, Month, lblSalary_Basic.Text, lblSalary_BasicPayScale.Text, lblSalary_PayableDays.Text, lblSalary_EarningTotal.Text, lblSalary_DeductionTotal.Text, lblSalary_NetSalary.Text, lblSalary_NoDayDeduAmt.Text, lblSalary_NoDayEarnAmt.Text, LoginUserID, "Generated", IPAddress }, "dataset");
                            flag = "0";
                        }
                       
                    }
                    if (flag == "1")
                    {
                        FillGrid();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please Select Atleast One Employee.');", true);
                    }

                    Session["Page"] = Server.UrlEncode(System.DateTime.Now.ToString());

                    
                }
                FillGrid();

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
    /*******New PP**********/
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {

        ViewState["Office_ID"] = ddlOfficeName.SelectedItem.Value;
        DivDetail.Visible = false;
        FillDropdown();

    }
    protected void btnHold_Click(object sender, EventArgs e)
    {
        try
        {
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            lblMsg.Text = "";
            string msg = "";
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year.\\n";
            }
            if (ddlMonth.SelectedIndex == 0)
            {
                msg += "Select Month.\\n";
            }
            if (msg == "")
            {
                //StringBuilder sbSet_Attendance = new StringBuilder();
                string Year = ddlYear.SelectedValue.ToString();
                string MonthNo = ddlMonth.SelectedValue.ToString();
                string Month = ddlMonth.SelectedItem.ToString();
                string flag = "0";

                string LoginUserID = ViewState["Emp_ID"].ToString();
                string Office_ID = ViewState["Office_ID"].ToString();
                if (ViewState["UPage"].ToString() == Session["Page"].ToString())
                {
                    foreach (GridViewRow gr in GridView1.Rows)
                    {

                        CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                        Label lblRowNumber = (Label)gr.FindControl("lblRowNumber");
                        Label lblSalary_Basic = (Label)gr.FindControl("lblSalary_Basic");
                        Label lblSalary_BasicPayScale = (Label)gr.FindControl("lblSalary_BasicPayScale");
                        Label lblSalary_PayableDays = (Label)gr.FindControl("lblSalary_PayableDays");
                        Label lblSalary_EarningTotal = (Label)gr.FindControl("lblSalary_EarningTotal");
                        Label lblSalary_DeductionTotal = (Label)gr.FindControl("lblSalary_DeductionTotal");
                        Label lblSalary_NetSalary = (Label)gr.FindControl("lblSalary_NetSalary");
                        Label lblSalary_NoDayDeduAmt = (Label)gr.FindControl("lblSalary_NoDayDeduAmt");
                        Label lblSalary_NoDayEarnAmt = (Label)gr.FindControl("lblSalary_NoDayEarnAmt");
                        Label lblGenStatus = (Label)gr.FindControl("lblGenStatus");
                        if (chkSelect.Checked == true && lblGenStatus.Text == "Not Generated")
                        {
                            objdb.ByProcedure("SpPayrollSalaryDetail",
                            new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo", "Salary_Month", "Salary_Basic", "Salary_BasicPayScale", "Salary_PayableDays", "Salary_EarningTotal", "Salary_DeductionTotal", "Salary_NetSalary", "Salary_NoDayDeduAmt", "Salary_NoDayEarnAmt", "Salary_UpdatedBy", "SalaryStatus", "CreatedByIP" },
                            new string[] { "0", lblRowNumber.ToolTip.ToString(), Office_ID, Year, MonthNo, Month, lblSalary_Basic.Text, lblSalary_BasicPayScale.Text, lblSalary_PayableDays.Text, lblSalary_EarningTotal.Text, lblSalary_DeductionTotal.Text, lblSalary_NetSalary.Text, lblSalary_NoDayDeduAmt.Text, lblSalary_NoDayEarnAmt.Text, LoginUserID, "Hold", IPAddress }, "dataset");
                            flag = "1";
                        }
                    }
                    if (flag == "1")
                    {

                        FillGrid();

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please Select Atleast One Employee.');", true);
                    }

                    Session["Page"] = Server.UrlEncode(System.DateTime.Now.ToString());

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                FillGrid();

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
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillMonth();
    }
}