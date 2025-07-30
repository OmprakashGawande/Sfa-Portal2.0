using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HRLoginPermissionListForApproval : System.Web.UI.Page
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
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            string FROMDATE = Convert.ToDateTime(txtStartDate.Text, cult).ToString("yyyy-MM-dd");
            string TODATE = Convert.ToDateTime(txtEndDate.Text, cult).ToString("yyyy-MM-dd");
            DateTime d1 = DateTime.Parse(FROMDATE);
            DateTime d2 = DateTime.Parse(TODATE);

            ds = objdb.ByProcedure("SpHRLoginPermission",
                new string[] { "flag", "FromDate", "ToDate" },
                new string[] { "10", FROMDATE, TODATE }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = new string[] { };
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
            string FROMDATE = Convert.ToDateTime(txtStartDate.Text, cult).ToString("yyyy-MM-dd");
            string TODATE = Convert.ToDateTime(txtEndDate.Text, cult).ToString("yyyy-MM-dd");
            DateTime d1 = DateTime.Parse(FROMDATE);
            DateTime d2 = DateTime.Parse(TODATE);
            string Per_ID = GridView1.SelectedDataKey.Value.ToString();
            ViewState["Per_ID"] = Per_ID.ToString();
            ds = objdb.ByProcedure("SpHRLoginPermission", new string[] { "flag", "Per_ID", "FromDate", "ToDate" }, new string[] { "11", Per_ID.ToString(), FROMDATE, TODATE }, "dataset");
            if(ds!= null && ds.Tables[0].Rows.Count > 0)
             {
                 DetailsView1.DataSource = ds.Tables[0];
                 DetailsView1.DataBind();
                 if (ds.Tables[0].Rows[0]["PermissionStatus"].ToString() == "Pending")
                {
                    btnApprove.Visible = true;
                    ddlStatus.ClearSelection();
                    txtHRRemark.Text = "";
                    ddlStatus.Enabled = true;
                    txtHRRemark.Enabled = true;
                }
                else
                {
                    btnApprove.Visible = false;
                    ddlStatus.ClearSelection();
                    ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["PermissionStatus"].ToString()).Selected = true;
                    txtHRRemark.Text = ds.Tables[0].Rows[0]["HR_Remark"].ToString();
                    ddlStatus.Enabled = false;
                    txtHRRemark.Enabled = false;   
                }
             }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
        }
         catch (Exception ex)
         {
             lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
         }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            if(ddlStatus.SelectedIndex ==0)
            {
                msg = "Select Status.";
            }
            if(msg == "")
            {
                objdb.ByProcedure("SpHRLoginPermission", new string[] { "flag", "Per_ID", "Status", "HR_Remark", "ApprovedBy" }, new string[] { "12", ViewState["Per_ID"].ToString(), ddlStatus.SelectedValue.ToString(), txtHRRemark.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                Fillgrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}