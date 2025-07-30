using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;

public partial class mis_Finance_SchemeMaster : System.Web.UI.Page
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
                    ViewState["SchemeTx_ID"] = "0";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillScheme();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillScheme()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinSchemeTx", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridViewSchemeDetail.DataSource = ds;
                GridViewSchemeDetail.DataBind();
                foreach(GridViewRow rows in GridViewSchemeDetail.Rows)
                {
                    LinkButton LnkDelete = (LinkButton)rows.FindControl("LnkDelete");
                    Label lblSchemeTx_ID = (Label)rows.FindControl("lblSchemeTx_ID");
                    ds = objdb.ByProcedure("[SpFinSchemeTx]", new string[] { "flag", "SchemeTx_ID" }, new string[] { "6", lblSchemeTx_ID.Text }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["status"].ToString() == "true")
                        {
                            LnkDelete.Visible = false;
                        }
                        else
                        {
                            LnkDelete.Visible = true;
                        }

                    }
                }
            }
            else
            {
                GridViewSchemeDetail.DataSource = new string[] { };
                GridViewSchemeDetail.DataBind();

            }
            GridViewSchemeDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridViewSchemeDetail.UseAccessibleHeader = true;

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
            Response.Redirect("SchemeMaster.aspx");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnAddScheme_Click(object sender, EventArgs e)
    {
        try
        {
            
            lblMsg.Text = "";
            string msg = "";
            string SchemeTx_IsActive = "1";
            if (txtSchemeTx_Name.Text == "")
            {
                msg += "Enter Scheme Name.";
            }
            if (msg == "")
            {
                int Status = 0;
                txtSchemeTx_Name.Text = FirstLetterToUpper(txtSchemeTx_Name.Text);
                ds = objdb.ByProcedure("SpFinSchemeTx", new string[] { "flag", "SchemeTx_Name", "SchemeTx_ID" }, new string[] { "5", txtSchemeTx_Name.Text, ViewState["SchemeTx_ID"].ToString() }, "dataset");
                if(ds!= null && ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (btnAddScheme.Text == "Add" && Status == 0 && ViewState["SchemeTx_ID"].ToString() == "0")
                {
                    objdb.ByProcedure("SpFinSchemeTx", new string[] { "flag", "SchemeTx_Name", "SchemeTx_IsActive", "SchemeTx_InsertedBy", "Office_ID" }, new string[] { "0", txtSchemeTx_Name.Text, SchemeTx_IsActive, ViewState["Emp_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                    FillScheme();
                    txtSchemeTx_Name.Text = "";
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else if (btnAddScheme.Text == "Update" && Status == 0 && ViewState["SchemeTx_ID"].ToString() != "0")
                {
                    objdb.ByProcedure("SpFinSchemeTx", new string[] { "flag", "SchemeTx_ID", "SchemeTx_Name", "Office_ID" }, new string[] { "3", ViewState["SchemeTx_ID"].ToString(), txtSchemeTx_Name.Text, ViewState["Office_ID"].ToString() }, "dataset");
                    FillScheme();
                    txtSchemeTx_Name.Text = "";
                    btnAddScheme.Text = "Add";
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Scheme Name Updated Successfully");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Scheme name is already exist.');", true);
                    lblMsg.Text = "";
                    FillScheme();
                    
                }
                ViewState["SchemeTx_ID"] = "0";
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
    protected void GridViewSchemeDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string SchemeTx_ID = GridViewSchemeDetail.SelectedDataKey.Value.ToString();
            ViewState["SchemeTx_ID"] = SchemeTx_ID;
            ds = objdb.ByProcedure("SpFinSchemeTx", new string[] { "flag", "SchemeTx_ID" }, new string[] { "2", SchemeTx_ID.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtSchemeTx_Name.Text = ds.Tables[0].Rows[0]["SchemeTx_Name"].ToString();
                btnAddScheme.Text = "Update";
            }
            GridViewSchemeDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridViewSchemeDetail.UseAccessibleHeader = true;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewSchemeDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string SchemeTx_ID = GridViewSchemeDetail.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpFinSchemeTx", new string[] { "flag", "SchemeTx_ID" }, new string[] { "4", SchemeTx_ID }, "dataset");
            FillScheme();
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