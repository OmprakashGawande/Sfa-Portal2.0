using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;

public partial class mis_Admin_AdminUnit : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Unit_id"] = "0";
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (txtUnit.Text == "")
            {
                msg += "Enter Unit. \\n";
            }
            if (ddlQuantity_Type.SelectedIndex <= 0)
            {
                msg = "Please select Quantity Type";
            }
            if (txtUqc_Code.Text == "")
            {
                msg += "Enter QUC Code. \\n";
            }
            if (msg.Trim() == "")
            {
                int Status = 0;
                txtUnit.Text = FirstLetterToUpper(txtUnit.Text);
                ds = objdb.ByProcedure("SpUnit", new string[] { "flag", "UnitName", "Unit_id" }, new string[] { "5", txtUnit.Text, ViewState["Unit_id"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (btnSave.Text == "Accept" && ViewState["Unit_id"].ToString() == "0" && Status == 0)
                {
                    objdb.ByProcedure("SpUnit",
                    new string[] { "flag", "UnitName", "QuantityType", "UQCCode", "NoOfDecimalPlace", "CreatedBy" },
                    new string[] { "0", txtUnit.Text, ddlQuantity_Type.SelectedItem.Text, txtUqc_Code.Text,txtNoOfDecimal.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();

                }
                else if (btnSave.Text == "Edit" && ViewState["Unit_id"].ToString() != "0" && Status == 0)
                {
                    objdb.ByProcedure("SpUnit",
                    new string[] { "flag", "Unit_id", "UnitName", "QuantityType", "UQCCode", "NoOfDecimalPlace", "CreatedBy" },
                    new string[] { "2", ViewState["Unit_id"].ToString(), txtUnit.Text, ddlQuantity_Type.SelectedItem.Text, txtUqc_Code.Text,txtNoOfDecimal.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Unit Name is already exist.');", true);
                    ClearText();
                }
                ViewState["Unit_id"] = "0";


            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
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
            ds = objdb.ByProcedure("SpUnit", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
                foreach(GridViewRow rows in GridView1.Rows)
                {
                    LinkButton lnkdelete = (LinkButton)rows.FindControl("Delete");
                    Label lblid = (Label)rows.FindControl("lblid");
                    ds = objdb.ByProcedure("SpUnit", new string[] { "flag", "Unit_id" }, new string[] { "6", lblid.Text }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["status"].ToString() == "true")
                        {
                            lnkdelete.Visible = false;
                        }
                        else
                        {
                            lnkdelete.Visible = true;
                        }

                    }
                }
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
            ClearText();
            ViewState["Unit_id"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpUnit", new string[] { "flag", "Unit_id" }, new string[] { "4", ViewState["Unit_id"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtUnit.Text = ds.Tables[0].Rows[0]["UnitName"].ToString();
                ddlQuantity_Type.Items.FindByValue(ds.Tables[0].Rows[0]["QuantityType"].ToString()).Selected = true;
                txtUqc_Code.Text = ds.Tables[0].Rows[0]["UQCCode"].ToString();
                txtNoOfDecimal.Text = ds.Tables[0].Rows[0]["NoOfDecimalPlace"].ToString();
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ClearText();
            string Unit_id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpUnit",
                   new string[] { "flag", "Unit_id" },
                   new string[] { "3", Unit_id }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        txtUnit.Text = "";
        ddlQuantity_Type.ClearSelection();
        txtUqc_Code.Text = "";
        txtNoOfDecimal.Text = "";
        ViewState["Unit_id"] = "0";
        btnSave.Text = "Accept";
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridView1.PageIndex = e.NewPageIndex;
            ds = objdb.ByProcedure("SpUnit", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public string FirstLetterToUpper(string str)
    {
        string txt = cult.TextInfo.ToTitleCase(str.ToLower());
        return txt;
    }
}