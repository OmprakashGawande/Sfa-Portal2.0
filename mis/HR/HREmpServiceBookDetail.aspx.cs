using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HREmpServiceBookDetail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Request.QueryString["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["NewEmp_ID"] = objdb.Decrypt(Request.QueryString["Emp_ID"].ToString());
                    if (ViewState["NewEmp_ID"] != null)
                    {
                        FillGrid();
                        FillPersonalDetail();
                    }
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
    protected void FillGrid()
    {
        try
        {

            ds = objdb.ByProcedure("SpHREmpServiceBook", new string[] { "flag", "Emp_ID" }, new string[] { "1", ViewState["NewEmp_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillPersonalDetail()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Emp_ID" }, new string[] { "24", ViewState["NewEmp_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DVPersonalDetail.DataSource = ds.Tables[0];
                DVPersonalDetail.DataBind();

                DVOfficialDetail.DataSource = ds.Tables[0];
                DVOfficialDetail.DataBind();
            }
            else
            {
                DVPersonalDetail.DataSource = null;
                DVPersonalDetail.DataBind();

                DVOfficialDetail.DataSource = null;
                DVOfficialDetail.DataBind();
            }
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                divBankDetail.Visible = true;
                GVBankDetail.DataSource = ds.Tables[1];
                GVBankDetail.DataBind();
            }
            else
            {
                divBankDetail.Visible = false;
                GVBankDetail.DataSource = null;
                GVBankDetail.DataBind();
            }
            if (ds != null && ds.Tables[2].Rows.Count > 0)
            {
                divChildDetail.Visible = true;
                GVChildDetail.DataSource = ds.Tables[2];
                GVChildDetail.DataBind();
            }
            else
            {
                divChildDetail.Visible = false;
                GVChildDetail.DataSource = null;
                GVChildDetail.DataBind();
            }
            if (ds != null && ds.Tables[3].Rows.Count > 0)
            {
                divFixedAssetsDetail.Visible = true;
                GVFixedAssetsDetail.DataSource = ds.Tables[3];
                GVFixedAssetsDetail.DataBind();
            }
            else
            {
                divFixedAssetsDetail.Visible = false;
                GVFixedAssetsDetail.DataSource = null;
                GVFixedAssetsDetail.DataBind();
            }
            if (ds != null && ds.Tables[4].Rows.Count > 0)
            {
                divNomineeDetail.Visible = true;
                GVNomineeDetail.DataSource = ds.Tables[4];
                GVNomineeDetail.DataBind();
            }
            else
            {
                divNomineeDetail.Visible = false;
                GVNomineeDetail.DataSource = null;
                GVNomineeDetail.DataBind();
            }
            if (ds != null && ds.Tables[5].Rows.Count > 0)
            {
                divOtherDetail.Visible = true;
                GVOtherDetail.DataSource = ds.Tables[5];
                GVOtherDetail.DataBind();
            }
            else
            {
                divOtherDetail.Visible = false;
                GVOtherDetail.DataSource = null;
                GVOtherDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}