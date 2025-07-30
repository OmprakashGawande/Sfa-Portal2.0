using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_Payroll_PayrollUpdateArrearsDAOnlyReport : System.Web.UI.Page
{
    DataSet ds,ds2 = new DataSet();
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    //btnSave.Visible = false;
                    //FillGrid();
                    FillDropdown();
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
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));

            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            }
            ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            if (ddlArrearMonths.SelectedValue.ToString() == "1")
            {
                ds = objdb.ByProcedure("SpPayrollEmpArrearOnlyDA", new string[] { "flag", "Office_ID", "TypeOfPost" }, new string[] { "7", ddlOfficeName.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
            }
            if (ddlArrearMonths.SelectedValue.ToString() == "2")
            {
                ds = objdb.ByProcedure("SpPayrollEmpArrearOnlyDA", new string[] { "flag", "Office_ID", "TypeOfPost" }, new string[] { "8", ddlOfficeName.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
            }
            if (ddlArrearMonths.SelectedValue.ToString() == "3")
            {
                ds = objdb.ByProcedure("SpPayrollEmpArrearOnlyDA", new string[] { "flag", "Office_ID", "TypeOfPost" }, new string[] { "5", ddlOfficeName.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    decimal NetDa1 = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("DA_Jan_To_Mar"));
                    decimal NetDa2 = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("DA_Apr_To_Aug"));
                    decimal NetEpf = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPF_Amount"));
                    decimal NetTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount"));

                    GridView1.FooterRow.Cells[0].Text = "Total";
                    GridView1.FooterRow.Cells[3].Text = NetDa1.ToString("N2");
                    GridView1.FooterRow.Cells[3].CssClass = "TotalDa";
                    GridView1.FooterRow.Cells[4].Text = NetDa2.ToString("N2");
                    GridView1.FooterRow.Cells[4].CssClass = "TotalDa";
                    GridView1.FooterRow.Cells[5].Text = NetEpf.ToString("N2");
                    GridView1.FooterRow.Cells[5].CssClass = "TotalEpf";
                    GridView1.FooterRow.Cells[6].Text = NetTotal.ToString("N2");
                    GridView1.FooterRow.Cells[6].CssClass = "TotalNet";
                }
            }



            //ds = objdb.ByProcedure("SpPayrollEmpArrearOnlyDA",
            //    new string[] { "flag", "Emp_ID", "DaOldRate", "DaRate", "FromMonth", "FromYear", "ToMonth", "ToYear" },
            //    new string[] { "0", ddlEmployee.SelectedValue.ToString(), olddarate.ToString(), newdarate.ToString(), frommonth.ToString(), fromyear.ToString(), tomonth.ToString(), toyear.ToString() }, "dataset");

            //if (ds != null && ds.Tables[0].Rows.Count != 0)
            //{
            //    GridView1.DataSource = ds;
            //    GridView1.DataBind();

            //    decimal NetDa = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("DaRemainingArrearAmount"));
            //    decimal NetEpf = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("RemainingEpfAmount"));
            //    decimal NetTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("NetPayment"));
            //    GridView1.FooterRow.Cells[1].Text = "Total";
            //    GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;

            //    GridView1.FooterRow.Cells[7].Text = NetDa.ToString("N2");
            //    GridView1.FooterRow.Cells[7].CssClass = "TotalDa";
            //    GridView1.FooterRow.Cells[8].Text = NetEpf.ToString("N2");
            //    GridView1.FooterRow.Cells[8].CssClass = "TotalEpf";
            //    GridView1.FooterRow.Cells[9].Text = NetTotal.ToString("N2");
            //    GridView1.FooterRow.Cells[9].CssClass = "TotalNet";
            //    btnSave.Visible = true;
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
			FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlArrearMonths_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlEmployee.SelectedIndex = 0;
            GridView1.DataSource = new string[] { };
        GridView1.DataBind();
    }
}