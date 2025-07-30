using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Linq;


public partial class mis_PayrollEarnDedHeadEmpWiseDetail : System.Web.UI.Page
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
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            ddlFYear.Items.Insert(0, new ListItem("Select", "0"));
            ddlTYear.Items.Insert(0, new ListItem("Select", "0"));

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
            ds = null;
            ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag" }, new string[] { "5" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEarnDeducHead.DataSource = ds;
                ddlEarnDeducHead.DataTextField = "EarnDeduction_Name";
                ddlEarnDeducHead.DataValueField = "EarnDeduction_ID";
                ddlEarnDeducHead.DataBind();
                ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
            }


            ds = null;
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // ==== From Year ====
                ddlFYear.DataSource = ds;
                ddlFYear.DataTextField = "Year";
                ddlFYear.DataValueField = "Year";
                ddlFYear.DataBind();
                ddlFYear.Items.Insert(0, new ListItem("Select", "0"));
                // ==== To Year ====
                ddlTYear.DataSource = ds;
                ddlTYear.DataTextField = "Year";
                ddlTYear.DataValueField = "Year";
                ddlTYear.DataBind();
                ddlTYear.Items.Insert(0, new ListItem("Select", "0"));
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
            ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "Emp_ID", "EarnDeduction_ID", "FromYear", "ToYear" },
                new string[] { "3", ddlEmployee.SelectedValue.ToString(), ddlEarnDeducHead.SelectedValue.ToString(), ddlFYear.SelectedValue.ToString(), ddlTYear.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView2.UseAccessibleHeader = true;
            }
            else
            {
                lblmsg1.Text = "No Record Found";
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
            if (ddlOfficeName.SelectedIndex == 0)
            {
                msg += "Select Office. \n";
            }
            if (ddlEmployee.SelectedIndex == 0)
            {
                msg += "Select Employee Name. \n";
            }
            if (ddlEarnDeducHead.SelectedIndex == 0)
            {
                msg += "Select Earn & Deduction. \n";
            }
            if (ddlFYear.SelectedIndex == 0)
            {
                msg += "Select From Year. \n";
            }
            if (ddlTYear.SelectedIndex == 0)
            {
                msg += "Select To Year. \n";
            }
            
            if (msg.Trim() == "")
            {
                int FYear = int.Parse(ddlFYear.SelectedValue.ToString());
                int TYear = int.Parse(ddlTYear.SelectedValue.ToString());
                if (FYear > TYear)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select correct Year and Month.');", true);
                }
                else
                {
                    FillGrid();
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
    
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        // GridView1
        try
        {

            ddlFYear.ClearSelection();
            ddlTYear.ClearSelection();
           

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}