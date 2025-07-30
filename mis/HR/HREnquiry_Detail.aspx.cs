using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_HR_HREnquiry_Detail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Emp_Name"] = Session["Emp_Name"].ToString();
                ViewState["EnquiryID"] = objdb.Decrypt(Request.QueryString["EnquiryID"].ToString()); ;

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
    protected void FillRelpy()
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            ds = null;
            ds = objdb.ByProcedure("SpHREnquiryReply", new string[] { "flag", "EnquiryID" }, new string[] { "1", ViewState["EnquiryID"].ToString() }, "dataset");
            int NoOfRecords = ds.Tables[0].Rows.Count;
            if (ds != null && NoOfRecords > 0)
            {
                lblMsg.Text = "";
                lblDepartmentRecord.Text = "";

                for (int i = 0; i < NoOfRecords; i++)
                {
                    sb.Append(" <div class='direct-chat-msg'>");
                    sb.Append("<div class='direct-chat-info clearfix'>");
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
                        sb.Append(" <a href='Upload_Doc/" + Reply_Document + "' target='blank'>Attachment1</a>");
                    }
                    sb.Append("</div></div></div>");
                    dvChat.InnerHtml = sb.ToString();
                }

            }
            else
            {
                lblDepartmentRecord.Text = "There is No Enquiry Reply available.";
                lblDepartmentRecord.Style.Add("color", "Red");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}