using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_Finance_FinSupplierMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Supplier_ID"] = "0";
                FillDropdown();
                FillGrid();
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
            ds = objdb.ByProcedure("SpFinSupplier",
                new string[] { "flag" },
                new string[] { "6" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlState.DataSource = ds;
                ddlState.DataTextField = "GST_StateName";
                ddlState.DataValueField = "GST_StateCode";
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("Select", "0"));
                
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
            if (txtNameOfTheBank.Text == "")
            {
                msg = msg + "Enter Name Of The Bank. \\n";
            }
            if (txtNameOfTheBranch.Text == "")
            {
                msg = msg + "Enter Name Of The Branch. \\n";
            }
            if (txtNameOfAccountHolder.Text == "")
            {
                msg = msg + "Enter Name Of Account Holder. \\n";
            }
            if (txtAcNoAsOnChkBook.Text == "")
            {
                msg = msg + "Enter Ac. No. As On Chk. Book. \\n";
            }
            if (txtIFSC.Text == "")
            {
                msg = msg + "Enter IFSC. \\n";
            }
            //if (txtBankAddress.Text == "")
            //{
            //    msg = msg + "Enter Bank Address. \\n";
            //}
            if (txtAddress.Text == "")
            {
                msg = msg + "Enter Supplier Address. \\n";
            }

            if (ddlState.SelectedIndex <= 0)
            {
                msg = msg + "Enter State. \\n";
            }
           
            if (txtGSTNo.Text == "")
            {
                msg = msg + "Enter GST No. \\n";
            }
            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpFinSupplier", new string[] { "flag", "NameOfAccountHolder", "ID" }, new string[] { "5", txtNameOfAccountHolder.Text, ViewState["Supplier_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }

                if (btnSave.Text == "Save" && ViewState["Supplier_ID"].ToString() == "0" && Status == 0)
                {
                    ds = objdb.ByProcedure("SpFinSupplier",
                    new string[] { "flag", "NameOfTheBank", "NameOfTheBranch", "NameOfAccountHolder", "AcNoAsOnChkBook", "IFSC", "Address"
                        , "BankAddress", "StateID", "StateName", "EmailID", "MobileNo", "GSTNo", "PANNo", "UpdatedBy" },
                    new string[] { "0", txtNameOfTheBank.Text, txtNameOfTheBranch.Text, txtNameOfAccountHolder.Text, txtAcNoAsOnChkBook.Text, txtIFSC.Text, txtAddress.Text
                        , txtBankAddress.Text, ddlState.SelectedValue.ToString(), ddlState.SelectedItem.ToString(), txtEmailID.Text, txtMobileNo.Text, txtGSTNo.Text, txtPANNo.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Data Successfully Saved.");
                    ClearText();
                    FillGrid();
                }
                else if (btnSave.Text == "Update" && ViewState["Supplier_ID"].ToString() != "0" && Status == 0)
                {
                    objdb.ByProcedure("SpFinSupplier",
                     new string[] { "flag", "ID", "NameOfTheBank", "NameOfTheBranch", "NameOfAccountHolder", "AcNoAsOnChkBook", "IFSC", "Address", "BankAddress", "StateID", "StateName", "EmailID", "MobileNo", "GSTNo", "PANNo", "UpdatedBy" },
                     new string[] { "3", ViewState["Supplier_ID"].ToString(), txtNameOfTheBank.Text, txtNameOfTheBranch.Text, txtNameOfAccountHolder.Text, txtAcNoAsOnChkBook.Text, txtIFSC.Text, txtAddress.Text, txtBankAddress.Text, ddlState.SelectedValue.ToString(), ddlState.SelectedItem.ToString(), txtEmailID.Text, txtMobileNo.Text, txtGSTNo.Text, txtPANNo.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Data Successfully Updated");
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Name Of Account Holder already exist.');", true);
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
            GridView1.DataSource = null;
            GridView1.DataBind();

            ds = objdb.ByProcedure("SpFinSupplier",
                new string[] { "flag" },
                new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Supplier_ID"] = GridView1.SelectedValue.ToString();
        lblMsg.Text = "";
        ds = objdb.ByProcedure("SpFinSupplier",
                   new string[] { "flag", "ID" },
                   new string[] { "4", ViewState["Supplier_ID"].ToString() }, "dataset");

        if (ds.Tables.Count > 0)
        {
            //   txtState_Name.Text = ds.Tables[0].Rows[0]["State_Name"].ToString();
            txtNameOfTheBank.Text = ds.Tables[0].Rows[0]["NameOfTheBank"].ToString();
            txtNameOfTheBranch.Text = ds.Tables[0].Rows[0]["NameOfTheBranch"].ToString();
            txtNameOfAccountHolder.Text = ds.Tables[0].Rows[0]["NameOfAccountHolder"].ToString();
            txtAcNoAsOnChkBook.Text = ds.Tables[0].Rows[0]["AcNoAsOnChkBook"].ToString();
            txtIFSC.Text = ds.Tables[0].Rows[0]["IFSC"].ToString();
            txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();

            txtBankAddress.Text = ds.Tables[0].Rows[0]["BankAddress"].ToString();
            ddlState.ClearSelection();
            ddlState.Items.FindByValue(ds.Tables[0].Rows[0]["StateID"].ToString()).Selected = true;
           
            txtEmailID.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
            txtMobileNo.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
            txtGSTNo.Text = ds.Tables[0].Rows[0]["GSTNo"].ToString();
            txtPANNo.Text = ds.Tables[0].Rows[0]["PANNo"].ToString();

            btnSave.Text = "Update";
        }

    }


    protected void ClearText()
    {
        txtNameOfTheBank.Text = "";
        txtNameOfTheBranch.Text = "";
        txtNameOfAccountHolder.Text = "";
        txtAcNoAsOnChkBook.Text = "";
        txtIFSC.Text = "";
        txtAddress.Text = "";

        txtBankAddress.Text = "";
        ddlState.ClearSelection();
        txtEmailID.Text = "";
        txtMobileNo.Text = "";
        txtGSTNo.Text = "";
        txtPANNo.Text = "";


        ViewState["Supplier_ID"] = "0";
        btnSave.Text = "Save";
    }
}