using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_HRManage_HRManageAttAllow : System.Web.UI.Page
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
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillOffice();
                    FillEmployee();
                    txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEmployee()
    {
        try
        {
            ds = objdb.ByProcedure("SpHRRptDailyAttendance", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = ds;
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("ALL", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOffice()
    {
        try
        {
            ds = objdb.ByProcedure("SpHRRptDailyAttendance", new string[] { "flag" }, new string[] { "4" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                if (ViewState["Office_ID"].ToString() != "1")
                {
                    ddlOffice.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void Fillgrid()
    {
        try
        {
            lblMsg.Text = "";
            GridView2.DataSource = new String[] { };
            string FROMDATE = Convert.ToDateTime(txtStartDate.Text, cult).ToString("yyyy-MM-dd");
            string TODATE = Convert.ToDateTime(txtEndDate.Text, cult).ToString("yyyy-MM-dd");
            ds = objdb.ByProcedure("SpHRManageAttAllow", new string[] { "flag", "Emp_ID", "Office_ID", "Fromdate", "Todate" }, new string[] { "0", ddlEmployee.SelectedValue, ddlOffice.SelectedValue, FROMDATE, TODATE }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = ds;
            }
            GridView2.DataBind();
            GridView2.UseAccessibleHeader = true;
            GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            Fillgrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridView2.UseAccessibleHeader = true;
            GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;       
            string Allow_ID = e.CommandArgument.ToString();
            if (e.CommandName == "Editing")
            {
                
                ViewState["Allow_ID"] = Allow_ID.ToString();
                ds = objdb.ByProcedure("SpHRManageAttAllow", new string[] { "flag", "Allow_ID" }, new string[] { "1", Allow_ID }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                     
                    ddlAllowReason.ClearSelection();
                    ddlAllowReason.Items.FindByValue(ds.Tables[0].Rows[0]["Reason"].ToString()).Selected = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ModalMangAttAllow();", true);
                }
            }
             if (e.CommandName == "Deleting")
             {
                 objdb.ByProcedure("SpHRManageAttAllow", new string[] { "flag", "Allow_ID" }, new string[] { "3", Allow_ID }, "dataset");
                 lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Thank you!", "Operation Completed Successfully.");
                 Fillgrid();
             }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            if(ddlAllowReason.SelectedIndex == 0)
            {
                msg = "Select Allow Reason.";
            }
            if(msg == "")
            {
                objdb.ByProcedure("SpHRManageAttAllow", new string[] { "flag", "Allow_ID", "Reason" }, new string[] { "2", ViewState["Allow_ID"].ToString(),ddlAllowReason.SelectedValue }, "dataset");
                lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Thank you!", "Operation Completed Successfully.");
                Fillgrid();
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