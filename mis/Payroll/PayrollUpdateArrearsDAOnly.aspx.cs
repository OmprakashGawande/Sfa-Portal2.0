using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_Payroll_PayrollUpdateArrearsDAOnly : System.Web.UI.Page
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
                    btnSave.Visible = false;
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
            ds = null;
            ds = objdb.ByProcedure("SpPayrollEmpArrearOnlyDA", new string[] { "flag", "Office_ID" }, new string[] { "6", ddlOfficeName.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = ds;
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            }
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
            int olddarate = 9;
            int newdarate = 12;
            
            ds = objdb.ByProcedure("SpPayrollEmpArrearOnlyDA", new string[] { "flag", "Emp_ID" }, new string[] { "1", ddlEmployee.SelectedValue.ToString() }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0){
                if (ds.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent")
                {
                    olddarate = 9;
                    newdarate = 12;
                }else if (ds.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                {
                    olddarate = 148;
                    newdarate = 154;
                }

            }

            
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();

            int frommonth = 01;
            int fromyear = 2019;
            int tomonth = 08;
            int toyear = 2019;
            if (ddlArrearMonths.SelectedValue.ToString() == "1")
            {
                frommonth = 01;
                fromyear = 2019;
                tomonth = 03;
                toyear = 2019;
            }
            else
            {
                frommonth = 04;
                fromyear = 2019;
                tomonth = 08;
                toyear = 2019;
            }

            ds = objdb.ByProcedure("SpPayrollEmpArrearOnlyDA",
                new string[] { "flag", "Emp_ID", "DaOldRate", "DaRate", "FromMonth", "FromYear", "ToMonth", "ToYear" },
                new string[] { "0", ddlEmployee.SelectedValue.ToString(), olddarate.ToString(), newdarate.ToString(), frommonth.ToString(), fromyear.ToString(), tomonth.ToString(), toyear.ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();

                decimal NetDa = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("DaRemainingArrearAmount"));
                decimal NetEpf = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("RemainingEpfAmount"));
                decimal NetTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("NetPayment"));
                GridView1.FooterRow.Cells[1].Text = "Total";
                GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;

                GridView1.FooterRow.Cells[7].Text = NetDa.ToString("N2");
                GridView1.FooterRow.Cells[7].CssClass = "TotalDa";
                GridView1.FooterRow.Cells[8].Text = NetEpf.ToString("N2");
                GridView1.FooterRow.Cells[8].CssClass = "TotalEpf";
                GridView1.FooterRow.Cells[9].Text = NetTotal.ToString("N2");
                GridView1.FooterRow.Cells[9].CssClass = "TotalNet";
                btnSave.Visible = true;
            }
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
			lblMsg.Text = "";
            string order_no = "HO/ACCOUNT/2019/2670";
            string order_date = "2019-08-27";
            foreach (GridViewRow row in GridView1.Rows)
            {
                Label ArrearMonth = (Label)row.FindControl("lblRowNumber");
                string ArrearMonth_No = ArrearMonth.ToolTip.ToString();

                Label ArrearYear = (Label)row.FindControl("ArrearMonth");
                string ArrearYear_No = ArrearYear.ToolTip.ToString();
                TextBox BasicSalary = (TextBox)row.FindControl("txtBasicSalary");
                TextBox ArrearBasicSalary = (TextBox)row.FindControl("txtArrearBasicSalary");
                TextBox TotalBasicSalary = (TextBox)row.FindControl("txtTotalBasicSalary");
                TextBox PaidDa = (TextBox)row.FindControl("txtPaidDa");
                TextBox DaToBePaid = (TextBox)row.FindControl("txtDaToBePaid");
                TextBox DaRemainingArrearAmount = (TextBox)row.FindControl("txtDaRemainingArrearAmount");
                HiddenField RemainingEpfAmount = (HiddenField)row.FindControl("hfRemainingEpfAmount");
                HiddenField NetPayment = (HiddenField)row.FindControl("hfNetPayment");

                ds = objdb.ByProcedure("SpPayrollEmpArrearOnlyDA",
                    new string[] { "flag", "Office_ID", "Emp_ID", "CurrentYear", "CurrentMonth", "OrderNo", "OrderDate", "FromYear", "FromMonth", "FromMonthName", "ToYear", "ToMonth", "ToMonthName", "BasicSalary", "TotalEarning", "Policy", "TotalDeduction", "NetPaymentAmount", "Arrear_UpdatedBy", "Arrear_Type" },
                    new string[] { "2", ddlOfficeName.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(),"2019", "09", order_no,order_date, ArrearYear_No, ArrearMonth_No,ArrearMonth_No, ArrearYear_No, ArrearMonth_No,ArrearMonth_No
                        ,"0", DaRemainingArrearAmount.Text, "0", RemainingEpfAmount.Value, NetPayment.Value, ViewState["Emp_ID"].ToString(),"DA" }, "dataset");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string Arrear_ID = ds.Tables[0].Rows[0]["EmpArrearID"].ToString();
                        string LoginUserID = ViewState["Emp_ID"].ToString();

                        int[] EarningDeductionId = new int[] { 1, 2, 3, 4, 5, 6, 7, 25, 27, 29, 8, 9, 30, 10, 11, 12, 13, 14, 16, 18, 19, 20, 22, 23, 28 };
                        foreach (int ED_ID in EarningDeductionId)
                        {

                            string EarningDeductionType = "";
                            string EarningDeductionName = "";
                            ds2 = objdb.ByProcedure("SpPayrollEmpArrearOnlyDA",
                                              new string[] { "flag", "EarnDedution_ID" },
                                              new string[] { "4",ED_ID.ToString() }, "dataset");

                            if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                            {
                                EarningDeductionType = ds2.Tables[0].Rows[0]["EarnDeduction_Type"].ToString();
                                EarningDeductionName = ds2.Tables[0].Rows[0]["EarnDeduction_Name"].ToString();
                            }

                            if (ED_ID==1)
                            {
                                objdb.ByProcedure("SpPayrollEmpArrearOnlyDA",
                                              new string[] { "flag", "EmpArrear_ID", "Emp_ID", "EarnDedution_ID", "EarnDeductionName", "RemainingAmount", "EarnDeductionType", "ArrearEarnDeduction_UpdatedBy" },
                                              new string[] { "3", Arrear_ID, ddlEmployee.SelectedValue.ToString(), ED_ID.ToString(), EarningDeductionName, DaRemainingArrearAmount.Text, EarningDeductionType, LoginUserID }, "dataset");

                            } else if (ED_ID == 8)
                            {
                                objdb.ByProcedure("SpPayrollEmpArrearOnlyDA",
                                              new string[] { "flag", "EmpArrear_ID", "Emp_ID", "EarnDedution_ID", "EarnDeductionName", "RemainingAmount", "EarnDeductionType", "ArrearEarnDeduction_UpdatedBy" },
                                              new string[] { "3", Arrear_ID, ddlEmployee.SelectedValue.ToString(), ED_ID.ToString(), EarningDeductionName, RemainingEpfAmount.Value, EarningDeductionType, LoginUserID }, "dataset");

                            }
                            else
                            {
                                objdb.ByProcedure("SpPayrollEmpArrearOnlyDA",
                                              new string[] { "flag", "EmpArrear_ID", "Emp_ID", "EarnDedution_ID", "EarnDeductionName", "RemainingAmount", "EarnDeductionType", "ArrearEarnDeduction_UpdatedBy" },
                                              new string[] { "3", Arrear_ID, ddlEmployee.SelectedValue.ToString(), ED_ID.ToString(), EarningDeductionName, "0", EarningDeductionType, LoginUserID }, "dataset");
                            }

                        }


                    }
            }
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
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