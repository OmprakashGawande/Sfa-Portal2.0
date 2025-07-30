using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;


public partial class mis_HR_HRDateRangeWiseAttendenceRpt_New : System.Web.UI.Page
{
    DataSet ds, ds2, ds3, ds4;
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
               new string[] { "15", FROMDATE, TODATE, ddlOffice.SelectedValue.ToString(), ddlEmployee.SelectedValue, ddlEmployeeType.SelectedValue.ToString() }, "dataset");
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
                   new string[] { "16", HF_Emp_ID.Value, FROMDATE, TODATE, ddlOffice.SelectedValue.ToString() }, "dataset");

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

                //lblAvailableLeave.Text = TotalRemainingLeaves.ToString();

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ModalAttendenceFun()", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GridView2.Rows)
            {
                Label lblDate = (Label)row.FindControl("lblDate");
                Label lblAllow = (Label)row.FindControl("lblAllow");
                Label lblEmpStatus = (Label)row.FindControl("lblEmpStatus");
                CheckBox chkAllow = (CheckBox)row.FindControl("chkAllow");
                //TextBox txtReason = (TextBox)row.FindControl("txtReason");
                DropDownList ddlMyQuantity = (DropDownList)row.FindControl("ddlMyQuantity");

                // ViewState["Emp_ID_Att"]
                if (chkAllow.Checked == true)
                {
                    objdb.ByProcedure("SpHRDaily_Att_Allow",
                    new string[] { "flag", "Emp_ID", "Office_ID", "Att_Date", "Allow_Status", "Reason", "Att_Type", "UpdatedBy" },
                    new string[] { "0", ViewState["Emp_ID_Att"].ToString(), ddlOffice.SelectedValue.ToString(), Convert.ToDateTime(lblDate.Text, cult).ToString("yyyy-MM-dd"), "Yes", ddlMyQuantity.SelectedValue.ToString(), lblEmpStatus.Text, ViewState["Emp_ID"].ToString() }, "dataset");



                    lblMsgEmp.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else if (lblAllow.Text == "Yes" && chkAllow.Checked == false)
                {
                    //txtReason.Text = "";
                    objdb.ByProcedure("SpHRDaily_Att_Allow",
                    new string[] { "flag", "Emp_ID", "Office_ID", "Att_Date", "Allow_Status", "Reason", "Att_Type", "UpdatedBy" },
                    new string[] { "0", ViewState["Emp_ID_Att"].ToString(), ddlOffice.SelectedValue.ToString(), Convert.ToDateTime(lblDate.Text, cult).ToString("yyyy-MM-dd"), "No", "", lblEmpStatus.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                    lblMsgEmp.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");

                }

                /**********Leave Entry On Leave Deduction********/

                if (ddlMyQuantity.SelectedValue.ToString() == "First Half")
                {
                    ApplyLeave("2", Convert.ToDateTime(lblDate.Text, cult).ToString("yyyy-MM-dd"), "First Half", "", "Leave Deducted by department due to Attendance.");
                }
                else if (ddlMyQuantity.SelectedValue.ToString() == "Second Half")
                {
                    ApplyLeave("2", Convert.ToDateTime(lblDate.Text, cult).ToString("yyyy-MM-dd"), "Second Half", "", "Leave Deducted by department due to Attendance.");
                }
                else if (ddlMyQuantity.SelectedValue.ToString() == "Late 1/3")
                {
                    ApplyLeave("2", Convert.ToDateTime(lblDate.Text, cult).ToString("yyyy-MM-dd"), "1/3 Leave", "", "Leave Deducted by department due to Attendance.");

                }
                else if (ddlMyQuantity.SelectedValue.ToString() == "Full Day")
                {
                    ApplyLeave("2", Convert.ToDateTime(lblDate.Text, cult).ToString("yyyy-MM-dd"), "Full Day", "", "Leave Deducted by department due to Attendance.");
                }
                else if (ddlMyQuantity.SelectedValue.ToString() == "Medical Leave")
                {
                    ApplyLeave("3", Convert.ToDateTime(lblDate.Text, cult).ToString("yyyy-MM-dd"), "Full Day", "", "Leave Deducted by department due to Attendance.");

                }
                else if (ddlMyQuantity.SelectedValue.ToString() == "Earned Leave")
                {
                    ApplyLeave("1", Convert.ToDateTime(lblDate.Text, cult).ToString("yyyy-MM-dd"), "Full Day", "", "Leave Deducted by department due to Attendance.");
                }
                /******************/
                /******************/

                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ModalAttendenceFun()", true);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    private void ApplyLeave(string leave_type, string fromdate, string ddlLeaveDays, string DocPath, string LeaveRemark)
    {
        try
        {
            /****************/
            /****************/
             ds3 = objdb.ByProcedure("SpHRBalanceLeaveDetail",
               new string[] { "flag", "Emp_ID", "LeaveType_ID", "Financial_Year" },
               new string[] { "18", ViewState["Emp_ID_Att"].ToString(), leave_type, Convert.ToDateTime(fromdate, cult).ToString("yyyy") }, "datatset");

             if (ds3 != null && ds3.Tables.Count != 0)
             {
                 if (ds3.Tables[0].Rows[0]["TotalRemainingLeaves"].ToString() == "Available")
                 {
                     ds2 = objdb.ByProcedure("SpHRBalanceLeaveDetail",
                        new string[] { "flag", "Emp_ID", "LeaveType_ID", "LeaveDate" },
                        new string[] { "19", ViewState["Emp_ID_Att"].ToString(), leave_type, Convert.ToDateTime(fromdate, cult).ToString("yyyy/MM/dd") }, "datatset");

                     if (ds2 != null && ds2.Tables.Count != 0)
                     {
                         if (ds2.Tables[0].Rows[0]["TotalLeave"].ToString() == "0")
                         {
                             ds = objdb.ByProcedure("SpHRLeaveApplication",
                                new string[] { "flag", "Emp_ID", "Office_ID", "LeaveType", "LeaveFromDate", "LeaveToDate", "LeaveDay", "LeaveApproveAuthority", "LeaveRemark", "LeaveDocument", "LeaveStatus", "IsActive" },
                                new string[] { "0", ViewState["Emp_ID_Att"].ToString(), ViewState["Office_ID"].ToString(), leave_type, Convert.ToDateTime(fromdate, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(fromdate, cult).ToString("yyyy/MM/dd"), ddlLeaveDays, ViewState["Emp_ID"].ToString(), LeaveRemark, DocPath, "Pending", "1" }, "datatset");
                             if (ds != null && ds.Tables.Count != 0)
                             {
                                 ApproveLeave(Convert.ToDateTime(fromdate, cult).ToString("yyyy-MM-dd"), "Approved", ds.Tables[2].Rows[0]["TypeOfLeave"].ToString(), ds.Tables[2].Rows[0]["LID"].ToString());
                             }
                         }
                     }
                 }
             }
            /*****************/
            /*****************/
        }
        catch (Exception ex)
        {

        }
    }
    private void ApproveLeave(string fromdate, string LeaveStatus, string leavetype, string LeaveId)
    {
        try
        {
            DataSet ds1;
            string DocPath = "";
            string OrderDate = "";
            string msg = "";
            string TakenLeave = "";
            int DayCount = 0;
            decimal TotalTakenLeave = 0;
            OrderDate = "";

            ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveType_ID", "Financial_Year" },
            new string[] { "7", leavetype, Convert.ToDateTime(fromdate, cult).ToString("yyyy") }, "datatset");
            string financialYear = ds.Tables[0].Rows[0]["Financial_Year"].ToString();
            string LeaveDay1 = ds.Tables[0].Rows[0]["Leave_Days"].ToString();

            if (LeaveStatus == "Approved")
            {
                ds1 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId" },
               new string[] { "8", LeaveId }, "datatset");
                if (ds1 != null && ds1.Tables.Count != 0)
                {
                    if (ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "First Half" || ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "Second Half" || ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "Half Day")
                    {
                        TakenLeave = "0.5";
                        TotalTakenLeave = Convert.ToDecimal(TakenLeave.ToString());
                    }
                    else if (ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "Late 1/3" || ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "1/3 Leave" || ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "1/3")
                    {
                        TakenLeave = "0.33";
                        TotalTakenLeave = Convert.ToDecimal(TakenLeave.ToString());
                    }
                    else
                    {
                        int count = ds1.Tables[1].Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            string DayName = ds1.Tables[1].Rows[i]["DayName"].ToString();
                            string Leave_Date = ds1.Tables[1].Rows[i]["LeaveDate"].ToString();
                            if (DayName == "Sunday")
                            {
                                DayCount = DayCount;
                            }
                            else
                            {
                                int status = 1;
                                DataSet ds2 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId" },
             new string[] { "21", LeaveId }, "datatset");
                                if (ds1 != null)
                                {
                                    int leaveCount = ds2.Tables[0].Rows.Count;
                                    if (ds2 != null && ds2.Tables[0].Rows.Count != 0)
                                    {
                                        for (int j = 0; j < leaveCount; j++)
                                        {
                                            string Holiday_Date = ds2.Tables[0].Rows[j]["Holiday_Date"].ToString();
                                            if (Leave_Date == Holiday_Date)
                                            {
                                                status = 0;
                                                break;
                                            }
                                            else
                                            {
                                                status = 1;
                                            }
                                        }
                                    }
                                    if (status == 1)
                                    {
                                        DayCount++;
                                    }
                                    else
                                    {
                                        DayCount = DayCount;
                                    }
                                }
                            }
                        }
                        TotalTakenLeave = DayCount;
                    }
                }

            }
            else
            {
                TotalTakenLeave = 0;
            }
            objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "Emp_ID", "LeaveType", "TotalAllowedLeave", "TotalTakenLeave", "FinancialYear" },
            new string[] { "2", ViewState["Emp_ID_Att"].ToString(), leavetype, LeaveDay1, TotalTakenLeave.ToString(), financialYear }, "datatset");

            objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId", "LeaveStatus", "RemarkByApprovalAuth", "LeaveApprovalOrderNo", "LeaveApprovalOrderDate", "LeaveApprovalOrderFile" },
            new string[] { "9", LeaveId, LeaveStatus, "Leave Deducted by department due to Attendance.", "", OrderDate, DocPath }, "datatset");

        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text != "&nbsp;")
            {
                DateTime T1 = DateTime.Parse(e.Row.Cells[4].Text);
                DateTime T2 = DateTime.Parse("10:00:00");
               
                if (T1.TimeOfDay > T2.TimeOfDay)
                {
                    e.Row.BackColor = Color.Orange;
                }
                
           
            }
            else if (e.Row.Cells[5].Text != "&nbsp;")
            {
                
                DateTime T3 = DateTime.Parse(e.Row.Cells[5].Text);
                DateTime T4 = DateTime.Parse("06:00:00");
                if (T3.TimeOfDay > T4.TimeOfDay)
                {
                    e.Row.BackColor = Color.Red;
                }

            }
                 
            //If Salary is less than 10000 than set the row Background Color to Cyan  
            
        }  
    }
}