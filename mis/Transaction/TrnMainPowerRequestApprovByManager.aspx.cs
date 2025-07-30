using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Transaction_TrnMainPowerRequestApprovByManager : System.Web.UI.Page
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
                BindGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    private void BindGrid()
    {
        try
        {
            string empId = null;

            if (Session["Designation_ID"].ToString() == "8" || Session["Designation_ID"].ToString() == "13")
            {
                empId = ViewState["Emp_ID"].ToString();
            }
            ds = objdb.ByProcedure("Usp_GetManPowerReqApproveDetail", new string[] { "EmpId" }, new string[] { empId }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Grid.DataSource = ds.Tables[0];
                Grid.DataBind();
            }
            else
            {
                Grid.DataSource = null;
                Grid.DataBind();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox chkbox = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chkbox.NamingContainer;
        bool isChecked = chkbox.Checked;
        UpdateSubmitButtonVisibility();
    }

    private void UpdateSubmitButtonVisibility()
    {
        // Initialize a flag to track if any checkbox is checked
        bool anyChecked = false;

        // Loop through all rows in the GridView
        foreach (GridViewRow row in Grid.Rows)
        {
            // Find the checkbox in the current row
            CheckBox chkbox = (CheckBox)row.FindControl("chkbox");
            if (chkbox != null && chkbox.Checked)
            {
                anyChecked = true; // Set the flag if any checkbox is checked
                break; // No need to check further, we found a checked checkbox
            }
        }
        DivBtn.Visible = anyChecked;
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        UpdateManPowerStatus(2); // 2 = Approved
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        UpdateManPowerStatus(3); // 3 = Rejected
    }

    private void UpdateManPowerStatus(int newStatus)
    {
        foreach (GridViewRow row in Grid.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkbox");
            HiddenField hdnId = (HiddenField)row.FindControl("hdnManPowerReqId");

            if (chk != null && chk.Checked && hdnId != null)
            {
                int manpowerReqId = Convert.ToInt32(hdnId.Value);
                UpdateRequestStatusInDB(manpowerReqId, newStatus);
            }
        }

        // Rebind Grid
        BindGrid();
    }
    private void UpdateRequestStatusInDB(int manpowerReqId, int status)
    {
        string[] paramNames = { "ManPowerReqId", "RequestStatus" };
        string[] paramValues = { manpowerReqId.ToString(), status.ToString() };
        DataSet ds = objdb.ByProcedure("Usp_UpdateManPowerRequestStatus", paramNames, paramValues,
            new string[] { },
            new DataTable[] { },
            "N"
        );
        string ErrMsg = ds.Tables[0].Rows[0]["Message"].ToString();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["Message"].ToString() != "")
            {
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                BindGrid();
                DivBtn.Visible = false;
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
            }
        }
    }







}