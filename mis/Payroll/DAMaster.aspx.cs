using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;

public partial class mis_Payroll_DAMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    APIProcedure apiproc = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                try
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();

                    FillDropdown();
                    FillGrid();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/mis/Login.aspx");
                }

            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillDropdown()
    {
        try
        {

            ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "8" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlFinancialYear.DataSource = ds;
                ddlFinancialYear.DataTextField = "Year";
                ddlFinancialYear.DataValueField = "Year";
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
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
            string Msg = "";
            if(ddlFinancialYear.SelectedIndex == 0)
            {
                Msg = "Select Financial Year.//n";
            }
            if (ddlMonth.SelectedIndex == 0)
            {
                Msg = "Select Month.//n";
            }
            if (ddlEmp_TypeOfPost.SelectedIndex == 0)
            {
                Msg = "Select Type of Post.//n";
            }
            if (txtDARate.Text == "")
            {
                Msg = "Enter DA Rate.//n";
            }
            if(Msg =="")
            {
                ds = objdb.ByProcedure("Sp_DARateMaster",
                           new string[] { "flag", 
                                          "FinancialYear", 
                                          "Month", 
                                          "MonthNo",
                                          "TypeofPost", 
                                          "DARate", 
                                          "IsActive", 
                                          "CreatedBy", 
                                          "CreatedByIP" 
                                        },
                           new string[] { "1",
                                          ddlFinancialYear.SelectedValue.ToString(), 
                                          ddlMonth.SelectedItem.Text, 
                                          ddlMonth.SelectedValue.ToString(), 
                                          ddlEmp_TypeOfPost.SelectedValue.ToString(), 
                                          txtDARate.Text, 
                                          "1", 
                                          ViewState["Emp_ID"].ToString(), 
                                          apiproc.GetLocalIPAddress()
                                        }, "dataset");
                if(ds != null && ds.Tables.Count > 0)
                {
                    if(ds.Tables[0].Rows.Count > 0)
                    {
                       if(ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                       {
                           lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You !", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                       }
                       else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                       {
                           lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning !", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                       }
                       else
                       {
                           lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                       }

                    }
                }
                ddlFinancialYear.ClearSelection();
                ddlMonth.ClearSelection();
                ddlEmp_TypeOfPost.ClearSelection();
                txtDARate.Text = "";
                FillGrid();



            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + Msg + "')", true);
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
            GvDetail.DataSource = string.Empty;
            GvDetail.DataBind();
            ds = objdb.ByProcedure("Sp_DARateMaster", new string[] {"flag" }, new string[] {"2"}, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    GvDetail.DataSource = ds;
                    GvDetail.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}