using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HRDateRangeWiseAttendenceReport : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillOffice();
                    FillEmployee();
                    txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEmployee()
    {
        try
        {
            ds = objdb.ByProcedure("SpHRRptDailyAttendance", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = ds;
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("ALL", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOffice()
    {
        try
        {
            ds = objdb.ByProcedure("SpHRRptDailyAttendance", new string[] { "flag" }, new string[] { "4" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                ddlOffice.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void Fillgrid()
    {
        try
        {
            lblMsg.Text = "";

            string FROMDATE = Convert.ToDateTime(txtStartDate.Text, cult).ToString("yyyy-MM-dd");
            string TODATE = Convert.ToDateTime(txtEndDate.Text, cult).ToString("yyyy-MM-dd");

            if (Rbtn_Type1.SelectedValue.ToString() == "1")
            {
                ds = objdb.ByProcedure("SpHRRptDailyAttendance",
               new string[] { "flag", "FromDate", "ToDate", "Office_ID", "Emp_ID" },
               new string[] { "9", FROMDATE, TODATE, ddlOffice.SelectedValue.ToString(), ddlEmployee.SelectedValue }, "dataset");
            }
            //else if (Rbtn_Type.SelectedValue.ToString() == "2")
            //{
            //    ds = objdb.ByProcedure("SpHRRptDailyAttendance",
            //   new string[] { "flag", "FromDate", "ToDate", "Office_ID", "Emp_ID" },
            //   new string[] { "10", FROMDATE, TODATE, ddlOffice.SelectedValue.ToString(), ddlEmployee.SelectedValue }, "dataset");
            //}
            else if (Rbtn_Type1.SelectedValue.ToString() == "2")
            {
                ds = objdb.ByProcedure("SpHRRptDailyAttendance",
               new string[] { "flag", "FromDate", "ToDate", "Office_ID", "Emp_ID" },
               new string[] { "8", FROMDATE, TODATE, ddlOffice.SelectedValue.ToString(), ddlEmployee.SelectedValue }, "dataset");
            }
            else if (Rbtn_Type1.SelectedValue.ToString() == "3")
            {
                ds = objdb.ByProcedure("SpHRRptDailyAttendance",
               new string[] { "flag", "FromDate", "ToDate", "Office_ID", "Emp_ID" },
               new string[] { "14", FROMDATE, TODATE, ddlOffice.SelectedValue.ToString(), ddlEmployee.SelectedValue }, "dataset");
            }

            else if (Rbtn_Type2.SelectedValue.ToString() == "4")
            {
                ds = objdb.ByProcedure("SpHRRptDailyAttendance",
               new string[] { "flag", "FromDate", "ToDate", "Office_ID", "Emp_ID" },
               new string[] { "11", FROMDATE, TODATE, ddlOffice.SelectedValue.ToString(), ddlEmployee.SelectedValue }, "dataset");
            }
            else if (Rbtn_Type2.SelectedValue.ToString() == "5")
            {
                ds = objdb.ByProcedure("SpHRRptDailyAttendance",
               new string[] { "flag", "FromDate", "ToDate", "Office_ID", "Emp_ID" },
               new string[] { "12", FROMDATE, TODATE, ddlOffice.SelectedValue.ToString(), ddlEmployee.SelectedValue }, "dataset");
            }
            else if (Rbtn_Type2.SelectedValue.ToString() == "6")
            {
                ds = objdb.ByProcedure("SpHRRptDailyAttendance",
               new string[] { "flag", "FromDate", "ToDate", "Office_ID", "Emp_ID" },
               new string[] { "13", FROMDATE, TODATE, ddlOffice.SelectedValue.ToString(), ddlEmployee.SelectedValue }, "dataset");
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
                if (Rbtn_Type2.SelectedValue.ToString() == "4" || Rbtn_Type2.SelectedValue.ToString() == "5" || Rbtn_Type2.SelectedValue.ToString() == "6")
                {
                    GridView1.Columns[2].Visible = false;
                    GridView1.Columns[3].Visible = false;
                    GridView1.Columns[4].Visible = false;
                    //GridView1.Columns[5].HeaderText = "Average Working Hours";
                }
                else
                {
                    GridView1.Columns[2].Visible = true;
                    GridView1.Columns[3].Visible = true;
                    GridView1.Columns[4].Visible = true;
                    //GridView1.Columns[5].HeaderText = "Working Hours";
                }
            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
            }
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (Rbtn_Type1.SelectedIndex > -1 || Rbtn_Type2.SelectedIndex > -1)
            {
                Fillgrid();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


}