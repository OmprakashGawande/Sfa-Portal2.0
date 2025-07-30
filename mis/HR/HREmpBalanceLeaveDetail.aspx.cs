using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HREmpBalanceLeaveDetail : System.Web.UI.Page
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
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Department_ID"] = Session["Department_ID"].ToString();
                    TxtDate.Attributes.Add("readonly", "readonly");
                      TxtDate.Text = "01/01/"+ DateTime.Now.Year.ToString();
                    //TxtDate.Text = DateTime.Now.ToString("31/12/2018");
                    FillDropDown();
                    DivInsertDetail.Visible = false;
                    DivFillDetail.Visible = false;
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
    protected void FillDropDown()
    {
        try
        {
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
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ddlOfficeName.SelectedValue.ToString() }, "dataset");
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
    protected void FillGrid()
    {
        try
        {
            lblMsg.Text = "";
            DivFillDetail.Visible = false;
            DivInsertDetail.Visible = true;
            ds = objdb.ByProcedure("SpHRBalanceLeaveDetail", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
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
    protected void FillDetail()
    {
        try
        {
            lblMsg.Text = "";
            DivFillDetail.Visible = true;
            DivInsertDetail.Visible = false;
            ds = objdb.ByProcedure("SpHRBalanceLeaveDetail", new string[] { "flag", "Office_ID", "Emp_ID" }, new string[] { "3", ddlOfficeName.SelectedValue.ToString(), ddlEmpList.SelectedValue.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = new string[] { };
                GridView2.DataBind();
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
            if (ddlOfficeName.SelectedIndex == 0)
            {
                msg += "Select Office Name";
            }
            if (ddlEmpList.SelectedIndex == 0)
            {
                msg += "Select Employee Name";
            }
            if (TxtDate.Text == "")
            {
                msg += "Select Date";
            }
            if (msg == "")
            {
                FillGrid();
                DivInsertDetail.Visible = true;
                DivFillDetail.Visible = false;
            }
            else
            {
                lblMsg.Text = "";
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
            ddlEmpList.ClearSelection();
            foreach (GridViewRow row in GridView1.Rows)
            {
                TextBox txtLeaveDays = (TextBox)row.FindControl("txtLeaveDays");
                txtLeaveDays.Text = "";
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
            foreach (GridViewRow row in GridView1.Rows)
            {
                Label lblLeaveID = (Label)row.FindControl("lblLeave_ID");
                TextBox txtLeaveDays = (TextBox)row.FindControl("txtLeaveDays");
                if (txtLeaveDays.Text == "")
                {
                    txtLeaveDays.Text = "0";
                }
                objdb.ByProcedure("SpHRBalanceLeaveDetail", new string[] { "flag", "Office_ID", "Emp_ID", "Date", "LeaveType_ID", "LeaveDays", "UpdatedBy" },
                    new string[] { "8", ddlOfficeName.SelectedValue.ToString(), ddlEmpList.SelectedValue.ToString(), Convert.ToDateTime(TxtDate.Text, cult).ToString("yyyy/MM/dd"), lblLeaveID.Text, txtLeaveDays.Text, ViewState["Emp_ID"].ToString() }, "dataset");
            }
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            ClearText();
            DivInsertDetail.Visible = false;

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
            FillDetail();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}