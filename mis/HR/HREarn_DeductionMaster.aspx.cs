using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_HR_HREarningAndDecductionMaster : System.Web.UI.Page
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
                    FillYear();
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
    protected void FillYear()
    {
        try
        {
            DataSet dsM = objdb.ByProcedure("SpHrYear_Master",
                 new string[] { "flag" },
                 new string[] { "2" }, "dataset");

            if (dsM != null && dsM.Tables[0].Rows.Count > 0)
            {
                ddlEarnDeduction_Year.DataSource = dsM;
                ddlEarnDeduction_Year.DataTextField = "Financial_Year";
                ddlEarnDeduction_Year.DataValueField = "Year";
                ddlEarnDeduction_Year.DataBind();
                ddlEarnDeduction_Year.Items.Insert(0, new ListItem("Select", "0"));
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
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            ds = objdb.ByProcedure("SpHREarnDeduction",
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
            if (ddlEarnDeduction_Year.SelectedIndex == 0) 
            {
                msg += "Select Year<br/>";
            }
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
                //if (chkEarnDeduction_IsActive.Checked)
                //{
                //    EarnDeduction_IsActive = "1";
                //}
                //else
                //{
                //    EarnDeduction_IsActive = "0";
                //}
                string Year = ddlEarnDeduction_Year.SelectedValue;
                string Type = ddlEarnDeduction_Type.SelectedValue;
                ds = null;
                ds = objdb.ByProcedure("SpHREarnDeduction",
                            new string[] { "flag", "EarnDeduction_Year", "EarnDeduction_Type", "EarnDeduction_Name", "EarnDeduction_ID" },
                            new string[] { "8", Year, Type, txtEarnDeduction_Name.Text, ViewState["EarnDeduction_ID"].ToString() }, "dataset");

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
                        objdb.ByProcedure("SpHREarnDeduction",
                        new string[] { "flag", "EarnDeduction_IsActive", "EarnDeduction_Year", "EarnDeduction_Type", "EarnDeduction_Name", "EarnDeduction_Calculation", "EarnDeduction_UpdatedBy" },
                        new string[] { "0", EarnDeduction_IsActive, Year, Type, txtEarnDeduction_Name.Text.ToUpper().Trim(), ddlEarnDeduction_Calculation.SelectedValue, ViewState["Emp_ID"].ToString() }, "dataset");

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");

                    }
                     if (btnSave.Text == "Edit" && ViewState["EarnDeduction_ID"].ToString() != "0")
                {
                    objdb.ByProcedure("SpHREarnDeduction",
                   new string[] { "flag", "EarnDeduction_ID", "EarnDeduction_IsActive", "EarnDeduction_Year", "EarnDeduction_Type", "EarnDeduction_Name", "EarnDeduction_Calculation", "EarnDeduction_UpdatedBy" },
                   new string[] { "4", ViewState["EarnDeduction_ID"].ToString(), EarnDeduction_IsActive, Year, Type, txtEarnDeduction_Name.Text.ToUpper().Trim(), ddlEarnDeduction_Calculation.SelectedValue, ViewState["Emp_ID"].ToString() }, "dataset");
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
              //  lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", msg);
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
            // ddlEarnDeduction_Type.ClearSelection(); 
            // ddlEarnDeduction_Year.ClearSelection();
            txtEarnDeduction_Name.Text = "";
            // chkEarnDeduction_IsActive.Checked = true;
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
            ds = objdb.ByProcedure("SpHREarnDeduction",
                       new string[] { "flag", "EarnDeduction_ID" },
                       new string[] { "3", ViewState["EarnDeduction_ID"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlEarnDeduction_Year.ClearSelection();
                ddlEarnDeduction_Year.Items.FindByValue(ds.Tables[0].Rows[0]["EarnDeduction_Year"].ToString()).Selected = true;
                ddlEarnDeduction_Type.ClearSelection();
                ddlEarnDeduction_Type.Items.FindByValue(ds.Tables[0].Rows[0]["EarnDeduction_Type"].ToString()).Selected = true;
                txtEarnDeduction_Name.Text = ds.Tables[0].Rows[0]["EarnDeduction_Name"].ToString();
                ddlEarnDeduction_Calculation.ClearSelection();
                ddlEarnDeduction_Calculation.Items.FindByValue(ds.Tables[0].Rows[0]["EarnDeduction_Calculation"].ToString()).Selected = true;
                //string chk = ds.Tables[0].Rows[0]["EarnDeduction_IsActive"].ToString();
                //if(chk == "1")
                //{
                //    chkEarnDeduction_IsActive.Checked = true;
                //}
                //else
                //{
                //    chkEarnDeduction_IsActive.Checked = false;
                //}
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
    //        CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
    //        string EarnDeduction_ID = chk.ToolTip.ToString();
    //        string EarnDeduction_IsActive = "0";
    //        if (chk != null & chk.Checked)
    //        {
    //            EarnDeduction_IsActive = "1";
    //        }
    //        objdb.ByProcedure("SpHREarnDeduction",
    //                   new string[] { "flag", "EarnDeduction_IsActive", "EarnDeduction_ID", "Form_UpdatedBy" },
    //                   new string[] { "5", EarnDeduction_IsActive, EarnDeduction_ID, ViewState["Emp_ID"].ToString() }, "dataset");
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void ddlEarnDeduction_Year_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            txtEarnDeduction_Name.Text = "";
            ddlEarnDeduction_Calculation.ClearSelection();
            btnSave.Text = "Save";
            string Year = ddlEarnDeduction_Year.SelectedValue;
            string Type = ddlEarnDeduction_Type.SelectedValue;
            ds = null;
            if (ddlEarnDeduction_Type.SelectedIndex <= 0)
            {
                ds = objdb.ByProcedure("SpHREarnDeduction",
                       new string[] { "flag", "EarnDeduction_Year" },
                       new string[] { "6", Year }, "dataset");
            }
            else
            {
                ds = objdb.ByProcedure("SpHREarnDeduction",
                        new string[] { "flag", "EarnDeduction_Year", "EarnDeduction_Type" },
                        new string[] { "7", Year, Type }, "dataset");
            }


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
            string Year = ddlEarnDeduction_Year.SelectedValue;
            string Type = ddlEarnDeduction_Type.SelectedValue;
            ds = null;
            ds = objdb.ByProcedure("SpHREarnDeduction",
                        new string[] { "flag", "EarnDeduction_Year", "EarnDeduction_Type" },
                        new string[] { "7", Year, Type }, "dataset");

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