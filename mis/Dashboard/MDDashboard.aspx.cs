using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class mis_Dashboard_MDDashboard : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillDetail();

                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDetail()
    {
        try
        {
            ds = null;

            ds = objdb.ByProcedure("SpDashboard", new string[] { "flag" }, new string[] { "3" }, "dataset");
            
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblTotalEmp.Text = ds.Tables[0].Rows[0]["TotalEmp"].ToString();
                    lblTotalPresent.Text = ds.Tables[0].Rows[0]["TotalPresent"].ToString();
                    lblTotalOnLeave.Text = ds.Tables[0].Rows[0]["TotalOnLeave"].ToString();
                    lblTotalAbsent.Text = ds.Tables[0].Rows[0]["TotalAbsent"].ToString();
                    lblToDay.Text = ds.Tables[0].Rows[0]["ToDay"].ToString();
                    //lblTomorrow.Text = ds.Tables[0].Rows[0]["Tomorrow"].ToString();
                    lblDepEnq.Text = ds.Tables[0].Rows[0]["DepEnq"].ToString();
                    lblBirthday.Text = ds.Tables[0].Rows[0]["Birthday"].ToString();
                    lblFileOnDes.Text = ds.Tables[0].Rows[0]["FileOnDesk"].ToString();

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    RepeaterTodayMeeting.DataSource = ds.Tables[1];
                    RepeaterTodayMeeting.DataBind();
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    RepeaterTomorrowMeeting.DataSource = ds.Tables[2];
                    RepeaterTomorrowMeeting.DataBind();
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    lblPendingRTI.Text = ds.Tables[3].Rows[0]["PendingRTI"].ToString();
                    lblPendingFirstAppeal.Text = ds.Tables[3].Rows[0]["PendingFirstAppeal"].ToString();
                    lblPendingGrvCount.Text = ds.Tables[3].Rows[0]["PendingGrvCount"].ToString();
                    lblLegalCount.Text = ds.Tables[3].Rows[0]["LegalCount"].ToString();

                }
                if (ds.Tables[4].Rows.Count > 0)
                {
                    GridViewDirectory.DataSource = ds.Tables[4];
                    GridViewDirectory.DataBind();
                }

                if (ds.Tables[5].Rows.Count > 0)
                {
                    GridViewRetirement.DataSource = ds.Tables[5];
                    GridViewRetirement.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}