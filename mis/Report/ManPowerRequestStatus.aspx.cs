using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Report_ManPowerRequestStatus : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["UserTypeId"] = Session["UserTypeId"].ToString();
                ViewState["Designation_ID"] = Session["Designation_ID"].ToString();
                BindDropdown();
                btnSearch_Click(sender, e);
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    public void BindDropdown()
    {
        try
        {
            string empId = "0";

            DataSet ds3 = objdb.ByProcedure("Usp_GetddlEmployee", new string[] { "EmpId" }, new string[] { empId }, "dataset");

            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {

                ddlEmployee.DataSource = ds3.Tables[0];
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_ID";
                ddlEmployee.DataBind();
            }
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            // Optional: log or show error
            throw new Exception("Error while binding Type of Project dropdown: " + ex.Message);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Grid.DataSource = null;
            Grid.DataBind();
            div1.Visible = true;

            string CreatedBy = "0";

            if (ViewState["Designation_ID"].ToString() == "8" || ViewState["Designation_ID"].ToString() == "13")
            {

                CreatedBy = ViewState["Emp_ID"].ToString();
            }

            DataSet ds = objdb.ByProcedure("Usp_GetManPowerReqDetail", new string[] { "EmpId", "CreatedBy", "Forwardedto" }, new string[] {ddlEmployee.SelectedValue,CreatedBy,ddlForwardedto.SelectedValue  }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Grid.DataSource = ds.Tables[0];
                Grid.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}