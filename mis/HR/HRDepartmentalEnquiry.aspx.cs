using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_HR_HRDepartmentalEnquiry : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    txtOrderDate.Attributes.Add("readonly", "readonly");
                    FillDropdown();
                }
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
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("Select Office", "0"));
            }

            ds.Reset();
            ds = objdb.ByProcedure("SpHRDepartmentalEnquiry", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlPresentingOfficer.DataSource = ds;
                ddlPresentingOfficer.DataTextField = "Emp_Name";
                ddlPresentingOfficer.DataValueField = "Emp_ID";
                ddlPresentingOfficer.DataBind();
                ddlPresentingOfficer.Items.Insert(0, new ListItem("Select Presenting Officer", "0"));


                ddlEnquiryOfficer.DataSource = ds;
                ddlEnquiryOfficer.DataTextField = "Emp_Name";
                ddlEnquiryOfficer.DataValueField = "Emp_ID";
                ddlEnquiryOfficer.DataBind();
                ddlEnquiryOfficer.Items.Insert(0, new ListItem("Select Enquiry Officer", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ddlOffice.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployeeName.DataSource = ds;
                ddlEmployeeName.DataTextField = "Emp_Name";
                ddlEmployeeName.DataValueField = "Emp_ID";
                ddlEmployeeName.DataBind();
                ddlEmployeeName.Items.Insert(0, new ListItem("Select Employee", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        try
        {
            ddlOffice.ClearSelection();
            ddlEnquiryOfficer.ClearSelection();
            txtOrderNo.Text = "";
            txtOrderDate.Text = "";
            ddlEnquiryOfficer.ClearSelection();
            ddlPresentingOfficer.ClearSelection();
            txtRemark.Text = "";
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
            string msg = "";
            if (ddlOffice.SelectedIndex == 0)
            {
                msg = msg + "Select Office Name. \\n";
            }
            if (ddlEmployeeName.SelectedIndex == 0)
            {
                msg = msg + "Select Employee Name. \\n";
            }
            if (txtOrderNo.Text == "")
            {
                msg = msg + "Enter Order No. \\n";
            }
            if (txtOrderDate.Text == "")
            {
                msg = msg + "Select Order Date. \\n";
            }
            if (ddlEnquiryOfficer.SelectedIndex == 0)
            {
                msg = msg + "Select Enquiry Officer. \\n";
            }
            if (ddlOffice.SelectedIndex == 0)
            {
                msg = msg + "Select Office Name. \\n";
            }
            if (ddlPresentingOfficer.SelectedIndex == 0)
            {
                msg = msg + "Select Presenting Officer. \\n";
            }
            if (msg == "")
            {
                objdb.ByProcedure("SpHRDepartmentalEnquiry", new string[] { "flag", "Emp_ID", "EmployeeName", "OrderNo", "OrderDate", "EnquiryOfficer", "PresentingOfficer", "Remark", "IsActive", "Enquiry_Updatedby" },
                    new string[] { "0", ddlEmployeeName.SelectedValue.ToString(), ddlEmployeeName.SelectedItem.Text, txtOrderNo.Text, Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy/MM/dd"), ddlEnquiryOfficer.SelectedItem.Text, ddlPresentingOfficer.SelectedItem.Text, txtRemark.Text, "1", ViewState["Emp_ID"].ToString() }, "dataset");
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                ClearText();
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