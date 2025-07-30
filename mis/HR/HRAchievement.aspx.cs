using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HRAchievement : System.Web.UI.Page
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
                ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "20", ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlEmployeeName.DataSource = ds;
                    ddlEmployeeName.DataTextField = "Emp_Name";
                    ddlEmployeeName.DataValueField = "Emp_ID";
                    ddlEmployeeName.DataBind();
                    ddlEmployeeName.Items.Insert(0, new ListItem("Select", "0"));
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
            if (ddlEmployeeName.SelectedIndex <= 0)
            {
                msg += "Select Employee Name. \\n";
            }
            if (ddlAchievementType.SelectedIndex <= 0)
            {
                msg += "Select Punishment/Suspension. \\n";
            }
            if (txtTopic.Text == "")
            {
                msg += "Enter Topic. \\n";
            }
            if (txtOrganization.Text == "")
            {
                msg += "Enter Organization. \\n";
            }
            if (txtDate.Text == "")
            {
                msg += "Enter Title. \\n";
            }

            if (txtDocument.HasFile)
            {
                Attachment1 = Guid.NewGuid() + "-" + txtDocument.FileName;
                txtDocument.PostedFile.SaveAs(Server.MapPath("~/mis/HR/AchievementUpload/" + Attachment1));
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
                if (txtDate.Text != "")
                {
                    time = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy-MM-dd");
                }
                else
                {
                    time = "";
                }
                if (btnSave.Text == "Save")
                {
                    objdb.ByProcedure("SpHRAchievement",
                    new string[] { "flag", "Emp_ID", "AchievementType", "Achievement_Topic", "Achievement_Organization", "Achievement_Date", "Achievement_Document", "Achievement_Description", "Achievement_UpdatedBy" },
                    new string[] { "0", ddlEmployeeName.SelectedValue.ToString(), ddlAchievementType.SelectedItem.Text, txtTopic.Text, txtOrganization.Text, time, Attachment1, txtDescription.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillGrid();
                    ClearText();
                }
                else if (btnSave.Text == "Edit")
                {
                    objdb.ByProcedure("SpHRAchievement",
                    new string[] { "flag", "AchievementID", "Emp_ID", "AchievementType", "Achievement_Topic", "Achievement_Organization", "Achievement_Date", "Achievement_Document", "Achievement_Description", "Achievement_UpdatedBy" },
                    new string[] { "2", ViewState["AchievementID"].ToString(), ddlEmployeeName.SelectedValue.ToString(), ddlAchievementType.SelectedItem.Text, txtTopic.Text, txtOrganization.Text, time, Attachment1, txtDescription.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    btnSave.Text = "Save";
                    FillGrid();
                    ClearText();
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
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            ds = objdb.ByProcedure("SpHRAchievement",
                new string[] { "flag" },
                new string[] { "1" }, "dataset");
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
        lblMsg.Text = "";
        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        GridView1.UseAccessibleHeader = true;
        ViewState["AchievementID"] = GridView1.SelectedDataKey.Value.ToString();

        ds = objdb.ByProcedure("SpHRAchievement",
            new string[] { "flag", "AchievementID" },
            new string[] { "3", ViewState["AchievementID"].ToString() }, "dataset");

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlEmployeeName.ClearSelection();
            ddlEmployeeName.Items.FindByValue(ds.Tables[0].Rows[0]["Emp_ID"].ToString()).Selected = true;
            ddlAchievementType.ClearSelection();
            ddlAchievementType.Items.FindByValue(ds.Tables[0].Rows[0]["AchievementType"].ToString()).Selected = true;
            txtTopic.Text = ds.Tables[0].Rows[0]["Achievement_Topic"].ToString();
            txtOrganization.Text = ds.Tables[0].Rows[0]["Achievement_Organization"].ToString();
            txtDate.Text = ds.Tables[0].Rows[0]["Achievement_Date"].ToString();
            txtDescription.Text = ds.Tables[0].Rows[0]["Achievement_Description"].ToString();
          //  txtDocument. = ds.Tables[0].Rows[0]["Achievement_Description"].ToString();
            btnSave.Text = "Edit";
        }
    }
    protected void ClearText()
    {
        ddlEmployeeName.ClearSelection();
        ddlAchievementType.ClearSelection();
        txtTopic.Text = "";
        txtOrganization.Text = "";
        txtDate.Text = "";
        txtDocument.Dispose();
        txtDescription.Text = "";
    }
}