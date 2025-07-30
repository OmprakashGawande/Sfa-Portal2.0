using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;

public partial class mis_Admin_MISReport : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
         if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {

                    ds = objdb.ByProcedure("SpMISReport",
                  new string[] { "flag" },
                  new string[] { "0"},"dataset");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/mis/Admin/MISReport.rdlc");
                        ReportDataSource datasource = new ReportDataSource("dsMISReport", ds.Tables[0]);
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
