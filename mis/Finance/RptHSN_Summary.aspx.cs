using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Finance_RptHSN_Summary : System.Web.UI.Page
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
                    ddlOffice.Enabled = false;
                    FillDropdown();
                    FillVoucherDate();
                    FillFromDate();
                    GetCommonSearch();
                    DataSet ds3 = objdb.ByProcedure("SpFinHSNMaster", new string[] { "flag" }, new string[] { "2" }, "dataset");
                    if (ds3.Tables[0].Rows.Count > 0)
                    {
                        ddlHsnCode.DataSource = ds3;
                        ddlHsnCode.DataTextField = "HSN_Code";
                        ddlHsnCode.DataValueField = "HSN_Code";
                        ddlHsnCode.DataBind();
                        //ddlHsnCode.Items.Insert(0, new ListItem("All", "0"));
                    }
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillVoucherDate()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtToDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillFromDate()
    {
        try
        {
            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd")));
            //String dy = datevalue.Day.ToString();
            int mn = datevalue.Month;
            int yy = datevalue.Year;
            if (mn < 4)
            {
                txtFromDate.Text = "01/04/" + (yy - 1).ToString();
            }
            else
            {
                txtFromDate.Text = "01/04/" + (yy).ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDropdown()
    {
        try
        {
            if (ViewState["Office_ID"].ToString() == "1")
            {
                ddlOffice.Enabled = true;
            }
            ds = objdb.ByProcedure("SpFinRptTrialBalanceNew", new string[] { "flag" }, new string[] { "0" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                //ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            getHSNSummary();
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
            getHSNSummary();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void getHSNSummary()
    {
        try
        {
            lblMsg.Text = "";
            lblParticulars.Text = "";
            lblParticularsRate.Text = "";
            GridHSNSummery.DataSource = null;
            GridHSNSummery.DataBind();

            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                string FromDate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
                string ToDate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
                string Office = "", HSN_Code = "";
                foreach (ListItem item in ddlOffice.Items)
                {
                    if (item.Selected)
                    {
                        Office += item.Value + ",";
                    }
                }
                foreach (ListItem HSN_item in ddlHsnCode.Items)
                {
                    if (HSN_item.Selected)
                    {
                        HSN_Code += HSN_item.Value + ",";
                    }
                }

                /*****************SET Search Option Start********************/
                #region SetSearchOption
                Session["CommonOffice"] = Office;
                Session["CommonFromDate"] = txtFromDate.Text;
                Session["CommonToDate"] = txtToDate.Text;
                #endregion
                /*****************SET Search Option End********************/

                ds = objdb.ByProcedure("SpFinRptHSN_Summary", 
                    new string[] { "flag", "Office_ID_Mlt", "HSN_Code", "FromDate", "ToDate" }, 
                    new string[] { "1", Office, HSN_Code, FromDate, ToDate }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    lblParticulars.Text = "HSN/SAC Summary - 13 for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
                    GridHSNSummery.DataSource = ds;
                    GridHSNSummery.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridHSNSummery_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        try
        {
            GridHSNSummery.PageIndex = e.NewPageIndex;
            lblMsg.Text = "";
            getHSNSummary();
        }
        catch { }
    }
    protected void GetCommonSearch()
    {
        try
        {
            if (Session["CommonOffice"] != null)
            {
                string Office = Session["CommonOffice"].ToString();
                string[] OfficeList = Office.Split(new Char[] { ',' });
                if (OfficeList.Count() > 0)
                {
                    ddlOffice.ClearSelection();
                    foreach (string OfficeID in OfficeList)
                    {
                        // ddlOffice.SelectedValue = OfficeID.ToString();
                        foreach (ListItem item in ddlOffice.Items)
                        {
                            if (item.Value == OfficeID)
                                item.Selected = true;
                        }
                    }
                }

            }
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
