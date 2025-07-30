using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollEmpArrearYearWiseDetail : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillDropdown();
                    DivGridDetail.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDropdown()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
            }

            ds.Reset();
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Financial_Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlEarDedType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByProcedure("SpPayrollEmpArrear", new string[] { "flag", "EarnDeduction_Year" }, new string[] { "8", ddlYear.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables.Count != 0)
            {
                if (ddlEarDedType.SelectedValue.ToString() == "Earning")
                {
                    ddlEarnDed.DataSource = ds.Tables[0];
                    ddlEarnDed.DataTextField = "EarnDeduction_Name";
                    ddlEarnDed.DataValueField = "EarnDeduction_ID";
                    ddlEarnDed.DataBind();
                    ddlEarnDed.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlEarnDed.DataSource = ds.Tables[1];
                    ddlEarnDed.DataTextField = "EarnDeduction_Name";
                    ddlEarnDed.DataValueField = "EarnDeduction_ID";
                    ddlEarnDed.DataBind();
                    ddlEarnDed.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            DivGridDetail.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DivGridDetail.Visible = true;
            string FromYear = "";
            string ToYear = "";
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            spnHead.InnerText = ddlEarnDed.SelectedItem.Text;
            spnOffice.InnerText = ddlOffice.SelectedItem.Text;
            int Year = Convert.ToInt16(ddlYear.SelectedValue.ToString());
            FromYear = Year.ToString();
            ToYear = (Year + 1).ToString();
            SpnFromYear.InnerText = FromYear;
            SpnToYear.InnerText = ToYear;
            ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "Office_ID", "Year", "EarnDeduction_ID" }, new string[] { "6", ddlOffice.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlEarnDed.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                decimal Arrear = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ArrearSum"));
                decimal Apr_Amount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Apr_Amount"));
                decimal May_Amount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("May_Amount"));
                decimal Jun_Amount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Jun_Amount"));
                decimal Jul_Amount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Jul_Amount"));
                decimal Aug_Amount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Aug_Amount"));
                decimal Sep_Amount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Sep_Amount"));
                decimal Oct_Amount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Oct_Amount"));
                decimal Nov_Amount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Nov_Amount"));
                decimal Dec_Amount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Dec_Amount"));
                decimal Jan_Amount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Jan_Amount"));
                decimal Feb_Amount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Feb_Amount"));
                decimal Mar_Amount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Mar_Amount"));
                GridView1.FooterRow.Cells[1].Text = "<b>| TOTAL |</b>";
                GridView1.FooterRow.Cells[2].Text = "<b>" + Arrear.ToString() + "</b>";
                GridView1.FooterRow.Cells[3].Text = "<b>" + Apr_Amount.ToString() + "</b>";
                GridView1.FooterRow.Cells[4].Text = "<b>" + May_Amount.ToString() + "</b>";
                GridView1.FooterRow.Cells[5].Text = "<b>" + Jun_Amount.ToString() + "</b>";
                GridView1.FooterRow.Cells[6].Text = "<b>" + Jul_Amount.ToString() + "</b>";
                GridView1.FooterRow.Cells[7].Text = "<b>" + Aug_Amount.ToString() + "</b>";
                GridView1.FooterRow.Cells[8].Text = "<b>" + Sep_Amount.ToString() + "</b>";
                GridView1.FooterRow.Cells[9].Text = "<b>" + Oct_Amount.ToString() + "</b>";
                GridView1.FooterRow.Cells[10].Text = "<b>" + Nov_Amount.ToString() + "</b>";
                GridView1.FooterRow.Cells[11].Text = "<b>" + Dec_Amount.ToString() + "</b>";
                GridView1.FooterRow.Cells[12].Text = "<b>" + Jan_Amount.ToString() + "</b>";
                GridView1.FooterRow.Cells[13].Text = "<b>" + Feb_Amount.ToString() + "</b>";
                GridView1.FooterRow.Cells[14].Text = "<b>" + Mar_Amount.ToString() + "</b>";
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}