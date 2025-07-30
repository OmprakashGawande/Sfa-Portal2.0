using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollFYSalaryLedgerAdvanceSingle : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Emp_ID"] != null)
            {

                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillDropdown();

            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
    }
    protected void FillDropdown()
    {
        try
        {
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Financial_Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            }
            ds = null;
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            }
            ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
            ds = null;
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ddlOfficeName.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = ds;
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            }
            ddlEmployee.SelectedValue = ViewState["Emp_ID"].ToString();
            //if (ViewState["Emp_ID"].ToString()=="40")
            //{
            //    ddlEmployee.Enabled = true;
            //}
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
			//string selectedEmpDetail = "<div class='row'><div class='col-md-12'><div class='text-center'>// <img  style='max-width:200px;' src='../image/favicon-icon.png' class='salary-logo'><br></div></div></div>";
            string selectedEmpDetail = "<div class='row'><div class='col-md-12'><div class='text-center'><br></div></div></div>";
            selectedEmpDetail += "<div class='row'><div class='col-md-12'><p style='text-align: center; font-size:14px;'>SFA Technologies Pvt. Ltd. <br/>  SFA Tower 28-Sector A, Kasturba Nagar, Chetak Bridge, Bhopal - 462023, India</p></div></div>";
            selectedEmpDetail += "<span id='lbltotalworkingdays'>";

            selectedEmpDetail += "<b style='font-size:14px; margin-right:30px;'><span style='color:blue; font-weight:bold;'> Employee Name </span>&nbsp;&nbsp;:&nbsp;&nbsp;" + ddlEmployee.SelectedItem.Text + "</b>";
            selectedEmpDetail += "<b style='font-size:14px; margin-right:30px;'><span style='color:blue; font-weight:bold;'> Financial Year </span>&nbsp;&nbsp;: &nbsp;&nbsp;" + ddlYear.SelectedItem.Text + "</b>";
            selectedEmpDetail += "<b style='font-size:14px; margin-right:30px;'><span style='color:blue; font-weight:bold;'> Branch Name </span>&nbsp;&nbsp;:&nbsp;&nbsp;" + ddlOfficeName.SelectedItem.Text + "</b>";
            selectedEmpDetail += "<br/><b style='font-size:9px; margin-right:30px;'><span style='color:blue; font-weight:bold;'> Time </span>&nbsp;&nbsp;:&nbsp;&nbsp;" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</b></span>";
            
            //string selectedEmpDetail = "Advance Taxation Details for Financial Year  " + ddlYear.SelectedItem.Text + " , Officer/ Employee Name: " + ddlEmployee.SelectedItem.Text+"  , Office: "+ddlOfficeName.SelectedItem.Text;
            lblEmpDetail.Text=selectedEmpDetail.ToString();
            ds = objdb.ByProcedure("SpPayrollSalaryAdvanceTax_FY", new string[] { "flag", "Year", "Emp_ID" }, new string[] { "0", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                print_buttons.Visible = true;
            }
            else
            {
              
            }
        }
        catch (Exception ex)
        {
           
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    
}