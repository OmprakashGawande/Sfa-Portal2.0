using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;


public partial class mis_Daily_Task_Task_Print : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    IFormatProvider culture = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.AllKeys.Contains("dt"))
            {
                if (Request.QueryString["dt"] != null)
                {
                    string dt = obj.Decrypt(Request.QueryString["dt"]);
                    getData(dt);
                }
                else
                {
                    Response.Redirect("Daily_Task_Status.aspx");
                }
            }
            else
            {
                Response.Redirect("Daily_Task_Status.aspx");
            }
            //if (!Page.IsPostBack)
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: window.print(); ", true);
            //}
        }
        catch (Exception)
        {

            Response.Redirect("../Login.aspx");
        }
    }
    protected void getData(string dat)
    {
        ds = obj.ByProcedure("USP_Daily_Task_NoFill_Emp_ByTaskDate", new string[] { "Task_date" }, new string[] { dat }, "dataset");
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                string Latedate = dat;
                StringBuilder sb = new StringBuilder();
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
                        divPrint.InnerHtml = sb.ToString();




                        // string a = sb.ToString();
                    }
                    else
                    {
                        Response.Redirect("Daily_Task_Status.aspx");
                    }

                }
                else
                {
                    Response.Redirect("Daily_Task_Status.aspx");
                }
            }
        }
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
                if (j ==0)
                {
                    if (sub != dt.Rows[i][0].ToString())
                    {
                        html += "<td rowspan='" + Employeecount + "' style='border: 1px solid black; border-collapse: collapse;text-align:center'>" + Convert.ToString(rowcount) + "</td>";
                        rowcount++;
                        html += "<td rowspan='" + Employeecount + "' style='border: 1px solid black; border-collapse: collapse;text-align:" + textAlign + ";'>" + dt.Rows[i][j].ToString() + "</td>";
                    }
                    continue;
                }
                if (j ==1)
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Daily_Task_Status.aspx");
    }
}