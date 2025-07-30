using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Range_mst : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    DataSet dsDFO = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"].ToString() != null && Session["Emp_ID"].ToString() != "" && Session["Office_ID"].ToString() != null && Session["Office_ID"].ToString() != "")
        {
            //Page Load
            if (!IsPostBack)
            {
                ViewState["user"] = Session["Emp_ID"].ToString();
                LblMsg.Text = "";
                GetDivisionDetails();
                FillRangeGrid();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                Selectddl();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }

    }
    #region GetDetails
    protected void Selectddl()
    {
        LblMsg.Text = "";
        ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
        //ddlBlock.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    //Fill Range Grid
    protected void FillRangeGrid()
    {
        try
        {
            grvRangeMaster.DataSource = string.Empty;
            grvRangeMaster.DataBind();

            ds = obj.ByProcedure("Usp_tblAdminRangeMst_FillRangeGrid", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grvRangeMaster.DataSource = ds;
                    grvRangeMaster.DataBind();
                }
                else
                {
                    grvRangeMaster.DataSource = null;
                    grvRangeMaster.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    // Get Division Details
    private void GetDivisionDetails()
    {
        try
        {
            LblMsg.Text = "";
            ddlDivision.ClearSelection();
            ds = obj.ByProcedure("SpAdminDivision", new string[] { "flag" }, new string[] {"2" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name_E";
                ddlDivision.DataValueField = "Division_ID";
                ddlDivision.DataSource = ds.Tables[0];
                ddlDivision.DataBind();

            }
            ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    // Division Selected Index Changed 
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LblMsg.Text = "";
            if (ddlDivision.SelectedIndex > 0)
            {
                FillDistrict();
            }

        }
        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //Get District Details
    protected void FillDistrict()
    {
        try
        {
            LblMsg.Text = "";
            ddlDistrict.Items.Clear();
            ds = obj.ByProcedure("SpAdminDistrict", new string[] { "flag", "Division_ID" }, new string[] { "9", ddlDivision.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_ID";
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataBind();

            }
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDFO()
    {
        try
        {
            LblMsg.Text = "";
            ddlDFO.Items.Clear();
            dsDFO = obj.ByProcedure("USP_Trade_DFO_DistrictWise", new string[] { "District_ID" }, new string[] { ddlDistrict.SelectedValue }, "dataset");
            if (dsDFO != null && dsDFO.Tables[0].Rows.Count > 0)
            {
                ddlDFO.DataTextField = "DFO_Name";
                ddlDFO.DataValueField = "DFO_ID";
                ddlDFO.DataSource = dsDFO.Tables[0];
                ddlDFO.DataBind();

            }
            ddlDFO.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    // District Selected Index Changed 
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LblMsg.Text = "";
            if (ddlDistrict.SelectedIndex > 0)
            {
                FillDFO();
                // FillBlock();
            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    // Get Block Details
    //protected void FillBlock()
    //{
    //    try
    //    {
    //        ddlBlock.Items.Clear();
    //        ds = obj.ByProcedure("Usp_tblAdminRangeMst_GetBlockDetails", new string[] { "District_ID" }, new string[] { ddlDistrict.SelectedValue }, "dataset");
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlBlock.DataTextField = "BlockName";
    //            ddlBlock.DataValueField = "Block_ID";
    //            ddlBlock.DataSource = ds.Tables[0];
    //            ddlBlock.DataBind();
    //            ddlBlock.Items.Insert(0, new ListItem("Select", "0"));
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }

    //}
    //Dropdownlist and textbox Clear
    protected void Clearddl()
    {
        ddlDivision.ClearSelection();
        ddlDistrict.Items.Clear();
        ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
        //ddlBlock.Items.Clear();
        //ddlBlock.Items.Insert(0, new ListItem("Select", "0"));
        txtRangeEnglish.Text = "";
        txtRangeHindi.Text = "";
        btnSave.Text = "Save";
    }
    #endregion


    #region Btn andCheckbox Events
    //Button Save data
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            LblMsg.Text = "";
            //if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            //{



                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("Usp_tblAdminRangeMst_Insert", new string[] { "Range_Name_E", "Range_Name_H", "CreatedBy", "CreatedByIP", "District_ID" ,"DFO_ID"},
                                                                        new string[] { txtRangeEnglish.Text.Trim(), txtRangeHindi.Text.Trim(), ViewState["user"].ToString(), obj.GetLocalIPAddress(), ddlDistrict.SelectedValue.ToString(),ddlDFO.SelectedValue }, "dataset");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        LblMsg.Text = obj.Alert("fa-check", "alert-success", "Thankyou!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                    {
                        LblMsg.Text = obj.Alert("fa-check", "alert-warning", "!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Not Ok")
                    {
                        LblMsg.Text = obj.Alert("fa-ban", "alert-warning", "!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                }

                //Button Update Data
                else if (btnSave.Text == "Modify")
                {
                    ds = obj.ByProcedure("Usp_tblAdminRangeMst_Update", new string[] { "Range_Name_E", "Range_Name_H", "LastUpdatedBy", "LastUpdatedByIP", "Range_ID", "District_ID", "DFO_ID" },
                                                                        new string[] { txtRangeEnglish.Text.Trim(), txtRangeHindi.Text.Trim(), ViewState["user"].ToString(), obj.GetLocalIPAddress(), ViewState["Id"].ToString(), ddlDistrict.SelectedValue.ToString().Trim(),ddlDFO.SelectedValue }, "dataset");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        LblMsg.Text = obj.Alert("fa-check", "alert-success", "Thankyou!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                    {
                        LblMsg.Text = obj.Alert("fa-check", "alert-warning", "!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Not Ok")
                    {
                        LblMsg.Text = obj.Alert("fa-ban", "alert-warning", "!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                }
                FillRangeGrid();
                btnSave.Text = "Save";
                Clearddl();
            //}
            //else
            //{
            //    LblMsg.Text = obj.Alert("fa-warning", "alert-warning", "Warning!", " Select Division Name");
            //}
            //Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Fill Dropdownlist And Textbox
    protected void grvRangeMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            LblMsg.Text = "";
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            if (e.CommandName == "btnSelect")
            {
                Label lblDivisionId = (Label)row.FindControl("lblDivisionId");
                Label lblDistrictId = (Label)row.FindControl("lblDistrictId");
                Label lblDFOId = (Label)row.FindControl("lblDFOId");
                Label lblBlockId = (Label)row.FindControl("lblBlockId");
                Label lblRangeNameE = (Label)row.FindControl("lblRangeNameE");
                Label lblRangeNameH = (Label)row.FindControl("lblRangeNameH");

                ddlDivision.ClearSelection();
                if (lblDivisionId.Text != "")
                {
                    ddlDivision.Items.FindByValue(lblDivisionId.Text).Selected = true;
                }
                FillDistrict();
                ddlDistrict.ClearSelection();
                if (lblDistrictId.Text != "")
                {
                    ddlDistrict.Items.FindByValue(lblDistrictId.Text).Selected = true;
                }
                FillDFO();
                if (lblDFOId.Text != "")
                {
                    ddlDFO.SelectedValue = lblDFOId.Text;
                }


                txtRangeEnglish.Text = lblRangeNameE.Text;
                txtRangeHindi.Text = lblRangeNameH.Text;

                ViewState["Id"] = e.CommandArgument.ToString();
                btnSave.Text = "Modify";
            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    // Range IsActive
    protected void chkActiveRange_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            LblMsg.Text = "";

            CheckBox cbx = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cbx.NamingContainer;
            string Range_Id = grvRangeMaster.DataKeys[row.RowIndex].Value.ToString();
            string Activecbx = "";
            //ds = obj.ByProcedure("Usp_tblAdminRangeMst_GetBlockDetails", new string[] { "Range_ID" }, new string[] { Range_Id }, "dataset");
            //string Range = ds.Tables[1].Rows[0]["RangeName"].ToString();
            if (cbx.Checked == true)
            {
                Activecbx = "1";

            }
            else
            {
                Activecbx = "0";

            }
            ds = obj.ByProcedure("Usp_tblAdminRangeMst_IsActive", new string[] { "Range_IsActive", "LastIsActiveBy", "LastIsActiveByIp", "Range_ID" },
                                                                  new string[] { Activecbx, ViewState["user"].ToString(), obj.GetLocalIPAddress(), Range_Id }, "dataset");
            if (ds != null && ds.Tables.Count > 0 )
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                    {
                        LblMsg.Text = obj.Alert("fa-check", "alert-success", "ThankYou", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                    else
                    {
                        LblMsg.Text = obj.Alert("fa-ban", "alert-info", "Sorry! ", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                }
            }
            FillRangeGrid();
        }
        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //Btn Clear
    protected void btnClear_Click(object sender, EventArgs e)
    {
        LblMsg.Text = "";
        Clearddl();
    }
    #endregion


}