using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Payroll_Payroll_Mst_OvertimePayRate : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds2, ds1, ds5, dsdetail;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (objdb.createdBy() != null && objdb.Office_ID() != null)
            {
                if (!IsPostBack)
                {
                    GetOverTimePayDetails();
                    txtEffectiveDate.Attributes.Add("readonly", "true");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error ", ex.Message.ToString());
        }
    }

   
    public void GetOverTimePayDetails()
    {
        try
        {

            GridView1.DataSource = null;
            GridView1.DataBind();
            lblMsg.Text = string.Empty;
            ds1 = objdb.ByProcedure("USP_Payroll_Mst_OverTimePayRate_GetAll",
                   new string[] { },
                   new string[] { }, "dataset");

            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds1.Tables[0];
                        GridView1.DataBind();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error BindData", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    public void GetOverTimePayChildDetails(string payrateid)
    {
        try
        {

            GridView2.DataSource = null;
            GridView2.DataBind();
            lblMsg.Text = string.Empty;
            ds2 = objdb.ByProcedure("USP_Payroll_Mst_OverTimePayRateChild_GetAll",
                   new string[] { "OverTimePayRateId" },
                   new string[] { payrateid.ToString() }, "dataset");

            if (ds2 != null)
            {
                if (ds2.Tables.Count > 0)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        GridView2.DataSource = ds2.Tables[0];
                        GridView2.DataBind();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error BindData", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    private void Clear()
    {
       
        txtOvertimePayRate.Text = string.Empty;
        txtEffectiveDate.Text = string.Empty;
        btnSave.Text = "Save";
        GridView1.SelectedIndex = -1;
    }
    private void InsertOrUpdateOvertimePayRate()
    {
        try
        {

            string efdate = txtEffectiveDate.Text;
            string newefdate = "01" + "/" + efdate;
            DateTime effectivedate = DateTime.ParseExact(newefdate, "dd/MM/yyyy", culture);
            string effectivedat = effectivedate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            if (btnSave.Text == "Save")
            {               
                ds5 = objdb.ByProcedure("USP_Payroll_Mst_OverTimePayRate_Insert",
                          new string[] { "PayRate", "EffectiveDate", "CreatedBy", "CreatedByIP" },
                         new string[] { txtOvertimePayRate.Text.Trim(), effectivedat, objdb.createdBy(), IPAddress },
                            "dataset");


                if (ds5.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    string success = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    Clear();
                    GetOverTimePayDetails();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                }
                else if (ds5.Tables[0].Rows[0]["Msg"].ToString() == "already")
                {
                    string warning = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning!", warning);
                }
                else
                {
                    string error = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                }
            }
            else if (btnSave.Text == "Update" && hfrowid.Value!="")
            {
                ds5 = objdb.ByProcedure("USP_Payroll_Mst_OverTimePayRate_Update",
                          new string[] { "OverTimePayRateId", "PayRate", "EffectiveDate", "CreatedBy", "CreatedByIP" },
                         new string[] { hfrowid.Value,txtOvertimePayRate.Text.Trim(), effectivedat, objdb.createdBy(), IPAddress },
                            "dataset");

                if (ds5.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    string success = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    Clear();
                    GetOverTimePayDetails();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                }
                else if (ds5.Tables[0].Rows[0]["Msg"].ToString() == "already")
                {
                    string warning = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning!",  warning);
                }
                else
                {
                    string error = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                }
            }
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
          
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error " + ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Page.Validate("a");

        if (!Page.IsValid)
        {
            return;
        }
        else
        {
            InsertOrUpdateOvertimePayRate();
        }
    }
    protected void lnkbtnClear_Click(object sender, EventArgs e)
    {
        Clear();
        lblMsg.Text = string.Empty;
        GridView1.SelectedIndex = -1;
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                    Label lblEffectiveMonthYear = (Label)row.FindControl("lblEffectiveMonthYear");
                    Label lblPayRateRate = (Label)row.FindControl("lblPayRate");
                    txtEffectiveDate.Text = lblEffectiveMonthYear.Text;
                    txtOvertimePayRate.Text = lblPayRateRate.Text;
                    btnSave.Text = "Update";
                    hfrowid.Value= e.CommandArgument.ToString();

                    foreach (GridViewRow gvRow in GridView1.Rows)
                    {
                        if (GridView1.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView1.SelectedIndex = gvRow.DataItemIndex;
                            GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                }

            }
            else if (e.CommandName == "RecordView")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    GetOverTimePayChildDetails(e.CommandArgument.ToString());
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ViewOverTimePayRateDetails()", true);
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}