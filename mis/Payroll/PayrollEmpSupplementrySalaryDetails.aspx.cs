using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mis_Payroll_PayrollEmpSupplementrySalaryDetails : System.Web.UI.Page
{
    DataSet ds1, ds2, ds3 = new DataSet();
    APIProcedure objdb = new APIProcedure();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null)
        {
            if (!IsPostBack)
            {
                DivDetail.Visible = false;
                FillYear();
                FillOffice();
                FillMonth();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void FillYear()
    {
        try
        {
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            ds1 = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlYear.DataSource = ds1.Tables[0];
                        ddlYear.DataTextField = "Year";
                        ddlYear.DataValueField = "Year";
                        ddlYear.DataBind();
                        ddlYear.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }

    protected void FillMonth()
    {
        try
        {
            ddlMonth.Items.Clear();
            for (int i = 12; i >= 1; i--)
            {
                DateTime date = new DateTime(DateTime.Now.Year, i, 1);
                if (i <= DateTime.Now.Month)
                    ddlMonth.Items.Insert(0, new ListItem(date.ToString("MMMM"), i.ToString()));

            }
            ddlMonth.DataBind();
            ddlMonth.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            // lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    protected void FillOffice()
    {
        try
        {
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ds2 = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds2 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {

                        ddlOfficeName.DataSource = ds2.Tables[0];
                        ddlOfficeName.DataTextField = "Office_Name";
                        ddlOfficeName.DataValueField = "Office_ID";
                        ddlOfficeName.DataBind();
                        ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
            }
            ddlOfficeName.SelectedValue = objdb.Office_ID();
            if (ddlOfficeName.SelectedValue.ToString() != "1")
            {
                ddlOfficeName.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            DivDetail.Visible = false;
            lblrowcount.Text = "";
            string Office = "0";
            lblTab.Text = "";
            if (Convert.ToString(objdb.Office_ID()) != "1")
            {
                Office = Convert.ToString(objdb.Office_ID());
                ddlOfficeName.SelectedValue = objdb.Office_ID();
            }
            else
            {
                Office = ddlOfficeName.SelectedValue.ToString();
            }

            ds3 = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Year", "MonthNo", "Office_ID", "SalaryType" }, new string[] { "24", ddlYear.SelectedValue, ddlMonth.SelectedValue, Office,"" }, "dataset");
            if (ds3 != null)
            {
                if (ds3.Tables.Count > 0)
                {
                    if (ds3.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds3.Tables[0];
                        GridView1.DataBind();
                        lblrowcount.Text = "Employee Count : " + (ds3.Tables[0].Rows.Count);
                        DivDetail.Visible = true;
                    }
                }
            }
            else
            {
                lblTab.Text = "Salary Details Not Found For This Month.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Page.Validate("a");

            if (!Page.IsValid)
            {
                return;
            }
            else
            {
                try
                {
                    lblMsg.Text = "";
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    DivDetail.Visible = false;
                    if (ddlYear.SelectedIndex > 0 && ddlOfficeName.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
                    {
                        FillGrid();
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 5 : ", ex.Message.ToString());
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnGenerateSupplementry_Click(object sender, EventArgs e)
    {
        Page.Validate("a");

        if (!Page.IsValid)
        {
            return;
        }
        else
        {
            try
            {
                string flag = "0";
                lblMsg.Text = "";
                string msg = "";
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    string LoginUserID = objdb.createdBy();
                    string Office_ID = ddlOfficeName.SelectedValue.ToString();
                    foreach (GridViewRow gr in GridView1.Rows)
                    {

                        CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                        HiddenField gvhfsalaryid = (HiddenField)gr.FindControl("gvhfsalaryid");

                        if (chkSelect.Checked == true)
                        {
                            objdb.ByProcedure("SpPayrollSalaryDetail",
                            new string[] { "flag", "Salary_ID", "Salary_UpdatedBy" },
                            new string[] { "25", gvhfsalaryid.Value, Office_ID, LoginUserID }, "dataset");

                            flag = "1";
                        }
                    }

                    if (flag == "1")
                    {

                        FillGrid();

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Salary Reset Successfully.");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please Select Atleast One Employee.');", true);
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Select Employee");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }
}