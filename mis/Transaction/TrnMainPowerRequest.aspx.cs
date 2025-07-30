using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class mis_Transaction_TrnMainPowerRequest : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
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
                GetProjecName();
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
    public void GetProjecName()
    {
        try
        {
            DataSet ds = objdb.ByProcedure("Usp_GetTaskAllocationDropdown", new string[] { "flag" }, new string[] { "1" }, "dataset");


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlProjectB.DataSource = ds.Tables[0];
                ddlProjectB.DataTextField = "ProjectName";
                ddlProjectB.DataValueField = "ProjectId";
                ddlProjectB.DataBind();

                ddlProjectW.DataSource = ds.Tables[0];
                ddlProjectW.DataTextField = "ProjectName";
                ddlProjectW.DataValueField = "ProjectId";
                ddlProjectW.DataBind();
            }
            ddlProjectB.Items.Insert(0, new ListItem("Select", "0"));
            ddlProjectW.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            throw new Exception("Error while binding Type of Project dropdown: " + ex.Message);
        }
    }
    //private void BindGrid()
    //{
    //    // Example: Replace with your actual data source
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("EmpId");
    //    dt.Columns.Add("EmployeeName");
    //    dt.Columns.Add("Designation");
    //    dt.Columns.Add("BenchFromDate");

    //    // Add sample data
    //    dt.Rows.Add(1, "Kapil", "Developer", "25/06/2025");
    //    Grid.DataSource = dt;
    //    Grid.DataBind();
    //    Grid.DataSource = dt;
    //    Grid.DataBind();
    //}

    //private void BindGrid2()
    //{
    //    // Example: Replace with your actual data source
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("EmpId");
    //    dt.Columns.Add("EmployeeName");
    //    dt.Columns.Add("Designation");
    //    dt.Columns.Add("Manager");
    //    dt.Columns.Add("Project");


    //    // Add sample data
    //    dt.Rows.Add(1, "Kapil", "Developer", "Ramesh", "Education Portal");
    //    GridView1.DataSource = dt;
    //    GridView1.DataBind();
    //    GridView1.DataSource = dt;
    //    GridView1.DataBind();
    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Grid.DataSource = null;
            Grid.DataBind();
            Grid2.DataSource = null;
            Grid2.DataBind();
            div1.Visible = true;
            div2.Visible = true;

            string EmpID = "0";


            if (ddlEmployee.SelectedValue != "0")
            {
                EmpID = ddlEmployee.SelectedValue;
            }
            else
            {
                EmpID = ViewState["Emp_ID"].ToString();
            }




             DataSet ds = objdb.ByProcedure("Usp_GetMainPowerRequest", new string[] { "EmpId" }, new string[] { EmpID }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Grid.DataSource = ds.Tables[0];
                Grid.DataBind();
            }
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                Grid2.DataSource = ds.Tables[1];
                Grid2.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["BanchEmpId"] = "";
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblEmployeeName = (Label)row.FindControl("lblEmployeeName");
            Label lblDesignation = (Label)row.FindControl("lblDesignation");

            txtEmpName.Text = lblEmployeeName.Text;
            txtDesignation.Text = lblDesignation.Text;
            ViewState["BanchEmpId"] = e.CommandArgument;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchModal", "$('#exampleModal2').modal('show');", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Grid2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["WorkingEmpId"] = "";
            ViewState["WorkingProjectId"] = "";
            ViewState["ManagerId"] = "";
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblEmployeeName2 = (Label)row.FindControl("lblEmployeeName2");
            Label lblDesignation2 = (Label)row.FindControl("lblDesignation2");
            Label lblProjectName = (Label)row.FindControl("lblProjectName");
            Label lblWorkingProjectId = (Label)row.FindControl("lblWorkingProjectId");
            Label lblManagerId = (Label)row.FindControl("lblManagerId");

            txtEmpName2.Text = lblEmployeeName2.Text;
            txtDesignation2.Text = lblDesignation2.Text;
            txtWorkingProject.Text = lblProjectName.Text;
            ViewState["WorkingEmpId"] = e.CommandArgument;
            ViewState["WorkingProjectId"] = lblWorkingProjectId.Text;
            ViewState["ManagerId"] = lblManagerId.Text;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchModal", "$('#exampleModal3').modal('show');", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnBech_Click(object sender, EventArgs e)
    {
        try
        {
            string FromDateB = txtFromDateB.Text != "" ? Convert.ToDateTime(txtFromDateB.Text, cult).ToString("yyyy/MM/dd") : "";
            string ToDateB = txtToDateB.Text != "" ? Convert.ToDateTime(txtToDateB.Text, cult).ToString("yyyy/MM/dd") : "";

            if (btnBech.Text == "Request")
            {
                ds = objdb.ByProcedure("Usp_InsertManPowerRequest", new string[] { "EmpId", "Forwardedto", "RequestedProjectId", "RequestStatus"
                    , "FromDate","FromDateDay", "ToDate","ToDateDay", "Remark", "UserTypeId", "OfficeId", "CreatedBy", "CreatedByIP" }, new string[] {
                   ViewState["BanchEmpId"].ToString(),"HR",ddlProjectB.SelectedValue,"1",FromDateB,ddlFromDateDay.SelectedItem.Text,ToDateB,ddlToDateDay.SelectedItem.Text,
                        txtRemarkB.Value.Trim(),Session["UserTypeId"].ToString(),Session["Office_ID"].ToString(),ViewState["Emp_ID"].ToString(),objdb.GetLocalIPAddress() }, "dataset");
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);

                    ViewState["BanchEmpId"] = "";
                    ddlProjectB.ClearSelection();
                    ddlFromDateDay.ClearSelection();
                    ddlToDateDay.ClearSelection();
                    txtFromDateB.Text = "";
                    txtToDateB.Text = "";
                    txtRemarkB.Value = "";
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                }
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void tbnWorkin_Click(object sender, EventArgs e)
    {
        try
        {
            string FromDateW = txtFromDateW.Text != "" ? Convert.ToDateTime(txtFromDateW.Text, cult).ToString("yyyy/MM/dd") : "";
            string ToDateW = txtToDateW.Text != "" ? Convert.ToDateTime(txtToDateW.Text, cult).ToString("yyyy/MM/dd") : "";

            if (btnBech.Text == "Request")
            {
                ds = objdb.ByProcedure("Usp_InsertManPowerRequest", new string[] { "WorkingProjectId","EmpId", "ManagerId","Forwardedto", "RequestedProjectId", "RequestStatus"
                    , "FromDate","FromDateDay", "ToDate","ToDateDay", "Remark", "UserTypeId", "OfficeId", "CreatedBy", "CreatedByIP" }, new string[] {
                   ViewState["WorkingProjectId"].ToString(),ViewState["WorkingEmpId"].ToString(), ViewState["ManagerId"].ToString() ,"Manager",ddlProjectW.SelectedValue,"1",FromDateW,
                        ddlfromDatedayW.SelectedItem.Text,ToDateW,ddlTodatedayW.SelectedItem.Text,
                        txtRemarkB.Value.Trim(),Session["UserTypeId"].ToString(),Session["Office_ID"].ToString(),ViewState["Emp_ID"].ToString(),objdb.GetLocalIPAddress() }, "dataset");
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);

                    ViewState["BanchEmpId"] = "";
                    ddlProjectB.ClearSelection();
                    ddlFromDateDay.ClearSelection();
                    ddlToDateDay.ClearSelection();
                    txtFromDateB.Text = "";
                    txtToDateB.Text = "";
                    txtRemarkB.Value = "";
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                }
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}