using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Master_MstTechnology : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds, ds1;
    CultureInfo cult = new CultureInfo("gu-IN", true);
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["TechnologyId"] = "0";
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
            Grid.DataSource = null;
            Grid.DataBind();
            ds = objdb.ByProcedure("Usp_GetTechnology", new string[] { }, new string[] { }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                Grid.DataSource = ds;
                Grid.DataBind();
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
            txtTechnologyName.Text = "";
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

            if (string.IsNullOrWhiteSpace(txtTechnologyName.Text))
            {
                msg += "Enter Technology Name.\\n";
            }

            if (msg == "")
            {
                string technologyName = txtTechnologyName.Text.Trim();
                string isActive = chkIsActive.Checked ? "1" : "0";
                string empId = ViewState["Emp_ID"].ToString();
                string ip = Request.UserHostAddress;
                string techId = (string)ViewState["TechnologyId"]; // default for insert
                
                string flag = (btnSave.Text == "Save") ? "INSERT" : "UPDATE";

                try
                {
                    objdb.ByProcedure("Usp_InsertOrUpdateMstTechnology",
                        new string[] {
                        "Flag", "TechnologyName", "IsActive",
                        "LastUpdatedBy",  "LastUpdatedByIp", "TechnologyId"
                        },
                        new string[] {
                        flag, technologyName, isActive,
                        empId, ip,
                        (flag == "UPDATE" ? techId : null)
                        },
                        "dataset");

                    if (flag == "INSERT")
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Success!", "Technology inserted successfully.");
                    else
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Updated!", "Technology updated successfully.");

                    btnSave.Text = "Save";
                    ViewState["TechnologyId"] = "0";

                    ClearText();
                    FillGrid();
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Message.Contains("already exists"))
                    {
                        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Duplicate!", "Technology name already exists.");
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "SQL Error!", sqlEx.Message);
                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Error!", ex.Message);
        }
    }
    protected void Grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "EditRecord")
            {
                string techId = e.CommandArgument.ToString();

                // Fetch record by ID (assumes a method to get data by ID)
                DataSet ds = objdb.ByProcedure("Usp_GetTechnologyById",
                    new string[] { "TechnologyId" },
                    new string[] { techId },
                    "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    // Fill form controls
                    txtTechnologyName.Text = row["TechnologyName"].ToString();           
                    chkIsActive.Checked = row["IsActive"].ToString() == "True" ? true : false;

                    ViewState["TechnologyId"] = techId;
                    btnSave.Text = "Update";
                }
            }
            else if (e.CommandName == "ChangeStatus")
            {
                string techId = e.CommandArgument.ToString();

                objdb.ByProcedure("Usp_UpdateTechnologyIsActive",
                    new string[] { "TechnologyId" },
                    new string[] { techId },
                    "dataset");

                FillGrid(); // Refresh grid after status update
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Error!", ex.Message);
        }
    }

}