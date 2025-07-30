using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HREmpPromotion : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillDropDown();
                    txtOrderDate.Attributes.Add("readonly", "readonly");
                    txtEffectiveDate.Attributes.Add("readonly", "readonly");
                    ddlOldOffice.Attributes.Add("readonly", "readonly");
                    ddlOldLevel.Attributes.Add("readonly", "readonly");
                    ddlOldClass.Attributes.Add("readonly", "readonly");
                    txtOldBasicSalary.Attributes.Add("readonly", "readonly");
                    ddlOldDepartment.Attributes.Add("readonly", "readonly");
                    ddlOldDesignation.Attributes.Add("readonly", "readonly");
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
            ds = objdb.ByProcedure("SpHREmpPromotion", new string[] { "flag" }, new string[] { "4" }, "dataset");
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
    protected void ClearText()
    {
        try
        {
            txtOrderNo.Text = "";
            txtOrderDate.Text = "";
            ddlOldOffice.ClearSelection();
            ddlEmployee.ClearSelection();
            ddlOldLevel.ClearSelection();
            txtOldBasicSalary.Text = "";
            ddlOldClass.ClearSelection();
            ddlOldDesignation.ClearSelection();
            ddlOldDepartment.ClearSelection();
            ddlNewLevel.ClearSelection();
            ddlNewBasicSalary.ClearSelection();
            ddlNewDepartment.ClearSelection();
            ddlNewDesignation.ClearSelection();
            ddlNewClass.ClearSelection();
            txtRemark.Text = "";
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
            DataSet ds2;
            ds2 = objdb.ByProcedure("SpHREmpIncreament", new string[] { "flag", "Office_ID" }, new string[] { flag, ViewState["Office_ID"].ToString() }, "dataset");
            if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = ds2;
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("Select Employee", "0"));
            }
            if (flag == "6")
            {
                ddlEmployee.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDropDown()
    {
        try
        {
            ddlOldOffice.Items.Insert(0, new ListItem("Select Old Office", "0"));
            ddlNewLevel.Items.Insert(0, new ListItem("Select New Level", "0"));
            ddlOldLevel.Items.Insert(0, new ListItem("Select Old Level", "0"));
            ddlEmployee.Items.Insert(0, new ListItem("Select Employee", "0"));
            ddlNewBasicSalary.Items.Insert(0, new ListItem("Select New Basic Salary", "0"));

            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOldOffice.DataSource = ds;
                ddlOldOffice.DataTextField = "Office_Name";
                ddlOldOffice.DataValueField = "Office_ID";
                ddlOldOffice.DataBind();
                ddlOldOffice.Items.Insert(0, new ListItem("Select Old Office", "0"));
            }

            ddlOldOffice.SelectedValue = ViewState["Office_ID"].ToString();
            FillEmployee("6");
            ds.Reset();
            ds = objdb.ByProcedure("SpHRLevelMaster", new string[] { "flag" }, new string[] { "6" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOldLevel.DataSource = ds;
                ddlOldLevel.DataTextField = "Level_Name";
                ddlOldLevel.DataValueField = "Level_ID";
                ddlOldLevel.DataBind();
                ddlOldLevel.Items.Insert(0, new ListItem("Select", "0"));

                ddlNewLevel.DataSource = ds;
                ddlNewLevel.DataTextField = "Level_Name";
                ddlNewLevel.DataValueField = "Level_ID";
                ddlNewLevel.DataBind();
                ddlNewLevel.Items.Insert(0, new ListItem("Select New Level", "0"));

            }

            ds.Reset();
            ds = objdb.ByProcedure("SpHREmpPromotion", new string[] { "flag" }, new string[] { "3" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOldClass.DataSource = ds;
                ddlOldClass.DataTextField = "Class_Name";
                ddlOldClass.DataValueField = "Class_ID";
                ddlOldClass.DataBind();
                ddlOldClass.Items.Insert(0, new ListItem("Select Old Level", "0"));

                ddlNewClass.DataSource = ds;
                ddlNewClass.DataTextField = "Class_Name";
                ddlNewClass.DataValueField = "Class_ID";
                ddlNewClass.DataBind();
                ddlNewClass.Items.Insert(0, new ListItem("Select New Level", "0"));
            }

            ds.Reset();
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
            DataSet ds1;
            ddlOldLevel.ClearSelection();
            txtOldBasicSalary.Text = "";
            if (ddlEmployee.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Emp_ID" }, new string[] { "21", ddlEmployee.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    txtOldBasicSalary.Text = ds.Tables[0].Rows[0]["Emp_BasicSalery"].ToString();
                    if (ds.Tables[0].Rows[0]["EmpLevel_ID"].ToString() != "")
                    {
                        ddlOldLevel.SelectedValue = ds.Tables[0].Rows[0]["EmpLevel_ID"].ToString();
                    }
                    ddlOldClass.ClearSelection();
                    ddlOldClass.Items.FindByText(ds.Tables[0].Rows[0]["Emp_Class"].ToString()).Selected = true;
                    ds1 = objdb.ByProcedure("SpAdminDesignation", new string[] { "flag", "Class" }, new string[] { "6", ddlOldClass.SelectedItem.Text }, "dataset");
                    if (ds1.Tables[0].Rows.Count != 0)
                    {
                        ddlOldDesignation.DataSource = ds1;
                        ddlOldDesignation.DataTextField = "Designation_Name";
                        ddlOldDesignation.DataValueField = "Designation_ID";
                        ddlOldDesignation.DataBind();
                        ddlOldDesignation.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    ddlOldDesignation.SelectedValue = ds.Tables[0].Rows[0]["Designation_ID"].ToString();
                    ddlOldDepartment.ClearSelection();
                    ddlOldDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["Department_ID"].ToString()).Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlNewLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlNewLevel.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpHRLevelPayMaster", new string[] { "flag", "Level_ID" }, new string[] { "8", ddlNewLevel.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlNewBasicSalary.DataSource = ds;
                    ddlNewBasicSalary.DataTextField = "PayScale";
                    ddlNewBasicSalary.DataValueField = "PayScale_ID";
                    ddlNewBasicSalary.DataBind();
                    ddlNewBasicSalary.Items.Insert(0, new ListItem("Select New Basic Salary", "0"));
                }
            }
            else
            {
                ddlNewLevel.ClearSelection();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlNewClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminDesignation", new string[] { "flag", "Class" }, new string[] { "6", ddlNewClass.SelectedItem.Text }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlNewDesignation.DataSource = ds;
                ddlNewDesignation.DataTextField = "Designation_Name";
                ddlNewDesignation.DataValueField = "Designation_ID";
                ddlNewDesignation.DataBind();
                ddlNewDesignation.Items.Insert(0, new ListItem("Select", "0"));
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
            lblMsg.Text = "";
            string msg = "";
            if (txtOrderNo.Text == "")
            {
                msg += "Enter Order No.\n";
            }
            if (txtOrderDate.Text == "")
            {
                msg += "Select Order Date.\n";
            }
            if (ddlEmployee.SelectedIndex == 0)
            {
                msg += "Select Employee Name.\n";
            }
            if (ddlNewLevel.SelectedIndex == 0)
            {
                msg += "Select New Level.\n";
            }
            if (ddlNewBasicSalary.SelectedIndex == 0)
            {
                msg += "Select New Basic Salary.\n";
            }
            if (txtEffectiveDate.Text == "")
            {
                msg += "Select Effective Date.\n";
            }
            if (msg == "")
            {
                if (btnSave.Text == "Save")
                {
                    objdb.ByProcedure("SpHREmpPromotion", new string[] { "flag", "OrderNo", "OrderDate", "Emp_ID", "OldOffice", "OldClass", "OldDesignation_ID", "OldDepartment", "OldLevel", "OldBasicSalery", "NewEffectiveDate", "NewClass", "NewDesignation_ID", "NewDepartment", "NewLevel", "NewBasicSalery", "Remark", "PromotionStatus", "PromotionUpdatedBy" },
               new string[] { "0", txtOrderNo.Text, Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd"), ddlEmployee.SelectedValue.ToString(), ViewState["Office_ID"].ToString(), ddlOldClass.SelectedItem.Text, ddlOldDesignation.SelectedValue.ToString(), ddlOldDepartment.SelectedValue.ToString(), ddlOldLevel.SelectedValue.ToString(), txtOldBasicSalary.Text, Convert.ToDateTime(txtEffectiveDate.Text, cult).ToString("yyyy-MM-dd"), ddlNewClass.SelectedItem.Text, ddlNewDesignation.SelectedValue.ToString(), ddlNewDepartment.SelectedValue.ToString(), ddlNewLevel.SelectedValue.ToString(), ddlNewBasicSalary.SelectedItem.Text, txtRemark.Text, "Pending", ViewState["Emp_ID"].ToString() }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillGrid();
                    ClearText();
                }
                if (btnSave.Text == "Edit")
                {
                    objdb.ByProcedure("SpHREmpPromotion", new string[] { "flag", "PromotionID", "OrderNo", "OrderDate", "NewEffectiveDate", "NewClass", "NewDesignation_ID", "NewDepartment", "NewLevel", "NewBasicSalery", "Remark", "PromotionUpdatedBy" },
               new string[] { "7", ViewState["PromotionID"].ToString(), txtOrderNo.Text, Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtEffectiveDate.Text, cult).ToString("yyyy-MM-dd"), ddlNewClass.SelectedItem.Text, ddlNewDesignation.SelectedValue.ToString(), ddlNewDepartment.SelectedValue.ToString(), ddlNewLevel.SelectedValue.ToString(), ddlNewBasicSalary.SelectedItem.Text, txtRemark.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    btnSave.Text = "Save";
                    FillGrid();
                    FillEmployee("6");
                    ClearText();
                }
            }
            else
            {

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
            DataSet ds3;
            DataSet ds4;
            DataSet ds5;
            lblMsg.Text = "";
            ClearText();
            ddlOldLevel.ClearSelection();
            ViewState["PromotionID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpHREmpPromotion", new string[] { "flag", "PromotionID" }, new string[] { "1", ViewState["PromotionID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlOldOffice.SelectedValue = ViewState["Office_ID"].ToString();
                txtOrderNo.Text = ds.Tables[0].Rows[0]["OrderNo"].ToString();
                txtOrderDate.Text = ds.Tables[0].Rows[0]["OrderDate"].ToString();
                FillEmployee("5");
                ddlEmployee.SelectedValue = ds.Tables[0].Rows[0]["Emp_ID"].ToString();
                ddlEmployee.Enabled = false;
                txtOldBasicSalary.Text = ds.Tables[0].Rows[0]["OldBasicSalery"].ToString();
                if (ds.Tables[0].Rows[0]["OldLevel"].ToString() != "")
                {
                    ddlOldLevel.Items.FindByValue(ds.Tables[0].Rows[0]["OldLevel"].ToString()).Selected = true;
                }
                ddlOldClass.Items.FindByText(ds.Tables[0].Rows[0]["OldClass"].ToString()).Selected = true;
                ds4 = objdb.ByProcedure("SpAdminDesignation", new string[] { "flag", "Class" }, new string[] { "6", ddlOldClass.SelectedItem.Text }, "dataset");
                if (ds4.Tables[0].Rows.Count != 0)
                {
                    ddlOldDesignation.DataSource = ds4;
                    ddlOldDesignation.DataTextField = "Designation_Name";
                    ddlOldDesignation.DataValueField = "Designation_ID";
                    ddlOldDesignation.DataBind();
                    ddlOldDesignation.Items.Insert(0, new ListItem("Select", "0"));
                }
                ddlOldDesignation.ClearSelection();
                ddlOldDesignation.Items.FindByValue(ds.Tables[0].Rows[0]["OldDesignation_ID"].ToString()).Selected = true;
                ddlOldDepartment.ClearSelection();
                ddlOldDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["OldDepartment"].ToString()).Selected = true;
                ddlNewLevel.ClearSelection();
                ddlNewLevel.SelectedValue = ds.Tables[0].Rows[0]["NewLevel"].ToString();
                ddlNewBasicSalary.ClearSelection();
                ds3 = objdb.ByProcedure("SpHRLevelPayMaster", new string[] { "flag", "Level_ID" }, new string[] { "7", ddlNewLevel.SelectedValue.ToString() }, "dataset");
                if (ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
                {
                    ddlNewBasicSalary.DataSource = ds3;
                    ddlNewBasicSalary.DataTextField = "PayScale";
                    ddlNewBasicSalary.DataValueField = "PayScale_ID";
                    ddlNewBasicSalary.DataBind();
                    ddlNewBasicSalary.Items.Insert(0, new ListItem("Select New Basic Salary", "0"));
                }
                ddlNewBasicSalary.Items.FindByText(ds.Tables[0].Rows[0]["NewBasicSalery"].ToString()).Selected = true;
                ddlNewClass.Items.FindByText(ds.Tables[0].Rows[0]["NewClass"].ToString()).Selected = true;
                ddlNewDesignation.ClearSelection();
                ds5 = objdb.ByProcedure("SpAdminDesignation", new string[] { "flag", "Class" }, new string[] { "6", ddlNewClass.SelectedItem.Text }, "dataset");
                if (ds5.Tables[0].Rows.Count != 0)
                {
                    ddlNewDesignation.DataSource = ds5;
                    ddlNewDesignation.DataTextField = "Designation_Name";
                    ddlNewDesignation.DataValueField = "Designation_ID";
                    ddlNewDesignation.DataBind();
                    ddlNewDesignation.Items.Insert(0, new ListItem("Select", "0"));
                }
                ddlNewDesignation.Items.FindByValue(ds.Tables[0].Rows[0]["NewDesignation_ID"].ToString()).Selected = true;
                ddlNewDepartment.ClearSelection();
                ddlNewDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["NewDepartment"].ToString()).Selected = true;
                txtRemark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
                txtEffectiveDate.Text = ds.Tables[0].Rows[0]["NewEffectiveDate"].ToString();
                btnSave.Text = "Edit";
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
            string PromotionID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            ds = objdb.ByProcedure("SpHREmpPromotion", new string[] { "flag", "PromotionID", "PromotionStatus", "PromotionUpdatedBy" },
                new string[] { "2", PromotionID, "Deleted", ViewState["Emp_ID"].ToString() }, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Deleted Successfully");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}