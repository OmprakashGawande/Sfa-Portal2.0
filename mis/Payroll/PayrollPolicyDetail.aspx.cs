using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollPolicyDetail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["Policy_ID"] = "0";
                txtPolicy_StartDate.Attributes.Add("readonly", "readonly");
                txtPolicy_EndDate.Attributes.Add("readonly", "readonly");
                FillDropdown();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillDropdown()
    {
        try
        {
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));

                ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
                if(ddlOfficeName.SelectedIndex >0)
                {
                    FillEmployee();
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
            lblMsg.Text = "";
            if (ddlEmployee.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpPayrollPolicyDetail", new string[] { "flag", "Emp_ID" }, new string[] { "1", ddlEmployee.SelectedValue.ToString() }, "dataset");
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
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlEmployee.Items.Clear();
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            FillEmployee();
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEmployee()
    {
        try
        {
            if (ddlOfficeName.SelectedIndex > 0)
            {
                DataSet ds1 = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ddlOfficeName.SelectedValue.ToString() }, "dataset");
                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    ddlEmployee.DataSource = ds1;
                    ddlEmployee.DataTextField = "Emp_Name";
                    ddlEmployee.DataValueField = "Emp_ID";
                    ddlEmployee.DataBind();
                    ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
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
            FillGrid();
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
            if (ddlOfficeName.SelectedIndex == 0)
            {
                msg += "Select Office Name. \\n";
            }
            if (ddlEmployee.SelectedIndex == 0)
            {
                msg += "Select Employee Name. \\n";
            }
            if (ddlPolicy_Type.SelectedIndex == 0)
            {
                msg += "Select Policy Type. \\n";
            }
            if (txtPolicy_No.Text == "")
            {
                msg += "Enter Policy No. \\n";
            }
            if (txtPolicy_Name.Text == "")
            {
                msg += "Enter Policy Name. \\n";
            }
            if (txtPolicy_PremiumAmount.Text == "")
            {
                msg += "Enter Premium Amount. \\n";
            }
            else if (!objdb.isDecimal(txtPolicy_PremiumAmount.Text))
            {
                msg += "Enter Correct Premium Amount. \\n";
            }
            if (ddlPolicy_PremiumFrequency.SelectedIndex == 0)
            {
                msg += "Select Premium Frequency. \\n";
            }
            if (txtPolicy_StartDate.Text == "")
            {
                msg += "Enter Policy Start Date. \\n";
            }
            if (txtPolicy_EndDate.Text == "")
            {
                msg += "Enter Policy End Date. \\n";
            }
            if (ddlPolicy_IsActive.SelectedIndex == 0)
            {
                msg += "Select Status. \\n";
            }

            if (txtPolicy_JanAmount.Text == "")
            {
                txtPolicy_JanAmount.Text = "0";
            }
            if (!objdb.isDecimal(txtPolicy_JanAmount.Text))
            {
                msg += "Enter Correct Jan Amount. \\n";
            }

            if (txtPolicy_FebAmount.Text == "")
            {
                  txtPolicy_FebAmount.Text = "0";
            }
            if (!objdb.isDecimal(txtPolicy_FebAmount.Text))
            {
                msg += "Enter Correct Feb Amount. \\n";
            }

            if (txtPolicy_MarAmount.Text == "")
            {
                 txtPolicy_MarAmount.Text = "0";
            }
            if (!objdb.isDecimal(txtPolicy_MarAmount.Text))
            {
                msg += "Enter Correct Mar Amount. \\n";
            }

            if (txtPolicy_AprAmount.Text == "")
            {
                 txtPolicy_AprAmount.Text = "0";
            }
            if (!objdb.isDecimal(txtPolicy_AprAmount.Text))
            {
                msg += "Enter Correct Apr Amount. \\n";
            }

            if (txtPolicy_MayAmount.Text == "")
            {
                  txtPolicy_MayAmount.Text = "0";
            }
            if (!objdb.isDecimal(txtPolicy_MayAmount.Text))
            {
                msg += "Enter Correct May Amount. \\n";
            }

            if (txtPolicy_JunAmount.Text == "")
            {
                  txtPolicy_JunAmount.Text = "0";
            }
            if (!objdb.isDecimal(txtPolicy_JunAmount.Text))
            {
                msg += "Enter Correct Jun Amount. \\n";
            }

            if (txtPolicy_JulAmount.Text == "")
            {
                 txtPolicy_JulAmount.Text = "0";
            }
            if (!objdb.isDecimal(txtPolicy_JulAmount.Text))
            {
                msg += "Enter Correct Jul Amount. \\n";
            }

            if (txtPolicy_AugAmount.Text == "")
            {
                 txtPolicy_AugAmount.Text = "0";
            }
            if (!objdb.isDecimal(txtPolicy_AugAmount.Text))
            {
                msg += "Enter Correct Aug Amount. \\n";
            }

            if (txtPolicy_SepAmount.Text == "")
            {
                 txtPolicy_SepAmount.Text = "0";
            }
            if (!objdb.isDecimal(txtPolicy_SepAmount.Text))
            {
                msg += "Enter Correct Sep Amount. \\n";
            }

            if (txtPolicy_OctAmount.Text == "")
            {
                 txtPolicy_OctAmount.Text = "0";
            }
            if (!objdb.isDecimal(txtPolicy_OctAmount.Text))
            {
                msg += "Enter Correct Oct Amount. \\n";
            }

            if (txtPolicy_NovAmount.Text == "")
            {
                  txtPolicy_NovAmount.Text = "0";
            }
            if (!objdb.isDecimal(txtPolicy_NovAmount.Text))
            {
                msg += "Enter Correct Nov Amount. \\n";
            }

            if (txtPolicy_DecAmount.Text == "")
            {
                 txtPolicy_DecAmount.Text = "0";
            }
            if (!objdb.isDecimal(txtPolicy_DecAmount.Text))
            {
                msg += "Enter Correct Dec Amount. \\n";
            }

            if (txtPolicy_JanAmount.Text =="0" && txtPolicy_FebAmount.Text == "0" && txtPolicy_MarAmount.Text == "0" && txtPolicy_AprAmount.Text == "0"&& txtPolicy_MayAmount.Text == "0")
            {
                if (txtPolicy_JunAmount.Text == "0" && txtPolicy_JulAmount.Text == "0" && txtPolicy_AugAmount.Text == "0" && txtPolicy_SepAmount.Text == "0" && txtPolicy_OctAmount.Text == "0" && txtPolicy_NovAmount.Text == "0" && txtPolicy_DecAmount.Text == "0")
                {
                    msg += "Please fill atleast one month amount. \\n";
                }
            }
           


            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpPayrollPolicyDetail", new string[] { "flag", "Policy_ID", "Policy_No" }, new string[] { "4", ViewState["Policy_ID"].ToString(), txtPolicy_No.Text }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (btnSave.Text == "Save" && ViewState["Policy_ID"].ToString() == "0" && Status == 0)
                {
                    objdb.ByProcedure("SpPayrollPolicyDetail",
                    new string[] { "flag", "Policy_IsActive", "Emp_ID", "Office_ID", "Policy_Type", "Policy_No", "Policy_Name", "Policy_PremiumAmount", "Policy_PremiumFrequency", "Policy_StartDate", "Policy_EndDate", "Policy_AprAmount", "Policy_MayAmount", "Policy_JunAmount", "Policy_JulAmount", "Policy_AugAmount", "Policy_SepAmount", "Policy_OctAmount", "Policy_NovAmount", "Policy_DecAmount", "Policy_JanAmount", "Policy_FebAmount", "Policy_MarAmount", "Policy_UpdatedBy" },
                    new string[] { "0", ddlPolicy_IsActive.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), ddlOfficeName.SelectedValue.ToString(), ddlPolicy_Type.SelectedValue.ToString(), txtPolicy_No.Text, txtPolicy_Name.Text, txtPolicy_PremiumAmount.Text, ddlPolicy_PremiumFrequency.SelectedValue.ToString(), Convert.ToDateTime(txtPolicy_StartDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtPolicy_EndDate.Text, cult).ToString("yyyy/MM/dd"), txtPolicy_AprAmount.Text, txtPolicy_MayAmount.Text, txtPolicy_JunAmount.Text, txtPolicy_JulAmount.Text, txtPolicy_AugAmount.Text, txtPolicy_SepAmount.Text, txtPolicy_OctAmount.Text, txtPolicy_NovAmount.Text, txtPolicy_DecAmount.Text, txtPolicy_JanAmount.Text, txtPolicy_FebAmount.Text, txtPolicy_MarAmount.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    ClearText();
                    FillGrid();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else if (btnSave.Text == "Edit" && ViewState["Policy_ID"].ToString() != "0" && Status == 0)
                {
                    txtPolicy_No.ReadOnly = false;
                    objdb.ByProcedure("SpPayrollPolicyDetail",
                    new string[] { "flag", "Policy_ID", "Policy_IsActive", "Policy_Name", "Policy_PremiumAmount", "Policy_PremiumFrequency", "Policy_StartDate", "Policy_EndDate", "Policy_AprAmount", "Policy_MayAmount", "Policy_JunAmount", "Policy_JulAmount", "Policy_AugAmount", "Policy_SepAmount", "Policy_OctAmount", "Policy_NovAmount", "Policy_DecAmount", "Policy_JanAmount", "Policy_FebAmount", "Policy_MarAmount", "Policy_UpdatedBy" },
                    new string[] { "2", ViewState["Policy_ID"].ToString(), ddlPolicy_IsActive.SelectedValue.ToString(), txtPolicy_Name.Text, txtPolicy_PremiumAmount.Text, ddlPolicy_PremiumFrequency.SelectedValue.ToString(), Convert.ToDateTime(txtPolicy_StartDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtPolicy_EndDate.Text, cult).ToString("yyyy/MM/dd"), txtPolicy_AprAmount.Text, txtPolicy_MayAmount.Text, txtPolicy_JunAmount.Text, txtPolicy_JulAmount.Text, txtPolicy_AugAmount.Text, txtPolicy_SepAmount.Text, txtPolicy_OctAmount.Text, txtPolicy_NovAmount.Text, txtPolicy_DecAmount.Text, txtPolicy_JanAmount.Text, txtPolicy_FebAmount.Text, txtPolicy_MarAmount.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                    txtPolicy_No.ReadOnly = true;
                    ClearText();
                    FillGrid();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Policy No already exist.');", true);
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ClearText();
            ViewState["Policy_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpPayrollPolicyDetail", new string[] { "flag", "Policy_ID" }, new string[] { "3", ViewState["Policy_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ddlPolicy_IsActive.Items.FindByValue(ds.Tables[0].Rows[0]["Policy_IsActive"].ToString()).Selected = true;
                //ddlOfficeName.ClearSelection();

                //ddlOfficeName.Items.FindByValue(ds.Tables[0].Rows[0]["Office_ID"].ToString()).Selected = true;
                //FillEmployee();
                //ddlEmployee.Items.FindByValue(ds.Tables[0].Rows[0]["Emp_ID"].ToString()).Selected = true;

                ddlPolicy_Type.ClearSelection();
                ddlPolicy_Type.Items.FindByValue(ds.Tables[0].Rows[0]["Policy_Type"].ToString()).Selected = true;
                txtPolicy_No.Text = ds.Tables[0].Rows[0]["Policy_No"].ToString();
                txtPolicy_Name.Text = ds.Tables[0].Rows[0]["Policy_Name"].ToString();
                txtPolicy_PremiumAmount.Text = ds.Tables[0].Rows[0]["Policy_PremiumAmount"].ToString();
                ddlPolicy_PremiumFrequency.Items.FindByValue(ds.Tables[0].Rows[0]["Policy_PremiumFrequency"].ToString()).Selected = true;
                txtPolicy_StartDate.Text = ds.Tables[0].Rows[0]["Policy_StartDate"].ToString();
                txtPolicy_EndDate.Text = ds.Tables[0].Rows[0]["Policy_EndDate"].ToString();
                txtPolicy_AprAmount.Text = ds.Tables[0].Rows[0]["Policy_AprAmount"].ToString();
                txtPolicy_MayAmount.Text = ds.Tables[0].Rows[0]["Policy_MayAmount"].ToString();
                txtPolicy_JunAmount.Text = ds.Tables[0].Rows[0]["Policy_JunAmount"].ToString();
                txtPolicy_JulAmount.Text = ds.Tables[0].Rows[0]["Policy_JulAmount"].ToString();
                txtPolicy_AugAmount.Text = ds.Tables[0].Rows[0]["Policy_AugAmount"].ToString();
                txtPolicy_SepAmount.Text = ds.Tables[0].Rows[0]["Policy_SepAmount"].ToString();
                txtPolicy_OctAmount.Text = ds.Tables[0].Rows[0]["Policy_OctAmount"].ToString();
                txtPolicy_NovAmount.Text = ds.Tables[0].Rows[0]["Policy_NovAmount"].ToString();
                txtPolicy_DecAmount.Text = ds.Tables[0].Rows[0]["Policy_DecAmount"].ToString();
                txtPolicy_JanAmount.Text = ds.Tables[0].Rows[0]["Policy_JanAmount"].ToString();
                txtPolicy_FebAmount.Text = ds.Tables[0].Rows[0]["Policy_FebAmount"].ToString();
                txtPolicy_MarAmount.Text = ds.Tables[0].Rows[0]["Policy_MarAmount"].ToString();
                btnSave.Text = "Edit";
                FunDesable();
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

            //ddlOfficeName.ClearSelection();
            //ddlEmployee.Items.Clear();
            //ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            ddlPolicy_Type.ClearSelection();
            txtPolicy_No.Text = "";
            txtPolicy_Name.Text = "";
            txtPolicy_PremiumAmount.Text = "";
            ddlPolicy_PremiumFrequency.ClearSelection();
            txtPolicy_StartDate.Text = "";
            txtPolicy_EndDate.Text = "";
            ddlPolicy_IsActive.ClearSelection();
            txtPolicy_AprAmount.Text = "";
            txtPolicy_MayAmount.Text = "";
            txtPolicy_JunAmount.Text = "";
            txtPolicy_JulAmount.Text = "";
            txtPolicy_AugAmount.Text = "";
            txtPolicy_SepAmount.Text = "";
            txtPolicy_OctAmount.Text = "";
            txtPolicy_NovAmount.Text = "";
            txtPolicy_DecAmount.Text = "";
            txtPolicy_JanAmount.Text = "";
            txtPolicy_FebAmount.Text = "";
            txtPolicy_MarAmount.Text = "";
            FunEnable();
            ViewState["Policy_ID"] = "0";
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FunEnable()
    {
        try
        {
            ddlOfficeName.Enabled = true;
            ddlEmployee.Enabled = true;
            ddlPolicy_Type.Enabled = true;
            txtPolicy_No.ReadOnly = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FunDesable()
    {
        try
        {
            ddlOfficeName.Enabled = false;
            ddlEmployee.Enabled = false;
            ddlPolicy_Type.Enabled = false;
            txtPolicy_No.ReadOnly = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}