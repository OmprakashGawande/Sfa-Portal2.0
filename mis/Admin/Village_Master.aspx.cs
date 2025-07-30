using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Village_Master : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure obj = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"].ToString() != null && Session["Emp_ID"].ToString() != "" && Session["Office_ID"].ToString() != null && Session["Office_ID"].ToString() != "")
        {
            if (!IsPostBack)
            {
                LblMsg.Text = "";
                ViewState["user"] = Session["Emp_ID"].ToString();
                GetDivisionDetails();
                FillVillageGrid();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                Selectddl();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
    #region User Defined Function and Selected Index change events
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void Selectddl()
    {
        ddlDistrict.Items.Clear();
        ddlBlock.Items.Clear();
        ddlRange.Items.Clear();
        ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
        ddlBlock.Items.Insert(0, new ListItem("Select", "0"));
        ddlRange.Items.Insert(0, new ListItem("Select", "0"));
    }
    //Fill Block Grid
    protected void FillVillageGrid()
    {
        try
        {
            grvVillageMaster.VirtualItemCount = GetRowCount();
            GetPageData(1, 100);
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
            ddlDivision.ClearSelection();
            ds = obj.ByProcedure("SpAdminDivision", new string[] { "flag" }, new string[] { "2" }, "dataset");
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
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillVillageGrid();
            ddlDistrict.Items.Clear();
            ddlRange.Items.Clear();
            ddlBlock.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            ddlRange.Items.Insert(0, new ListItem("Select", "0"));
            ddlBlock.Items.Insert(0, new ListItem("Select", "0"));
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
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LblMsg.Text = "";
            FillVillageGrid();
            ddlRange.Items.Clear();
            ddlBlock.Items.Clear();

            ddlRange.Items.Insert(0, new ListItem("Select", "0"));
            ddlBlock.Items.Insert(0, new ListItem("Select", "0"));
            if (ddlDistrict.SelectedIndex > 0)
            {
                FillRange();
            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    // Get Block Details
    protected void FillBlock()
    {
        try
        {
            LblMsg.Text = "";
            if (ddlDistrict.SelectedIndex > 0)
            {
                ddlBlock.Items.Clear();
                ds = obj.ByProcedure("Usp_tblAdminRangeMst_GetBlockDetails", new string[] { "Range_ID" }, new string[] { ddlRange.SelectedValue }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlBlock.DataTextField = "BlockName";
                    ddlBlock.DataValueField = "Block_ID";
                    ddlBlock.DataSource = ds.Tables[0];
                    ddlBlock.DataBind();

                }

            }
            ddlBlock.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    // Get Range Details
    protected void FillRange()
    {
        try
        {
            ddlRange.Items.Clear();
            ds = obj.ByProcedure("Usp_tblAdminVillageMst_GetRangeDetails", new string[] { "District_ID" }, new string[] { ddlDistrict.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlRange.DataTextField = "RangeName";
                ddlRange.DataValueField = "Range_ID";
                ddlRange.DataSource = ds.Tables[0];
                ddlRange.DataBind();

            }
            ddlRange.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //Clear Dropdownlist and Textbox
    protected void Clearddl()
    {
        //ddlDivision.ClearSelection();
        //ddlDistrict.Items.Clear();
        //ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
        //ddlBlock.Items.Clear();
        //ddlBlock.Items.Insert(0, new ListItem("Select", "0"));
        //ddlRange.Items.Clear();
        //ddlRange.Items.Insert(0, new ListItem("Select", "0"));
        txtVillageEnglish.Text = "";
        txtVillageHindi.Text = "";
        btnSave.Text = "Save";
    }
    #endregion
    #region Btn Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            LblMsg.Text = "";
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                //Btn Save Data
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("Usp_tblAdminVillageMst_Insert", new string[] { "Village_Name_E", "Village_Name_H", "Block_ID", "CreatedBy", "CreatedByIP" },
                                                                          new string[] { txtVillageEnglish.Text.Trim(), txtVillageHindi.Text.Trim(), ddlBlock.SelectedValue, ViewState["user"].ToString(), obj.GetLocalIPAddress() }, "dataset");
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
                        LblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                }
                //Btn Update Data
                else if (btnSave.Text == "Modify")
                {
                    ds = obj.ByProcedure("Usp_tblAdminVillageMst_Update", new string[] { "Village_Name_E", "Village_Name_H", "Block_ID", "LastUpdatedBy", "LastUpdatedByIP", "Village_ID" },
                                                                        new string[] { txtVillageEnglish.Text.Trim(), txtVillageHindi.Text.Trim(), ddlBlock.SelectedValue, ViewState["user"].ToString(), obj.GetLocalIPAddress(), ViewState["Id"].ToString() }, "dataset");
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
                        LblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                }
                FillVillageGrid();
                btnSave.Text = "Save";
                Clearddl();
            }
            else
            {
                LblMsg.Text = obj.Alert("fa-warning", "alert-warning", "Warning!", " Select Division Name");
            }
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }

        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //Village Is Active
    protected void grvVillageMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            LblMsg.Text = "";
            
            if (e.CommandName == "btnSelect")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblDivisionId = (Label)row.FindControl("lblDivisionId");
                Label lblDistrictId = (Label)row.FindControl("lblDistrictId");
                Label lblBlockId = (Label)row.FindControl("lblBlockId");
                Label lblRangeId = (Label)row.FindControl("lblRangeId");
                Label lblVillageNameE = (Label)row.FindControl("lblVillageNameE");
                Label lblVillageNameH = (Label)row.FindControl("lblVillageNameH");

                //Select Division
                ddlDivision.ClearSelection();
                if (lblDivisionId.Text != "")
                {
                    ddlDivision.Items.FindByValue(lblDivisionId.Text).Selected = true;
                }

                //Select District
                ddlDistrict.Items.Clear();
                FillDistrict();
                if (lblDistrictId.Text != "")
                {
                    ddlDistrict.Items.FindByValue(lblDistrictId.Text).Selected = true;
                }
                //Select Range
                ddlRange.Items.Clear();
                FillRange();
                if (lblRangeId.Text != "")
                {
                    ddlRange.Items.FindByValue(lblRangeId.Text).Selected = true;
                }
                //Select Block
                ddlBlock.Items.Clear();
                FillBlock();
                if (lblBlockId.Text != "")
                {
                    ddlBlock.Items.FindByValue(lblBlockId.Text).Selected = true;
                }



                //Select Text
                txtVillageEnglish.Text = lblVillageNameE.Text;
                txtVillageHindi.Text = lblVillageNameH.Text;

                ViewState["Id"] = e.CommandArgument.ToString();
                btnSave.Text = "Modify";
            }

        }
        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void chkIsActiveVillage_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            LblMsg.Text = "";
            CheckBox cbx = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cbx.NamingContainer;
            string Village_Id = grvVillageMaster.DataKeys[row.RowIndex].Value.ToString();
            string Activecbx = "";
            //ds = obj.ByProcedure("Usp_tblAdminVillageMst_GetRangeDetails", new string[] { "Village_ID" }, new string[] { Village_Id }, "dataset");
            //string village = ds.Tables[1].Rows[0]["VillageName"].ToString();
            if (cbx.Checked == true)
            {
                Activecbx = "1";
            }
            else
            {
                Activecbx = "0";

            }
            ds = obj.ByProcedure("Usp_tblAdminVillageMst_IsActive", new string[] { "Village_IsActive", "LastIsActiveBy", "LastIsActiveByIp", "Village_ID" },
                                                              new string[] { Activecbx, ViewState["user"].ToString(), obj.GetLocalIPAddress(), Village_Id }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
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
            FillVillageGrid();
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
    protected void ddlRange_SelectedIndexChanged(object sender, EventArgs e)
    {
        LblMsg.Text = "";
        try
        {
            FillVillageGrid();
            ddlBlock.Items.Clear();
            ddlBlock.Items.Insert(0, new ListItem("Select", "0"));
            if (ddlRange.SelectedIndex > 0)
            {
                FillBlock();
            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillVillageGrid();
    }
    protected void grvVillageMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            grvVillageMaster.PageIndex = e.NewPageIndex;

            GetPageData((e.NewPageIndex + 1), 100);
        }
        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetPageData(int currentPage, int pageSize)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = obj.ByProcedure("Usp_tblAdminVillageMst_FillVillageGrid", new string[] { "Division_ID", "District_ID", "Range_ID", "Block_ID", "PageSize", "PageNumber" },
                                                                            new string[] {ddlDivision.SelectedValue.ToString()
                                                                                ,ddlDistrict.SelectedValue.ToString()
                                                                                ,ddlRange.SelectedValue.ToString()
                                                                                ,ddlBlock.SelectedValue.ToString(),pageSize.ToString(), currentPage.ToString()}, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grvVillageMaster.DataSource = ds;
                    grvVillageMaster.DataBind();
                }
                else
                {
                    grvVillageMaster.DataSource = null;
                    grvVillageMaster.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds != null)
            {
                ds.Dispose();
            }
        }
    }
    protected int GetRowCount()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = obj.ByProcedure("Usp_tblAdminVillageMst_GetRowsCount", new string[] { }, new string[] { }, "dataset");

            return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        }
        catch (Exception ex)
        {
            LblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            return 0;
        }
        finally
        {
            if (ds != null)
            {
                ds.Dispose();
            }
        }
    }
}