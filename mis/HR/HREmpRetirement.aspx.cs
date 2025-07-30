using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HREmpRetirement : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != "" && Session["Emp_ID"] != null)
                {
					int ToDay = DateTime.Now.Day;
                    if (ToDay < 5)
                    {
                        btnSave.Visible = false;
                    }
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    if (ViewState["Office_ID"].ToString() == "1")
                    {
                        //ddlOldOffice.Attributes.Add("readonly", "readonly");
                        ddlOffice.Enabled = true;
                    }

                    FillDropdown();
                    FillEmployee();
                    //FillGrid();
                }
                else
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
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
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
            }
            ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEmployee()
    {
        try
        {
            //lblMsg.Text = "";
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "18", ddlOffice.SelectedValue }, "dataset");
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

    protected void ClearText()
    {
        try
        {
            ddlEmployee.ClearSelection();
            ddlSeparation_Type.ClearSelection();
            txtRetired_On.Text = "";
            txtOrder_Date.Text = "";
            txtOrder_No.Text = "";
            txtRemark.Text = "";
            txtoldretirementdate.Text = "";
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
            string Document = "";
            string Retired_On = "";
            string Order_Date = "";
            if (ddlOffice.SelectedIndex == 0)
            {
                msg += "Select Office. \\n ";
            }
            if (ddlEmployee.SelectedIndex == 0)
            {
                msg += "Select Employee. \\n ";
            }
            if (txtoldretirementdate.Text == "")
            {
                msg += "Enter Old Retirement Date. \\n ";
            }
            if (ddlSeparation_Type.SelectedIndex == 0)
            {
                msg += "Select Separation Type. \\n ";
            }
            if (txtRetired_On.Text.Trim() == "")
            {
                msg += "Enter Separated/RetiredOn Date. \\n ";
            }
            if (txtOrder_No.Text == "")
            {
                msg += "Enter Order No. \\n ";
            }
            if (txtOrder_Date.Text == "")
            {
                msg += "Enter OrderDate. \\n ";
            }
            if (txtRemark.Text == "")
            {
                msg += "Enter Remark. \\n ";
            }

            if (txtRetired_On.Text != "")
            {
                Retired_On = Convert.ToDateTime(txtRetired_On.Text, cult).ToString("yyyy/MM/dd");
            }
            else
            {
                Retired_On = null;
            }
            if (txtOrder_Date.Text != "")
            {
                Order_Date = Convert.ToDateTime(txtOrder_Date.Text, cult).ToString("yyyy/MM/dd");
            }
            else
            {
                Order_Date = null;
            }
            if (FileUpload1.HasFile)
            {
                Document = "UploadDoc/" + Guid.NewGuid() + "-" + FileUpload1.FileName;
                FileUpload1.PostedFile.SaveAs(Server.MapPath(Document));
            }
            if (msg == "")
            {

                ds = objdb.ByProcedure("SpHREmpRetirement", new string[] { "flag", "Office_ID", "Emp_ID", "Old_RetirementDate", "Separation_Type", "Retired_On", "Order_No", "Order_Date", "Documents", "Remark", "Status", "Updated_By" }, new string[] { "0", ddlOffice.SelectedValue, ddlEmployee.SelectedValue, Convert.ToDateTime(txtoldretirementdate.Text, cult).ToString("yyyy/MM/dd"), ddlSeparation_Type.Text, Retired_On, txtOrder_No.Text, Order_Date, Document, txtRemark.Text, "Done", ViewState["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows[0]["Status"].ToString() == "True")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillEmployee();
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Record not saved Successfully");
                    ClearText();
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

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlEmployee.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpHREmpRetirement", new string[] { "flag", "Emp_ID" }, new string[] { "5", ddlEmployee.SelectedValue }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtoldretirementdate.Text = ds.Tables[0].Rows[0]["Emp_RetirementDate"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee();
    }
}