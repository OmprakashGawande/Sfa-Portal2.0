using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace sfaDailyTask
{
  public class CommonMethod
    {
      public static void sendmail(string TO, string CC,string subject ,string content)
      {
          try
          {
              //string Latedate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
              //  string AttachedEmailHTMLPath = Server.MapPath("~/HtmlTemplete/OIC_Email_Templete.html");
              //ServicePointManager.Expect100Continue = true;
              //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
              SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
              using (MailMessage mm = new MailMessage(smtpSection.From, TO))
              {
                  mm.Subject = subject;
                  mm.Body = content;
                  mm.IsBodyHtml = true;
                  mm.CC.Add(CC);
                  SmtpClient smtp = new SmtpClient();
                  smtp.Host = smtpSection.Network.Host;
                  smtp.EnableSsl = smtpSection.Network.EnableSsl;
                  NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                  System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                  smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                  smtp.Credentials = networkCred;
                  smtp.Port = smtpSection.Network.Port;
                  smtp.Send(mm);
                  //HttpContext.Current.Response.End();
              }
          }
          catch (Exception ex)
          {

          }
      }
      public static void WriteToFile(string Message)
      {
          string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
          if (!Directory.Exists(path))
          {
              Directory.CreateDirectory(path);
          }
          string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\DailyReportLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
          if (!File.Exists(filepath))
          {
              // Create a file to write to.   
              using (StreamWriter sw = File.CreateText(filepath))
              {
                  sw.WriteLine(Message);
              }
          }
          else
          {
              using (StreamWriter sw = File.AppendText(filepath))
              {
                  sw.WriteLine(Message);
              }
          }
          try
          {
              byProcedure("USP_Daily_Task_NoFill_Emp_ByTaskDate_Log", new string[] { "LogMassage" }, new string[] { Message });
          }
          catch (Exception ex)
          {

          }

      }
      public static string ConvertDataTableToHTML(DataTable dt, string colName, string textAlign)
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
      public static string ConvertDataTableToHTMLTaskData(DataTable dt, string textAlign)
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

      public static DataSet byProcedure(string ProcedureName, string[] Param,string[] ParmValue)
      {
          DataSet ds = new DataSet();
          using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
          {
              using (SqlCommand cmd = new SqlCommand(ProcedureName, con))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  for (int i = 0; i < Param.Length; i++)
                  {
                      cmd.Parameters.AddWithValue("@" + Param[i], ParmValue[i]);
                  }
                  using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                  {
                      sda.Fill(ds);
                  }
              }
          }
          return ds;
      }


        public static string TaskData(DataSet ds,string Latedate)
        {
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
                        sb.Append(CommonMethod.ConvertDataTableToHTMLTaskData(ds.Tables[1], "left"));
                    }
                    else
                    {
                        CommonMethod.WriteToFile("Daily Reporting List Record Not Found  Tables[1] :" + DateTime.Now);
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        sb.Append("<hr/>");
                        sb.Append("<div style='text-align:center;color:black;'>");
                        sb.Append("<h3 style=' margin: 1px; padding:0'>Project Wise Report</h3>");
                        sb.Append("</div>");
                        sb.Append(CommonMethod.ConvertDataTableToHTML(ds.Tables[2], "Project_Name", "center"));
                    }
                    else
                    {
                        CommonMethod.WriteToFile("Project Wise Report Record Not Found  Tables[2] :" + DateTime.Now);
                    }
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        sb.Append("<hr/>");
                        sb.Append("<div style='text-align:center;color:black;'>");
                        sb.Append("<h3 style=' margin: 1px; padding:0'>Project Wise Work Category Report</h3>");
                        sb.Append("</div>");
                        sb.Append(CommonMethod.ConvertDataTableToHTML(ds.Tables[4], "Project_Name", "center"));
                    }
                    else
                    {
                        CommonMethod.WriteToFile("Project Wise Work Category Report Record Not Found  Tables[4] :" + DateTime.Now);
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
                        CommonMethod.WriteToFile("Employee Task Not Filled, Leave and Tour Report Record Not Found  Tables[0] :" + DateTime.Now);
                    }
                    //string empEmail = "";
                    //if (ds.Tables[3].Rows.Count > 0)
                    //{
                    //    int count = ds.Tables[3].Rows.Count;
                    //    for (int i = 0; i < count; i++)
                    //    {
                    //        if (ds.Tables[3].Rows[i]["Emp_Email"].ToString() != "")
                    //        {
                    //            empEmail += ds.Tables[3].Rows[i]["Emp_Email"].ToString() + ",";
                    //        }
                    //    }

                    //}
                    //if (empEmail != "")
                    //{
                    //    CommonMethod.sendmail(Mail_TO, Mail_CC,Latedate +" "+ Mail_SUB, sb.ToString());
                    //    CommonMethod.WriteToFile("Email has been successfully sent "+ Mail_SUB + " :" + DateTime.Now);
                    //}
                    //else
                    //{
                    //    CommonMethod.WriteToFile("Employee Email Not Found  Tables[3] :" + DateTime.Now);
                    //}

                    // string a = sb.ToString();
                }
                else
                {
                    CommonMethod.WriteToFile("No Record Found :" + DateTime.Now);
                }

            }
            else
            {
                CommonMethod.WriteToFile("No Record Found :" + DateTime.Now);
            }
            return sb.ToString();
        }

    }
}
