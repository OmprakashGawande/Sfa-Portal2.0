using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_HR_RptCustomLoginTimeReport : System.Web.UI.Page
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
            string[] startwords = txtStartTime.Text.Split(':');
            string[] startwords2 = startwords[1].Split(' ');
            if (txtStartTime.Text.Contains("PM") && startwords[0] == "12")
            {
                ViewState["starthour"] = startwords[0];
            }
            if (txtStartTime.Text.Contains("PM") && startwords[0] != "12")
            {
                ViewState["starthour"] = (12 + Int32.Parse(startwords[0])).ToString();
            }
            if (txtStartTime.Text.Contains("AM") && startwords[0] == "12")
            {
                ViewState["starthour"] = "0";
            }
            if (txtStartTime.Text.Contains("AM") && startwords[0] != "12")
            {
                ViewState["starthour"] = startwords[0];
            }

            string[] endwords = txtEndTime.Text.Split(':');
            string[] endwords2 = endwords[1].Split(' ');
            if (txtEndTime.Text.Contains("PM") && endwords[0] == "12")
            {
                ViewState["endhour"] = endwords[0];
            }
            if (txtEndTime.Text.Contains("PM") && endwords[0] != "12")
            {
                ViewState["endhour"] = (12 + Int32.Parse(endwords[0])).ToString();
            }
            if (txtEndTime.Text.Contains("AM") && endwords[0] == "12")
            {
                ViewState["endhour"] = "0";
            }
            if (txtEndTime.Text.Contains("AM") && endwords[0] != "12")
            {
                ViewState["endhour"] = endwords[0];
            }
            string starttime = ViewState["starthour"].ToString() + ":" + startwords2[0];
            string endtime = ViewState["endhour"].ToString() + ":" + endwords2[0];
            TimeSpan t1 = TimeSpan.Parse(starttime);
            TimeSpan t2 = TimeSpan.Parse(endtime);
           ds = objdb.ByProcedure("RptHrCustomLoginTimeReport", new string[] { "flag", "StartTime", "EndTime", "SelectDate" }, new string[] { "0", t1.ToString(), t2.ToString(), Convert.ToDateTime(txtStartDate.Text, cult).ToString("yyyy/MM/dd")}, "dataSet");

            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
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
}