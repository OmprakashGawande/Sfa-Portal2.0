using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollEarn_DedMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["EarnDeduction_ID"] = "";
                    FillGrid();
                    lblMsg.Text = "";
                    lblRecord.Text = "";
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
            GridView1.DataSource = null;
            GridView1.DataBind();

            ds = objdb.ByProcedure("SpHRPayrollEarnDedMaster",
                new string[] { "flag" },
                new string[] { "1" }, "dataset");
            GridView1.DataSource = ds;
            GridView1.DataBind();

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
            string EarnDeduction_IsActive = "1";
           
            if (ddlEarnDeduction_Type.SelectedIndex == 0)
            {
                msg += "Select Type<br/>";
            }
            if (txtEarnDeduction_Name.Text.Trim() == "")
            {
                msg += "Enter Name<br/>";
            }
            if (ddlEarnDeduction_Calculation.SelectedIndex == 0)
            {
                msg += "Select Calculation<br/>";
            }

            if (msg.Trim() == "")
            {
               
                string Type = ddlEarnDeduction_Type.SelectedValue;
                ds = null;
                ds = objdb.ByProcedure("SpHRPayrollEarnDedMaster",
                            new string[] { "flag", "EarnDeduction_Type", "EarnDeduction_Name", "EarnDeduction_ID" },
                            new string[] { "8", Type, txtEarnDeduction_Name.Text, ViewState["EarnDeduction_ID"].ToString() }, "dataset");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string EarnDeduction_Name = ds.Tables[0].Rows[0]["EarnDeduction_Name"].ToString();
                    if (EarnDeduction_Name == txtEarnDeduction_Name.Text)
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", "This Form Name Is Already Exist.");
                    }
                }
                else
                {
                    if (btnSave.Text == "Save" && (ViewState["EarnDeduction_ID"].ToString() == "" || ViewState["EarnDeduction_ID"].ToString() == "0"))
                    {
                        objdb.ByProcedure("SpHRPayrollEarnDedMaster",
                        new string[] { "flag", "EarnDeduction_IsActive","EarnDeduction_Type", "EarnDeduction_Name", "EarnDeduction_Calculation", "EarnDeduction_UpdatedBy" },
                        new string[] { "0", EarnDeduction_IsActive,Type, txtEarnDeduction_Name.Text.ToUpper().Trim(), ddlEarnDeduction_Calculation.SelectedValue, ViewState["Emp_ID"].ToString() }, "dataset");

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");

                    }
                    if (btnSave.Text == "Edit" && ViewState["EarnDeduction_ID"].ToString() != "0")
                    {
                        objdb.ByProcedure("SpHRPayrollEarnDedMaster",
                       new string[] { "flag", "EarnDeduction_ID", "EarnDeduction_IsActive","EarnDeduction_Type", "EarnDeduction_Name", "EarnDeduction_Calculation", "EarnDeduction_UpdatedBy" },
                       new string[] { "4", ViewState["EarnDeduction_ID"].ToString(), EarnDeduction_IsActive,Type, txtEarnDeduction_Name.Text.ToUpper().Trim(), ddlEarnDeduction_Calculation.SelectedValue, ViewState["Emp_ID"].ToString() }, "dataset");
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        ViewState["EarnDeduction_ID"] = 0;
                    }
                }




                FillGridWithDDL();
                //FillGrid();
                ClearField();
                btnSave.Text = "Save";
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ClearField()
    {
        try
        {
            ddlEarnDeduction_Calculation.ClearSelection();
            txtEarnDeduction_Name.Text = "";
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
            ViewState["EarnDeduction_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpHRPayrollEarnDedMaster",
                       new string[] { "flag", "EarnDeduction_ID" },
                       new string[] { "3", ViewState["EarnDeduction_ID"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
               
                ddlEarnDeduction_Type.ClearSelection();
                ddlEarnDeduction_Type.Items.FindByValue(ds.Tables[0].Rows[0]["EarnDeduction_Type"].ToString()).Selected = true;
                txtEarnDeduction_Name.Text = ds.Tables[0].Rows[0]["EarnDeduction_Name"].ToString();
                ddlEarnDeduction_Calculation.ClearSelection();
                ddlEarnDeduction_Calculation.Items.FindByValue(ds.Tables[0].Rows[0]["EarnDeduction_Calculation"].ToString()).Selected = true;
                
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
 
    protected void ddlEarnDeduction_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            txtEarnDeduction_Name.Text = "";
            ddlEarnDeduction_Calculation.ClearSelection();
            btnSave.Text = "Save";
            FillGridWithDDL();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGridWithDDL()
    {
        try
        {
         
            string Type = ddlEarnDeduction_Type.SelectedValue;
            ds = null;
            ds = objdb.ByProcedure("SpHRPayrollEarnDedMaster",
                        new string[] { "flag","EarnDeduction_Type" },
                        new string[] { "7",Type }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblRecord.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblRecord.Text = "No Record Found";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}