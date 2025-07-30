using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_HR_HrYearWiseEarnDed_Master : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["Emp_ID"] = Session["Emp_ID"];
            ViewState["Office_ID"] = Session["Office_ID"];
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSave.Visible = false;
        GridView1.DataSource = string.Empty;
        if(ddlYear.SelectedIndex > 0)
        {
            ds = objdb.ByProcedure("SpHREarnDeduction_YearWise", new string[] {"flag","EarnDeduction_Year" }, new string[] {"2",ddlYear.SelectedValue.ToString() }, "dataset");
            if(ds !=null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    btnSave.Visible = true;
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
            }
        }
        
    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        string Status = "0";
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
            Label lblEarnDeduction_Type = (Label)row.FindControl("lblEarnDeduction_Type");
            Label lblEarnDeduction_ID = (Label)row.FindControl("lblEarnDeduction_ID");
            Label lblEarnDeduction_Name = (Label)row.FindControl("lblEarnDeduction_Name");
            Label lblEarnDeduction_Calculation = (Label)row.FindControl("lblEarnDeduction_Calculation");
            Label lblEarnDedMaster_ID = (Label)row.FindControl("lblEarnDedMaster_ID");
            string EarnDeduction_IsActive = "0";
            if (chkSelect.Checked == true)
            {
                EarnDeduction_IsActive = "1";
                Status = "1";
            }
            if(lblEarnDedMaster_ID.Text == "0")
            {
                if (chkSelect.Checked == true)
                {
                    objdb.ByProcedure("SpHREarnDeduction_YearWise", new string[] 
                                  { "flag", 
                                    "EarnDeduction_ID",
                                    "EarnDeduction_IsActive",
                                    "EarnDeduction_Year", 
                                    "EarnDeduction_Type",
                                    "EarnDeduction_Name",
                                    "EarnDeduction_Calculation" },
                                    new string[]
                                    {"0",
                                     lblEarnDeduction_ID.Text,
                                     EarnDeduction_IsActive,
                                     ddlYear.SelectedValue.ToString(),
                                     lblEarnDeduction_Type.Text,
                                     lblEarnDeduction_Name.Text,
                                     lblEarnDeduction_Calculation.Text
                                    }, "dataset");
                }
                

               
            }
            else
            {

                objdb.ByProcedure("SpHREarnDeduction_YearWise", new string[] { "flag", "EarnDedMaster_ID", "EarnDeduction_IsActive" }, new string[] { "3", lblEarnDedMaster_ID.Text, EarnDeduction_IsActive }, "dataset");

                
            }
        }
        ddlYear_SelectedIndexChanged(sender, e);
        if (Status=="0")
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "warning!", "please select atleast one chaeckbox");
        }
        else
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Thankyou!", "operation Successfully completed");
        }
    }
}