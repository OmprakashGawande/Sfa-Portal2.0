using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    string Attachment1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
			Response.Redirect("mis/Login.aspx");
            ds = objdb.ByProcedure("SpAdminAddScheme",
                    new string[] { "flag" },
                    new string[] { "7" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlSchemeName.DataTextField = "Scheme_Name";
                ddlSchemeName.DataValueField = "SchemeID";
                ddlSchemeName.DataSource = ds;
                ddlSchemeName.DataBind();
                ddlSchemeName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    string msg = "";
        //    if (txtDate.Text == "")
        //    {
        //        msg += " Please Select Date.\\n";
        //    }
        //    if (txtBeneficiary_Name.Text == "")
        //    {
        //        msg += "Please Enter Applicant Name.\\n";
        //    }
        //    if (txtFatherName.Text == "")
        //    {
        //        msg += "Please Enter Father Name.\\n";
        //    }
        //    if (txtCityName.Text == "")
        //    {
        //        msg += "Please Enter City Name.\\n";
        //    }
        //    if (txtPost.Text == "")
        //    {
        //        msg += "Please Enter Post Office Name.\\n";
        //    }
        //    if (txtBlock_Name.Text == "")
        //    {
        //        msg += "Please Enter Block Name.\\n";
        //    }
        //    if (txtTehsil_Name.Text == "")
        //    {
        //        msg += "Please Enter Tehsil Name.\\n";
        //    }
        //    if (txtDistrictName.Text == "")
        //    {
        //        msg += "Please Enter District Name.\\n";
        //    }
        //    if (ddlFinancialYear.SelectedIndex <= 0)
        //    {
        //        msg += "Please Select Financial Year.\\n";
        //    }
        //    if (txtBiogas.Text =="")
        //    {
        //        msg += "Please Enter Bio Gas.\\n";
        //    }
        //    if (txtCapacity.Text == "")
        //    {
        //        msg += "Please Enter Capacity.\\n";
        //    }
        //    if (txtAmount.Text == "")
        //    {
        //        msg += "Please Enter Amount.\\n";
        //    }
        //    if (txtBigAnimal.Text == "")
        //    {
        //        msg += "Please Enter Big Animal.\\n";
        //    }
        //    if (txtSmallAnimal.Text == "")
        //    {
        //        msg += "Please Enter Small Animal.\\n";
        //    }
        //    if (txtNumberOfAnimal.Text == "")
        //    {
        //        msg += "Please Enter Number of Animal.\\n";
        //    }
        //    if (txtGover.Text == "")
        //    {
        //        msg += "Please Enter Gover.\\n";
        //    }
        //    if (txtNumberOfMember.Text == "")
        //    {
        //        msg += "Please Enter Number Of Member.\\n";
        //    }
        //    if (txtOwnership_Lordships.Text == "")
        //    {
        //        msg += "Please Enter Ownership/Lordships .\\n";
        //    }
        //    if (txtBlock_Name.Text == "")
        //    {
        //        msg += "Please Enter Block Name.\\n";
        //    }
        //    if (txtBranchName.Text == "")
        //    {
        //        msg += "Please Enter Branch Name.\\n";
        //    }
        //    if (txtIFSCCode.Text == "")
        //    {
        //        msg += "Please Enter IFSC Code .\\n";
        //    }
        //    if (txtAccountNo.Text == "")
        //    {
        //        msg += "Please Enter Account No .\\n";
        //    }
        //    if (txtAddharNumber.Text == "")
        //    {
        //        msg += "Please Enter Addhar No.\\n";
        //    }
        //    if (msg == "")
        //    {

        //        if (BtnSubmit.Text == "Submit")
        //        {
        //            string Beneficiary_IsActive = "1";
        //            objdb.ByProcedure("SpApplicantDetail",
        //                new string[] { "flag", "SchemeID", "Beneficiary_IsActive","Date" ,"Beneficiary_Name", "App_Email", "App_UserType", "App_Gender", "App_Address", "App_Pincode", "App_Block", "App_District", "App_State", "App_PLStatus" },
        //                    new string[] { "21",ddlSchemeName.SelectedValue.ToString(),Beneficiary_IsActive,txtDate.Text, txtBeneficiary_Name.Text, txtEmail.Text, App_UserType, ddlGender.SelectedValue, txtAddress.Text, txtPincode.Text, ddlApp_Block.SelectedValue.ToString(), ddlApp_District.SelectedValue.ToString(), ddlApp_State.SelectedValue.ToString(), App_PLStatus }, "Dataset");

        //            string Applicant_IsActive = "1";
        //            objdb.ByProcedure("SpGrvApplicationDetail",
        //                    new string[] { "flag", "App_ID", "Applicant_IsActive", "App_GrvStatus", "Application_Subject", "Application_Descritption", "Application_Doc1", "Application_Doc2", "Application_ApplicantType", "Application_GrievanceType" },
        //                    new string[] { "0", ViewState["App_ID"].ToString(), Applicant_IsActive, "Open", txtSubject.Text, txtGrvDescription.Text, Attachment1, Attachment2, ddlApplicantType.SelectedItem.Text, ddlGrvType.SelectedItem.Text }, "Dataset");

        //            ClearText();
        //            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
        //        }
        //    }
        //    else
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        //}
    }
}