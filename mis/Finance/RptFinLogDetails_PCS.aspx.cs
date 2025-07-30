using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;

public partial class mis_Finance_RptFinLogDetails : System.Web.UI.Page
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
                    FillGrid();
                }
                lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt");
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void FillGrid()
    {
        try
        {
            Repeater1.DataSource = new string[] { };
            Repeater1.DataBind();
            int cssStrong = 0;
            int cssweek = 0;
            lblcssStrong.Text = "&nbsp;&nbsp;&nbsp;";
            lblcssweek.Text = "&nbsp;&nbsp;&nbsp;";
            ds = objdb.ByProcedure("SpLogDetail", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                int rowcount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < rowcount; i++)
                {
                    string RowColor = ds.Tables[0].Rows[i]["csscolor"].ToString();
                    if (RowColor == "cssStrong")
                        cssStrong++;
                    else
                        cssweek++;

                }

            }
            lblcssStrong.Text = cssStrong.ToString();
            lblcssweek.Text = cssweek.ToString();
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            FillGrid();

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

}
