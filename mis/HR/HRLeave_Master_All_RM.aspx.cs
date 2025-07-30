using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_HR_HRLeave_Master_All_RM : System.Web.UI.Page
{
    DataSet ds, ds2;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                lblMsg.Text = "";
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                
                FillFinancialYear();
                FillLeaveType();

                if (ViewState["Office_ID"].ToString() == "1")
                {
                    //ddlOldOffice.Attributes.Add("readonly", "readonly");
                    ddlOffice.Enabled = true;
                }

                FillOffices();
                
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillFinancialYear()
    {
        try
        {
            ds = objdb.ByProcedure("SpHrYear_Master",
                           new string[] { "flag" },
                           new string[] { "2" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlFinancial_Year.DataTextField = "Year";
                ddlFinancial_Year.DataValueField = "Year";
                ddlFinancial_Year.DataSource = ds;
                ddlFinancial_Year.DataBind();
                ddlFinancial_Year.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOffices()
    {
        try
        {
            //ds = objdb.ByProcedure("SpAdminOffice",
            //            new string[] { "flag" },
            //            new string[] { "10" }, "dataset");

            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlOffice.DataSource = ds;
            //    ddlOffice.DataTextField = "Office_Name";
            //    ddlOffice.DataValueField = "Office_ID";
            //    ddlOffice.DataBind();
            //    ddlOffice.Items.Insert(0, new ListItem("Select Office", "0"));

            //}
            //ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag", "Office_ID" },
                           new string[] { "20", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("Select Office", "0"));
            }
            //ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillLeaveType()
    {
        try
        {
            ds = objdb.ByProcedure("SpHRLeave_Type",
                                    new string[] { "flag" },
                                    new string[] { "2" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlLeave_Type.DataTextField = "Leave_Type";
                ddlLeave_Type.DataValueField = "Leave_ID";
                ddlLeave_Type.DataSource = ds;
                ddlLeave_Type.DataBind();
                ddlLeave_Type.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillData()
    {
        try
        {

            lblGridHeading.Text = "<h5><b>Office:</b> " + ddlOffice.SelectedItem.ToString() + ",  <b>Leave Type:</b> " + ddlLeave_Type.SelectedItem.ToString() + ",  <b>Year:</b> " + ddlFinancial_Year.SelectedItem.ToString() + "</h5>";

            ds = objdb.ByProcedure("SpHRLeaveAssignMaster", new string[] { "flag", "AdminOffice_ID" }, new string[] { "0", ddlOffice.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }


            lblMsg.Text = "";
            BtnSubmit.Enabled = true;
            foreach (GridViewRow row in GridView1.Rows)
            {
                TextBox txtAvailableLeave = (TextBox)row.FindControl("txtAvailableLeave");
                TextBox txtLastYearLeave = (TextBox)row.FindControl("txtLastYearLeave");
                Label EmpId = (Label)row.FindControl("LblEmployeeId");
                Label lblAvailableSelectYear = (Label)row.FindControl("lblAvailableSelectYear");
                Button BtnSave = (Button)row.FindControl("BtnSave");

                ds2 = objdb.ByProcedure("SpHRLeaveAssignMaster", new string[] { "flag", "Emp_ID", "Financial_Year", "LeaveType_ID" }, new string[] { "1", EmpId.Text.ToString(), ddlFinancial_Year.SelectedValue.ToString(), ddlLeave_Type.SelectedValue.ToString() }, "dataset");
                if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                {
                    txtAvailableLeave.Text = ds2.Tables[0].Rows[0]["Leave_Days"].ToString();
                    txtLastYearLeave.Text =  ds2.Tables[0].Rows[0]["Leave_LastYearClosing"].ToString();
                    int TotalAvailableLeave = int.Parse(ds2.Tables[0].Rows[0]["Leave_Days"].ToString()) + int.Parse(ds2.Tables[0].Rows[0]["Leave_LastYearClosing"].ToString());
                    lblAvailableSelectYear.Text = (TotalAvailableLeave == null) ? "0" : TotalAvailableLeave.ToString()+" Day(s)";
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }
                else { }

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
            lblMsg.Text = "";
            string msg = "";

            if (ddlFinancial_Year.SelectedIndex < 1)
            {
                msg += "Please Select Year. \\n";
            }
            if (ddlLeave_Type.SelectedIndex < 1)
            {
                msg += "Please Select Leave Type. \\n";
            }
            if (ddlOffice.SelectedIndex < 1)
            {
                msg += "Please Select Office. \\n";
            }
            if (msg.Trim() == "")
            {
                FillData();
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
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlOffice.SelectedIndex == 0)
        //{
        //    GridView1.DataSource = null;
        //    GridView1.DataBind();
        //}
        //if (ddlOffice.SelectedIndex > 0)
        //{
            GridView1.DataSource = null;
            GridView1.DataBind();
       // }
        BtnSubmit.Enabled = false;
        lblGridHeading.Text = "";
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";

            if (ddlFinancial_Year.SelectedIndex < 1)
            {
                msg += "Please Select Year. \\n";
            }
            if (ddlLeave_Type.SelectedIndex < 1)
            {
                msg += "Please Select Leave Type. \\n";
            }
            if (ddlOffice.SelectedIndex < 1)
            {
                msg += "Please Select Office. \\n";
            }
            if (msg.Trim() == "")
            {
                string CarryForwardOrNot = "N";

                if (ddlLeave_Type.SelectedItem.ToString() == "Earned Leave" || ddlLeave_Type.SelectedItem.ToString() == "Medical Leave" || ddlLeave_Type.SelectedValue.ToString() == "1" || ddlLeave_Type.SelectedValue.ToString() == "3")
                {
                    CarryForwardOrNot = "Y";
                }
                foreach (GridViewRow row in GridView1.Rows)
                {

                    Label EmpId = (Label)row.FindControl("LblEmployeeId");
                    Label EmpName = (Label)row.FindControl("LblEmployee");
                    Label Designation = (Label)row.FindControl("LblDesignation");
                    TextBox txtAvailableLeave = (TextBox)row.FindControl("txtAvailableLeave");
                    TextBox txtLastYearLeave = (TextBox)row.FindControl("txtLastYearLeave");


                    objdb.ByProcedure("SpHRLeaveAssignMaster",
                              new string[] { "flag", "LeaveMaster_IsActive", "Financial_Year", "LeaveType_ID", "Leave_Days", "LeaveMaster_UpdatedBy", "Emp_ID", "LeaveCarryFor", "Leave_LastYearClosing" },
                              new string[] { "2", "1", ddlFinancial_Year.SelectedValue.ToString(), ddlLeave_Type.SelectedValue.ToString(), txtAvailableLeave.Text.ToString(), ViewState["Emp_ID"].ToString(), EmpId.Text.ToString(), CarryForwardOrNot.ToString(), txtLastYearLeave.Text.ToString() }, "dataset");

                }
                 
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
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
    protected void ddlFinancial_Year_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            BtnSubmit.Enabled = false;
            lblGridHeading.Text = "";
            lblMsg.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlLeave_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            BtnSubmit.Enabled = false;
            lblGridHeading.Text = "";
            lblMsg.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}