using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_HR_HRDepartmentalEnquiryList : System.Web.UI.Page
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
                    txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    FillGrid();
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
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHRDepartmentalEnquiry", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds != null && ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid2()
    {
        try
        {
            GridView2.DataSource = new string[] { };
            GridView2.DataBind();
            ds = objdb.ByProcedure("SpHRDepartmentalEnquiry", new string[] { "flag", "DepartmentEnquiry_ID" }, new string[] { "5", ViewState["DepartmentEnquiry_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
                    lblMsg.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["DepartmentEnquiry_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpHRDepartmentalEnquiry", new string[] { "flag", "DepartmentEnquiry_ID" }, new string[] { "3", ViewState["DepartmentEnquiry_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                txtRemark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
                txtOrderNo.Text = ds.Tables[0].Rows[0]["OrderNo"].ToString();
            }
            FillGrid2();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
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
            ddlEnquiryOfficer.ClearSelection();
            ddlPresentingOfficer.ClearSelection();
            txtENQ_Remark.Text = "";
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
            if (txtOrderNo.Text == "")
            {
                msg = msg + "Enter Order No.//n";
            }
            if (txtOrderDate.Text == "")
            {
                msg = msg + "Enter Order Date.//n";
            }
            if (ddlEnquiryOfficer.SelectedIndex == 0)
            {
                msg = msg + "Select Enquiry Officer.//n";
            }
            if (ddlPresentingOfficer.SelectedIndex == 0)
            {
                msg = msg + "Select Presenting Officer.//n";
            }
            if (msg == "")
            {
                objdb.ByProcedure("SpHRDepartmentalEnquiry", new string[] { "flag", "DepartmentEnquiry_ID", "Status", "ENQ_OrderNo", "ENQ_OrderDate", "EnquiryOfficer", "PresentingOfficer", "ENQ_Remark", "UpdatedBy" },
      new string[] { "4", ViewState["DepartmentEnquiry_ID"].ToString(), RblStatus.SelectedItem.Text, txtOrderNo.Text, Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy/MM/dd"), ddlEnquiryOfficer.SelectedItem.Text, ddlPresentingOfficer.SelectedItem.Text, txtENQ_Remark.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                ClearText();
                FillGrid2();
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