using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using System.Collections.Generic;

public partial class mis_Finance_LedgerDetailBOther : System.Web.UI.Page
{
    static DataSet ds6;
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
                    GetLedgerName();
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
                new string[] { "flag", "Office_ID"}, 
                new string[] { "53", ViewState["Office_ID"].ToString()}, "dataset");
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
            Response.Redirect("LedgerMasterB.aspx?Ledger_ID=" + objdb.Encrypt(Ledger_ID) + "&Mode=" + objdb.Encrypt("Edit"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GVLedgerOther_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVLedgerOther.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void GetLedgerName()
    {
        try
        {
            string Office_ID = Session["Office_ID"].ToString();
            DataSet ds4 = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Office_ID" }, new string[] { "56", Office_ID }, "dataset");
            ds6 = ds4;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> SearchLedger(string Ledger_Name)
    {
        List<string> Ledgers = new List<string>();
        try
        {
            DataView dv = new DataView();
            dv = ds6.Tables[0].DefaultView;
            dv.RowFilter = "Ledger_Name like '%" + Ledger_Name + "%'";
            DataTable dt = dv.ToTable();

            foreach (DataRow rs in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    Ledgers.Add(rs[col].ToString());
                }
            }

        }
        catch { }
        return Ledgers;
    }
    protected void FillGridSearch(string Ledger_Name)
    {
        try
        {
            GVLedgerOther.DataSource = new string[] { };

            ds = objdb.ByProcedure("SpFinLedgerMaster",
                new string[] { "flag", "Office_ID", "Ledger_Name" },
                new string[] { "57", ViewState["Office_ID"].ToString(),  Ledger_Name }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVLedgerOther.DataSource = ds.Tables[0];
                }

            }

            GVLedgerOther.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FillGridSearch(txtLedgerName.Text);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            txtLedgerName.Text = string.Empty;
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}