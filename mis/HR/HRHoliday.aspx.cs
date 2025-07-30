using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
public partial class mis_HR_HRHoliday : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["Page"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Holiday_ID"] = "0";
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPage"] = Session["Page"];
    }

    protected void ClearText()
    {
        ddlHoliday_Type.ClearSelection();
        txtHoliday_Name.Text = "";
        txtHoliday_Date.Text = "";
    }
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHRHoliday", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ViewState["UPage"].ToString() == Session["Page"].ToString())
        {
            try
            {
                lblMsg.Text = "";
                string msg = "";
                if (txtHoliday_Date.Text == "")
                {
                    msg += "Select Holiday Date. \\n";
                }
                if (ddlHoliday_Type.SelectedIndex == 0)
                {
                    msg += "Select Holiday Type. \\n";
                }
                if (txtHoliday_Name.Text == "")
                {
                    msg += "Enter Holiday Name. \\n";
                }
                if (msg.Trim() == "")
                {
                    ds = objdb.ByProcedure("SpHRHoliday", new string[] { "flag", "Holiday_Name", "Holiday_Date" },
                        new string[] { "5", txtHoliday_Name.Text, Convert.ToDateTime(txtHoliday_Date.Text, cult).ToString("yyyy/MM/dd"), }, "dataset");

                    if (btnSave.Text == "Save" && ds.Tables[0].Rows.Count == 0)
                    {
                        objdb.ByProcedure("SpHRHoliday",
                        new string[] { "flag", "Holiday_Type", "Holiday_Name", "Holiday_Date", "Holiday_UpdatedBy" },
                        new string[] { "0", ddlHoliday_Type.SelectedItem.Text, txtHoliday_Name.Text, Convert.ToDateTime(txtHoliday_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Emp_ID"].ToString() }, "dataset");

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        ClearText();
                        FillGrid();

                    }
                    else if (btnSave.Text == "Edit" && (ds.Tables[0].Rows.Count == 1 || ds.Tables[0].Rows.Count == 0))
                    {
                        objdb.ByProcedure("SpHRHoliday",
                        new string[] { "flag", "Holiday_ID", "Holiday_Type", "Holiday_Name", "Holiday_Date", "Holiday_UpdatedBy" },
                        new string[] { "2", ViewState["Holiday_ID"].ToString(), ddlHoliday_Type.SelectedItem.Text, txtHoliday_Name.Text, Convert.ToDateTime(txtHoliday_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Emp_ID"].ToString() }, "dataset");

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        btnSave.Text = "Save";
                        ClearText();
                        FillGrid();
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Holiday is already exist.');", true);
                        ClearText();
                    }
                    Session["Page"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
        
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ClearText();
	    GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            ViewState["Holiday_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpHRHoliday", new string[] { "flag", "Holiday_ID" }, new string[] { "4", ViewState["Holiday_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlHoliday_Type.ClearSelection();
                ddlHoliday_Type.Items.FindByValue(ds.Tables[0].Rows[0]["Holiday_Type"].ToString()).Selected = true;
                txtHoliday_Name.Text = ds.Tables[0].Rows[0]["Holiday_Name"].ToString();
                txtHoliday_Date.Text = ds.Tables[0].Rows[0]["Holiday_Date"].ToString();
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        try
        {
	    
            lblMsg.Text = "";
            string Holiday_IsActive = "0";
	    GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            string Holiday_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpHRHoliday",
                   new string[] { "flag", "Holiday_ID", "Holiday_IsActive", "Holiday_UpdatedBy" },
                   new string[] { "3", Holiday_ID, Holiday_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtHoliday_Date_TextChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
    }
        //protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        ClearText();
    //        GridView1.PageIndex = e.NewPageIndex;
    //        FillGrid();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
}