using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_Dashboard_Home : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    IFormatProvider culture = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["Emp_ID"] != null)
            {

                string[] arr = { "1", "2", "3", "4", "5", "59" };
                if (arr.Contains(Session["Emp_ID"].ToString()))
                {
                    //aLeave.HRef = "../HR/HREmpWiseLeaveDetail.aspx";
                    // aPendingLeave.HRef = "../HR/HREmpLeaveRequests.aspx";
                    divTask.Visible = false;
                }
                else
                {
                    //aLeave.HRef = "";
                    // aPendingLeave.HRef = "";
                    divTask.Visible = true;
                    fillGrid();
                }
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillDetail();

                }
                GetTotalResourcesBenchandProjects();
                GetTotalDailytasks();
                GetTotalTotaltaskAllocatedbySeniors();
                string PreviousDate = DateTime.Now.Date.AddDays(-1).ToString("dd-MM-yyyy");
                lblPreviousdate.Text = PreviousDate;
                lblDate2.Text = PreviousDate;




            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }

        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillDetail()
    {
        try
        {
            ds = null;
            GridViewHoliday.DataSource = null;
            GridViewHoliday.DataBind();
            ds = objdb.ByProcedure("Sp_DashboardAllEmployee", new string[] { "flag", "emp_ID" }, new string[] { "0", ViewState["Emp_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //lblOtherPendingLeave.Text = ds.Tables[0].Rows[0]["OtherPendingLeave"].ToString();
                    //lblMyPendingLeave.Text = ds.Tables[0].Rows[0]["MyPendingLeave"].ToString();
                }
                if (ds.Tables[4].Rows.Count > 0)
                {
                    ///lblSalaryMonth.Text = ds.Tables[4].Rows[0]["Salary_Month"].ToString();
                    ///lblSalaryYear.Text = ds.Tables[4].Rows[0]["Salary_Year"].ToString();
                    ///lblTotalEarnings.Text = ds.Tables[4].Rows[0]["Salary_EarningTotal"].ToString();
                    ///lblTotalDeductions.Text = ds.Tables[4].Rows[0]["Salary_DeductionTotal"].ToString();
                    ///lblNetSalary.Text = ds.Tables[4].Rows[0]["Salary_NetSalary"].ToString();
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    GridViewBirth.DataSource = ds.Tables[3];
                    GridViewBirth.DataBind();
                }
                if (ds.Tables[5].Rows.Count > 0)
                {
                    GridViewHoliday.DataSource = ds.Tables[5];
                    GridViewHoliday.DataBind();
                }
                if (ds.Tables[6].Rows.Count > 0)
                {
                    lblReportStatus.Text = ds.Tables[6].Rows[0]["TaskSts"].ToString();
                }

            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewBirth_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDOB_Day = (Label)e.Row.FindControl("lblDOB_Day");
            Image imgNew = (Image)e.Row.FindControl("ImgNew");
            imgNew.Visible = false;
            //DateTime bdate = Convert.ToDateTime(effectiveDat.Text, culture);
            //DateTime CurrentDt = Convert.ToDateTime(DateTime.Now.ToString(), culture);
            //int res = DateTime.Compare(bdate, CurrentDt);
            if (lblDOB_Day.Text.Trim() == DateTime.Now.Day.ToString().Trim())
            {
                imgNew.Visible = true;
                imgNew.ImageUrl = "../image/Happy-Birthday-1.gif";
            }
        }
    }


    protected void fillGrid()
    {
        try
        {
            DataSet ds1 = new DataSet();
            gridvew1.DataSource = null;
            gridvew1.DataBind();
            //string fromdate = DateTime.Now.ToString("yyyy/MM/dd");
            //string todate = DateTime.Now.AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd");
            DateTime now = DateTime.Now;
            var NewDate = new DateTime(now.Year, now.Month, 1);
            string fromdate = NewDate.ToString("yyyy/MM/dd");
            string todate = NewDate.AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd");
            ds1 = objdb.ByProcedure("USP_Daily_Task_RPT_Emp_Details_DateWise", new string[] { "FromDate", "ToDate", "EmpId" }, new string[] { fromdate, todate, Session["Emp_ID"].ToString() }, "dataset");
            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        gridvew1.DataSource = ds1.Tables[0];
                        gridvew1.DataBind();
                    }

                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", ex.Message.ToString());
        }

    }
    protected void fillTaskGrid(string TaskId)
    {
        try
        {
            DataSet ds1 = new DataSet();
            gridEmpTask.DataSource = null;
            gridEmpTask.DataBind();
            ds1 = objdb.ByProcedure("USP_Daliy_Task_Child_GetBy_Task_Id", new string[] { "Task_Id" }, new string[] { TaskId }, "dataset");
            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        gridEmpTask.DataSource = ds1.Tables[0];
                        gridEmpTask.DataBind();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#ViewDetails').modal('show');", true);
                    }

                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", ex.Message.ToString());
        }

    }
    protected void gridvew1_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            if (e.CommandName == "ViewDetails")
            {
                fillTaskGrid(e.CommandArgument.ToString());
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 6 : ", ex.Message.ToString());
        }
    }

    protected void GetTotalResourcesBenchandProjects()
    {
        try
        {

            string empId = ViewState["Emp_ID"].ToString();
            DataSet ds = objdb.ByProcedure("Usp_GetTotalResourcesBenchandProjects", new string[] { "EmpId" }, new string[] { empId }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblbanch.Text = ds.Tables[0].Rows[0]["TotalResourcesOnBench"].ToString();
                lblProjects.Text = ds.Tables[0].Rows[0]["TotalResourcesOnProjects"].ToString();
                if (ds.Tables[1].Rows[0]["RoleName"].ToString() == "Emp")
                {
                    Div_ResourcesOnProjects.Visible = false;
                    Div_ResourcesOnBench.Visible = false;
                    Div_DailyTask.Visible = true;
                }
                else if (ds.Tables[1].Rows[0]["RoleName"].ToString() == "TeamLead" || ds.Tables[1].Rows[0]["RoleName"].ToString() == "Manager")
                {
                    Div_ResourcesOnProjects.Visible = true;
                    Div_ResourcesOnBench.Visible = true;
                    Div_DailyTask.Visible = true;
                }
                else
                {
                    Div_ResourcesOnProjects.Visible = true;
                    Div_ResourcesOnBench.Visible = true;
                    Div_DailyTask.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    protected void GetTotalDailytasks()
    {
        try
        {

            string empId = ViewState["Emp_ID"].ToString();
            DataSet ds = objdb.ByProcedure("Usp_GetTotalDailyTaskFilledorNot", new string[] { "EmpId" }, new string[] { empId }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblTotalFilled.Text = ds.Tables[0].Rows[0]["TotalFilled"].ToString();
                lblTotalNotFilled.Text = ds.Tables[0].Rows[0]["TotalNotFilled"].ToString();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    protected void GetTotalTotaltaskAllocatedbySeniors()
    {
        try
        {

            string empId = ViewState["Emp_ID"].ToString();
            DataSet ds = objdb.ByProcedure("Usp_GetTotaltaskAllocatedbySeniors", new string[] { "EmpId" }, new string[] { empId }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblTotaltaskAllocated.Text = ds.Tables[0].Rows[0]["TotalTasksAssignedThisWeek"].ToString();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }


    protected void lblbanch_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objdb.ByProcedure("Usp_GetMainPowerRequest", new string[] { "EmpId" }, new string[] { "0" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridonBench.DataSource = ds.Tables[0];
                GridonBench.DataBind();
                GridonBench.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridonBench.UseAccessibleHeader = true;

                string script = "var myModal = new bootstrap.Modal(document.getElementById('OnBenchModal')); myModal.show();";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script, true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#OnBenchModal').modal('show');", true);
            }
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                GridOnProject.DataSource = ds.Tables[1];
                GridOnProject.DataBind();
            }

        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void lblProjects_Click(object sender, EventArgs e)
    {
        try
        {
            GridOnProject.DataSource = null;
            GridOnProject.DataBind();
            string empId = ViewState["Emp_ID"].ToString();
            DataSet ds = objdb.ByProcedure("Usp_GetMainPowerRequest", new string[] { "EmpId" }, new string[] { empId }, "dataset");

            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                GridOnProject.DataSource = ds.Tables[1];
                GridOnProject.DataBind();

                GridOnProject.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridOnProject.UseAccessibleHeader = true;
                string script = "var myModal = new bootstrap.Modal(document.getElementById('OnProjectModal')); myModal.show();";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script, true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#OnProjectModal').modal('show');", true);
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void lblTotalFilled_Click(object sender, EventArgs e)
    {
        try
        {
            GridTaskFilled.DataSource = null;
            GridTaskFilled.DataBind();
            string empId = ViewState["Emp_ID"].ToString();
            DataSet ds = objdb.ByProcedure("Usp_GetTotalDailyTaskFilledorNot", new string[] { "EmpId" }, new string[] { empId }, "dataset");

            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                GridTaskFilled.DataSource = ds.Tables[1];
                GridTaskFilled.DataBind();

                GridTaskFilled.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridTaskFilled.UseAccessibleHeader = true;

                string script = "var myModal = new bootstrap.Modal(document.getElementById('TaskFilledModal')); myModal.show();";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script, true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#TaskFilledModal').modal('show');", true);
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void lblTotalNotFilled_Click(object sender, EventArgs e)
    {
        try
        {
            GridTaskNotFilled.DataSource = null;
            GridTaskNotFilled.DataBind();
            string empId = ViewState["Emp_ID"].ToString();
            DataSet ds = objdb.ByProcedure("Usp_GetTotalDailyTaskFilledorNot", new string[] { "EmpId" }, new string[] { empId }, "dataset");

            if (ds != null && ds.Tables[2].Rows.Count > 0)
            {
                GridTaskNotFilled.DataSource = ds.Tables[2];
                GridTaskNotFilled.DataBind();

                GridTaskNotFilled.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridTaskNotFilled.UseAccessibleHeader = true;
                string script = "var myModal = new bootstrap.Modal(document.getElementById('TaskNotFilledModal')); myModal.show();";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script, true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#TaskNotFilledModal').modal('show');", true);
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void lblTotaltaskAllocated_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/mis/Report/RptTaskAllocationStatics.aspx");
    }

    protected void lblMyPendingLeave_Click(object sender, EventArgs e)
    {
        Response.Redirect("../HR/HREmpWiseLeaveDetail.aspx");
    }

    protected void lblOtherPendingLeave_Click(object sender, EventArgs e)
    {
        Response.Redirect("../HR/HREmpLeaveRequests.aspx");
    }
}