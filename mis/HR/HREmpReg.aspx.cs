using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

public partial class mis_HR_HREmpReg : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Emp_ID"] != null)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Emp_IDNew"] = "0";
                Visiblebtn();
                TextReadonly();
                FillDropdown();
                FillYear();
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
    }
    // Personal Details
    protected void ddlEmp_CurState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlEmp_CurCity.Items.Clear();
            ddlEmp_CurCity.Items.Insert(0, new ListItem("Select", "0"));
            if (ddlEmp_CurState.SelectedIndex > 0)
            {
                FillCurCity();
            }
            // ddlEmp_CurState.Focus();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillCurCity()
    {
        try
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
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlEmp_PerState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlEmp_PerCity.Items.Clear();
            ddlEmp_PerCity.Items.Insert(0, new ListItem("Select", "0"));
            if (ddlEmp_PerState.SelectedIndex > 0)
            {
                FillPerCity();
            }
            //ddlEmp_PerState.Focus();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillPerCity()
    {
        try
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
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSavePersonalDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string Emp_ProfileImage = "";
            if (txtEmp_Name.Text == "")
            {
                msg += "Entar Employee Name. \\n";
            }
            if (ddlEmp_Gender.SelectedIndex == 0)
            {
                msg += "Select Gender. \\n";
            }
            if (txtEmp_Dob.Text == "")
            {
                msg += "Entar Date of birth. \\n";
            }
            if (ddlEmp_Relation.SelectedIndex == 0)
            {
                msg += "Select Relation. \\n";
            }
            if (txtEmp_FatherHusbandName.Text == "")
            {
                msg += "Enter Father /Husband Name. \\n";
            }
            if (ddlEmp_MaritalStatus.SelectedIndex == 0)
            {
                msg += "Select Marital Status. \\n";
            }
            if (ddlEmp_BloodGroup.SelectedIndex == 0)
            {
                msg += "Select Blood Group. \\n";
            }
            if (txtEmp_MobileNo.Text == "")
            {
                msg += "Enter Mobile Number. \\n";
            }
            if (txtEmp_AadharNo.Text == "")
            {
                msg += "Enter Aadhar Number. \\n";
            }
            if (txtEmp_PanCardNo.Text == "")
            {
                msg += "Enter Pan Card Number. \\n";
            }
            if (txtEmp_Email.Text == "")
            {
                msg += "Enter Email ID. \\n";
            }
            if (!FU_Emp_ProfileImage.HasFile)
            {
                msg += "Select Profile Image. \\n";
            }
            //if (txtEmp_HusbWifeName.Text == "" && ddlEmp_MaritalStatus.SelectedIndex != 1)
            //{
            //    msg += "Enter Husband/Wife Name. \\n";
            //}
            //if (txtEmp_HusbWifeJob.Text == "" && ddlEmp_MaritalStatus.SelectedIndex != 1)
            //{
            //    msg += "Enter Husband/Wife Job/Business . \\n";
            //}
            //if (txtEmp_HusbWifeDep.Text == "" && ddlEmp_MaritalStatus.SelectedIndex != 1)
            //{
            //    msg += "Enter Husband/Wife Designation/Department . \\n";
            //}
            if (ddlEmp_Religion.SelectedIndex == 0)
            {
                msg += "Select Category. \\n";
            }
            if (ddlEmp_Category.SelectedIndex == 0)
            {
                msg += "Select Religion. \\n";
            }
            if (ddlEmp_DisabilityType.SelectedValue == "Select")
            {
                msg += "Select Disability Type. \\n";
            }
            if (ddlEmp_CurState.SelectedIndex == 0)
            {
                msg += "Select Current State. \\n";
            }
            if (ddlEmp_CurCity.SelectedIndex == 0)
            {
                msg += "Select Current City. \\n";
            }
            if (txtEmp_CurPinCode.Text == "")
            {
                msg += "Enter Current Pin Code . \\n";
            }
            if (txtEmp_CurAddress.Text == "")
            {
                msg += "Enter Current Address . \\n";
            }

            if (ddlEmp_PerState.SelectedIndex == 0)
            {
                msg += "Select Permanent State. \\n";
            }
            if (ddlEmp_PerCity.SelectedIndex == 0)
            {
                msg += "Select Permanent City. \\n";
            }
            if (txtEmp_PerPinCode.Text == "")
            {
                msg += "Enter Permanent Pin Code . \\n";
            }
            if (txtEmp_PerAddress.Text == "")
            {
                msg += "Enter Permanent Address . \\n";
            }
            if (msg.Trim() == "" && ViewState["Emp_IDNew"].ToString() == "0")
            {
                lblDob.Text = "";
                if (FU_Emp_ProfileImage.HasFile)
                {
                    Emp_ProfileImage = "UploadDoc/" + Guid.NewGuid() + "-" + FU_Emp_ProfileImage.FileName;
                    FU_Emp_ProfileImage.PostedFile.SaveAs(Server.MapPath(Emp_ProfileImage));
                }
                string msg1 = "";
                DataSet ds1 = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Emp_AadharNo", "Emp_PanCardNo", "Emp_MobileNo", "Emp_Email" }, new string[] { "34", txtEmp_AadharNo.Text, txtEmp_PanCardNo.Text, txtEmp_MobileNo.Text, txtEmp_Email.Text }, "dataset");
                if (ds1.Tables[0].Rows.Count != 0)
                {
                    msg1 += "AADHAAR No. is Already Exists.\\n";
                }
                if (ds1.Tables[1].Rows.Count != 0)
                {
                    msg1 += "PAN CARD No. is Already Exists. \\n";
                }
                if (ds1.Tables[2].Rows.Count != 0)
                {
                    msg1 += "MOBILE No. is Already Exists. \\n";
                }
                if (ds1.Tables[3].Rows.Count != 0)
                {
                    msg1 += "EMAIL ID is Already Exists. \\n";
                }
                if (msg1 == "")
                {
                    ds = objdb.ByProcedure("SpHREmployee",
              new string[] { "flag", "Emp_Name", "Emp_Gender", "Emp_Dob", "Emp_Relation", "Emp_FatherHusbandName", "Emp_MaritalStatus", "Emp_BloodGroup", "Emp_MobileNo", "Emp_AadharNo", "Emp_PanCardNo", "Emp_Email", "Emp_ProfileImage", "Emp_HusbWifeName", "Emp_HusbWifeJob", "Emp_HusbWifeDep", "Emp_Category", "Emp_Religion", "Emp_Disability", "Emp_DisabilityType", "Emp_CurState", "Emp_CurCity", "Emp_CurPinCode", "Emp_CurAddress", "Emp_PerState", "Emp_PerCity", "Emp_PerPinCode", "Emp_PerAddress", "Emp_UpdatedBy" },
              new string[] { "0", txtEmp_Name.Text, ddlEmp_Gender.SelectedValue.ToString(), Convert.ToDateTime(txtEmp_Dob.Text, cult).ToString("yyyy/MM/dd"), ddlEmp_Relation.SelectedValue.ToString(), txtEmp_FatherHusbandName.Text, ddlEmp_MaritalStatus.SelectedValue.ToString(), ddlEmp_BloodGroup.SelectedValue.ToString(), txtEmp_MobileNo.Text, txtEmp_AadharNo.Text, txtEmp_PanCardNo.Text, txtEmp_Email.Text, Emp_ProfileImage,
                  txtEmp_HusbWifeName.Text, txtEmp_HusbWifeJob.Text, txtEmp_HusbWifeDep.Text, ddlEmp_Category.SelectedValue.ToString(), ddlEmp_Religion.SelectedValue.ToString(), rbtEmp_Disability.SelectedValue.ToString(),ddlEmp_DisabilityType.SelectedValue.ToString(), ddlEmp_CurState.SelectedValue.ToString(), ddlEmp_CurCity.SelectedValue.ToString(), txtEmp_CurPinCode.Text, txtEmp_CurAddress.Text, ddlEmp_PerState.SelectedValue.ToString(), ddlEmp_PerCity.SelectedValue.ToString(), txtEmp_PerPinCode.Text, txtEmp_PerAddress.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["Emp_IDNew"] = ds.Tables[0].Rows[0]["Emp_ID"].ToString();
                        txtUserName.Text = ds.Tables[0].Rows[0]["UserName"].ToString();
                        txtPassword.Text = ds.Tables[0].Rows[0]["Password"].ToString();
                        lblDob.Text = ds.Tables[0].Rows[0]["Emp_Dob"].ToString();
                        txtEmp_RetirementDate.Text = ds.Tables[0].Rows[0]["Emp_Retiredate"].ToString();
                        divPersonalDetail.Visible = false;
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record not save.');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg1 + "');", true);
                }
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
    protected void btnPersonalDetail_Click(object sender, EventArgs e)
    {
        try
        {
            //Changetabcolour();
            btnPersonalDetail.CssClass = "btn btn-block btn-danger";

            btnOfficialDetail.CssClass = "btn btn-block btn-default";
            btnBankDetail.CssClass = "btn btn-block btn-default";
            btnFixedAssetsDetail.CssClass = "btn btn-block btn-default";
            btnChildrenDetail.CssClass = "btn btn-block btn-default";
            btnNomineeDetail.CssClass = "btn btn-block btn-default";
            btnOtherDetail.CssClass = "btn btn-block btn-default";

            lblMsg.Text = "";
            Visiblebtn();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void btnClearPersonalDetail_Click(object sender, EventArgs e)
    {
        ClearPersonalDetail();
    }
    protected void ClearPersonalDetail()
    {
        txtEmp_Name.Text = "";
        ddlEmp_Gender.ClearSelection();
        txtEmp_Dob.Text = "";
        ddlEmp_Relation.ClearSelection();
        txtEmp_FatherHusbandName.Text = "";
        ddlEmp_MaritalStatus.ClearSelection();
        ddlEmp_BloodGroup.ClearSelection();
        txtEmp_MobileNo.Text = "";
        txtEmp_MobileNo.Text = "";
        txtEmp_AadharNo.Text = "";
        txtEmp_AadharNo.Text = "";
        txtEmp_PanCardNo.Text = "";
        txtEmp_PanCardNo.Text = "";
        txtEmp_Email.Text = "";
        txtEmp_HusbWifeName.Text = "";
        txtEmp_HusbWifeJob.Text = "";
        txtEmp_HusbWifeDep.Text = "";
        ddlEmp_Category.ClearSelection();
        ddlEmp_Religion.ClearSelection();
        ddlEmp_DisabilityType.ClearSelection();
        ddlEmp_CurState.ClearSelection();
        ddlEmp_CurCity.ClearSelection();
        txtEmp_CurPinCode.Text = "";
        txtEmp_CurAddress.Text = "";
        ddlEmp_PerState.ClearSelection();
        ddlEmp_PerCity.ClearSelection();
        txtEmp_PerPinCode.Text = "";
        txtEmp_PerAddress.Text = "";

    }
    // Official Details
    protected void ddlOfficeType_Title_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlOffice_ID.Items.Clear();
            ddlOffice_ID.Items.Insert(0, new ListItem("Select", "0"));
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
            ddlOfficeType_Title.Focus();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlEmp_Class_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlDesignation_ID.Items.Clear();
            ddlDesignation_ID.Items.Insert(0, new ListItem("Select", "0"));
            ds = null;
            ds = objdb.ByProcedure("SpAdminDesignation", new string[] { "flag", "Class" }, new string[] { "6", ddlEmp_Class.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlDesignation_ID.DataSource = ds;
                ddlDesignation_ID.DataTextField = "Designation_Name";
                ddlDesignation_ID.DataValueField = "Designation_ID";
                ddlDesignation_ID.DataBind();
                ddlDesignation_ID.Items.Insert(0, new ListItem("Select", "0"));
            }
            ddlEmp_Class.Focus();

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
            ddlPayScale_ID.Focus();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSaveOfficialDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string Emp_ProfileImage = "";

            if (ddlOfficeType_Title.SelectedIndex == 0)
            {
                msg += "Select Office Type. \\n";
            }
            if (ddlOffice_ID.SelectedIndex == 0)
            {
                msg += "Select Office. \\n";
            }
            if (ddlLevel.SelectedIndex == 0)
            {
                msg += "Select Level. \\n";
            }
            if (ddlEmp_Class.SelectedIndex == 0)
            {
                msg += "Select Class. \\n";
            }
            if (ddlDepartment_ID.SelectedIndex == 0)
            {
                msg += "Select Department. \\n";
            }
            if (ddlDesignation_ID.SelectedIndex == 0)
            {
                msg += "Select Designation. \\n";
            }
            if (ddlPayScale_ID.SelectedIndex == 0)
            {
                msg += "Select Pay Scale. \\n";
            }
            if (ddlGradPay_ID.SelectedIndex == 0)
            {
                msg += "Select Grad Pay. \\n";
            }
            if (txtEmp_BasicSalery.Text == "")
            {
                msg += "Entar Basic Salery. \\n";
            }
            if (txtEmp_JoiningDate.Text == "")
            {
                msg += "Entar Joining Date. \\n";
            }
            if (txtEmp_PostingDate.Text == "")
            {
                msg += "Entar Posting Date. \\n";
            }
            if (txtEmp_RetirementDate.Text == "")
            {
                msg += "Entar Retirement Date. \\n";
            }
            if (ddlEmp_TypeOfRecruitment.SelectedIndex == 0)
            {
                msg += "Select Type of Recruitment. \\n";
            }
            if (ddlEmp_TypeOfPost.SelectedIndex == 0)
            {
                msg += "Select Type of Post. \\n";
            }
            if (ddlEmp_GpfType.SelectedIndex == 0)
            {
                msg += "Select GPF / DPF /NPS . \\n";
            }
            if (txtEmp_GpfNo.Text == "")
            {
                msg += "Entar GPF/DPF/NPS No. \\n";
            }

            if (msg.Trim() == "" && ViewState["Emp_IDNew"].ToString() != "0")
            {
                ds = objdb.ByProcedure("SpHREmployee",
                new string[] { "flag", "Emp_ID", "EmpLevel_ID", "Emp_Class", "Department_ID", "Designation_ID", "PayScale_ID", "GradPay_ID", "Emp_BasicSalery", "Emp_JoiningDate", "Emp_PostingDate", "Emp_RetirementDate", "Emp_TypeOfRecruitment", "Emp_TypeOfPost", "Emp_GpfType", "Emp_GpfNo", "OfficeType_Title", "Office_ID", "Emp_UpdatedBy" },
                new string[] { "1", ViewState["Emp_IDNew"].ToString(), ddlLevel.SelectedValue.ToString(), ddlEmp_Class.SelectedValue.ToString(), ddlDepartment_ID.SelectedValue.ToString(), ddlDesignation_ID.SelectedValue.ToString(), ddlPayScale_ID.SelectedValue.ToString(), ddlGradPay_ID.SelectedValue.ToString(), txtEmp_BasicSalery.Text, Convert.ToDateTime(txtEmp_JoiningDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtEmp_PostingDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtEmp_RetirementDate.Text, cult).ToString("yyyy/MM/dd"), ddlEmp_TypeOfRecruitment.SelectedValue.ToString(), ddlEmp_TypeOfPost.SelectedValue.ToString(), ddlEmp_GpfType.SelectedValue.ToString(), txtEmp_GpfNo.Text, ddlOfficeType_Title.SelectedValue.ToString(), ddlOffice_ID.SelectedValue.ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    btnFinalSubmit.Visible = true;
                    divOfficialDetail.Visible = false;
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record not save.');", true);
                }
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
    protected void btnOfficialDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (ViewState["Emp_IDNew"].ToString() != "0")
            {
                //Changetabcolour();
                btnOfficialDetail.CssClass = "btn btn-block btn-danger";//For Active Tab

                btnPersonalDetail.CssClass = "btn btn-block btn-default";
                btnBankDetail.CssClass = "btn btn-block btn-default"; 
                btnFixedAssetsDetail.CssClass = "btn btn-block btn-default";
                btnChildrenDetail.CssClass = "btn btn-block btn-default";
                btnNomineeDetail.CssClass = "btn btn-block btn-default";
                btnOtherDetail.CssClass = "btn btn-block btn-default";

                boxPersonalDetail.Visible = false;
                boxOfficialDetail.Visible = true;
                boxBankDetail.Visible = false;
                boxFixedAssetsDetail.Visible = false;
                boxChildrenDetail.Visible = false;
                boxNomineeDetail.Visible = false;
                boxOtherDetail.Visible = false;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Enter Personal Details.');", true);

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClearOfficialDetail_Click(object sender, EventArgs e)
    {
        ClearOfficialDetail();
    }
    protected void ClearOfficialDetail()
    {
        ddlOfficeType_Title.ClearSelection();
        ddlOffice_ID.ClearSelection();
        ddlEmp_Class.ClearSelection();
        ddlDepartment_ID.ClearSelection();
        ddlDesignation_ID.ClearSelection();
        ddlPayScale_ID.ClearSelection();
        ddlGradPay_ID.ClearSelection();
        txtEmp_BasicSalery.Text = "";
        txtEmp_JoiningDate.Text = "";
        txtEmp_PostingDate.Text = "";
        txtEmp_RetirementDate.Text = "";
        ddlEmp_TypeOfRecruitment.ClearSelection();
        ddlEmp_TypeOfPost.ClearSelection();
        ddlEmp_GpfType.ClearSelection();
        txtEmp_GpfNo.Text = "";
    }
    protected void FillBank()
    {
        try
        {
            ddlBank_Name.Items.Clear();
            ds = objdb.ByProcedure("SpHRBankDetail", new string[] { "flag" }, new string[] { "4" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlBank_Name.DataSource = ds;
                ddlBank_Name.DataTextField = "BankName";
                ddlBank_Name.DataValueField = "Bank_id";
                ddlBank_Name.DataBind();


            }
            ddlBank_Name.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    // Bank Details

    protected void btnSaveBankDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";

            if (txtBank_AccountNo.Text == "")
            {
                msg += "Entar Account Number. \\n";
            }
            if (ddlBank_AccountType.SelectedIndex == 0)
            {
                msg += "Select Account Type. <br/>";
            }
            //if (txtBank_Name.Text == "")
            //{
            //    msg += "Entar Bank Name. \\n";
            //}
            if (ddlBank_Name.SelectedIndex == 0)
            {
                msg += "Select Bank Name. \\n";
            }
            if (txtBank_BranchName.Text == "")
            {
                msg += "Entar Branch Name. \\n";
            }
            if (txtBank_IfscCode.Text == "")
            {
                msg += "Entar Ifsc Code. \\n";
            }
            if (msg.Trim() == "" && ViewState["Emp_IDNew"].ToString() != "0")
            {

                string msg1 = "";
                DataSet ds1 = objdb.ByProcedure("SpHRBankDetail", new string[] { "flag", "Bank_AccountNo" }, new string[] { "5", txtBank_AccountNo.Text }, "dataset");
                if (ds1.Tables[0].Rows.Count != 0)
                {
                    msg1 += "Bank Account No. is Already Exists.\\n";
                }

                if (msg1 == "")
                {

                    // ds = objdb.ByProcedure("SpHRBankDetail",
                    // new string[] { "flag", "Emp_ID", "Bank_AccountNo", "Bank_AccountType", "Bank_Name", "Bank_BranchName", "Bank_IfscCode", "Bank_UpdatedBy" },
                    // new string[] { "0", ViewState["Emp_IDNew"].ToString(), txtBank_AccountNo.Text, ddlBank_AccountType.SelectedValue, txtBank_Name.Text, txtBank_BranchName.Text, txtBank_IfscCode.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                    ds = objdb.ByProcedure("SpHRBankDetail",
                    new string[] { "flag", "Emp_ID", "Bank_AccountNo", "Bank_AccountType", "Bank", "Bank_Name", "Bank_BranchName", "Bank_IfscCode", "Bank_UpdatedBy" },
                    new string[] { "0", ViewState["Emp_IDNew"].ToString(), txtBank_AccountNo.Text, ddlBank_AccountType.SelectedValue, ddlBank_Name.SelectedValue, ddlBank_Name.SelectedItem.Text, txtBank_BranchName.Text, txtBank_IfscCode.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //  divBankDetail.Visible = false;
                        txtBank_AccountNo.Text = "";
                        // txtBank_Name.Text = "";
                        ddlBank_Name.ClearSelection();
                        txtBank_BranchName.Text = "";
                        txtBank_IfscCode.Text = "";
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        FillBankDetail();
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record not save.');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg1 + "');", true);
                }

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
    protected void btnBankDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (ViewState["Emp_IDNew"].ToString() != "0")
            {
                //Changetabcolour();
                btnBankDetail.CssClass = "btn btn-flat btn-danger";//for active tab
                btnPersonalDetail.CssClass = "btn btn-block btn-default";
                btnOfficialDetail.CssClass = "btn btn-block btn-default";
                btnFixedAssetsDetail.CssClass = "btn btn-block btn-default";
                btnChildrenDetail.CssClass = "btn btn-block btn-default";
                btnNomineeDetail.CssClass = "btn btn-block btn-default";
                btnOtherDetail.CssClass = "btn btn-block btn-default";


                boxPersonalDetail.Visible = false;
                boxOfficialDetail.Visible = false;
                boxBankDetail.Visible = true;
                boxFixedAssetsDetail.Visible = false;
                boxChildrenDetail.Visible = false;
                boxNomineeDetail.Visible = false;
                boxOtherDetail.Visible = false;
                FillBankDetail();
                FillBank();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Enter Personal Details.');", true);
            }
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
            GridViewBankDetail.DataSource = null;
            GridViewBankDetail.DataBind();
            ds = objdb.ByProcedure("SpHRBankDetail", new string[] { "flag", "Emp_ID" }, new string[] { "1", ViewState["Emp_IDNew"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridViewBankDetail.DataSource = ds;
                GridViewBankDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClearBankDetail_Click(object sender, EventArgs e)
    {
        ClearBankDetail();
    }
    protected void ClearBankDetail()
    {
        txtBank_AccountNo.Text = "";
        ddlBank_AccountType.ClearSelection();
        //txtBank_Name.Text = "";
        ddlBank_Name.ClearSelection();
        txtBank_BranchName.Text = "";
        txtBank_IfscCode.Text = "";
    }
    // Fixed Assets Details
    protected void btnSaveFixedAssetsDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";

            if (ddlProperty_Type.SelectedIndex == 0)
            {
                msg += "Select Type of Property. \\n";
            }
            if (txtProperty_Location.Text == "")
            {
                msg += "Entar Location Of Property. \\n";
            }
            if (ddlProperty_PurchaseYear.SelectedIndex == 0)
            {
                msg += "Select Property Purchase Year. \\n";
            }
            if (txtProperty_PurchasePrice.Text == "")
            {
                msg += "Entar Property Purchase Price Rs. \\n";
            }
            if (txtProperty_CurrentRate.Text == "")
            {
                msg += "Entar Current rate of property Rs. \\n";
            }
            if (msg.Trim() == "" && ViewState["Emp_IDNew"].ToString() != "0")
            {
                ds = objdb.ByProcedure("SpHRProperty",
                new string[] { "flag", "Emp_ID", "Property_Type", "Property_Location", "Property_PurchaseYear", "Property_PurchasePrice", "Property_CurrentRate", "Property_UpdatedBy" },
                new string[] { "0", ViewState["Emp_IDNew"].ToString(), ddlProperty_Type.SelectedValue.ToString(), txtProperty_Location.Text, ddlProperty_PurchaseYear.SelectedValue, txtProperty_PurchasePrice.Text, txtProperty_CurrentRate.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //  divFixedAssetsDetail.Visible = false;
                    ddlProperty_Type.ClearSelection();
                    txtProperty_Location.Text = "";
                    ddlProperty_PurchaseYear.ClearSelection();
                    txtProperty_PurchasePrice.Text = "";
                    txtProperty_CurrentRate.Text = "";
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillFixedAssetsDetail();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record not save.');", true);
                }
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
    protected void btnFixedAssetsDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (ViewState["Emp_IDNew"].ToString() != "0")
            {
                //Changetabcolour();
                btnFixedAssetsDetail.CssClass = "btn btn-flat btn-danger";//for active tab
                btnPersonalDetail.CssClass = "btn btn-block btn-default";
                btnOfficialDetail.CssClass = "btn btn-block btn-default";
                btnBankDetail.CssClass = "btn btn-block btn-default"; 
                btnChildrenDetail.CssClass = "btn btn-block btn-default";
                btnNomineeDetail.CssClass = "btn btn-block btn-default";
                btnOtherDetail.CssClass = "btn btn-block btn-default";

                boxPersonalDetail.Visible = false;
                boxOfficialDetail.Visible = false;
                boxBankDetail.Visible = false;
                boxFixedAssetsDetail.Visible = true;
                boxChildrenDetail.Visible = false;
                boxNomineeDetail.Visible = false;
                boxOtherDetail.Visible = false;
                FillFixedAssetsDetail();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Enter Personal Details.');", true);
            }
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
            GridViewFixedAssetsDetail.DataSource = null;
            GridViewFixedAssetsDetail.DataBind();
            ds = objdb.ByProcedure("SpHRProperty", new string[] { "flag", "Emp_ID" }, new string[] { "1", ViewState["Emp_IDNew"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridViewFixedAssetsDetail.DataSource = ds;
                GridViewFixedAssetsDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    //Add by Rohit
    protected void FillYear()
    {
        ddlProperty_PurchaseYear.Items.Clear();

        for (int i = DateTime.Now.Year; i >= 1950; i--)
        {
            ddlProperty_PurchaseYear.Items.Add(i.ToString());
        }
        ddlProperty_PurchaseYear.Items.Insert(0, new ListItem("Select", "0"));

    }
    protected void btnClearFixedAssetsDetail_Click(object sender, EventArgs e)
    {
        ClearFixedAssetsDetail();
    }
    protected void ClearFixedAssetsDetail()
    {
        ddlProperty_Type.ClearSelection();
        txtProperty_Location.Text = "";
        ddlProperty_PurchaseYear.ClearSelection();
        txtProperty_PurchasePrice.Text = "";
        txtProperty_CurrentRate.Text = "";

    }
    // Children Details
    protected void btnSaveChildrenDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";

            //if (ddlChild_Type.SelectedIndex == 0)
            //{
            //    msg += "Select Son / Daughter. \\n";
            //}
            //if (txtChild_Name.Text == "")
            //{
            //    msg += "Entar Name. \\n";
            //}
            //if (txtChild_Dob.Text == "")
            //{
            //    msg += "Entar Date of birth. \\n";
            //}
            //if (ddlChild_MaritalStatus.SelectedIndex == 0)
            //{
            //    msg += "Select Marital Status. \\n";
            //}
            //if (txtChild_Education.Text == "")
            //{
            //    msg += "Entar Education. \\n";
            //}
            //if (txtChild_Business.Text == "")
            //{
            //    msg += "Entar Business / Job. \\n";
            //}
            if (msg.Trim() == "" && ViewState["Emp_IDNew"].ToString() != "0")
            {
                ds = objdb.ByProcedure("SpHRChildDetail",
                new string[] { "flag", "Emp_ID", "Child_Type", "Child_Name", "Child_Dob", "Child_MaritalStatus", "Child_Education", "Child_Business", "Child_UpdatedBy" },
                new string[] { "0", ViewState["Emp_IDNew"].ToString(), ddlChild_Type.SelectedValue.ToString(), txtChild_Name.Text, Convert.ToDateTime(txtChild_Dob.Text, cult).ToString("yyyy/MM/dd"), ddlChild_MaritalStatus.SelectedValue.ToString(), txtChild_Education.Text, txtChild_Business.Text, ViewState["Emp_ID"].ToString() }, "dataset");
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
                    FillChildrenDetail();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record not save.');", true);
                }
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
    protected void btnChildrenDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (ViewState["Emp_IDNew"].ToString() != "0")
            {
                //Changetabcolour();
                btnChildrenDetail.CssClass = "btn btn-flat btn-danger";//for active tab

                btnPersonalDetail.CssClass = "btn btn-block btn-default";
                btnOfficialDetail.CssClass = "btn btn-block btn-default";
                btnBankDetail.CssClass = "btn btn-block btn-default";
                btnFixedAssetsDetail.CssClass = "btn btn-block btn-default";
                btnNomineeDetail.CssClass = "btn btn-block btn-default";
                btnOtherDetail.CssClass = "btn btn-block btn-default";



                boxPersonalDetail.Visible = false;
                boxOfficialDetail.Visible = false;
                boxBankDetail.Visible = false;
                boxFixedAssetsDetail.Visible = false;
                boxChildrenDetail.Visible = true;
                boxNomineeDetail.Visible = false;
                boxOtherDetail.Visible = false;
                FillChildrenDetail();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Enter Personal Details.');", true);
            }
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
            GridViewChildrenDetail.DataSource = null;
            GridViewChildrenDetail.DataBind();
            ds = objdb.ByProcedure("SpHRChildDetail", new string[] { "flag", "Emp_ID" }, new string[] { "1", ViewState["Emp_IDNew"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridViewChildrenDetail.DataSource = ds;
                GridViewChildrenDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClearChildrenDetail_Click(object sender, EventArgs e)
    {
        ClearChildernDetail();
    }
    protected void ClearChildernDetail()
    {
        ddlChild_Type.ClearSelection();
        txtChild_Name.Text = "";
        txtChild_Dob.Text = "";
        ddlChild_MaritalStatus.ClearSelection();
        txtChild_Education.Text = "";
        txtChild_Business.Text = "";
    }
    // Nominee Detail
    protected void btnSaveNomineeDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";

            //if (txtNominee_Name.Text == "")
            //{
            //    msg += "Entar Nominee Name. \\n";
            //}
            //if (txtNominee_Relation.Text == "")
            //{
            //    msg += "Entar Relation With Nominee. \\n";
            //}

            if (msg.Trim() == "" && ViewState["Emp_IDNew"].ToString() != "0")
            {
                ds = objdb.ByProcedure("SpHRNomineeDetail",
                new string[] { "flag", "Emp_ID", "Nominee_Name", "Nominee_Relation", "Nominee_UpdatedBy" },
                new string[] { "0", ViewState["Emp_IDNew"].ToString(), txtNominee_Name.Text, txtNominee_Relation.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //  divNomineeDetail.Visible = false;
                    txtNominee_Name.Text = "";
                    txtNominee_Relation.Text = "";
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillNomineeDetail();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record not save.');", true);
                }
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
    protected void btnNomineeDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (ViewState["Emp_IDNew"].ToString() != "0")
            {
                //Changetabcolour();
                btnNomineeDetail.CssClass = "btn btn-flat btn-danger";//For Active Tab

                btnPersonalDetail.CssClass = "btn btn-block btn-default";
                btnOfficialDetail.CssClass = "btn btn-block btn-default";
                btnBankDetail.CssClass = "btn btn-block btn-default";
                btnFixedAssetsDetail.CssClass = "btn btn-block btn-default";
                btnChildrenDetail.CssClass = "btn btn-block btn-default"; 
                btnOtherDetail.CssClass = "btn btn-block btn-default";

                boxPersonalDetail.Visible = false;
                boxOfficialDetail.Visible = false;
                boxBankDetail.Visible = false;
                boxFixedAssetsDetail.Visible = false;
                boxChildrenDetail.Visible = false;
                boxNomineeDetail.Visible = true;
                boxOtherDetail.Visible = false;
                FillNomineeDetail();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Enter Personal Details.');", true);
            }
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
            GridViewNomineeDetail.DataSource = null;
            GridViewNomineeDetail.DataBind();
            ds = objdb.ByProcedure("SpHRNomineeDetail", new string[] { "flag", "Emp_ID" }, new string[] { "1", ViewState["Emp_IDNew"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridViewNomineeDetail.DataSource = ds;
                GridViewNomineeDetail.DataBind();
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
    }
    protected void ClearNomineeDetail()
    {
        txtNominee_Name.Text = "";
        txtNominee_Relation.Text = "";
    }
    // Other Detail
    protected void btnSaveOtherDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";

            //if (txtOther_Training.Text == "")
            //{
            //    msg += "Entar प्रशिक्षण से संबंधित जानकारी (Training related Information ). \\n";
            //}
            //if (txtOther_Achievement.Text == "")
            //{
            //    msg += "Entar विशेष उपलब्धि आदि की जानकारी (इस क़ालम में निगम में यदि आपको पुरस्कृत किया है अथवा प्रशारित पत्र आदि दिया गया है उसका विवरण दिया जाना है ). \\n";
            //}

            if (msg.Trim() == "" && ViewState["Emp_IDNew"].ToString() != "0")
            {
                ds = objdb.ByProcedure("SpHROtherDetail",
                new string[] { "flag", "Emp_ID", "Other_Training", "Other_Achievement", "Other_UpdatedBy" },
                new string[] { "0", ViewState["Emp_IDNew"].ToString(), txtOther_Training.Text, txtOther_Achievement.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //   divOtherDetail.Visible = false;
                    txtOther_Training.Text = "";
                    txtOther_Achievement.Text = "";
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillOtherDetail();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record not save.');", true);
                }
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
    protected void btnOtherDetail_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (ViewState["Emp_IDNew"].ToString() != "0")
            {

               // Changetabcolour();
                btnOtherDetail.CssClass = "btn btn-flat btn-danger";


                btnPersonalDetail.CssClass = "btn btn-block btn-default";
                btnOfficialDetail.CssClass = "btn btn-block btn-default";
                btnBankDetail.CssClass = "btn btn-block btn-default";
                btnFixedAssetsDetail.CssClass = "btn btn-block btn-default";
                btnChildrenDetail.CssClass = "btn btn-block btn-default";
                btnNomineeDetail.CssClass = "btn btn-block btn-default";
                

                boxPersonalDetail.Visible = false;
                boxOfficialDetail.Visible = false;
                boxBankDetail.Visible = false;
                boxFixedAssetsDetail.Visible = false;
                boxChildrenDetail.Visible = false;
                boxNomineeDetail.Visible = false;
                boxOtherDetail.Visible = true;
                FillOtherDetail();

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Enter Personal Details.');", true);
            }
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
            GridViewOtherDetail.DataSource = null;
            GridViewOtherDetail.DataBind();
            ds = objdb.ByProcedure("SpHROtherDetail", new string[] { "flag", "Emp_ID" }, new string[] { "1", ViewState["Emp_IDNew"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridViewOtherDetail.DataSource = ds;
                GridViewOtherDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClearOtherDetail_Click(object sender, EventArgs e)
    {
        ClearOtherDetail();
    }
    protected void ClearOtherDetail()
    {
        txtOther_Training.Text = "";
        txtOther_Achievement.Text = "";
    }
    // Page Load
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
    protected void FillDropdown()
    {
        try
        {
            // State
            ds = objdb.ByProcedure("SpAdminState", new string[] { "flag" }, new string[] { "2" }, "dataset");
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
            else
            {
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
                ddlDesignation_ID.Items.Clear();
                ddlDesignation_ID.Items.Insert(0, new ListItem("Select", "0"));

                //ddlDesignation_ID.Items.Clear();
                //ddlDesignation_ID.Items.Insert(0, new ListItem("Select", "0"));
                //DataSet dsdesignation = new DataSet();
                //dsdesignation = objdb.ByProcedure("SpAdminDesignation", new string[] { "flag", "Class" }, new string[] { "6", ddlEmp_Class.SelectedValue.ToString() }, "dataset");
                //if (dsdesignation.Tables.Count > 0 && dsdesignation.Tables[0].Rows.Count > 0)
                //{
                //    ddlDesignation_ID.DataSource = dsdesignation;
                //    ddlDesignation_ID.DataTextField = "Designation_Name";
                //    ddlDesignation_ID.DataValueField = "Designation_ID";
                //    ddlDesignation_ID.DataBind();
                //    ddlDesignation_ID.Items.Insert(0, new ListItem("Select", "0"));
                //}
                ////ddlEmp_Class.Focus();

            }
            else
            {
                ddlEmp_Class.Items.Insert(0, new ListItem("Select", "0"));
                ddlDesignation_ID.Items.Insert(0, new ListItem("Select", "0"));
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

            ddlGradPay_ID.Items.Insert(0, new ListItem("Select", "0"));
            ddlOffice_ID.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmp_CurCity.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmp_PerCity.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Visiblebtn()
    {
        boxPersonalDetail.Visible = true;
        boxOfficialDetail.Visible = false;
        boxBankDetail.Visible = false;
        boxFixedAssetsDetail.Visible = false;
        boxChildrenDetail.Visible = false;
        boxNomineeDetail.Visible = false;
        boxOtherDetail.Visible = false;
        btnFinalSubmit.Visible = false;
    }
    protected void btnFinalSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //Changetabcolour();
            btnPersonalDetail.CssClass = "btn btn-block btn-danger";
            btnOfficialDetail.CssClass = "btn btn-block btn-default";
            btnBankDetail.CssClass = "btn btn-block btn-default";
            btnFixedAssetsDetail.CssClass = "btn btn-block btn-default";
            btnChildrenDetail.CssClass = "btn btn-block btn-default";
            btnNomineeDetail.CssClass = "btn btn-block btn-default";
            btnOtherDetail.CssClass = "btn btn-block btn-default";

            objdb.ByProcedure("SpHREmployee",
                new string[] { "flag", "Emp_ID", "Nominee_UpdatedBy" },
                new string[] { "5", ViewState["Emp_IDNew"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
            ViewState["Emp_IDNew"] = "0";
            Visiblebtn();
            divPersonalDetail.Visible = true;
            divOfficialDetail.Visible = true;
            //divBankDetail.Visible = true;
            //divFixedAssetsDetail.Visible = true;
            //divChildrenDetail.Visible = true;
            //divNomineeDetail.Visible = true;
            //divOtherDetail.Visible = true;

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    //BY Rohit
    protected void rbtEmp_Disability_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            ddlEmp_DisabilityType.ClearSelection();
            if (rbtEmp_Disability.SelectedValue == "Yes")
            {
                ddlEmp_DisabilityType.Enabled = true;
                //ddlEmp_DisabilityType.SelectedValue = "Select";
            }
            else if (rbtEmp_Disability.SelectedValue == "No")
            {

                if (ddlEmp_DisabilityType.Items.FindByValue("NA/लागू नहीं") != null)
                {
                    ddlEmp_DisabilityType.Items.FindByValue("NA/लागू नहीं").Selected = true;
                }
                //ddlEmp_DisabilityType.SelectedValue = "NA/लागू नहीं";
                ddlEmp_DisabilityType.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void Changetabcolour()
    {
        try
        {
            btnPersonalDetail.CssClass = "";
            btnOfficialDetail.CssClass = "";
            btnBankDetail.CssClass = "";
            btnFixedAssetsDetail.CssClass = "";
            btnChildrenDetail.CssClass = "";
            btnNomineeDetail.CssClass = "";
            btnOtherDetail.CssClass = "";

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}