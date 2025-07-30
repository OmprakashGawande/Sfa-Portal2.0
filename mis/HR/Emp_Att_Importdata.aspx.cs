using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;

public partial class mis_HR_Emp_Att_Importdata : System.Web.UI.Page
{
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
	    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            txtAttDate.Text = @DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void btnImport_Click(object sender, System.EventArgs e)
    {
        try
        {
            string txt = string.Empty;


            lblMsg.Text = "";
            //GridView1.DataSource = null;
            //GridView1.DataBind();


           // File.WriteAllText(@"C:\Users\Public\TextLogfile.txt", "Start \r\n");
            
           
            if (ViewState["UPageTokan"] != null && Session["PageTokan"] != null)
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    string msg = "";
                    if (txtAttDate.Text == "")
                    {
                        msg += "Enter Attendance Date\\n";
                    }
                    if (FileUpload1.HasFile)
                    {
                        int count = FileUpload1.FileName.Split('.').Length;
                        if (count != 2)
                        {
                            msg += "Invalid File Name Format.";
                        }
                        else
                        {
                            string ext = System.IO.Path.GetExtension(this.FileUpload1.PostedFile.FileName);
                            if (ext == ".mdb")
                            {
                                Regex regex = new Regex(@"[\?~@#\$%`\^<>]+");
                                MatchCollection matches = regex.Matches(FileUpload1.PostedFile.FileName);
                                if (matches.Count > 0)
                                {
                                    msg += "Invalid File Name Format.";
                                }
                                else
                                {
                                    string fileName = "~/mis/HR/UploadDoc/AttendanceDoc/" + FileUpload1.PostedFile.FileName;
                                    fileName = fileName.Replace("\0", "").Replace(Convert.ToChar(0x0).ToString(), "");
                                    ViewState["fileName"] = fileName;
                                    FileUpload1.PostedFile.SaveAs(Server.MapPath(fileName).Replace("\\", "//"));
                                }
                            }
                            else
                            {
                                msg += "Invalid File Format. Allow only mdb format for file upload.";
                            }
                        }

                   //  File.WriteAllText(@"C:\Users\Public\TextLogfile.txt", "Upload Successfull \r\n");  
                    }
                    else
                    {
                        msg += "Enter File\\n";
                    }
                    if (msg == "")
                    {
                        // OleDbConnection cn = new OleDbConnection(ConnectionAccess.con());   
                     //    File.WriteAllText(@"C:\Users\Public\TextLogfile.txt", "Read Start \r\n");  
                        string AttDate = Convert.ToDateTime(txtAttDate.Text, cult).ToString("yyyy/MM/dd");
                        string con = ConfigurationManager.ConnectionStrings["conn2"].ConnectionString;
                        OleDbConnection cn = new OleDbConnection(con);
                        cn.Open();
						// string sql = "select EmployeeCode,EmployeeName,Format (AttendanceDate,'yyyy/mm/dd') as AttendanceDate,InTime,OutTime,Duration AS Duration_IN_MIN from AttendanceLogs LEFT JOIN Employees on Employees.EmployeeId=AttendanceLogs.EmployeeId  where CInt(Duration)>0 and Len(Employees.EmployeeCode)>4 AND Duration > 0  AND Format (AttendanceDate,'yyyy/mm/dd') = '" + AttDate + "'";
                        string sql = "select EmployeeCode,EmployeeName,Format (AttendanceDate,'yyyy/mm/dd') as AttendanceDate,InTime,OutTime,Duration AS Duration_IN_MIN from AttendanceLogs LEFT JOIN Employees on Employees.EmployeeId=AttendanceLogs.EmployeeId  where  Len(Employees.EmployeeCode)>4 AND Format (AttendanceDate,'yyyy/mm/dd') = '" + AttDate + "'";
                        OleDbDataAdapter da = new OleDbDataAdapter(sql, cn);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        cn.Close();
                      //  File.WriteAllText(@"C:\Users\Public\TextLogfile.txt", "Read End \r\n");  
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                         //    File.WriteAllText(@"C:\Users\Public\TextLogfile.txt", "Read Successfull \r\n");
                           
                            string EmployeeCode = "", SelectDate = "", LoginTime = "", LogoutTime = "", WorkingHours = "";
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                EmployeeCode = ds.Tables[0].Rows[i]["EmployeeCode"].ToString();
                                SelectDate = ds.Tables[0].Rows[i]["AttendanceDate"].ToString();
                                LoginTime = ds.Tables[0].Rows[i]["InTime"].ToString();
                                LogoutTime = ds.Tables[0].Rows[i]["OutTime"].ToString();								
								 if (LogoutTime == "00:00")
                                   {
                                       LogoutTime = LoginTime;
                                   }
								 
                                int comma1 = LoginTime.IndexOf('(');
                                if (comma1 != -1)
                                {
                                    LoginTime = LoginTime.Substring(0, comma1);
                                }

                                int comma = LogoutTime.IndexOf('(');
                                if (comma != -1)
                                {
                                    LogoutTime = LogoutTime.Substring(0, comma);
                                }
                                if (WorkingHours == "")
                                {
                                    WorkingHours = "0";
                                }
                                WorkingHours = ds.Tables[0].Rows[i]["Duration_IN_MIN"].ToString();
                                WorkingHours = int.Parse((int.Parse(WorkingHours) / 60).ToString()).ToString() + ':' + (int.Parse(WorkingHours) % 60).ToString();
							   if(LoginTime != "00:00")
                               {
                                objdb.ByProcedure("SpHRDaily_Attendance_New",
                                    new string[] { "flag", "EmployeeCode", "SelectDate", "LoginTime", "LogoutTime", "WorkingHours" },
                                    new string[] { "0", EmployeeCode, SelectDate, LoginTime, LogoutTime, WorkingHours }, "dataset");
									
									lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Saved");

                              //      File.WriteAllText(@"C:\Users\Public\TextLogfile.txt", "EmployeeCode [" + EmployeeCode + "] Record Inserted \r\n");
								}
                                
                            }
                            
                            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

                            ds = objdb.ByProcedure("SpHRDaily_Attendance_New", new string[] { "flag", "SelectDate" }, new string[] { "1", AttDate }, "dataset");
                            if (ds.Tables[0].Rows.Count != 0)
                            {
                                GridView1.DataSource = ds;
                                GridView1.DataBind();
                            }

                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('No Record Found...!!!');", true);
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}