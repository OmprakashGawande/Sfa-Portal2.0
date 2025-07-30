using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Text;


public partial class mis_HR_HRApplyLeaveByAdmin : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    StringBuilder sb = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    //spnAsterisk.Visible = false;
                    //divLeaveDay.Visible = false;
                    FillDropdown();
                    FillEmployee();
                    FillFinancialYear();
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
    protected void ClearText()
    {
        try
        {
            ddlLeaveTpye.ClearSelection();
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtLeaveRemark.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillFinancialYear()
    {
        try
        {
            ds = objdb.ByProcedure("SpHrYear_Master",
                           new string[] { "flag" },
                           new string[] { "2" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlFinancial_Year.DataTextField = "Year";
                ddlFinancial_Year.DataValueField = "Year";
                ddlFinancial_Year.DataSource = ds;
                ddlFinancial_Year.DataBind();
                ddlFinancial_Year.SelectedValue = DateTime.Now.Year.ToString();
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
            DataSet ds1 = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ViewState["Office_ID"].ToString() }, "dataset");
                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    ddlEmployee.DataSource = ds1;
                    ddlEmployee.DataTextField = "Emp_Name";
                    ddlEmployee.DataValueField = "Emp_ID";
                    ddlEmployee.DataBind();
                    ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
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
            ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag" }, new string[] { "39" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlLeaveTpye.DataSource = ds;
                ddlLeaveTpye.DataTextField = "Leave_Type";
                ddlLeaveTpye.DataValueField = "Leave_ID";
                ddlLeaveTpye.DataBind();
                ddlLeaveTpye.Items.Insert(0, new ListItem("Select", "0"));
            }

            //ds.Reset();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void InsertData()
    {
        try
        {
            string DocPath = "";
            string msg1 = "";
            if (EmpLeaveDoc.HasFile)
            {
                DocPath = "../HR/UploadDoc/LeaveDoc/" + Guid.NewGuid() + "-" + EmpLeaveDoc.FileName;
                EmpLeaveDoc.PostedFile.SaveAs(Server.MapPath(DocPath));
            }
            if (ddlLeaveTpye.SelectedItem.Text == "Medical Leave")
            {
                if (DocPath == "")
                {
                    msg1 += "कृपया सम्बंधित दस्तावेज संलग्न करें  | \\n";
                }
            }
            //if (txtFromDate.Text != txtToDate.Text)
            //{
            //  RbLeaveDays.SelectedItem.Text = "Full Day";
            // }
            string radioListValue = ddlLeaveDays.Text;
            if (txtFromDate.Text != txtToDate.Text)
            {
                ddlLeaveDays.SelectedItem.Text = "Multiple Day";
            }
            else if (txtFromDate.Text == txtToDate.Text && radioListValue == "Full Day")
            {
                ddlLeaveDays.SelectedItem.Text = "Full Day";
            }
            else if (txtFromDate.Text == txtToDate.Text && radioListValue == "First Half")
            {
                ddlLeaveDays.SelectedItem.Text = "First Half";
            }
            else if (txtFromDate.Text == txtToDate.Text && radioListValue == "Second Half")
            {
                ddlLeaveDays.SelectedItem.Text = "Second Half";
            }
            else
            {
                //ddlLeaveDays.SelectedItem.Text = "";
                ddlLeaveDays.SelectedItem.Text = "Full Day";
            }

            if (msg1 == "")
            {
                ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "Emp_ID", "Office_ID", "LeaveType", "LeaveFromDate", "LeaveToDate", "LeaveDay", "LeaveApproveAuthority", "LeaveRemark", "LeaveDocument", "LeaveStatus", "IsActive" },
                      new string[] { "0", ddlEmployee.SelectedValue.ToString(), ViewState["Office_ID"].ToString(), ddlLeaveTpye.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ddlLeaveDays.SelectedItem.Text, ViewState["Emp_ID"].ToString(), txtLeaveRemark.Text, DocPath, "Pending", "1" }, "datatset");
                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[1].Rows[0]["Status"].ToString() == "True")
                    {


                        

                        ViewState["LeaveId"] = ds.Tables[2].Rows[0]["LID"].ToString();
                        Approve_Leave();
                        lblMsg.Text = "<div class='alert alert-success alert-dismissible'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button><h4><i class='icon fa fa-check'></i> Success</h4>आपके द्वारा दिया हुआ छुट्टी का विवरण सफलता पूर्वक सुरक्षित हुआ |</div>";

                    }
                    else
                    {
                        lblMsg.Text = "<div class='alert alert-dnager alert-dismissible'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button><h4><i class='icon fa fa-check'></i> Success</h4>आपके द्वारा दिया हुआ छुट्टी का विवरण सफलता पूर्वक सुरक्षित नहीं हुआ |</div>";
                    }
                }
                ClearText();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('सम्बंधित दस्तावेज संलग्न करें |');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void applyleave_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds1;
            lblMsg.Text = "";
            string msg = "";
            int TotalDays = 0;
            int Optional = 0;
            if (ddlLeaveTpye.SelectedIndex == 0)
            {
                msg += "कृपया छुट्टी का प्रकार चुनें |<br/>";
            }
            if (txtFromDate.Text == "")
            {
                msg += "कृपया 'कब से' दिनांक चुने | <br/>";
            }
            if (txtToDate.Text == "")
            {
                msg += "कृपया 'कब तक' दिनांक चुने | <br/>";
            }

            /*********************/
            string fdate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            string ldate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            DateTime date1 = DateTime.Parse(fdate);
            DateTime date2 = DateTime.Parse(ldate);
            TotalDays = ((date2 - date1).Days);
            if (TotalDays > float.Parse(hfBalanceLeave.Value.ToString()))
            {
              msg += "इनके पास इतनी छुट्टियाँ शेष नहीं हैं  | ";

              if (ddlLeaveTpye.SelectedItem.Text != "Earned Leave" && ddlLeaveTpye.SelectedItem.Text != "Medical Leave" && ddlLeaveTpye.SelectedItem.Text != "Child Care Leave")
              {
                  if (date1.Year != int.Parse(ddlFinancial_Year.SelectedValue.ToString()) || date2.Year != int.Parse(ddlFinancial_Year.SelectedValue.ToString()))
                  {
                      msg += "From Date और To Date सेलेक्ट किए हुए वर्ष की होनी चाहिए | ";
                  }                 
              }
            }
            /*********************/

            if (msg == "")
            {
                ds1 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "Emp_ID", "LeaveFromDate", "LeaveToDate" },
                  new string[] { "22", ddlEmployee.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "datatset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    if (ddlLeaveTpye.SelectedItem.Text == "Casual Leave")
                    {
                        string d11 = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
                        string d22 = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
                        DateTime d1 = DateTime.Parse(d11);
                        DateTime d2 = DateTime.Parse(d22);
                        TotalDays = ((d2 - d1).Days);
                        TotalDays = TotalDays + 1;
                        if (TotalDays <= 8)
                        {
                            InsertData();
                        }
                        else
                        {
                            msg = "आप 8 दिनों से अधिक के लिए लगातार " + ddlLeaveTpye.SelectedItem.Text + " नहीं ले सकते";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                            txtFromDate.Text = "";
                            txtToDate.Text = "";
                        }
                    }
                    else if (ddlLeaveTpye.SelectedItem.Text == "Optional Leave")
                    {
                        if (txtFromDate.Text == txtToDate.Text)
                        {
                            DataSet ds2 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "FromDate", "ToDate", "Emp_ID" },
                new string[] { "25", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ddlEmployee.SelectedValue.ToString() }, "datatset");
                            if (ds2 != null && ds2.Tables.Count != 0)
                            {
                                if (ds2.Tables[0].Rows[0]["Status"].ToString() == "True")
                                {
                                    if (ds2.Tables[1].Rows.Count > 0)
                                    {
                                        decimal OptionalLeave = Convert.ToDecimal(ds2.Tables[1].Rows[0]["OptionalLeave"].ToString());


                                        decimal TotalOptionalAllowed = 0;
                                        if (ds2.Tables[2].Rows.Count > 0)
                                        {
                                            TotalOptionalAllowed = Convert.ToDecimal(ds2.Tables[2].Rows[0]["Leave_Days"].ToString());
                                        }

                                        if (OptionalLeave < TotalOptionalAllowed)
                                        {
                                            InsertData();
                                        }
                                        else
                                        {
                                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Your all optional Leaves have been consumed.');", true);
                                            txtFromDate.Text = "";
                                            txtToDate.Text = "";
                                        }
                                    }
                                    else
                                    {
                                        InsertData();
                                    }
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please Select Valid Date For Optional Leave');", true);
                                    txtFromDate.Text = "";
                                    txtToDate.Text = "";
                                }
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please select same date to apply optional leave');", true);
                            txtFromDate.Text = "";
                            txtToDate.Text = "";
                        }
                    }
                    else
                    {
                        InsertData();
                    }
                }
                else
                {
                    string FromDate = ds1.Tables[0].Rows[0]["LeaveFromDate"].ToString();
                    string ToDate = ds1.Tables[0].Rows[0]["LeaveToDate"].ToString();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Leave Already Applied on " + FromDate + " to " + ToDate + ".');", true);
                    txtFromDate.Text = "";
                    txtToDate.Text = "";
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

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlLeaveTpye.ClearSelection();
        lblBalanceLeave.Text = "";
    }
    protected void ddlLeaveTpye_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblBalanceLeave.Text = "";
        ds = objdb.ByProcedure("SpHRBalanceLeaveDetail", new string[] { "flag", "Emp_ID", "LeaveType_ID", "Financial_Year" },
                  new string[] { "20", ddlEmployee.SelectedValue.ToString(), ddlLeaveTpye.SelectedValue.ToString(), ddlFinancial_Year.SelectedValue.ToString() }, "datatset");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblBalanceLeave.Text = "Leave Type: <span style='color:tomato'>" + ddlLeaveTpye.SelectedItem.Text.ToString() + "</span>,   Remaining : <span style='color:tomato'>" + ds.Tables[0].Rows[0]["TotalRemainingLeaves"].ToString() + "  Day(s) Leaves</span>";
            hfBalanceLeave.Value = ds.Tables[0].Rows[0]["TotalRemainingLeaves"].ToString();

            if (decimal.Parse(ds.Tables[0].Rows[0]["TotalRemainingLeaves"].ToString()) > 0)
            {
                applyleave.Enabled = true;
            }
            else
            {
                applyleave.Enabled = false;
            }

        }

    }


    
    protected void Approve_Leave()
    {
        DataSet ds1;
        string DocPath = "";
        string OrderDate = "";
        lblMsg.Text = "";
        string msg = "";
        string TakenLeave = "";
        int DayCount = 0;
        decimal TotalTakenLeave = 0;
        if (txtOrderDate.Text != "")
        {
            OrderDate = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy/MM/dd");
        }
        
        ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveType_ID", "Financial_Year", "Emp_ID" },
            new string[] { "38", ddlLeaveTpye.SelectedValue.ToString(), ddlFinancial_Year.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString() }, "datatset");

        string financialYear = ds.Tables[0].Rows[0]["Financial_Year"].ToString();
        string LeaveDay1 = ds.Tables[0].Rows[0]["Leave_Days"].ToString();

            ds1 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId" },
        new string[] { "8", ViewState["LeaveId"].ToString() }, "datatset");
            if (ds1 != null && ds1.Tables.Count != 0)
            {
                if (ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "First Half" || ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "Second Half" || ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "Half Day")
                {
                    TakenLeave = "0.5";
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
         new string[] { "21", ViewState["LeaveId"].ToString() }, "datatset");
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
                    //TotalTakenLeave = Convert.ToDecimal(ds1.Tables[0].Rows[0]["TakenLeave"].ToString());
                }
            }


        objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "Emp_ID", "LeaveType", "TotalAllowedLeave", "TotalTakenLeave", "FinancialYear" },
        new string[] { "2",ddlEmployee.SelectedValue.ToString(), ddlLeaveTpye.SelectedValue.ToString(), LeaveDay1, TotalTakenLeave.ToString(), financialYear }, "datatset");
        if (EmpLeaveDoc.HasFile)
        {
            DocPath = "../HR/UploadDoc/LeaveApproveDoc/" + Guid.NewGuid() + "-" + EmpLeaveDoc.FileName;
            EmpLeaveDoc.PostedFile.SaveAs(Server.MapPath(DocPath));
        }
        objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId", "LeaveStatus", "RemarkByApprovalAuth", "LeaveApprovalOrderNo", "LeaveApprovalOrderDate", "LeaveApprovalOrderFile" },
        new string[] { "9", ViewState["LeaveId"].ToString(), "Approved", "", txtOrderNo.Text, OrderDate, DocPath }, "datatset");


        lblMsg.Text = "<div class='alert alert-success alert-dismissible'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button><h4><i class='icon fa fa-check'></i> Success</h4>आपके द्वारा दिया हुआ छुट्टी का विवरण सफलता पूर्वक सुरक्षित हुआ |</div>";
			
    }
}