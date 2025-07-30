using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_Finance_VoucherInvoice : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, Dset;
    string VoucherTx_ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
          if (objdb.createdBy() != null)
        {
            if (!IsPostBack)
            {
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                if (Request.QueryString["VoucherTx_ID"] != null)
                {
                    VoucherTx_ID = objdb.Decrypt(Request.QueryString["VoucherTx_ID"].ToString());
                    Dset = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "Office_ID" },
                                                new string[] { "11", VoucherTx_ID, ViewState["Office_ID"].ToString() }, "dataset");
                    if (Dset!= null)
                    {
                        if (Dset.Tables[0].Rows.Count > 0)
                        {
                            lblVoucherNo.Text = Dset.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                            lblVoucherDt.Text = Dset.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                            //lblWarehouseName.Text = Dset.Tables[0].Rows[0]["WarehouseName"].ToString();
                            lblVoucherName.Text = Dset.Tables[0].Rows[0]["VoucherTx_Name"].ToString();
                            lblVoucherType.Text = Dset.Tables[0].Rows[0]["VoucherTx_Type"].ToString();
                            lblNarration.Text = Dset.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
                            lblVoucherAmt.Text = Dset.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                            lblGrandTotal.Text = Dset.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                            gvAddLedger.DataSource = Dset.Tables[1];
                            gvAddLedger.DataBind();
                            decimal LedgerCreditTotal = 0;
                            decimal LedgerDebitTotal = 0;
                            LedgerCreditTotal = Dset.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
                            LedgerDebitTotal = Dset.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));
                            gvAddLedger.FooterRow.Cells[2].Text = "<b>Total : </b>";
                            gvAddLedger.FooterRow.Cells[3].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";
                            gvAddLedger.FooterRow.Cells[4].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";

                        }
                        if (Dset.Tables[4].Rows.Count > 0)
                        {
                            gvAddItems.DataSource = Dset.Tables[4];
                            gvAddItems.DataBind();
                            divgrandtotal.Visible = true;
                        }
                        if (Dset.Tables[5].Rows.Count > 0)
                        {
                            GridViewLedger.DataSource = Dset.Tables[5];
                            GridViewLedger.DataBind();
                            divgrandtotal.Visible = true;

                        }

                    }
                    
                    else
                    {
                        Response.Redirect("purchaseOrderDetail.aspx");
                    }
                }
                else
                {
                    //  Clear();
                }
            }
        }
        else
        {
            objdb.redirectToHome();
        }

   

    }
}