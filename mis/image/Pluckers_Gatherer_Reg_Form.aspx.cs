using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Trade_Pluckers_Gatherer_Reg_Form : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds, ds1, dschild, dsPhad, dsp, dsR, dsC, dsCheck = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    IFormatProvider culture = new CultureInfo("en-US", true);
    static DataTable dt = new DataTable();
    static DataTable dtNew = new DataTable();
    static DataTable dtUpdate = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (objdb.createdBy() != null && objdb.Office_ID() != null)
            {
                if (!IsPostBack)
                {
                    ViewState["UserID"] = Session["Emp_ID"];
                    ViewState["UserOffice_ID"] = Session["Office_ID"];
                    ViewState["User_Role"] = Session["Designation_ID"];
                    ViewState["OfficeType_ID"] = Session["OfficeType_ID"];
                    //ViewState["UserID"] = "1";
                    //ViewState["UserOffice_ID"] = "1";
                    GetCast();
                    GetRelation();
                    GetBank();
                    Getdatavilage_byRange();
                    Getdataplucker();
                    GetPhad();
                    //ds = objdb.ByProcedure("USP_Trade_distirict_divisionof_office",
                    // new string[] { "Office_ID" },
                    //   new string[] { ViewState["UserOffice_ID"].ToString() }, "dataset");
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    ViewState["Division_ID"] = ds.Tables[0].Rows[0]["Division_ID"];
                    //    ViewState["District_ID"] = ds.Tables[0].Rows[0]["District_ID"];
                    //    ViewState["Block_ID"] = ds.Tables[0].Rows[0]["Block_ID"];
                    //}
                    //// txtSocietyRegDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    //GetOfficetype();
                    //RegistrationType();

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error ", ex.Message.ToString());
        }
    }
    protected void GetCast()
    {
        try
        {

            dsR = objdb.ByProcedure("USP_Trade_GetCast",
                 new string[] { },
                   new string[] { }, "dataset");
            if (dsR.Tables.Count > 0 && dsR.Tables[0].Rows.Count > 0)
            {
                // divgrid.Visible = true;
                ddlcast.DataTextField = "Cast_Name";
                ddlcast.DataValueField = "Cast_ID";
                ddlcast.DataSource = dsR.Tables[0];
                ddlcast.DataBind();
                ddlcast.Items.Insert(0, new ListItem("Select", "0"));


            }
            else
            {

                ddlcast.DataSource = null;
                ddlcast.DataBind();


            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error ", ex.Message.ToString());
        }


    }
    protected void GetRelation()
    {
        try
        {

            dsR = objdb.ByProcedure("USP_Trade_GetRelation",
                 new string[] { },
                   new string[] { }, "dataset");
            if (dsR.Tables.Count > 0 && dsR.Tables[0].Rows.Count > 0)
            {
                // divgrid.Visible = true;
                ddlRelation.DataTextField = "Relation_Name_Hi";
                ddlRelation.DataValueField = "Relation_Id";
                ddlRelation.DataSource = dsR.Tables[0];
                ddlRelation.DataBind();
                ddlRelation.Items.Insert(0, new ListItem("Select", "0"));

                ddlRelationNew.DataTextField = "Relation_Name_Hi";
                ddlRelationNew.DataValueField = "Relation_Id";
                ddlRelationNew.DataSource = dsR.Tables[0];
                ddlRelationNew.DataBind();
                ddlRelationNew.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {

                ddlRelation.DataSource = null;
                ddlRelation.DataBind();

                ddlRelationNew.DataSource = null;
                ddlRelationNew.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error ", ex.Message.ToString());
        }


    }
    protected void GetPhad()
    {
        try
        {
            if (Session["OfficeType_ID"].ToString() == "5")
            {
                dsPhad = objdb.ByProcedure("USP_Trade_GetAllPhud_PhudMunsiwise",
                     new string[] { "Phud_Munsi_ID" },
                       new string[] { ViewState["UserID"].ToString() }, "dataset");
            }
            else if (Session["OfficeType_ID"].ToString() == "4")
            {
                dsPhad = objdb.ByProcedure("USP_Trade_GetAllPhud_PCSwise",
                     new string[] { "PCS_ID" },
                       new string[] { Session["Office_ID"].ToString() }, "dataset");
            }
            if (dsPhad.Tables.Count > 0 && dsPhad.Tables[0].Rows.Count > 0)
            {
                // divgrid.Visible = true;
                ddlPhadName.DataTextField = "Phad_Name";
                ddlPhadName.DataValueField = "Phad_ID";
                ddlPhadName.DataSource = dsPhad.Tables[0];
                ddlPhadName.DataBind();
                //ddlPhadName.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                //  divgrid.Visible = false;
                ddlPhadName.DataSource = null;
                ddlPhadName.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error ", ex.Message.ToString());
        }


    }
    protected void Getdataplucker()
    {
        try
        {
            Repeater1.DataSource = null;
            Repeater1.DataBind();
            btnExport.Visible = false;
            dsp = objdb.ByProcedure("USP_Trade_PluckerRegistration_Select",
                 new string[] { "UserID", "OfficeType_ID", "Office_ID" },
                   new string[] { ViewState["UserID"].ToString(), Session["OfficeType_ID"].ToString(), Session["Office_ID"].ToString() }, "dataset");
            if (dsp.Tables.Count > 0 && dsp.Tables[0].Rows.Count > 0)
            {
                // divgrid.Visible = true;
                GridViewPlucker.DataSource = dsp.Tables[0];
                GridViewPlucker.DataBind();
                Repeater1.DataSource = dsp.Tables[0];
                Repeater1.DataBind();
                btnExport.Visible = true;
            }
            else
            {
                //  divgrid.Visible = false;
                GridViewPlucker.DataSource = null;
                GridViewPlucker.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error ", ex.Message.ToString());
        }


    }
    protected void Getdatavilage_byRange()
    {
        try
        {
            string a = ViewState["UserOffice_ID"].ToString();
            //if (ViewState["OfficeType_ID"].ToString() == "4")
            //{

            //    ds = objdb.ByProcedure("USP_Trade_Mst_village_Rangewise",
            //         new string[] { "PCS_ID", "OfficeType_ID" },
            //           new string[] { ViewState["UserOffice_ID"].ToString(), ViewState["OfficeType_ID"].ToString() }, "dataset");
            //}
            //else if (ViewState["OfficeType_ID"].ToString() == "5")
            //{

            //    ds = objdb.ByProcedure("USP_Trade_Mst_village_Rangewise",
            //         new string[] { "Phad_ID", "OfficeType_ID" },
            //           new string[] { ViewState["UserOffice_ID"].ToString(), ViewState["OfficeType_ID"].ToString() }, "dataset");
            //}

            ds = objdb.ByProcedure("USP_Trade_Mst_village_Rangewise",
                     new string[] { "Office_ID" },
                       new string[] { ViewState["UserOffice_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlvillage.DataTextField = "Village_Name";
                ddlvillage.DataValueField = "Village_ID";
                ddlvillage.DataSource = ds.Tables[0];
                ddlvillage.DataBind();
                ddlvillage.Items.Insert(0, new ListItem("Select", "0"));
                ViewState["Range_ID"] = ds.Tables[1].Rows[0]["Range_ID"];
            }
            else
            {
                ddlvillage.DataSource = null;
                ddlvillage.DataBind();
                ddlvillage.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error", ex.Message.ToString());
        }


    }
    protected void GetBank()
    {
        try
        {
            lblMsg.Text = string.Empty;
            ddlBankName.DataTextField = "BankName";
            ddlBankName.DataValueField = "B_Id";
            ddlBankName.DataSource = objdb.ByProcedure("USP_Trade_SelectBank",
                 new string[] { },
                   new string[] { }, "dataset");
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error", ex.Message.ToString());
        }


    }
    protected void BindGrid()
    {
        Gridfamilymember.DataSource = ViewState["dt"] as DataTable;
        Gridfamilymember.DataBind();
        dt.Clear();
        dt = ViewState["dt"] as DataTable;
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
           dsCheck.Dispose();
            lblMsg.Text = "";
            if (txtAadharNo.Text.Trim() != txtadharno.Text.Trim())
            {
                if (txtChildSamagraID.Text.Trim() != txtsamagraid.Text.Trim())
                {
                    dsCheck = objdb.ByProcedure("USP_Trade_Plucker_Check_Child_AdharandSamagra_ID",
                        new string[] { "Adhar_No", "Samagra_ID" },
                        new string[] { txtAadharNo.Text, txtChildSamagraID.Text },
                                      "dataset");
                    if (dsCheck.Tables.Count > 0 && dsCheck.Tables[0].Rows[0]["Msg"].ToString() == "ok" && dsCheck.Tables[0].Rows[0]["ErrorMsg"].ToString() == "Data Unique")
                    {
                        int i = Gridfamilymember.Rows.Count;
                        if (i == 0)
                        {

                            if (dt.Columns.Count == 0)
                            {
                                //dt.Columns.Add("SNo");
                                dt.Columns.Add("PLKR_ChildName_H");
                                dt.Columns.Add("PLKR_Child_Age");
                                dt.Columns.Add("PLKR_Relation");
                                dt.Columns.Add("Relation_Id");
                                dt.Columns.Add("Mobile_No");
                                dt.Columns.Add("PLKR_ChildAdhar_No");
                                dt.Columns.Add("Account_No");
                                dt.Columns.Add("Samagra_ID");

                            }
                            dt.Rows.Add(txtMember_Name.Text, txtChildAge.Text, ddlRelation.SelectedItem.Text, ddlRelation.SelectedValue, "", txtAadharNo.Text, "", txtChildSamagraID.Text);
                            ViewState["dt"] = dt;
                            Gridfamilymember.DataSource = dt;
                            Gridfamilymember.DataBind();
                            if (Gridfamilymember.Rows.Count > 0)
                            {
                                divfamilymember.Visible = true;
                            }
                            else
                            {
                                divfamilymember.Visible = false;
                            }
                            txtAadharNo.Text = "";
                            txtChildSamagraID.Text = "";
                            txtMember_Name.Text = "";
                            txtChildAge.Text = "";
                            ddlRelation.SelectedIndex = 0;
                        }
                        else
                        {
                            dt.Rows.Add(txtMember_Name.Text, txtChildAge.Text, ddlRelation.SelectedItem.Text, ddlRelation.SelectedValue, "", txtAadharNo.Text, "", txtChildSamagraID.Text);
                            ViewState["dt"] = dt;
                            Gridfamilymember.DataSource = dt;
                            Gridfamilymember.DataBind();
                            if (Gridfamilymember.Rows.Count > 0)
                            {
                                divfamilymember.Visible = true;
                            }
                            else
                            {
                                divfamilymember.Visible = false;
                            }
                            txtAadharNo.Text = "";
                            txtChildSamagraID.Text = "";
                            txtMember_Name.Text = "";
                            txtChildAge.Text = "";
                            ddlRelation.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        string error = dsCheck.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Info", error);

                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa fa-exclamation-triangle", "alert-warning", "Info", "Samagra ID is Same as parent Samagra ID");
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa fa-exclamation-triangle", "alert-warning", "Info", "Adhar No. is Same as parent Adhar No.");
            }
            //TxtPoDate.Enabled = false;
            //ddlSection.Enabled = false;
            //FillDropdown();
            //Clear();
            //}
        }
        catch (Exception Ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Error", Ex.Message);
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", objdb.Alert("fa-exclamation", "alert-danger", "Sorry!", ex.Message), true);
        }
        finally
        {
        }
    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridViewPlucker.Rows.Count > 0)
            {
                GridViewPlucker.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridViewPlucker.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (dt.Columns.Count == 0)
                {
                    //dt.Columns.Add("SNo");
                    dt.Columns.Add("PLKR_ChildName_H");
                    dt.Columns.Add("PLKR_Child_Age");
                    dt.Columns.Add("PLKR_Relation");
                    dt.Columns.Add("Relation_Id");
                    dt.Columns.Add("Mobile_No");
                    dt.Columns.Add("PLKR_ChildAdhar_No");
                    dt.Columns.Add("Account_No");
                    dt.Columns.Add("Samagra_ID");

                }

                lblMsg.Text = "";
                ds = objdb.ByProcedure("USP_Trade_PluckerRegistration_Insert",
                    new string[] { "PLKRName_H", "PLKR_Age", "PLKR_Cast","Cast_ID", "PLKR_RegNo", "Village_ID",
                        "Mobile_No", "Adhar_No", "Office_ID","IsActive","CreatedBy","CreatedByIP","Samagra_ID","BPL_Card_no","Main_samagra_ID","Bank_ID"
                                ,"Branch_Name","IFSC_Code","Bank_Account_No","Acount_Holder_Name","Samble_ID" },
                    new string[] {txtMukhiyaname.Text,txtage.Text, ddlcast.SelectedItem.Text,ddlcast.SelectedValue,txtRegno.Text,ddlvillage.SelectedValue
                                ,txtmobileno.Text,txtadharno.Text,ddlPhadName.SelectedValue,"1",ViewState["UserID"].ToString(),objdb.GetLocalIPAddress()
                                 ,txtsamagraid.Text,txtbplcardno.Text,txtMainSamagraID.Text,ddlBankName.SelectedValue,
                                txtBranchName.Text,txtIFSCCode.Text,txtbankaccountNo.Text,txtAccountHolderName.Text,txtSamble_ID.Text},
                                 new string[] { "Type_Trade_PluckerRegistration_Child" },
                               new DataTable[] { dt },
                               "dataset");

                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                {

                    string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    Getdataplucker();
                    GetDatatableHeaderDesign();
                    clear();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                }
                else
                {
                    string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    if (error == "Data Already Exists")
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Info " + error);
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                    }
                }

            }
            if (btnSave.Text == "Update")
            {
                lblMsg.Text = "";
                // Session["Office_ID"] = "1";
                ds = objdb.ByProcedure("USP_Trade_PluckerRegistration_Update",
                   new string[] {"PLKR_ID" ,"PLKRName_H", "PLKR_Age", "PLKR_Cast","Cast_ID", "PLKR_RegNo", "Village_ID",
                        "Mobile_No", "Adhar_No","Office_ID","CreatedBy","CreatedByIP","Samagra_ID","BPL_Card_no","Main_samagra_ID","Bank_ID"
                                ,"Branch_Name","IFSC_Code","Bank_Account_No","Acount_Holder_Name","Samble_ID" },
                    new string[] {ViewState["PLKR_ID"].ToString(),txtMukhiyaname.Text,txtage.Text, ddlcast.SelectedItem.Text,ddlcast.SelectedValue,txtRegno.Text,ddlvillage.SelectedValue
                                ,txtmobileno.Text,txtadharno.Text,ddlPhadName.SelectedValue,ViewState["UserID"].ToString(),objdb.GetLocalIPAddress()
                                 ,txtsamagraid.Text,txtbplcardno.Text,txtMainSamagraID.Text,ddlBankName.SelectedValue,
                                txtBranchName.Text,txtIFSCCode.Text,txtbankaccountNo.Text,txtAccountHolderName.Text,txtSamble_ID.Text},
                                "TableUpdate");

                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                {
                    string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                    Getdataplucker();
                    GetDatatableHeaderDesign();
                    clear();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                }
                else
                {
                    string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    if (error == "Already Exists.")
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "info " + error);
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                }
            }
            // Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void GridViewPlucker_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblmsgModal.Text = string.Empty;
            lblMsg.Text = string.Empty;
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {

                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblPLKR_ID = (Label)row.FindControl("lblPLKR_ID");
                    Label lblPLKR_RegNo = (Label)row.FindControl("lblPLKR_RegNo");
                    Label lblPLKRName_H = (Label)row.FindControl("lblPLKRName_H");
                    Label lblPLKR_Age = (Label)row.FindControl("lblPLKR_Age");
                    Label lblPLKR_Cast = (Label)row.FindControl("lblPLKR_Cast");
                    Label lblVillage_Name = (Label)row.FindControl("lblVillage_Name");
                    // lblDCoDE.Text = lblDCode.Text;//for self superstockist
                    Label lblVillage_ID = (Label)row.FindControl("lblVillage_ID");
                    Label lblAdhar_No = (Label)row.FindControl("lblAdhar_No");
                    Label lblMobile_No = (Label)row.FindControl("lblMobile_No");
                    Label lblSamagra_ID = (Label)row.FindControl("lblSamagra_ID");
                    Label lblMain_samagra_ID = (Label)row.FindControl("lblMain_samagra_ID");
                    Label lblBPL_Card_no = (Label)row.FindControl("lblBPL_Card_no");
                    Label lblBank_ID = (Label)row.FindControl("lblBank_ID");
                    Label lblBranch_Name = (Label)row.FindControl("lblBranch_Name");
                    Label lblIFSC_Code = (Label)row.FindControl("lblIFSC_Code");
                    Label lblBank_Account_No = (Label)row.FindControl("lblBank_Account_No");
                    Label lblAcount_Holder_Name = (Label)row.FindControl("lblAcount_Holder_Name");
                    Label lblSamble_ID = (Label)row.FindControl("lblSamble_ID");
                    Label lblPhad_ID = (Label)row.FindControl("lblPhad_ID");
                    Label lblCast_ID = (Label)row.FindControl("lblCast_ID");

                    txtMukhiyaname.Text = lblPLKRName_H.Text;
                    txtage.Text = lblPLKR_Age.Text;

                    txtRegno.Text = lblPLKR_RegNo.Text;


                    txtmobileno.Text = lblMobile_No.Text;
                    txtadharno.Text = lblAdhar_No.Text;

                    txtsamagraid.Text = lblSamagra_ID.Text;
                    txtbplcardno.Text = lblBPL_Card_no.Text;
                    txtSamble_ID.Text = lblSamble_ID.Text;
                    txtMainSamagraID.Text = lblMain_samagra_ID.Text;


                    txtAccountHolderName.Text = lblAcount_Holder_Name.Text;
                    txtbankaccountNo.Text = lblBank_Account_No.Text;
                    txtBranchName.Text = lblBranch_Name.Text;
                    txtIFSCCode.Text = lblIFSC_Code.Text;

                    GetCast();
                    ddlcast.SelectedValue = lblCast_ID.Text;
                    // ddlcast.SelectedItem.Text = lblPLKR_Cast.Text;
                    GetBank();
                    ddlBankName.SelectedValue = lblBank_ID.Text;
                    Getdatavilage_byRange();
                    ddlvillage.SelectedValue = lblVillage_ID.Text;
                    GetPhad();
                    ddlPhadName.SelectedValue = lblPhad_ID.Text;


                    btnSave.Text = "Update";
                    ddlPhadName.Enabled = false;
                    ViewState["PLKR_ID"] = lblPLKR_ID.Text;
                    ds = objdb.ByProcedure("USP_Trade_PluckerRegistration_Child_Select",
                    new string[] { "PLKR_ID" },
                   new string[] { ViewState["PLKR_ID"].ToString() }, "dataset");

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        divfamilymember.Visible = true;
                        Gridfamilymember.DataSource = ds;
                        Gridfamilymember.DataBind();
                        divfamilymemberdetail.Visible = false;
                        Gridfamilymember.Columns[6].Visible = false;
                    }
                    else
                    {
                        divfamilymember.Visible = false;
                        divfamilymemberdetail.Visible = false;
                        Gridfamilymember.DataSource = null;
                        Gridfamilymember.DataBind();

                    }
                    GetDatatableHeaderDesign();
                }

            }
            if (e.CommandName == "RecordDelete")
            {
                Control ctrl = e.CommandSource as Control;
                string status = "";
                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                LinkButton lnkDelete = (LinkButton)row.FindControl("lnkDelete");
                if (lnkDelete.Text == "Active")
                {
                    status = "0";
                }
                else if (lnkDelete.Text == "Deactive")
                {
                    status = "1";
                }
                ViewState["PLKR_ID"] = e.CommandArgument;
                ds = objdb.ByProcedure("USP_Trade_PluckerRegistration_Delete",
                            new string[] { "PLKR_ID", "IsActive", "CreatedBy", "CreatedByIP" },
                            new string[] { ViewState["PLKR_ID"].ToString(),status, ViewState["UserID"].ToString(),objdb.GetLocalIPAddress()
                              }, "TableSave");

                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                {
                    string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                    GetDatatableHeaderDesign();
                    Getdataplucker();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                }
                else
                {
                    string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                }
                ds.Clear();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            else if (e.CommandName == "View")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {

                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblPLKR_ID = (Label)row.FindControl("lblPLKR_ID");

                    Label lblPLKRName_H = (Label)row.FindControl("lblPLKRName_H");
                    ViewState["PLKR_ID"] = e.CommandArgument;
                    ds = objdb.ByProcedure("USP_Trade_PluckerRegistration_Child_Select",
                   new string[] { "PLKR_ID" },
                  new string[] { lblPLKR_ID.Text }, "dataset");

                    if (ds.Tables.Count > 1 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[1].Rows[0]["Msg"].ToString() == "ok")
                        {
                            lblMukhiyaname.Text = lblPLKRName_H.Text;
                            gridFamilymemberdetail.DataSource = ds.Tables[0];
                            gridFamilymemberdetail.DataBind();
                            gridFamilymemberdetail.Visible = true;

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal();", true);
                        }
                    }
                    else
                    {

                        gridFamilymemberdetail.DataSource = null;
                        gridFamilymemberdetail.DataBind();
                        gridFamilymemberdetail.Visible = false;
                        string error = "There is no Member in this family";
                        // string error = "परिवार के सदस्य जोड़े नहीं गए";
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Error :" + error);

                    }

                    // }



                }


            }
            else if (e.CommandName == "ChildUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {

                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblPLKR_ID = (Label)row.FindControl("lblPLKR_ID");

                    Label lblPLKRName_H = (Label)row.FindControl("lblPLKRName_H");
                    ViewState["PLKR_ID"] = e.CommandArgument;
                    ds = objdb.ByProcedure("USP_Trade_PluckerRegistration_Child_Select",
                   new string[] { "PLKR_ID" },
                  new string[] { lblPLKR_ID.Text }, "dataset");

                    if (ds.Tables.Count > 1 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[1].Rows[0]["Msg"].ToString() == "ok")
                        {
                            lblMukhiyaname.Text = lblPLKRName_H.Text;
                            GridViewchildupdate.DataSource = ds.Tables[0];
                            GridViewchildupdate.DataBind();
                            GridViewchildupdate.Visible = true;

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalmamberupdate();", true);
                        }
                    }
                    else
                    {

                        GridViewchildupdate.DataSource = null;
                        GridViewchildupdate.DataBind();
                        GridViewchildupdate.Visible = false;
                        string error = "There is no Member in this family";
                        //string error = "परिवार के सदस्य जोड़े नहीं गए";
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Error :" + error);

                    }

                    // }



                }


            }
            else if (e.CommandName == "RecordAdd")
            {
                ClearNew();
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    divfamilymemberNew.Visible = false;
                    btnSaveNew.Visible = false;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblPLKR_ID = (Label)row.FindControl("lblPLKR_ID");


                    ViewState["PLKR_ID"] = e.CommandArgument;


                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalADD();", true);


                    // }



                }
            }





        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void OnDelete(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
        DataTable dt = ViewState["dt"] as DataTable;
        dt.Rows.RemoveAt(row.RowIndex);
        ViewState["dt"] = dt;
        BindGrid();
        if (Gridfamilymember.Rows.Count > 0)
        {
            divfamilymember.Visible = true;
        }
        else
        {
            divfamilymember.Visible = false;
            dt.Clear();
            dt.Columns.Clear();
        }
    }



    protected void gridFamilymemberdetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            lblmsgModal.Text = string.Empty;
            if (e.CommandName == "RecordDelete")
            {
                Control ctrl = e.CommandSource as Control;
                string status = "";
                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                LinkButton lnkDeletechild = (LinkButton)row.FindControl("lnkDelete");
                Label lblPLKR_ID = (Label)row.FindControl("lblPLKR_ID");
                //if (lnkDeletechild.Text == "Active")
                //{
                //    status = "0";
                //}
                //else if (lnkDeletechild.Text == "Deactive")
                //{
                //    lblPLKR_Child_ID = "1";
                //}
                ViewState["PLKR_Child_ID"] = e.CommandArgument;
                ds = objdb.ByProcedure("USP_Trade_PluckerRegistration_Child_Delete",
                            new string[] { "PLKR_Child_ID", "IsActive", "CreatedBy", "CreatedByIP" },
                            new string[] { ViewState["PLKR_Child_ID"].ToString(),"0", ViewState["UserID"].ToString(),objdb.GetLocalIPAddress()
                              }, "TableSave");

                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                {

                    dschild = objdb.ByProcedure("USP_Trade_PluckerRegistration_Child_Select",
                  new string[] { "PLKR_ID" },
                 new string[] { lblPLKR_ID.Text }, "dataset");

                    if (dschild.Tables.Count > 1 && dschild.Tables[0].Rows.Count > 0)
                    {
                        if (dschild.Tables[1].Rows[0]["Msg"].ToString() == "ok")
                        {
                            //  lblMukhiyaname.Text = lblPLKRName_H.Text;
                            gridFamilymemberdetail.DataSource = dschild.Tables[0];
                            gridFamilymemberdetail.DataBind();
                            gridFamilymemberdetail.Visible = true;
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal();", true);
                            lblmsgModal.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                    }
                    else
                    {

                        gridFamilymemberdetail.DataSource = null;
                        gridFamilymemberdetail.DataBind();
                        gridFamilymemberdetail.Visible = false;
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString() + ". No Member in Family Now";
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                    }
                }
                else
                {
                    string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                }
                ds.Clear();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }





        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void BindGridNew()
    {
        GridfamilymemberNew.DataSource = ViewState["dtNew"] as DataTable;
        GridfamilymemberNew.DataBind();
        dtNew.Clear();
        dtNew = ViewState["dtNew"] as DataTable;
    }
    protected void BindGridUpdate()
    {
        GridViewchildupdate.DataSource = ViewState["dtUpdate"] as DataTable;
        GridViewchildupdate.DataBind();
        dtUpdate.Clear();
        dtUpdate = ViewState["dtUpdate"] as DataTable;
    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsgmodalNew.Text="";
            //dsCheck.Clear();
            dsCheck = objdb.ByProcedure("USP_Trade_Plucker_Check_Child_AdharandSamagra_ID",
                       new string[] { "Adhar_No", "Samagra_ID" },
                       new string[] { txtAadharNoNew.Text, txtChildSamagraIDNew.Text },
                                     "dataset");
            if (dsCheck.Tables.Count > 0 && dsCheck.Tables[0].Rows[0]["Msg"].ToString() == "ok" && dsCheck.Tables[0].Rows[0]["ErrorMsg"].ToString() == "Data Unique")
            {
                int i = GridfamilymemberNew.Rows.Count;
                if (i == 0)
                {
                    if (dtNew.Columns.Count == 0)
                    {
                        //dt.Columns.Add("SNo");
                        dtNew.Columns.Add("PLKR_ChildName_H");
                        dtNew.Columns.Add("PLKR_Child_Age");
                        dtNew.Columns.Add("PLKR_Relation");
                        dtNew.Columns.Add("Relation_Id");
                        dtNew.Columns.Add("Mobile_No");
                        dtNew.Columns.Add("PLKR_ChildAdhar_No");
                        dtNew.Columns.Add("Account_No");
                        dtNew.Columns.Add("Samagra_ID");

                    }
                    dtNew.Rows.Add(txtMember_NameNew.Text, txtChildAgeNew.Text, ddlRelationNew.SelectedItem.Text, ddlRelationNew.SelectedValue, "", txtAadharNoNew.Text, "", txtChildSamagraIDNew.Text);
                    ViewState["dtNew"] = dtNew;
                    GridfamilymemberNew.DataSource = dtNew;
                    GridfamilymemberNew.DataBind();
                    if (GridfamilymemberNew.Rows.Count > 0)
                    {
                        divfamilymemberNew.Visible = true;
                        btnSaveNew.Visible = true;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalADD();", true);
                    }
                    else
                    {
                        divfamilymemberNew.Visible = false;
                        btnSaveNew.Visible = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalADD();", true);
                    }
                    txtAadharNoNew.Text = "";
                    txtChildSamagraIDNew.Text = "";
                    txtMember_NameNew.Text = "";
                    txtChildAgeNew.Text = "";
                    ddlRelationNew.SelectedIndex = 0;
                }
                else
                {
                    dtNew.Rows.Add(txtMember_NameNew.Text, txtChildAgeNew.Text, ddlRelationNew.SelectedItem.Text, ddlRelationNew.SelectedValue, "", txtAadharNoNew.Text, "", txtChildSamagraIDNew.Text);
                    ViewState["dtNew"] = dtNew;
                    GridfamilymemberNew.DataSource = dtNew;
                    GridfamilymemberNew.DataBind();
                    if (GridfamilymemberNew.Rows.Count > 0)
                    {
                        divfamilymemberNew.Visible = true;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalADD();", true);
                    }
                    else
                    {
                        divfamilymemberNew.Visible = false;
                    }
                    txtAadharNoNew.Text = "";
                    txtChildSamagraIDNew.Text = "";
                    txtMember_NameNew.Text = "";
                    txtChildAgeNew.Text = "";
                    ddlRelationNew.SelectedIndex = 0;
                }
            }
            else
            {
                string error = dsCheck.Tables[0].Rows[0]["ErrorMsg"].ToString();

                lblmsgmodalNew.Text = objdb.Alert("fa-ban", "alert-warning", "Info", error);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalADD();", true);
            }
            //TxtPoDate.Enabled = false;
            //ddlSection.Enabled = false;
            //FillDropdown();
            //Clear();
            //}
        }
        catch (Exception Ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Error", Ex.Message);
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", objdb.Alert("fa-exclamation", "alert-danger", "Sorry!", ex.Message), true);
        }
        finally
        {
        }
    }
    protected void OnDeleteNew(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
        DataTable dtNew = ViewState["dtNew"] as DataTable;
        dtNew.Rows.RemoveAt(row.RowIndex);
        ViewState["dtNew"] = dtNew;
        BindGridNew();
        if (GridfamilymemberNew.Rows.Count > 0)
        {
            GridfamilymemberNew.Visible = true;
        }
        else
        {
            GridfamilymemberNew.Visible = false;
            dtNew.Clear();
            dtNew.Columns.Clear();
        }
    }
    protected void btnSaveNew_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSaveNew.Text == "Save New Member")
            {
                if (dtNew.Rows.Count > 0)
                {
                    lblMsg.Text = objdb.GetLocalIPAddress();
                    //dtNew = ViewState["dtNew"]; 
                    ds = objdb.ByProcedure("USP_Trade_PluckerRegistration_NewChild_Insert",
                        new string[] { "PLKR_ID", "CreatedBy", "CreatedByIP" },
                        new string[] { ViewState["PLKR_ID"].ToString(), ViewState["UserID"].ToString(), objdb.GetLocalIPAddress() },
                                     new string[] { "Type_Trade_PluckerRegistration_Child" },
                                   new DataTable[] { dtNew },
                                   "dataset");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                    {

                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        Getdataplucker();
                        GetDatatableHeaderDesign();
                        ClearNew();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        if (error == "Data Already Exists")
                        {
                            lblMsg.Text = objdb.Alert("fa fa-exclamation-triangle", "alert-warning", "Warning!", "Info " + error);
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa fa-exclamation-triangle", "alert-warning", "Warning!", "" + error);
                        }
                    }

                }
                else
                {
                    string error = "There is no Member to Add";
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                }
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Error", Ex.Message);
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", objdb.Alert("fa-exclamation", "alert-danger", "Sorry!", ex.Message), true);
        }
        finally
        {
        }

    }
    protected void clear()
    {
        try
        {


            txtMukhiyaname.Text = "";
            txtage.Text = "";

            txtRegno.Text = "";
            txtadharno.Text = "";
            txtMainSamagraID.Text = "";
            txtsamagraid.Text = "";
            txtSamble_ID.Text = "";
            txtmobileno.Text = "";

            txtbplcardno.Text = "";



            txtAccountHolderName.Text = "";
            txtbankaccountNo.Text = "";
            txtBranchName.Text = "";
            txtIFSCCode.Text = "";


            GetBank();
            ddlBankName.SelectedIndex = 0;
            Getdatavilage_byRange();
            ddlvillage.SelectedIndex = 0;

            txtMember_Name.Text = "";
            txtAadharNo.Text = "";
            txtChildSamagraID.Text = "";
            GetRelation();
            ddlRelation.SelectedIndex = 0;
            txtChildAge.Text = "1";
            Gridfamilymember.DataSource = null;
            Gridfamilymember.DataBind();
            divfamilymemberdetail.Visible = true;



            btnSave.Text = "Save";
            ddlPhadName.Enabled = true;
            ViewState["PLKR_ID"] = "";
            dt.Clear();
            dt.Columns.Clear();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Error", Ex.Message);
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", objdb.Alert("fa-exclamation", "alert-danger", "Sorry!", ex.Message), true);
        }
        finally
        {
        }
    }
    protected void ClearNew()
    {
        try
        {




            txtAadharNoNew.Text = "";
            txtChildSamagraIDNew.Text = "";
            txtMember_NameNew.Text = "";
            txtChildAgeNew.Text = "";
            ddlRelationNew.SelectedIndex = 0;
            ViewState["PLKR_ID"] = "";
            dtNew.Clear();
            dtNew.Columns.Clear();
            GridfamilymemberNew.DataSource = null;
            GridfamilymemberNew.DataBind();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Error", Ex.Message);
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", objdb.Alert("fa-exclamation", "alert-danger", "Sorry!", ex.Message), true);
        }
        finally
        {
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {

        try
        {

            clear();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Error", Ex.Message);
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", objdb.Alert("fa-exclamation", "alert-danger", "Sorry!", ex.Message), true);
        }
        finally
        {
        }


    }
    protected void Getupdtedchild_Data()
    {
        try
        {
            dsCheck.Clear();
            if (dtUpdate.Columns.Count == 0)
            {
                //dt.Columns.Add("SNo");
                dtUpdate.Columns.Add("PLKR_Child_ID");
                dtUpdate.Columns.Add("PLKR_ChildName_H");
                dtUpdate.Columns.Add("PLKR_Child_Age");
                dtUpdate.Columns.Add("PLKR_Relation");
                dtUpdate.Columns.Add("Relation_ID");
                dtUpdate.Columns.Add("Mobile_No");
                dtUpdate.Columns.Add("PLKR_ChildAdhar_No");
                dtUpdate.Columns.Add("Account_No");
                dtUpdate.Columns.Add("Samagra_ID");

            }
            foreach (GridViewRow row in GridViewchildupdate.Rows)
            {

                Label lblPLKR_Child_ID = (Label)row.FindControl("lblPLKR_Child_ID");
                TextBox txtPLKR_ChildName_H = (TextBox)row.FindControl("txtPLKR_ChildName_H");
                TextBox txtPLKR_ChildAdhar_No = (TextBox)row.FindControl("txtPLKR_ChildAdhar_No");
                TextBox txtSamagra_ID = (TextBox)row.FindControl("txtSamagra_ID");
                TextBox txtPLKR_Child_Age = (TextBox)row.FindControl("txtPLKR_Child_Age");
                Label lblPLKR_Relation = (Label)row.FindControl("lblPLKR_Relation");
                Label lblRelation_ID = (Label)row.FindControl("lblRelation_ID");

                dsCheck = objdb.ByProcedure("USP_Trade_Plucker_Check_ChildUpdate_AdharandSamagra_ID",
                       new string[] { "Adhar_No", "Samagra_ID" },
                       new string[] { txtPLKR_ChildAdhar_No.Text, txtSamagra_ID.Text },
                                     "dataset");
                if (dsCheck.Tables.Count > 0 && dsCheck.Tables[0].Rows[0]["Msg"].ToString() == "ok" && dsCheck.Tables[0].Rows[0]["ErrorMsg"].ToString() == "Data Unique")
              {
                  dtUpdate.Rows.Add(lblPLKR_Child_ID.Text, txtPLKR_ChildName_H.Text, txtPLKR_Child_Age.Text, lblPLKR_Relation.Text, lblRelation_ID.Text, "", txtPLKR_ChildAdhar_No.Text, "", txtSamagra_ID.Text);
              }
              else
              {
                  string error = dsCheck.Tables[0].Rows[0]["ErrorMsg"].ToString();

                  lblmsgModalupdate.Text = objdb.Alert("fa-ban", "alert-warning", "Info", error);
                  Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalmamberupdate();", true);
              }
            }
            ViewState["dtUpdate"] = dtUpdate;
        }
        catch (Exception Ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Error", Ex.Message);
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", objdb.Alert("fa-exclamation", "alert-danger", "Sorry!", ex.Message), true);
        }
        finally
        {
        }
    }
    protected void BtnUpdateChild_Click(object sender, EventArgs e)
    {
        try
        {

            if (BtnUpdateChild.Text == "Update")
            {
                Getupdtedchild_Data();

                lblMsg.Text = objdb.GetLocalIPAddress();
                ds = objdb.ByProcedure("USP_Trade_PluckerRegistrationChild_Update",
                    new string[] { "PLKR_ID", "CreatedBy", "CreatedByIP" },
                    new string[] { ViewState["PLKR_ID"].ToString(), ViewState["UserID"].ToString(), objdb.GetLocalIPAddress() },
                                 new string[] { "Type_Trade_PluckerRegistration_Child_update" },
                               new DataTable[] { dtUpdate },
                               "dataset");

                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                {

                    string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    Getdataplucker();
                    GetDatatableHeaderDesign();
                    // ClearNew();
                    ViewState["PLKR_ID"] = "";
                    dtUpdate.Clear();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                }
                else
                {
                    string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    if (error == "Data Already Exists")
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Info" + error);
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                    }
                }

            }
        }

        catch (Exception Ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Error", Ex.Message);
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", objdb.Alert("fa-exclamation", "alert-danger", "Sorry!", ex.Message), true);
        }
        finally
        {
        }
    }
    protected void chkActive_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            // string ID = GridViewPlucker.DataKeys[row.RowIndex].Value.ToString();
            string status = "";


            Label lblPLKR_ID = (Label)row.FindControl("lblPLKR_ID");


            LinkButton lnkDelete = (LinkButton)row.FindControl("lnkDelete");
            if (lnkDelete.Text == "Active")
            {
                status = "0";
            }
            else if (lnkDelete.Text == "Deactive")
            {
                status = "1";
            }
            ViewState["PLKR_ID"] = lblPLKR_ID.Text;
            ds = objdb.ByProcedure("USP_Trade_PluckerRegistration_Delete",
                        new string[] { "PLKR_ID", "IsActive", "CreatedBy", "CreatedByIP" },
                        new string[] { ViewState["PLKR_ID"].ToString(),status, ViewState["UserID"].ToString(),objdb.GetLocalIPAddress()
                              }, "TableSave");

            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
            {
                string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                GetDatatableHeaderDesign();
                Getdataplucker();
                if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "Activate Successfully.")
                {
                    string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-info", "Alert !", success);
                }
                else if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "Deactivate Successfully.")
                {
                    string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-warning", "Alert !", success);
                }
                //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
            }
            else
            {
                string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
            }
            ds.Clear();
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try{
            pnldata.Visible = true;
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=Plucker_Report"+ DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            pnldata.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
            pnldata.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
}