using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_HRSetLeaveApprovalAuth : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    //FillEmployee();
                    //FillDetail();
                    FillDropDown();
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
    protected void FillDetail()
    {
        try
        {
            gvDetails.DataSource = null;
            gvDetails.DataBind();
            ds = objdb.ByProcedure("SpHRBalanceLeaveDetail", new string[] { "flag", "Office_ID", "Emp_ID", "LeaveType_ID" },
                    new string[] { "13", "0", ddlEmpList.SelectedValue.ToString(), ddlLeaveTpye.SelectedValue.ToString() }, "datatset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                btnSend.Enabled = true;
                myInput.Visible = true;
                lblmsgbox.Text = "Please select officer/employee (s) from the below list, whose leave approval authority is  <span style='color:blue'><b>" + ddlEmpList.SelectedItem.Text.ToString() + "</b></span> <br/>";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //protected void FillEmployee()
    //{
    //    try
    //    {
    //        ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
    //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlOffice.DataSource = ds;
    //            ddlOffice.DataTextField = "Office_Name";
    //            ddlOffice.DataValueField = "Office_ID";
    //            ddlOffice.DataBind();
    //            ddlOffice.Items.Insert(0, new ListItem("All", "0"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    //protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        FillDetail();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {

            int count = gvDetails.Rows.Count;
            foreach (GridViewRow gvrow in gvDetails.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                HiddenField HF_Emp_ID = (HiddenField)gvrow.FindControl("HF_Emp_ID");
                if (chk.Checked == true)
                {
                    string Approval_Emp_Id = HF_Emp_ID.Value.ToString();
                    string Approval_Updated_By = ViewState["Emp_ID"].ToString();
                    string Approval_Auth_Id = ddlEmpList.SelectedValue.ToString();
                    ds = objdb.ByProcedure("SpHRBalanceLeaveDetail",
                        new string[] { "flag", "Approval_Emp_Id", "Approval_Auth_Id", "Emp_ID", "LeaveType_ID" },
                        new string[] { "12", Approval_Emp_Id, Approval_Auth_Id, Approval_Updated_By, ddlLeaveTpye.SelectedValue.ToString() }, "dataset");
                }
                else
                {
                    string Approval_Emp_Id = HF_Emp_ID.Value.ToString();
                    string Approval_Auth_Id = ddlEmpList.SelectedValue.ToString();
                    ds = objdb.ByProcedure("SpHRBalanceLeaveDetail",
                        new string[] { "flag", "Approval_Emp_Id", "Approval_Auth_Id", "LeaveType_ID" },
                        new string[] { "16", Approval_Emp_Id, Approval_Auth_Id,ddlLeaveTpye.SelectedValue.ToString() }, "dataset");
                }
            }
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void FillDropDown()
    {
        try
        {
            ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag" }, new string[] { "37" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlLeaveTpye.DataSource = ds;
                ddlLeaveTpye.DataTextField = "Leave_Type";
                ddlLeaveTpye.DataValueField = "Leave_ID";
                ddlLeaveTpye.DataBind();
                //ddlLeaveTpye.Items.Insert(0, new ListItem("Select", "0"));
            }
            ds.Reset();

            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.Items.Insert(0, new ListItem("Select Office", "0"));
                ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
                if (ddlOfficeName.SelectedIndex > 0)
                {
                    FillEmp();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEmp()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpHRBalanceLeaveDetail", new string[] { "flag", "Office_ID" }, new string[] { "14", ddlOfficeName.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmpList.DataSource = ds;
                ddlEmpList.DataTextField = "Emp_Name";
                ddlEmpList.DataValueField = "Emp_ID";
                ddlEmpList.DataBind();
                ddlEmpList.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlOfficeName.SelectedIndex > 0)
            {
                lblMsg.Text = "";
                FillEmp();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlEmpList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlEmpList.SelectedIndex > 0)
            {
                lblMsg.Text = "";
                FillDetail();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillDetail();
    }
}