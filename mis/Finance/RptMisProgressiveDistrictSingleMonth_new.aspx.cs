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

public partial class mis_Finance_RptMisProgressiveDistrictSingleMonth_new : System.Web.UI.Page
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
                    ViewState["OfficeType_Title"] = Session["OfficeType_Title"].ToString();
                    ViewState["Division_ID"] = Session["Division_ID"].ToString();
                    FillDropdown();
                    //FillOffice();
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
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ddlOffice.Items.Clear();
            string Office_ID_mlt = "";

            foreach (ListItem item in ddlRegionalOffice.Items)
            {
                if (item.Selected)
                {
                    Office_ID_mlt += item.Value + ",";
                }
            }
            //if (ddlRegionalOffice.SelectedIndex > 0)
            //{
            //    if (ddlRegionalOffice.SelectedValue.ToString() != "010" && ddlRegionalOffice.SelectedItem.ToString() != "Production Unit")
            //    {
            //        //ds = objdb.ByProcedure("SpAdminOffice",
            //        //       new string[] { "flag", "Office_ID" },
            //        //       new string[] { "23", ddlRegionalOffice.SelectedValue.ToString() }, "dataset");
            //        ds = objdb.ByProcedure("SpAdminOffice",
            //              new string[] { "flag", "Office_ID_mlt" },
            //              new string[] { "26", Office_ID_mlt }, "dataset");
            //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //        {
            //            ddlOffice.DataSource = ds;
            //            ddlOffice.DataTextField = "Office_Name";
            //            ddlOffice.DataValueField = "Office_ID";
            //            ddlOffice.DataBind();
            //            //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
            //            // ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            //        }
            //    }
            //    else
            //    {
            //        ds = objdb.ByProcedure("SpAdminOffice",
            //               new string[] { "flag" },
            //               new string[] { "24" }, "dataset");
            //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //        {
            //            ddlOffice.DataSource = ds;
            //            ddlOffice.DataTextField = "Office_Name";
            //            ddlOffice.DataValueField = "Office_ID";
            //            ddlOffice.DataBind();
            //            //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
            //            // ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            //        }

            //    }
            //}
            //else
            //{
            //    ds = objdb.ByProcedure("SpAdminOffice",
            //            new string[] { "flag" },
            //            new string[] { "22" }, "dataset");
            //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //    {
            //        ddlOffice.DataSource = ds;
            //        ddlOffice.DataTextField = "Office_Name";
            //        ddlOffice.DataValueField = "Office_ID";
            //        ddlOffice.DataBind();
            //        //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
            //        ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            //    }
            //}

            ds = objdb.ByProcedure("SpAdminOffice",
                         new string[] { "flag", "Office_ID_mlt" },
                         new string[] { "26", Office_ID_mlt }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                // ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
            DataSet dsYear = objdb.ByProcedure("SpFinMisProgressiveNew", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (dsYear != null)
            {
                ddlYear.DataSource = dsYear;
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));

                ddlToYear.DataSource = dsYear;
                ddlToYear.DataTextField = "Year";
                ddlToYear.DataValueField = "Year";
                ddlToYear.DataBind();
                ddlToYear.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDropdown()
    {
        try
        {

            ddlOffice.Enabled = false;
            ddlRegionalOffice.Enabled = false;
            divRegionalOffice.Visible = false;
            if (ViewState["OfficeType_Title"].ToString() != "District Office" && ViewState["OfficeType_Title"].ToString() != "Production Unit")
            {
                ddlOffice.Enabled = true;
                ddlRegionalOffice.Enabled = true;
                divRegionalOffice.Visible = true;
                //ds = objdb.ByProcedure("SpAdminOffice",
                // new string[] { "flag" },
                // new string[] { "21" }, "dataset");
                ds = objdb.ByProcedure("SpAdminOffice",
                new string[] { "flag" },
                new string[] { "25" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlRegionalOffice.DataSource = ds;
                    ddlRegionalOffice.DataTextField = "Office_Name";
                    ddlRegionalOffice.DataValueField = "Office_ID";
                    ddlRegionalOffice.DataBind();
                    foreach (ListItem item in ddlRegionalOffice.Items)
                    {
                        item.Selected = true;
                    }
                    //ddlRegionalOffice.Items.Insert(0, new ListItem("All", "0"));
                    //ddlRegionalOffice.Items.Insert(ds.Tables[0].Rows.Count + 1, new ListItem("Production Unit", "010"));
                    if (ViewState["OfficeType_Title"].ToString() == "Regional Office")
                    {
                        ddlRegionalOffice.Enabled = false;
                        ddlRegionalOffice.SelectedValue = ViewState["Division_ID"].ToString();
                    }
                }
            }

             FillOffice();



            //if (ViewState["Office_ID"].ToString() == "1")
            //{
            //    ddlOffice.Enabled = true;
            //}

            //ds = objdb.ByProcedure("SpFinRptTrialBalanceNewFF",
            //       new string[] { "flag" },
            //       new string[] { "0" }, "dataset");
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlOffice.DataSource = ds;
            //    ddlOffice.DataTextField = "Office_Name";
            //    ddlOffice.DataValueField = "Office_ID";
            //    ddlOffice.DataBind();
            //    //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
            //    ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            //}
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
                msg += "Select From Year";
            }
            if (ddlMonth.SelectedIndex == 0)
            {
                msg += "Select From Month";
            }
            if (ddlToYear.SelectedIndex == 0)
            {
                msg += "Select To Year";
            }
            if (ddlToMonth.SelectedIndex == 0)
            {
                msg += "Select To Month";
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
            string Office = "";

            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            ds = objdb.ByProcedure("SpFinMisProgressiveNew", new string[] { "flag", "Office_ID_mlt", "FromYear", "ToYear", "FromMonth_ID", "ToMonth_ID" }, new string[] { "17", Office, ddlYear.SelectedValue, ddlToYear.SelectedValue, ddlMonth.SelectedValue, ddlToMonth.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                /***********************/
                StringBuilder sb = new StringBuilder();
                sb.Append("<script>");
                sb.Append("Highcharts.chart('container1', {chart: {type: 'column'},title: { text: ' Graphical Report.'}, xAxis: { categories:");

                
                int Count = ds.Tables[0].Rows.Count;
                //string particularname = "[";
                //string lastyear = "[";
                //string currentyear = "[";
                //for (int i = 0; i < Count; i++)
                //{

                //    if(i==0){
                //        particularname = particularname+"'"+ (ds.Tables[0].Rows[i]["Particular_Name"].ToString()).Trim()+"'";
                //        lastyear = lastyear + "" + ds.Tables[0].Rows[i]["LYear"].ToString();
                //        currentyear = currentyear + "" + ds.Tables[0].Rows[i]["CYear"].ToString();
                //    }
                //    else
                //    {
                //        particularname = particularname+",'"+(ds.Tables[0].Rows[i]["Particular_Name"].ToString()).Trim()+"'";
                //        lastyear = lastyear + "," + ds.Tables[0].Rows[i]["LYear"].ToString();
                //        currentyear = currentyear + "," + ds.Tables[0].Rows[i]["CYear"].ToString();
                //    }
                    
                //}

                //particularname = particularname+"]";
                //lastyear = lastyear+"]";
                //currentyear = currentyear+"]";

                //sb.Append("" + particularname.ToString() + "");

                //sb.Append("},");
                //sb.Append("yAxis: {allowDecimals: true,title: { text: 'In Lac'}}");

                //sb.Append(",credits: {enabled: false }, series: [{ name: 'Last Year', data:");
                //sb.Append("" + lastyear.ToString() + "");

                //sb.Append("}, {name: 'Current Year', data:");
                //sb.Append(""+currentyear.ToString()+"");



                //sb.Append(" }]});");

                //sb.Append("</script>");
                //divchartPregressive.InnerHtml = sb.ToString();
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
                FromDate = "" + ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedValue;
                Todate = "" + ddlToMonth.SelectedItem.Text + "-" + ddlToYear.SelectedValue;
                // }

                lblheading.Text = "M.P. STATE AGRO INDUSTRIES DEVELOPENT CORPORATION LTD., " + ddlOffice.SelectedItem.ToString() + " <br/> MIS MONTHLY REPORT FOR " + FromDate + " - " + Todate;
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
    protected void ddlRegionalOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillOffice();
    }

}