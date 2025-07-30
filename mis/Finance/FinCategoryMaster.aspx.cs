using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_FinCategoryMaster : System.Web.UI.Page
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
            GvCategory.DataSource = null;
            GvCategory.DataBind();
            ds = objdb.ByProcedure("SpFinCategoryMaster",
                      new string[] { "flag", "OfficeID" },
                      new string[] { "5", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GvCategory.DataSource = ds;
                GvCategory.DataBind();
                GvCategory.UseAccessibleHeader = true;
                GvCategory.HeaderRow.TableSection = TableRowSection.TableHeader;
                int i = 0;
                foreach (GridViewRow row in GvCategory.Rows)
                {
                    LinkButton lnkDelete = (LinkButton)row.FindControl("lnkDelete");
                    string ID = lnkDelete.CommandArgument.ToString();
                    if (ID == ds.Tables[0].Rows[i]["CatLedgerMapping"].ToString())
                    {
                        lnkDelete.Visible = false;
                    }
                    else
                    {
                        lnkDelete.Visible = true;
                    }
                    i = i + 1;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (txtCategory.Text == "")
            {
                msg += "Enter Category.\\n";
            }
            if (msg.Trim() == "")
            {
                if (btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("SpFinCategoryMaster",
                       new string[] { "flag", "CategoryName", "IsActive", "OfficeID", "CreatedBy", "CreatedIP" },
                       new string[] { "1", txtCategory.Text.Trim(), "1", ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), "" }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            txtCategory.Text = "";
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-Ban", "alert-warning", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    ds = objdb.ByProcedure("SpFinCategoryMaster",
                      new string[] { "flag", "CategoryId", "CategoryName", "IsActive", "OfficeID", "UpdatedBy", "UpdatedIP" },
                      new string[] { "2", ViewState["CategoryId"].ToString(), txtCategory.Text.Trim(), "1", ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), "" }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            txtCategory.Text = "";
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-Ban", "alert-warning", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                    }
                }
                txtCategory.Text = "";
                FillGrid();
                btnSave.Text = "Save";
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
    protected void GvCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["CategoryId"] = e.CommandArgument.ToString();
            if (e.CommandName == "EditRecord")
            {
                ds = objdb.ByProcedure("SpFinCategoryMaster",
                     new string[] { "flag", "CategoryId" },
                     new string[] { "4", ViewState["CategoryId"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtCategory.Text = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                    btnSave.Text = "Update";
                }
            }
            else if (e.CommandName == "DeleteRecord")
            {
                ds = objdb.ByProcedure("SpFinCategoryMaster",
                     new string[] { "flag", "CategoryId", "DeletedBy", "DeletedIP" },
                     new string[] { "3", ViewState["CategoryId"].ToString(), ViewState["Emp_ID"].ToString(), "" }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        FillGrid();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}