using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollTaxAssesmentForm : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

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
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            string selectedEmpDetail = "Advance Taxation Details for Financial Year  " + ddlYear.SelectedItem.Text + " , Officer/ Employee Name: " + ddlEmployee.SelectedItem.Text + "  , Office: " + ddlOfficeName.SelectedItem.Text;
            lblEmpDetail.Text = selectedEmpDetail.ToString();
            ds = objdb.ByProcedure("SpPayrollSalaryAdvanceTax_FY", new string[] { "flag", "Year", "Emp_ID" }, new string[] { "0", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {

            }
        }
        catch (Exception ex)
        {

        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillForm();
    }

    public void FillForm()
    {

        try
        {
            lblEmpName.Text = "-";
            lblDesignation.Text = "-";
            lblPostingPlace.Text = "-";
            lblPanNo.Text = "-";
            lblAccountingYear.Text = "-";
            lblAssessmentYear.Text = "-";
            lblGrossSalary.Text = "0";
            txtbox80c.Text = "0";
            lblbox80g.Text = "0";
            txtOtherIncome.Text = "0";
            txtHouseProperty.Text = "0";//
            txtMedicalReimbur.Text = "0";//

            txtbox24.Text = "0";
            txtbox80dd.Text = "0";
            txtbox80d.Text = "0";
            txtcovid.Text = "0";
            txt80tta.Text = "0";//
            txt80ttb.Text = "0";//
            txthrrebate.Text = "0";//

            lblProfessionalTax.Text = "0";
            lblStandardDeduction.Text = "50000";
            lblTaxableIncome.Text = "0";
            lblTotalPayableTax.Text = "0";
            float professionalTax = 0;
            lblEmployeeType.Text = "";

            ds = objdb.ByProcedure("SpPayrollTaxAssesment", new string[] { "flag", "Year", "Emp_ID" }, new string[] { "0", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                dvAssesmentForm.Visible = true;
                lblEmpName.Text = ds.Tables[0].Rows[0]["Emp_Name"].ToString();
                lblDesignation.Text = ds.Tables[0].Rows[0]["Designation_Name"].ToString();
                lblPostingPlace.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                lblPanNo.Text = ds.Tables[0].Rows[0]["Emp_PanCardNo"].ToString();
                int selectedYear = int.Parse(ddlYear.SelectedValue.ToString());
                lblAccountingYear.Text = selectedYear.ToString() + "-" + (selectedYear + 1).ToString();
                lblAssessmentYear.Text = (selectedYear + 1).ToString() + "-" + (selectedYear + 2).ToString();

                if (ds.Tables[0].Rows[0]["CitizenType"].ToString() == "SeniorEmployee")
                {
                    lblEmployeeType.Text = "Senior Employee as per the DOB in ERP";
                    hfEmployeeType.Value = "SeniorEmployee";
                }
                else
                {
                    lblEmployeeType.Text = "Normal Employee as per the DOB in ERP";
                    hfEmployeeType.Value = "NormalEmployee";
                }



            }

            if (ds.Tables[1].Rows.Count != 0)
            {
                lblGrossSalary.Text = ds.Tables[1].Rows[0]["GrossSalary"].ToString();
                if (float.Parse(ds.Tables[1].Rows[0]["ProfessionalTax"].ToString()) > 2500)
                {
                    lblProfessionalTax.Text = "2500";
                    professionalTax = 2500;

                }
                else
                {
                    lblProfessionalTax.Text = ds.Tables[1].Rows[0]["ProfessionalTax"].ToString();
                    professionalTax = float.Parse(ds.Tables[1].Rows[0]["ProfessionalTax"].ToString());
                }

                txtbox80c.Text = ds.Tables[1].Rows[0]["Deduction80C"].ToString();

                lblStandardDeduction.Text = "50000";
                lblTaxableIncome.Text = ((float.Parse(ds.Tables[1].Rows[0]["GrossSalary"].ToString())) - ((50000) + (professionalTax) + (float.Parse(ds.Tables[1].Rows[0]["Deduction80C"].ToString())))).ToString();
                lblTotalPayableTax.Text = "<span style='color:red'>Please click on calculate button.</span>";

           
            }

        }
        catch (Exception ex)
        {

        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {


            float GrossSalary = float.Parse(lblGrossSalary.Text.ToString());
            float OtherIncome = float.Parse(txtOtherIncome.Text.ToString());
            float HouseProperty = float.Parse(txtHouseProperty.Text.ToString());
            float MedicalReimbur = float.Parse(txtMedicalReimbur.Text.ToString());
            float box80c = float.Parse(txtbox80c.Text.ToString());
            float box80g = float.Parse(txtbox80dd.Text.ToString()) + float.Parse(txtbox80d.Text.ToString()) + float.Parse(txtbox24.Text.ToString()) + float.Parse(txtcovid.Text.ToString())+ float.Parse(txt80tta.Text.ToString())+ float.Parse(txt80ttb.Text.ToString()) + float.Parse(txthrrebate.Text.ToString());

            float ProfessionalTax = float.Parse(lblProfessionalTax.Text.ToString());
            float StandardDeduction = float.Parse(lblStandardDeduction.Text.ToString());
            lblbox80g.Text = box80g.ToString();


            float TotalPayableTax = 0;
            TotalPayableTax = (GrossSalary + OtherIncome+HouseProperty+MedicalReimbur) - ((ProfessionalTax) + (StandardDeduction) + (box80c) + (box80g));
            /**********/
            lblTaxableIncome.Text = TotalPayableTax.ToString();
            /*******/
            float slab1 = 250000;
            float slabrate1 = 5;

            float slab2 = 500000;
            float slabrate2 = 20;

            float slab3 = 1000000;
            float slabrate3 = 30;

            ds = objdb.ByProcedure("SpPayrollTaxAssesment", new string[] { "flag", "Year", "V_TaxType", "V_TaxFor" }, new string[] { "2", ddlYear.SelectedValue.ToString(), "ITax", hfEmployeeType.Value }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {

                slab1 = float.Parse(ds.Tables[0].Rows[0]["V_TaxSlabMin"].ToString());
                slabrate1 = float.Parse(ds.Tables[0].Rows[0]["N_TaxRate"].ToString());

                slab2 = float.Parse(ds.Tables[0].Rows[1]["V_TaxSlabMin"].ToString());
                slabrate2 = float.Parse(ds.Tables[0].Rows[1]["N_TaxRate"].ToString());

                slab3 = float.Parse(ds.Tables[0].Rows[2]["V_TaxSlabMin"].ToString());
                slabrate3 = float.Parse(ds.Tables[0].Rows[2]["N_TaxRate"].ToString());

            }


            //float TotalPayableTax = float.Parse(lblTaxableIncome.Text.ToString());

            float TotalRemainingIncome = TotalPayableTax;
            float finalPayableTax = 0;

            if (TotalRemainingIncome > slab3)
            {
                finalPayableTax += (TotalRemainingIncome - slab3) * slabrate3 / 100;
                TotalRemainingIncome = TotalRemainingIncome - (TotalRemainingIncome - slab3);
            }

            if ((TotalRemainingIncome > slab2) && (TotalRemainingIncome <= slab3))
            {
                finalPayableTax += (TotalRemainingIncome - slab2) * slabrate2 / 100;
                TotalRemainingIncome = TotalRemainingIncome - (TotalRemainingIncome - slab2);
            }

            if ((TotalRemainingIncome > slab1) && (TotalRemainingIncome <= slab2))
            {
                finalPayableTax += (TotalRemainingIncome - slab1) * slabrate1 / 100;
                TotalRemainingIncome = TotalRemainingIncome - (TotalRemainingIncome - slab1);
            }

            lblTotalPayableTax.Text = " Rs. " + finalPayableTax.ToString("F") + " /-";
            lblcess.Text = " Rs. " + (finalPayableTax * 0.04).ToString("F") + " /-";
            lbltaxwithcess.Text = " Rs. " + ((finalPayableTax * 0.04) + finalPayableTax).ToString("F") + " /-";
        }
        catch (Exception ex)
        {

        }
    }
}