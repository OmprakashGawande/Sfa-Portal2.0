using DocumentFormat.OpenXml.Presentation;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Daily_Task_RptEmpTaskDetail : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds, ds1;
    CultureInfo cult = new CultureInfo("gu-IN", true);
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                string currentPath = Request.Url.AbsolutePath.Substring(Request.Url.AbsolutePath.LastIndexOf("/") + 1);
                ((MainMaster)this.Master).GenerateBreadcrumb(currentPath);
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["UserTypeId"] = Session["UserTypeId"].ToString();
                ViewState["Designation_ID"] = Session["Designation_ID"].ToString();
                BindDropdown();

                div2.Visible = false;
                div1.Visible = false;
                ddlEmp.Enabled = true;
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }


    }


    public void BindDropdown()
    {
        try
        {
        
            string LoggedInRole = "";
            //if (ViewState["Designation_ID"].ToString() == "1")
            //{
            //    LoggedInRole = "Admin";
            //    RequiredFieldValidator1.Enabled = false;
            //}
            //else if (ViewState["Designation_ID"].ToString() == "8" || ViewState["Designation_ID"].ToString() == "13")
            //{
            //    LoggedInRole = "Manager";
            //    empId = ViewState["Emp_ID"].ToString();
            //}
            //else
            //{
            string empId = ViewState["Emp_ID"].ToString();
            //}

            DataSet ds3 = objdb.ByProcedure("UspGetEmpForDailyReport", new string[] { "EmpId", "LoggedInRole" }, new string[] { empId, LoggedInRole }, "dataset");

            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {

                ddlEmp.DataSource = ds3.Tables[0];
                ddlEmp.DataTextField = "Emp_Name";
                ddlEmp.DataValueField = "Emp_ID";
                ddlEmp.DataBind();


            }
            if (ds3.Tables[1].Rows[0]["Status"].ToString() == "Admin")
            {
                ddlEmp.Items.Insert(0, new ListItem("ALL", "0"));
                RequiredFieldValidator1.Enabled = false;
              
            }
            else
            {
                //ddlEmp.SelectedIndex = 0;
                ddlEmp.Items.Insert(0, new ListItem("Select", "0"));
                RequiredFieldValidator1.Enabled = true;
            }

        }
        catch (Exception ex)
        {
            // Optional: log or show error
            throw new Exception("Error while binding Type of Project dropdown: " + ex.Message);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Grid.DataSource = null;
            Grid.DataBind();
            GridView1.DataSource = null;
            GridView1.DataBind();
            div1.Visible = true;
            div2.Visible = true;

            string EmpID = "0";


            if (ddlEmp.SelectedValue != "0")
            {
                EmpID = ddlEmp.SelectedValue;
            }



            CultureInfo cult = new CultureInfo("en-GB");

            DateTime fromDateVal;
            DateTime toDateVal;

            string FromDate = !string.IsNullOrWhiteSpace(txtFromDate.Text) && DateTime.TryParse(txtFromDate.Text, cult, DateTimeStyles.None, out fromDateVal)
                ? fromDateVal.ToString("dd/MM/yyyy")
                : "";

            string ToDate = !string.IsNullOrWhiteSpace(txtToDate.Text) && DateTime.TryParse(txtToDate.Text, cult, DateTimeStyles.None, out toDateVal)
                ? toDateVal.ToString("dd/MM/yyyy")
                : "";
            DataSet ds = objdb.ByProcedure("Usp_TaskStatusReportCurrentAndPriviousWeek", new string[] { "EmpId", "FromDate", "Todate" }, new string[] { EmpID, FromDate, ToDate }, "dataset");

            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                Grid.DataSource = ds.Tables[1];  // Privious week grid 
                Grid.DataBind();
                Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                Grid.UseAccessibleHeader = true;
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0]; // current week
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }

            div2.Visible = true;
            div1.Visible = true;
        }
        catch (Exception ex) { throw ex; }

    }
}