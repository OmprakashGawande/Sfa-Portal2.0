using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_Payroll_PayRollMonthlyEarnDedDetail : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillOffice();
                FillFinancialYear();
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    ddlOffice_Name.Enabled = true;
                    ddlOffice_Name.Items.FindByValue(ViewState["Office_ID"].ToString()).Selected = true;
                }
                else
                {
                    ddlOffice_Name.Enabled = false;
                    ddlOffice_Name.Items.FindByValue(ViewState["Office_ID"].ToString()).Selected = true;
                }
                FillEmptyGrid();
                ShowHide("default");
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    
    private void ShowHide(string strval)
    {
        switch (strval)
        {
            case "all":
                pnlgriddata.Visible = true;
                GridView1.SelectedIndex = -1;
                pnlback.Visible = false;
            pnlreceiptAll.Visible = false;
            pnldownloadAll.Visible = false;
            pnlreceiptSingle.Visible = false;
            pnldownloadSingle.Visible = false;
                break;

            case "a":
                pnlback.Visible = false;
                pnldownloadSingle.Visible = false;
                pnlgriddata.Visible = true;
                pnldownloadAll.Visible = true;
                GridView1.SelectedIndex = -1;
            break;

            case "b":
                pnlreceiptSingle.Visible = true;
                pnldownloadSingle.Visible = true;
                pnlback.Visible = true;
                pnldownloadAll.Visible = false;
                pnlgriddata.Visible = false;
           
            break;

            case "c":
                pnlreceiptSingle.Visible = false;
                pnldownloadSingle.Visible = false;
                pnlback.Visible = false;
                pnldownloadAll.Visible = true;
                pnlgriddata.Visible = true;
           
            break;

            default:
            pnlback.Visible = false;
            pnlreceiptAll.Visible = false;
            pnldownloadAll.Visible = false;
            pnlreceiptSingle.Visible = false;
            pnldownloadSingle.Visible = false;
            pnlgriddata.Visible = false;
            GridView1.SelectedIndex = -1;
            break;

        }
    }
    protected void FillOffice()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminOffice",
                        new string[] { "flag" },
                        new string[] { "1" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice_Name.DataSource = ds;
                ddlOffice_Name.DataTextField = "Office_Name";
                ddlOffice_Name.DataValueField = "Office_ID";
                ddlOffice_Name.DataBind();
                ddlOffice_Name.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlOffice_Name.DataSource = null;
                ddlOffice_Name.DataBind();
                ddlOffice_Name.Items.Clear();
                ddlOffice_Name.Items.Insert(0, new ListItem("All", "0"));
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblDeductionDetails.Text = "";
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                ds = null;
                ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "Year" }, new string[] { "2", ddlFinancialYear.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlEarnDeducHead.DataSource = ds;
                    ddlEarnDeducHead.DataTextField = "EarnDeduction_Name";
                    ddlEarnDeducHead.DataValueField = "EarnDeduction_ID";
                    ddlEarnDeducHead.DataBind();
                    ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlEarnDeducHead.Items.Clear();
                ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillFinancialYear()
    {
        try
        {

            ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "8" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlFinancialYear.DataSource = ds;
                ddlFinancialYear.DataTextField = "Year";
                ddlFinancialYear.DataValueField = "Year";
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
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
            ds = null;
            lblMsg.Text = "";
            string msg = "";
            lblDeductionDetails.Text = "";
            //if(ddlOffice_Name.SelectedIndex <= 0)
            //{
            //    msg += "Select Office Name \\n";
            //}
            if (ddlFinancialYear.SelectedIndex <= 0)
            {
                msg += "Select Financial Year \\n";
            }
            if (ddlMonth.SelectedIndex <= 0)
            {
                msg += "Select Month \\n";
            }
            if (ddlEarnDeducHead.SelectedIndex <= 0)
            {
                msg += "Select Earning Deduction Head \\n";
            }
            //if (txtmonth.Text == "")
            //{
            //    msg += "Select Month \\n";
            //}
            if (msg == "")
            {
                lblDeductionDetails.Text = ddlEarnDeducHead.SelectedItem.ToString()+" Details ("+ddlMonth.SelectedItem.ToString()+"-"+ddlFinancialYear.SelectedItem.ToString()+") of "+ddlOffice_Name.SelectedItem.ToString();
                
                ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise",
                    new string[] { "flag", "Office_ID", "SalaryMonth", "SalaryYear", "EarnDeduction_ID" },
                    new string[] { "12", ddlOffice_Name.SelectedValue, ddlMonth.SelectedValue, ddlFinancialYear.SelectedValue,ddlEarnDeducHead.SelectedValue }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    if (ddlEarnDeducHead.SelectedValue == "10" || ddlEarnDeducHead.SelectedValue == "31")
                    {
                        ShowHide("a");
                        GridView1.Columns[5].Visible = true;

                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportDataSource datasource = new ReportDataSource();
                        if (ddlEarnDeducHead.SelectedValue == "10")
                        {
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Rpt_ReceiptMonthlyEarningDeductionGIS.rdlc");
                            datasource = new ReportDataSource("dtMonthlyEarningDeductionReceipt", ds.Tables[0]);
                        }
                        else if (ddlEarnDeducHead.SelectedValue == "31")
                        {
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Rpt_ReceiptMonthlyEarningDeductionGPF.rdlc");
                            datasource = new ReportDataSource("dtMonthlyEarningDeductionReceiptGPF", ds.Tables[0]);
                        }


                        ReportParameter office_name1, office_name2, office_Address, offic_contact;
                        if (objdb.Office_ID() == "81")
                        {
                            office_name1 = new ReportParameter("OfficeName1", "Minor Forest Produce-Processing and Research Centre (MFP-PARC)");
                            office_name2 = new ReportParameter("OfficeName2", "(A unit of M.P.State Minor Forest Produce (Trading & Development) Co-Op. Federation Ltd., Bhopal)");
                            office_Address = new ReportParameter("OfficeAddress", "Van Parisar, Barkhera Pathani, Bhopal (MP)-462021");
                            offic_contact = new ReportParameter("OfficeContact", "Phone & Fax-0755-2970629 E-mail - mfp_parc@rediffmail.com ");

                        }
                        else
                        {
                            office_name1 = new ReportParameter("OfficeName1", "M.P. State Minor Forest Produce Federation");
                            office_name2 = new ReportParameter("OfficeName2", "(Trade & Development)");
                            office_Address = new ReportParameter("OfficeAddress", "Sports Complex Indira Nikunj, 74 Bungalows Bhopal - 462003 (MP) INDIA");
                            offic_contact = new ReportParameter("OfficeContact", "Phone (91)-755-2760253, 2674202 E-mail - md.mfpfed@mp.gov.in ");
                        }
                        ReportParameter rp = new ReportParameter("SubjectName", "Statement showing remittance of " + ddlEarnDeducHead.SelectedItem.Text + " from the salary for the month of " + ddlMonth.SelectedItem.Text + "-" + ddlFinancialYear.SelectedItem.Text);
                        ReportParameter rp1 = new ReportParameter("HeadName", ddlEarnDeducHead.SelectedItem.Text);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        if (ddlEarnDeducHead.SelectedValue == "10")
                        {
                            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { office_name1, office_name2, office_Address, offic_contact, rp, rp1 });
                        }
                        else if (ddlEarnDeducHead.SelectedValue == "31")
                        {
                            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { office_name1, office_name2, office_Address, offic_contact, rp });
                        }

                        ReportViewer1.LocalReport.DataSources.Add(datasource);
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ShowHide("all");
                        GridView1.Columns[5].Visible = false;
                    }
                  

                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        Label lblEarn = (Label)row.FindControl("lblEarnDed");
                        Label lblArrearEarnDed = (Label)row.FindControl("lblArrearEarnDed");


                        if(lblArrearEarnDed.Text =="")
                        {
                            lblArrearEarnDed.Text = "0.00";
                        }
                        if (lblEarn.Text == "")
                        {
                            lblEarn.Text = "0.00";
                        }
                    }

                    //Decimal TotalEarningAmt = 0;
                    Decimal ArrearEarningAmt = 0;
                    Decimal totalEarnDedAmt = 0;
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        //if (ds.Tables[0].Rows[i]["EarningTotal"].ToString() != "")
                        //{
                        //    TotalEarningAmt += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal"].ToString());
                        //}
                        if (ds.Tables[0].Rows[i]["EarnDed"].ToString() != "")
                        {
                            totalEarnDedAmt += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarnDed"].ToString());
                        }
                        if (ds.Tables[0].Rows[i]["ArrearEarnDed"].ToString() != "")
                        {
                            ArrearEarningAmt += Convert.ToDecimal(ds.Tables[0].Rows[i]["ArrearEarnDed"].ToString());
                        }  

                    }
                    
                    GridView1.HeaderRow.Cells[3].Text = ddlEarnDeducHead.SelectedItem.ToString();
                    GridView1.HeaderRow.Cells[4].Text = "Arrear "+ddlEarnDeducHead.SelectedItem.ToString();

                    //ViewState["TotalEarningAmt"] = TotalEarningAmt.ToString();
                    ViewState["totalEarnDedAmt"] = totalEarnDedAmt.ToString();
                    GridView1.FooterRow.Cells[0].Text = "<b>| TOTAL |</b>";
                    //GridView1.FooterRow.Cells[3].Text = "<b>" + ViewState["TotalEarningAmt"].ToString() + "</b>";
                    GridView1.FooterRow.Cells[3].Text = "<b>" + ViewState["totalEarnDedAmt"].ToString() + "</b>";
                    GridView1.FooterRow.Cells[4].Text = "<b>" + ArrearEarningAmt.ToString() + "</b>";

                }
                else if (ds != null && ds.Tables[0].Rows.Count == 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    ShowHide("all");

                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    ShowHide("all");
                }
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            {}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void ddlOffice_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblDeductionDetails.Text = "";
            ddlFinancialYear.ClearSelection();
            ddlMonth.ClearSelection();
           FillEmptyGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEmptyGrid()
    {
        try
        {
             ds = objdb.ByProcedure("SpPayrollPolicyDetail",
                    new string[] { "flag"},
                    new string[] { "6" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else if (ds != null && ds.Tables[0].Rows.Count == 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;
            }
       
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblDeductionDetails.Text = "";
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                ds = null;
                ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "Year" }, new string[] { "2", ddlFinancialYear.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlEarnDeducHead.DataSource = ds;
                    ddlEarnDeducHead.DataTextField = "EarnDeduction_Name";
                    ddlEarnDeducHead.DataValueField = "EarnDeduction_ID";
                    ddlEarnDeducHead.DataBind();
                    ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlEarnDeducHead.Items.Clear();
                ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        ShowHide("c");
        GetDatatableHeaderDesign();
    }
    protected void lnkbtnDownload_Click(object sender, EventArgs e)
    {
        Warning[] warnings;
        string[] streamIds;
        string contentType;
        string encoding;
        string extension;

        //Export the RDLC Report to Byte Array.
        //byte[] bytes = ReportViewer1.LocalReport.Render(rbFormat.SelectedItem.Value, null, out contentType, out encoding, out extension, out streamIds, out warnings);
        byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);
        //Download the RDLC Report in Word, Excel, PDF and Image formats.
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + ddlEarnDeducHead.SelectedItem.Text + DateTime.Now.ToString() + "." + extension);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }

    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "RecordView")
         {
             Control ctrl = e.CommandSource as Control;
             if (ctrl != null)
             {
                 GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                 Label lblEmp_Name = (Label)row.FindControl("lblEmp_Name");
                 Label lblDesignationName = (Label)row.FindControl("lblDesignationName");
                 Label lblEarningTotal = (Label)row.FindControl("lblEarningTotal");
                 Label lblEarnDed = (Label)row.FindControl("lblEarnDed");
                 Label lblArrearEarnDed = (Label)row.FindControl("lblArrearEarnDed");
                 Label lblEmp_GpfNo = (Label)row.FindControl("lblEmp_GpfNo");
                 hfempname.Value = lblEmp_Name.Text;
                 DataTable dt = new DataTable();

                 dt.Columns.AddRange(new DataColumn[7] { new DataColumn("Emp_ID",typeof(int))
                         ,new DataColumn("Emp_Name",typeof(string))
                         ,new DataColumn("DesignationName",typeof(string))
                         ,new DataColumn("EarningTotal",typeof(decimal))
                         ,new DataColumn("ArrearEarnDed",typeof(decimal))
                          ,new DataColumn("EarnDed",typeof(decimal))
                            ,new DataColumn("Emp_GpfNo",typeof(string))
                             });

                 dt.Rows.Add(e.CommandArgument.ToString(), lblEmp_Name.Text, lblDesignationName.Text
                     , lblEarningTotal.Text, lblArrearEarnDed.Text, lblEarnDed.Text,lblEmp_GpfNo.Text
                    
                 );


                 ShowHide("b");

                 ReportViewer2.ProcessingMode = ProcessingMode.Local;
                 ReportDataSource datasource = new ReportDataSource();
                 if (ddlEarnDeducHead.SelectedValue == "10")
                 {
                     ReportViewer2.LocalReport.ReportPath = Server.MapPath("Rpt_ReceiptMonthlyEarningDeductionGIS.rdlc");
                     datasource = new ReportDataSource("dtMonthlyEarningDeductionReceipt", dt);
                 }
                 else if (ddlEarnDeducHead.SelectedValue == "31")
                 {
                     ReportViewer2.LocalReport.ReportPath = Server.MapPath("Rpt_ReceiptMonthlyEarningDeductionGPF.rdlc");
                     datasource = new ReportDataSource("dtMonthlyEarningDeductionReceiptGPF", dt);
                 }
                 ReportParameter office_name1, office_name2, office_Address, offic_contact;
                 if (objdb.Office_ID() == "81")
                 {
                     office_name1 = new ReportParameter("OfficeName1", "Minor Forest Produce-Processing and Research Centre (MFP-PARC)");
                     office_name2 = new ReportParameter("OfficeName2", "(A unit of M.P.State Minor Forest Produce (Trading & Development) Co-Op. Federation Ltd., Bhopal)");
                     office_Address = new ReportParameter("OfficeAddress", "Van Parisar, Barkhera Pathani, Bhopal (MP)-462021");
                     offic_contact = new ReportParameter("OfficeContact", "Phone & Fax-0755-2970629 E-mail - mfp_parc@rediffmail.com ");

                 }
                 else
                 {
                     office_name1 = new ReportParameter("OfficeName1", "M.P. State Minor Forest Produce Federation");
                     office_name2 = new ReportParameter("OfficeName2", "(Trade & Development)");
                     office_Address = new ReportParameter("OfficeAddress", "Sports Complex Indira Nikunj, 74 Bungalows Bhopal - 462 003 (M.P) INDIA");
                     offic_contact = new ReportParameter("OfficeContact", "Phone (91)-755-2760253, 2674202 E-mail - md.mfpfed@mp.gov.in ");
                 }
                 ReportParameter rp = new ReportParameter("SubjectName", "Statement showing remittance of " + ddlEarnDeducHead.SelectedItem.Text + " from the salary for the month of " + ddlMonth.SelectedItem.Text + "-" + ddlFinancialYear.SelectedItem.Text);
                 ReportParameter rp1 = new ReportParameter("HeadName", ddlEarnDeducHead.SelectedItem.Text);
                 ReportViewer2.LocalReport.DataSources.Clear();
                 if (ddlEarnDeducHead.SelectedValue == "10")
                 {
                     ReportViewer2.LocalReport.SetParameters(new ReportParameter[] { office_name1, office_name2, office_Address, offic_contact, rp, rp1 });
                 }
                 else if (ddlEarnDeducHead.SelectedValue == "31")
                 {
                     ReportViewer2.LocalReport.SetParameters(new ReportParameter[] { office_name1, office_name2, office_Address, offic_contact, rp });
                 }
                 ReportViewer2.LocalReport.DataSources.Add(datasource);
                 ReportViewer2.LocalReport.Refresh();

                 foreach (GridViewRow gvRow in GridView1.Rows)
                 {
                     if (GridView1.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                     {
                         GridView1.SelectedIndex = gvRow.DataItemIndex;
                         GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                         break;
                     }
                 }
                 GetDatatableHeaderDesign();
             }
         }
    }
    protected void lnkbtnDownloadSingle_Click(object sender, EventArgs e)
    {
        Warning[] warnings;
        string[] streamIds;
        string contentType;
        string encoding;
        string extension;

        //Export the RDLC Report to Byte Array.
        //byte[] bytes = ReportViewer1.LocalReport.Render(rbFormat.SelectedItem.Value, null, out contentType, out encoding, out extension, out streamIds, out warnings);
        byte[] bytes = ReportViewer2.LocalReport.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);
        //Download the RDLC Report in Word, Excel, PDF and Image formats.
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + hfempname.Value + ddlEarnDeducHead.SelectedItem.Text + DateTime.Now.ToString() + "." + extension);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
}