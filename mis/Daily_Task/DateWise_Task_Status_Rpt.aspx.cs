using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class mis_Daily_Task_DateWise_Task_Status_Rpt : System.Web.UI.Page
{
    string[] arr = { "1", "2", "3", "4", "5", "59" };
    APIProcedure objdb = new APIProcedure();
    DataSet ds, ds1, ds5, dsC, dsCU, dsphad;
    static DataTable dt = new DataTable();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {

        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!IsPostBack)
            {
                gridvew1.DataSource = null;
                gridvew1.DataBind();
                lblMsg.Text = "";
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Attributes.Add("readonly", "readonly");

                DateTime dd = DateTime.Now;
                txtFromDate.Text = (Convert.ToDateTime(dd, culture).ToString("dd/MM/yyyy"));
                txtToDate.Text = (Convert.ToDateTime(dd, culture).ToString("dd/MM/yyyy"));
                fillEmp();
                //string[] arr = {"1" , "2", "3" , "4","5","59"};
                if (arr.Contains(Session["Emp_ID"].ToString()))
                {

                }
                else
                {
                    if (ddlEmp.Items.FindByValue(Session["Emp_ID"].ToString()) != null)
                    {
                        ddlEmp.Items.FindByValue(Session["Emp_ID"].ToString()).Selected = true;
                    }
                    ddlEmp.Enabled = false;
                }
            }
        }
    }
    protected void fillEmp()
    {
        try
        {
            ddlEmp.Items.Clear();
            ds1 = objdb.ByProcedure("USP_Daily_Task_GetAllEmp", new string[] { }, new string[] { }, "dataset");
            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlEmp.DataValueField = "Emp_ID";
                        ddlEmp.DataTextField = "Emp_Name";
                        ddlEmp.DataSource = ds1;
                        ddlEmp.DataBind();
                        ddlEmp.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        ddlEmp.Items.Insert(0, new ListItem("No Record Found", "0"));
                    }
                }
                else
                {
                    ddlEmp.Items.Insert(0, new ListItem("No Record Found", "0"));
                }
            }
            else
            {
                ddlEmp.Items.Insert(0, new ListItem("No Record Found", "0"));
            }
            if (ds1 != null) ds1.Dispose();

        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7: " + ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                int res = DateTime.Compare(Convert.ToDateTime(txtFromDate.Text.Trim(), cult), Convert.ToDateTime(txtToDate.Text.Trim(), cult));
                if (res < 1)
                {
                    lblMsg.Text = "";
                    gridvew1.DataSource = null;
                    gridvew1.DataBind();
                    if (ddlEmp.SelectedValue != "0")
                    {
                        if (arr.Contains(Session["Emp_ID"].ToString()))
                        {

                        }
                        else
                        {
                            ddlEmp.ClearSelection();
                            if (ddlEmp.Items.FindByValue(Session["Emp_ID"].ToString()) != null)
                            {
                                ddlEmp.Items.FindByValue(Session["Emp_ID"].ToString()).Selected = true;
                            }
                            ddlEmp.Enabled = false;
                        }
                        fillGrid();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert(' \\n SELECT EMPLOYEE NAME')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert(' \\n TO DATE SHOULD BE GREATER THAN FROM DATE.')", true);
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", ex.Message.ToString());
        }
    }
    protected void fillGrid()
    {
        try
        {
            gridvew1.DataSource = null;
            gridvew1.DataBind();
            string fromdate = Convert.ToDateTime(txtFromDate.Text.Trim(), cult).ToString("yyyy/MM/dd");
            string todate = Convert.ToDateTime(txtToDate.Text.Trim(), cult).ToString("yyyy/MM/dd");
            ds1 = objdb.ByProcedure("USP_Daily_Task_RPT_Emp_Details_DateWise", new string[] { "FromDate", "ToDate", "EmpId" }, new string[] { fromdate, todate, ddlEmp.SelectedValue }, "dataset");
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
}