using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Globalization;

public partial class mis_Finance_BankMaster : System.Web.UI.Page
{
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {
                lblMsg.Text = "";
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillGrid();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if(txtBankName.Text == "")
            {
                msg += "Enter Name of the Bank.\\n";
            }
            if (txtacntno.Text == "")
            {
                msg += "Enter A/c No.\\n";
            }
            if (txtifsccode.Text == "")
            {
                msg += "Enter IFSC Code.";
            }
            if(msg == "")
            {
                string IsActive = "1";
                if(btnSave.Text == "Accept")
                {
                    objdb.ByProcedure("SpFinBankMaster", new string[] { "flag", "Office_ID", "Bank_Name", "Acnt_No", "IFSC", "UpdatedBy", "IsActive" }, new string[] { "0", ViewState["Office_ID"].ToString(), txtBankName.Text, txtacntno.Text, txtifsccode.Text, ViewState["Office_ID"].ToString(), IsActive }, "dataset");
                    txtBankName.Text = "";
                    txtacntno.Text = "";
                    txtifsccode.Text = "";
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                    FillGrid();
                }
                else if (btnSave.Text == "Update")
                {
                    objdb.ByProcedure("SpFinBankMaster", new string[] { "flag", "ID", "Office_ID", "Bank_Name", "Acnt_No", "IFSC", "UpdatedBy" }, new string[] { "2",ViewState["ID"].ToString(), ViewState["Office_ID"].ToString(), txtBankName.Text, txtacntno.Text, txtifsccode.Text, ViewState["Office_ID"].ToString() }, "dataset");
                    txtBankName.Text = "";
                    txtacntno.Text = "";
                    txtifsccode.Text = "";
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                    FillGrid();
                }
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
    protected void FillGrid()
    {
        try
        {
            btnSave.Text = "Accept";
            btnSave.Enabled = true;
            GridView1.DataSource = null;
            ds = objdb.ByProcedure("SpFinBankMaster", new string[] { "flag", "Office_ID" }, new string[] { "3", ViewState["Office_ID"].ToString() }, "dataset");
            if(ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                btnSave.Enabled = false;
            }
            GridView1.DataBind();
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
            string ID = GridView1.SelectedDataKey.Value.ToString();
            ViewState["ID"] = ID.ToString();
            ds = objdb.ByProcedure("SpFinBankMaster", new string[] { "flag", "ID" }, new string[] { "1", ID.ToString() }, "dataset");
            if(ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtBankName.Text = ds.Tables[0].Rows[0]["Bank_Name"].ToString();
                txtacntno.Text = ds.Tables[0].Rows[0]["Acnt_No"].ToString();
                txtifsccode.Text = ds.Tables[0].Rows[0]["IFSC"].ToString();
                btnSave.Text = "Update";
                btnSave.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}