using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;

public partial class mis_Payroll_RPT_PayrollGenerateXML : System.Web.UI.Page
{
     DataSet ds1, ds2, ds3 = new DataSet();
    APIProcedure objdb = new APIProcedure();
    static DataTable dt = new DataTable();
  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null)
        {
            if (!IsPostBack)
            {
                DivDetail.Visible = false;
                FillYear();
                FillOffice();
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

            ds3 = objdb.ByProcedure("SpPayrollSalaryDetailXMLData_Report", new string[] {  "Salary_Year", "Salary_MonthNo", "Office_ID"
               // , "SalaryType"
            }, new string[] {  ddlYear.SelectedValue, ddlMonth.SelectedValue, Office
               // , rbnlist.SelectedValue 
            }, "dataset");
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
                lblTab.Text = "XML Files nOt found for this Month.";
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
                    if (ddlYear.SelectedIndex > 0 && ddlOfficeName.SelectedIndex > 0)
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
    protected void DownloadFile(object sender, EventArgs e)
    {


        string filePath = (sender as LinkButton).CommandArgument;
       string filePath1 = "/mis/Payroll/UploadSalaryXML/" + filePath;
        Response.ContentType = ".xml";
         Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
        Response.TransmitFile(Server.MapPath(filePath1));
       // Response.TransmitFile(filePath1);
       // Response.WriteFile(filePath1);
        Response.End();

    }
}