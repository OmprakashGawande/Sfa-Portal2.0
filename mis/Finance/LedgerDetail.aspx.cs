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
            GVLedgerDetail.DataSource = new string[] { };

            ds = objdb.ByProcedure("SpFinMapUnMapLedger",
                new string[] { "flag", "Office_ID" },
                new string[] { "0", ViewState["Office_ID"].ToString() }, "dataset");
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
            //foreach (GridViewRow rows in GVLedgerDetail.Rows)
            //{
            //    Label lblID = (Label)rows.FindControl("lblID");
            //    LinkButton lnkDelete = (LinkButton)rows.FindControl("Delete");
            //    string Ledger_ID = lblID.Text;
            //    ds = objdb.ByProcedure("SpFinLedgerMaster",
            //                 new string[] { "flag", "Ledger_ID","Office_ID" },
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
            Response.Redirect("LedgerMaster_Forotherofc.aspx?Ledger_ID=" + objdb.Encrypt(Ledger_ID) + "&Mode=" + objdb.Encrypt("Edit"));
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
}