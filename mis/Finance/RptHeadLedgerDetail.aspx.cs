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

public partial class mis_Finance_RptHeadLedgerDetail : System.Web.UI.Page
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
                ds = objdb.ByProcedure("SpFinHeadLedgerDetail", new string[] { "flag" }, new string[] { "1" }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    sb.Append("<tbody>");

                    int rowcount = ds.Tables[0].Rows.Count;



                    for (int i = 0; i < rowcount; i++)
                    {
                        int Grid1 = 0; int Grid2 = 0;

                        StringBuilder sbChild = new StringBuilder();

                        ds1 = objdb.ByProcedure("SpFinHeadLedgerDetail",
                            new string[] { "flag", "Office_ID_Mlt", "Head_ID" },
                            new string[] { "2", Office, ds.Tables[0].Rows[i]["Head_ID"].ToString() }, "dataset");
                        if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                        {

                            Grid1 = 1;

                            int rowcountChild = ds1.Tables[0].Rows.Count;
                            for (int k = 0; k < rowcountChild; k++)
                            {
                                string HeadID = ds1.Tables[0].Rows[k]["Head_ID"].ToString();
                                sbChild.Append("<tr class='Chead'>");
                                sbChild.Append("<td style='padding-left:13px;'><b><a onclick='GetHeadData(" + HeadID + ")'>" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</a></b></td>");
                                sbChild.Append("<td class='HRow' style='width:5%;'><a id='at" + HeadID.ToString() + "' onclick='HideFun(" + HeadID.ToString() + ")'  class='backCss'></a></td>");
                                sbChild.Append("</tr>");
                                sbChild.Append("<tr> <td class='CHeadC' colspan='2' id='td" + HeadID + "'></ <td>");
                            }

                        }

                        ds1 = objdb.ByProcedure("SpFinHeadLedgerDetail", new string[] { "flag", "Office_ID_Mlt", "Head_ID" },
                            new string[] { "3", Office, ds.Tables[0].Rows[i]["Head_ID"].ToString() }, "dataset");
                        if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                        {
                            Grid2 = 1;
                            int rowcountChild = ds1.Tables[0].Rows.Count;
                            for (int k = 0; k < rowcountChild; k++)
                            {
                                sbChild.Append("<tr>");
                                sbChild.Append("<td style='padding-left:23px;font-style: italic;'>" + ds1.Tables[0].Rows[k]["Ledger_Name"].ToString() + "</td>");
                                sbChild.Append("<td class='HRow' style='width:5%;'><a href='LedgerMasterB.aspx?Ledger_ID=" + objdb.Encrypt(ds1.Tables[0].Rows[k]["Ledger_ID"].ToString()) + "&Mode=" + objdb.Encrypt("View") + "' target='_blank'>View</a></td>");
                                sbChild.Append("</tr>");
                            }
                        }
                        sb.Append("<tr class='Mhead'>");
                        sb.Append("<td style='padding-left:4px;'>" + ds.Tables[0].Rows[i]["Head_Name"].ToString() + "</td>");
                        sb.Append("<td class='HRow'></td>");
                        sb.Append("</tr>");
                        if (Grid1 != 0 || Grid2 != 0)
                        {
                            sb.Append(" " + sbChild.ToString() + " ");
                        }

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

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetHeadDetail(string HeadID)
    {
        AbstApiDBApi objdbWeb = new APIProcedure();
        CultureInfo cult = new CultureInfo("gu-IN", true);
        DataSet ds1;
        string Office = HttpContext.Current.Session["Office"].ToString();
        StringBuilder sbChild = new StringBuilder();
        sbChild.Append("<table class='table table-hover table-bordered' style='margin-bottom: 0px;'><tbody>");
        ds1 = objdbWeb.ByProcedure("SpFinHeadLedgerDetail",
             new string[] { "flag", "Office_ID_Mlt", "Head_ID" },
             new string[] { "2", Office, HeadID.ToString() }, "dataset");
        if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
        {

            // Opening Balance
            int rowcountChild = ds1.Tables[0].Rows.Count;
            for (int k = 0; k < rowcountChild; k++)
            {
                string HeadChild = ds1.Tables[0].Rows[k]["Head_ID"].ToString();
                sbChild.Append("<tr class='Chead'>");
                sbChild.Append("<td style='padding-left:13px;width:94.8%;'><b><a onclick='GetHeadData(" + HeadChild.ToString() + ")'>" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</a></b></td>");
                sbChild.Append("<td class='HRow Wcss' style='width:5.2%;'></td>");
                sbChild.Append("</tr>");
                sbChild.Append("<tr> <td class='CHeadC' colspan='2' id='td" + HeadChild + "'></ <td>");
            }

        }

        ds1 = objdbWeb.ByProcedure("SpFinHeadLedgerDetail", new string[] { "flag", "Office_ID_Mlt", "Head_ID" },
            new string[] { "3", Office, HeadID.ToString() }, "dataset");
        if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
        {
            int rowcountChild = ds1.Tables[0].Rows.Count;
            for (int k = 0; k < rowcountChild; k++)
            {
                sbChild.Append("<tr>");
                sbChild.Append("<td style='padding-left:23px;font-style: italic;width:94.8%;' >" + ds1.Tables[0].Rows[k]["Ledger_Name"].ToString() + "</td>");
                sbChild.Append("<td class='HRow Wcss' style='width:5.2%;'><a href='LedgerMasterB.aspx?Ledger_ID=" + objdbWeb.Encrypt(ds1.Tables[0].Rows[k]["Ledger_ID"].ToString()) + "&Mode=" + objdbWeb.Encrypt("View") + "' target='_blank'>View</a></td>");
                sbChild.Append("</tr>");
            }
        }
        sbChild.Append("</tbody></Table>");
        return sbChild.ToString();
    }

    protected void ddlRegionalOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillOffice();
    }

}