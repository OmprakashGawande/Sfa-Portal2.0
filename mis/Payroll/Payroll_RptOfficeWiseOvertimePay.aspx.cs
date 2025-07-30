using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Drawing;
using Microsoft.Reporting.WebForms;


public partial class mis_Payroll_Payroll_RptOfficeWiseOvertimePay : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds2, ds3, ds1, ds5, dsdetail;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (objdb.createdBy() != null && objdb.Office_ID() != null)
            {
                if (!IsPostBack)
                {
                    FillOffice();
                    Hideshow("default");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error ", ex.Message.ToString());
        }
    }
    private void Hideshow(string strval)
    {
        switch(strval)
        {
            case "a":
                pnlreport.Visible=true;
                break;
            default:
                 pnlreport.Visible = false;
                break;
        }
    }
    protected void FillOffice()
    {
        try
        {
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));

            ds2 = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds2 != null)
            {
                if (ds2.Tables.Count > 0)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {

                        ddlOfficeName.DataSource = ds2.Tables[0];
                        ddlOfficeName.DataTextField = "Office_Name";
                        ddlOfficeName.DataValueField = "Office_ID";
                        ddlOfficeName.DataBind();


                    }
                }
            }
            ddlOfficeName.SelectedValue = objdb.Office_ID();
            if (ddlOfficeName.SelectedValue.ToString() != "1")
            {
                ddlOfficeName.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }

    private void GetEmployeeOvertimePayDetails()
    {
        try
        {

            string fm = "01/" + txtMonth.Text;
            //  Code for current month date
            DateTime fmonth = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
            string month_name = fmonth.ToString("MMMM");
            string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string[] monthandyear = fromnonth.Split('/');

            DateTime addmonth = fmonth.AddMonths(1);
            DateTime minusday = addmonth.AddDays(-1);

            string tmnth = minusday.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tmonth = DateTime.ParseExact(tmnth, "dd/MM/yyyy", culture);
            string tomonth = tmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            string tm = tmonth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            dsdetail=objdb.ByProcedure("USP_Payroll_Trn_EmpOverTimePayMonthWise",
                         new string[] {"Office_ID","OverTimePayYear","OverTimePayMonth","FromDate","ToDate"},
                         new string[] { ddlOfficeName.SelectedValue, monthandyear[0], monthandyear[1], fromnonth, tomonth }, "dataset");

            if(dsdetail!=null)
            {
                if(dsdetail.Tables.Count>0)
                {
                    if (Convert.ToString(dsdetail.Tables[0].Rows[0]["Msg"])=="Ok" && dsdetail.Tables[1].Rows.Count > 0)
                    {
                        Hideshow("a");
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportDataSource datasource = new ReportDataSource();
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("Payroll_RptEmployeeOvertimePay.rdlc");
                        datasource = new ReportDataSource("dtOvertimePayReport", dsdetail.Tables[1]);

                        ReportParameter HeaderVal1, Tittle, OfficeName;
                        OfficeName = new ReportParameter("OfficeName", ddlOfficeName.SelectedItem.Text);
                        if (ddlOfficeName.SelectedValue == "81")
                        {
                            HeaderVal1 = new ReportParameter("HeaderVal1", "Minor Forest Produce-Processing and Research Centre (MFP-PARC)");
                            Tittle = new ReportParameter("Tittle", " Over Time Pay " + month_name + "-" + monthandyear[0]);


                        }
                        else
                        {
                            HeaderVal1 = new ReportParameter("HeaderVal1", "M.P. State Minor Forest Produce Federation (T & D) CO.OP.FED.LTD.");
                            Tittle = new ReportParameter("Tittle", " Over Time Pay " + month_name + "-" + monthandyear[0]);

                        }
                      
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { HeaderVal1, OfficeName, Tittle });
                        ReportViewer1.LocalReport.DataSources.Add(datasource);
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning!", "No Record Found");
                        Hideshow("default");
                        return;
                    }
                }
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (dsdetail != null) { dsdetail.Dispose(); }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Page.Validate("a");

        if (!Page.IsValid)
        {
            return;
        }
        else
        {
            GetEmployeeOvertimePayDetails();
        }
    }
    protected void lnkbtnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Hideshow("default");
    }
}