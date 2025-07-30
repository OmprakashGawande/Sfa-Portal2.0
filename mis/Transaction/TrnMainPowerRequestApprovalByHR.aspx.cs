using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Transaction_TrnMainPowerRequestApprovalByHR : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                div1.Visible = true;
                DivBtn.Visible = false;
                BindGrid();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }

    private void BindGrid()
    {
        string[] paramNames = { };
        string[] paramValues = { };
        DataSet ds = objdb.ByProcedure("Usp_GetPendingHRManPowerRequests", paramNames, paramValues, new string[] { }, new DataTable[] { }, "N");

        if (ds != null && ds.Tables.Count > 0)
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

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string msg = ds.Tables[0].Rows[0]["Message"].ToString();

            if (!string.IsNullOrEmpty(msg))
            {
                // You can conditionally change alert class based on status
                string icon = status == 2 ? "fa-check" : "fa-times";
                string alertType = status == 2 ? "alert-success" : "alert-danger";
                string title = status == 2 ? "Approved!" : "Rejected!";

                lblMsg.Text = objdb.Alert(icon, alertType, title, msg);
                BindGrid(); // Refresh after update
                DivBtn.Visible = false;
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning!", "No message returned from procedure.");
            }
        }
        else
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Error!", "Procedure did not return any data.");
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
}