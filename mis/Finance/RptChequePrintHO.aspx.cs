using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_Finance_RptChequePrintHO : System.Web.UI.Page
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
                    txtInstrumentDate.Attributes.Add("readonly", "readonly");
                    txtInstrumentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                   // FillVoucherDate();

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

    protected void FillVoucherDate()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtInstrumentDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }



    protected void btnPnb_Click(object sender, ImageClickEventArgs e)
    {
        lblMsg.Text = "";
        string msg = "";
       
        if (txtInstrumentDate.Text == "")
        {
            msg += "Select Cheque Date.\\n";
        }
        if (txtBeneficiaryName.Text == "")
        {
            msg += "Enter Beneficiary Name.\\n";
        }
        if (txtAmount.Text == "")
        {
            msg += "Enter Amount.\\n";
        }
        if (msg == "")
        {
            
            Session["ChkInstrumentDate"] = txtInstrumentDate.Text;
            Session["ChkAmount"] = Math.Round(double.Parse(txtAmount.Text), 2).ToString("0.00");
            Session["BeneficiaryName"] = txtBeneficiaryName.Text;    
        
            if (chkAcPayee.Checked == true)
                Session["ChkAcPayee"] = "Yes";
            else
                Session["ChkAcPayee"] = "No";

            string Url = "ChequePrintHO.aspx?";
            Url = Url + "Type=" + objdb.Encrypt("PNB");
            Response.Redirect(Url);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert(" + msg + ");", true);
        }


    }
    protected void btnSbi_Click(object sender, ImageClickEventArgs e)
    {
        lblMsg.Text = "";
        string msg = "";
       
        if (txtInstrumentDate.Text == "")
        {
            msg += "Select Cheque Date.\\n";
        }
        if (txtBeneficiaryName.Text == "")
        {
            msg += "Enter Beneficiary Name.\\n";
        }
        if (txtAmount.Text == "")
        {
            msg += "Enter Amount.\\n";
        }
        if (msg == "")
        {
          
           
            Session["ChkInstrumentDate"] = txtInstrumentDate.Text;
            Session["ChkAmount"] = Math.Round(double.Parse(txtAmount.Text), 2).ToString("0.00");
            Session["BeneficiaryName"] = txtBeneficiaryName.Text;

            if (chkAcPayee.Checked == true)
                Session["ChkAcPayee"] = "Yes";
            else
                Session["ChkAcPayee"] = "No";


            string Url = "ChequePrintHO.aspx?";
            Url = Url + "Type=" + objdb.Encrypt("SBI");
            Response.Redirect(Url);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert(" + msg + ");", true);
        }

    }

}
