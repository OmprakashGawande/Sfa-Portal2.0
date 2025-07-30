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

public partial class mis_Finance_RptMisProgressiveDistrictSingleMonth : System.Web.UI.Page
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
            DataSet dsOffice = objdb.ByProcedure("SpFinMisProgressiveNew", new string[] { "flag" }, new string[] { "8" }, "dataset");
            if (dsOffice != null)
            {
                ddlOffice.DataSource = dsOffice;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                if (ViewState["Office_ID"].ToString() != "1")
                {
                    ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();

                }
                else
                {
                    ddlOffice.Enabled = true;
                }

            }  
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
            if (msg == "")
            {
                FillGrid();
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
            ds = objdb.ByProcedure("SpFinMisProgressiveNew", new string[] { "flag", "Office_ID", "Year", "Month_ID" }, new string[] { "10", ddlOffice.SelectedValue.ToString(), ddlYear.SelectedValue, ddlMonth.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                /***********************/
                StringBuilder sb = new StringBuilder();
                sb.Append("<script>");
                sb.Append("Highcharts.chart('container1', {chart: {type: 'column'},title: { text: ' Graphical Report.'}, xAxis: { categories:");
                

                int Count = ds.Tables[0].Rows.Count;
                string particularname = "[";
                string lastyear = "[";
                string currentyear = "[";
                for (int i = 0; i < Count; i++)
                {

                    if(i==0){
                        particularname = particularname+"'"+ (ds.Tables[0].Rows[i]["Particular_Name"].ToString()).Trim()+"'";
                        lastyear = lastyear + "" + ds.Tables[0].Rows[i]["LYear"].ToString();
                        currentyear = currentyear + "" + ds.Tables[0].Rows[i]["CYear"].ToString();
                    }
                    else
                    {
                        particularname = particularname+",'"+(ds.Tables[0].Rows[i]["Particular_Name"].ToString()).Trim()+"'";
                        lastyear = lastyear + "," + ds.Tables[0].Rows[i]["LYear"].ToString();
                        currentyear = currentyear + "," + ds.Tables[0].Rows[i]["CYear"].ToString();
                    }
                    
                }

                particularname = particularname+"]";
                lastyear = lastyear+"]";
                currentyear = currentyear+"]";

                sb.Append("" + particularname.ToString() + "");

                sb.Append("},");
                sb.Append("yAxis: {allowDecimals: true,title: { text: 'In Lac'}}");

                sb.Append(",credits: {enabled: false }, series: [{ name: 'Last Year', data:");
                sb.Append("" + lastyear.ToString() + "");

                sb.Append("}, {name: 'Current Year', data:");
                sb.Append(""+currentyear.ToString()+"");



                sb.Append(" }]});");

                sb.Append("</script>");
                divchartPregressive.InnerHtml = sb.ToString();
                /***********************/



                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                string FromDate = "";
                string Todate = "";
                //if (Convert.ToInt32(ddlMonth.SelectedValue) < 4)
                //{
                //    FromDate = "01/04/" + (Convert.ToInt32(ddlYear.SelectedValue) - 1).ToString();
                //    Todate = "" + DateTime.DaysInMonth((Convert.ToInt32(ddlYear.SelectedValue)), (Convert.ToInt32(ddlMonth.SelectedValue))) + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue;

                //}
                //else
                //{
                //    FromDate = "01/04/" + (Convert.ToInt32(ddlYear.SelectedValue)).ToString();
                //    Todate = "" + DateTime.DaysInMonth((Convert.ToInt32(ddlYear.SelectedValue)), (Convert.ToInt32(ddlMonth.SelectedValue))) + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue;
                //}

                //lblheading.Text = "<p><br/>M.P. STATE AGRO INDUSTRIES DEVELOPENT CORPORATION LTD., BHOPAL <br/> MIS REPORT FROM " + FromDate + " TO " + Todate + " (PROGRESSIVE) <br/></p>";
                Todate = "" + ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedValue;
                // }

                lblheading.Text = "M.P. STATE AGRO INDUSTRIES DEVELOPENT CORPORATION LTD., "+ddlOffice.SelectedItem.ToString()+" <br/> MIS MONTHLY REPORT FOR " + Todate + " ";
            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();


                //GridView1.DataSource = new string[] { };
                //GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

}