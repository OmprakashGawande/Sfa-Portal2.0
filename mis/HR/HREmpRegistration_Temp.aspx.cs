using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_HR_HREmpRegistration_Temp : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != "")
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillOffice();
                }
                else
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
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
            ds = objdb.ByProcedure("SpAdminOfficeType", new string[] { "flag" }, new string[] { "7" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeType_Title.DataSource = ds;
                ddlOfficeType_Title.DataTextField = "OfficeType_Title";
                ddlOfficeType_Title.DataValueField = "OfficeType_Title";
                ddlOfficeType_Title.DataBind();
                ddlOfficeType_Title.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlOfficeType_Title.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        ddlOffice.ClearSelection();
        txtEmpName.Text = "";
        txtBasicSalary.Text = "";
        ddlOfficeType_Title.ClearSelection();
    }
    protected void ddlOfficeType_Title_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlOfficeType_Title.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "OfficeType_Title" }, new string[] { "7", ddlOfficeType_Title.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOffice.DataSource = ds;
                    ddlOffice.DataTextField = "Office_Name";
                    ddlOffice.DataValueField = "Office_ID";
                    ddlOffice.DataBind();
                    ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
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
            if (ddlOffice.SelectedIndex == 0)
            {
                msg += "Select Office <br/>";
            }
            if (ddlOfficeType_Title.SelectedIndex == 0)
            {
                msg += "select Office Type<br/>";
            }
            if (txtEmpName.Text.Trim() == "")
            {
                msg += "Enter Employee Name <br/>";
            }
            if (ddlEmpType.SelectedIndex == 0)
            {
                msg += "select Employee Type<br/>";
            }
            if (txtBasicSalary.Text.Trim() == "")
            {
                msg += "Enter Basic Salary <br/>";
            }
            if (msg == "")
            {
               ds = objdb.ByProcedure("SpHREmployee",
                new string[] { "flag", "Office_ID", "OfficeType_Title", "Emp_Name", "Emp_TypeOfPost", "Emp_BasicSalery", "EPF_No", "UAN_No" },
                new string[] { "31", ddlOffice.SelectedValue.ToString(), ddlOfficeType_Title.SelectedItem.Text, txtEmpName.Text.Trim(), ddlEmpType.SelectedValue.ToString(), txtBasicSalary.Text.Trim(), txtEPFNo.Text.Trim(), txtUANNo.Text.Trim() }, "dataset");
               string UserId = ds.Tables[1].Rows[0]["UserName"].ToString();
               lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "User ID '" + UserId + "' Successfully Createed");
                ClearText();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}