using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

public partial class mis_Payroll_Payroll_FinalStatementEmployeewise : System.Web.UI.Page
{
    DataSet ds, ds5 = new DataSet();
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Emp_ID"] != null)
            {

                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillDropdown();

            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
    }
    protected void FillDropdown()
    {
        try
        {
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Financial_Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            }
            ds = null;
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
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ddlOfficeName.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = ds;
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            }
            ddlEmployee.SelectedValue = ViewState["Emp_ID"].ToString();
            if (ViewState["Emp_ID"].ToString() == "40")
            {
                ddlEmployee.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetPayrollSalaryDetails();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=PayrollSalaryDetailsForAdvanceTaxDeduction.xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            div_page_content.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    private void GetPayrollSalaryDetails()
    {
        DataSet ds5 = new DataSet();
        try
        {
            ds = objdb.ByProcedure("SpPayrollSalaryAdvanceTax_FY", new string[] { "flag", "Emp_ID" }, new string[] { "2", ddlEmployee.SelectedValue.ToString() }, "dataset");



            ds5 = objdb.ByProcedure("SpPayrollSalaryAdvanceTax_FY", new string[] { "flag", "Year", "Emp_ID", "Office_ID" }, new string[] { "1", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), ddlOfficeName.SelectedValue.ToString() }, "dataset");
            StringBuilder sb1 = new StringBuilder();
            int Count1 = ds5.Tables[0].Rows.Count;
            int ColCount1 = ds5.Tables[0].Columns.Count;
            if (ds5 != null && ds5.Tables[0].Rows.Count > 0)
            {
                decimal bs = 0, hra = 0, da = 0, Conv = 0, Ord = 0, Wash = 0, OtherEarning, et = 0,gpfval=0,epfval=0,etval=0;
                decimal epf = 0, gpf, itax = 0, ptax = 0, OtherDeduction = 0, DeductionTotal = 0, dtval = 0;

                decimal at_bs = 0, at_hra = 0, at_da = 0, at_Conv = 0, at_Ord = 0, at_Wash = 0, at_OtherEarning = 0, at_et = 0,at_gpfval=0,at_epfval=0, at_etval = 0;
                decimal at_epf = 0, at_gpf = 0, at_itax = 0, at_ptax = 0, at_OtherDeduction = 0, at_DeductionTotal = 0, at_dtval=0;

                lblMsg.Text = string.Empty;
                btnPrint.Visible = true;
                btnExcel.Visible = true;

                sb1.Append("<table class='table1' style='width:100%;'>");
                sb1.Append("<thead>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center' colspan='17'><b>SFA Technologies Pvt. Ltd.<br/>Branch : " + ds.Tables[0].Rows[0]["Office_Name"] + "<b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center' colspan='17'><b>Income Tax assesment for the Year</b> " + ddlYear.SelectedItem.Text + " <b>(Tentative Statement)</b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:left;' colspan='6'><b>Name of the Employee: </b>" + ds.Tables[0].Rows[0]["Emp_Name"] + "</td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:left;' colspan='4'><b>Designation: </b>" + ds.Tables[0].Rows[0]["Designation_Name"] + "</td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:left;' colspan='4'><b>PAN No.: </b>" + ds.Tables[0].Rows[0]["Emp_PanCardNo"] + "</td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:left;' colspan='1'><b>Code: </b>" + ds.Tables[0].Rows[0]["SalarySec_No"] + "  " + ds.Tables[0].Rows[0]["SalaryEmp_No"] + "</td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:left;' colspan='2'><b>Run Date: </b>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>Month<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>Year<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>BASIC<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>DA<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>HRA<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>Conv<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>Ord<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>Wash<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>Other<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>Total Earning<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>EPF / GPF<b></td>");
                //sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>ADA<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>ITax<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>PTax<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>LIC<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>Total Deduction<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>Net Income<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>EnCash Date<b></td>");
                sb1.Append("</tr>");
                sb1.Append("</thead>");
                for (int i = 0; i < Count1; i++)
                {
                    var outputParame_gpf = ds5.Tables[0].Rows[i]["GPF"];
                   
                    if (!(outputParame_gpf is DBNull))
                    {
                        gpfval = Convert.ToDecimal(ds5.Tables[0].Rows[i]["GPF"]);
                    }
                    var outputParame_epf = ds5.Tables[0].Rows[i]["EPF"];
                    if (!(outputParame_epf is DBNull))
                    {
                        epfval = Convert.ToDecimal(ds5.Tables[0].Rows[i]["EPF"]);
                    }

                    var outputParame_et = ds5.Tables[0].Rows[i]["EarningTotal"];
                   
                    if (!(outputParame_et is DBNull))
                    {
                        etval = Convert.ToDecimal(ds5.Tables[0].Rows[i]["EarningTotal"]);
                    }
                    var outputParame_dtval = ds5.Tables[0].Rows[i]["DeductionTotal"];
                    if (!(outputParame_dtval is DBNull))
                    {
                        dtval = Convert.ToDecimal(ds5.Tables[0].Rows[i]["DeductionTotal"]);
                    }
                    
                    sb1.Append("<tr>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["Particular"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["MYear"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["BasicSalary"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["DA"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["HRA"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["Conv"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["Ord"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["Wash"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["OtherEarning"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["EarningTotal"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + (ds.Tables[0].Rows[0]["EPF_No"].ToString() == "" ? gpfval : ds5.Tables[0].Rows[i]["EPF"]) + "</td>");
                    //sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["ADA"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["ITax"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["PTax"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["OtherDeduction"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + (ds.Tables[0].Rows[0]["EPF_No"].ToString() == "" ? (gpfval + Convert.ToDecimal(dtval)) : (epfval + Convert.ToDecimal(dtval))) + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + (ds.Tables[0].Rows[0]["EPF_No"].ToString() == "" ? (Convert.ToDecimal(etval) - (gpfval + Convert.ToDecimal(dtval))) : (Convert.ToDecimal(etval) - (epfval+ Convert.ToDecimal(dtval)))) + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["EnCashDate"] + "</td>");
                    sb1.Append("</tr>");
                }
                bs = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("BasicSalary") ?? 0);
                da = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DA") ?? 0);
                hra = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("HRA") ?? 0);
                Conv = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("Conv") ?? 0);
                Ord = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("Ord") ?? 0);
                Wash = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("Wash") ?? 0);
                OtherEarning = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("OtherEarning") ?? 0);
                et = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("EarningTotal") ?? 0);
                epf = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("EPF") ?? 0);
                gpf = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("GPF") ?? 0);
                //ada = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("ADA")??0);
                itax = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("ITax") ?? 0);
                ptax = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("PTax") ?? 0);
                OtherDeduction = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("OtherDeduction") ?? 0);
                DeductionTotal = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DeductionTotal") ?? 0);
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center' colspan='2'><b>| Total |</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + bs + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + da + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + hra + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + Conv + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + Ord + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + Wash + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + OtherEarning + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + et + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (ds.Tables[0].Rows[0]["EPF_No"].ToString() == "" ? gpf : epf) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + itax + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + ptax + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + OtherDeduction + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (ds.Tables[0].Rows[0]["EPF_No"].ToString() == "" ? (DeductionTotal + gpf) : (DeductionTotal+epf)) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (ds.Tables[0].Rows[0]["EPF_No"].ToString() == "" ? (et-(DeductionTotal + gpf)) : (et-(DeductionTotal + epf))) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'></td>");
                sb1.Append("</tr>");

                if (ds5.Tables[1].Rows.Count > 0)
                {
                    int Count11 = ds5.Tables[1].Rows.Count;
                    for (int i = 0; i < Count11; i++)
                    {

                        var outputParame_atgpf = ds5.Tables[1].Rows[i]["GPF"];
                        if (!(outputParame_atgpf is DBNull))
                        {
                                at_gpfval = Convert.ToDecimal(ds5.Tables[1].Rows[i]["GPF"]);
                        }
                        var outputParame_atepf = ds5.Tables[1].Rows[i]["EPF"];
                        if (!(outputParame_atepf is DBNull))
                        {
                            at_epfval = Convert.ToDecimal(ds5.Tables[1].Rows[i]["EPF"]);
                        }
                        var outputParame_at_etval = ds5.Tables[1].Rows[i]["EarningTotal"];

                        if (!(outputParame_at_etval is DBNull))
                        {
                            at_etval = Convert.ToDecimal(ds5.Tables[1].Rows[i]["EarningTotal"]);
                        }
                        var outputParame_at_dtval = ds5.Tables[1].Rows[i]["DeductionTotal"];
                        if (!(outputParame_at_dtval is DBNull))
                        {
                            at_dtval = Convert.ToDecimal(ds5.Tables[1].Rows[i]["DeductionTotal"]);
                        }
                    
                        sb1.Append("<tr>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;' colspan='2'>" + ds5.Tables[1].Rows[i]["ArrearType"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["BasicSalary"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["DA"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["HRA"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["Conv"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["Ord"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["Wash"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["OtherEarning"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["EarningTotal"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + (ds.Tables[0].Rows[0]["EPF_No"].ToString() == "" ? ds5.Tables[1].Rows[i]["GPF"] : ds5.Tables[1].Rows[i]["EPF"]) + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["ITax"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["PTax"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["OtherDeduction"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + (ds.Tables[0].Rows[0]["EPF_No"].ToString() == "" ? (at_gpfval + Convert.ToDecimal(at_dtval)) : (at_epfval + Convert.ToDecimal(at_dtval))) + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + (ds.Tables[0].Rows[0]["EPF_No"].ToString() == "" ? (Convert.ToDecimal(at_etval) - (at_gpfval + Convert.ToDecimal(at_dtval))) : (Convert.ToDecimal(at_etval) - (at_epfval + Convert.ToDecimal(at_dtval)))) + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'></td>");
                        sb1.Append("</tr>");
                    }
                    at_bs = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("BasicSalary") ?? 0);
                    at_da = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("DA") ?? 0);
                    at_hra = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("HRA") ?? 0);

                    at_Conv = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("Conv") ?? 0);
                    at_Ord = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("Ord") ?? 0);
                    at_Wash = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("Wash") ?? 0);
                    at_OtherEarning = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("OtherEarning") ?? 0);
                    at_et = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("EarningTotal") ?? 0);
                    at_epf = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("EPF") ?? 0);
                    at_gpf = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("GPF") ?? 0);
                    //at_ada = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("ADA")??0);
                    at_itax = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("ITax") ?? 0);
                    at_ptax = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("PTax") ?? 0);
                    at_OtherDeduction = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("OtherDeduction") ?? 0);
                    at_DeductionTotal = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("DeductionTotal") ?? 0);
                    sb1.Append("<tr>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center' colspan='2'><b>| Total |</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_bs + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_da + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_hra + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_Conv + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_Ord + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_Wash + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_OtherEarning + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_et + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (ds.Tables[0].Rows[0]["EPF_No"].ToString() == "" ? at_gpf : at_epf) + "</b></td>");
                    //sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_ada + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_itax + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_ptax + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_OtherDeduction + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (ds.Tables[0].Rows[0]["EPF_No"].ToString() == "" ? (at_DeductionTotal + at_gpf) : (at_DeductionTotal + at_epf)) + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (ds.Tables[0].Rows[0]["EPF_No"].ToString() == "" ? (at_et - (at_DeductionTotal + at_gpf)) : (at_et-(at_DeductionTotal + at_epf))) + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'></td>");
                    sb1.Append("</tr>");
                }
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center' colspan='2'><b>| Grand Total |</b></td>");

                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (bs + at_bs) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (da + at_da) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (hra + at_hra) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (Conv + at_Conv) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (Ord + at_Ord) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (Wash + at_Wash) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (OtherEarning + at_OtherEarning) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (et + at_et) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (ds.Tables[0].Rows[0]["EPF_No"].ToString() == "" ? (gpf + at_gpf) : (epf + at_epf))  + "</b></td>");
                //sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (ada+at_ada) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (itax + at_itax) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (ptax + at_ptax) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (OtherDeduction + at_OtherDeduction) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (ds.Tables[0].Rows[0]["EPF_No"].ToString() == "" ? (DeductionTotal + at_DeductionTotal + gpf + at_gpf) : (DeductionTotal + at_DeductionTotal + epf+ at_epf)) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (ds.Tables[0].Rows[0]["EPF_No"].ToString() == "" ? ((et + at_et)-(DeductionTotal + at_DeductionTotal + gpf + at_gpf)) : ((et + at_et)-(DeductionTotal + at_DeductionTotal + epf + at_epf))) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                div_page_content.InnerHtml = sb1.ToString();
                Print.InnerHtml = sb1.ToString();
            }
            else
            {
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                btnPrint.Visible = false;
                btnExcel.Visible = false;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "No Record Found.");
            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
    }
}