using System;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollMonthSalarySlip : System.Web.UI.Page
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
                    ddlYear.Items.Insert(0, new ListItem("Select", "0"));
                    if (ViewState["Office_ID"].ToString() == "1")
                    {
                        ddlOfficeName.Enabled = true;
                    }
                    else
                    {
                        ddlOfficeName.Enabled = false;
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
            ds.Reset();
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
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
            string Year = ddlYear.SelectedValue.ToString();
            string Month = ddlMonth.SelectedValue.ToString();
            string Office = ddlOfficeName.SelectedValue.ToString();
            string EmpType = ddlEmpType.SelectedValue.ToString();
            string QueryString = "SalarySlip.aspx?Year=" + objdb.Encrypt(Year) + "&Month=" + objdb.Encrypt(Month) + "&Office=" + objdb.Encrypt(Office) + "&EmpType=" + objdb.Encrypt(EmpType);
            Response.Write("<script>");
            Response.Write("window.open('" + QueryString + "','_blank')");
            Response.Write("</script>");
            Response.Redirect("SalarySlip.aspx?Year=" + objdb.Encrypt(Year) + "&Month=" + objdb.Encrypt(Month) + "&Office=" + objdb.Encrypt(Office) + "&EmpType=" + objdb.Encrypt(EmpType));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}