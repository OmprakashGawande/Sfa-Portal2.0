using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Admin_AdminMeetingMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    lblMsg.Text = "";
                    FillGrid();
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
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
            ds = null;
            ds = objdb.ByProcedure("SpAdminMeetingMaster", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblGridMsg.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                lblGridMsg.Text = "No Record Found";
                GridView1.DataSource = null;
                GridView1.DataBind();
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
            lblGridMsg.Text = "";
            lblMsg.Text = "";
            string msg = "";
            if (txtMeeting_Subject.Text == "")
            {
                msg += "Enter Subject<br/>";
            }
            if (txtMeeting_Venue.Text == "")
            {
                msg += "Enter Venue<br/>";
            }
            if (txtMeeting_OfficerName.Text == "")
            {
                msg += "Enter Officer Name<br/>";
            }
            if (txtMeeting_Date.Text == "")
            {
                msg += "Select Date<br/>";
            }
            if (msg == "")
            {
                string Meeting_IsActive = "1";
                string Meeting_Doc = "";
                string Meeting_Date = "";
                if (txtMeeting_Date.Text != "")
                {
                    Meeting_Date = Convert.ToDateTime(txtMeeting_Date.Text, cult).ToString("yyyy/MM/dd");
                }
                else
                {
                    Meeting_Date = "";
                }
                if (fuMeeting_Doc.HasFile)
                {
                    Meeting_Doc = Guid.NewGuid() + "-" + fuMeeting_Doc.FileName;
                }
                else
                {
                    Meeting_Doc = "";
                }

                string Meeting_StartTime = (txtMeeting_StartTime.Text);

                if (btnSave.Text == "Save")
                {
                    ds = null;

                    ds = objdb.ByProcedure("SpAdminMeetingMaster",
                       new string[] { "flag", "Meeting_IsActive", "Meeting_Subject", "Meeting_Venue", "Meeting_OfficerName", "Meeting_Date", "Meeting_StartTime", "Meeting_Doc", "Meeting_Description", "Meeting_UpdatedBy" },
                       new string[] { "0", Meeting_IsActive, txtMeeting_Subject.Text.Trim(), txtMeeting_Venue.Text.Trim(), txtMeeting_OfficerName.Text.Trim(), Meeting_Date, Meeting_StartTime.ToString(), Meeting_Doc, txtMeeting_Description.Text.Trim(), ViewState["Emp_ID"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        ClearFields();
                        FillGrid();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-danger", "Alert!", "Meeting already scheduled for selected time");
                        
                        txtMeeting_Date.Text = "";
                        txtMeeting_Description.Text = "";
                        txtMeeting_OfficerName.Text = "";
                        txtMeeting_Subject.Text = "";
                        txtMeeting_Venue.Text = "";

                    }
                }

                if (fuMeeting_Doc.HasFile)
                {
                    fuMeeting_Doc.PostedFile.SaveAs(Server.MapPath("~/mis/Admin/Upload/" + Meeting_Doc));
                }
                else
                { }
                //ClearFields();
                //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Alert!", "Data Saved Successfully");
                //FillGrid();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ClearFields()
    {
        try
        {
            lblMsg.Text = "";
            lblGridMsg.Text = "";
            txtMeeting_Date.Text = "";
            txtMeeting_Description.Text = "";
            txtMeeting_OfficerName.Text = "";
            txtMeeting_Subject.Text = "";
            txtMeeting_Venue.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ClearFields();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string Meeting_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            ds = null;
            ds = objdb.ByProcedure("SpAdminMeetingMaster", new string[] { "flag", "Meeting_ID" }, new string[] { "5", Meeting_ID }, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Alert!", "Data Saved Successfully");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}