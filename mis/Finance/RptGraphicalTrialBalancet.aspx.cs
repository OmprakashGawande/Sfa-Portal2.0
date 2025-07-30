using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_RptGraphicalTrialBalancet : System.Web.UI.Page
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
                        //string headingfirst = objdb.Decrypt(Request.QueryString["headingFirst"].ToString());
                        if (Session["headingFirst"].ToString() != null)
                        {
                            lblheadingFirst.Text = Session["headingFirst"].ToString();
                        }
                        
                            //headingfirst.ToString();
                        //if (ViewState["OfficeID"].ToString() == "0")
                        //{
                        //    ViewState["OfficeName"] = "All Offices";
                        //}
                        //else
                        //{
                        //    ds = objdb.ByProcedure("SpFinDayBookGraphicalReport", new string[] { "flag", "Office_ID" }, new string[] { "3", ViewState["OfficeID"].ToString() }, "dataset");
                        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
                        //    {
                        //        ViewState["OfficeName"] = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                        //    }
                        //}

                        ViewState["FromDate"] = objdb.Decrypt(Request.QueryString["FromDate"].ToString());
                        DateTime FDate = Convert.ToDateTime(ViewState["FromDate"].ToString());
                        //ViewState["FMonth"] = FDate.ToString("MMMM");

                        ViewState["ToDate"] = objdb.Decrypt(Request.QueryString["ToDate"].ToString());
                        DateTime TDate = Convert.ToDateTime(ViewState["ToDate"].ToString());
                        //ViewState["TMonth"] = TDate.ToString("MMMM");
                        string FY = GetCurrentFinancialYear();
                        //ViewState["Year"] = FY.ToString();
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
        ds = objdb.ByProcedure("SpFinRptGraphicalTrialBalance", new string[] { "flag", "Office_ID_Mlt", "FromDate", "ToDate" }, new string[] { "0", ViewState["OfficeID"].ToString(), Convert.ToDateTime(ViewState["FromDate"].ToString(), cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(ViewState["ToDate"].ToString(), cult).ToString("yyyy/MM/dd") }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            DataView dv = new DataView();
            dv = ds.Tables[0].DefaultView;
            dv.RowFilter = "CRDR='Cr'";
            DataTable dtCR = dv.ToTable();
            dv.RowFilter = "CRDR='Dr'";
            DataTable dtDR = dv.ToTable();
            
            StringBuilder sb = new StringBuilder();
            sb.Append("<script>");
            sb.Append("Highcharts.chart('container', {");
            sb.Append("chart: {plotBackgroundColor: null,plotBorderWidth: null,plotShadow: false, type: 'pie'},");
            sb.Append("title: { text: 'Closing Balance(In Cr)'},");
            sb.Append("tooltip: {pointFormat: '{series.name}: <b>{point.y:.2f}</b>'},");
            sb.Append("plotOptions: { pie: {allowPointSelect: true,cursor: 'pointer',dataLabels: {enabled: true,format: '<b>{point.name}</b>: {point.y:.2f}'}}},");
            sb.Append("series: [{name: 'Closing Balance',colorByPoint: true,data: [");

            int Count = dtCR.Rows.Count;
            for (int i = 0; i < Count; i ++ )
            {
                sb.Append("{name:'" + dtCR.Rows[i]["Head_Name"].ToString() + "',");
                string Amount = dtCR.Rows[i]["OpeningBalance"].ToString();               
                sb.Append("y: " + Amount.ToString() + ",");
                sb.Append("sliced: false,selected: true");
                sb.Append("},");
            }

            sb.Append("] }]});");
            
            sb.Append("</script>");
            divchartCr.InnerHtml = sb.ToString();
            
            StringBuilder sb1 = new StringBuilder();
            sb1.Append("<script>");
            sb1.Append("Highcharts.chart('container1', {");
            sb1.Append("chart: {plotBackgroundColor: null,plotBorderWidth: null,plotShadow: false, type: 'pie'},");
            sb1.Append("title: { text: 'Closing Balance(In Dr)'},");
            sb.Append("tooltip: {pointFormat: '{series.name}: <b>{point.y:.2f}</b>'},");
            sb1.Append("plotOptions: { pie: {allowPointSelect: true,cursor: 'pointer',dataLabels: {enabled: true,format: '<b>{point.name}</b>: {point.y:.2f}'}}},");
            sb1.Append("series: [{name: 'Closing Balance',colorByPoint: true,data: [");

            int Count1 = dtDR.Rows.Count;
            for (int i = 0; i < Count1; i++)
            {
                sb1.Append("{name:'" + dtDR.Rows[i]["Head_Name"].ToString() + "',");
                string Amount = dtDR.Rows[i]["OpeningBalance"].ToString();
                sb1.Append("y: " + Amount.ToString() + ",");
                sb1.Append("sliced: false,selected: true");
                sb1.Append("},");
            }

            sb1.Append("] }]});");

            sb1.Append("</script>");
            divchartDr.InnerHtml = sb1.ToString();




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