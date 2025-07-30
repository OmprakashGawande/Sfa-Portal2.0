using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollAccountInfo : System.Web.UI.Page
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
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillGrid();
                    FillOfficeName();
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
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        txtEPFNo.Text = "";
        txtEDLIId.Text = "";
        txtEGLSId.Text = "";
        txtGratuityID.Text = "";
        txtGroupInsurance.Text = "";
        txtLeaveEncashmentID.Text = "";
        txtUANNo.Text = "";
    }
    protected void FillOfficeName()
    {
        try
        {
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
    protected void FillGrid()
    {
        try
        {
            ds = objdb.ByProcedure("SpPayrollAccountInfo", new string[] { "flag", "Office_ID" }, new string[] { "0", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                GetDatatableHeaderDesign();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
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
            string msg = "";
            //ds = objdb.ByProcedure("SpPayrollAccountInfo", new string[] { "flag", "Emp_ID", "Office_ID", "EPF_No", "UAN_No", "GroupInsurance_No", "Gratuity_ID", "EGLS_ID", "EDLI_ID", "LeaveEncashment_ID", "Account_UpdatedBy" },
            //new string[] { "3", ViewState["EMPID"].ToString(), ViewState["Office_ID"].ToString(), txtEPFNo.Text, txtUANNo.Text, txtGroupInsurance.Text, txtGratuityID.Text, txtEGLSId.Text, txtEDLIId.Text, txtLeaveEncashmentID.Text, ViewState["Emp_ID"].ToString() }, "dataset");
            //if (ds.Tables[0].Rows.Count != 0)
            //{
            //    msg = msg + "EPF NO is Already Exist.\\n";
            //}
            //if (ds.Tables[1].Rows.Count != 0)
            //{
            //    msg = msg + "UAN NO is Already Exist.\\n";
            //}
            //if (ds.Tables[2].Rows.Count != 0)
            //{
            //    msg = msg + "Group Insurance No is Already Exist.\\n";
            //}
            //if (ds.Tables[3].Rows.Count != 0)
            //{
            //    msg = msg + "Gratuity ID is Already Exist.\\n";
            //}
            //if (ds.Tables[4].Rows.Count != 0)
            //{
            //    msg = msg + "EGLS ID is Already Exist.\\n";
            //}
            //if (ds.Tables[5].Rows.Count != 0)
            //{
            //    msg = msg + "EDLI ID is Already Exist.\\n";
            //}
            //if (ds.Tables[6].Rows.Count != 0)
            //{
            //    msg = msg + "Leave Encashment ID is Already Exist.\\n";
            //}
            //if(msg == "")
            //{
                objdb.ByProcedure("SpPayrollAccountInfo", new string[] { "flag", "Emp_ID", "Office_ID", "EPF_No", "UAN_No", "GroupInsurance_No", "Gratuity_ID", "EGLS_ID", "EDLI_ID", "LeaveEncashment_ID", "Account_UpdatedBy" },
                new string[] { "2", ViewState["EMPID"].ToString(), ViewState["Office_ID"].ToString(), txtEPFNo.Text, txtUANNo.Text, txtGroupInsurance.Text, txtGratuityID.Text, txtEGLSId.Text, txtEDLIId.Text, txtLeaveEncashmentID.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                ClearText();
                FillGrid();
            //}
            //else
            //{   
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('"+msg+"');", true);
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ClearText();
			/***/
            int errorCounter_epf = 0;
            int errorCounter_uan = 0;
            /***/
            ViewState["EMPID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpPayrollAccountInfo", new string[] { "flag", "Emp_ID" },
            new string[] { "1", ViewState["EMPID"].ToString() }, "dataset");
            if(ds.Tables[0].Rows.Count != 0)
            {
				/***/
                txtEPFNo.Text = ds.Tables[0].Rows[0]["EPF_No"].ToString();
                txtUANNo.Text = ds.Tables[0].Rows[0]["UAN_No"].ToString();
                if (Session["Office_ID"].ToString() != "1")
                {
                    errorCounter_epf = Regex.Matches(txtEPFNo.Text, @"[a-zA-Z]").Count;
                    if (errorCounter_epf == 0 && txtEPFNo.Text != "")
                    {
                        if (Int32.Parse(txtEPFNo.Text) > 0)
                        {
                            txtEPFNo.Enabled = false;
                        }
                    }
                    errorCounter_uan = Regex.Matches(txtUANNo.Text, @"[a-zA-Z]").Count;
                    if (errorCounter_uan == 0 && txtUANNo.Text != "")
                    {
                        txtUANNo.Enabled = false;
                    }
                }
                /***/
				
                //txtEPFNo.Text = ds.Tables[0].Rows[0]["EPF_No"].ToString();
                //txtUANNo.Text = ds.Tables[0].Rows[0]["UAN_No"].ToString();
                txtGroupInsurance.Text = ds.Tables[0].Rows[0]["GroupInsurance_No"].ToString();
                txtGratuityID.Text = ds.Tables[0].Rows[0]["Gratuity_ID"].ToString();
                txtEGLSId.Text = ds.Tables[0].Rows[0]["EGLS_ID"].ToString();
                txtEDLIId.Text = ds.Tables[0].Rows[0]["EDLI_ID"].ToString();
                txtLeaveEncashmentID.Text = ds.Tables[0].Rows[0]["LeaveEncashment_ID"].ToString();
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {

        ViewState["Office_ID"] = ddlOfficeName.SelectedItem.Value;
        FillGrid();
        FillOfficeName();

    }
}