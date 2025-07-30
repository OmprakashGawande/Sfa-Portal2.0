using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Linq;


public partial class mis_Payroll_PayrollEarnDedHeadWiseDetail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
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
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));

            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.Items.Insert(0, new ListItem("All", "0"));
            }
            ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
            if (ViewState["Office_ID"].ToString()!="1")
            {
                ddlOfficeName.Enabled = false;
            }
            

            ds = null;
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // ==== From Year ====
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
                // ==== To Year ====
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
            lblmsg1.Text = "";
            GridView2.DataSource = null;
            GridView2.DataBind();
            ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "Office_ID", "EarnDeduction_ID", "Year" },
                new string[] { "4", ddlOfficeName.SelectedValue.ToString(), ddlEarnDeducHead.SelectedValue.ToString(), ddlYear.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView2.UseAccessibleHeader = true;
            }
            else
            {
                lblmsg1.Text = "No Record Found...";
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
            string msg = "";
            lblMsg.Text = "";
            //if (ddlOfficeName.SelectedIndex == 0)
            //{
            //    msg += "Select Office. \n";
            //}
            if (ddlEarnDeducHead.SelectedIndex == 0)
            {
                msg += "Select Earn & Deduction. \n";
            }
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year. \n";
            }

            if (msg.Trim() == "")
            {

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
        try
        {
            if(ddlYear.SelectedIndex >0)
            {
                ds = null;
                ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "Year" }, new string[] { "2", ddlYear.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlEarnDeducHead.DataSource = ds;
                    ddlEarnDeducHead.DataTextField = "EarnDeduction_Name";
                    ddlEarnDeducHead.DataValueField = "EarnDeduction_ID";
                    ddlEarnDeducHead.DataBind();
                    ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlEarnDeducHead.Items.Clear();
                ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
            }
            

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}