using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Finance_FinSupplierOrderAuditRequest : System.Web.UI.Page
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
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                txtDeductionAmount.Attributes.Add("readonly", "readonly");
                txtNetPayment.Attributes.Add("readonly", "readonly");

                ViewState["OrderID"] = "0";

            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
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
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                ds = objdb.ByProcedure("SpFinSupplierOrder",
                    new string[] { "flag", "FromDate", "ToDate", "Status" },
                    new string[] { "6", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ddlStatus.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }

            }
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
            ViewState["OrderID"] = GridView1.SelectedValue.ToString();

            lblMsg.Text = "";
            btnApprove.Visible = true;
            ClearText();
            ds = objdb.ByProcedure("SpFinSupplierOrder",
                   new string[] { "flag", "OrderID" },
                   new string[] { "3", ViewState["OrderID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                lblOrderNo.Text = ds.Tables[0].Rows[0]["OrderNo"].ToString();
                lblOrderDate.Text = ds.Tables[0].Rows[0]["OrderDate"].ToString();
                lblSupplierName.Text = ds.Tables[0].Rows[0]["SupplierName"].ToString();
                lblPlantName.Text = ds.Tables[0].Rows[0]["PlantName"].ToString();
                lblItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                lblUnit.Text = ds.Tables[0].Rows[0]["Unit"].ToString();
                lblItemRate.Text = ds.Tables[0].Rows[0]["ItemRate"].ToString();
                lblQuantity.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                lblCessApplicable.Text = ds.Tables[0].Rows[0]["CessApplicable"].ToString();
                lblCessRate.Text = ds.Tables[0].Rows[0]["CessRate"].ToString();
                lblGSTApplicable.Text = ds.Tables[0].Rows[0]["GSTApplicable"].ToString();
                lblGSTRate.Text = ds.Tables[0].Rows[0]["GSTRate"].ToString();
                lblGSTType.Text = ds.Tables[0].Rows[0]["GSTType"].ToString();
                lblGSTIncInDeduction.Text = ds.Tables[0].Rows[0]["GSTIncInDeduction"].ToString();
                lblQualityCleanReport.Text = ds.Tables[0].Rows[0]["QualityCleanReport"].ToString();
                lblBillNo.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();
                lblBillDate.Text = ds.Tables[0].Rows[0]["BillDate"].ToString();
                lblQuantityReceiveDate.Text = ds.Tables[0].Rows[0]["QuantityReceiveDate"].ToString();
                lblQuantityReceive.Text = ds.Tables[0].Rows[0]["QuantityReceive"].ToString();
                lblDiffQuantity.Text = ds.Tables[0].Rows[0]["DiffQuantity"].ToString();
                lblBasicRate.Text = ds.Tables[0].Rows[0]["BasicRate"].ToString();
                lblGST.Text = ds.Tables[0].Rows[0]["GST"].ToString();
                lblBasicAmount.Text = ds.Tables[0].Rows[0]["BasicAmount"].ToString();
                lblGSTAmount.Text = ds.Tables[0].Rows[0]["GSTAmount"].ToString();
                lblCess.Text = ds.Tables[0].Rows[0]["Cess"].ToString();
                lblTotalAmount.Text = ds.Tables[0].Rows[0]["TotalAmount"].ToString();
                lblDeductionProposed.Text = ds.Tables[0].Rows[0]["DeductionProposed"].ToString();
                lblDeductionAmount.Text = ds.Tables[0].Rows[0]["DeductionAmount"].ToString();
                lblExtraSupDed.Text = ds.Tables[0].Rows[0]["ExtraSupDed"].ToString();
                lblTCS.Text = ds.Tables[0].Rows[0]["TCS"].ToString();
                //lblIGST.Text = ds.Tables[0].Rows[0]["IGST"].ToString();
                //lblCGST.Text = ds.Tables[0].Rows[0]["CGST"].ToString();
                //lblSGST.Text = ds.Tables[0].Rows[0]["SGST"].ToString();
                lblTDSPer.Text = ds.Tables[0].Rows[0]["TDSPer"].ToString();
                lblTDS.Text = ds.Tables[0].Rows[0]["TDS"].ToString();
                lblRoundOff.Text = ds.Tables[0].Rows[0]["RoundOff"].ToString();
                lblNetPayment.Text = ds.Tables[0].Rows[0]["NetPayment"].ToString();
                lblRemark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();

                lblAccountDeductionProposed.Text = ds.Tables[0].Rows[0]["AccountDeductionProposed"].ToString();
                lblAccountDedAmount.Text = ds.Tables[0].Rows[0]["AccountDedAmount"].ToString();
                lblAccountRoundOff.Text = ds.Tables[0].Rows[0]["AccountRoundOff"].ToString();
                lblAccountNetPayment.Text = ds.Tables[0].Rows[0]["AccountNetPayment"].ToString();
                lblAccountRemark.Text = ds.Tables[0].Rows[0]["AccountRemark"].ToString();

                HF_BasicTotalAmount.Value = ds.Tables[0].Rows[0]["BasicTotalAmount"].ToString();
                HF_RoundOff.Value = ds.Tables[0].Rows[0]["AccountRoundOff"].ToString();
                HF_NetPayment.Value = ds.Tables[0].Rows[0]["AccountNetPayment"].ToString();
                HF_DeductionAmount.Value = ds.Tables[0].Rows[0]["AccountDedAmount"].ToString();


                if (ds.Tables[0].Rows[0]["AuditStatus"].ToString() == "Pending")
                {
                    txtDeductionProposed.Text = ds.Tables[0].Rows[0]["AccountDeductionProposed"].ToString();
                    txtDeductionAmount.Text = ds.Tables[0].Rows[0]["AccountDedAmount"].ToString();
                    txtRoundOff.Text = ds.Tables[0].Rows[0]["AccountRoundOff"].ToString();
                    txtNetPayment.Text = ds.Tables[0].Rows[0]["AccountNetPayment"].ToString();
                    txtRemark.Text = ds.Tables[0].Rows[0]["AccountRemark"].ToString();
                }
                else
                {
                    txtDeductionProposed.Text = ds.Tables[0].Rows[0]["AuditDeductionProposed"].ToString();
                    txtRoundOff.Text = ds.Tables[0].Rows[0]["AuditRoundOff"].ToString();
                    txtDeductionAmount.Text = ds.Tables[0].Rows[0]["AuditDedAmount"].ToString();
                    txtNetPayment.Text = ds.Tables[0].Rows[0]["AuditNetPayment"].ToString();
                    txtRemark.Text = ds.Tables[0].Rows[0]["AuditRemark"].ToString();
                    btnApprove.Visible = false;
                }
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowPamentApprovalModal('');", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            //if (txtDeductionProposed.Text == "")
            //{
            //    msg = msg + "Enter Deduction Proposed (%). \\n";
            //}

            //if (txtRoundOff.Text == "")
            //{
            //    msg = msg + "Enter Round. \\n";
            //}
            if (txtNetPayment.Text == "")
            {
                msg = msg + "Enter Net Payment. \\n";
            }
            if (txtRemark.Text == "")
            {
                msg = msg + "Enter Remark. \\n";
            }

            if (msg.Trim() == "")
            {
                string DeductionProposed = txtDeductionProposed.Text;
                if (txtDeductionProposed.Text == "")
                    DeductionProposed = "0.00";


                string DeductionAmount = txtDeductionAmount.Text;
                if (txtDeductionAmount.Text == "")
                    DeductionAmount = "0.00";


                string RoundOff = txtRoundOff.Text;
                if (txtRoundOff.Text == "")
                    RoundOff = "0.00";


                string NetPayment = txtNetPayment.Text;
                if (txtNetPayment.Text == "")
                    NetPayment = "0.00";





                ds = objdb.ByProcedure("SpFinSupplierOrder",
               new string[] { "flag", "OrderID", "DeductionProposed", "DeductionAmount", "RoundOff", "NetPayment", "Remark", "UpdatedBy" },
               new string[] { "5", ViewState["OrderID"].ToString(), DeductionProposed, DeductionAmount, RoundOff, NetPayment, txtRemark.Text, ViewState["Emp_ID"].ToString() }, "dataset");


                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Data Successfully Approved.");
                ClearText();
                FillGrid();


            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowPamentApprovalModal('" + msg + "');", true);
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        lblOrderNo.Text = "";
        lblOrderDate.Text = "";
        lblSupplierName.Text = "";
        lblPlantName.Text = "";
        lblItemName.Text = "";
        lblUnit.Text = "";
        lblItemRate.Text = "";
        lblQuantity.Text = "";
        lblCessApplicable.Text = "";
        lblCessRate.Text = "";
        lblGSTApplicable.Text = "";
        lblGSTRate.Text = "";
        lblGSTType.Text = "";
        lblGSTIncInDeduction.Text = "";
        lblQualityCleanReport.Text = "";
        lblBillNo.Text = "";
        lblBillDate.Text = "";
        lblQuantityReceiveDate.Text = "";
        lblQuantityReceive.Text = "";
        lblDiffQuantity.Text = "";
        lblBasicRate.Text = "";
        lblGST.Text = "";
        lblBasicAmount.Text = "";
        lblGSTAmount.Text = "";
        lblCess.Text = "";
        lblTotalAmount.Text = "";
        lblDeductionProposed.Text = "";
        lblDeductionAmount.Text = "";
        lblTCS.Text = "";
        //lblIGST.Text =  "";
        //lblCGST.Text =  "";
        //lblSGST.Text =  "";
        lblTDS.Text = "";
        lblRoundOff.Text = "";
        lblNetPayment.Text = "";
        lblRemark.Text = "";
        HF_BasicTotalAmount.Value = "";
        HF_NetPayment.Value = "";
        HF_DeductionAmount.Value = "";
        txtDeductionProposed.Text = "";
        txtDeductionAmount.Text = "";
        txtNetPayment.Text = "";

        lblAccountDeductionProposed.Text = "";
        lblAccountDedAmount.Text = "";
        lblAccountRoundOff.Text = "";
        lblAccountNetPayment.Text = "";
        txtRemark.Text = "";
    }
}