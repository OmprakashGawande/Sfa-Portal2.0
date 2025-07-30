using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_RptGraphicalDayBook : System.Web.UI.Page
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
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    if (Request.QueryString["OfficeID"] != null && Request.QueryString["Date"] != null)
                    {
                        ViewState["OfficeID"] = objdb.Decrypt(Request.QueryString["OfficeID"].ToString());
                        ViewState["Date"] = objdb.Decrypt(Request.QueryString["Date"].ToString());
                        if (ViewState["OfficeID"].ToString() == "0")
                        {
                            ViewState["OfficeName"] = "All Offices";
                        }
                        else
                        {
                            ds = objdb.ByProcedure("SpFinDayBookGraphicalReport", new string[] { "flag", "Office_ID" }, new string[] { "3", ViewState["OfficeID"].ToString() }, "dataset");
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                ViewState["OfficeName"] = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                            }
                        }
                        //DateTime date = Convert.ToDateTime(ViewState["Date"].ToString());
                        //date = Convert.ToDateTime(date, cult).ToString("dd/MM/yyyy");
                        hfoffice.Value = ViewState["OfficeName"].ToString();
                        hfDate.Value = ViewState["Date"].ToString();
                        FillChart();

                    }
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
    protected void FillChart()
    {
        ds = objdb.ByProcedure("SpFinDayBookGraphicalReport", new string[] { "flag", "Office_ID", "Date" }, new string[] { "2", ViewState["OfficeID"].ToString(), Convert.ToDateTime(ViewState["Date"].ToString(), cult).ToString("yyyy/MM/dd")}, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='datatable'>");
            sb.Append("<thead'>");
            sb.Append("<tr>");
            sb.Append("<th></th>");
            sb.Append("<th>Total Vouchers</th>");
            sb.Append("</tr>");
            sb.Append("</thead'>");
            sb.Append("<tbody>");
            int count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                sb.Append("<tr>");
                sb.Append("<th>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</th>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["TotalVoucher"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            divchart.InnerHtml = sb.ToString();


        }
    }
   
}