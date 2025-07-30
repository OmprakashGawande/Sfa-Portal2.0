using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HRPunishment : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    string Attachment1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                lblMsg.Text = "";
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID", "Emp_ID" }, new string[] { "20", ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlEmployee.DataSource = ds;
                    ddlEmployee.DataTextField = "Emp_Name";
                    ddlEmployee.DataValueField = "Emp_ID";
                    ddlEmployee.DataBind();
                    ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
                }
                FillGrid();

            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlEmployee.SelectedIndex <= 0)
            {
                msg += "Select Employee Name. \\n";
            }
            if (ddlPunishmentSuspension.SelectedIndex <= 0)
            {
                msg += "Please Select Punishment or Suspension. \\n";
            }
            if (ddlPunishmentType.SelectedIndex <= 0)
            {
                msg += "Select Punishment Type. \\n";
            }
            if (ddlPunishmentStatus.SelectedIndex <= 0)
            {
                msg += "Select Punishment Status. \\n";
            }
            if (txtFrom_Date.Text == "")
            {
                msg += "Enter From Date. \\n";
            }
            if (txtEnd_Date.Text == "")
            {
                msg += "Enter End Date. \\n";
            }
            if (txtTitle.Text == "")
            {
                msg += "Enter Title. \\n";
            }

            if (txtDocument.HasFile)
            {
                Attachment1 = Guid.NewGuid() + "-" + txtDocument.FileName;
                txtDocument.PostedFile.SaveAs(Server.MapPath("~/mis/HR/PunishmentUpload/" + Attachment1));
            }
            else
            {
                Attachment1 = "";
            }
            if (txtDescription.Text == "")
            {
                msg += "Enter Description. \\n";
            }
            if (msg.Trim() == "")
            {
                String time = "";
                String Endtime = "";
                if (txtFrom_Date.Text != "")
                {
                    time = Convert.ToDateTime(txtFrom_Date.Text, cult).ToString("yyyy-MM-dd");
                }
                else
                {
                    time = "";
                }
                if (txtEnd_Date.Text != "")
                {
                    Endtime = Convert.ToDateTime(txtEnd_Date.Text, cult).ToString("yyyy-MM-dd");
                }
                else
                {
                    Endtime = "";
                }
                if (btnSave.Text == "Save")
                {
                    objdb.ByProcedure("SpHRPunishment",
                    new string[] { "flag", "Emp_ID", "PunishmentSuspension", "Punishment_Type", "Punishment_Status", "From_Date", "End_Date", "Title", "Document", "Description", "Punishment_UpdatedBy" },
                    new string[] { "0", ddlEmployee.SelectedValue.ToString(), ddlPunishmentSuspension.SelectedItem.Text, ddlPunishmentType.SelectedItem.Text, ddlPunishmentStatus.SelectedItem.Text, time, Endtime, txtTitle.Text, Attachment1, txtDescription.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();

                }
                else if (btnSave.Text == "Edit")
                {

                    objdb.ByProcedure("SpHRPunishment",
                    new string[] { "flag", "Punishment_ID", "Emp_ID", "PunishmentSuspension", "Punishment_Type", "Punishment_Status", "From_Date", "End_Date", "Title", "Document", "Description", "Punishment_UpdatedBy" },
                    new string[] { "1", ViewState["Punishment_ID"].ToString(), ddlEmployee.SelectedValue.ToString(), ddlPunishmentSuspension.SelectedItem.Text, ddlPunishmentType.SelectedItem.Text, ddlPunishmentStatus.SelectedItem.Text, time, Endtime, txtTitle.Text, Attachment1, txtDescription.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    btnSave.Text = "Save";
                    ClearText();
                    FillGrid();
                }
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
    protected void ClearText()
    {
        ddlEmployee.ClearSelection();
        ddlPunishmentSuspension.ClearSelection();
        ddlPunishmentType.ClearSelection();
        ddlPunishmentStatus.ClearSelection();
        txtFrom_Date.Text = "";
        txtEnd_Date.Text = "";
        txtTitle.Text = "";
        txtDocument.Dispose();
        txtDescription.Text = "";
    }
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHRPunishment", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
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

            lblMsg.Text = "";
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;
            ViewState["Punishment_ID"] = GridView1.SelectedDataKey.Value.ToString();

            ds = objdb.ByProcedure("SpHRPunishment",
                         new string[] { "flag", "Punishment_ID" },
                         new string[] { "3", ViewState["Punishment_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.ClearSelection();
                ddlEmployee.Items.FindByValue(ds.Tables[0].Rows[0]["Emp_ID"].ToString()).Selected = true;
                ddlPunishmentSuspension.ClearSelection();
                ddlPunishmentSuspension.Items.FindByValue(ds.Tables[0].Rows[0]["PunishmentSuspension"].ToString()).Selected = true;
                ddlPunishmentType.ClearSelection();
                ddlPunishmentType.Items.FindByValue(ds.Tables[0].Rows[0]["Punishment_Type"].ToString()).Selected = true;
                ddlPunishmentStatus.ClearSelection();
                ddlPunishmentStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Punishment_Status"].ToString()).Selected = true;
                txtFrom_Date.Text = ds.Tables[0].Rows[0]["From_Date"].ToString();
                txtEnd_Date.Text = ds.Tables[0].Rows[0]["End_Date"].ToString();
                txtTitle.Text = ds.Tables[0].Rows[0]["Title"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();

                string UploadedDocument = ds.Tables[0].Rows[0]["Document"].ToString();
                if (UploadedDocument != null && UploadedDocument != "")
                {
                    UploadDocument.NavigateUrl = "~/mis/HR/PunishmentUpload/" + UploadedDocument;
                    UploadDocument.Visible = true;
                }
                else
                {
                    UploadDocument.Visible = false;
                }
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}