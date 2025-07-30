using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;


public partial class mis_Finance_RptListOfLedger : System.Web.UI.Page
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
                    ddlOffice.Enabled = false;
                    FillDropdown();
                }
                //lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt");
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
                        // ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
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
                        // ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
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



    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }




    protected void btnSearchDetailed_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            string Office = "";
            lblMsg.Text = "";
            string OfficeName = "";
            int SerialNo = 0;
            DataSet ds1;
            int totalListItem = ddlOffice.Items.Count;
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

                Session["Office"] = Office;
                // Session["Header"] = "<p class='text-center' style='font-weight:600'>Detailed Trial Balance <br /> MP State Agro Industries Development Corporation, <br/> [ " + OfficeName + " ]");
                sb.Append("<p class='text-center' style='font-weight:600'>Detailed Trial Balance <br /> MP State Agro Industries Development Corporation, <br/> [ " + OfficeName + " ]");

                sb.Append("<table id='GrvTable' style='width:100%;' class='table table-hover table-bordered'>");
                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<th style='width:95%;'>Group Name</th>");
                sb.Append("<th class='HRow' style='width:5%;'>Action</th>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                ds = objdb.ByProcedure("SpFinHeadLedgerDetail", new string[] { "flag", "Office_ID_Mlt" }, new string[] { "4", Office }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    sb.Append("<tbody>");
                    int rowcount = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < rowcount; i++)
                    {
                        string HeadID = ds.Tables[0].Rows[i]["Head_ID"].ToString();
                        sb.Append("<tr class='Chead'>");
                        sb.Append("<td style='padding-left:13px;'><b>" + ds.Tables[0].Rows[i]["Head_Name"].ToString() + "</b></td>");
                        sb.Append("<td class='HRow' style='width:5%;'></td>");
                        sb.Append("</tr>");

                        DataView dv1 = new DataView();
                        dv1 = ds.Tables[1].DefaultView;
                        dv1.RowFilter = "Head_ParentID = '" + HeadID + "'";
                        DataTable dt1 = dv1.ToTable();
                        int dt1RowCount = dt1.Rows.Count;
                        for (int i1 = 0; i1 < dt1RowCount; i1++)
                        {

                            string HeadID_Dt1 = dt1.Rows[i1]["Head_ID"].ToString();
                            sb.Append("<tr class='Chead'>");
                            sb.Append("<td style='padding-left:20px;'><b>" + dt1.Rows[i1]["Head_Name"].ToString() + "</b></td>");
                            sb.Append("<td class='HRow' style='width:5%;'></td>");
                            sb.Append("</tr>");

                            DataView dv2 = new DataView();
                            dv2 = ds.Tables[1].DefaultView;
                            dv2.RowFilter = "Head_ParentID = '" + HeadID_Dt1 + "'";
                            DataTable dt2 = dv2.ToTable();
                            int dt2RowCount = dt2.Rows.Count;
                            for (int i2 = 0; i2 < dt2RowCount; i2++)
                            {

                                string HeadID_Dt2 = dt2.Rows[i2]["Head_ID"].ToString();
                                sb.Append("<tr class='Chead'>");
                                sb.Append("<td style='padding-left:30px;'><b>" + dt2.Rows[i2]["Head_Name"].ToString() + "</b></td>");
                                sb.Append("<td class='HRow' style='width:5%;'></td>");
                                sb.Append("</tr>");


                                DataView dv3 = new DataView();
                                dv3 = ds.Tables[1].DefaultView;
                                dv3.RowFilter = "Head_ParentID = '" + HeadID_Dt2 + "'";
                                DataTable dt3 = dv3.ToTable();
                                int dt3RowCount = dt3.Rows.Count;
                                for (int i3 = 0; i3 < dt3RowCount; i3++)
                                {

                                    string HeadID_Dt3 = dt3.Rows[i3]["Head_ID"].ToString();
                                    sb.Append("<tr class='Chead'>");
                                    sb.Append("<td style='padding-left:40px;'><b>" + dt3.Rows[i3]["Head_Name"].ToString() + "</b></td>");
                                    sb.Append("<td class='HRow' style='width:5%;'></td>");
                                    sb.Append("</tr>");

                                    DataView dv4 = new DataView();
                                    dv4 = ds.Tables[1].DefaultView;
                                    dv4.RowFilter = "Head_ParentID = '" + HeadID_Dt3 + "'";
                                    DataTable dt4 = dv4.ToTable();
                                    int dt4RowCount = dt4.Rows.Count;
                                    for (int i4 = 0; i4 < dt4RowCount; i4++)
                                    {

                                        string HeadID_Dt4 = dt4.Rows[i4]["Head_ID"].ToString();
                                        sb.Append("<tr class='Chead'>");
                                        sb.Append("<td style='padding-left:50px;'><b>" + dt4.Rows[i4]["Head_Name"].ToString() + "</b></td>");
                                        sb.Append("<td class='HRow' style='width:5%;'></td>");
                                        sb.Append("</tr>");

                                        DataView dv5 = new DataView();
                                        dv5 = ds.Tables[1].DefaultView;
                                        dv5.RowFilter = "Head_ParentID = '" + HeadID_Dt4 + "'";
                                        DataTable dt5 = dv5.ToTable();
                                        int dt5RowCount = dt5.Rows.Count;
                                        for (int i5 = 0; i5 < dt5RowCount; i5++)
                                        {

                                            string HeadID_Dt5 = dt5.Rows[i5]["Head_ID"].ToString();
                                            sb.Append("<tr class='Chead'>");
                                            sb.Append("<td style='padding-left:60px;'><b>" + dt5.Rows[i5]["Head_Name"].ToString() + "</b></td>");
                                            sb.Append("<td class='HRow' style='width:5%;'></td>");
                                            sb.Append("</tr>");

                                            DataView dv6 = new DataView();
                                            dv6 = ds.Tables[1].DefaultView;
                                            dv6.RowFilter = "Head_ParentID = '" + HeadID_Dt5 + "'";
                                            DataTable dt6 = dv6.ToTable();
                                            int dt6RowCount = dt6.Rows.Count;
                                            for (int i6 = 0; i6 < dt6RowCount; i6++)
                                            {

                                                string HeadID_Dt6 = dt6.Rows[i6]["Head_ID"].ToString();
                                                sb.Append("<tr class='Chead'>");
                                                sb.Append("<td style='padding-left:70px;'><b>" + dt6.Rows[i6]["Head_Name"].ToString() + "</b></td>");
                                                sb.Append("<td class='HRow' style='width:6%;'></td>");
                                                sb.Append("</tr>");


                                                DataView dv7 = new DataView();
                                                dv7 = ds.Tables[1].DefaultView;
                                                dv7.RowFilter = "Head_ParentID = '" + HeadID_Dt6 + "'";
                                                DataTable dt7 = dv7.ToTable();
                                                int dt7RowCount = dt7.Rows.Count;
                                                for (int i7 = 0; i7 < dt7RowCount; i7++)
                                                {

                                                    string HeadID_Dt7 = dt7.Rows[i7]["Head_ID"].ToString();
                                                    sb.Append("<tr class='Chead'>");
                                                    sb.Append("<td style='padding-left:80px;'><b>" + dt7.Rows[i7]["Head_Name"].ToString() + "</b></td>");
                                                    sb.Append("<td class='HRow' style='width:7%;'></td>");
                                                    sb.Append("</tr>");

                                                    DataView dv8 = new DataView();
                                                    dv8 = ds.Tables[1].DefaultView;
                                                    dv8.RowFilter = "Head_ParentID = '" + HeadID_Dt7 + "'";
                                                    DataTable dt8 = dv8.ToTable();
                                                    int dt8RowCount = dt8.Rows.Count;
                                                    for (int i8 = 0; i8 < dt8RowCount; i8++)
                                                    {

                                                        string HeadID_Dt8 = dt8.Rows[i8]["Head_ID"].ToString();
                                                        sb.Append("<tr class='Chead'>");
                                                        sb.Append("<td style='padding-left:90px;'><b>" + dt8.Rows[i8]["Head_Name"].ToString() + "</b></td>");
                                                        sb.Append("<td class='HRow' style='width:8%;'></td>");
                                                        sb.Append("</tr>");

                                                        sb.Append(SBLedgerDetail(HeadID_Dt8, ds, 100));
                                                    }
                                                    sb.Append(SBLedgerDetail(HeadID_Dt7, ds, 90));
                                                }
                                                sb.Append(SBLedgerDetail(HeadID_Dt6, ds, 80));
                                            }
                                            sb.Append(SBLedgerDetail(HeadID_Dt5, ds, 70));
                                        }
                                        sb.Append(SBLedgerDetail(HeadID_Dt4, ds, 60));

                                    }
                                    sb.Append(SBLedgerDetail(HeadID_Dt3, ds, 50));
                                }
                                sb.Append(SBLedgerDetail(HeadID_Dt2, ds, 40));
                            }
                            sb.Append(SBLedgerDetail(HeadID_Dt1, ds, 30));
                
                        }
                        sb.Append(SBLedgerDetail(HeadID, ds, 20));
                        
                    }
                    sb.Append("</tbody>");
                }
                sb.Append("</table>");
                DivTBMain.InnerHtml = sb.ToString();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select atleast one Office.');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

   
    protected void ddlRegionalOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillOffice();
    }

    protected string SBLedgerDetail(string HeadID,DataSet ds1,int px)
    {
        StringBuilder sbLedger = new StringBuilder();
        DataView dv = new DataView();
        dv = ds1.Tables[2].DefaultView;
        dv.RowFilter = "Ledger_HeadID = '" + HeadID + "'";
        DataTable dt = dv.ToTable();
        int dtRowCount = dt.Rows.Count;
        for (int i = 0; i < dtRowCount; i++)
        {
            sbLedger.Append("<tr>");
            sbLedger.Append("<td style='font-style: italic;padding-left:" + px.ToString() + "px;'>" + dt.Rows[i]["Ledger_Name"].ToString() + "</td>");
            sbLedger.Append("<td class='HRow' style='width:5%;'><a href='LedgerMasterB.aspx?Ledger_ID=" + objdb.Encrypt(dt.Rows[i]["Ledger_ID"].ToString()) + "&Mode=" + objdb.Encrypt("View") + "' target='_blank'>View</a></td>");
            sbLedger.Append("</tr>");
        }

        return sbLedger.ToString();

    }

}