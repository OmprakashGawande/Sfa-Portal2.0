using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class mis_Masters_PackagingType : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = string.Empty;
            if (Session["Emp_ID"] != null && Session["Emp_ID"] != "" && Session["Office_ID"] != null && Session["Office_ID"] != "")
            {
                if(!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"];
                    ViewState["Office_ID"] = Session["Office_ID"];
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    FillGrid();
                        
                }
                
            }
            else
            {
                Response.Redirect("../login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void FillGrid()
    {
        try
        {
            GVPackagingtype.DataSource = string.Empty;
            GVPackagingtype.DataBind();

            ds = objdb.ByProcedure("USP_PackagingTypeGrid", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVPackagingtype.DataSource = ds.Tables[0];
                    GVPackagingtype.DataBind();
                }
            }
            else
            {
                GVPackagingtype.DataSource = string.Empty;
                GVPackagingtype.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void GVPackagingtype_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            if (e.CommandName == "EditRecord")
            {
                Label lbl1 = (Label)row.FindControl("lblpackagingtype");

                txtpackagetype.Text = lbl1.Text;
                ViewState["Packaging_Id"] = e.CommandArgument.ToString();
                btnsave.Text = "Update";
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
            string PackagingType_ID = GVPackagingtype.DataKeys[row.RowIndex].Value.ToString();
            string Activecbx = "";
            if (cbx.Checked == true)
            {
                Activecbx = "1";
            }
            else
            {
                Activecbx = "0";
            }
            ds = objdb.ByProcedure("USP_PackagingTypeIsActive", new string[] { "Packaging_Id", "PackagingType_IsActive", "LastIsActiveBy", "LastIsActiveByIp" }, new string[] { PackagingType_ID, Activecbx, Session["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");


            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        lblmsg.Text = objdb.Alert("fa-check", "alert-success", "Alert !", ErrMsg);
                    }

                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "NotOk")
                    {
                        lblmsg.Text = objdb.Alert("fa-ban", "alert-warning", "Alert !", ErrMsg);
                    }
                }
            }
            FillGrid();
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        try
        {
          txtpackagetype.Text = string.Empty;
           btnsave.Text = "Save";
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtsave_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = string.Empty;
            if(Page.IsValid)
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnsave.Text == "Save")
                    {
                        ds = objdb.ByProcedure("USP_PackagingType_Insert", new string[] { "Packaging_Type", "CreatedBy", "CreatedByIP", }, new string[] { txtpackagetype.Text,ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");
                    }

                    else if (btnsave.Text == "Update")
                    {
                        ds = objdb.ByProcedure("USP_PackagingType_Update", new string[] { "Packaging_Type", "LastUpdatedBy", "LastUpdatedByIP", "Packaging_Id" }, new string[] { txtpackagetype.Text, ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress(), ViewState["Packaging_Id"].ToString() }, "dataset");
                    }
                    if(ds!=null && ds.Tables.Count>0)
                    {
                        if(ds.Tables[0].Rows.Count>0)
                        {

                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                lblmsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());

                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                            {
                                lblmsg.Text = objdb.Alert("fa-check", "alert-warning", "Warning", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Not Ok")
                            {
                                lblmsg.Text = objdb.Alert("fa-check", "alert-danger", "warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                         
                        }
                    }
                }
            }
            FillGrid();
            txtpackagetype.Text = string.Empty;
            btnsave.Text = "Save";
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}