using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_Finance_GroupWiseLedgerDetail : System.Web.UI.Page
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
                    FillHead();
                    GVLedgerDetail.DataSource = new string[] { };
                    GVLedgerDetail.DataBind();
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
    protected void FillHead()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinMasterHead",
                          new string[] { "flag" },
                          new string[] { "7" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlGroup.DataTextField = "Head_Name";
                ddlGroup.DataValueField = "Head_ID";
                ddlGroup.DataSource = ds;
                ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, new ListItem("All", "0"));
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
                new string[] { "flag", "Ledger_HeadID" },
                new string[] { "43",ddlGroup.SelectedValue }, "dataset");
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

   
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
               FillGrid();
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}