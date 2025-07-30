using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Block_mst : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if (Session["user_id"].ToString() != null)
            //{
            //Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            lblmsg.Text = string.Empty;
            if (Session["Emp_ID"].ToString() != null && Session["Emp_ID"].ToString() != "" && Session["Office_ID"].ToString() != null && Session["Office_ID"].ToString() != "")
            {
                if (!IsPostBack)
                {
                    lblmsg.Text = "";
                    Session["Emp_ID"] = Session["Emp_ID"].ToString();
                    Session["Office_ID"] = Session["Office_ID"].ToString();
                    FillBlockDetails();
                    GetDivision();
                    ddlDistrict.Items.Insert(0, new ListItem("select", "0"));
                    ddlRange.Items.Insert(0, new ListItem("select", "0"));
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }

        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void FillBlockDetails()
    {
        try
        {
            GVBlock.DataSource = string.Empty;
            GVBlock.DataBind();

            ds = objdb.ByProcedure("USP_BlockDetails", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GVBlock.DataSource = ds.Tables[0];
                GVBlock.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetDivision()
    {
        try
        {
            ddlDivision.Items.Clear();
            ds = objdb.ByProcedure("SP_tblAdminDistrict_GetDiv", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDivision.DataTextField = "Division_Name";
                    ddlDivision.DataValueField = "Division_ID";
                    ddlDivision.DataSource = ds.Tables[0];
                    ddlDivision.DataBind();

                }
                ddlDivision.Items.Insert(0, new ListItem("select", "0"));

            }

        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void GVBlock_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            if (e.CommandName == "EditRecord")
            {

                Label lbl1 = (Label)row.FindControl("lbldivisioId");
                Label lbl2 = (Label)row.FindControl("lbldistrictId");
                Label lbl3 = (Label)row.FindControl("lblblockE");
                Label lbl4 = (Label)row.FindControl("lblblockH");
                Label RangeID = (Label)row.FindControl("RangeID");

                ddlDivision.SelectedValue = lbl1.Text;

                ddlDivision_SelectedIndexChanged(sender, e);
                ddlDistrict.SelectedValue = lbl2.Text;
                ddlDistrict_SelectedIndexChanged(sender, e);
                ddlRange.SelectedValue = RangeID.Text;
                txtBlockEnglish.Text = lbl3.Text;
                txtBlockHindi.Text = lbl4.Text;
                ViewState["Block_ID"] = e.CommandArgument.ToString();
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void chkactive_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            CheckBox cbx = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cbx.NamingContainer;
            string Block_Id = GVBlock.DataKeys[row.RowIndex].Value.ToString();
            string Activecbx = "";
            if (cbx.Checked == true)
            {
                Activecbx = "1";
            }
            else
            {
                Activecbx = "0";
            }
            ds = objdb.ByProcedure("USP_BlockActive", new string[] { "Block_ID", "Block_IsActive", "LastIsActiveBy", "LastIsActiveByIp" }, new string[] { Block_Id, Activecbx, Session["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                    {
                        lblmsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                    else
                    {
                        lblmsg.Text = objdb.Alert("fa-ban", "alert-info", "Sorry! ", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                }
            }
            FillBlockDetails();
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            if (btnSave.Text == "Save")
            {
                ds = objdb.ByProcedure("USP_InsertBlock", new string[] { "Range_ID", "Block_Name_Eng", "Block_Name_Hin", "CreatedBy", "CreatedByIP" },
                                                          new string[] { ddlRange.SelectedValue.ToString(), txtBlockEnglish.Text.Trim(), txtBlockHindi.Text.Trim(), Session["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            lblmsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            if (ds != null) { ds.Dispose(); }
                            FillBlockDetails();
                            ddlDivision.ClearSelection();
                            ddlDistrict.ClearSelection();
                            txtBlockEnglish.Text = string.Empty;
                            txtBlockHindi.Text = "";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            lblmsg.Text = objdb.Alert("fa-check", "alert-warning", "Warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Not Ok")
                        {
                            lblmsg.Text = objdb.Alert("fa-check", "alert-danger", "warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                    }

                }

            }
            if (btnSave.Text == "Update")
            {
                ds = objdb.ByProcedure("USP_BlockUpdate", new string[] { "Range_ID", "Block_Name_Eng", "Block_Name_Hin", "Block_ID", "LastUpdatedBy", "LastUpdatedByIP" },
                                                          new string[] { ddlRange.SelectedValue.ToString(), txtBlockEnglish.Text.Trim(), txtBlockHindi.Text.Trim(), ViewState["Block_ID"].ToString(), Session["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            lblmsg.Text = objdb.Alert("fa-check", "alert-success", "ThanYou", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            if (ds != null) { ds.Dispose(); }
                            FillBlockDetails();
                            btnSave.Text = "Save";
                            ddlDivision.ClearSelection();
                            ddlDistrict.Items.Clear();
                            ddlRange.Items.Clear();
                            ddlDistrict.Items.Insert(0, new ListItem("select", "0"));
                            ddlRange.Items.Insert(0, new ListItem("select", "0"));
                            txtBlockEnglish.Text = string.Empty;
                            txtBlockHindi.Text = "";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            lblmsg.Text = objdb.Alert("fa-check", "alert-warning", "Warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Not Ok")
                        {
                            lblmsg.Text = objdb.Alert("fa-check", "alert-danger", "warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                    }

                }
            }

        }

        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            ddlDivision.ClearSelection();
            ddlDistrict.ClearSelection();
            txtBlockEnglish.Text = string.Empty;
            txtBlockHindi.Text = "";
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-bam", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            ddlDistrict.Items.Clear();
            ds = objdb.ByProcedure("Usp_tblAdminRangeMst_GetDistrictDetails", new string[] { "Division_ID" }, new string[] { ddlDivision.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDistrict.DataTextField = "DistrictName";
                    ddlDistrict.DataValueField = "District_ID";
                    ddlDistrict.DataSource = ds.Tables[0];
                    ddlDistrict.DataBind();

                }
                ddlDistrict.Items.Insert(0, new ListItem("select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            ddlRange.Items.Clear();
            ds = objdb.ByProcedure("Usp_tblAdminRangeMst_ByDistrictID", new string[] { "District_ID" }, new string[] { ddlDistrict.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlRange.DataTextField = "Range_Name";
                    ddlRange.DataValueField = "Range_ID";
                    ddlRange.DataSource = ds.Tables[0];
                    ddlRange.DataBind();

                }
                ddlRange.Items.Insert(0, new ListItem("select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}


