using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_Admin_AdminOffice : System.Web.UI.Page
{
    DataSet ds, ds1;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Emp_ID"] != null)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                txtGstNumber.Attributes.Add("readonly", "readonly");
                txtPanNumber.Attributes.Add("readonly", "readonly");
                ViewState["Office_ID"] = "0";
                // txtTanNumber.Attributes.Add("readonly", "readonly");

            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
            FillGrid1();
            FillDivision();
            FillOfficeType();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void FillOfficeType()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminOfficeType",
                            new string[] { "flag" },
                            new string[] { "8" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlOfficeType_Title.DataTextField = "OfficeType_Title";
                ddlOfficeType_Title.DataValueField = "OfficeType_ID";
                ddlOfficeType_Title.DataSource = ds;
                ddlOfficeType_Title.DataBind();
                ddlOfficeType_Title.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDivision()
    {
        try
        {
            string State_ID = "12";
            ds = objdb.ByProcedure("SpAdminDivision",
                        new string[] { "flag", "State_ID" },
                        new string[] { "7", State_ID }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlDivision_Name.DataTextField = "Division_Name";
                ddlDivision_Name.DataValueField = "Division_ID";
                ddlDivision_Name.DataSource = ds;
                ddlDivision_Name.DataBind();
                ddlDivision_Name.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid1()
    {
        try
        {

            ds = objdb.ByProcedure("SpAdminOffice",
                new string[] { "flag" },
                new string[] { "1" }, "dataset");

            GridView1.DataSource = ds;
            GridView1.DataBind();

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

            ds = objdb.ByProcedure("SpAdminOffice",
                new string[] { "flag", "OfficeType_Title", "Division_ID", "District_ID" },
                new string[] { "2", ddlOfficeType_Title.SelectedItem.Text, ddlDivision_Name.SelectedValue, ddlDistrict_Name.SelectedValue }, "dataset");
            GridView1.DataSource = ds;
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        txtOffice_Name.Text = "";
        txtOffice_ContactNo.Text = "";
        txtOffice_Email.Text = "";
        ddlOfficeType_Title.ClearSelection();
        ddlDivision_Name.ClearSelection();
        ddlDistrict_Name.ClearSelection();
        ddlBlock_Name.ClearSelection();
        txtOffice_Address.Text = "";
        txtTanNumber.Text = "";
        txtOffice_Code.Text = "";
        GridView1.SelectedIndex = -1;
        btnSave.Text = "Save";
        txtOfficePincode.Text = string.Empty;


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    int Status = 0;
                    ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_Code", "Office_ID" }, new string[] { "13", txtOffice_Code.Text, ViewState["Office_ID"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                    }
                    if (Status == 0)
                    {
                        ds = objdb.ByProcedure("SpAdminOffice",
                          new string[] { "flag", "Office_Name", "Office_Code", "Office_ID" },
                          new string[] { "6", txtOffice_Name.Text.Trim(), txtOffice_Code.Text, ViewState["Office_ID"].ToString() }, "dataset");

                        if (btnSave.Text == "Save" && ds.Tables[0].Rows.Count == 0 && ViewState["Office_ID"].ToString() == "0")
                        {
                            objdb.ByProcedure("SpAdminOffice",
                            new string[] { "flag", "Office_Name", "Office_ContactNo", "Office_Email", "OfficeType_Title", "Division_ID", "District_ID", "Office_Address", "Office_GstNumber", "Office_PanNumber", "Office_TanNumber", "Office_UpdatedBy", "OfficeType_ID", "Office_Pincode", "Office_Code" },
                            new string[] { "0", txtOffice_Name.Text.Trim(), txtOffice_ContactNo.Text.Trim(), txtOffice_Email.Text.Trim(), ddlOfficeType_Title.SelectedItem.Text, ddlDivision_Name.SelectedValue.ToString(), ddlDistrict_Name.SelectedValue.ToString(), txtOffice_Address.Text.Trim(), txtGstNumber.Text, txtPanNumber.Text, txtTanNumber.Text, ViewState["Emp_ID"].ToString(), ddlOfficeType_Title.SelectedValue, txtOfficePincode.Text.Trim(), txtOffice_Code.Text.Trim() }, "dataset");
                            ClearText();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                            FillGrid1();
                        }
                        else if (btnSave.Text == "Update" && ds.Tables[0].Rows.Count == 0 && ViewState["Office_ID"].ToString() != "0")
                        {
                            ds1 = objdb.ByProcedure("SpAdminOffice",
                                new string[] { "flag", "Office_ID", "Office_Name", "Office_ContactNo", "Office_Email", "OfficeType_Title", "Division_ID", "District_ID", "Office_Address", "Office_GstNumber", "Office_PanNumber", "Office_TanNumber", "Office_UpdatedBy", "OfficeType_ID", "Office_Pincode", "Office_Code" },
                                new string[] { "4", ViewState["Office_ID"].ToString(), txtOffice_Name.Text.Trim(), txtOffice_ContactNo.Text.Trim(), txtOffice_Email.Text.Trim(), ddlOfficeType_Title.SelectedItem.Text.Trim(), ddlDivision_Name.SelectedValue.ToString(), ddlDistrict_Name.SelectedValue.ToString(), txtOffice_Address.Text.Trim(), txtGstNumber.Text, txtPanNumber.Text, txtTanNumber.Text, ViewState["Emp_ID"].ToString(), ddlOfficeType_Title.SelectedValue, txtOfficePincode.Text.Trim(), txtOffice_Code.Text.Trim() }, "dataset");

                            if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                ClearText();
                                string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", success);
                                ds1.Clear();
                                FillGrid1();
                            }
                            else
                            {
                                string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                                ds1.Clear();
                                FillGrid1();
                            }
                        }
                        else
                        {
                            lblMsg.Text = "";
                            ClearText();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooopss!", "Office Name  or Office Code is already exist ");
                            FillGrid1();
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Office Code is already exist.');", true);
                    }
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Enter Valid Data");
            }
            //lblMsg.Text = "";
            //string msg = "";
            //if (txtOffice_Name.Text.Trim() == "")
            //{
            //    msg += "Enter Office Name /n";
            //}
            //if (txtOffice_ContactNo.Text.Trim() == "")
            //{
            //    msg += "Enter Office Contact Number /n";
            //}
            //if (txtOffice_Email.Text.Trim() == "")
            //{
            //    msg += "Enter Office Email /n";
            //}
            //if (txtOffice_Address.Text.Trim() == "")
            //{
            //    msg += "Enter Office Address /n";
            //}
            //if (ddlOfficeType_Title.SelectedIndex <= 0)
            //{
            //    msg += "Select Office Title /n";
            //}
            //else if (ddlOfficeType_Title.SelectedIndex == 1) { }
            // else if (ddlOfficeType_Title.SelectedIndex == 2)
            //    {
            //     if (ddlDivision_Name.SelectedIndex <= 0)
            //    {
            //        msg += "Select Region Name /n";
            //    }
            //    }
            //else
            //{
            //    if (ddlDivision_Name.SelectedIndex <= 0)
            //    {
            //        msg += "Select Region Name /n";
            //    }
            //    if (ddlDistrict_Name.SelectedIndex <= 0)
            //    {
            //        msg += "Select District Name /n";
            //    }
            //}

            //if (ddlBlock_Name.SelectedIndex < 0)
            //{
            //    msg += "Select Block Name /n";
            //}
            //if (msg.Trim() == "")
            //{


            //if (btnSave.Text == "Save" && ds.Tables[0].Rows.Count == 0)
            //{
            //    objdb.ByProcedure("SpAdminOffice",
            //    new string[] { "flag", "Office_Name", "Office_ContactNo", "Office_Email", "OfficeType_Title", "Division_ID", "District_ID", "Block_ID", "Office_Address", "Office_GstNumber", "Office_PanNumber", "Office_UpdatedBy", "OfficeType_ID" },
            //    new string[] { "0", txtOffice_Name.Text.Trim(), txtOffice_ContactNo.Text.Trim(), txtOffice_Email.Text.Trim(), ddlOfficeType_Title.SelectedItem.Text, ddlDivision_Name.SelectedValue.ToString(), ddlDistrict_Name.SelectedValue.ToString(), ddlBlock_Name.SelectedValue.ToString(), txtOffice_Address.Text.Trim(), txtGstNumber.Text, txtPanNumber.Text, ViewState["Emp_ID"].ToString(), ddlOfficeType_Title.SelectedValue }, "dataset");
            //    ClearText();
            //    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            //    FillGrid1();
            //}
            //else if (btnSave.Text == "Edit" && ds.Tables[0].Rows.Count != 0)
            //{
            //    objdb.ByProcedure("SpAdminOffice",
            //        new string[] { "flag", "Office_ID", "Office_Name", "Office_ContactNo", "Office_Email", "OfficeType_Title", "Division_ID", "District_ID", "Block_ID", "Office_Address", "Office_UpdatedBy", "OfficeType_ID" },
            //        new string[] { "4", ViewState["Office_ID"].ToString(), txtOffice_Name.Text.Trim(), txtOffice_ContactNo.Text.Trim(), txtOffice_Email.Text.Trim(), ddlOfficeType_Title.SelectedItem.Text.Trim(), ddlDivision_Name.SelectedValue.ToString(), ddlDistrict_Name.SelectedValue.ToString(), ddlBlock_Name.SelectedValue.ToString(), txtOffice_Address.Text.Trim(), ViewState["Emp_ID"].ToString() , ddlOfficeType_Title.SelectedValue}, "dataset");
            //    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Updated Successfully");
            //    FillGrid1();
            //}
            // else
            //  {
            // ClearText();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Office Name is already exist.');", true);
            // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooopss!", "Office Name is already exist");
            // FillGrid1();
            // }
            //}
            //else
            //{
            //    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", msg);
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlDivision_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


            lblMsg.Text = "";
            ddlDistrict_Name.Items.Clear();
            ddlBlock_Name.Items.Clear();

            FillGrid();
            string DivisionID = ddlDivision_Name.SelectedValue.ToString();

            ds = objdb.ByProcedure("SpAdminDistrict",
                           new string[] { "flag", "Division_ID" },
                           new string[] { "9", DivisionID }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlDistrict_Name.DataTextField = "District_Name";
                ddlDistrict_Name.DataValueField = "District_ID";
                ddlDistrict_Name.DataSource = ds;
                ddlDistrict_Name.DataBind();
                ddlDistrict_Name.Items.Insert(0, new ListItem("Select", "0"));
                ddlDistrict_Name.Focus();
            }
            else
            {
                ddlDistrict_Name.Items.Insert(0, new ListItem("Select", "0"));
                ddlBlock_Name.Items.Insert(0, new ListItem("Select", "0"));
            }

            DisplayDropdwon_OfficeTittlewise();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlDistrict_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


            lblMsg.Text = "";
            ddlBlock_Name.Items.Clear();
            DisplayDropdwon_OfficeTittlewise();
            FillGrid();
            string DistrictID = ddlDistrict_Name.SelectedValue.ToString();

            ds = objdb.ByProcedure("SpAdminBlock",
                            new string[] { "flag", "District_ID" },
                            new string[] { "6", DistrictID }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlBlock_Name.DataTextField = "Block_Name";
                ddlBlock_Name.DataValueField = "Block_ID";
                ddlBlock_Name.DataSource = ds;
                ddlBlock_Name.DataBind();
                ddlBlock_Name.Items.Insert(0, new ListItem("Select", "0"));
                ddlBlock_Name.Focus();
            }
            else
            {
                ddlBlock_Name.Items.Insert(0, new ListItem("Select", "0"));
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
            string Office_ID = chk.ToolTip.ToString();
            string Office_IsActive = "0";
            if (chk != null & chk.Checked)
            {
                Office_IsActive = "1";
            }
            ds = objdb.ByProcedure("SpAdminOffice",
                      new string[] { "flag", "Office_ID", "Office_IsActive", "Office_UpdatedBy" },
                      new string[] { "5", Office_ID, Office_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
            {
               // FillGrid1();
                FillGrid();
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Updated Successfully!");
            }
            else
            {
               // FillGrid1();
                FillGrid();
                string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
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
            ViewState["Office_ID"] = GridView1.SelectedValue.ToString();
            ds = objdb.ByProcedure("SpAdminOffice",
                       new string[] { "flag", "Office_ID" },
                       new string[] { "3", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtOffice_Name.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                txtOffice_ContactNo.Text = ds.Tables[0].Rows[0]["Office_ContactNo"].ToString();
                txtOffice_Email.Text = ds.Tables[0].Rows[0]["Office_Email"].ToString();
                ddlOfficeType_Title.ClearSelection();
                txtOfficePincode.Text = ds.Tables[0].Rows[0]["Office_Pincode"].ToString();

                if (ds.Tables[0].Rows[0]["OfficeType_Title"].ToString() != "")
                    ddlOfficeType_Title.Items.FindByText(ds.Tables[0].Rows[0]["OfficeType_Title"].ToString()).Selected = true;
                if (ds.Tables[0].Rows[0]["Division_ID"].ToString() != "")
                {
                    ddlDivision_Name.ClearSelection();
                    if (ds.Tables[0].Rows[0]["Division_ID"].ToString() != "")
                        ddlDivision_Name.Items.FindByValue(ds.Tables[0].Rows[0]["Division_ID"].ToString()).Selected = true;
                    if (ddlDivision_Name.SelectedIndex > 0)
                    {
                        DataSet ds1 = objdb.ByProcedure("SpAdminDistrict",
                                   new string[] { "flag", "Division_ID" },
                                   new string[] { "9", ddlDivision_Name.SelectedValue.ToString() }, "dataset");

                        if (ds1.Tables[0].Rows.Count != 0)
                        {
                            ddlDistrict_Name.DataTextField = "District_Name";
                            ddlDistrict_Name.DataValueField = "District_ID";
                            ddlDistrict_Name.DataSource = ds1;
                            ddlDistrict_Name.DataBind();
                            ddlDistrict_Name.Items.Insert(0, new ListItem("Select", "0"));

                        }
                        ddlDistrict_Name.ClearSelection();
                        if (ds.Tables[0].Rows[0]["District_ID"].ToString() != "")
                            ddlDistrict_Name.Items.FindByValue(ds.Tables[0].Rows[0]["District_ID"].ToString()).Selected = true;
                    }
                    if (ddlDistrict_Name.SelectedIndex > 0)
                    {
                        DataSet ds2 = objdb.ByProcedure("SpAdminBlock",
                                    new string[] { "flag", "District_ID" },
                                    new string[] { "6", ddlDistrict_Name.SelectedValue.ToString() }, "dataset");

                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            ddlBlock_Name.DataTextField = "Block_Name";
                            ddlBlock_Name.DataValueField = "Block_ID";
                            ddlBlock_Name.DataSource = ds2;
                            ddlBlock_Name.DataBind();
                            ddlBlock_Name.Items.Insert(0, new ListItem("Select", "0"));
                            ddlBlock_Name.ClearSelection();
                            if (ds.Tables[0].Rows[0]["Block_ID"].ToString() != "")
                                ddlBlock_Name.Items.FindByValue(ds.Tables[0].Rows[0]["Block_ID"].ToString()).Selected = true;
                        }
                    }
                }
                else
                {
                    ddlDivision_Name.ClearSelection();
                    ddlDistrict_Name.ClearSelection();
                    ddlBlock_Name.ClearSelection();
                }
                txtOffice_Address.Text = ds.Tables[0].Rows[0]["Office_Address"].ToString();
                if (ds.Tables[0].Rows[0]["Office_TanNumber"].ToString() != "")
                {
                    txtTanNumber.Text = ds.Tables[0].Rows[0]["Office_TanNumber"].ToString();
                }
                if (String.IsNullOrEmpty(ds.Tables[0].Rows[0]["Office_GstNumber"].ToString()))
                {
                    txtGstNumber.ReadOnly = false;
                }
                if (String.IsNullOrEmpty(ds.Tables[0].Rows[0]["Office_PanNumber"].ToString()))
                {
                    txtPanNumber.ReadOnly = false;
                }
                if (String.IsNullOrEmpty(ds.Tables[0].Rows[0]["Office_Name"].ToString()))
                {
                    txtOffice_Name.Enabled = true;
                }
                if (ds.Tables[0].Rows[0]["Office_TanNumber"].ToString() != "")
                {
                    txtTanNumber.Text = ds.Tables[0].Rows[0]["Office_TanNumber"].ToString();
                }
                txtGstNumber.Text = ds.Tables[0].Rows[0]["Office_GstNumber"].ToString();
                txtPanNumber.Text = ds.Tables[0].Rows[0]["Office_PanNumber"].ToString();

                txtOffice_Code.Text = ds.Tables[0].Rows[0]["Office_Code"].ToString();

                btnSave.Text = "Update";
                EditDisplayDropdwon_OfficeTittlewise();
                GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LemonChiffon;


            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void ddlOfficeType_Title_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDivision_Name.ClearSelection();
        ddlDistrict_Name.ClearSelection();
        ddlBlock_Name.ClearSelection();
        lblMsg.Text = string.Empty;


        if (ddlOfficeType_Title.SelectedIndex == 0)
        {
            FillGrid1();
        }
        else
        {
            DisplayDropdwon_OfficeTittlewise();
            FillGrid();
        }
    }
    private void EditDisplayDropdwon_OfficeTittlewise()
    {
        try
        {
            if (ddlOfficeType_Title.SelectedValue == "1")
            {
                spanRegion.Visible = false;
                spanDistrict.Visible = false;
                txtOffice_Name.Enabled = false;

            }
            else if (ddlOfficeType_Title.SelectedValue == "2")
            {
                spanRegion.Visible = true;
                spanDistrict.Visible = false;
                txtOffice_Name.Enabled = false;
            }
            else if (ddlOfficeType_Title.SelectedValue == "3")
            {
                spanRegion.Visible = true;
                spanDistrict.Visible = true;
                txtOffice_Name.Enabled = false;
            }
            else if (ddlOfficeType_Title.SelectedValue == "4")
            {
                spanRegion.Visible = true;
                spanDistrict.Visible = true;
                txtOffice_Name.Enabled = false;
                txtOffice_Name.Enabled = true;
            }
            else if (ddlOfficeType_Title.SelectedValue == "5")
            {
                spanRegion.Visible = true;
                spanDistrict.Visible = true;
                txtOffice_Name.Enabled = true;
            }
            else if (ddlOfficeType_Title.SelectedValue == "16")
            {
                spanRegion.Visible = true;
                spanDistrict.Visible = true;
                txtOffice_Name.Enabled = true;
            }
            else if (ddlOfficeType_Title.SelectedValue == "17")
            {
                spanRegion.Visible = true;
                spanDistrict.Visible = true;
                txtOffice_Name.Enabled = true;
            }
            else
            {
                spanRegion.Visible = false;
                spanDistrict.Visible = false;
                txtOffice_Name.Text = string.Empty;
                txtOffice_Name.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void DisplayDropdwon_OfficeTittlewise()
    {
        try
        {


            if (ddlOfficeType_Title.SelectedValue == "1")
            {
                spanRegion.Visible = false;
                spanDistrict.Visible = false;
                txtOffice_Name.Text = ddlOfficeType_Title.SelectedItem.Text;
                txtOffice_Name.Enabled = false;

            }
            else if (ddlOfficeType_Title.SelectedValue == "2")
            {
                spanRegion.Visible = true;
                spanDistrict.Visible = false;
                txtOffice_Name.Enabled = false;
                txtOffice_Name.Text = ddlDivision_Name.SelectedItem.Text + "-" + ddlOfficeType_Title.SelectedItem.Text;
            }
            else if (ddlOfficeType_Title.SelectedValue == "3")
            {
                spanRegion.Visible = true;
                spanDistrict.Visible = true;
                txtOffice_Name.Text = ddlDistrict_Name.SelectedItem.Text + "-" + ddlOfficeType_Title.SelectedItem.Text;
                txtOffice_Name.Enabled = false;
            }
            else if (ddlOfficeType_Title.SelectedValue == "4")
            {
                spanRegion.Visible = true;
                spanDistrict.Visible = true;
                txtOffice_Name.Text = ddlDistrict_Name.SelectedItem.Text + "-" + ddlOfficeType_Title.SelectedItem.Text;
                txtOffice_Name.Enabled = false;
                txtOffice_Name.Enabled = true;
            }
            else if (ddlOfficeType_Title.SelectedValue == "5")
            {
                spanRegion.Visible = true;
                spanDistrict.Visible = true;
                txtOffice_Name.Text = ddlDistrict_Name.SelectedItem.Text + "-" + ddlOfficeType_Title.SelectedItem.Text;
                txtOffice_Name.Enabled = true;
            }
            else if (ddlOfficeType_Title.SelectedValue == "16")
            {
                spanRegion.Visible = true;
                spanDistrict.Visible = true;
                txtOffice_Name.Text = ddlDistrict_Name.SelectedItem.Text + "-" + ddlOfficeType_Title.SelectedItem.Text;
                txtOffice_Name.Enabled = true;
            }
            else if (ddlOfficeType_Title.SelectedValue == "17")
            {
                spanRegion.Visible = true;
                spanDistrict.Visible = true;
                txtOffice_Name.Text = ddlDistrict_Name.SelectedItem.Text + "-" + ddlOfficeType_Title.SelectedItem.Text;
                txtOffice_Name.Enabled = true;
            }
            else
            {
                spanRegion.Visible = false;
                spanDistrict.Visible = false;
                txtOffice_Name.Text = string.Empty;
                txtOffice_Name.Enabled = false;
            }
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
            Response.Redirect("AdminOffice.aspx");
            // ClearText();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : ). " + ex.Message.ToString());
        }
    }
}