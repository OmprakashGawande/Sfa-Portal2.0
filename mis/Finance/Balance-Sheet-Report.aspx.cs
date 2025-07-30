using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Finance_Balance_Sheet_Report : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PanelSheet.Visible = false;
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("All", "0"));
            }
            if (Session["Office_ID"].ToString() == "1")
            {
                ddlOffice.SelectedValue = "1";
            }
            else
            {
                ddlOffice.Enabled = false;
            }
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlFinancialYear.DataSource = ds;
                ddlFinancialYear.DataTextField = "Financial_Year";
                ddlFinancialYear.DataValueField = "Year_ID";
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select FY", "0"));
            }

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlFinancialYear.SelectedIndex > 0 && ddlOffice.SelectedIndex > 0)
            PanelSheet.Visible = true;
        else
            PanelSheet.Visible = false;
    }
}