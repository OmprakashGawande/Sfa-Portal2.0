using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class mis_Finance_RptGraphicalCustDayBook : System.Web.UI.Page
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
                    if (Request.QueryString["OfficeID"] != null && Request.QueryString["FromDate"] != null && Request.QueryString["ToDate"] != null)
                    {
                        ViewState["OfficeID"] = objdb.Decrypt(Request.QueryString["OfficeID"].ToString());
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
                        
                        ViewState["FromDate"] = objdb.Decrypt(Request.QueryString["FromDate"].ToString());
                        
                       
                        ViewState["ToDate"] = objdb.Decrypt(Request.QueryString["ToDate"].ToString());                      
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
        ds = objdb.ByProcedure("SpFinDayBookGraphicalReport", new string[] { "flag", "Office_ID", "FromDate", "ToDate" }, new string[] { "0", ViewState["OfficeID"].ToString(), Convert.ToDateTime(ViewState["FromDate"].ToString(), cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(ViewState["ToDate"].ToString(), cult).ToString("yyyy/MM/dd") }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            string name2 = "";
            StringBuilder sb = new StringBuilder();
            sb.Append("<script>");
            sb.Append("Highcharts.chart('container', {");
            sb.Append("chart: {type: 'line'},");
            sb.Append("title: {text: 'Graphical Representation Of Custom DayBook From (" + ViewState["FromDate"].ToString() + " - " + ViewState["ToDate"].ToString() + ")<br>(" + ViewState["OfficeName"].ToString() + ")'},");
           // sb.Append("subtitle: {text: '(" + ViewState["OfficeName"].ToString() + ")'},");
            //sb.Append("subtitle: {text: '(" + ViewState["OfficeName"].ToString() + ")'},");
            sb.Append("xAxis: {categories: [");
            int count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {

                sb.Append("'" + ds.Tables[0].Rows[i]["MonthName"].ToString() + "',");

            }
            sb.Append("]");
            sb.Append("},");
            sb.Append("yAxis: {title: {text: 'No of Voucher' }},");
            sb.Append("plotOptions: {line: {dataLabels: { enabled: true}, enableMouseTracking: false}},");
            sb.Append("series: [");
            int countT = ds.Tables[1].Rows.Count;
            for (int i = 0; i < countT; i++)
            {
                string name1 = ds.Tables[1].Rows[i]["VoucherType"].ToString();

                if (name1 != name2)
                {
                    if (i > 1)
                    {
                        sb.Append("]},");
                    }
                    sb.Append("{name: '" + name1.ToString() + "',");
                    sb.Append("data: [");
                    sb.Append("" + ds.Tables[1].Rows[i]["TotalVoucher"].ToString() + ",");
                }
                else
                {

                    sb.Append("" + ds.Tables[1].Rows[i]["TotalVoucher"].ToString() + ",");
                    if (i == (countT - 1))
                    {
                        sb.Append("]}]");
                    }
                }
                name2 = ds.Tables[1].Rows[i]["VoucherType"].ToString();

            }
            sb.Append("});");
            sb.Append("</script>");
            divchart.InnerHtml = sb.ToString();



        }
    }
    public static string GetCurrentFinancialYear()
    {
        int CurrentYear = DateTime.Today.Year;
        int PreviousYear = DateTime.Today.Year - 1;
        int NextYear = DateTime.Today.Year + 1;
        string PreYear = PreviousYear.ToString();
        string NexYear = NextYear.ToString();
        string CurYear = CurrentYear.ToString();
        string FinYear = null;

        if (DateTime.Today.Month > 3)
            FinYear = CurYear + "-" + NexYear;
        else
            FinYear = PreYear + "-" + CurYear;
        return FinYear.Trim();
    }
   
}