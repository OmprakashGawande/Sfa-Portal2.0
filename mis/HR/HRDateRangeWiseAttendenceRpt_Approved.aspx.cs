using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_HR_HRDateRangeWiseAttendenceRpt_Approved : System.Web.UI.Page
{
    DataSet ds,ds4;
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
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillOffice();
                    FillEmployee();
                    txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
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
            ds = objdb.ByProcedure("SpHRRptDailyAttendance", new string[] { "flag", "Office_ID", "Emp_TypeOfPost" }, new string[] { "19", ViewState["Office_ID"].ToString(), ddlEmployeeType.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = ds;
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("ALL", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOffice()
    {
        try
        {
            ds = objdb.ByProcedure("SpHRRptDailyAttendance", new string[] { "flag" }, new string[] { "4" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();

                if (ViewState["Office_ID"].ToString() != "1")
                {
                    ddlOffice.Enabled = false;
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void Fillgrid()
    {
        try
        {
            lblMsg.Text = "";

            string FROMDATE = Convert.ToDateTime(txtStartDate.Text, cult).ToString("yyyy-MM-dd");
            string TODATE = Convert.ToDateTime(txtEndDate.Text, cult).ToString("yyyy-MM-dd");

            ds = objdb.ByProcedure("SpHRRpt_AllowFullDay",
               new string[] { "flag", "FromDate", "ToDate", "Office_ID", "Emp_ID", "Emp_TypeOfPost" },
               new string[] { "17", FROMDATE, TODATE, ddlOffice.SelectedValue.ToString(), ddlEmployee.SelectedValue, ddlEmployeeType.SelectedValue.ToString() }, "dataset");
            string OfficialWorkingDays = "";
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                OfficialWorkingDays += "<b style='font-size:15px; margin-right:20px;'><span style='color:blue; font-weight:bold;'> Official Working Days ";
                OfficialWorkingDays += "</span>&nbsp;&nbsp;:&nbsp;&nbsp;";
                OfficialWorkingDays += ds.Tables[0].Rows[0]["OfficialWorkingDays"].ToString();
                OfficialWorkingDays += "</b>";
                OfficialWorkingDays += "<b style='font-size:15px; margin-right:20px;'><span style='color:blue; font-weight:bold;'> Holidays ";
                OfficialWorkingDays += "</span>&nbsp;&nbsp;:&nbsp;&nbsp;";
                OfficialWorkingDays += ds.Tables[0].Rows[0]["TotalHoliDays"].ToString();
                OfficialWorkingDays += "</b>";
                
                lblMsg.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();

            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
            }
            lbltotalworkingdays.Text = OfficialWorkingDays;
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            //if (Rbtn_Type1.SelectedIndex > -1 || Rbtn_Type2.SelectedIndex > -1)
            //{
            Fillgrid();
            //}

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }



    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblEmployee.Text = "";
            lblMsgEmp.Text = "";
            string FROMDATE = Convert.ToDateTime(txtStartDate.Text, cult).ToString("yyyy-MM-dd");
            string TODATE = Convert.ToDateTime(txtEndDate.Text, cult).ToString("yyyy-MM-dd");
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                //Label lblEmp_ID = (Label)row.Cells[1].FindControl("lblEmp_ID");
                HiddenField HF_Emp_ID = (HiddenField)row.Cells[1].FindControl("HF_Emp_ID");
                Label lblEmp_Name = (Label)row.Cells[1].FindControl("lblEmp_Name");

                ViewState["Emp_ID_Att"] = HF_Emp_ID.Value;

                lblEmployee.Text = lblEmp_Name.Text + "<br /> DATE [ " + txtStartDate.Text + " - " + txtEndDate.Text + " ]";



                ds = objdb.ByProcedure("SpHRRpt_AllowFullDay",
                   new string[] { "flag", "Emp_ID", "startDate", "endDate", "Office_ID" },
                   new string[] { "18", HF_Emp_ID.Value, FROMDATE, TODATE, ddlOffice.SelectedValue.ToString() }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {                    
                    lblMsg.Text = "";
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                }
                else
                {
                    GridView2.DataSource = new string[] { };
                    GridView2.DataBind();
                }

                GridView2.UseAccessibleHeader = true;
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;


                ds4 = objdb.ByProcedure("SpHRBalanceLeaveDetail",
                   new string[] { "flag", "Emp_ID", "Financial_Year" },
                   new string[] { "17", HF_Emp_ID.Value, Convert.ToDateTime(FROMDATE, cult).ToString("yyyy") }, "dataset");
                string TotalRemainingLeaves = "";
                if (ds4 != null && ds4.Tables[0].Rows.Count > 0)
                {
                    int count = ds4.Tables[0].Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        TotalRemainingLeaves += "<b style='font-size:15px; margin-right:20px;'><span style='color:blue; font-weight:bold;'> Balance ";
                        TotalRemainingLeaves += ds4.Tables[0].Rows[i]["Leave_Type"].ToString();
                        TotalRemainingLeaves += "</span>&nbsp;&nbsp;:&nbsp;&nbsp;";
                        TotalRemainingLeaves += ds4.Tables[0].Rows[i]["TotalRemainingLeaves"].ToString();
                        TotalRemainingLeaves += "</b>";
                    }
                }

                lblAvailableLeave.Text = TotalRemainingLeaves.ToString();

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ModalAttendenceFun()", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee();
    }
}