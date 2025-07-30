using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HREmpIncrementRegOffice : System.Web.UI.Page
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
                   
                    ddlOldLevel.Attributes.Add("readonly", "readonly");
                    txtOldBasicSalary.Attributes.Add("readonly", "readonly");
                    txtEffectiveDate.Attributes.Add("readonly", "readonly");
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
          //  ds = objdb.ByProcedure("SpHREmpIncreament", new string[] { "flag" }, new string[] { "11" }, "dataset");
            ds = objdb.ByProcedure("SpHREmpIncreament", new string[] { "flag", "Office_ID" }, new string[] { "11", ddlOldOffice.SelectedValue.ToString() }, "dataset");
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
            ddlEmployee.ClearSelection();
            ddlNewLevel.ClearSelection();
            ddlNewBasicSalary.Items.Clear();
            ddlNewBasicSalary.Items.Insert(0, new ListItem("Select New Basic Salary", "0"));
            txtEffectiveDate.Text = "";
            lblMsg.Text = "";
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
            ddlOldOffice.Items.Insert(0, new ListItem("Select Office", "0"));
            ddlNewLevel.Items.Insert(0, new ListItem("Select New Level", "0"));
            ddlOldLevel.Items.Insert(0, new ListItem("Select Old Level", "0"));
            ddlEmployee.Items.Insert(0, new ListItem("Select Employee", "0"));
            ddlNewBasicSalary.Items.Insert(0, new ListItem("Select New Basic Salary", "0"));

            ds = objdb.ByProcedure("SpAdminOffice",new string[] { "flag", "Office_ID" },
                            new string[] { "20", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOldOffice.DataSource = ds;
                ddlOldOffice.DataTextField = "Office_Name";
                ddlOldOffice.DataValueField = "Office_ID";
                ddlOldOffice.DataBind();
                ddlOldOffice.Items.Insert(0, new ListItem("Select Office", "0"));
                ddlOldOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }

           
            ds.Reset();
            FillEmployee("6");

            ds.Reset();
            ds = objdb.ByProcedure("SpHRLevelMaster", new string[] { "flag" }, new string[] { "6" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlNewLevel.DataSource = ds;
                ddlNewLevel.DataTextField = "Level_Name";
                ddlNewLevel.DataValueField = "Level_ID";
                ddlNewLevel.DataBind();
                ddlNewLevel.Items.Insert(0, new ListItem("Select New Level", "0"));

                ddlOldLevel.DataSource = ds;
                ddlOldLevel.DataTextField = "Level_Name";
                ddlOldLevel.DataValueField = "Level_ID";
                ddlOldLevel.DataBind();
                ddlOldLevel.Items.Insert(0, new ListItem("Select", "0"));
            }
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
            lblMsg.Text = "";
            DataSet ds2;
            ds2 = objdb.ByProcedure("SpHREmpIncreament", new string[] { "flag", "Office_ID" }, new string[] { flag, ddlOldOffice.SelectedValue.ToString() }, "dataset");
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
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlOldLevel.ClearSelection();
            txtOldBasicSalary.Text = "";
            if (ddlEmployee.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpHREmpIncreament", new string[] { "flag", "Emp_ID" }, new string[] { "3", ddlEmployee.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    txtOldBasicSalary.Text = ds.Tables[0].Rows[0]["Emp_BasicSalery"].ToString();
                    if (ds.Tables[0].Rows[0]["EmpLevel_ID"].ToString() != "")
                    {
                        ddlOldLevel.SelectedValue = ds.Tables[0].Rows[0]["EmpLevel_ID"].ToString();
                    }
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
            if (ddlOldOffice.SelectedIndex == 0)
            {
                msg += "Select Office.\n";
            }
            if (ddlEmployee.SelectedIndex == 0)
            {
                msg += "Select Employee Name.\n";
            }
            if (ddlOldLevel.SelectedIndex == 0)
            {
                msg += "Select Current Level.\n";
            }
            if (ddlNewLevel.SelectedIndex == 0)
            {
                msg += "Select New Level.\n";
            }
            if (txtOldBasicSalary.Text == "")
            {
                msg += "Enter Current Basic Salary.\n";
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
                    objdb.ByProcedure("SpHREmpIncreament", new string[] { "flag", "Office_ID", "Order_No", "Order_Date", "Emp_ID", "Old_LevelID", "Old_BasicSalary", "New_LevelID", "New_BasicSalary", "EffectiveDate", "IncreamentStatus", "UpdatedBy" },
               new string[] { "0", ddlOldOffice.SelectedValue.ToString(), txtOrderNo.Text, Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd"), ddlEmployee.SelectedValue.ToString(), ddlOldLevel.SelectedValue.ToString(), txtOldBasicSalary.Text, ddlNewLevel.SelectedValue.ToString(), ddlNewBasicSalary.SelectedItem.Text, Convert.ToDateTime(txtEffectiveDate.Text, cult).ToString("yyyy-MM-dd"), "Pending", ViewState["Emp_ID"].ToString() }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillEmployee("6");
                    FillGrid();
                    ClearText();
                }
                if (btnSave.Text == "Edit")
                {
                    objdb.ByProcedure("SpHREmpIncreament", new string[] { "flag", "Increament_ID", "Order_No", "Order_Date", "New_LevelID", "New_BasicSalary", "EffectiveDate", "UpdatedBy" },
               new string[] { "7", ViewState["Increament_ID"].ToString(), txtOrderNo.Text, Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd"), ddlNewLevel.SelectedValue.ToString(), ddlNewBasicSalary.SelectedItem.Text, Convert.ToDateTime(txtEffectiveDate.Text, cult).ToString("yyyy-MM-dd"), ViewState["Emp_ID"].ToString() }, "dataset");
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
    protected void ddlNewLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlNewLevel.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpHRLevelPayMaster", new string[] { "flag", "Level_ID" }, new string[] { "7", ddlNewLevel.SelectedValue.ToString() }, "dataset");
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
                ddlNewBasicSalary.ClearSelection();
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
            lblMsg.Text = "";
            ClearText();
            ddlOldLevel.ClearSelection();
            ViewState["Increament_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpHREmpIncreament", new string[] { "flag", "Increament_ID" }, new string[] { "1", ViewState["Increament_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                txtOrderNo.Text = ds.Tables[0].Rows[0]["Order_No"].ToString();
                txtOrderDate.Text = ds.Tables[0].Rows[0]["Order_Date"].ToString();
                ddlOldOffice.SelectedValue = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                FillEmployee("5");
                ddlEmployee.SelectedValue = ds.Tables[0].Rows[0]["Emp_ID"].ToString();
                ddlEmployee.Enabled = false;
                txtOldBasicSalary.Text = ds.Tables[0].Rows[0]["Old_BasicSalary"].ToString();
                if (ds.Tables[0].Rows[0]["Old_LevelID"].ToString() != "")
                {
                    ddlOldLevel.Items.FindByValue(ds.Tables[0].Rows[0]["Old_LevelID"].ToString()).Selected = true;
                }
                ddlNewLevel.ClearSelection();
                ddlNewLevel.SelectedValue = ds.Tables[0].Rows[0]["New_LevelID"].ToString();
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
                ddlNewBasicSalary.Items.FindByText(ds.Tables[0].Rows[0]["New_BasicSalary"].ToString()).Selected = true;
                txtEffectiveDate.Text = ds.Tables[0].Rows[0]["EffectiveDate"].ToString();
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
            lblMsg.Text = "";
            string Increament_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            ds = objdb.ByProcedure("SpHREmpIncreament", new string[] { "flag", "Increament_ID", "IncreamentStatus", "UpdatedBy" },
                new string[] { "2", Increament_ID, "Deleted", ViewState["Emp_ID"].ToString() }, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Deleted Successfully");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlOldOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlEmployee.Items.Clear();
        GridView1.DataSource = null;
        GridView1.DataBind();
        btnSave.Text = "Save";
        ClearText();
        if (ddlOldOffice.SelectedIndex > 0)
        {
            FillGrid();
            FillEmployee("6");
        }
    }
}