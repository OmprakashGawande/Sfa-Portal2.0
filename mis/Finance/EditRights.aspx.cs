using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Finance_EditRights : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    int ColCount = 11;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
              
                btnSave.Visible = false;
              
                FillDropdown();

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
           
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpFinEditRight", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Financial_Year";
                ddlYear.DataValueField = "Financial_Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
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
            ds = objdb.ByProcedure("SpFinEditRight", new string[] { "flag", "FinancialYear" }, new string[] { "1", ddlYear.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
               
               
                btnSave.Visible = true;

            }
            else
            {
                lblMsg.Text = "";
                GridView1.DataSource = null;
                GridView1.DataBind();
               
                btnSave.Visible = false;
            }
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
            lblMsg.Text = "";
            if (ddlYear.SelectedIndex > 0 )
            {
                FillGrid();

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
            string msg = "";
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year.\\n";
            }
            
            if (msg == "")
            {
                //StringBuilder sbSet_Attendance = new StringBuilder();
                string Year = ddlYear.SelectedValue.ToString();
               
                
                string LoginUserID = ViewState["Emp_ID"].ToString();
              
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    Label lblGenerateStatus = (Label)gr.FindControl("lblGenerateStatus");
                    TextBox txtValidDays = (TextBox)gr.FindControl("txtValidDays");
                    
                    string  AuditStatus ="No";
                    string ValidDays = "0";
                    if (lblGenerateStatus.Text == "No")
                    {
                        Label Office_ID = (Label)gr.FindControl("lblOffice_ID");
                        if (txtValidDays.Text == "")
                        {
                            ValidDays = "0";
                        }
                        else
                        {
                            ValidDays = txtValidDays.Text;
                        }
                        if(chkSelect.Checked == true)
                        {
                            AuditStatus = "Yes";
                            ValidDays = "0";
                        }

                        objdb.ByProcedure("SpFinEditRight",
                        new string[] { "flag", "Office_ID", "ValidDays", "FinancialYear", "Audit", "UpdatedBy" },
                        new string[] { "0", Office_ID.Text,ValidDays, Year, AuditStatus.ToString(), LoginUserID }, "dataset");
                    }
                }
              
                FillGrid();
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");

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
   
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        try
        {
           


            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

            TableHeaderCell cell = new TableHeaderCell();
            cell.Text = "";
            cell.ColumnSpan = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "";
            cell.ColumnSpan = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "";
            cell.ColumnSpan = 1;
            row.Controls.Add(cell);

            //cell = new TableHeaderCell();
            //cell.Text = "Current Month";
            //cell.ColumnSpan = 20;
            //row.Controls.Add(cell);

            //cell = new TableHeaderCell();
            //cell.Text = "Previous Month";
            //cell.ColumnSpan = ColCount;
            //row.Controls.Add(cell);

            //cell = new TableHeaderCell();
            //cell.Text = "";
            //cell.ColumnSpan = 1;
            //row.Controls.Add(cell);





            GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            lblMsg.Text = "";
        }
    }
    /*******New PP**********/

}