using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Globalization;

public partial class mis_HR_PostRpt : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ds = objdb.ByProcedure("SpHRPostMaster", new string[] { "flag" }, new string[] { "3" }, "dataset");
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     ReportViewer1.ProcessingMode = ProcessingMode.Local;
                     ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/mis/HR/PostRpt.rdlc");
                     ReportDataSource datasource = new ReportDataSource("dsHRPostMaster", ds.Tables[0]);
                     ReportViewer1.LocalReport.DataSources.Clear();
                     ReportViewer1.LocalReport.DataSources.Add(datasource);
                 }
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
}