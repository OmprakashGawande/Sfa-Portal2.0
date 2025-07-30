using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;


public partial class mis_HR_HREmpACR : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillDropDown();
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    FillGrid();
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
    protected void FillGrid()
    {
        try
        {
            ds = objdb.ByProcedure("SpHrEmpACR", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
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
            ddlEmployee.ClearSelection();
            ddlDepartment.ClearSelection();
            ddlDesignation.ClearSelection();
            ddlYear.ClearSelection();
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtRemark.Text = "";
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
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOldOffice.DataSource = ds;
                ddlOldOffice.DataTextField = "Office_Name";
                ddlOldOffice.DataValueField = "Office_ID";
                ddlOldOffice.DataBind();
                ddlOldOffice.Items.Insert(0, new ListItem("Select Old Office", "0"));
            }
            ddlOldOffice.SelectedValue = ViewState["Office_ID"].ToString();

            ds.Reset();
            ds = objdb.ByProcedure("SpHREmpIncreament", new string[] { "flag", "Office_ID" }, new string[] { "5", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = ds;
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("Select Employee", "0"));
            }

            ds.Reset();
            ds = objdb.ByProcedure("SpAdminDepartment", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.DataSource = ds;
                ddlDepartment.DataTextField = "Department_Name";
                ddlDepartment.DataValueField = "Department_ID";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
            }

            ds.Reset();
            ds = objdb.ByProcedure("SpAdminDesignation", new string[] { "flag" }, new string[] { "7" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlDesignation.DataSource = ds;
                ddlDesignation.DataTextField = "Designation_Name";
                ddlDesignation.DataValueField = "Designation_ID";
                ddlDesignation.DataBind();
                ddlDesignation.Items.Insert(0, new ListItem("Select", "0"));
            }

            ds.Reset();
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "7" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select Year", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDepartment.ClearSelection();
            ddlDesignation.ClearSelection();
            if (ddlEmployee.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Emp_ID" }, new string[] { "21", ddlEmployee.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ddlDepartment.ClearSelection();
                    ddlDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["Department_ID"].ToString()).Selected = true;
                    ddlDesignation.ClearSelection();
                    ddlDesignation.Items.FindByValue(ds.Tables[0].Rows[0]["Designation_ID"].ToString()).Selected = true;
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
            string Doc_Path = "";
            if (ddlEmployee.SelectedIndex == 0)
            {
                msg += "Select Employee.\n";
            }
            if (ddlDepartment.SelectedIndex == 0)
            {
                msg += "Select Department.\n";
            }
            if (ddlDesignation.SelectedIndex == 0)
            {
                msg += "Select Designation.\n";
            }
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year.\n";
            }
            if (txtFromDate.Text == "")
            {
                msg += "Select From Date.\n";
            }
            if (txtToDate.Text == "")
            {
                msg += "Select To Date.\n";
            }
            if (txtRemark.Text == "")
            {
                msg += "Enter Remark.\n";
            }
            if (Doc_Upload.HasFile)
            {
                Doc_Path = "../HR/ACRDoc/" + Guid.NewGuid() + "-" + Doc_Upload.FileName;
                Doc_Upload.PostedFile.SaveAs(Server.MapPath(Doc_Path));
            }
            if (msg == "")
            {
                if (btnSave.Text == "Save")
                {
                    objdb.ByProcedure("SpHrEmpACR", new string[] { "flag", "OfficeID", "Emp_ID", "Department_ID", "Designation_ID", "Year", "From_Date", "To_Date", "Attach_Doc", "Remark", "Updated_By" },
               new string[] { "0", ddlOldOffice.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), ddlDepartment.SelectedValue.ToString(), ddlDesignation.SelectedValue.ToString(), ddlYear.SelectedItem.Text, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy-MM-dd"), Doc_Path, txtRemark.Text, ViewState["Emp_ID"].ToString() }, "datatset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}