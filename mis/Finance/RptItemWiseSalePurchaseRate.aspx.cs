using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;


public partial class mis_Finance_RptItemWiseSalePurchaseRate : System.Web.UI.Page
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

                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillDropdown();
                    GetCommonSearch();

                }
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
    protected void FillDropdown()
    {
        try
        {

            ds = objdb.ByProcedure("Sp_tblSpItemStock",
                   new string[] { "flag" },
                   new string[] { "31" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlItem.DataSource = ds;
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "Item_id";
                ddlItem.DataBind();
                ddlItem.Items.Insert(0, new ListItem("Selct", "0"));

            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            lblMsg.Text = "";
            if (ddlItem.SelectedIndex == 0)
            {
                msg += "Select Item Name \\n";
            }
            if (txtFromDate.Text == "")
            {
                msg += "Enter From Date. \\n";
            }
            if (txtToDate.Text == "")
            {
                msg += "Enter To Date. \\n";
            }
            if (msg == "")
            {
                /*****************SET Search Option Start********************/
                #region SetSearchOption
                Session["CommonFromDate"] = txtFromDate.Text;
                Session["CommonToDate"] = txtToDate.Text;
                #endregion
                /*****************SET Search Option End********************/
                FillGrid();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillGrid()
    {
        try
        {

            if (ddlItem.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("Sp_tblSpItemStock",
                   new string[] { "flag", "Item_id", "FromDate", "ToDate" },
                   new string[] { "30", ddlItem.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    GVItemDetail.DataSource = ds;

                }
                GVItemDetail.DataBind();
                GVItemDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                GVItemDetail.UseAccessibleHeader = true;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void GetCommonSearch()
    {
        try
        {
            
            if (Session["CommonFromDate"] != null)
            {
                string FromDate = Session["CommonFromDate"].ToString();
                txtFromDate.Text = FromDate.ToString();
            }
            if (Session["CommonToDate"] != null)
            {
                string ToDate = Session["CommonToDate"].ToString();
                txtToDate.Text = ToDate.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
   
}