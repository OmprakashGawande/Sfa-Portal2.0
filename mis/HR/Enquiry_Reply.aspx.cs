using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_HR_Enquiry_Reply : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    string Attachment1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null && Session["Emp_Name"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Emp_Name"] = Session["Emp_Name"];
                //ViewState["Designation_ID"] = Session["Designation_ID"];
                ViewState["EnquiryID"] = objdb.Decrypt(Request.QueryString["EnquiryID"].ToString()); ;
                DivReply.Visible = true;
                FillGrid();
                FillRelpy();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillGrid()
    {
        try
        {
            DetailsView1.DataSource = null;
            DetailsView1.DataBind();

            ds = objdb.ByProcedure("SpHREnquiry", new string[] { "flag", "EnquiryID" }, new string[] { "4", ViewState["EnquiryID"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                DetailsView1.DataSource = ds;
                DetailsView1.DataBind();
                Enquiry_Description.InnerHtml = ds.Tables[0].Rows[0]["Enquiry_Description"].ToString();
                string Enquiry_Document = ds.Tables[0].Rows[0]["Enquiry_Document"].ToString();

                if (Enquiry_Document != "")
                {
                    hyprEnquiry_Document.Visible = true;
                    hyprEnquiry_Document.NavigateUrl = "~/mis/HR/EnquiryDoc/" + Enquiry_Document;
                }
                else
                {
                    hyprEnquiry_Document.Visible = false;
                    hyprEnquiry_Document.NavigateUrl = "";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void BtnReply_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlStatus.SelectedIndex <= 0)
            {
                msg = "Please Select Status Type.\\n";
            }
            if (FUReply_SuppDoc.HasFile)
            {
                Attachment1 = Guid.NewGuid() + "-" + FUReply_SuppDoc.FileName;
                FUReply_SuppDoc.PostedFile.SaveAs(Server.MapPath("~/mis/HR/EnquiryDoc/" + Attachment1));
            }
            else
            {
                Attachment1 = "";
            }
            if (txtRemark.Text.Trim() == "")
            {
                msg += "Enter Remark. \\n";
            }
            if (msg.Trim() == "")
            {

                if (BtnReply.Text == "Reply")
                {
                    if (ddlStatus.Text == "Open")
                    {
                        DivReply.Visible = true;
                        objdb.ByProcedure("SpHREnquiryReply",
                            new string[] { "flag", "EnquiryID", "Emp_ID", "Reply_Status", "Emp_Name", "Reply_Document", "Reply_Remark", "Reply_UpdatedBy" },
                            new string[] { "0", ViewState["EnquiryID"].ToString(), ViewState["Emp_ID"].ToString(), ddlStatus.SelectedValue.ToString(), ViewState["Emp_Name"].ToString(), Attachment1, txtRemark.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('The Admin has been successfully Reply to Employee.');", true);


                    }
                    if (ddlStatus.Text == "Close")
                    {
                        DivReply.Visible = true;
                        objdb.ByProcedure("SpHREnquiryReply",
                            new string[] { "flag", "EnquiryID", "Emp_ID", "Reply_Status", "Emp_Name", "Reply_Document", "Reply_Remark", "Reply_UpdatedBy" },
                            new string[] { "0", ViewState["EnquiryID"].ToString(), ViewState["Emp_ID"].ToString(), ddlStatus.SelectedValue.ToString(), ViewState["Emp_Name"].ToString(), Attachment1, txtRemark.Text, ViewState["Emp_ID"].ToString() }, "dataset");


                        objdb.ByProcedure("SpHREnquiry", new string[] { "flag", "EnquiryID", "Enquiry_Status" }, new string[] { "5", ViewState["EnquiryID"].ToString(), ddlStatus.SelectedItem.Text }, "datset");
                    }

                    ddlStatus.ClearSelection();
                    FUReply_SuppDoc.Dispose();
                    txtRemark.Text = "";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('The Admin has been successfully Reply to Employee.');", true);

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
    protected void FillRelpy()
    {
        try
        {
            ds = null;
            DivReply.Visible = true;
            ds = objdb.ByProcedure("SpHREnquiryReply", new string[] { "flag", "EnquiryID" }, new string[] { "1", ViewState["EnquiryID"].ToString() }, "dataset");
            int NoOfRecords = ds.Tables[0].Rows.Count;
            if (ds != null && NoOfRecords > 0)
            {
                lblMsg.Text = "";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < NoOfRecords; i++)
                {
                    sb.Append(" <div class='direct-chat-msg'>");
                    sb.Append("<div class='direct-chat-info clearfix'>");
                    //sb.Append("<span class='direct-chat-name pull-left'>" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + " [" + ds.Tables[0].Rows[i]["Designation_Name"].ToString() + "]</span><br/>");
                    sb.Append("<span class='direct-chat-name pull-left'>" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + "</span><br/>");

                    sb.Append("<span class='direct-chat-timestamp pull-right'>" + ds.Tables[0].Rows[i]["Reply_UpdatedOn"].ToString() + "</span>");     //16 Aug 2:00 pm
                    sb.Append("</div>");
                    //.direct-chat-info -->
                    sb.Append("<img class='direct-chat-img' src='../image/User1.png' alt='message user image'/>");
                    //.direct-chat-img -->
                    sb.Append("<div class='direct-chat-text form-group' style='word-wrap:break-word; min-height:80px;'>" + ds.Tables[0].Rows[i]["Reply_Remark"].ToString());
                    //sb.Append("RTI Act has been made by legislation of Parliament of India on 15 June 2005.");

                    sb.Append("<div class='attachment text-right''><br />");
                    string Reply_Document = ds.Tables[0].Rows[i]["Reply_Document"].ToString();
                    if (Reply_Document != null && Reply_Document != "")
                    {
                        sb.Append(" <a href='EnquiryDoc/" + Reply_Document + "' target='blank'>Attachment1</a>");
                    }
                    sb.Append("</div></div></div>");
                    dvChat.InnerHtml = sb.ToString();
                }
                if (ds.Tables[0].Rows[0]["Reply_Status"].ToString() == "Open")
                {
                    DivReply.Visible = true;
                }
                else
                {
                    DivReply.Visible = false;
                }

            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
}