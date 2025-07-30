using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HREmpTransfer : System.Web.UI.Page
{
    DataSet ds;
    DataSet ds1;
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
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    txtOrderDate.Attributes.Add("readonly", "readonly");
                    txtEmp_PostingDate.Attributes.Add("readonly", "readonly");
                    txtEffectiveDate.Attributes.Add("readonly", "readonly");
                    txtClass.Attributes.Add("readonly", "readonly");
                    txtRelievingDate.Attributes.Add("readonly", "readonly");
                    ViewState["rlv"] = "0";
                    FillDropdown();
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

    protected void FillDropdown()
    {
        try
        {
            ddlOldOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlNewOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            ddlOldDesignation.Items.Insert(0, new ListItem("Select", "0"));
            ddlNewDesignation.Items.Insert(0, new ListItem("Select", "0"));
            ddlOldDepartment.Items.Insert(0, new ListItem("Select", "0"));
            ddlNewDepartment.Items.Insert(0, new ListItem("Select", "0"));

            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                ddlOldOfficeName.DataSource = ds;
                ddlOldOfficeName.DataTextField = "Office_Name";
                ddlOldOfficeName.DataValueField = "Office_ID";
                ddlOldOfficeName.DataBind();
                ddlOldOfficeName.Items.Insert(0, new ListItem("Select", "0"));


                ddlNewOfficeName.DataSource = ds;
                ddlNewOfficeName.DataTextField = "Office_Name";
                ddlNewOfficeName.DataValueField = "Office_ID";
                ddlNewOfficeName.DataBind();
                ddlNewOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            }
            // Department
            ds = null;
            ds = objdb.ByProcedure("SpAdminDepartment", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOldDepartment.DataSource = ds;
                ddlOldDepartment.DataTextField = "Department_Name";
                ddlOldDepartment.DataValueField = "Department_ID";
                ddlOldDepartment.DataBind();
                ddlOldDepartment.Items.Insert(0, new ListItem("Select", "0"));

                ddlNewDepartment.DataSource = ds;
                ddlNewDepartment.DataTextField = "Department_Name";
                ddlNewDepartment.DataValueField = "Department_ID";
                ddlNewDepartment.DataBind();
                ddlNewDepartment.Items.Insert(0, new ListItem("Select", "0"));
            }

            ds = null;

            ddlOldOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
            ds = null;
            FillEmployee("6");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEmployee(string flag)
    {
        try
        {

            ddlEmployee.Items.Clear();
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            DataSet ds2 = objdb.ByProcedure("SpHRTransfer", new string[] { "flag", "OldOffice" }, new string[] { flag, ddlOldOfficeName.SelectedValue.ToString() }, "dataset");
            if (ds2.Tables.Count > 0)
            {
                ddlEmployee.DataSource = ds2;
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            }
            if (flag == "6")
                ddlEmployee.Enabled = true;
            //else
            //    ddlEmployee.Enabled = false;
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
            if (ddlOldOfficeName.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpHRTransfer", new string[] { "flag", "OldOffice" }, new string[] { "1", ddlOldOfficeName.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            txtEmp_PostingDate.Text = "";
            txtClass.Text = "";
            ddlOldDesignation.Items.Clear();
            ddlNewDesignation.Items.Clear();
            ddlOldDesignation.Items.Insert(0, new ListItem("Select", "0"));
            ddlNewDesignation.Items.Insert(0, new ListItem("Select", "0"));
            ddlOldDepartment.ClearSelection();
            ddlNewDepartment.ClearSelection();
            if (ddlEmployee.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpHRTransfer", new string[] { "flag", "Emp_ID" }, new string[] { "4", ddlEmployee.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    txtEmp_PostingDate.Text = ds.Tables[0].Rows[0]["Emp_PostingDate"].ToString();
                    txtClass.Text = ds.Tables[0].Rows[0]["Emp_Class"].ToString();

                    ddlOldDesignation.DataSource = ds.Tables[1];
                    ddlOldDesignation.DataTextField = "Designation_Name";
                    ddlOldDesignation.DataValueField = "Designation_ID";
                    ddlOldDesignation.DataBind();
                    ddlOldDesignation.Items.Insert(0, new ListItem("Select", "0"));

                    ddlOldDesignation.SelectedValue = ds.Tables[0].Rows[0]["Designation_ID"].ToString();

                    ddlNewDesignation.DataSource = ds.Tables[1];
                    ddlNewDesignation.DataTextField = "Designation_Name";
                    ddlNewDesignation.DataValueField = "Designation_ID";
                    ddlNewDesignation.DataBind();
                    ddlNewDesignation.Items.Insert(0, new ListItem("Select", "0"));

                    ddlNewDesignation.SelectedValue = ds.Tables[0].Rows[0]["Designation_ID"].ToString();


                    ddlOldDepartment.SelectedValue = ds.Tables[0].Rows[0]["Department_ID"].ToString();
                    ddlNewDepartment.SelectedValue = ds.Tables[0].Rows[0]["Department_ID"].ToString();



                }
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
            string msg = "";
            lblMsg.Text = "";
            if (txtOrderNo.Text.Trim() == "")
            {
                msg += "Enter Order No. \\n";
            }
            if (txtOrderDate.Text.Trim() == "")
            {
                msg += "Enter Order Date. \\n";
            }
            if (ddlOldOfficeName.SelectedIndex == 0)
            {
                msg += "Select Current Office. \\n";
            }
            if (ddlEmployee.SelectedIndex == 0)
            {
                msg += "Select Employee Name. \\n";
            }
            if (txtEmp_PostingDate.Text.Trim() == "")
            {
                msg += "Enter Posting Date. \\n";
            }
            if (ddlOldDesignation.SelectedIndex == 0)
            {
                msg += "Select Current Designation. \\n";
            }
            if (ddlOldDepartment.SelectedIndex == 0)
            {
                msg += "Select Current Department. \\n";
            }
            if (ddlNewOfficeName.SelectedIndex == 0)
            {
                msg += "Select New Office. \\n";
            }
            if (txtEffectiveDate.Text.Trim() == "")
            {
                msg += "Enter Effective Date. \\n";
            }
            if (txtClass.Text.Trim() == "")
            {
                msg += "Enter Class. \\n";
            }
            if (ddlNewDesignation.SelectedIndex == 0)
            {
                msg += "Select New Designation. \\n";
            }
            if (ddlNewDepartment.SelectedIndex == 0)
            {
                msg += "Select New Department. \\n";
            }
            if (txtRemark.Text.Trim() == "")
            {
                msg += "Enter Remark. \\n";
            }
            if (msg.Trim() == "")
            {
                if (btnSave.Text == "Save")
                {
                    objdb.ByProcedure("SpHRTransfer", new string[] { "flag", "OrderNo", "OrderDate", "Emp_ID", "OldOffice", "OldPostingDate", "OldDesignation_ID", "OldDepartment", "Class", "NewOffice", "NewEffectiveDate", "NewDesignation_ID", "NewDepartment", "Remark", "TransferUpdatedBy" },
                   new string[] { "0", txtOrderNo.Text, Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy/MM/dd"), ddlEmployee.SelectedValue.ToString(), ddlOldOfficeName.SelectedValue.ToString(), Convert.ToDateTime(txtEmp_PostingDate.Text, cult).ToString("yyyy/MM/dd"), ddlOldDesignation.SelectedValue.ToString(), ddlOldDepartment.SelectedValue.ToString(), txtClass.Text, ddlNewOfficeName.SelectedValue.ToString(), Convert.ToDateTime(txtEffectiveDate.Text, cult).ToString("yyyy/MM/dd"), ddlNewDesignation.SelectedValue.ToString(), ddlNewDepartment.SelectedValue.ToString(), txtRemark.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillEmployee("6");
                    ClearText();
                    FillGrid();
                }
                else if (btnSave.Text == "Edit")
                {
                    objdb.ByProcedure("SpHRTransfer", new string[] { "flag", "TransferID", "OrderNo", "OrderDate", "Emp_ID", "OldOffice", "OldPostingDate", "OldDesignation_ID", "OldDepartment", "Class", "NewOffice", "NewEffectiveDate", "NewDesignation_ID", "NewDepartment", "Remark", "TransferUpdatedBy" },
                   new string[] { "5", ViewState["TransferID"].ToString(), txtOrderNo.Text, Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy/MM/dd"), ddlEmployee.SelectedValue.ToString(), ddlOldOfficeName.SelectedValue.ToString(), Convert.ToDateTime(txtEmp_PostingDate.Text, cult).ToString("yyyy/MM/dd"), ddlOldDesignation.SelectedValue.ToString(), ddlOldDepartment.SelectedValue.ToString(), txtClass.Text, ddlNewOfficeName.SelectedValue.ToString(), Convert.ToDateTime(txtEffectiveDate.Text, cult).ToString("yyyy/MM/dd"), ddlNewDesignation.SelectedValue.ToString(), ddlNewDepartment.SelectedValue.ToString(), txtRemark.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillEmployee("6");
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
        try
        {
            txtOrderNo.Text = "";
            txtOrderDate.Text = "";
            ddlEmployee.ClearSelection();
            txtEmp_PostingDate.Text = "";
            ddlOldDesignation.Items.Clear();
            ddlOldDesignation.Items.Insert(0, new ListItem("Select", "0"));
            ddlNewOfficeName.ClearSelection();
            ddlOldDepartment.ClearSelection();
            ddlNewDepartment.ClearSelection();
            txtEffectiveDate.Text = "";
            txtClass.Text = "";
            ddlNewDesignation.Items.Clear();
            ddlNewDesignation.Items.Insert(0, new ListItem("Select", "0"));
            txtRemark.Text = "";
            btnSave.Text = "Save";

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
            ClearText();
            lblMsg.Text = "";
            ViewState["TransferID"] = GridView1.SelectedDataKey.Value.ToString();
            if (ViewState["rlv"].ToString() == "0")
            {
                ds = objdb.ByProcedure("SpHRTransfer", new string[] { "flag", "TransferID" }, new string[] { "2", ViewState["TransferID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtOrderNo.Text = ds.Tables[0].Rows[0]["OrderNo"].ToString();
                    txtOrderDate.Text = ds.Tables[0].Rows[0]["OrderDate"].ToString();
                    FillEmployee("7");
                    ddlEmployee.SelectedValue = ds.Tables[0].Rows[0]["Emp_ID"].ToString();
                    ddlEmployee.Enabled = false;
                    txtEmp_PostingDate.Text = ds.Tables[0].Rows[0]["OldPostingDate"].ToString();
                    txtClass.Text = ds.Tables[0].Rows[0]["Class"].ToString();
                    ddlOldDepartment.ClearSelection();
                    ddlOldDepartment.SelectedValue = ds.Tables[0].Rows[0]["OldDepartment"].ToString();

                    ds1 = objdb.ByProcedure("SpHRTransfer", new string[] { "flag", "Class" }, new string[] { "3", ds.Tables[0].Rows[0]["Class"].ToString() }, "dataset");
                    if (ds.Tables.Count > 0)
                    {

                        ddlOldDesignation.DataSource = ds1.Tables[0];
                        ddlOldDesignation.DataTextField = "Designation_Name";
                        ddlOldDesignation.DataValueField = "Designation_ID";
                        ddlOldDesignation.DataBind();
                        ddlOldDesignation.Items.Insert(0, new ListItem("Select", "0"));

                        ddlOldDesignation.SelectedValue = ds.Tables[0].Rows[0]["OldDesignation_ID"].ToString();

                        ddlNewDesignation.DataSource = ds1.Tables[0];
                        ddlNewDesignation.DataTextField = "Designation_Name";
                        ddlNewDesignation.DataValueField = "Designation_ID";
                        ddlNewDesignation.DataBind();
                        ddlNewDesignation.Items.Insert(0, new ListItem("Select", "0"));

                        ddlNewDesignation.SelectedValue = ds.Tables[0].Rows[0]["NewDesignation_ID"].ToString();
                    }

                    ddlNewOfficeName.SelectedValue = ds.Tables[0].Rows[0]["NewOffice"].ToString();
                    txtEffectiveDate.Text = ds.Tables[0].Rows[0]["NewEffectiveDate"].ToString();
                    ddlNewDepartment.ClearSelection();
                    ddlNewDepartment.SelectedValue = ds.Tables[0].Rows[0]["NewDepartment"].ToString();
                    txtRemark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();

                    btnSave.Text = "Edit";
                }
            }
            else if (ViewState["rlv"].ToString() == "1")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal();", true);
                ViewState["rlv"] = "0";
            }

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
            lblMsg.Text = "";
            string TransferID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpHRTransfer", new string[] { "flag", "TransferID", "TransferUpdatedBy" },
                       new string[] { "8", TransferID, ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillEmployee("6");
            ClearText();
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnRelieved_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            txtRelievingDate.Text = "";
            ViewState["rlv"] = "1";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnRelieve_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            lblMsg.Text = "";
            ViewState["rlv"] = "0";
            if (txtRelievingDate.Text.Trim() == "")
            {
                msg += "Enter Relieving Date. \\n";
            }
            if (msg.Trim() == "")
            {
                objdb.ByProcedure("SpHRTransfer", new string[] { "flag", "TransferID", "RelievingDate", "TransferUpdatedBy" },
                   new string[] { "9", ViewState["TransferID"].ToString(), Convert.ToDateTime(txtRelievingDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Emp_ID"].ToString() }, "dataset");

                txtRelievingDate.Text = "";
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                FillEmployee("6");
                ClearText();
                FillGrid();

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
    protected void ddlOldOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee("6");
    }
}