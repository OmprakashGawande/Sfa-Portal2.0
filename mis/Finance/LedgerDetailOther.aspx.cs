using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
public partial class mis_Finance_LedgerDetail : System.Web.UI.Page
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
           
            GVLedgerOther.DataSource = new string[] { };
            ds = objdb.ByProcedure("SpFinLedgerMaster",
                new string[] { "flag", "Office_ID" },
                new string[] { "54", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVLedgerOther.DataSource = ds.Tables[0];
                }
            }

           
     
            GVLedgerOther.DataBind();
            GVLedgerOther.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVLedgerOther.UseAccessibleHeader = true;

        }                                                    
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
 
    protected void GVLedgerOther_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string Ledger_ID = GVLedgerOther.SelectedDataKey.Value.ToString();
            Response.Redirect("LedgerMaster_Forotherofc.aspx?Ledger_ID=" + objdb.Encrypt(Ledger_ID) + "&Mode=" + objdb.Encrypt("Edit"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
   
}