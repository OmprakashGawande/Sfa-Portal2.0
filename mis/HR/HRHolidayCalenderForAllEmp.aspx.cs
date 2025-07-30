using System;
using System.Data;
using System.Globalization;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;


public partial class mis_HR_HRHolidayCalenderForAllEmp : System.Web.UI.Page
{
    static Hashtable HolidayList;
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["Page"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();

                /******Holiday Calender*********/
                HolidayList = Getholiday();
                Calendar1.Caption = "";
                Calendar1.VisibleDate = DateTime.Today;
                Calendar1.FirstDayOfWeek = FirstDayOfWeek.Sunday;
                Calendar1.NextPrevFormat = NextPrevFormat.ShortMonth;
                Calendar1.TitleFormat = TitleFormat.MonthYear;
                Calendar1.ShowGridLines = true;
                Calendar1.DayStyle.Height = new Unit(50);
                Calendar1.DayStyle.Width = new Unit(150);
                Calendar1.DayStyle.HorizontalAlign = HorizontalAlign.Center;
                Calendar1.DayStyle.VerticalAlign = VerticalAlign.Middle;
                Calendar1.OtherMonthDayStyle.BackColor = System.Drawing.Color.AliceBlue;
                /***************/
                FillGrid();
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
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHRHoliday", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private Hashtable Getholiday()
    {
        Hashtable holiday = new Hashtable();
        ds = objdb.ByProcedure("SpHRHoliday", new string[] { "flag" }, new string[] { "7" }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string newdate = "";
            string txtDate = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                txtDate = ds.Tables[0].Rows[i]["Holiday_Date"].ToString();
                //newdate = DateTime.Parse(txtDate).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                //newdate = DateTime.ParseExact(txtDate, "dd/mm/yyyy", CultureInfo.InvariantCulture).ToString("mm/dd/yyyy", CultureInfo.InvariantCulture);
                newdate = txtDate;
                string val = ds.Tables[0].Rows[i]["Holiday_Name"].ToString();
                //val = "Ram Navmi"; 
                holiday[newdate] = val;

            }

        }

        return holiday;
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        LabelAction.Text = "Date changed to :" + Calendar1.SelectedDate.ToShortDateString();
    }

    protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        LabelAction.Text = "Month changed to :" + e.NewDate.ToShortDateString();
    }

    bool IsSecondSaturdayInMonth(DateTime date)
    {
        // 2nd Saturday cannot be before the 8th day or after 14th of the month...
        if (date.Day < 8 || date.Day > 14)
        {
            return false;
        }

        return date.DayOfWeek == DayOfWeek.Saturday;

    }

    bool IsThirdSaturdayInMonth(DateTime date)
    {
        // 2nd Saturday cannot be before the 8th day or after 14th of the month...
        if (date.Day < 15 || date.Day > 21)
        {
            return false;
        }

        return date.DayOfWeek == DayOfWeek.Saturday;

    }

    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        if (HolidayList[e.Day.Date.ToShortDateString()] != null)
        {
            Literal literal1 = new Literal();
            literal1.Text = "<br/>";
            e.Cell.Controls.Add(literal1);
            Label label1 = new Label();
            label1.Text = (string)HolidayList[e.Day.Date.ToShortDateString()];
            label1.Font.Size = new FontUnit(FontSize.Small);
            e.Cell.Controls.Add(label1);
        }

        if (IsSecondSaturdayInMonth(e.Day.Date) || IsThirdSaturdayInMonth(e.Day.Date))
        {
            e.Cell.ForeColor = System.Drawing.Color.Red;
        }

    }
    protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridView1.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}