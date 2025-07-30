using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

public partial class mis_Finance_RptMisProgressiveOverAll : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillDropdown();
                    //FillGrid1();
                   
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }

        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    protected void FillDropdown()
    {
        try
        {
            lblMsg.Text = "";
            DataSet dsYear = objdb.ByProcedure("SpFinMisProgressiveNew", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (dsYear != null)
            {
                ddlYear.DataSource = dsYear;
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
           
           
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year";
            }
            if (ddlMonth.SelectedIndex == 0)
            {
                msg += "Select Month";
            }
            if(msg == "")
            {
                FillGrid();
                //CreateChart();
            }
            else
            {

            }
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
            ds = objdb.ByProcedure("SpFinMisProgressiveNew", new string[] { "flag", "Year", "Month_ID" }, new string[] { "14", ddlYear.SelectedValue, ddlMonth.SelectedValue }, "dataset");
            if(ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                string FromDate = "";
                string Todate = "";
                if (Convert.ToInt32(ddlMonth.SelectedValue) < 4)
                {
                    FromDate = "01/04/" + (Convert.ToInt32(ddlYear.SelectedValue) - 1).ToString();
                    Todate = "" + DateTime.DaysInMonth((Convert.ToInt32(ddlYear.SelectedValue)), (Convert.ToInt32(ddlMonth.SelectedValue))) + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue;

                }
                else
                {
                    FromDate = "01/04/" + (Convert.ToInt32(ddlYear.SelectedValue)).ToString();
                    Todate = "" + DateTime.DaysInMonth((Convert.ToInt32(ddlYear.SelectedValue)), (Convert.ToInt32(ddlMonth.SelectedValue))) + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue;
                }

                lblheading.Text = "M.P. STATE AGRO INDUSTRIES DEVELOPENT CORPORATION LTD., BHOPAL <br/> MIS REPORT FROM " + FromDate + " TO " + Todate + " (PROGRESSIVE)";


                int count = GridView1.Rows.Count;
               
                for(int i = 0;i < count;i++)
                {
                    decimal GrandLYearTotal = 0;
                    decimal GrandCYearTotal = 0;
                    int ColumnCount = GridView1.Rows[i].Cells.Count;
                    for(int j=0;j< ColumnCount;j++)
                    {
                        string LYear = GridView1.HeaderRow.Cells[j].Text;
                        if (LYear.EndsWith("( LY)"))
                        {
                            GrandLYearTotal += decimal.Parse(GridView1.Rows[i].Cells[j].Text);
                        }
                        GridView1.Rows[i].Cells[ColumnCount-2].Text = GrandLYearTotal.ToString();
                        if (LYear.EndsWith("(CY)"))
                        {
                            GrandCYearTotal += decimal.Parse(GridView1.Rows[i].Cells[j].Text);
                        }
                        GridView1.Rows[i].Cells[ColumnCount - 1].Text = GrandCYearTotal.ToString();
                        
                    }
                   
                }

            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
            }
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                CreateChart2(ds);
            }
            if (ds != null && ds.Tables[2].Rows.Count > 0)
            {
                CreateChart(ds);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

   protected void CreateChart(DataSet ds)
   {
       try
       {
           //ds = objdb.ByProcedure("SpFinMisProgressiveNew", new string[] { "flag", "Division_ID", "Year", "Month_ID" }, new string[] { "12", ddlOffice.SelectedValue, ddlYear.SelectedValue, ddlMonth.SelectedValue }, "dataset");
           if (ds != null && ds.Tables[2].Rows.Count > 0)
           {
               /***********************/
               StringBuilder sb = new StringBuilder();
               sb.Append("<script>");
               sb.Append("Highcharts.chart('container1', {chart: {type: 'column'},title:{align:'center', margin:15 , text:'Graphical report for selected month of RTE v/s Other .'}, xAxis: { categories:");

               int Count = ds.Tables[2].Rows.Count;
               string particularname = "[";
               string rteturnover = "[";
               string otherturnover = "[";
               string totalturnover = "[";
               for (int i = 0; i < Count; i++)
               {

                   if (i == 0)
                   {
                       particularname = particularname + "'" + (ds.Tables[2].Rows[i]["Office_Name"].ToString()).Trim() + "'";
                       rteturnover = rteturnover + "" + ds.Tables[2].Rows[i]["RteTurnOver"].ToString();
                       otherturnover = otherturnover + "" + ds.Tables[2].Rows[i]["WithoutRteTurnOver"].ToString();
                       totalturnover = totalturnover + "" + ds.Tables[2].Rows[i]["TotalTurnOver"].ToString();
                   }
                   else
                   {
                       particularname = particularname + ",'" + (ds.Tables[2].Rows[i]["Office_Name"].ToString()).Trim() + "'";
                       rteturnover = rteturnover + "," + ds.Tables[2].Rows[i]["RteTurnOver"].ToString();
                       otherturnover = otherturnover + "," + ds.Tables[2].Rows[i]["WithoutRteTurnOver"].ToString();
                       totalturnover = totalturnover + "," + ds.Tables[2].Rows[i]["TotalTurnOver"].ToString();
                   }

               }

               particularname = particularname + "]";
               rteturnover = rteturnover + "]";
               otherturnover = otherturnover + "]";
               totalturnover = totalturnover + "]";

               sb.Append("" + particularname.ToString() + "");

               sb.Append("},");
               sb.Append("yAxis: {allowDecimals: true,title: { text: 'Turnover In Lac'}}");

               sb.Append(",credits: {enabled: false }, plotOptions: {series: {dataLabels: {enabled: true}}}, series: [{ name: 'RTE', data:");
               sb.Append("" + rteturnover.ToString() + "");
               sb.Append("},");

               sb.Append("{name: 'Without RTE', data:");
               sb.Append("" + otherturnover.ToString() + "");
               sb.Append("},");

               sb.Append("{name: 'Total', data:");
               sb.Append("" + totalturnover.ToString() + "");
               //sb.Append(",color:'#64E572'},");
               sb.Append("},");

               sb.Append(" ]});");

               sb.Append("</script>");
               divchartPregressive.InnerHtml = sb.ToString();

               /***********************/
           }
       }
       catch (Exception ex)
       {
           lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
       }
   }

   //protected void CreateChart()
   //{
   //    try
   //    {
   //        ds = objdb.ByProcedure("SpFinMisProgressiveNew", new string[] { "flag", "Division_ID", "Year", "Month_ID" }, new string[] { "12", ddlOffice.SelectedValue, ddlYear.SelectedValue, ddlMonth.SelectedValue }, "dataset");
   //        if (ds != null && ds.Tables[0].Rows.Count > 0)
   //        {
   //            /***********************/
   //            StringBuilder sb = new StringBuilder();
   //            sb.Append("<script>");
   //            sb.Append("Highcharts.chart('container1', {chart: {type: 'column'},title: { text: 'Progressive graphical report for selected month.'}, xAxis: { categories:");


   //            int Count = ds.Tables[0].Rows.Count;
   //            string particularname = "[";
   //            string rteturnover = "[";
   //            string otherturnover = "[";
   //            string totalturnover = "[";
   //            for (int i = 0; i < Count; i++)
   //            {

   //                if (i == 0)
   //                {
   //                    particularname = particularname + "'" + (ds.Tables[0].Rows[i]["Office_Name"].ToString()).Trim() + "'";
   //                    rteturnover = rteturnover + "" + ds.Tables[0].Rows[i]["RteTurnOver"].ToString();
   //                    otherturnover = otherturnover + "" + ds.Tables[0].Rows[i]["WithoutRteTurnOver"].ToString();
   //                    totalturnover = totalturnover + "" + ds.Tables[0].Rows[i]["TotalTurnOver"].ToString();
   //                }
   //                else
   //                {
   //                    particularname = particularname + ",'" + (ds.Tables[0].Rows[i]["Office_Name"].ToString()).Trim() + "'";
   //                    rteturnover = rteturnover + "," + ds.Tables[0].Rows[i]["RteTurnOver"].ToString();
   //                    otherturnover = otherturnover + "," + ds.Tables[0].Rows[i]["WithoutRteTurnOver"].ToString();
   //                    totalturnover = totalturnover + "," + ds.Tables[0].Rows[i]["TotalTurnOver"].ToString();
   //                }

   //            }

   //            particularname = particularname + "]";
   //            rteturnover = rteturnover + "]";
   //            otherturnover = otherturnover + "]";
   //            totalturnover = totalturnover + "]";

   //            sb.Append("" + particularname.ToString() + "");

   //            sb.Append("},");
   //            sb.Append("yAxis: {allowDecimals: true,title: { text: 'Turnover In Lac'}}");

   //            sb.Append(",credits: {enabled: false }, series: [{ name: 'RTE', data:");
   //            sb.Append("" + rteturnover.ToString() + "");

   //            sb.Append("},");

   //            sb.Append("{name: 'Without RTE', data:");
   //            sb.Append("" + otherturnover.ToString() + "");
   //            sb.Append("},");

   //            sb.Append("{name: 'Total', data:");
   //            sb.Append("" + totalturnover.ToString() + "");
   //            sb.Append(" }");

   //            sb.Append(" ]});");

   //            sb.Append("</script>");
   //            divchartPregressive.InnerHtml = sb.ToString();
   //            /***********************/
   //        }
   //    }
   //    catch (Exception ex)
   //    {
   //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
   //    }
   //}

   protected void CreateChart2(DataSet ds)
   {
       try
       {
           if (ds != null && ds.Tables[1].Rows.Count > 0)
           {
               /***********************/
               StringBuilder sb = new StringBuilder();
               sb.Append("<script>");
               sb.Append("Highcharts.chart('container2', {chart: {type: 'column'},title:{align:'center', margin:15 , text:'Graphical report for selected month of current year v/s last year .'}, xAxis: { categories:");

               int Count = ds.Tables[1].Rows.Count;
               string particularname = "[";
               string rteturnover = "[";
               string otherturnover = "[";
               for (int i = 0; i < Count; i++)
               {

                   if (i == 0)
                   {
                       particularname = particularname + "'" + (ds.Tables[1].Rows[i]["OName"].ToString()).Trim() + "'";
                       rteturnover = rteturnover + "" + ds.Tables[1].Rows[i]["LastYear"].ToString();
                       otherturnover = otherturnover + "" + ds.Tables[1].Rows[i]["CurrentYear"].ToString();
                   }
                   else
                   {
                       particularname = particularname + ",'" + (ds.Tables[1].Rows[i]["OName"].ToString()).Trim() + "'";
                       rteturnover = rteturnover + "," + ds.Tables[1].Rows[i]["LastYear"].ToString();
                       otherturnover = otherturnover + "," + ds.Tables[1].Rows[i]["CurrentYear"].ToString();
                   }

               }

               particularname = particularname + "]";
               rteturnover = rteturnover + "]";
               otherturnover = otherturnover + "]";

               sb.Append("" + particularname.ToString() + "");

               sb.Append("},");
               sb.Append("yAxis: {allowDecimals: true,title: { text: 'Turnover In Lac'}}");

               sb.Append(",credits: {enabled: false }, plotOptions: {series: {dataLabels: {enabled: true}}}, series: [{ name: 'Last Year', data:");
               sb.Append("" + rteturnover.ToString() + "");
               sb.Append("},");
               sb.Append("{name: 'Current Year', data:");
               sb.Append("" + otherturnover.ToString() + "");
               sb.Append("},");
               sb.Append(" ]});");
               sb.Append("</script>");
               divchartPregressive2.InnerHtml = sb.ToString();
               /***********************/
           }
       }
       catch (Exception ex)
       {
           lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
       }
   }

}