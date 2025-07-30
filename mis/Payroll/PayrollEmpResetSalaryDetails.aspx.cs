using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollEmpResetSalaryDetails : System.Web.UI.Page
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
            if (ddlOfficeName.SelectedValue.ToString() != "1")
            {
                ddlOfficeName.Enabled = false;
            }
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
            GridView1.DataSource = null;
            GridView1.DataBind();
            DivDetail.Visible = false;
			lblrowcount.Text = "";
            string Office = "0";
            lblTab.Text = "";
            if (ViewState["Office_ID"].ToString() != "1")
            {
                Office = ViewState["Office_ID"].ToString();
                ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
            }
            else
            {
                Office = ddlOfficeName.SelectedValue.ToString();
            }
          //  ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Year", "MonthNo", "Office_ID" }, new string[] { "21", ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), Office }, "dataset");
			 ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Year", "MonthNo", "Office_ID", "Office_ID_Login" }, new string[] { "21", ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), Office, ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
				lblrowcount.Text = "Employee Count : " + (ds.Tables[0].Rows.Count);
                DivDetail.Visible = true;
            }
            else
            {
                lblTab.Text = "Salary Details Not Found For This Month.";
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
            lblMsg.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            DivDetail.Visible = false;
            if (ddlYear.SelectedIndex > 0 && ddlOfficeName.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
            {
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            string flag = "0";
            lblMsg.Text = "";
            string msg = "";
            if (ddlOfficeName.SelectedIndex == 0)
            {
                msg += "Select Office Name.\\n";
            }
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
                string Year = ddlYear.SelectedValue.ToString();
                string MonthNo = ddlMonth.SelectedValue.ToString();
                string LoginUserID = ViewState["Emp_ID"].ToString();
                string Office_ID = ddlOfficeName.SelectedValue.ToString();
                foreach (GridViewRow gr in GridView1.Rows)
                {

                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    Label lblEmp_ID = (Label)gr.FindControl("lblRowNumber");

                    if (chkSelect.Checked == true)
                    {
                        objdb.ByProcedure("SpPayrollSalaryDetail",
                        new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo", "LoginUserID" },
                        new string[] { "13", lblEmp_ID.ToolTip.ToString(), Office_ID, Year, MonthNo, LoginUserID }, "dataset");

                        flag = "1";
                    }
                }

                if (flag == "1")
                {

                    FillGrid();

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Salary Reset Successfully.");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please Select Atleast One Employee.');", true);
                }

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
}