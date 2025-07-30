using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Master_MstTypeOfProject : System.Web.UI.Page
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
                    ViewState["TypeOfProjectId"] = "0";
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
            ds = objdb.ByProcedure("Usp_GetTypeOfProject", new string[] { }, new string[] { }, "dataset");
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
            txtTypeOfProject.Text = "";
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

            if (string.IsNullOrWhiteSpace(txtTypeOfProject.Text))
            {
                msg += "Enter Type of Project .\\n";
            }

            if (msg == "")
            {
                string TypeOfProject = txtTypeOfProject.Text.Trim();
                string isActive = chkIsActive.Checked ? "1" : "0";
                string empId = ViewState["Emp_ID"].ToString();
                string ip = Request.UserHostAddress;
                string Id = (string)ViewState["TypeOfProjectId"]; // default for insert

                string flag = (btnSave.Text == "Save") ? "INSERT" : "UPDATE";

                try
                {
                    objdb.ByProcedure("Usp_InsertOrUpdateTypeOfProject",
                        new string[] {
                        "Flag", "TypeOfProjectName", "IsActive",
                        "LastUpdatedBy",  "LastUpdatedByIp", "TypeOfProjectId"
                        },
                        new string[] {
                        flag, TypeOfProject, isActive,
                        empId, ip,
                        (flag == "UPDATE" ? Id : null)
                        },
                        "dataset");

                    if (flag == "INSERT")
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Success!", "Type Of project inserted successfully.");
                    else
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Updated!", "Type Of project updated successfully.");

                    btnSave.Text = "Save";
                    ViewState["TypeOfProjectId"] = "0";

                    ClearText();
                    FillGrid();
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Message.Contains("already exists"))
                    {
                        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Duplicate!", "Type Of project already exists.");
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
                string Id = e.CommandArgument.ToString();

                // Fetch record by ID (assumes a method to get data by ID)
                DataSet ds = objdb.ByProcedure("Usp_GetTypeOfProjectById",
                    new string[] { "TypeOfProjectId" },
                    new string[] { Id },
                    "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    // Fill form controls
                    txtTypeOfProject.Text = row["TypeOfProjectName"].ToString();
                    chkIsActive.Checked = row["IsActive"].ToString() == "True" ? true : false;

                    ViewState["TypeOfProjectId"] = Id;
                    btnSave.Text = "Update";
                }
            }
            else if (e.CommandName == "ChangeStatus")
            {
                string techId = e.CommandArgument.ToString();

                objdb.ByProcedure("Usp_UpdateTypeOfProjectIsActive",
                    new string[] { "TypeOfProjectId" },
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

    public void BindTypeOfProjectDropDown(DropDownList ddl)
    {
        //< asp:DropDownList ID = "ddlTypeOfProject" runat = "server" CssClass = "form-control" ></ asp:DropDownList >

        try
        {
            DataSet ds = objdb.ByProcedure("Usp_GetTypeOfProject", new string[] { }, new string[] { }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddl.DataSource = ds.Tables[0];
                ddl.DataTextField = "TypeOfProjectName";
                ddl.DataValueField = "TypeOfProjectId";
                ddl.DataBind();
            }

            ddl.Items.Insert(0, new ListItem("-- Select Type of Project --", "0"));
        }
        catch (Exception ex)
        {
            // Optional: log or show error
            throw new Exception("Error while binding Type of Project dropdown: " + ex.Message);
        }
    }

}
