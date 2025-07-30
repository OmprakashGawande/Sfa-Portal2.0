using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollSetEarnDedNextMonth : System.Web.UI.Page
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
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "8" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlFromYear.DataSource = ds;
                ddlFromYear.DataTextField = "Year";
                ddlFromYear.DataValueField = "Year";
                ddlFromYear.DataBind();
                ddlFromYear.Items.Insert(0, new ListItem("Select", "0"));

                ddlToYear.DataSource = ds;
                ddlToYear.DataTextField = "Year";
                ddlToYear.DataValueField = "Year";
                ddlToYear.DataBind();
                ddlToYear.Items.Insert(0, new ListItem("Select", "0"));
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
            lblMsg.Text = "";


            //ds = objdb.ByProcedure("SpPayrollEmpArrear", new string[] { "flag", "CurrentYear", "CurrentMonth", "Office_ID" },
            //    new string[] { "14", ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOffice.SelectedValue.ToString() }, "dataset"); //"4"

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
            if (ddlFromYear.SelectedIndex <= 0)
            {
                msg += "Select From Year.  \\n";
            }
            if (ddlFromMonth.SelectedIndex <= 0)
            {
                msg += "Select From Month.  \\n";
            }
            if (ddlToYear.SelectedIndex <= 0)
            {
                msg += "Select To Year.  \\n";
            }
            if (ddlToMonth.SelectedIndex <= 0)
            {
                msg += "Select To Month.  \\n";
            }
            if (msg.Trim() == "")
            {
                string Status = "";
                int FromYear = int.Parse(ddlFromYear.SelectedValue.ToString());
                int FromMonth = int.Parse(ddlFromMonth.SelectedValue.ToString());
                int ToYear = int.Parse(ddlToYear.SelectedValue.ToString());
                int ToMonth = int.Parse(ddlToMonth.SelectedValue.ToString());

                string FromMonthName = ddlFromMonth.SelectedItem.ToString();
                string ToMonthName = ddlToMonth.SelectedItem.ToString();
                if (FromYear == ToYear)
                {
                    if (FromMonth >= ToMonth)
                    {
                        msg = "Select Valid Year and Month.  \\n";
                    }
                }
                else if (FromYear > ToYear)
                {
                    msg = "Select Valid Year and Month.  \\n";
                }
                if (msg.Trim() == "")
                {
                    string class1 ="";
                    ds = objdb.ByProcedure("SpPayrollSetEarnDedNextMonth",
                        new string[] { "flag", "Office_ID ", "FromYear", "ToYear", "FromMonth", "ToMonth", "FromMonthName", "ToMonthName" },
                        new string[] { "1", ViewState["Office_ID"].ToString(), FromYear.ToString(), ToYear.ToString(), FromMonth.ToString(), ToMonth.ToString(), FromMonthName, ToMonthName }, "dataset");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Status = ds.Tables[0].Rows[0]["Status"].ToString();
                        class1 = ds.Tables[0].Rows[0]["Class"].ToString();
                    }
                    lblMsg.Text = objdb.Alert("fa-check", class1.ToString(), "Massage!", Status.ToString());
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
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