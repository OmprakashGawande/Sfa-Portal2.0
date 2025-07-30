using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_HR_HREmpCancelLeaveRequest : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
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

                    txtReason.Attributes.Add("readonly", "readonly");
                    FillDropdown();
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

    protected void FillDropdown()
    {
        try
        {
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlFinancialYear.DataSource = ds;
                ddlFinancialYear.DataTextField = "Year";
                ddlFinancialYear.DataValueField = "Year";
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select Office", "0"));
            }
            ddlFinancialYear.SelectedValue = DateTime.Now.Year.ToString(); ;

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
            lblMsg2.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "Office_ID", "LeaveApproveAuthority", "Year" },
                  new string[] { "18", ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), ddlFinancialYear.SelectedValue.ToString() }, "datatset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                lblMsg2.Text = "No Record Found...";
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
            ViewState["LeaveId"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId" },
                  new string[] { "4", ViewState["LeaveId"].ToString() }, "datatset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                DetailsView1.DataSource = ds;
                DetailsView1.DataBind();
                txtReason.Text = ds.Tables[0].Rows[0]["LeaveRemark"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
                ViewState["LeaveType"] = ds.Tables[0].Rows[0]["LeaveType"].ToString();
                ViewState["EmpID"] = ds.Tables[0].Rows[0]["Emp_ID"].ToString();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds1;
            string DocPath = "";
            lblMsg.Text = "";
            string msg = "";
            string TakenLeave = "";
            string LeaveStatus = "";
            if (txtRemark.Text == "")
            {
                msg += "Enter Remark. <br/>";
            }
            if (msg == "")
            {
                ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveType_ID", "Financial_Year" },
            new string[] { "7", ViewState["LeaveType"].ToString(), ddlFinancialYear.SelectedValue.ToString() }, "datatset");
                string financialYear = ds.Tables[0].Rows[0]["Financial_Year"].ToString();
                if (chkCancel.Checked == true)
                {
                    LeaveStatus = "Cancelled";
                    ds1 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId" },
                new string[] { "8", ViewState["LeaveId"].ToString() }, "datatset");
                    TakenLeave = ds1.Tables[0].Rows[0]["TakenLeave"].ToString();
                }
                else
                {
                    TakenLeave = "0";
                }
                objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "Emp_ID", "LeaveType", "TotalTakenLeave", "FinancialYear" },
                new string[] { "19", ViewState["EmpID"].ToString(), ViewState["LeaveType"].ToString(), TakenLeave, financialYear }, "datatset");

                objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId", "LeaveStatus", "RemarkByApprovalAuth" },
                new string[] { "20", ViewState["LeaveId"].ToString(), LeaveStatus, txtRemark.Text }, "datatset");
                FillGrid();
                lblMsg.Text = "<div class='alert alert-success alert-dismissible'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button><h4><i class='icon fa fa-check'></i> Success</h4>Leave has been " + LeaveStatus + ". <a href='HRLeaveAppliedByStaff.aspx'>Click Here</a> " + LeaveStatus + " leave requests..</div>";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}