using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Item_Unit_Mst : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            if (Session["Emp_ID"] != null && Session["Emp_ID"] != "" && Session["Office_ID"] != null && Session["Office_ID"] != "")
            {
                if (!IsPostBack)
                {
                  
                    Session["Emp_ID"] = Session["Emp_ID"].ToString();
                    Session["Office_ID"] = Session["Office_ID"].ToString();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    FillItemUnitGrid();

                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void GVItemUnit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            if (e.CommandName == "EditRecord")
            {
                Label lbl1 = (Label)row.FindControl("lblItemUnitE");
                Label lbl2 = (Label)row.FindControl("lblItemUnitH");

                txtUnitEnglish.Text = lbl1.Text;
                txtUnitHindi.Text = lbl2.Text;
                ViewState["Unit_id"] = e.CommandArgument.ToString();
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
            string  ItemUnitId = GVItemUnit.DataKeys[row.RowIndex].Value.ToString();
            string Activecbx = "";
            if (cbx.Checked == true)
            {
                Activecbx = "1";
            }
            else
            {
                Activecbx = "0";
            }
            ds = objdb.ByProcedure("USP_ItemUnitIsActive", new string[] { "Unit_id", "Unit_IsActive", "LastIsActiveBy", "LastIsActiveByIP" }, new string[] { ItemUnitId, Activecbx, Session["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        lblmsg.Text = objdb.Alert("fa-check", "alert-success", "Alert !", ErrMsg);
                    }

                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Not Ok")
                    {
                        lblmsg.Text = objdb.Alert("fa-ban", "alert-warning", "Alert !", ErrMsg);
                    }
                }
            }
            FillItemUnitGrid();
            
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void FillItemUnitGrid()
    {
        try
        {
             GVItemUnit.DataSource = "";
             GVItemUnit.DataBind();

             ds = objdb.ByProcedure("USP_ItemUnitGrid", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count>0)
                {
                    GVItemUnit.DataSource = ds.Tables[0];
                    GVItemUnit.DataBind();
                }
              
            }
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
            lblmsg.Text = string.Empty;
            if(Page.IsValid)
            {
                if(btnSave.Text=="Save")
                {
                    ds = objdb.ByProcedure("USP_ItemUnit_Insert", new string[] { "UnitName_Eng", "UnitName_Hin", "CreatedBy", "CreatedByIP" }, new string[] { txtUnitEnglish.Text, txtUnitHindi.Text, Session["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");
                    if(ds!=null && ds.Tables.Count>0)
                    {
                        if(ds.Tables[0].Rows.Count>0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {

                                lblmsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                if (ds != null) { ds.Dispose(); }
                                FillItemUnitGrid();
                                txtUnitEnglish.Text = string.Empty;
                                txtUnitHindi.Text = string.Empty;
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
                if(btnSave.Text=="Update")
                {
                    ds = objdb.ByProcedure("USP_ItemUnit_Update", new string[] { "UnitName_Eng", "UnitName_Hin", "LastUpdatedBy", "LastUpdatedByIP", "Unit_id" }, new string[] { txtUnitEnglish.Text, txtUnitHindi.Text, Session["Emp_ID"].ToString(), objdb.GetLocalIPAddress(), ViewState["Unit_id"].ToString() }, "dataset");
                    if(ds!=null && ds.Tables.Count>0)
                    {
                        if(ds.Tables[0].Rows.Count>0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {

                                lblmsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                if (ds != null) { ds.Dispose(); }
                                FillItemUnitGrid();
                                btnSave.Text = "Save";
                                txtUnitEnglish.Text = string.Empty;
                                txtUnitHindi.Text = string.Empty;
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
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
        
            txtUnitEnglish.Text = string.Empty;
            txtUnitHindi.Text = string.Empty;
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}