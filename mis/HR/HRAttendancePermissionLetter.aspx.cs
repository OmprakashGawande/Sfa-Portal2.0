using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using System.Globalization;

public partial class mis_HR_HRAttendancePermissionLetter : System.Web.UI.Page
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
                if (Request.QueryString["allow"] == null)
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
                ds = objdb.ByProcedure("SpHRRptLoginPermissionLetter",
                    new string[] { "flag","Allow_ID" },
                    new string[] { "1", Request.QueryString["allow"].ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    lblDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["UpdatedOn"].ToString(), cult).ToString("dddd, dd MMMM yyyy");
                    lblEmpName.Text = ds.Tables[0].Rows[0]["Emp_Name"].ToString();
                    lblEmpDesignation.Text = ds.Tables[0].Rows[0]["Designation_Name"].ToString();
                    
                }
            }
            
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
}