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

public partial class mis_Finance_LedgerDetailB : System.Web.UI.Page
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
            GVLedgerDetail.DataSource = new string[] { };

            ds = objdb.ByProcedure("SpFinMapUnMapLedger", 
                new string[] { "flag", "Office_ID","LedgerType" }, 
                new string[] { "2", ViewState["Office_ID"].ToString(),ddlHOOfficeLedger.SelectedValue.ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVLedgerDetail.DataSource = ds.Tables[0];
                }
               
            }

            GVLedgerDetail.DataBind();
            //GVLedgerDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            //GVLedgerDetail.UseAccessibleHeader = true;

            //foreach (GridViewRow rows in GVLedgerDetail.Rows)
            //{
            //    Label lblID = (Label)rows.FindControl("lblID");
            //    LinkButton lnkDelete = (LinkButton)rows.FindControl("Delete");
            //    string Ledger_ID = lblID.Text;
            //    ds = objdb.ByProcedure("SpFinLedgerMaster",
            //                 new string[] { "flag", "Ledger_ID", "Office_ID" },
            //                 new string[] { "46", Ledger_ID.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            //    if (ds != null && ds.Tables[0].Rows.Count > 0)
            //    {
            //        if (ds.Tables[1].Rows[0]["status"].ToString() == "true")
            //        {

            //            if (ds.Tables[0].Rows[0]["status"].ToString() == "true")
            //            {
            //                lnkDelete.Visible = false;
            //            }
            //            else
            //            {
            //                lnkDelete.Visible = true;
            //            }
            //        }
            //        else
            //        {
            //            lnkDelete.Visible = false;
            //        }
            //    }
            //    DataSet ds1 = objdb.ByProcedure("SpFinLedgerMaster",
            //                 new string[] { "flag", "Ledger_ID" },
            //                 new string[] { "48", Ledger_ID.ToString() }, "dataset");
            //    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            //    {
            //        if (ds1.Tables[0].Rows[0]["status"].ToString() == "true")
            //        {
            //            lnkDelete.Visible = false;
            //        }
            //        else
            //        {

            //        }
            //    }
            //}



           

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
            Response.Redirect("LedgerMasterB.aspx?Ledger_ID=" + objdb.Encrypt(Ledger_ID) + "&Mode=" + objdb.Encrypt("Edit"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlHOOfficeLedger_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void GVLedgerDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string Ledger_ID = GVLedgerDetail.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpFinMapUnMapLedger", new string[] { "flag", "Ledger_ID", "Ledger_UpdatedBy", "Office_ID" }, new string[] { "1", Ledger_ID.ToString(), ViewState["Emp_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GVLedgerDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVLedgerDetail.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void GetLedgerName()
    {
        try
        {
            string Office_ID = Session["Office_ID"].ToString();
            DataSet ds4 = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Office_ID" }, new string[] { "50", Office_ID }, "dataset");
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
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> SearchLedger(string Ledger_Name, string SearchOption)
{
        List<string> Ledgers = new List<string>();
        try
        {
            DataView dv = new DataView();
           
            if (SearchOption == "1")
            {
                dv = ds6.Tables[0].DefaultView;
                dv.RowFilter = "Ledger_Name like '%" + Ledger_Name + "%'";
            }
            else
            {
                dv = ds6.Tables[1].DefaultView;
                dv.RowFilter = "Ledger_Code like '%" + Ledger_Name + "%'";
            }
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
            GVLedgerDetail.DataSource = new string[] { };
            string flag = "";
            if(ddlSearchWith.SelectedValue == "1")
            {
                flag = "55";
            }
            else
            {
                flag = "64";
            }
            ds = objdb.ByProcedure("SpFinLedgerMaster",
                new string[] { "flag", "Office_ID", "LedgerType", "Ledger_Name" },
                new string[] { flag, ViewState["Office_ID"].ToString(), ddlHOOfficeLedger.SelectedValue.ToString(), Ledger_Name }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVLedgerDetail.DataSource = ds.Tables[0];
                }

            }

            GVLedgerDetail.DataBind();
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