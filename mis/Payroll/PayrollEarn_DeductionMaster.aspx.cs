using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollEarn_DeductionMaster : System.Web.UI.Page
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
                    DivDetail.Visible = false;
                    FillYear();
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
                ddlEarnDeduction_Year.DataTextField = "Year";
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
    protected void ddlEarnDeduction_Year_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DivDetail.Visible = false;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            if (ddlEarnDeduction_Year.SelectedIndex > 0 && ddlEarnDeduction_Type.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpPayrollEarnDedYearWiseMaster",
                new string[] { "flag", "EarnDeduction_Type", "EarnDeduction_Year" },
                new string[] { "1", ddlEarnDeduction_Type.SelectedValue.ToString(), ddlEarnDeduction_Year.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    Label header = (Label)GridView1.HeaderRow.FindControl("lblHeader");
                    header.Text = ddlEarnDeduction_Type.SelectedValue.ToString();
                    DivDetail.Visible = true;
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
            if (ddlEarnDeduction_Year.SelectedIndex == 0)
            {
                msg += "Select Year.\\n";
            }
            if (ddlEarnDeduction_Type.SelectedIndex == 0)
            {
                msg += "Select Month.\\n";
            }
            if (msg == "")
            {
                string EarnDeduction_Year = ddlEarnDeduction_Year.SelectedValue.ToString();
                string EarnDeduction_Type = ddlEarnDeduction_Type.SelectedValue.ToString();

                string LoginUserID = ViewState["Emp_ID"].ToString();
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    Label lblRowNumber = (Label)gr.FindControl("lblRowNumber");
                    Label lblEarnDeduction_Name = (Label)gr.FindControl("lblEarnDeduction_Name");
                    Label lblEarnDeduction_Calculation = (Label)gr.FindControl("lblEarnDeduction_Calculation");
                    string EarnDeduction_IsActive = "0";
                    if (chkSelect.Checked == true)
                    {
                        EarnDeduction_IsActive = "1";
                    }

                    objdb.ByProcedure("SpPayrollEarnDedYearWiseMaster",
                    new string[] { "flag", "EarnDeduction_ID", "EarnDeduction_IsActive", "EarnDeduction_Year", "EarnDeduction_Type", "EarnDeduction_Name", "EarnDeduction_Calculation", "EarnDeduction_UpdatedBy" },
                    new string[] { "0", lblRowNumber.ToolTip.ToString(), EarnDeduction_IsActive, EarnDeduction_Year, EarnDeduction_Type, lblEarnDeduction_Name.Text, lblEarnDeduction_Calculation.Text, LoginUserID }, "dataset");

                }
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");

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

}