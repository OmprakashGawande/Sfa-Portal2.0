using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HREmpPromotionPending : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillGrid();
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
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHREmpPromotion", new string[] { "flag" }, new string[] { "4" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                lblMsg2.Text = "No Record Found..";
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
            txtPromotionDate.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
            ViewState["PromotionID"] = GridView1.SelectedDataKey.Value.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnPromotion_Click(object sender, EventArgs e)
    {

        try
        {
            string msg = "";
            if (txtPromotionDate.Text == "")
            {
                msg += "Select Promotion date. <br/>";
            }


            if (msg == "")
            {


                string Promotiondate = txtPromotionDate.Text != "" ? Convert.ToDateTime(txtPromotionDate.Text, cult).ToString("yyyy/MM/dd") : "";

                ds = objdb.ByProcedure("SpHREmpPromotion", new string[] { "flag", "PromotionID", "PromotionDate", "PromotionUpdatedBy" },
                        new string[] { "8", ViewState["PromotionID"].ToString(), Promotiondate, ViewState["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "True")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        FillGrid();
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-danger", "Sorry!", "Promotion not Completed please try again.");
                    }
                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
               // lblmsgmodal.Text = objdb.Alert("fa-check", "alert-warning", "Sorry!", msg);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}