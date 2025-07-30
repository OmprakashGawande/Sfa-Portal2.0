using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HRDateWiseAttendenceReport : System.Web.UI.Page
{
    DataSet ds;
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
                    string CurrentDate = DateTime.Now.ToString("dd/MM/yyy");
                    txtDate.Text = CurrentDate.ToString();
                    Fillgrid();
                }
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
            ds = objdb.ByProcedure("SpHRRptDailyAttendance", new string[] { "flag" }, new string[] { "4"}, "dataset");
            if(ds!= null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                ddlOffice.Enabled = false;
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
            string DATE = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy-MM-dd");           

            ds = objdb.ByProcedure("SpHRRptDailyAttendance",
                new string[] { "flag", "SDate", "Office_ID","EmpStatus" },
                new string[] { "6", DATE, ddlOffice.SelectedValue.ToString(), ddlEmpStatus.SelectedValue.ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
                if(ds.Tables[1].Rows[0]["TotalPresent"].ToString() != "")
                {
                    lbltotalpresent.Text = ds.Tables[1].Rows[0]["TotalPresent"].ToString();
                }
                else
                {
                    lbltotalpresent.Text = "0";
                }
                if (ds.Tables[2].Rows[0]["TotalAbsent"].ToString() != "")
                {
                    lbltotalabsent.Text = ds.Tables[2].Rows[0]["TotalAbsent"].ToString();
                }
                else
                {
                    lbltotalabsent.Text = "0";
                }

            }
            else
            {               
                GridView1.DataSource = new string[]{};
                GridView1.DataBind();
            }
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
            Fillgrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            string Per_ID = GridView1.SelectedDataKey.Value.ToString();
            ViewState["Per_ID"] = Per_ID.ToString(); 
            ds = objdb.ByProcedure("SpHRRptDailyAttendance", new string[] { "flag", "Per_ID" }, new string[] { "7", Per_ID.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DetailsView1.DataSource = ds.Tables[0];
                DetailsView1.DataBind();             
                ddlStatus.ClearSelection();
                ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["PermissionStatus"].ToString()).Selected = true;
                txtHRRemark.Text = ds.Tables[0].Rows[0]["HR_Remark"].ToString();
                ddlStatus.Enabled = false;
                txtHRRemark.Enabled = false;
               
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

   
}