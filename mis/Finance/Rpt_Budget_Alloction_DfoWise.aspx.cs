using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;


public partial class mis_Finance_Rpt_Budget_Alloction_DfoWise : System.Web.UI.Page
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
                lblMsg.Text = "";
                ViewState["Office_ID"] = Session["Office_ID"].ToString();

                GridView1.DataSource = null;
                GridView1.DataBind();
                FillDropdown();
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
           // ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
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
        if (Office != "")
        {
            ds = objdb.ByProcedure("Usp_RptFinBudgetAllocationUtilization",
                         new string[] { "FinancialYear", "Month", "Office_ID_mlt" },
                         new string[] { ddlFyear.SelectedValue, ddlMonth.SelectedValue, Office }, "dataset");

            btnPrint.Visible = false;
            btnExport.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnPrint.Visible = true;
                    btnExport.Visible = true;
                    lblHeader.Text = "<p class='text-center' style='font-weight:600'>Budget Utilization Report (" + ddlMonth.SelectedItem.Text + " - " + ddlFyear.SelectedItem.Text + " )" + "</br>[ " + OfficeName + " ]</p>";
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    decimal TotalAllocation = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Allocation"));
                    decimal TotalUtilization = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Utilization"));
                    GridView1.FooterRow.Cells[2].Text = "<b>Total : </b>";
                    GridView1.FooterRow.Cells[3].Text = "<b>" + TotalAllocation.ToString() + "</b>";
                    GridView1.FooterRow.Cells[4].Text = "<b>" + TotalUtilization.ToString() + "</b>";

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
            if (ddlFyear.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0  )
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = "";


            GridView1.DataSource = null;
            GridView1.DataBind();
            ddlFyear.ClearSelection();
            ddlRegionalOffice.ClearSelection();
            ddlOffice.Items.Clear();
            ddlOffice.Items.Insert(0, new ListItem("No Record Found", "0"));
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou", "Record Save successfully");
        }
    }
}