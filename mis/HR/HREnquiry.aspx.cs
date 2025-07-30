using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HREnquiry : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    string Attachment1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                lblMsg.Text = "";
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID", "Emp_ID" }, new string[] { "20", ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlEmployee.DataSource = ds;
                    ddlEmployee.DataTextField = "Emp_Name";
                    ddlEmployee.DataValueField = "Emp_ID";
                    ddlEmployee.DataBind();
                    ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
                }
                ddlEmployee.SelectedValue = ViewState["Emp_ID"].ToString();
                FillGrid();
                FillEnquiryOfficer();
                FillPresentingofficer();
                FillGridAddOfficer();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlEmployee.SelectedIndex <= 0)
            {
                msg += "Select Employee Name. \\n";
            }

            if (txtOrderNo.Text == "")
            {
                msg += "Enter Order No. \\n";
            }
            if (txtOrderDate.Text == "")
            {
                msg += "Select Order Date. \\n";
            }
            if (txtTitle.Text == "")
            {
                msg += "Enter Title. \\n";
            }

            if (txtDocument.HasFile)
            {
                Attachment1 = Guid.NewGuid() + "-" + txtDocument.FileName;
                txtDocument.PostedFile.SaveAs(Server.MapPath("~/mis/HR/UploadDoc/" + Attachment1));
            }
            else
            {
                Attachment1 = "";
            }
            if (txtDescription.Text == "")
            {
                msg += "Enter Description. \\n";
            }
            if (msg.Trim() == "")
            {
                String time = "";

                if (txtOrderDate.Text != "")
                {
                    time = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd");
                }
                else
                {
                    time = "";
                }

                if (btnSave.Text == "Save")
                {
                    objdb.ByProcedure("SpHREnquiry",
                    new string[] { "flag", "Emp_ID", "Enquiry_OrderNo", "Enquiry_OrderDate", "Enquiry_Title", "Enquiry_Document", "Enquiry_Description", "Enquiry_Status", "Enquiry_UpdatedBy" },
                    new string[] { "0", ddlEmployee.SelectedValue.ToString(),txtOrderNo.Text, time, txtTitle.Text, Attachment1, txtDescription.Text,"Open", ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();

                }
                else if (btnSave.Text == "Edit")
                {

                    objdb.ByProcedure("SpHREnquiry",
                     new string[] { "flag", "EnquiryID", "Emp_ID", "Enquiry_OrderNo", "Enquiry_OrderDate", "Enquiry_Title", "Enquiry_Document", "Enquiry_Description", "Enquiry_UpdatedBy" },
                     new string[] { "3", ViewState["EnquiryID"].ToString(), ddlEmployee.SelectedValue.ToString(),txtOrderNo.Text, time, txtTitle.Text, Attachment1, txtDescription.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    btnSave.Text = "Save";
                    ClearText();
                    FillGrid();
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
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            ds = objdb.ByProcedure("SpHREnquiry", new string[] { "flag", "Emp_ID" }, new string[] { "1", ViewState["Emp_ID"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void ClearText()
    {
        ddlEmployee.ClearSelection();
        ddlInquiryofficer.ClearSelection();
        ddlPresentingofficer.ClearSelection();
        txtOrderNo.Text = "";
        txtOrderDate.Text = "";
        txtTitle.Text = "";
        txtDocument.Dispose();
        txtDescription.Text = "";
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["EnquiryID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpHREnquiry", 
                new string[] { "flag", "EnquiryID" }, 
                new string[] { "2", ViewState["EnquiryID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.ClearSelection();
                ddlEmployee.Items.FindByValue(ds.Tables[0].Rows[0]["Emp_ID"].ToString()).Selected = true;
                txtOrderNo.Text = ds.Tables[0].Rows[0]["Enquiry_OrderNo"].ToString();
                txtOrderDate.Text = ds.Tables[0].Rows[0]["Enquiry_OrderDate"].ToString();
                txtTitle.Text = ds.Tables[0].Rows[0]["Enquiry_Title"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["Enquiry_Description"].ToString();
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
           
        }
    }


    //--Addofficer
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkBankDetail = (LinkButton)GridView1.Rows[selRowIndex].FindControl("LinkButton1");
            ViewState["EnquiryID"] = lnkBankDetail.ToolTip.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert1()", true);

        }
        catch (Exception ex)
        {
            lblMsg1.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEnquiryOfficer()
    {
        try
        {
            ds = null;
            ClearText();
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID", "Emp_ID" }, new string[] { "30", ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlInquiryofficer.DataSource = ds;
                ddlInquiryofficer.DataTextField = "Emp_Name";
                ddlInquiryofficer.DataValueField = "Emp_ID";
                ddlInquiryofficer.DataBind();
                ddlInquiryofficer.Items.Insert(0, new ListItem("Select", "0"));
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillPresentingofficer()
    {
        try
        {
            ds = null;
            ClearText();
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID", "Emp_ID" }, new string[] { "30", ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPresentingofficer.DataSource = ds;
                ddlPresentingofficer.DataTextField = "Emp_Name";
                ddlPresentingofficer.DataValueField = "Emp_ID";
                ddlPresentingofficer.DataBind();
                ddlPresentingofficer.Items.Insert(0, new ListItem("Select", "0"));
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg1.Text = "";
            string msg = "";
            if (ddlInquiryofficer.SelectedIndex <= 0)
            {
                msg += "Select Employee Name. \\n";
            }
            if (ddlPresentingofficer.SelectedIndex <= 0)
            {
                msg += "Select Employee Name. \\n";
            }
            if (msg.Trim() == "")
            {

                if (btnAdd.Text == "Add")
                {
                    objdb.ByProcedure("SpHRAddEnquiryOfficer",
                    new string[] { "flag", "EnquiryID", "Emp_ID", "AddInquiryofficer", "AddPresentingOfficer", "AddEnquiry_UpdatedBy" },
                    new string[] { "0", ViewState["EnquiryID"].ToString(), ViewState["Emp_ID"].ToString(), ddlInquiryofficer.SelectedValue.ToString(), ddlPresentingofficer.SelectedValue.ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg1.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGridAddOfficer();

                }
                //else if (btnSave.Text == "Edit")
                //{

                //    objdb.ByProcedure("SpHREnquiry",
                //     new string[] { "flag", "EnquiryID", "Emp_ID", "Enquiry_OrderNo", "Enquiry_OrderDate", "Enquiry_Title", "Enquiry_Document", "Enquiry_Description", "Enquiry_UpdatedBy" },
                //     new string[] { "3", ViewState["EnquiryID"].ToString(), ddlEmployee.SelectedValue.ToString(), txtOrderNo.Text, time, txtTitle.Text, Attachment1, txtDescription.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                //    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                //    btnSave.Text = "Save";
                //    ClearText();
                //    FillGrid();
                //}
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
   
    protected void FillGridAddOfficer()
    {
        try
        {
            GridView2.DataSource = null;
            GridView2.DataBind();

            ds = objdb.ByProcedure("SpHRAddEnquiryOfficer", new string[] { "flag" }, new string[] { "1"}, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg1.Text = "";
            ViewState["AddOfficerID"] = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpHRAddEnquiryOfficer",
                   new string[] { "flag", "AddOfficerID" }, new string[] { "2", ViewState["AddOfficerID"].ToString() }, "dataset");

            lblMsg1.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGridAddOfficer();
            ClearText();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}