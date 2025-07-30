using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
public partial class mis_HR_HREmpIncreamentPendingRM : System.Web.UI.Page
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
                    txtIncreamentDate.Attributes.Add("readonly", "readonly");
                    FillDropDown();
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
    protected void FillDropDown()
    {
        try
        {
            ddlOffice.Items.Insert(0, new ListItem("Select Office", "0"));
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_ID" },
                            new string[] { "20", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("Select Office", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
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
            lblMsg.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHREmpIncreament", new string[] { "flag", "Office_ID" }, new string[] { "11",ddlOffice.SelectedValue.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();

            }
            else
            {
                lblMsg2.Text = "No Record Found.";
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
            txtIncreamentDate.Text = "";
            txtRemark.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
            ViewState["Increament_ID"] = GridView1.SelectedDataKey.Value.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
  
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByProcedure("SpHREmpIncreament", new string[] { "flag", "Increament_ID", "Increament_Date", "IncreamentRemark", "UpdatedBy" },
                    new string[] { "8", ViewState["Increament_ID"].ToString(), Convert.ToDateTime(txtIncreamentDate.Text, cult).ToString("yyyy-MM-dd"), txtRemark.Text, ViewState["Emp_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Status"].ToString() == "True")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillGrid();
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-danger", "Sorry!", "Increment not Completed please try again.");
                }
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
            lblMsg.Text = "";
            lblMsg2.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ddlOffice.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpHREmpIncreament", new string[] { "flag", "Office_ID" }, new string[] { "11", ddlOffice.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();

                }
                else
                {
                    lblMsg2.Text = "No Record Found.";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}