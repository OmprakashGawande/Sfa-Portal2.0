using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using System.Configuration;
using System.Web;
using System.Drawing;


public partial class mis_Finance_RptBudget_Alloction_DfoWise : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();

    APIProcedure api = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                lblMsg.Text = "";
               
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                GridView1.DataSource = null;
                GridView1.DataBind();
                FillDropdown();
                FillOffice();
                FillFYear();
                btnPrint.Visible = false;
                btnExport.Visible = false;
            }
        }
    }
    protected void FillFYear()
    {
        try
        {
            ddlFyear.Items.Clear();
            for (int i = ((DateTime.Now.Year) - 3); i <= ((DateTime.Now.Year)); i++)
            {
                string fyer = (i + 1).ToString();
                ddlFyear.Items.Add(i.ToString() + "-" + fyer.Substring(2));
            }
            ddlFyear.DataBind();
            ddlFyear.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    protected void FillDropdown()
    {
        try
        {

            ddlOffice.Enabled = true;
            ddlRegionalOffice.Enabled = true;
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

                //if (ViewState["OfficeType_Title"].ToString() == "Circle Office (CCF)")
                //{
                //    ddlRegionalOffice.Enabled = false;
                //    ddlRegionalOffice.SelectedValue = ViewState["Division_ID"].ToString();
                //}
            }

            FillOffice();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    //protected void FillOffice()
    //{
    //    try
    //    {


    //        ddlOffice.Items.Clear();
    //        if (ddlRegionalOffice.SelectedIndex > 0)
    //        {
                
    //                ds = objdb.ByProcedure("SpAdminOffice",
    //                       new string[] { "flag", "Office_ID" },
    //                       new string[] { "23", ddlRegionalOffice.SelectedValue.ToString() }, "dataset");
    //                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //                {
    //                    ddlOffice.DataSource = ds;
    //                    ddlOffice.DataTextField = "Office_Name";
    //                    ddlOffice.DataValueField = "Office_ID";
    //                    ddlOffice.DataBind();
    //                    ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
    //                    //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
    //                    // ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
    //                }
                
              
    //        }
    //        else
    //        {
    //            ddlOffice.Items.Insert(0, new ListItem("No Record Found", "0"));
    //        }
    //        //else
    //        //{
    //        //    ds = objdb.ByProcedure("SpAdminOffice",
    //        //            new string[] { "flag" },
    //        //            new string[] { "22" }, "dataset");
    //        //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        //    {
    //        //        ddlOffice.DataSource = ds;
    //        //        ddlOffice.DataTextField = "Office_Name";
    //        //        ddlOffice.DataValueField = "Office_ID";
    //        //        ddlOffice.DataBind();
    //        //        //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
    //        //        ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
    //        //    }
    //        //}

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

    //    }
    //}
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
            //ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillGrid()
    {
        string Office = "";
        string OfficeName = "";
        int SerialNo = 0;
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
        //ds = objdb.ByProcedure("USP_Finance_Budget_Allocation_GetHead",
        //                  new string[] { "Office_ID", "Budget_Year", "Budget_Month" },
        //                  new string[] { ddlOffice.SelectedValue,ddlFyear.SelectedValue,ddlMonth.SelectedValue}, "dataset");
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
            ds = objdb.ByProcedure("USP_GetFinance_Budget_Allocation_Report",
                        new string[] { "Office_ID_Mlt", "Budget_Year", "Budget_Month" },
                        new string[] { Office, ddlFyear.SelectedValue, ddlMonth.SelectedValue }, "dataset");
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnPrint.Visible = true;
                    btnExport.Visible = true;
                    lblHeader.Text = "<p class='text-center' style='font-weight:600'>Budget Allocation Report (" + ddlMonth.SelectedItem.Text + " - " + ddlFyear.SelectedItem.Text + " )" + "</br>[" + OfficeName + "]</span></p>";
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    decimal Total = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Budget_Amount"));
                    GridView1.FooterRow.Cells[2].Text = "<b>Total : </b>";
                    GridView1.FooterRow.Cells[3].Text = "<b>" + Total.ToString() + "</b>";
                }
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select atleast one Office.');", true);
        }
       
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = "";
            
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ddlFyear.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
            {
                FillGrid();
               

            }
        }
    }
    protected void ddlRegionalOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        
        GridView1.DataSource = null;
        GridView1.DataBind();
        FillOffice();
    }

    
}