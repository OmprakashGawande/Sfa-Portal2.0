using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HRLoginPermissionList : System.Web.UI.Page
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
            string FROMDATE = Convert.ToDateTime(txtStartDate.Text, cult).ToString("yyyy-MM-dd");
            string TODATE = Convert.ToDateTime(txtEndDate.Text, cult).ToString("yyyy-MM-dd");
            DateTime d1 = DateTime.Parse(FROMDATE);
            DateTime d2 = DateTime.Parse(TODATE);

            ds = objdb.ByProcedure("SpHRLoginPermission",
                new string[] { "flag", "Emp_ID", "FromDate", "ToDate" },
                new string[] { "3", ViewState["Emp_ID"].ToString(), FROMDATE, TODATE }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
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
            GridViewRow row = GridView1.SelectedRow;
            if (row.RowType == DataControlRowType.DataRow)
            {
                LinkButton Per_ID = (LinkButton)row.FindControl("lnkSelect");
                string PerID = Per_ID.ToolTip.ToString();
                ds = objdb.ByProcedure("SpHRLoginPermission", new string[] { "flag", "Per_ID" }, new string[] { "4", PerID.ToString() }, "dataset");
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