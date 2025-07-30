using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollPageForDeleteArrear : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillDropdown();
                    //FillGrid();
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
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
    protected void FillGrid()
    {
        try
        {
            int allowed_days = 100;
            lblMsg.Text = "";
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpPayrollEmpArrear", new string[] { "flag","Office_ID","Emp_ID" }, new string[] { "9",ddlOfficeName.SelectedValue.ToString(),ddlEmployee.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    LinkButton DeleteButton = (LinkButton)row.FindControl("LinkButton1");
                    string EntryDate = DeleteButton.ToolTip.ToString();
                    string CurrentDate = DateTime.Now.ToString("yyyy/MM/dd");
                    DateTime d1 = DateTime.Parse(EntryDate);
                    DateTime d2 = DateTime.Parse(CurrentDate);
                    int TotalDays = Convert.ToInt32((d2 - d1).TotalDays);
                    if (ViewState["Office_ID"].ToString()=="1")
                    {
                        allowed_days=100;
                    }
                    if (TotalDays <= allowed_days)
                    {
                        DeleteButton.Visible = true;
                    }
                    else
                    {
                        DeleteButton.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string ArrearID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            ds = objdb.ByProcedure("SpPayrollEmpArrear", new string[] { "flag", "EmpArrearID" }, new string[] { "10", ArrearID }, "dataset");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Delete Successfully.');", true);
            FillGrid();

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
}