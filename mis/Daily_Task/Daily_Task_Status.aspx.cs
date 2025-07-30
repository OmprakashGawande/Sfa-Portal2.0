using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Drawing;
using System.IO;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Text;



public partial class mis_Daily_Task_Daily_Task_Status : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds, ds1, ds5, dsC, dsCU, dsphad;
    static DataTable dt = new DataTable();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    IFormatProvider culture = new CultureInfo("en-US", true);
    string[] arr = { "1", "2", "3", "4", "5", "59" };
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;

        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!arr.Contains(Session["Emp_ID"].ToString()))
            {
                objdb.redirectToHome();
            }

            if (!IsPostBack)
            {
                gridvew1.DataSource = null;
                gridvew1.DataBind();
                lblMsg.Text = "";
                txtDate.Attributes.Add("readonly", "readonly");
                DateTime dd = DateTime.Now;
                txtDate.Text = (Convert.ToDateTime(dd, culture).ToString("dd/MM/yyyy"));
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            fillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", ex.Message.ToString());
        }
    }
    protected void fillGrid()
    {
        try
        {
            gridvew1.DataSource = null;
            gridvew1.DataBind();
            string ddate = Convert.ToDateTime(txtDate.Text.Trim(), cult).ToString("yyyy/MM/dd");
            ds1 = objdb.ByProcedure("USP_Daily_Task_RPT_Emp_Details", new string[] { "Task_date" }, new string[] { ddate }, "dataset");
            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        gridvew1.DataSource = ds1.Tables[0];
                        gridvew1.DataBind();
                    }

                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", ex.Message.ToString());
        }

    }
    protected void fillTaskGrid(string TaskId)
    {
        try
        {
            gridEmpTask.DataSource = null;
            gridEmpTask.DataBind();
            ds1 = objdb.ByProcedure("USP_Daliy_Task_Child_GetBy_Task_Id", new string[] { "Task_Id" }, new string[] { TaskId }, "dataset");
            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        gridEmpTask.DataSource = ds1.Tables[0];
                        gridEmpTask.DataBind();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#ViewDetails').modal('show');", true);
                    }

                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", ex.Message.ToString());
        }

    }
    protected void gridvew1_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            if (e.CommandName == "ViewDetails")
            {
                fillTaskGrid(e.CommandArgument.ToString());
               
            }
            else if (e.CommandName == "DeleteTask")
            {
                ds1 = objdb.ByProcedure("USP_Daily_Task_Delete", new string[] { "Task_Id", "UpdateBy", "UpdatedByIp" }, new string[] { e.CommandArgument.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
                if (ds1 != null)
                {
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You !", ds1.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                fillGrid();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-info", "Alert !", ds1.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }

                        }

                    }

                }

            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 6 : ", ex.Message.ToString());
        }
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDate.Text != "")
        {
            string ddate = Convert.ToDateTime(txtDate.Text.Trim(), cult).ToString("yyyy/MM/dd");
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            string url = baseUrl + "/mis/Daily_Task/Task_Print.aspx?dt=" + objdb.Encrypt(ddate);
            string script = "<script>window.open('" + url + "', '_blank');</script>";
            Response.Write(script);

        }
        }
        catch (Exception)
        {
            
        }
    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        // Define the recipient email, subject, and body
        string ddate = Convert.ToDateTime(txtDate.Text.Trim(), cult).ToString("yyyy/MM/dd");
        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        string url = baseUrl + "/mis/Daily_Task/Task_Print.aspx?dt=" + objdb.Encrypt(ddate);

        string recipient = "recipient@example.com";
        string subject = "Test Email";

        // Define the HTML body
        string body = "<html><body>" +
                      "<div>" +
                      "<a href='" + url + "'>View Employee Status List</a>" +
                      "</div>" +
                      "</body></html>";

        // Encode the subject and body to ensure special characters are handled correctly
        string encodedSubject = Server.UrlEncode(subject);
        string encodedBody = Server.UrlEncode(body);

        // Construct the Gmail compose URL (passing HTML body as URL-encoded string)
        string gmailComposeUrl = "https://mail.google.com/mail/?view=cm&fs=1&to=" + recipient + "&su=" + encodedSubject + "&body=" + encodedBody;

        // Open the Gmail compose window in a new tab
        string script = "<script>window.open('" + gmailComposeUrl + "', '_blank');</script>";
        Response.Write(script);
    }


    //protected void btnSendEmail_Click(object sender, EventArgs e)
    //{
    //    // Define the recipient email, subject, and body
    //    string ddate = Convert.ToDateTime(txtDate.Text.Trim(), cult).ToString("yyyy/MM/dd");
    //    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
    //    string url = baseUrl + "/mis/Daily_Task/Task_Print.aspx?dt=" + objdb.Encrypt(ddate);

    //    string recipient = "recipient@example.com";
    //    string subject = "Test Email";
    //    string body = "<div>"
    //                 +"<a href='" + url + "' value='View Employee Status List' />"
    //                 +" </div>";
    //    // Encode the subject and body to ensure special characters are handled correctly
    //    string encodedSubject = Server.UrlEncode(subject);
    //    string encodedBody = Server.UrlEncode(body);

    //    // Construct the Gmail compose URL
    //    string gmailComposeUrl = "https://mail.google.com/mail/?view=cm&fs=1&to=" + recipient + "&su=" + encodedSubject + "&body=" + encodedBody;

    //    string script = "<script>window.open('" + gmailComposeUrl + "', '_blank');</script>";
    //    // Redirect the user to Gmail's compose page
    //    Response.Write(script);

    //}
    //protected void btnSendEmail_Click(object sender, EventArgs e)
    //{
    //    // Define the recipient email, subject, and body
    //    string ddate = Convert.ToDateTime(txtDate.Text.Trim(), cult).ToString("yyyy/MM/dd");
    //    string recipient = "recipient@example.com";
    //    string subject = "Test Email";
    //    string body = getData(ddate); // Assuming getData() is your function to generate the body content

    //    // Encode the subject and body to ensure special characters are handled correctly
    //    string encodedSubject = Server.UrlEncode(subject);
    //    string encodedBody = Server.UrlEncode(body);

    //    // Construct the Gmail compose URL (ensure no URL is too long)
    //    string gmailComposeUrl = "https://mail.google.com/mail/?view=cm&fs=1&to=" + recipient + "&su=" + encodedSubject + "&body=" + encodedBody;

    //    // Check if the URL exceeds the browser limit for URL length (usually around 2000-8000 characters)
    //    if (gmailComposeUrl.Length > 2000)
    //    {
    //        // If it's too large, show a message or handle it differently
    //        Response.Write("<script>alert('The email content is too large to open in Gmail.');</script>");
    //    }
    //    else
    //    {
    //        // Open Gmail compose in a new tab
    //        string script = "<script>window.open('" + gmailComposeUrl + "', '_blank');</script>";
    //        Response.Write(script);
    //    }
    //}

    #region GetMailData
    protected string getData(string dat)
    {
        StringBuilder sb = new StringBuilder();
        ds = objdb.ByProcedure("USP_Daily_Task_NoFill_Emp_ByTaskDate", new string[] { "Task_date" }, new string[] { dat }, "dataset");
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                string Latedate = dat;
               
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        sb.Append("<div style='text-align:center;color:black;'>");
                        sb.Append("<h2 style=' margin: 0; padding:0'>SFA Technologies Pvt. Ltd. </h2>");
                        sb.Append("<h4 style=' margin: 0; padding:0'>Date : " + Latedate + "</h4>");
                        sb.Append("</div>");
                        sb.Append("<div>");
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            sb.Append("<hr/>");
                            sb.Append("<div style='text-align:center;color:black;'>");
                            sb.Append("<h3 style=' margin: 1px; padding:0'>Daily Reporting List</h3>");
                            sb.Append("</div>");
                            sb.Append(ConvertDataTableToHTMLTaskData(ds.Tables[1], "left"));
                        }
                        else
                        {

                        }
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            sb.Append("<hr/>");
                            sb.Append("<div style='text-align:center;color:black;'>");
                            sb.Append("<h3 style=' margin: 1px; padding:0'>Project Wise Report</h3>");
                            sb.Append("</div>");
                            sb.Append(ConvertDataTableToHTML(ds.Tables[2], "Project_Name", "center"));
                        }
                        else
                        {

                        }
                        if (ds.Tables[4].Rows.Count > 0)
                        {
                            sb.Append("<hr/>");
                            sb.Append("<div style='text-align:center;color:black;'>");
                            sb.Append("<h3 style=' margin: 1px; padding:0'>Project Wise Work Category Report</h3>");
                            sb.Append("</div>");
                            sb.Append(ConvertDataTableToHTML(ds.Tables[4], "Project_Name", "center"));
                        }
                        else
                        {

                        }
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            sb.Append("<hr/>");
                            sb.Append("<div style='text-align:center;color:black;'>");
                            sb.Append("<h3 style=' margin: 1px; padding:0'>Employee Task Not Filled, Leave and Tour Report</h3>");
                            sb.Append("</div>");
                            sb.Append("<table style='border: 1px solid black; border-collapse: collapse;width:90%;margin:1% 5%;'>");
                            sb.Append("<tr>");
                            sb.Append("<th style='border: 1px solid black; border-collapse: collapse;width:10%;color:white;background-color:#6b1216;'>S.No.</th>");
                            sb.Append("<th style='border: 1px solid black; border-collapse: collapse; width:70%;color:white;background-color:#6b1216;'>Employee Name</th>");

                            sb.Append("</tr>");

                            int Scount = 1;
                            string[] arr = { "0", "2", "4" };
                            int count = ds.Tables[0].Rows.Count;
                            for (int i = 0; i < count; i++)
                            {
                                //if (ds.Tables[0].Rows[i]["Emp_Email"].ToString() != "")
                                //{
                                //    empEmail += ds.Tables[0].Rows[i]["Emp_Email"].ToString() + ",";
                                //}
                                sb.Append("<tr>");

                                if (arr.Contains(ds.Tables[0].Rows[i]["order_No"].ToString()))
                                {
                                    sb.Append("<td colspan='2' style='border: 1px solid black; border-collapse: collapse;text-align:center'>" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + "</td>");
                                    Scount = 1;
                                }
                                else
                                {
                                    sb.Append("<td style='border: 1px solid black; border-collapse: collapse;text-align:center'>" + Scount + "</td>");
                                    sb.Append("<td style='border: 1px solid black; border-collapse: collapse;text-align:center'>" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + "</td>");
                                    Scount++;
                                }
                                sb.Append("</tr>");
                            }
                            sb.Append("</table>");
                            sb.Append("</div>");


                        }
                        else
                        {

                        }
                        // string a = sb.ToString();
                    }
                }
            }
        }
        return sb.ToString();
    }
    public string ConvertDataTableToHTML(DataTable dt, string colName, string textAlign)
    {
        string html = "<table  style='border: 1px solid black; border-collapse: collapse;width:90%;margin:1% 5%;'>";
        //add header row
        html += "<tr>";
        html += "<th style='border: 1px solid black; border-collapse: collapse;width:10%;color:white;background-color:#6b1216;'>S.No.</th>";
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            html += "<th style='border: 1px solid black; border-collapse: collapse;color:white;background-color:#6b1216;'>" + dt.Columns[i].ColumnName.Replace("_", " ") + "</th>";
        }
        html += "</tr>";
        //add rows
        string sub = "";
        int rowcount = 1;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            html += "<tr>";

            int count = dt.Select(colName + " ='" + dt.Rows[i][0].ToString() + "'").Count();
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                if (j < 1)
                {
                    if (sub != dt.Rows[i][0].ToString())
                    {
                        html += "<td rowspan='" + count + "' style='border: 1px solid black; border-collapse: collapse;text-align:center'>" + Convert.ToString(rowcount) + "</td>";
                        rowcount++;
                        html += "<td rowspan='" + count + "' style='border: 1px solid black; border-collapse: collapse;text-align:" + textAlign + ";'>" + dt.Rows[i][j].ToString() + "</td>";
                    }
                    continue;
                }
                html += "<td style='border: 1px solid black; border-collapse: collapse;text-align:" + textAlign + ";'>" + dt.Rows[i][j].ToString() + "</td>";
            }
            sub = dt.Rows[i][0].ToString();
            html += "</tr>";
        }
        html += "</table>";
        return html;
    }
    public string ConvertDataTableToHTMLTaskData(DataTable dt, string textAlign)
    {
        string html = "<table  style='border: 1px solid black; border-collapse: collapse;width:90%;margin:1% 5%;'>";
        //add header row
        html += "<tr>";
        html += "<th style='border: 1px solid black; border-collapse: collapse;width:10%;color:white;background-color:#6b1216;'>S.No.</th>";
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            html += "<th style='border: 1px solid black; border-collapse: collapse;color:white;background-color:#6b1216;'>" + dt.Columns[i].ColumnName.Replace("_", " ") + "</th>";
        }
        html += "</tr>";
        //add rows
        string sub = "";
        string sub2 = "";
        int rowcount = 1;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            html += "<tr>";

            int Employeecount = dt.Select("Employee_Name ='" + dt.Rows[i][0].ToString() + "'").Count();
            int Projectcount = dt.Select("Project_Name = '" + dt.Rows[i]["Project_Name"].ToString() + "' AND Employee_Name = '" + dt.Rows[i]["Employee_Name"].ToString() + "'").Count();
            for (int j = 0; j < dt.Columns.Count; j++)
            {

                if (sub != dt.Rows[i][0].ToString())
                {
                    sub2 = "";
                }
                if (j == 0)
                {
                    if (sub != dt.Rows[i][0].ToString())
                    {
                        html += "<td rowspan='" + Employeecount + "' style='border: 1px solid black; border-collapse: collapse;text-align:center'>" + Convert.ToString(rowcount) + "</td>";
                        rowcount++;
                        html += "<td rowspan='" + Employeecount + "' style='border: 1px solid black; border-collapse: collapse;text-align:" + textAlign + ";'>" + dt.Rows[i][j].ToString() + "</td>";
                    }
                    continue;
                }
                if (j == 1)
                {
                    if (sub2 != dt.Rows[i][1].ToString())
                    {
                        html += "<td rowspan='" + Projectcount + "' style='border: 1px solid black; border-collapse: collapse;text-align:" + textAlign + ";'>" + dt.Rows[i][j].ToString() + "</td>";
                    }
                    continue;
                }
                html += "<td style='border: 1px solid black; border-collapse: collapse;text-align:" + textAlign + ";'>" + dt.Rows[i][j].ToString() + "</td>";
            }
            sub = dt.Rows[i][0].ToString();
            sub2 = dt.Rows[i][1].ToString();
            html += "</tr>";
        }
        html += "</table>";
        return html;
    }

    
    #endregion
}