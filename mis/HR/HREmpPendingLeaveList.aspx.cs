using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_HR_HREmpPendingLeaveList : System.Web.UI.Page
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
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "Office_ID" },
                  new string[] { "3", ViewState["Office_ID"].ToString() }, "datatset");
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["LeaveId"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId" },
                  new string[] { "4", ViewState["LeaveId"].ToString() }, "datatset");
            if (ds.Tables[0].Rows.Count != 0)
            {
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
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds1;
            string DocPath = "";
            string OrderDate = "";
            lblMsg.Text = "";
            string msg = "";
            string TakenLeave = "";
            if (ddlStatus.SelectedIndex == 0)
            {
                msg += "Select Status. <br/>";
            }
            if (txtRemark.Text == "")
            {
                msg += "Enter Remark. <br/>";
            }
            if (txtOrderDate.Text != "")
            {
                OrderDate = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy/MM/dd");
            }
            else
            {
                OrderDate = "";
            }
            ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveType_ID" },
            new string[] { "7", ViewState["LeaveType"].ToString() }, "datatset");
            string financialYear = ds.Tables[0].Rows[0]["Financial_Year"].ToString();
            string LeaveDay = ds.Tables[0].Rows[0]["Leave_Days"].ToString();

            if (ddlStatus.SelectedItem.Text == "Approve")
            {
                ds1 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId" },
            new string[] { "8", ViewState["LeaveId"].ToString() }, "datatset");
                TakenLeave = ds1.Tables[0].Rows[0]["TakenLeave"].ToString();
            }
            else
            {
                TakenLeave = "0";
            }
            objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "Emp_ID", "LeaveType", "TotalAllowedLeave", "TotalTakenLeave", "FinancialYear" },
            new string[] { "2", ViewState["EmpID"].ToString(), ViewState["LeaveType"].ToString(), LeaveDay, TakenLeave, financialYear }, "datatset");
            if (ApprovalDoc.HasFile)
            {
                DocPath = "../HR/UploadDoc/LeaveApproveDoc/" + Guid.NewGuid() + "-" + ApprovalDoc.FileName;
                ApprovalDoc.PostedFile.SaveAs(Server.MapPath(DocPath));
            }
            objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId", "LeaveStatus", "RemarkByApprovalAuth", "LeaveApprovalOrderNo", "LeaveApprovalOrderDate", "LeaveApprovalOrderFile" },
            new string[] { "9", ViewState["LeaveId"].ToString(), ddlStatus.SelectedItem.Text, txtRemark.Text, txtOrderNo.Text, OrderDate, DocPath }, "datatset");
            FillGrid();
            lblMsg2.Text = "<div class='alert alert-success alert-dismissible'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button><h4><i class='icon fa fa-check'></i> Success</h4>Operation Successfully Completed.</div>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}
