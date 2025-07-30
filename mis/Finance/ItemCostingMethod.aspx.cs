using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_Finance_ItemCostingMethod : System.Web.UI.Page
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
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillOffice();
                    FillItem();
                    ddlOffice.Enabled = false;


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
    protected void FillOffice()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinItemCostingMethod",
                   new string[] { "flag" },
                   new string[] { "0" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillItem()
    {
        try
        {

            ds = objdb.ByProcedure("SpFinItemCostingMethod",
               new string[] { "flag", "Office_ID" },
               new string[] { "1", ddlOffice.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlItem.DataSource = ds;
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "Item_id";
                ddlItem.DataBind();
                ddlItem.Items.Insert(0, new ListItem("Select", "0"));

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            decimal StandardCost;
            lblMsg.Text = "";
            string msg = "";
            if (ddlItem.SelectedIndex == 0)
            {
                msg += "Select Item.\\n";
            }
            if (ddlcostingmethod.SelectedIndex == 0)
            {
                msg += "Select Costing Method.\\n";
            }

            if (txtFromDate.Text == "")
            {
                msg += "Select From Date. \\n";
            }
            if (txtToDate.Text == "")
            {
                msg += "Select To Date. \\n";
            }
            if (ddlcostingmethod.SelectedIndex > 0)
            {
                if (ddlcostingmethod.SelectedIndex == 2)
                {
                    if (txtstandardcost.Text == "")
                    {
                        msg += "Enter Standard Cost.";
                    }
                }

            }
            if (msg == "")
            {
                if (txtstandardcost.Text == "")
                {
                    StandardCost = 0;
                }
                else
                {
                    StandardCost = decimal.Parse(txtstandardcost.Text);
                }

                ds = objdb.ByProcedure("SpFinItemCostingMethod", new string[] { "flag", "Office_ID", "Item_id", "FromDate", "ToDate" }, new string[] { "5", ddlOffice.SelectedValue.ToString(), ddlItem.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[0].Rows[0]["TotalRecords"].ToString() == "0")
                    {
                        objdb.ByProcedure("SpFinItemCostingMethod", new string[] { "flag", "Office_ID", "Item_id", "ItemCostingMethod", "ItemStandardCost", "CreatedBy", "FromDate", "ToDate" }, new string[] { "2", ddlOffice.SelectedValue.ToString(), ddlItem.SelectedValue.ToString(), ddlcostingmethod.SelectedItem.Text.ToString(), StandardCost.ToString(), ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                        ddlItem.ClearSelection();
                        ddlcostingmethod.ClearSelection();
                        txtstandardcost.Text = "";
                        txtstandardcost.Enabled = true;
                        ClearText();
                    }
                    else
                    {

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Costing already exists. Please Change the Dates.');", true);
                    }

                }



            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlcostingmethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            EnableDisableStandardCost();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void EnableDisableStandardCost()
    {
        try
        {
            lblMsg.Text = "";
            if (ddlcostingmethod.SelectedIndex > 0)
            {
                if (ddlcostingmethod.SelectedValue.ToString() == "Average Costing")
                {
                    txtstandardcost.Enabled = false;
                    txtstandardcost.Text = "";
                }
                else if (ddlcostingmethod.SelectedValue.ToString() == "Standard Costing")
                {
                    txtstandardcost.Enabled = true;
                    txtstandardcost.Text = "";
                }
                else if (ddlcostingmethod.SelectedValue.ToString() == "NRV")
                {
                    txtstandardcost.Enabled = false;
                    txtstandardcost.Text = "";

                }
                else if (ddlcostingmethod.SelectedValue.ToString() == "FIFO")
                {
                    txtstandardcost.Enabled = false;
                    txtstandardcost.Text = "";

                }
            }
            else
            {
                txtstandardcost.Enabled = true;
                txtstandardcost.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string CostingId = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpFinItemCostingMethod",
                   new string[] { "flag", "CostingId", "CreatedBy" },
                   new string[] { "4", CostingId, ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();
            ClearText();
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
            //lblMsg.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ddlItem.SelectedIndex > 0)
            {

                ds = objdb.ByProcedure("SpFinItemCostingMethod", new string[] { "flag", "Item_id", "Office_ID" }, new string[] { "3", ddlItem.SelectedValue.ToString(), ddlOffice.SelectedValue.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        ddlItem.ClearSelection();
        ddlcostingmethod.ClearSelection();
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtstandardcost.Text = "";
        //ddlHoliday_Type.ClearSelection();
        //txtHoliday_Name.Text = "";
        //txtHoliday_Date.Text = "";
    }
}