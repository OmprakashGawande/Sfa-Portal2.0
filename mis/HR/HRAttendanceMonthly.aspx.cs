using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System;
using System.Text;
public partial class mis_HR_HRAttendanceMonthly : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        StringBuilder htmlStr = new StringBuilder();
        String monthName ="";
        monthName = ddlMonth.SelectedValue.ToString();
        if (monthName == "01")
        {
            htmlStr.Append("<a href='Attendance/JanAttendance.xlsx' class='btn btn-primary btn-flat' style='margin:2px;'>Excel</a>");
            htmlStr.Append("<iframe src='Attendance/JanAttendance.pdf' width='100%' height='1000'></iframe>");
        }
        if (monthName == "02")
        {
            htmlStr.Append("<a href='Attendance/FebAttendance.xlsx' class='btn btn-primary btn-flat' style='margin:2px;'>Excel</a>");
            htmlStr.Append("<iframe src='Attendance/FebAttendance.pdf' width='100%' height='1000'></iframe>");
        }
        if (monthName == "03")
        {
            htmlStr.Append("<a href='Attendance/MarAttendance.xlsx' class='btn btn-primary btn-flat' style='margin:2px;'>Excel</a>");
            htmlStr.Append("<iframe src='Attendance/MarAttendance.pdf' width='100%' height='1000'></iframe>");
        }
        if (monthName == "04")
        {
           htmlStr.Append("<a href='Attendance/AprAttendance.xlsx' class='btn btn-primary btn-flat' style='margin:2px;'>Excel</a>");
           htmlStr.Append("<iframe src='Attendance/AprAttendance.pdf' width='100%' height='1000'></iframe>");
        } 
        if (monthName =="05")
        {
            htmlStr.Append("<a href='Attendance/MayAttendance.xlsx' class='btn btn-primary btn-flat' style='margin:2px;'>Excel</a>");
            htmlStr.Append("<iframe src='Attendance/MayAttendance.pdf' width='100%' height='1000'></iframe>");
        }
        if (monthName == "06")
        {
            htmlStr.Append("<a href='Attendance/JunAttendance.xlsx' class='btn btn-primary btn-flat' style='margin:2px;'>Excel</a>");
            htmlStr.Append("<iframe src='Attendance/JunAttendance.pdf' width='100%' height='1000'></iframe>");
        }  
        divIframe.InnerHtml = htmlStr.ToString();
    }
}