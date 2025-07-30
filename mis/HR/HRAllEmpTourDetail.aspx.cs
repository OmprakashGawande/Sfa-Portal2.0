using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
public partial class mis_HR_HRAllEmpTourDetail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    txtReason.Attributes.Add("readonly", "readonly");
                    txtRemarkByHR.Attributes.Add("readonly", "readonly");
                    txtFeedBack.Attributes.Add("readonly", "readonly");
                    FillYear();
                    string sMonth = DateTime.Now.ToString("MM");
                    ddlMonth.SelectedValue = sMonth.ToString();
                    //FillGrid();
                }
                else
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillYear()
    {
        try
        {
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlFinancialYear.DataSource = ds;
                ddlFinancialYear.DataTextField = "Year";
                ddlFinancialYear.DataValueField = "Year";
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select Financial Year", "0"));
            }
            ddlFinancialYear.SelectedValue = DateTime.Now.Year.ToString();

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
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHRTourApplication", new string[] { "flag", "Year", "MonthNo" },
                  new string[] { "7", ddlFinancialYear.SelectedValue.ToString(),ddlMonth.SelectedValue.ToString() }, "datatset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                lblMsg2.Text = "No Record Found...";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["TourId"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpHRTourApplication", new string[] { "flag", "TourId" },
                  new string[] { "3", ViewState["TourId"].ToString() }, "datatset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                DetailsView1.DataSource = ds;
                DetailsView1.DataBind();
                //Label header = (Label)DetailsView1.HeaderRow.FindControl("lblHeader");
                //if (ds.Tables[0].Rows[0]["LeaveStatus1"].ToString() == "Approved")
                //{
                //    header.Text = "Leave Request Doc";
                //}
                //else if (ds.Tables[0].Rows[0]["LeaveStatus1"].ToString() == "Rejected")
                //{
                //    header.Text = "Leave Rejected Doc";
                //}
                //else
                //{
                //    header.Text = "Leave Doc";
                //}

                DetailsView2.DataSource = ds;
                DetailsView2.DataBind();
                DetailsView3.DataSource = ds;
                DetailsView3.DataBind();
                Label DocHeader = (Label)DetailsView2.HeaderRow.FindControl("lblDocHeader");
                if (ds.Tables[0].Rows[0]["TourStatus1"].ToString() == "Approved")
                {
                    DocHeader.Text = "Tour Approval Doc";
                    tourfeedback.Visible = true;
                }
                else if (ds.Tables[0].Rows[0]["TourStatus1"].ToString() == "Rejected")
                {
                    DocHeader.Text = "Tour Rejected Doc";
                    tourfeedback.Visible = false;
                }
                else
                {
                    DocHeader.Text = "Tour Doc";
                    tourfeedback.Visible = false;
                }
                txtFeedBack.Text = ds.Tables[0].Rows[0]["TourFeedBack"].ToString();
                txtReason.Text = ds.Tables[0].Rows[0]["TourDescription"].ToString();
                txtRemarkByHR.Text = ds.Tables[0].Rows[0]["RemarkByApprovalAuth"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg2.Text = "";
            FillGrid();
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
            lblMsg2.Text = "";
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}