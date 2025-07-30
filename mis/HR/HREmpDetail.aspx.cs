using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;

public partial class mis_HR_HREmpDetail : System.Web.UI.Page
{
    DataSet ds, dsRecord;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                lblMsg.Text = "";
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                TextReadonly();
                FillDetail();

                ViewState["Emp_NewID"] = "";
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    protected void FillDropdown()
    {
        try
        {
            // State
            ds = objdb.ByProcedure("SpAdminState", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                ddlEmp_CurState.DataSource = ds;
                ddlEmp_CurState.DataTextField = "State_Name";
                ddlEmp_CurState.DataValueField = "State_ID";
                ddlEmp_CurState.DataBind();
                ddlEmp_CurState.Items.Insert(0, new ListItem("Select", "0"));

                ddlEmp_PerState.DataSource = ds;
                ddlEmp_PerState.DataTextField = "State_Name";
                ddlEmp_PerState.DataValueField = "State_ID";
                ddlEmp_PerState.DataBind();
                ddlEmp_PerState.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlEmp_CurState.Items.Insert(0, new ListItem("Select", "0"));
                ddlEmp_PerState.Items.Insert(0, new ListItem("Select", "0"));
            }
            // Department
            ds = null;
            ds = objdb.ByProcedure("SpAdminDepartment", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlDepartment_ID.DataSource = ds;
                ddlDepartment_ID.DataTextField = "Department_Name";
                ddlDepartment_ID.DataValueField = "Department_ID";
                ddlDepartment_ID.DataBind();
                ddlDepartment_ID.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlDepartment_ID.Items.Insert(0, new ListItem("Select", "0"));
            }
            // Designation
            //ds = null;
            //ds = objdb.ByProcedure("SpAdminDesignation", new string[] { "flag" }, new string[] { "1" }, "dataset");
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlDesignation_ID.DataSource = ds;
            //    ddlDesignation_ID.DataTextField = "Designation_Name";
            //    ddlDesignation_ID.DataValueField = "Designation_ID";
            //    ddlDesignation_ID.DataBind();
            //    ddlDesignation_ID.Items.Insert(0, new ListItem("Select", "0"));
            //}
            //else
            //{
            //    ddlDesignation_ID.Items.Insert(0, new ListItem("Select", "0"));
            //}
            // Pay Scale
            ds = null;
            ds = objdb.ByProcedure("SpHRPayScale", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlPayScale_ID.DataSource = ds;
                ddlPayScale_ID.DataTextField = "PayScale_Name";
                ddlPayScale_ID.DataValueField = "PayScale_ID";
                ddlPayScale_ID.DataBind();
                ddlPayScale_ID.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlPayScale_ID.Items.Insert(0, new ListItem("Select", "0"));
            }
            // Office Type Title
            ds = null;
            ds = objdb.ByProcedure("SpAdminOfficeType", new string[] { "flag" }, new string[] { "7" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeType_Title.DataSource = ds;
                ddlOfficeType_Title.DataTextField = "OfficeType_Title";
                ddlOfficeType_Title.DataValueField = "OfficeType_Title";
                ddlOfficeType_Title.DataBind();
                ddlOfficeType_Title.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlOfficeType_Title.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        //Level
        ds = objdb.ByProcedure("SpHRLevelMaster", new string[] { "flag" }, new string[] { "6" }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlLevel.DataSource = ds;
            ddlLevel.DataTextField = "Level_Name";
            ddlLevel.DataValueField = "Level_ID";
            ddlLevel.DataBind();
            ddlLevel.Items.Insert(0, new ListItem("Select", "0"));
        }
        // Class
        ds = objdb.ByProcedure("SpHRClass", new string[] { "flag" }, new string[] { "1" }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            ddlEmp_Class.DataSource = ds;
            ddlEmp_Class.DataTextField = "Class_Name";
            ddlEmp_Class.DataValueField = "Class_Name";
            ddlEmp_Class.DataBind();
            ddlEmp_Class.Items.Insert(0, new ListItem("Select", "0"));
            ddlDesignation_ID.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {
            ddlEmp_Class.Items.Insert(0, new ListItem("Select", "0"));
            ddlDesignation_ID.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    protected void TextReadonly()
    {
        try
        {
            txtEmp_Dob.Attributes.Add("readonly", "readonly");
            txtUserName.Attributes.Add("readonly", "readonly");
            txtPassword.Attributes.Add("readonly", "readonly");
            txtEmp_JoiningDate.Attributes.Add("readonly", "readonly");
            txtEmp_PostingDate.Attributes.Add("readonly", "readonly");
            txtEmp_RetirementDate.Attributes.Add("readonly", "readonly");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDetail()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag" }, new string[] { "28" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //Start Personal Detail
    protected void lnkPersonalDetail_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Emp_NewID"] = "";
            lblMsg.Text = "";
            lblPersonalDetail.Text = "";
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            //CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
            //  GridViewRow row = GridView1.SelectedRow;
            // rowid = row.RowIndex;
            LinkButton lnkPersonalDetail = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkPersonalDetail");
            ViewState["Emp_NewID"] = lnkPersonalDetail.ToolTip.ToString();
            FillPersonalDetail();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillPersonalDetail()
    {
        try
        {
            lblMsg.Text = "";
            FillDropdown();
            dsRecord = null;
            dsRecord = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Emp_ID" }, new string[] { "3", ViewState["Emp_NewID"].ToString() }, "dataset");
            txtEmp_Name.Text = dsRecord.Tables[0].Rows[0]["Emp_Name"].ToString();
            txtEmp_Dob.Text = dsRecord.Tables[0].Rows[0]["Emp_Dob"].ToString();
            ddlEmp_Relation.ClearSelection();
            ddlEmp_Relation.Items.FindByText(dsRecord.Tables[0].Rows[0]["Emp_Relation"].ToString()).Selected = true;
            txtEmp_FatherHusbandName.Text = dsRecord.Tables[0].Rows[0]["Emp_FatherHusbandName"].ToString();
            ddlEmp_Gender.ClearSelection();
            ddlEmp_Gender.Items.FindByText(dsRecord.Tables[0].Rows[0]["Emp_Gender"].ToString()).Selected = true;
            ddlEmp_BloodGroup.ClearSelection();
            ddlEmp_BloodGroup.Items.FindByText(dsRecord.Tables[0].Rows[0]["Emp_BloodGroup"].ToString()).Selected = true;
            ddlEmp_MaritalStatus.ClearSelection();
            ddlEmp_MaritalStatus.Items.FindByText(dsRecord.Tables[0].Rows[0]["Emp_MaritalStatus"].ToString()).Selected = true;
            txtEmp_MobileNo.Text = dsRecord.Tables[0].Rows[0]["Emp_MobileNo"].ToString();
            txtEmp_AadharNo.Text = dsRecord.Tables[0].Rows[0]["Emp_AadharNo"].ToString();
            txtEmp_PanCardNo.Text = dsRecord.Tables[0].Rows[0]["Emp_PanCardNo"].ToString();
            txtEmp_Email.Text = dsRecord.Tables[0].Rows[0]["Emp_Email"].ToString();
            txtEmp_HusbWifeName.Text = dsRecord.Tables[0].Rows[0]["Emp_HusbWifeName"].ToString();
            txtEmp_HusbWifeJob.Text = dsRecord.Tables[0].Rows[0]["Emp_HusbWifeJob"].ToString();
            txtEmp_HusbWifeDep.Text = dsRecord.Tables[0].Rows[0]["Emp_HusbWifeDep"].ToString();
            ddlEmp_Category.ClearSelection();
            ddlEmp_Category.Items.FindByText(dsRecord.Tables[0].Rows[0]["Emp_Category"].ToString()).Selected = true;
            ddlEmp_Religion.ClearSelection();
            ddlEmp_Religion.Items.FindByText(dsRecord.Tables[0].Rows[0]["Emp_Religion"].ToString()).Selected = true;
            rbtEmp_Disability.ClearSelection();
            rbtEmp_Disability.SelectedValue = dsRecord.Tables[0].Rows[0]["Emp_Disability"].ToString();
            ddlEmp_DisabilityType.ClearSelection();
            ddlEmp_DisabilityType.Items.FindByText(dsRecord.Tables[0].Rows[0]["Emp_DisabilityType"].ToString()).Selected = true;
            txtEmp_PerPinCode.Text = dsRecord.Tables[0].Rows[0]["Emp_PerPinCode"].ToString();
            txtEmp_PerAddress.Text = dsRecord.Tables[0].Rows[0]["Emp_PerAddress"].ToString();
            ViewState["FU_Emp_ProfileImage"] = dsRecord.Tables[0].Rows[0]["Emp_ProfileImage"].ToString();
            ddlEmp_CurState.ClearSelection();
            if (objdb.isNumber(dsRecord.Tables[0].Rows[0]["Emp_CurState"].ToString()))
            {
                ddlEmp_CurState.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Emp_CurState"].ToString()).Selected = true;
            }

            FillCurCity();
            ddlEmp_CurCity.ClearSelection();
            if (objdb.isNumber(dsRecord.Tables[0].Rows[0]["Emp_CurCity"].ToString()))
            {
                ddlEmp_CurCity.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Emp_CurCity"].ToString()).Selected = true;
            }

            txtEmp_CurPinCode.Text = dsRecord.Tables[0].Rows[0]["Emp_CurPinCode"].ToString();
            txtEmp_CurAddress.Text = dsRecord.Tables[0].Rows[0]["Emp_CurAddress"].ToString();
            ddlEmp_PerState.ClearSelection();
            if (objdb.isNumber(dsRecord.Tables[0].Rows[0]["Emp_PerState"].ToString()))
            {
                ddlEmp_PerState.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Emp_PerState"].ToString()).Selected = true;
            }

            FillPerCity();
            ddlEmp_PerCity.ClearSelection();
            if (objdb.isNumber(dsRecord.Tables[0].Rows[0]["Emp_PerCity"].ToString()))
            {
                ddlEmp_PerCity.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Emp_PerCity"].ToString()).Selected = true;
            }
            if (dsRecord.Tables[0].Rows[0]["Emp_ProfileImage"].ToString() != "")
            {
                imgProfileImage.ImageUrl = "~/mis/HR/"+dsRecord.Tables[0].Rows[0]["Emp_ProfileImage"].ToString();
            }


            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlEmp_CurState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlEmp_CurCity.Items.Clear();
            ddlEmp_CurCity.Items.Insert(0, new ListItem("Select", "0"));
            FillCurCity();
            //ddlEmp_CurState.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillCurCity()
    {
        ddlEmp_CurCity.Items.Insert(0, new ListItem("Select", "0"));
        if (ddlEmp_CurState.SelectedIndex > 0)
        {
            ds = objdb.ByProcedure("SpAdminDistrict", new string[] { "flag", "State_ID" }, new string[] { "7", ddlEmp_CurState.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmp_CurCity.DataSource = ds;
                ddlEmp_CurCity.DataTextField = "District_Name";
                ddlEmp_CurCity.DataValueField = "District_ID";
                ddlEmp_CurCity.DataBind();
                ddlEmp_CurCity.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
    }
    protected void FillPerCity()
    {
        ddlEmp_PerCity.Items.Insert(0, new ListItem("Select", "0"));
        if (ddlEmp_PerState.SelectedIndex > 0)
        {
            ds = objdb.ByProcedure("SpAdminDistrict", new string[] { "flag", "State_ID" }, new string[] { "7", ddlEmp_PerState.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmp_PerCity.DataSource = ds;
                ddlEmp_PerCity.DataTextField = "District_Name";
                ddlEmp_PerCity.DataValueField = "District_ID";
                ddlEmp_PerCity.DataBind();
                ddlEmp_PerCity.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
    }
    protected void ddlEmp_PerState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlEmp_PerCity.Items.Clear();
            ddlEmp_PerCity.Items.Insert(0, new ListItem("Select", "0"));
            FillPerCity();
            // ddlEmp_CurState.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnUpdatePersonalDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string Emp_ProfileImage = "";
            if (txtEmp_Name.Text == "")
            {
                msg += "Entar Employee Name. <br/>";
            }
            if (ddlEmp_Gender.SelectedIndex == 0)
            {
                msg += "Select Gender. <br/>";
            }
            if (txtEmp_Dob.Text == "")
            {
                msg += "Entar Date of birth. <br/>";
            }
            if (ddlEmp_Relation.SelectedIndex == 0)
            {
                msg += "Select Relation. <br/>";
            }
            if (txtEmp_FatherHusbandName.Text == "")
            {
                msg += "Entar Father /Husband Name. <br/>";
            }
            if (ddlEmp_MaritalStatus.SelectedIndex == 0)
            {
                msg += "Select Marital Status. <br/>";
            }
            if (ddlEmp_MaritalStatus.SelectedIndex > 1)
            {
                if (txtEmp_HusbWifeName.Text == "")
                {
                    msg += "Select Husband/ Wife Name. <br/>";
                }
                if (txtEmp_HusbWifeJob.Text == "")
                {
                    msg += "Select Husband/ Wife Job. <br/>";
                }
                if (txtEmp_HusbWifeDep.Text == "")
                {
                    msg += "Select Husband/ Wife Department. <br/>";
                }

            }
            if (ddlEmp_BloodGroup.SelectedIndex == 0)
            {
                msg += "Select Blood Group. <br/>";
            }
            if (txtEmp_MobileNo.Text == "")
            {
                msg += "Entar Mobile Number. <br/>";
            }
            if (txtEmp_AadharNo.Text == "")
            {
                msg += "Entar Aadhar Number. <br/>";
            }
            if (txtEmp_PanCardNo.Text == "")
            {
                msg += "Entar Pan Card Number. <br/>";
            }
            if (txtEmp_Email.Text == "")
            {
                msg += "Entar Email ID. <br/>";
            }
            //if (!FU_Emp_ProfileImage.HasFile)
            //{
            //    msg += "Select Profile Image. <br/>";
            //}

            if (ddlEmp_Religion.SelectedIndex == 0)
            {
                msg += "Select Category. <br/>";
            }
            if (ddlEmp_Category.SelectedIndex == 0)
            {
                msg += "Select Religion. <br/>";
            }
            if (ddlEmp_DisabilityType.SelectedIndex == 0)
            {
                msg += "Select Disability Type. <br/>";
            }
            if (ddlEmp_CurState.SelectedIndex == 0)
            {
                msg += "Select Current State. <br/>";
            }
            if (ddlEmp_CurCity.SelectedIndex == 0)
            {
                msg += "Select Current City. <br/>";
            }
            if (txtEmp_CurPinCode.Text == "")
            {
                msg += "Entar Current Pin Code . <br/>";
            }
            if (txtEmp_CurAddress.Text == "")
            {
                msg += "Entar Current Address . <br/>";
            }
            if (ddlEmp_PerState.SelectedIndex == 0)
            {
                msg += "Select Permanent State. <br/>";
            }
            if (ddlEmp_PerCity.SelectedIndex == 0)
            {
                msg += "Select Permanent City. <br/>";
            }
            if (txtEmp_PerPinCode.Text == "")
            {
                msg += "Entar Permanent Pin Code . <br/>";
            }
            if (txtEmp_PerAddress.Text == "")
            {
                msg += "Entar Permanent Address . <br/>";
            }
            if (msg.Trim() == "" && ViewState["Emp_ID"].ToString() != "" && ViewState["Emp_NewID"].ToString() != "")
            {
                //lblDob.Text = "";
                if (FU_Emp_ProfileImage.HasFile)
                {
                    Emp_ProfileImage = "UploadDoc/" + Guid.NewGuid() + "-" + FU_Emp_ProfileImage.FileName;
                    FU_Emp_ProfileImage.PostedFile.SaveAs(Server.MapPath(Emp_ProfileImage));
                }
                else
                {
                    Emp_ProfileImage = ViewState["FU_Emp_ProfileImage"].ToString();
                }
                ds = objdb.ByProcedure("SpHREmployee",
                new string[] { "flag", "Emp_ID", "Emp_Name", "Emp_Gender", "Emp_Dob", "Emp_Relation", "Emp_FatherHusbandName", "Emp_MaritalStatus", "Emp_BloodGroup", "Emp_MobileNo", "Emp_AadharNo", "Emp_PanCardNo", "Emp_Email", "Emp_ProfileImage", "Emp_HusbWifeName", "Emp_HusbWifeJob", "Emp_HusbWifeDep", "Emp_Category", "Emp_Religion", "Emp_Disability", "Emp_DisabilityType", "Emp_CurState", "Emp_CurCity", "Emp_CurPinCode", "Emp_CurAddress", "Emp_PerState", "Emp_PerCity", "Emp_PerPinCode", "Emp_PerAddress", "Emp_UpdatedBy" },
                new string[] { "4", ViewState["Emp_NewID"].ToString(), txtEmp_Name.Text, ddlEmp_Gender.SelectedValue.ToString(), Convert.ToDateTime(txtEmp_Dob.Text, cult).ToString("yyyy/MM/dd"), ddlEmp_Relation.SelectedValue.ToString(), txtEmp_FatherHusbandName.Text, ddlEmp_MaritalStatus.SelectedValue.ToString(), ddlEmp_BloodGroup.SelectedValue.ToString(), txtEmp_MobileNo.Text, txtEmp_AadharNo.Text, txtEmp_PanCardNo.Text, txtEmp_Email.Text, Emp_ProfileImage,
                  txtEmp_HusbWifeName.Text, txtEmp_HusbWifeJob.Text, txtEmp_HusbWifeDep.Text, ddlEmp_Category.SelectedValue.ToString(), ddlEmp_Religion.SelectedValue.ToString(), rbtEmp_Disability.SelectedValue.ToString(),ddlEmp_DisabilityType.SelectedValue.ToString(), ddlEmp_CurState.SelectedValue.ToString(), ddlEmp_CurCity.SelectedValue.ToString(), txtEmp_CurPinCode.Text, txtEmp_CurAddress.Text, ddlEmp_PerState.SelectedValue.ToString(), ddlEmp_PerCity.SelectedValue.ToString(), txtEmp_PerPinCode.Text, txtEmp_PerAddress.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //ViewState["Emp_IDNew"] = ds.Tables[0].Rows[0]["Emp_ID"].ToString();
                    //txtUserName.Text = ds.Tables[0].Rows[0]["UserName"].ToString();
                    //txtPassword.Text = ds.Tables[0].Rows[0]["Password"].ToString();
                    //lblDob.Text = ds.Tables[0].Rows[0]["Emp_Dob"].ToString();
                    lblPersonalDetail.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillPersonalDetail();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal()", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record not save.');", true);
                }
            }
            else
            {
                lblPersonalDetail.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal()", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    // Official Detail Section
    protected void lnkOfficialDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblOfficialDetail.Text = "";
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkOfficialDetail = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkOfficialDetail");
            ViewState["Emp_NewID"] = lnkOfficialDetail.ToolTip.ToString();
            FillOfficialDetail();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal1()", true);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOfficialDetail()
    {
        try
        {
            lblMsg.Text = "";
            FillDropdown();
            dsRecord = null;
            dsRecord = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Emp_ID" }, new string[] { "6", ViewState["Emp_NewID"].ToString() }, "dataset");
            if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
            {
                ddlOfficeType_Title.ClearSelection();
                ddlOfficeType_Title.Items.FindByValue(dsRecord.Tables[0].Rows[0]["OfficeType_Title"].ToString()).Selected = true;
                FillOffice();
                ddlOffice_ID.ClearSelection();
                ddlOffice_ID.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Office_ID"].ToString()).Selected = true;
                txtUserName.Text = dsRecord.Tables[0].Rows[0]["UserName"].ToString();
                txtPassword.Text = dsRecord.Tables[0].Rows[0]["Password"].ToString();
                ddlLevel.ClearSelection();
                if (dsRecord.Tables[0].Rows[0]["EmpLevel_ID"].ToString() != "")
                {
                    ddlLevel.Items.FindByValue(dsRecord.Tables[0].Rows[0]["EmpLevel_ID"].ToString()).Selected = true;
                }
                ddlEmp_Class.ClearSelection();
                ddlEmp_Class.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Emp_Class"].ToString()).Selected = true;
                ddlDepartment_ID.ClearSelection();
                ddlDepartment_ID.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Department_ID"].ToString()).Selected = true;
                FillDesignation();
                ddlDesignation_ID.ClearSelection();
                ddlDesignation_ID.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Designation_ID"].ToString()).Selected = true;
                ddlPayScale_ID.ClearSelection();
                ddlPayScale_ID.Items.FindByValue(dsRecord.Tables[0].Rows[0]["PayScale_ID"].ToString()).Selected = true;
                FillGradPay();
                ddlGradPay_ID.ClearSelection();
                ddlGradPay_ID.Items.FindByValue(dsRecord.Tables[0].Rows[0]["GradPay_ID"].ToString()).Selected = true;
                txtEmp_BasicSalery.Text = dsRecord.Tables[0].Rows[0]["Emp_BasicSalery"].ToString();
                lblDob.Text = dsRecord.Tables[0].Rows[0]["Emp_Dob"].ToString();
                txtEmp_JoiningDate.Text = dsRecord.Tables[0].Rows[0]["Emp_JoiningDate"].ToString();
                txtEmp_PostingDate.Text = dsRecord.Tables[0].Rows[0]["Emp_PostingDate"].ToString();
                txtEmp_RetirementDate.Text = dsRecord.Tables[0].Rows[0]["Emp_RetirementDate"].ToString();
                ddlEmp_TypeOfRecruitment.ClearSelection();
                ddlEmp_TypeOfRecruitment.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Emp_TypeOfRecruitment"].ToString()).Selected = true;
                ddlEmp_TypeOfPost.ClearSelection();
                ddlEmp_TypeOfPost.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString()).Selected = true;
                ddlEmp_GpfType.ClearSelection();
                ddlEmp_GpfType.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Emp_GpfType"].ToString()).Selected = true;
                txtEmp_GpfNo.Text = dsRecord.Tables[0].Rows[0]["Emp_GpfNo"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal1()", true);
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOffice()
    {
        if (ddlOfficeType_Title.SelectedIndex > 0)
        {
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "OfficeType_Title" }, new string[] { "7", ddlOfficeType_Title.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice_ID.DataSource = ds;
                ddlOffice_ID.DataTextField = "Office_Name";
                ddlOffice_ID.DataValueField = "Office_ID";
                ddlOffice_ID.DataBind();
                ddlOffice_ID.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
    }
    protected void ddlOfficeType_Title_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlOffice_ID.Items.Clear();
            ddlOffice_ID.Items.Insert(0, new ListItem("Select", "0"));
            FillOffice();
            //ddlOfficeType_Title.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal1()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlPayScale_ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlGradPay_ID.Items.Clear();
            ddlGradPay_ID.Items.Insert(0, new ListItem("Select", "0"));
            if (ddlPayScale_ID.SelectedIndex > 0)
            {
                FillGradPay();
            }
            // ddlPayScale_ID.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal1()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDesignation()
    {
        ds = objdb.ByProcedure("SpAdminDesignation", new string[] { "flag", "Class" }, new string[] { "6", ddlEmp_Class.SelectedValue.ToString() }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlDesignation_ID.DataSource = ds;
            ddlDesignation_ID.DataTextField = "Designation_Name";
            ddlDesignation_ID.DataValueField = "Designation_ID";
            ddlDesignation_ID.DataBind();
            ddlDesignation_ID.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    protected void ddlEmp_Class_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblPersonalDetail.Text = "";
            ddlDesignation_ID.Items.Clear();
            ddlDesignation_ID.Items.Insert(0, new ListItem("Select", "0"));
            ds = null;
            FillDesignation();
            // ddlEmp_Class.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal1()", true);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGradPay()
    {
        if (ddlOfficeType_Title.SelectedIndex > 0)
        {
            ds = objdb.ByProcedure("SpHRGradePay", new string[] { "flag", "PayScale_ID" }, new string[] { "6", ddlPayScale_ID.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlGradPay_ID.DataSource = ds;
                ddlGradPay_ID.DataTextField = "GradePay_Name";
                ddlGradPay_ID.DataValueField = "GradePay_ID";
                ddlGradPay_ID.DataBind();
                ddlGradPay_ID.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
    }
    protected void btnUpdateOfficialDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlOfficeType_Title.SelectedIndex == 0)
            {
                msg += "Select Office Type. <br/>";
            }
            if (ddlOffice_ID.SelectedIndex == 0)
            {
                msg += "Select Office. <br/>";
            }
            if (ddlEmp_Class.SelectedIndex == 0)
            {
                msg += "Select Class. <br/>";
            }
            if (ddlDepartment_ID.SelectedIndex == 0)
            {
                msg += "Select Department. <br/>";
            }
            if (ddlDesignation_ID.SelectedIndex == 0)
            {
                msg += "Select Designation. <br/>";
            }
            if (ddlPayScale_ID.SelectedIndex == 0)
            {
                msg += "Select Pay Scale. <br/>";
            }
            if (ddlGradPay_ID.SelectedIndex == 0)
            {
                msg += "Select Grad Pay. <br/>";
            }
            if (txtEmp_BasicSalery.Text == "")
            {
                msg += "Entar Basic Salery. <br/>";
            }
            if (txtEmp_JoiningDate.Text == "")
            {
                msg += "Entar Joining Date. <br/>";
            }
            if (txtEmp_PostingDate.Text == "")
            {
                msg += "Entar Posting Date. <br/>";
            }
            if (txtEmp_RetirementDate.Text == "")
            {
                msg += "Entar Retirement Date. <br/>";
            }
            if (ddlEmp_TypeOfRecruitment.SelectedIndex == 0)
            {
                msg += "Select Type of Recruitment. <br/>";
            }
            if (ddlEmp_TypeOfPost.SelectedIndex == 0)
            {
                msg += "Select Type of Post. <br/>";
            }
            if (ddlEmp_GpfType.SelectedIndex == 0)
            {
                msg += "Select GPF / DPF /NPS . <br/>";
            }
            if (txtEmp_GpfNo.Text == "")
            {
                msg += "Entar GPF/DPF/NPS No. <br/>";
            }

            if (msg.Trim() == "" && ViewState["Emp_ID"].ToString() != "")
            {
                ds = objdb.ByProcedure("SpHREmployee",
                new string[] { "flag", "Emp_ID", "EmpLevel_ID", "Emp_Class", "Department_ID", "Designation_ID", "PayScale_ID", "GradPay_ID", "Emp_BasicSalery", "Emp_JoiningDate", "Emp_PostingDate", "Emp_RetirementDate", "Emp_TypeOfRecruitment", "Emp_TypeOfPost", "Emp_GpfType", "Emp_GpfNo", "OfficeType_Title", "Office_ID", "Emp_UpdatedBy", "UserName", "Password" },
                new string[] { "8", ViewState["Emp_NewID"].ToString(),ddlLevel.SelectedValue.ToString(), ddlEmp_Class.SelectedValue.ToString(), ddlDepartment_ID.SelectedValue.ToString(), ddlDesignation_ID.SelectedValue.ToString(), ddlPayScale_ID.SelectedValue.ToString(), ddlGradPay_ID.SelectedValue.ToString(), txtEmp_BasicSalery.Text, Convert.ToDateTime(txtEmp_JoiningDate.Text, cult).ToString("yyyy/MM/dd"), 
                        Convert.ToDateTime(txtEmp_PostingDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtEmp_RetirementDate.Text, cult).ToString("yyyy/MM/dd"), ddlEmp_TypeOfRecruitment.SelectedValue.ToString(), ddlEmp_TypeOfPost.SelectedValue.ToString(), ddlEmp_GpfType.SelectedValue.ToString(), txtEmp_GpfNo.Text, ddlOfficeType_Title.SelectedValue.ToString(), ddlOffice_ID.SelectedValue.ToString(), ViewState["Emp_ID"].ToString(), txtUserName.Text, txtPassword.Text }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblOfficialDetail.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillOfficialDetail();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal1()", true);

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record not save.');", true);
                }
            }
            else
            {
                lblOfficialDetail.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal1()", true);
            }
        }
        catch (Exception ex)
        {
            lblOfficialDetail.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal1()", true);
        }
    }

    // Start Bank Details Section
    protected void lnkBankDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblBankDetail.Text = "";
            lblGridBankMsg.Text = "";
            ClearBankDetail();
            // UpdateBankDetails.Visible = false;
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkBankDetail = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkBankDetail");
            ViewState["Emp_NewID"] = lnkBankDetail.ToolTip.ToString();
            FillGridBankDetail();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal2()", true);
            //FillBankDetail();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillBankDetail()
    {
        try
        {
            lblMsg.Text = "";
            ddlBank_AccountType.ClearSelection();
            // FillDropdown();
            dsRecord = null;
            dsRecord = objdb.ByProcedure("SpHRBankDetail", new string[] { "flag", "Bank_ID" }, new string[] { "2", ViewState["Bank_ID"].ToString() }, "dataset");
            if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
            {
                txtBank_AccountNo.Text = dsRecord.Tables[0].Rows[0]["Bank_AccountNo"].ToString();
                ddlBank_AccountType.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Bank_AccountType"].ToString()).Selected = true;
                txtBank_Name.Text = dsRecord.Tables[0].Rows[0]["Bank_Name"].ToString();
                txtBank_BranchName.Text = dsRecord.Tables[0].Rows[0]["Bank_BranchName"].ToString();
                txtBank_IfscCode.Text = dsRecord.Tables[0].Rows[0]["Bank_IfscCode"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal2()", true);
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGridBankDetail()
    {
        try
        {
            GridViewBankDetail.DataSource = null;
            GridViewBankDetail.DataBind();
            ds = objdb.ByProcedure("SpHRBankDetail", new string[] { "flag", "Emp_ID" }, new string[] { "1", ViewState["Emp_NewID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblGridBankMsg.Text = "";
                GridViewBankDetail.DataSource = ds;
                GridViewBankDetail.DataBind();
            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                lblGridBankMsg.Text = "Sorry! No Record Found";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewBankDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblBankDetail.Text = "";
            //UpdateBankDetails.Visible = true;
            btnUpdateBankDetail.Text = "Update";
            ViewState["Bank_ID"] = GridViewBankDetail.SelectedDataKey.Value.ToString();
            FillBankDetail();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal2()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnUpdateBankDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";

            if (txtBank_AccountNo.Text == "")
            {
                msg += "Entar Account Number. <br/>";
            }
            if (ddlBank_AccountType.SelectedIndex == 0)
            {
                msg += "Select Account Type. <br/>";
            }
            if (txtBank_Name.Text == "")
            {
                msg += "Entar Bank Name. <br/>";
            }
            if (txtBank_BranchName.Text == "")
            {
                msg += "Entar Branch Name. <br/>";
            }
            if (txtBank_IfscCode.Text == "")
            {
                msg += "Entar Ifsc Code. <br/>";
            }
            if (msg.Trim() == "")
            {
                if (btnUpdateBankDetail.Text == "Update" && ViewState["Bank_ID"].ToString() != "")
                {
                    ds = objdb.ByProcedure("SpHRBankDetail",
                           new string[] { "flag", "Bank_ID", "Bank_AccountNo", "Bank_AccountType", "Bank_Name", "Bank_BranchName", "Bank_IfscCode", "Bank_UpdatedBy" },
                           new string[] { "3", ViewState["Bank_ID"].ToString(), txtBank_AccountNo.Text, ddlBank_AccountType.SelectedValue, txtBank_Name.Text, txtBank_BranchName.Text, txtBank_IfscCode.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                }
                if (btnUpdateBankDetail.Text == "Save" && ViewState["Emp_NewID"].ToString() != "")
                {
                    ds = objdb.ByProcedure("SpHRBankDetail",
                            new string[] { "flag", "Emp_ID", "Bank_AccountNo", "Bank_AccountType", "Bank_Name", "Bank_BranchName", "Bank_IfscCode", "Bank_UpdatedBy" },
                            new string[] { "0", ViewState["Emp_NewID"].ToString(), txtBank_AccountNo.Text, ddlBank_AccountType.SelectedValue, txtBank_Name.Text, txtBank_BranchName.Text, txtBank_IfscCode.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //  divBankDetail.Visible = false;
                    txtBank_AccountNo.Text = "";
                    ddlBank_AccountType.ClearSelection();
                    txtBank_Name.Text = "";
                    txtBank_BranchName.Text = "";
                    txtBank_IfscCode.Text = "";
                    lblBankDetail.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    btnUpdateBankDetail.Text = "Save";
                    FillGridBankDetail();
                    //UpdateBankDetails.Visible = false;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal2()", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record not save.');", true);
                }
            }
            else
            {
                lblBankDetail.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal2()", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClearBankDetail_Click(object sender, EventArgs e)
    {
        try
        {
            ClearBankDetail();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal2()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearBankDetail()
    {
        try
        {
            txtBank_AccountNo.Text = "";
            ddlBank_AccountType.ClearSelection();
            txtBank_Name.Text = "";
            txtBank_BranchName.Text = "";
            txtBank_IfscCode.Text = "";
            btnUpdateBankDetail.Text = "Save";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Start Fixed Assets Detail
    protected void lnkFixedAssetsDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblFixedAssetsDetail.Text = "";
            lblGridFixedAssetsMsg.Text = "";
            ClearFixedAssetDetail();
            // UpdateFixedAssetsDetails.Visible = false;
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkFixedAssetsDetail = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkFixedAssetsDetail");
            ViewState["Emp_NewID"] = lnkFixedAssetsDetail.ToolTip.ToString();
            FillGridFixedAssetsDetail();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal3()", true);
            //FillBankDetail();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillFixedAssetsDetail()
    {
        try
        {
            lblMsg.Text = "";
            FillDropdown();
            dsRecord = null;
            dsRecord = objdb.ByProcedure("SpHRProperty", new string[] { "flag", "Property_ID" }, new string[] { "2", ViewState["Property_ID"].ToString() }, "dataset");
            if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
            {
                ddlProperty_Type.ClearSelection();
                ddlProperty_Type.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Property_Type"].ToString()).Selected = true;
                txtProperty_Location.Text = dsRecord.Tables[0].Rows[0]["Property_Location"].ToString();
                txtProperty_PurchaseYear.Text = dsRecord.Tables[0].Rows[0]["Property_PurchaseYear"].ToString();
                txtProperty_PurchasePrice.Text = dsRecord.Tables[0].Rows[0]["Property_PurchasePrice"].ToString();
                txtProperty_CurrentRate.Text = dsRecord.Tables[0].Rows[0]["Property_CurrentRate"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal3()", true);
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGridFixedAssetsDetail()
    {
        try
        {
            GridViewFixedAssetsDetail.DataSource = null;
            GridViewFixedAssetsDetail.DataBind();
            ds = objdb.ByProcedure("SpHRProperty", new string[] { "flag", "Emp_ID" }, new string[] { "1", ViewState["Emp_NewID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblGridFixedAssetsMsg.Text = "";
                GridViewFixedAssetsDetail.DataSource = ds;
                GridViewFixedAssetsDetail.DataBind();
            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                lblGridFixedAssetsMsg.Text = "Sorry! No Record Found";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewFixedAssetsDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblFixedAssetsDetail.Text = "";
            ViewState["Property_ID"] = GridViewFixedAssetsDetail.SelectedDataKey.Value.ToString();
            FillFixedAssetsDetail();
            //UpdateFixedAssetsDetails.Visible = true;
            btnUpdateFixedAssetsDetail.Text = "Update";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal3()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnUpdateFixedAssetsDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";

            if (ddlProperty_Type.SelectedIndex == 0)
            {
                msg += "Select Type of Property. <br/>";
            }
            if (txtProperty_Location.Text == "")
            {
                msg += "Entar Location Of Property. <br/>";
            }
            if (txtProperty_PurchaseYear.Text == "")
            {
                msg += "Entar Property Purchase Year. <br/>";
            }
            if (txtProperty_PurchasePrice.Text == "")
            {
                msg += "Entar Property Purchase Price Rs. <br/>";
            }
            if (txtProperty_CurrentRate.Text == "")
            {
                msg += "Entar Current rate of property Rs. <br/>";
            }
            if (msg.Trim() == "")
            {
                if (btnUpdateFixedAssetsDetail.Text == "Update" && ViewState["Property_ID"].ToString() != "")
                {
                    ds = objdb.ByProcedure("SpHRProperty",
                    new string[] { "flag", "Property_ID", "Property_Type", "Property_Location", "Property_PurchaseYear", "Property_PurchasePrice", "Property_CurrentRate", "Property_UpdatedBy" },
                    new string[] { "3", ViewState["Property_ID"].ToString(), ddlProperty_Type.SelectedValue.ToString(), txtProperty_Location.Text, txtProperty_PurchaseYear.Text, txtProperty_PurchasePrice.Text, txtProperty_CurrentRate.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                }
                if (btnUpdateFixedAssetsDetail.Text == "Save" && ViewState["Emp_NewID"].ToString() != "")
                {
                    ds = objdb.ByProcedure("SpHRProperty",
                new string[] { "flag", "Emp_ID", "Property_Type", "Property_Location", "Property_PurchaseYear", "Property_PurchasePrice", "Property_CurrentRate", "Property_UpdatedBy" },
                new string[] { "0", ViewState["Emp_NewID"].ToString(), ddlProperty_Type.SelectedValue.ToString(), txtProperty_Location.Text, txtProperty_PurchaseYear.Text, txtProperty_PurchasePrice.Text, txtProperty_CurrentRate.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                }
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //  divFixedAssetsDetail.Visible = false;
                    ddlProperty_Type.ClearSelection();
                    txtProperty_Location.Text = "";
                    txtProperty_PurchaseYear.Text = "";
                    txtProperty_PurchasePrice.Text = "";
                    txtProperty_CurrentRate.Text = "";
                    lblFixedAssetsDetail.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillGridFixedAssetsDetail();
                    // UpdateFixedAssetsDetails.Visible = false;
                    btnUpdateFixedAssetsDetail.Text = "Save";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal3()", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record not save.');", true);
                }
            }
            else
            {
                lblFixedAssetsDetail.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal3()", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClearFixedAssetsDetail_Click(object sender, EventArgs e)
    {
        ClearFixedAssetDetail();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal3()", true);
    }

    protected void ClearFixedAssetDetail()
    {
        try
        {
            ddlProperty_Type.ClearSelection();
            txtProperty_Location.Text = "";
            txtProperty_PurchaseYear.Text = "";
            txtProperty_PurchasePrice.Text = "";
            txtProperty_CurrentRate.Text = "";
            btnUpdateFixedAssetsDetail.Text = "Save";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    // Chilldern Detail Section
    protected void lnkChildrenDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblChildrenDetail.Text = "";
            lblGridChilderMsg.Text = "";
            ClearChildrenDetail();
            // UpdateChildrenDetails.Visible = false;
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkChildrenDetail = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkChildrenDetail");
            ViewState["Emp_NewID"] = lnkChildrenDetail.ToolTip.ToString();
            FillGridChildrenDetail();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal4()", true);
            //FillBankDetail();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillChildrenDetail()
    {
        try
        {
            lblMsg.Text = "";
            FillDropdown();
            dsRecord = null;
            dsRecord = objdb.ByProcedure("SpHRChildDetail", new string[] { "flag", "Child_ID" }, new string[] { "2", ViewState["Child_ID"].ToString() }, "dataset");
            if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
            {
                ddlChild_Type.ClearSelection();
                ddlChild_Type.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Child_Type"].ToString()).Selected = true;
                txtChild_Name.Text = dsRecord.Tables[0].Rows[0]["Child_Name"].ToString();
                ddlChild_MaritalStatus.ClearSelection();
                ddlChild_MaritalStatus.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Child_MaritalStatus"].ToString()).Selected = true;
                txtChild_Dob.Text = dsRecord.Tables[0].Rows[0]["Child_Dob"].ToString();
                txtChild_Education.Text = dsRecord.Tables[0].Rows[0]["Child_Education"].ToString();
                txtChild_Business.Text = dsRecord.Tables[0].Rows[0]["Child_Business"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal4()", true);
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGridChildrenDetail()
    {
        try
        {
            GridViewChildrenDetail.DataSource = null;
            GridViewChildrenDetail.DataBind();
            ds = objdb.ByProcedure("SpHRChildDetail", new string[] { "flag", "Emp_ID" }, new string[] { "1", ViewState["Emp_NewID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblGridChilderMsg.Text = "";
                GridViewChildrenDetail.DataSource = ds;
                GridViewChildrenDetail.DataBind();
            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                lblGridChilderMsg.Text = "Sorry! No Record Found";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewChildrenDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblChildrenDetail.Text = "";
            //UpdateChildrenDetails.Visible = true;
            btnUpdateChildrenDetail.Text = "Update";
            ViewState["Child_ID"] = GridViewChildrenDetail.SelectedDataKey.Value.ToString();
            FillChildrenDetail();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal4()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnUpdateChildrenDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";

            if (ddlChild_Type.SelectedIndex == 0)
            {
                msg += "Select Son / Daughter. <br/>";
            }
            if (txtChild_Name.Text == "")
            {
                msg += "Entar Name. <br/>";
            }
            if (txtChild_Dob.Text == "")
            {
                msg += "Entar Date of birth. <br/>";
            }
            if (ddlChild_MaritalStatus.SelectedIndex == 0)
            {
                msg += "Select Marital Status. <br/>";
            }
            if (txtChild_Education.Text == "")
            {
                msg += "Entar Education. <br/>";
            }
            if (txtChild_Business.Text == "")
            {
                msg += "Entar Business / Job. <br/>";
            }
            if (msg.Trim() == "")
            {
                if (btnUpdateChildrenDetail.Text == "Update" && ViewState["Child_ID"].ToString() != "")
                {
                    ds = objdb.ByProcedure("SpHRChildDetail",
                            new string[] { "flag", "Child_ID", "Child_Type", "Child_Name", "Child_Dob", "Child_MaritalStatus", "Child_Education", "Child_Business", "Child_UpdatedBy" },
                            new string[] { "3", ViewState["Child_ID"].ToString(), ddlChild_Type.SelectedValue.ToString(), txtChild_Name.Text, Convert.ToDateTime(txtChild_Dob.Text, cult).ToString("yyyy/MM/dd"), ddlChild_MaritalStatus.SelectedValue.ToString(), txtChild_Education.Text, txtChild_Business.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                }
                if (btnUpdateChildrenDetail.Text == "Save" && ViewState["Emp_NewID"].ToString() != "")
                {
                    ds = objdb.ByProcedure("SpHRChildDetail",
                           new string[] { "flag", "Emp_ID", "Child_Type", "Child_Name", "Child_Dob", "Child_MaritalStatus", "Child_Education", "Child_Business", "Child_UpdatedBy" },
                           new string[] { "0", ViewState["Emp_NewID"].ToString(), ddlChild_Type.SelectedValue.ToString(), txtChild_Name.Text, Convert.ToDateTime(txtChild_Dob.Text, cult).ToString("yyyy/MM/dd"), ddlChild_MaritalStatus.SelectedValue.ToString(), txtChild_Education.Text, txtChild_Business.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                }
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // divChildrenDetail.Visible = false;
                    ddlChild_Type.ClearSelection();
                    txtChild_Name.Text = "";
                    txtChild_Dob.Text = "";
                    ddlChild_MaritalStatus.ClearSelection();
                    txtChild_Education.Text = "";
                    txtChild_Business.Text = "";
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillGridChildrenDetail();
                    // UpdateChildrenDetails.Visible = false;
                    btnUpdateChildrenDetail.Text = "Save";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal4()", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record not save.');", true);
                }
            }
            else
            {
                lblChildrenDetail.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal4()", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClearChildrenDetail_Click(object sender, EventArgs e)
    {
        ClearChildrenDetail();
        FillGridChildrenDetail();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal4()", true);
    }
    protected void ClearChildrenDetail()
    {
        try
        {
            ddlChild_Type.ClearSelection();
            txtChild_Name.Text = "";
            txtChild_Dob.Text = "";
            ddlChild_MaritalStatus.ClearSelection();
            txtChild_Education.Text = "";
            txtChild_Business.Text = "";
            btnUpdateChildrenDetail.Text = "Save";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    // Nominee Detail Section
    protected void lnkNomineeDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblNomineeDetail.Text = "";
            lblGridNomineeMsg.Text = "";
            ClearNomineeDetail();
            //UpdateNomineeDetails.Visible = false;
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkNomineeDetail = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkNomineeDetail");
            ViewState["Emp_NewID"] = lnkNomineeDetail.ToolTip.ToString();
            FillGridNomineeDetail();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal5()", true);
            //FillBankDetail();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGridNomineeDetail()
    {
        try
        {
            GridViewNomineeDetail.DataSource = null;
            GridViewNomineeDetail.DataBind();
            ds = objdb.ByProcedure("SpHRNomineeDetail", new string[] { "flag", "Emp_ID" }, new string[] { "1", ViewState["Emp_NewID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblGridNomineeMsg.Text = "";
                GridViewNomineeDetail.DataSource = ds;
                GridViewNomineeDetail.DataBind();
            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                lblGridNomineeMsg.Text = "Sorry! No Record Found";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClearNomineeDetail_Click(object sender, EventArgs e)
    {
        ClearNomineeDetail();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal5()", true);
    }
    protected void GridViewNomineeDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblNomineeDetail.Text = "";
            // UpdateNomineeDetails.Visible = true;
            btnUpdateNomineeDetail.Text = "Update";
            ViewState["Nominee_ID"] = GridViewNomineeDetail.SelectedDataKey.Value.ToString();
            FillNomineeDetail();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal4()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillNomineeDetail()
    {
        try
        {
            lblMsg.Text = "";
            FillDropdown();
            dsRecord = null;
            dsRecord = objdb.ByProcedure("SpHRNomineeDetail", new string[] { "flag", "Nominee_ID" }, new string[] { "2", ViewState["Nominee_ID"].ToString() }, "dataset");
            if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
            {
                txtNominee_Name.Text = dsRecord.Tables[0].Rows[0]["Nominee_Name"].ToString();
                txtNominee_Relation.Text = dsRecord.Tables[0].Rows[0]["Nominee_Relation"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal5()", true);
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnUpdateNomineeDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";

            if (txtNominee_Name.Text == "")
            {
                msg += "Entar Nominee Name. <br/>";
            }
            if (txtNominee_Relation.Text == "")
            {
                msg += "Entar Relation With Nominee. <br/>";
            }

            if (msg.Trim() == "")
            {
                if (btnUpdateNomineeDetail.Text == "Update" && ViewState["Nominee_ID"].ToString() != "")
                {
                    ds = objdb.ByProcedure("SpHRNomineeDetail",
                    new string[] { "flag", "Nominee_ID", "Nominee_Name", "Nominee_Relation", "Nominee_UpdatedBy" },
                    new string[] { "3", ViewState["Nominee_ID"].ToString(), txtNominee_Name.Text, txtNominee_Relation.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                }
                if (btnUpdateNomineeDetail.Text == "Save" && ViewState["Emp_NewID"].ToString() != "")
                {
                    ds = objdb.ByProcedure("SpHRNomineeDetail",
                 new string[] { "flag", "Emp_ID", "Nominee_Name", "Nominee_Relation", "Nominee_UpdatedBy" },
                 new string[] { "0", ViewState["Emp_NewID"].ToString(), txtNominee_Name.Text, txtNominee_Relation.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                }
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //  divNomineeDetail.Visible = false;
                    txtNominee_Name.Text = "";
                    txtNominee_Relation.Text = "";
                    lblNomineeDetail.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillGridNomineeDetail();
                    btnUpdateNomineeDetail.Text = "Save";
                    //UpdateNomineeDetails.Visible = false;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal5()", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record not save.');", true);
                }
            }
            else
            {
                lblNomineeDetail.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal5()", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearNomineeDetail()
    {
        try
        {
            txtNominee_Name.Text = "";
            txtNominee_Relation.Text = "";
            lblNomineeDetail.Text = "";
            btnUpdateNomineeDetail.Text = "Save";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    // Other Detail Section
    protected void btnClearOtherDetail_Click(object sender, EventArgs e)
    {
        ClearOtherDetail();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal6()", true);
    }
    protected void ClearOtherDetail()
    {
        try
        {
            txtOther_Training.Text = "";
            txtOther_Achievement.Text = "";
            lblOtherDetail.Text = "";
            btnUpdateOtherDetail.Text = "Save";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkOtherDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblOtherDetail.Text = "";
            lblGridOtherMsg.Text = "";
            ClearOtherDetail();
            //UpdateOtherDetail.Visible = false;
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkOtherDetail = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkOtherDetail");
            ViewState["Emp_NewID"] = lnkOtherDetail.ToolTip.ToString();
            FillGridOtherDetail();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal6()", true);
            //FillBankDetail();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGridOtherDetail()
    {
        try
        {
            GridViewOtherDetail.DataSource = null;
            GridViewOtherDetail.DataBind();
            ds = objdb.ByProcedure("SpHROtherDetail", new string[] { "flag", "Emp_ID" }, new string[] { "1", ViewState["Emp_NewID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblGridOtherMsg.Text = "";
                GridViewOtherDetail.DataSource = ds;
                GridViewOtherDetail.DataBind();
            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                lblGridOtherMsg.Text = "Sorry! No Record Found";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewOtherDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblOtherDetail.Text = "";
            lblGridOtherMsg.Text = "";
            btnUpdateOtherDetail.Text = "Update";
            //UpdateOtherDetail.Visible = true;
            ViewState["Other_ID"] = GridViewOtherDetail.SelectedDataKey.Value.ToString();
            FillOtherDetail();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal6()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOtherDetail()
    {
        try
        {
            lblMsg.Text = "";
            FillDropdown();
            dsRecord = null;
            dsRecord = objdb.ByProcedure("SpHROtherDetail", new string[] { "flag", "Other_ID" }, new string[] { "2", ViewState["Other_ID"].ToString() }, "dataset");
            if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
            {
                txtOther_Training.Text = dsRecord.Tables[0].Rows[0]["Other_Training"].ToString();
                txtOther_Achievement.Text = dsRecord.Tables[0].Rows[0]["Other_Achievement"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal6()", true);
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnUpdateOtherDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";

            if (txtOther_Training.Text == "")
            {
                msg += "Entar प्रशिक्षण से संबंधित जानकारी (Training related Information ). \\n";
            }
            if (txtOther_Achievement.Text == "")
            {
                msg += "Entar विशेष उपलब्धि आदि की जानकारी (इस क़ालम में निगम में यदि आपको पुरस्कृत किया है अथवा प्रशारित पत्र आदि दिया गया है उसका विवरण दिया जाना है ). \\n";
            }

            if (msg.Trim() == "")
            {
                if (btnUpdateOtherDetail.Text == "Update" && ViewState["Other_ID"].ToString() != "")
                {
                    ds = objdb.ByProcedure("SpHROtherDetail",
                    new string[] { "flag", "Other_ID", "Other_Training", "Other_Achievement", "Other_UpdatedBy" },
                    new string[] { "3", ViewState["Other_ID"].ToString(), txtOther_Training.Text, txtOther_Achievement.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                }
                if (btnUpdateOtherDetail.Text == "Save" && ViewState["Emp_NewID"].ToString() != "")
                {
                    ds = objdb.ByProcedure("SpHROtherDetail",
                    new string[] { "flag", "Emp_ID", "Other_Training", "Other_Achievement", "Other_UpdatedBy" },
                    new string[] { "0", ViewState["Emp_NewID"].ToString(), txtOther_Training.Text, txtOther_Achievement.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //   divOtherDetail.Visible = false;
                    txtOther_Training.Text = "";
                    txtOther_Achievement.Text = "";
                    lblOtherDetail.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillGridOtherDetail();
                    btnUpdateOtherDetail.Text = "Save";
                    txtOther_Training.Text = "";
                    txtOther_Achievement.Text = "";
                    //UpdateOtherDetail.Visible = false;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal6()", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record not save.');", true);
                }
            }
            else
            {
                lblOtherDetail.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal6()", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "SearchText" }, new string[] { "27", txtSearch.Text }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkVerify_Click1(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkVerify = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkVerify");
            ViewState["Emp_NewID"] = lnkVerify.ToolTip.ToString();
            ds = null;
            if (ViewState["Emp_NewID"].ToString() != "")
            {
                ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Emp_ID", "Emp_UpdatedBy" }, new string[] { "9", ViewState["Emp_NewID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            }
            //    LinkButton lnkOfficialDetail = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkOfficialDetail");
            //    LinkButton lnkPersonalDetail = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkPersonalDetail");
            //    LinkButton lnkBankDetail = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkBankDetail");
            //    LinkButton lnkFixedAssetsDetail = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkFixedAssetsDetail");
            //    LinkButton lnkChildrenDetail = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkChildrenDetail");
            //    LinkButton lnkNomineeDetail = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkNomineeDetail");
            //    LinkButton lnkOtherDetail = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkOtherDetail");


            //    lnkOfficialDetail.Enabled = false;
            //    lnkPersonalDetail.Enabled = false;
            //    lnkBankDetail.Enabled = false;
            //    lnkFixedAssetsDetail.Enabled = false;
            //    lnkChildrenDetail.Enabled = false;
            //    lnkNomineeDetail.Enabled = false;
            //    lnkOtherDetail.Enabled = false;

            FillDetail();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}