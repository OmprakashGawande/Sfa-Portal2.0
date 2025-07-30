using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_Finance_AllLedgerDetail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {

                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillGrid();

                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
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
            GVLedgerDetail.DataSource = new string[] { };
            ds = objdb.ByProcedure("SpFinLedgerMaster",
                new string[] { "flag" },
                new string[] { "26" }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVLedgerDetail.DataSource = ds.Tables[0];
                }
            }
            GVLedgerDetail.DataBind();
            GVLedgerDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVLedgerDetail.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GVLedgerDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string Ledger_ID = GVLedgerDetail.SelectedDataKey.Value.ToString();
            Response.Redirect("LedgerMasterB.aspx?Ledger_ID=" + objdb.Encrypt(Ledger_ID) + "&Mode=" + objdb.Encrypt("View"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}