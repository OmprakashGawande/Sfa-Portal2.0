using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class mis_Finance_SpVendor_List : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Office_ID"] != null && Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    GVVendorDetail.DataSource = new string[] { };
                    GVVendorDetail.DataBind();
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
            ds = objdb.ByProcedure("SpVendorMaster", new string[] { "flag" }, new string[] {"9"}, "dataset");
            if(ds!= null && ds.Tables[0].Rows.Count > 0)
            {
                GVVendorDetail.DataSource = ds;
                GVVendorDetail.DataBind();
                GVVendorDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                GVVendorDetail.UseAccessibleHeader = true;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GVVendorDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string Vendorid = GVVendorDetail.SelectedDataKey.Value.ToString();
            Response.Redirect("LedgerMaster.aspx?Vendorid=" + objdb.Encrypt(Vendorid));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}