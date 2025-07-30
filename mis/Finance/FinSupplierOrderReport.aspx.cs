using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;


public partial class mis_Finance_FinSupplierOrderReport : System.Web.UI.Page
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
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                ds = objdb.ByProcedure("SpFinSupplierOrder",
                    new string[] { "flag","FromDate","ToDate" },
                    new string[] { "2", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}