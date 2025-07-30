using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

public partial class mis_Finance_Budget_Alloction_DfoWise : System.Web.UI.Page
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
                btnSave.Visible = false;
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                GridView1.DataSource = null;
                GridView1.DataBind();
                FillDropdown();
                FillOffice();
                FillFYear();
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
            ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillGrid()
    {
        ds = objdb.ByProcedure("USP_Finance_Budget_Allocation_GetHead",
                          new string[] { "Office_ID", "Budget_Year", "Budget_Month" },
                          new string[] { ddlOffice.SelectedValue,ddlFyear.SelectedValue,ddlMonth.SelectedValue}, "dataset");
        btnSave.Visible = false;
        
        GridView1.DataSource = null;
        GridView1.DataBind();
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnSave.Visible = true;
              
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = "";
            btnSave.Visible = false;
           
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ddlFyear.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0 && ddlOffice.SelectedIndex > 0)
            {
                FillGrid();
                ddlFyear.Enabled = false;
                ddlMonth.Enabled = false;
                ddlRegionalOffice.Enabled = false;
                ddlOffice.Enabled = false;

            }
        }
    }
    protected void ddlRegionalOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        btnSave.Visible = false;
       
        GridView1.DataSource = null;
        GridView1.DataBind();
        FillOffice();
    }
    protected DataTable GetInsertData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Ledger_ID", typeof(int));
        dt.Columns.Add("Budget_Amount", typeof(decimal));
        foreach(GridViewRow rows in GridView1.Rows)
        {
            Label lblBudgetAllocation_ID = (Label)rows.FindControl("lblBudgetAllocation_ID");
            Label lblLedger_ID = (Label)rows.FindControl("lblLedger_ID");
            TextBox txtBudget_Amount = (TextBox)rows.FindControl("txtBudget_Amount");
            if(lblBudgetAllocation_ID.Text == "0")
            {
                dt.Rows.Add(lblLedger_ID.Text, txtBudget_Amount.Text);
            }
        }
        return dt;
    }
    protected DataTable GetUpdateData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("BudgetAllocation_ID", typeof(int));
        dt.Columns.Add("Ledger_ID", typeof(int));
        dt.Columns.Add("Budget_Amount", typeof(decimal));
        foreach (GridViewRow rows in GridView1.Rows)
        {
            Label lblBudgetAllocation_ID = (Label)rows.FindControl("lblBudgetAllocation_ID");
            Label lblLedger_ID = (Label)rows.FindControl("lblLedger_ID");
            TextBox txtBudget_Amount = (TextBox)rows.FindControl("txtBudget_Amount");
            if (lblBudgetAllocation_ID.Text != "0")
            {
                dt.Rows.Add(lblBudgetAllocation_ID.Text,lblLedger_ID.Text, txtBudget_Amount.Text);
            }
        }
        return dt;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = "";
            DataTable dt = GetInsertData();
            DataTable dt1 = GetUpdateData();
            ds = objdb.ByProcedure("Usp_InsertUpdateFinDfoWiseBudgetAllocation",
                                  new string[] {"Budget_Month"
                                              ,"Budget_Year"
                                              ,"Office_ID"
                                              ,"IsActive"
                                              ,"CreatedBy"
                                              ,"CreatedByIp"
                                  }
                               , new string[] {ddlMonth.SelectedValue
                                              ,ddlFyear.SelectedValue
                                              ,ddlOffice.SelectedValue
                                              ,"1"
                                              ,Session["Emp_ID"].ToString()
                                              ,api.GetLocalIPAddress()}
                              ,new string []{"type_InsertFinDfoWiseBudgetAllocation","type_UpdateFinDfoWiseBudgetAllocation"}
                              ,new DataTable[] {dt,dt1},
                              "TableSave"
                              );
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows[0]["msg"].ToString() == "Ok")
                {
                    lblMsg.Text = objdb.Alert("fa-check","alert-success","ThankYou!",ds.Tables[0].Rows[0]["Errormsg"].ToString());
                }
                else
                {
                     lblMsg.Text = objdb.Alert("fa-ban","alert-danger","Sorry!",ds.Tables[0].Rows[0]["Errormsg"].ToString());
                }
            }
            FillGrid();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlFyear.Enabled = true;
        ddlMonth.Enabled = true;
        ddlRegionalOffice.Enabled = true;
        ddlOffice.Enabled = true;
        FillDropdown();
        ddlRegionalOffice_SelectedIndexChanged(sender, e);
    }
}