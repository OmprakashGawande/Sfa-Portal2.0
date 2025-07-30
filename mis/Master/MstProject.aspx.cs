using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Master_MstProject : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                                             
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["UserTypeId"] = Session["UserTypeId"].ToString();
                BindGrid();
                BindDropdown();

                fillWorkCategory();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    // Bind Project Detail Grid
    private void BindGrid()
    {

        string empId = ViewState["Emp_ID"].ToString();
        //if (Session["Designation_ID"].ToString() == "1")
        //{
        //    empId = "0";
        //}
        //if (Session["Designation_ID"].ToString() == "8")
        //{
        //    empId = ViewState["Emp_ID"].ToString();
        //}
        // Call your helper method
        DataSet ds = objdb.ByProcedure(
            "Usp_GetProjectDetailsByEmpId",                   // Procedure Name
            new string[] { "EmpId" },                        // Parameter Names
            new string[] { empId },                           // Parameter Values
            "dataset"                                         // Return Type
        );

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            Grid.DataSource = ds.Tables[0];
            Grid.DataBind();
            Datatable();

        }
        else
        {
            Grid.DataSource = null;
            Grid.DataBind();
        }
    }



    protected void Datatable()
    {

        Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
        Grid.UseAccessibleHeader = true;
    }
    // Bind Man Power detail Projest Wise
    private void BindGridManPowerDetailProjectWise(string ProjectID)
    {



        // Call your helper method
        DataSet ds = objdb.ByProcedure(
            "Usp_GetManPowerDetail",                   // Procedure Name
            new string[] { "ProjectId " },                        // Parameter Names
            new string[] { ProjectID },                           // Parameter Values
            "dataset"                                         // Return Type
        );
        Datatable();

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            GridManPowerDetail.DataSource = ds.Tables[0];
            GridManPowerDetail.DataBind();
        }
        else
        {
            GridManPowerDetail.DataSource = null;
            GridManPowerDetail.DataBind();
        }
    }
    public void BindDropdown()
    {
        try
        {
            string empId = "0";

            if (Session["Designation_ID"].ToString() == "8")  //Conditon for manager
            {
                empId = ViewState["Emp_ID"].ToString();
            }
            string designationid = "8,13";
            DataSet ds3 = objdb.ByProcedure("Usp_GetddlEmployee", new string[] { "EmpId", "DesignationId" }, new string[] { empId, designationid }, "dataset");

            DataSet ds4 = objdb.ByProcedure("Usp_GetddlEmployee", new string[] { "EmpId", "DesignationId" }, new string[] { "0", "0" }, "dataset");

            DataSet ds2 = objdb.ByProcedure("Usp_GetddlTypeofProject", new string[] { }, new string[] { }, "dataset");
            DataSet ds = objdb.ByProcedure("Usp_GetddlTechnology", new string[] { }, new string[] { }, "dataset");
            DataSet dsDesig = objdb.ByProcedure("Usp_GetDesignation", new string[] { }, new string[] { }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlTechnology.DataSource = ds.Tables[0];
                ddlTechnology.DataTextField = "TechnologyName";
                ddlTechnology.DataValueField = "TechnologyId";
                ddlTechnology.DataBind();
            }
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                ddlTypeofProject.DataSource = ds2.Tables[0];
                ddlTypeofProject.DataTextField = "TypeOfProjectName";
                ddlTypeofProject.DataValueField = "TypeOfProjectId";
                ddlTypeofProject.DataBind();
            }
            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                ddlOwner.DataSource = ds3.Tables[0];
                ddlOwner.DataTextField = "Emp_Name";
                ddlOwner.DataValueField = "Emp_ID";
                ddlOwner.DataBind();


                ddlEmployee.DataSource = ds4.Tables[0];
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_ID";
                ddlEmployee.DataBind();
            }
            if (dsDesig != null && dsDesig.Tables[0].Rows.Count > 0)
            {
                ddlRole.DataSource = dsDesig.Tables[0];
                ddlRole.DataTextField = "Designation_Name";
                ddlRole.DataValueField = "Designation_ID";
                ddlRole.DataBind();


            }
            //ddlTechnology.Items.Insert(0, new ListItem("Select", "0"));
            ddlTypeofProject.Items.Insert(0, new ListItem("Select", "0"));




            ddlOwner.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            ddlRole.Items.Insert(0, new ListItem("Select", "0"));


        }
        catch (Exception ex)
        {
            // Optional: log or show error
            throw new Exception("Error while binding Type of Project dropdown: " + ex.Message);
        }
    }
    // save Project Detail
    protected void btnSave1_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                string TechnologyId = "";
                foreach (ListItem item in ddlTechnology.Items)
                {
                    if (item.Selected)
                    {
                        TechnologyId += item.Value + ",";
                    }
                }

                string WorkOrderDate = txtWorkOrderDate.Text != "" ? Convert.ToDateTime(txtWorkOrderDate.Text, cult).ToString("yyyy/MM/dd") : "";
                string ComplitionDate = txtComplitionDate.Text != "" ? Convert.ToDateTime(txtComplitionDate.Text, cult).ToString("yyyy/MM/dd") : "";
                if (btnSave1.Text == "Save")
                {
                    ds = objdb.ByProcedure("Usp_InsertOrUpdateProjectMaster", new string[] { "ProjectName", "TechnologyId", "TypeOfProjectId", "OwnerId", "WorkOrderDate", "ComplitionDate", "Discription", "UserTypeId", "OfficeId", "CreatedBy", "CreatedByIP" }, new string[] {
                   txtProjectName.Text.Trim(),TechnologyId,ddlTypeofProject.SelectedValue,ddlOwner.SelectedValue,WorkOrderDate,ComplitionDate,txtDiscription.Value, Session["UserTypeId"].ToString(),Session["Office_ID"].ToString(),ViewState["Emp_ID"].ToString(),objdb.GetLocalIPAddress() }, "dataset");
                }
                else if (btnSave1.Text == "Update" && ViewState["ProjectId"] != "" && ViewState["ProjectId"] != null)
                {
                    ds = objdb.ByProcedure("Usp_InsertOrUpdateProjectMaster", new string[] { "ProjectName", "TechnologyId", "TypeOfProjectId", "OwnerId", "WorkOrderDate", "ComplitionDate", "Discription", "UserTypeId", "OfficeId", "LastUpdatedBy", "LastUpdatedByIp", "ProjectId" }, new string[] {
                   txtProjectName.Text.Trim(),TechnologyId,ddlTypeofProject.SelectedValue,ddlOwner.SelectedValue,WorkOrderDate,ComplitionDate,txtDiscription.Value, Session["UserTypeId"].ToString(),Session["Office_ID"].ToString(),ViewState["Emp_ID"].ToString(),objdb.GetLocalIPAddress(),ViewState["ProjectId"].ToString() }, "dataset");
                }
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert alert-light-success", "Thanks!", ErrMsg);

                    BindGrid();
                    btnSave1.Text = "Save";

                    txtProjectName.Text = "";
                    ddlTechnology.ClearSelection();
                    ddlTypeofProject.SelectedValue = "0";

                    ddlOwner.SelectedValue = "0";


                    txtWorkOrderDate.Text = "";
                    txtComplitionDate.Text = "";
                    txtDiscription.Value = "";

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-light-warning", "Warning !", ErrMsg);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error : " + ex.Message);

        }
    }
    // Project detail row Command
    protected void Grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            Datatable();
            if (e.CommandName == "EditRecord")
            {

                ViewState["ProjectId"] = "";
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblProjectName = (Label)row.FindControl("lblProjectName");
                Label lblTypeOfProjectId = (Label)row.FindControl("lblTypeOfProjectId");
                Label lblTechnologyId = (Label)row.FindControl("lblTechnologyId");
                Label lblOwnerId = (Label)row.FindControl("lblOwnerId");
                Label lblWorkOrderDate = (Label)row.FindControl("lblWorkOrderDate");
                Label lblComplitionDate = (Label)row.FindControl("lblComplitionDate");
                Label lblDiscription = (Label)row.FindControl("lblDiscription");
                btnSave1.Text = "Update";
                ViewState["ProjectId"] = e.CommandArgument;
                txtWorkOrderDate.Text = !string.IsNullOrEmpty(lblWorkOrderDate.Text) ? lblWorkOrderDate.Text : null;
                txtComplitionDate.Text = !string.IsNullOrEmpty(lblComplitionDate.Text) ? lblComplitionDate.Text : null;
                txtProjectName.Text = !string.IsNullOrEmpty(lblProjectName.Text) ? lblProjectName.Text : null;
                txtDiscription.Value = !string.IsNullOrEmpty(lblDiscription.Text) ? lblDiscription.Text : null;
                if (!string.IsNullOrEmpty(lblProjectName.Text))
                {
                    ddlTypeofProject.ClearSelection();
                    ddlTypeofProject.Items.FindByValue(lblTypeOfProjectId.Text).Selected = true;
                }
                //if (!string.IsNullOrEmpty(lblTechnologyId.Text))
                //{
                //    ddlTechnology.ClearSelection();
                //    ddlTechnology.Items.FindByValue(lblTechnologyId.Text).Selected = true;
                //}
                if (!string.IsNullOrEmpty(lblOwnerId.Text))
                {
                    ddlOwner.ClearSelection();

                    var item = ddlOwner.Items.FindByValue(lblOwnerId.Text);
                    if (item != null)
                    {
                        item.Selected = true;
                    }

                }
                string[] selectedTaskIds = lblTechnologyId.Text.Split(',');
                ddlTechnology.ClearSelection();
                foreach (ListItem item in ddlTechnology.Items)
                {
                    if (selectedTaskIds.Contains(item.Value))
                    {
                        item.Selected = true;
                    }
                }

            }
            if (e.CommandName == "AddManpower")
            {
                ViewState["SelectedProjectId"] = "";
                lblMsgManPower.Text = "";
                string projectId = e.CommandArgument.ToString();

                // 🔸 Store in ViewState
                ViewState["SelectedProjectId"] = projectId;
                GetTeamLead(projectId);
                BindGridManPowerDetailProjectWise(projectId);

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal').modal('show');", true);

                ddlWorkCategoryId.Attributes.Add("multiple", "multiple");
                ddlWorkCategoryId.Attributes.Add("multiselect-search", "true");
                ddlWorkCategoryId.Attributes.Add("multiselect-select-all", "true");
                ddlWorkCategoryId.Attributes.Add("multiselect-max-items", "3");
                ddlWorkCategoryId.ClearSelection();

                string script = @"
    var myModal = new bootstrap.Modal(document.getElementById('exampleModal'));
    myModal.show();
    setTimeout(function() {
        $('.select2').select2({ dropdownParent: $('#exampleModal')});
        $('.multiselect-dropdown').attr('style', 'width:250px !important;');
    }, 200);
";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script, true);








            }

            if (e.CommandName == "AddTask")
            {
                ViewState["SelectedProjectId"] = "";
                lblMsgTask.Text = "";
                string projectId = e.CommandArgument.ToString();
                ViewState["SelectedProjectId"] = projectId;
                BindGridTaskDetail(projectId);
                ddlParentTask.Items.Clear();

                GetParentTask(projectId);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal2').modal('show');", true);
            }

            if (e.CommandName == "ChangeStatus")
            {
                string projectId = e.CommandArgument.ToString();

                string lastUpdateddBy = ViewState["Emp_ID"].ToString();
                string lastUpdateddByIp = objdb.GetLocalIPAddress();
                string officeId = Session["Office_ID"].ToString();
                string userTypeId = Session["UserTypeId"].ToString();

                objdb.ByProcedure("Usp_UpdateProjectIsActive",
                    new string[] { "ProjectId ", "LastUpdatedBy", "LastUpdatedByIp" },
                    new string[] { projectId, lastUpdateddBy, lastUpdateddByIp },
                    "dataset");
                BindGrid();

            }

        }
        catch (Exception ex)
        {
            throw new Exception("Error : " + ex.Message);
        }
    }
    // Add multiple Project 
    public class ManpowerEntry
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string AllocationDate { get; set; }
    }
    // Add multiple Man Power Data table
    private DataTable ManpowerTable
    {
        get
        {
            if (ViewState["ManpowerTable"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("EmployeeId", typeof(int));
                dt.Columns.Add("EmployeeName", typeof(string));
                dt.Columns.Add("RoleId", typeof(int));
                dt.Columns.Add("RoleName", typeof(string));
                dt.Columns.Add("AllocationDate", typeof(string));
                dt.Columns.Add("MultiCategoreyId", typeof(string));
                dt.Columns.Add("WorkCategoreyName", typeof(string));
                dt.Columns.Add("TeamLead", typeof(string));
                dt.Columns.Add("TeamLeadId", typeof(string));
                ViewState["ManpowerTable"] = dt;
            }
            return (DataTable)ViewState["ManpowerTable"];
        }
        set
        {
            ViewState["ManpowerTable"] = value;
        }
    }
    // Add man power Multi add
    protected void btnAddManPower_Click(object sender, EventArgs e)
    {
        lblMsgManPower.Text = "";
        DataTable dt = ManpowerTable;

        string employeeName = ddlEmployee.SelectedItem.Text;
        string roleName = ddlRole.SelectedItem.Text;
        string teamLead = ddlTeamLead.SelectedItem.Text;

        Datatable();
        string categoreyName = "";

        foreach (ListItem item in ddlWorkCategoryId.Items)
        {
            if (item.Selected)
            {
                categoreyName += item.Text + ",";
            }
        }

        // Optional: remove the last comma
        if (categoreyName.EndsWith(","))
        {
            categoreyName = categoreyName.TrimEnd(',');
        }



        string MultiCategoreyId = "";
        foreach (ListItem item in ddlWorkCategoryId.Items)
        {
            if (item.Selected)
            {
                MultiCategoreyId += item.Value + ",";
            }
        }

        dt.Rows.Add(
            Convert.ToInt32(ddlEmployee.SelectedValue),
            employeeName,
            Convert.ToInt32(ddlRole.SelectedValue),
            roleName,
            txtAllocationDate.Text,
               //Convert.ToInt32(ddlWorkCategoryId.SelectedValue),
               MultiCategoreyId,
            categoreyName,
            teamLead,
              Convert.ToInt32(ddlTeamLead.SelectedValue)

        );

        ManpowerTable = dt;
        ddlEmployee.SelectedValue = "0";
        ddlRole.ClearSelection();
        txtAllocationDate.Text = "";
        ddlTeamLead.ClearSelection();


        BindManpowerGrid();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal').modal('show');", true);
    }
    // Bind man Power Temp data
    private void BindManpowerGrid()
    {
        grdManpower.DataSource = ManpowerTable;
        grdManpower.DataBind();
    }
    // tem man power data grid command
    protected void grdManpower_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsgManPower.Text = "";
        Datatable();
        if (e.CommandName == "DeleteRow")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = ManpowerTable;
            dt.Rows.RemoveAt(index);
            ManpowerTable = dt;
            BindManpowerGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal').modal('show');", true);
        }
    }
    // insert man power detail 
    protected void btnSaveManPower_Click(object sender, EventArgs e)
    {
        lblMsgManPower.Text = "";
        DataTable dt = ManpowerTable;
        Datatable();
        if (dt != null && dt.Rows.Count > 0)
        {
            // Prepare TVP DataTable structure
            DataTable tvp = new DataTable();
            tvp.Columns.Add("EmployeeId", typeof(int));
            tvp.Columns.Add("RoleId", typeof(int));
            tvp.Columns.Add("AllocationDate", typeof(DateTime));
            tvp.Columns.Add("ProjectId", typeof(int));
            tvp.Columns.Add("MultiCategoreyId", typeof(string));
            tvp.Columns.Add("TeamLeadId", typeof(string));

            int projectId = Convert.ToInt32(ViewState["SelectedProjectId"]);

            foreach (DataRow row in dt.Rows)
            {
                tvp.Rows.Add(
                    row["EmployeeId"],
                    row["RoleId"],
                    Convert.ToDateTime(row["AllocationDate"]),
                    projectId,
                    row["MultiCategoreyId"],
                    row["TeamLeadId"]
                );
            }

            // Add audit info
            string createdBy = ViewState["Emp_ID"].ToString();
            string createdByIP = objdb.GetLocalIPAddress();
            string officeId = Session["Office_ID"].ToString();
            string userTypeId = Session["UserTypeId"].ToString();

            // Call stored procedure using your custom DB method
            DataSet ds = objdb.ByProcedure(
                "Usp_InsertManpower",
                new string[] { "CreatedBy", "CreatedByIP", "CreatedOn", "UserTypeId", "OfficeId" },
                new string[] { createdBy, createdByIP, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), userTypeId, officeId },
                new string[] { "ManpowerTable" },
                new DataTable[] { tvp }, "dataset");

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string msg = ds.Tables[0].Rows[0]["Msg"].ToString();
                string errMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();

                if (msg == "OK")
                {
                    lblMsgManPower.Text = objdb.Alert("fa-check", "alert-success", "Success!", errMsg);
                    ViewState["ManpowerTable"] = null;


                    BindManpowerGrid();


                    BindGridManPowerDetailProjectWise(ViewState["SelectedProjectId"].ToString());
                    ddlEmployee.SelectedValue = "0";
                    ddlRole.ClearSelection(); ;
                    txtAllocationDate.Text = "";


                    ddlWorkCategoryId.Attributes.Add("multiple", "multiple");
                    ddlWorkCategoryId.Attributes.Add("multiselect-search", "true");
                    ddlWorkCategoryId.Attributes.Add("multiselect-select-all", "true");
                    ddlWorkCategoryId.Attributes.Add("multiselect-max-items", "3");
                    ddlWorkCategoryId.ClearSelection();


                    string script = @"var myModal = new bootstrap.Modal(document.getElementById('exampleModal')); myModal.show(); setTimeout(function() { $('.select2').select2({ dropdownParent: $('#exampleModal')}); $('.multiselect-dropdown').attr('style', 'width:250px !important;');}, 200);";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script, true);
                }
                else if (msg == "ERROR")
                {

                    lblMsgManPower.Text = objdb.Alert("fa-check", "alert-warning", "Warning!", errMsg);

                    ddlWorkCategoryId.Attributes.Add("multiple", "multiple");
                    ddlWorkCategoryId.Attributes.Add("multiselect-search", "true");
                    ddlWorkCategoryId.Attributes.Add("multiselect-select-all", "true");
                    ddlWorkCategoryId.Attributes.Add("multiselect-max-items", "3");
                    ddlWorkCategoryId.ClearSelection();


                    string script = @"var myModal = new bootstrap.Modal(document.getElementById('exampleModal')); myModal.show(); setTimeout(function() { $('.select2').select2({ dropdownParent: $('#exampleModal')}); $('.multiselect-dropdown').attr('style', 'width:250px !important;');}, 200);";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script, true);
                }
                else if (msg == "DUPLICATE")
                {
                    lblMsgManPower.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning!", errMsg);

                    ddlWorkCategoryId.Attributes.Add("multiple", "multiple");
                    ddlWorkCategoryId.Attributes.Add("multiselect-search", "true");
                    ddlWorkCategoryId.Attributes.Add("multiselect-select-all", "true");
                    ddlWorkCategoryId.Attributes.Add("multiselect-max-items", "3");
                    ddlWorkCategoryId.ClearSelection();


                    string script = @"var myModal = new bootstrap.Modal(document.getElementById('exampleModal')); myModal.show(); setTimeout(function() { $('.select2').select2({ dropdownParent: $('#exampleModal')}); $('.multiselect-dropdown').attr('style', 'width:250px !important;');}, 200);";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script, true);
                }
                else
                {
                    lblMsgManPower.Text = objdb.Alert("fa-info-circle", "alert-info", "Info", errMsg);

                    ddlWorkCategoryId.Attributes.Add("multiple", "multiple");
                    ddlWorkCategoryId.Attributes.Add("multiselect-search", "true");
                    ddlWorkCategoryId.Attributes.Add("multiselect-select-all", "true");
                    ddlWorkCategoryId.Attributes.Add("multiselect-max-items", "3");
                    ddlWorkCategoryId.ClearSelection();


                    string script = @"var myModal = new bootstrap.Modal(document.getElementById('exampleModal')); myModal.show(); setTimeout(function() { $('.select2').select2({ dropdownParent: $('#exampleModal')}); $('.multiselect-dropdown').attr('style', 'width:250px !important;');}, 200);";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script, true);
                }
            }
            else
            {
                lblMsgManPower.Text = objdb.Alert("fa-info-circle", "alert-info", "Info", "No response from database.");

                ddlWorkCategoryId.Attributes.Add("multiple", "multiple");
                ddlWorkCategoryId.Attributes.Add("multiselect-search", "true");
                ddlWorkCategoryId.Attributes.Add("multiselect-select-all", "true");
                ddlWorkCategoryId.Attributes.Add("multiselect-max-items", "3");
                ddlWorkCategoryId.ClearSelection();

                string script = @"var myModal = new bootstrap.Modal(document.getElementById('exampleModal')); myModal.show(); setTimeout(function() { $('.select2').select2({ dropdownParent: $('#exampleModal')}); $('.multiselect-dropdown').attr('style', 'width:250px !important;');}, 200);";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script, true);
            }
        }
        else
        {
            lblMsgManPower.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning!", "No manpower data to save.");
            string script = @"var myModal = new bootstrap.Modal(document.getElementById('exampleModal')); myModal.show(); setTimeout(function() { $('.select2').select2({ dropdownParent: $('#exampleModal')}); $('.multiselect-dropdown').attr('style', 'width:250px !important;');}, 200);";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script, true);
        }
    }
    // Man Power Detail Grid Commnd
    protected void GridManPowerDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Datatable();

        if (e.CommandName == "ChangeStatus")
        {
            string manpowerId = e.CommandArgument.ToString();

            string lastUpdateddBy = ViewState["Emp_ID"].ToString();
            string lastUpdateddByIp = objdb.GetLocalIPAddress();
            string officeId = Session["Office_ID"].ToString();
            string userTypeId = Session["UserTypeId"].ToString();

            objdb.ByProcedure("Usp_UpdateManPowerIsActive",
                new string[] { "ManPowerId ", "LastUpdatedBy", "LastUpdatedByIp" },
                new string[] { manpowerId, lastUpdateddBy, lastUpdateddByIp },
                "dataset");
            BindGridManPowerDetailProjectWise(ViewState["SelectedProjectId"].ToString());
            string script = @"var myModal = new bootstrap.Modal(document.getElementById('exampleModal')); myModal.show(); setTimeout(function() { $('.select2').select2({ dropdownParent: $('#exampleModal')}); $('.multiselect-dropdown').attr('style', 'width:250px !important;');}, 200);";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script, true);
        }
    }
    // Bind Parent task Dropdown
    public void GetParentTask(string projectId)
    {
        try
        {
            DataSet ds = objdb.ByProcedure("Usp_GetParentTask", new string[] { "ProjectId" }, new string[] { projectId }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlParentTask.DataSource = ds.Tables[0];
                ddlParentTask.DataTextField = "TaskName";
                ddlParentTask.DataValueField = "TaskId";
                ddlParentTask.DataBind();
            }
            ddlParentTask.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            throw new Exception("Error while binding Type of Project dropdown: " + ex.Message);
        }
    }
    public void GetTeamLead(string projectId)
    {
        try
        {
            DataSet ds = objdb.ByProcedure("Usp_GetProjectWiseTeamLead", new string[] { "ProjectId" }, new string[] { projectId }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlTeamLead.DataSource = ds.Tables[0];
                ddlTeamLead.DataTextField = "Emp_Name";
                ddlTeamLead.DataValueField = "Emp_ID";
                ddlTeamLead.DataBind();
            }
            ddlTeamLead.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            throw new Exception("Error while binding Type of Project dropdown: " + ex.Message);
        }
    }
    // Bind Categoery Dropdown
    protected void fillWorkCategory()
    {
        try
        {
            ddlWorkCategoryId.Items.Clear();
            DataSet ds1 = objdb.ByProcedure("USP_Daily_Task_GetAllWorkCategory", new string[] { }, new string[] { }, "dataset");
            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlWorkCategoryId.DataValueField = "WorkCategoryId";
                        ddlWorkCategoryId.DataTextField = "WorkCategoryEng";
                        ddlWorkCategoryId.DataSource = ds1;
                        ddlWorkCategoryId.DataBind();

                    }
                    else
                    {
                        ddlWorkCategoryId.Items.Insert(0, new ListItem("No Record Found", "0"));
                    }
                }
                else
                {
                    ddlWorkCategoryId.Items.Insert(0, new ListItem("No Record Found", "0"));
                }
            }
            else
            {
                ddlWorkCategoryId.Items.Insert(0, new ListItem("No Record Found", "0"));
            }
            if (ds1 != null) ds1.Dispose();

        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7: " + ex.Message.ToString());
        }
    }
    // Btn Save task 
    protected void btnSaveTask_Click(object sender, EventArgs e)
    {
        try
        {
            Datatable();
            if (Page.IsValid)
            {
                if (ViewState["SelectedProjectId"] != "" && ViewState["SelectedProjectId"] != null)
                {
                    DataSet ds = objdb.ByProcedure("Usp_InsertTaskDetail",
                      new string[] { "ProjectId", "ParentTaskId", "TaskName", "TaskDescription", "UserTypeId", "OfficeId", "CreatedBy", "CreatedByIp" },
                      new string[] {
                    ViewState["SelectedProjectId"].ToString(),
                    string.IsNullOrEmpty(ddlParentTask.SelectedValue) ? null : ddlParentTask.SelectedValue,
                    txtTaskName.Text.Trim(),
                    txtTaskDescription.Value,
                    Session["UserTypeId"].ToString(),
                    Session["Office_ID"].ToString(),
                    ViewState["Emp_ID"].ToString(),
                    objdb.GetLocalIPAddress()
                      },
                      "dataset"
                  );
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string msg = ds.Tables[0].Rows[0]["Msg"].ToString();
                        string errMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();

                        if (msg == "OK")
                        {
                            lblMsgTask.Text = objdb.Alert("fa-check", "alert-success", "Success!", errMsg);
                            // Reset form fields
                            GetParentTask(ViewState["SelectedProjectId"].ToString());
                            GetTeamLead(ViewState["SelectedProjectId"].ToString());
                            txtTaskName.Text = "";

                            txtTaskDescription.Value = "";
                            ddlParentTask.SelectedValue = "0";
                            BindGridTaskDetail(ViewState["SelectedProjectId"].ToString());

                            // Show modal again if needed
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal2').modal('show');", true);
                        }
                        else if (msg == "DUPLICATE")
                        {
                            lblMsgTask.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning!", errMsg);
                        }
                        else if (msg == "ERROR")
                        {
                            lblMsgTask.Text = objdb.Alert("fa-ban", "alert-danger", "Error!", errMsg);
                        }
                        else
                        {
                            lblMsgTask.Text = objdb.Alert("fa-info-circle", "alert-info", "Info", errMsg);
                        }
                    }
                    else
                    {
                        lblMsgTask.Text = objdb.Alert("fa-info-circle", "alert-info", "Info", "No response from database.");
                    }
                }
                else
                {
                    lblMsgTask.Text = objdb.Alert("fa-info-circle", "alert-info", "Info", "Please try again after some time.");
                }



            }

        }
        catch (Exception ex)
        {
            lblMsgTask.Text = objdb.Alert("fa-ban", "alert-danger", "Error!", ex.Message);
        }
    }

    // Bind Task Detail Grid
    private void BindGridTaskDetail(string ProjectID)
    {
        // Call your helper method
        DataSet ds = objdb.ByProcedure(
            "Usp_GetTaskDetail",
            new string[] { "ProjectId " },
            new string[] { ProjectID },
            "dataset"
        );

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            GridTaskDetail.DataSource = ds.Tables[0];
            GridTaskDetail.DataBind();
        }
        else
        {
            GridTaskDetail.DataSource = null;
            GridTaskDetail.DataBind();
        }
    }

    // Task Detail Grid command
    protected void GridTaskDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsgTask.Text = "";
        Datatable();
        if (e.CommandName == "ChangeStatus")
        {
            string taskId = e.CommandArgument.ToString();

            string lastUpdateddBy = ViewState["Emp_ID"].ToString();
            string lastUpdateddByIp = objdb.GetLocalIPAddress();
            string officeId = Session["Office_ID"].ToString();
            string userTypeId = Session["UserTypeId"].ToString();

            objdb.ByProcedure("Usp_UpdateTaskIsActive",
                new string[] { "TaskId ", "LastUpdatedBy", "LastUpdatedByIp" },
                new string[] { taskId, lastUpdateddBy, lastUpdateddByIp },
                "dataset");
            BindGridTaskDetail(ViewState["SelectedProjectId"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal2').modal('show');", true);
        }
    }
}