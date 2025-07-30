using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class mis_E_Invoice_BRptFeedback_DetailOffice : System.Web.UI.Page
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
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    string FY = GetCurrentFinancialYear();
                    string[] YEAR = FY.Split('-');
                    DateTime FromDate = new DateTime(int.Parse(YEAR[0]), 4, 1);
                    DateTime ToDate = new DateTime(int.Parse(YEAR[1]), 3, 31);
                    txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                    txtToDate.Text = ToDate.ToString("dd/MM/yyyy");

                    txtToDate.Attributes.Add("readonly", "readonly");

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
            string sDate = (Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd")).ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));


            //  GridView1.DataSource = new string[] { };

            //if (ddlOffice.SelectedIndex > 0)
            //{
            ds = objdb.ByProcedure("SpFinE_Invoice", new string[] { "flag", "FromDate", "ToDate", "Office_ID" }
                , new string[] { "11", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;

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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            lblMsg.Text = "";
            FillGrid();
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
         



            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView1.Rows[index - 1];
            HiddenField HF_E_ID = (HiddenField)row.Cells[0].FindControl("HF_E_ID");
            HiddenField HF_VoucherTx_ID = (HiddenField)row.Cells[0].FindControl("HF_VoucherTx_ID");
            Label lblVoucherTx_No = (Label)row.Cells[0].FindControl("lblVoucherTx_No");


            ViewState["E_ID"] = HF_E_ID.Value;
            ViewState["VoucherTx_ID"] = HF_VoucherTx_ID.Value;

            if (e.CommandName == "View")
            {
                lblVoucherView.Text = "";
                lblVoucherView.Text = "Voucher No. : " + lblVoucherTx_No.Text;

                HL_E_ExcelDoc.NavigateUrl = "";
                HL_E_JsonDoc.NavigateUrl = "";
                Repeater1.DataSource = null;
                Repeater1.DataBind();

                ds = objdb.ByProcedure("SpFinE_Invoice", new string[] { "flag", "E_ID", "VoucherTx_ID" }
               , new string[] { "8", ViewState["E_ID"].ToString(), ViewState["VoucherTx_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    txtAckNoView.Text = ds.Tables[0].Rows[0]["AckNo"].ToString();
                    txtIRNView.Text = ds.Tables[0].Rows[0]["IRN"].ToString();
                    HL_E_ExcelDoc.NavigateUrl = "Upload_Doc/" + ds.Tables[0].Rows[0]["E_ExcelDoc"].ToString();
                    HL_E_JsonDoc.NavigateUrl = "Upload_Doc/" + ds.Tables[0].Rows[0]["E_JsonDoc"].ToString();

                    Repeater1.DataSource = ds;
                    Repeater1.DataBind();
                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowFeedbackModal();", true);
            }
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
}