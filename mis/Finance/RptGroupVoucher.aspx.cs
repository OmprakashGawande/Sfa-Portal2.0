using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Finance_RptGroupVoucher : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
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
                    ddlOffice.Enabled = false;
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    string FY = GetCurrentFinancialYear();
                    string[] YEAR = FY.Split('-');
                    DateTime FromDate = new DateTime(int.Parse(YEAR[0]), 4, 1);
                    txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                    txtToDate.Attributes.Add("readonly", "readonly");


                    FillVoucherDate();
                    FillDropdown();
                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        ddlOffice.Enabled = false;
                    }

                    DivDayBookTotal.Visible = false;
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
            if (ViewState["Office_ID"].ToString() == "1")
            {
                ddlOffice.Enabled = true;
            }
            ds = objdb.ByProcedure("SpFinRptGroupVoucher",
                   new string[] { "flag" },
                   new string[] { "0" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
               // ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }

            DataSet ds1 = objdb.ByProcedure("SpFinRptGroupVoucher", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                ddlGroup.DataSource = ds1;
                ddlGroup.DataTextField = "Head_Name";
                ddlGroup.DataValueField = "Head_ID";
                ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, new ListItem("Select", "0"));

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillVoucherDate()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtToDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    public static string GetCurrentFinancialYear()
    {
        int CurrentYear = DateTime.Today.Year;
        int PreviousYear = DateTime.Today.Year - 1;
        int NextYear = DateTime.Today.Year + 1;
        string PreYear = PreviousYear.ToString();
        string NexYear = NextYear.ToString();
        string CurYear = CurrentYear.ToString();
        string FinYear = null;

        if (DateTime.Today.Month > 3)
            FinYear = CurYear + "-" + NexYear;
        else
            FinYear = PreYear + "-" + CurYear;
        return FinYear.Trim();
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblGrid.Text = "";
            DivDayBookTotal.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            ddlGroup.DataSource = null;
            ddlGroup.DataBind();
            lblOpeningBal.Text = "";
            lblCurrentBal.Text = "";
            lblClosingBal.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGridNext(string Head_ID)
    {
        try
        {
            DivDayBookTotal.Visible = false;
            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpFinRptGroupVoucher", new string[] { "flag", "Office_ID_Mlt", "Head_ID", "FromDate", "ToDate" }, new string[] { "2", Office, Head_ID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();

                DivDayBookTotal.Visible = true;
                decimal DebitAmt = 0;
                decimal CreditAmt = 0;

                decimal OpeningBal = 0;

                if (ds.Tables[1].Rows[0]["OpeningBalance"].ToString() != "")
                    OpeningBal = decimal.Parse(ds.Tables[1].Rows[0]["OpeningBalance"].ToString());

                decimal CurrentBal = 0;

                int rowcount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < rowcount; i++)
                {
                    if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                        DebitAmt = DebitAmt + decimal.Parse(ds.Tables[0].Rows[i]["DebitAmt"].ToString());

                    if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                        CreditAmt = CreditAmt + decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());


                    string DebitAmt1 = "0";
                    decimal CreditAmt1 = 0;



                    if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                        DebitAmt1 = "-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString();

                    if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                        CreditAmt1 = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());

                    CurrentBal = CurrentBal + decimal.Parse(DebitAmt1) + CreditAmt1;
                }


                if (OpeningBal < 0)
                    lblOpeningBal.Text = Math.Abs(OpeningBal).ToString() + " Dr";
                else
                    lblOpeningBal.Text = OpeningBal.ToString() + " Cr";

                if (CurrentBal < 0)
                    lblCurrentBal.Text = Math.Abs(CurrentBal).ToString() + " Dr";
                else
                    lblCurrentBal.Text = CurrentBal.ToString() + " Cr";


                if ((OpeningBal + CurrentBal) < 0)
                    lblClosingBal.Text = (Math.Abs(OpeningBal + CurrentBal)).ToString() + " Dr";
                else
                    lblClosingBal.Text = (Math.Abs(OpeningBal + CurrentBal)).ToString() + " Cr";




                GridView1.FooterRow.Style.Add("font-weight", "700");
                GridView1.FooterRow.Cells[2].Text = "Grand Total";

                GridView1.FooterRow.Cells[5].Text = DebitAmt.ToString();
                GridView1.FooterRow.Cells[5].CssClass = "align-right";
                GridView1.FooterRow.Cells[6].Text = CreditAmt.ToString();
                GridView1.FooterRow.Cells[6].CssClass = "align-right";
            }
            else
            {
                lblGrid.Text = "No Record Found...";
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
            string VoucherTx_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();

            objdb.ByProcedure("SpFinVoucherTx",
                   new string[] { "flag", "VoucherTx_ID", "Emp_ID" },
                   new string[] { "12", VoucherTx_ID, ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Deleted.");
            FillGridNext(ddlGroup.SelectedValue.ToString());

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            GridView1.DataSource = null;
            GridView1.DataBind();
            ddlGroup.ClearSelection();
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
            lblGrid.Text = "";
            lblOpeningBal.Text = "";
            lblCurrentBal.Text = "";
            lblClosingBal.Text = "";
            DivDayBookTotal.Visible = false;
            if (ddlGroup.SelectedIndex > 0)
            {
                FillGridNext(ddlGroup.SelectedValue.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
   
}