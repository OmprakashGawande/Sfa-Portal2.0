using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class mis_Finance_Rpt_GSTTDSDetailOfficeWise : System.Web.UI.Page
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
                    ViewState["OfficeType_Title"] = Session["OfficeType_Title"].ToString();
                    ViewState["Division_ID"] = Session["Division_ID"].ToString();
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    txtTodate.Attributes.Add("readonly", "readonly");
                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    divExcel.Visible = false;
                    FillDropdown();
                    GetCommonSearch();
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
            ddlOffice.Enabled = false;
            ddlRegionalOffice.Enabled = false;
            divRegionalOffice.Visible = false;
            if (ViewState["OfficeType_Title"].ToString() != "District Office" && ViewState["OfficeType_Title"].ToString() != "Production Unit")
            {
                ddlOffice.Enabled = true;
                ddlRegionalOffice.Enabled = true;
                divRegionalOffice.Visible = true;
                ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "21" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlRegionalOffice.DataSource = ds;
                    ddlRegionalOffice.DataTextField = "Office_Name";
                    ddlRegionalOffice.DataValueField = "Office_ID";
                    ddlRegionalOffice.DataBind();
                    ddlRegionalOffice.Items.Insert(0, new ListItem("All", "0"));
                    ddlRegionalOffice.Items.Insert(ds.Tables[0].Rows.Count + 1, new ListItem("Production Unit", "010"));
                    if (ViewState["OfficeType_Title"].ToString() == "Regional Office")
                    {
                        ddlRegionalOffice.Enabled = false;
                        ddlRegionalOffice.SelectedValue = ViewState["Division_ID"].ToString();
                    }
                }
            }
            FillOffice();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOffice()
    {
        try
        {

            //ddlOffice.Enabled = false;
            //ddlRegionalOffice.Enabled = false;

            ddlOffice.Items.Clear();
            if (ddlRegionalOffice.SelectedIndex > 0)
            {
                if (ddlRegionalOffice.SelectedValue.ToString() != "010" && ddlRegionalOffice.SelectedItem.ToString() != "Production Unit")
                {
                    ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag", "Office_ID" },
                           new string[] { "23", ddlRegionalOffice.SelectedValue.ToString() }, "dataset");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlOffice.DataSource = ds;
                        ddlOffice.DataTextField = "Office_Name";
                        ddlOffice.DataValueField = "Office_ID";
                        ddlOffice.DataBind();
                        //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                    }
                }
                else
                {
                    ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag" },
                           new string[] { "24" }, "dataset");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlOffice.DataSource = ds;
                        ddlOffice.DataTextField = "Office_Name";
                        ddlOffice.DataValueField = "Office_ID";
                        ddlOffice.DataBind();
                        //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                    }

                }
            }
            else
            {
                ds = objdb.ByProcedure("SpAdminOffice",
                        new string[] { "flag" },
                        new string[] { "22" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOffice.DataSource = ds;
                    ddlOffice.DataTextField = "Office_Name";
                    ddlOffice.DataValueField = "Office_ID";
                    ddlOffice.DataBind();
                    //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                    ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillDetail()
    {
        lblGrid.Text = "";
        StringBuilder sb = new StringBuilder();
        //gvTDSDetail.DataSource = null;
        //gvTDSDetail.DataBind();
        string Office = "";
        string PreOffice = "";
        string OfficeName = "";
        decimal TotalBillAmount = 0;
        decimal PaymentAmount = 0;
        decimal BasicAmount = 0;
        decimal GSTTDSAmount = 0;
        decimal CGST = 0;
        decimal SGST = 0;
        decimal IGST = 0;
        int SerialNo = 0;
        int totalListItem = ddlOffice.Items.Count;
        if (txtFromDate.Text != null && txtTodate.Text != null)
        {
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    SerialNo++;
                    Office += item.Value + ",";
                    OfficeName += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
                }
            }
            if (Office != "")
            {
                if (totalListItem == SerialNo)
                {
                    OfficeName = "All Offices";
                }
                else if (SerialNo == 0)
                {
                    OfficeName = "---Office Not Selected---";
                }
                else
                {
                    OfficeName = OfficeName.Remove(OfficeName.Length - 1, 1);
                }
            }
            /*****************SET Search Option Start********************/
            #region SetSearchOption
            Session["CommonOffice"] = Office;
            Session["CommonFromDate"] = txtFromDate.Text;
            Session["CommonToDate"] = txtTodate.Text;
            #endregion
            /*****************SET Search Option End********************/

            ds = objdb.ByProcedure("SpFinTDSGSTEntry",
                                  new string[] { "flag", "Office", "FromDate", "ToDate" },
                                  new string[] { "8", Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTodate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                divExcel.Visible = true;
                string headingFirst = "<p class='text-center' style='font-weight:600'>GST TDS Branch Wise Detail<br /> MP State Agro Industries Development Corporation, <br/> [ " + OfficeName + " ] <br />  (" + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtTodate.Text, cult).ToString("dd-MM-yyyy") + ")</p>";
                sb.Append("<table class='table table-bordered'>");
                sb.Append("<tr>");
                sb.Append("<th>S.No</th>");
                sb.Append("<th>Branch</th>");
                sb.Append("<th>Party Name</th>");
                sb.Append("<th>GST No</th>");
                sb.Append("<th>Bill No</th>");
                sb.Append("<th>Bill Date</th>");
                sb.Append("<th>Total Bill Amount</th>");
                sb.Append("<th>Payment Amount</th>");
                sb.Append("<th>Basic Amount</th>");
                sb.Append("<th>GST TDS Amount</th>");
                sb.Append("<th>CGST</th>");
                sb.Append("<th>SGST</th>");
                sb.Append("<th>IGST</th>");
                sb.Append("</tr>");
                int rowspan = 0;
                int Total = 0;
                int count = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    if (i == 0)
                    {
                        //Total = Total + 1;
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        sb.Append("<td style=' vertical-align:top;' rowspan='" + ds.Tables[0].Rows[i]["OFFCount"].ToString() + "'>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["GSTNo"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["BillNo"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["BillDate"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["TotalBillAmount"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["PaymentAmount"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["BasicAmount"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["GSTTDSAmount"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["CGST"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["SGST"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["IGST"].ToString() + "</td>");
                        TotalBillAmount += decimal.Parse(ds.Tables[0].Rows[i]["TotalBillAmount"].ToString());
                        PaymentAmount += decimal.Parse(ds.Tables[0].Rows[i]["PaymentAmount"].ToString());
                        BasicAmount += decimal.Parse(ds.Tables[0].Rows[i]["BasicAmount"].ToString());
                        GSTTDSAmount += decimal.Parse(ds.Tables[0].Rows[i]["GSTTDSAmount"].ToString());
                        CGST += decimal.Parse(ds.Tables[0].Rows[i]["CGST"].ToString());
                        SGST += decimal.Parse(ds.Tables[0].Rows[i]["SGST"].ToString());
                        IGST += decimal.Parse(ds.Tables[0].Rows[i]["IGST"].ToString());
                        sb.Append("</tr>");

                    }
                    else if (PreOffice == ds.Tables[0].Rows[i]["Office_Name"].ToString())
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        //sb.Append("<td></td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["GSTNo"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["BillNo"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["BillDate"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["TotalBillAmount"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["PaymentAmount"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["BasicAmount"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["GSTTDSAmount"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["CGST"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["SGST"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["IGST"].ToString() + "</td>");
                        TotalBillAmount += decimal.Parse(ds.Tables[0].Rows[i]["TotalBillAmount"].ToString());
                        PaymentAmount += decimal.Parse(ds.Tables[0].Rows[i]["PaymentAmount"].ToString());
                        BasicAmount += decimal.Parse(ds.Tables[0].Rows[i]["BasicAmount"].ToString());
                        GSTTDSAmount += decimal.Parse(ds.Tables[0].Rows[i]["GSTTDSAmount"].ToString());
                        CGST += decimal.Parse(ds.Tables[0].Rows[i]["CGST"].ToString());
                        SGST += decimal.Parse(ds.Tables[0].Rows[i]["SGST"].ToString());
                        IGST += decimal.Parse(ds.Tables[0].Rows[i]["IGST"].ToString());
                        sb.Append("</tr>");
                    }
                    else
                    {
                        sb.Append("<tr>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<th>Total :</td>");
                        sb.Append("<th class='right-align'>" + TotalBillAmount.ToString() + "</td>");
                        sb.Append("<th class='right-align'>" + PaymentAmount.ToString() + "</td>");
                        sb.Append("<th class='right-align'>" + BasicAmount.ToString() + "</td>");
                        sb.Append("<th class='right-align'>" + GSTTDSAmount.ToString() + "</td>");
                        sb.Append("<th class='right-align'>" + CGST.ToString() + "</td>");
                        sb.Append("<th class='right-align'>" + SGST.ToString() + "</td>");
                        sb.Append("<th class='right-align'>" + IGST.ToString() + "</td>");
                        sb.Append("</tr>");
                        TotalBillAmount = 0;
                        PaymentAmount = 0;
                        BasicAmount = 0;
                        GSTTDSAmount = 0;
                        CGST = 0;
                        SGST = 0;
                        IGST = 0;
                        count = 0;
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        sb.Append("<td rowspan='" + ds.Tables[0].Rows[i]["OFFCount"].ToString() + "'>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["GSTNo"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["BillNo"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["BillDate"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["TotalBillAmount"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["PaymentAmount"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["BasicAmount"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["GSTTDSAmount"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["CGST"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["SGST"].ToString() + "</td>");
                        sb.Append("<td class='right-align'>" + ds.Tables[0].Rows[i]["IGST"].ToString() + "</td>");
                        TotalBillAmount += decimal.Parse(ds.Tables[0].Rows[i]["TotalBillAmount"].ToString());
                        PaymentAmount += decimal.Parse(ds.Tables[0].Rows[i]["PaymentAmount"].ToString());
                        BasicAmount += decimal.Parse(ds.Tables[0].Rows[i]["BasicAmount"].ToString());
                        GSTTDSAmount += decimal.Parse(ds.Tables[0].Rows[i]["GSTTDSAmount"].ToString());
                        CGST += decimal.Parse(ds.Tables[0].Rows[i]["CGST"].ToString());
                        SGST += decimal.Parse(ds.Tables[0].Rows[i]["SGST"].ToString());
                        IGST += decimal.Parse(ds.Tables[0].Rows[i]["IGST"].ToString());
                        sb.Append("</tr>");

                    }

                    PreOffice = ds.Tables[0].Rows[i]["Office_Name"].ToString();
                }
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td><b>Total :</b></td>");
                sb.Append("<td class='right-align'><b>" + TotalBillAmount.ToString() + "</b></td>");
                sb.Append("<td class='right-align'><b>" + PaymentAmount.ToString() + "</b></td>");
                sb.Append("<td class='right-align'><b>" + BasicAmount.ToString() + "</b></td>");
                sb.Append("<td class='right-align'><b>" + GSTTDSAmount.ToString() + "</b></td>");
                sb.Append("<td class='right-align'><b>" + CGST.ToString() + "</b></td>");
                sb.Append("<td class='right-align'><b>" + SGST.ToString() + "</b></td>");
                sb.Append("<td class='right-align'><b>" + IGST.ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                DivHTML.InnerHtml = sb.ToString();
                //gvTDSDetail.DataSource = ds.Tables[0];
                //gvTDSDetail.DataBind();
                lblGrid.Text = headingFirst;
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DivHTML.InnerHtml = "";
            FillDetail();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ddlRegionalOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillOffice();
    }
    protected void GetCommonSearch()
    {
        try
        {
            if (Session["CommonOffice"] != null)
            {
                string Office = Session["CommonOffice"].ToString();
                string[] OfficeList = Office.Split(new Char[] { ',' });
                if (OfficeList.Count() > 0)
                {
                    ddlOffice.ClearSelection();
                    foreach (string OfficeID in OfficeList)
                    {
                        // ddlOffice.SelectedValue = OfficeID.ToString();
                        foreach (ListItem item in ddlOffice.Items)
                        {
                            if (item.Value == OfficeID)
                                item.Selected = true;
                        }
                    }
                }

            }
            if (Session["CommonFromDate"] != null)
            {
                string FromDate = Session["CommonFromDate"].ToString();
                txtFromDate.Text = FromDate.ToString();
            }
            if (Session["CommonToDate"] != null)
            {
                string ToDate = Session["CommonToDate"].ToString();
                txtTodate.Text = ToDate.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
   
}