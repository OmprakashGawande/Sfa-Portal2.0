using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class mis_Finance_ChequeBookDetail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {

                lblMsg.Text = "";
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillGrid();
                    FillYear();
                    FillLedger();
                    

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
    protected void FillLedger()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] {"flag","Year"}, new string[] {"7",ddlFinancialYear.SelectedValue.ToString()}, "dataset");
            if(ds!= null && ds.Tables[0].Rows.Count > 0)
            {
                ddlLedger.DataTextField = "Ledger_Name";
                ddlLedger.DataValueField = "Ledger_ID";
                ddlLedger.DataSource = ds;
                ddlLedger.DataBind();
                ddlLedger.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlLedger.Items.Clear();
                ddlLedger.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillYear()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpHrYear_Master",
                          new string[] { "flag" },
                          new string[] { "2" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlFinancialYear.DataTextField = "Financial_Year";
                ddlFinancialYear.DataValueField = "Year";
                ddlFinancialYear.DataSource = ds;
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlFinancialYear.Items.Clear();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string ChequeBook_IsActive = "1";
            if(ddlLedger.SelectedIndex == 0)
            {
                msg = msg + "Select Ledger. <br/>";
            }
            if (txtChqFrom.Text.Trim() == "")
            {
                msg = msg + "Enter Cheque No. From. <br/>";
            }
            if (txtChqTo.Text.Trim() == "")
            {
                msg = msg + "Enter Cheque No. To. <br/>";
            }
            if (txtNoOfChq.Text.Trim() == "")
            {
                msg = msg + "Enter No Of Cheques. <br/>";
            }
            if (txtChqBkName.Text.Trim() == "")
            {
                msg = msg + "Enter Cheque Book Name. <br/>";
            }
            if(msg == "")
            {
                if(btnSave.Text == "Save")
                {
                    objdb.ByProcedure("SpFinChequeBookDetail", new string[] { "flag", "Ledger_ID", "ChequeBook_IsActive", "Office_ID", "Year", "ChequeNoFrom", "ChequeNoTo", "NoOfCheques", "ChequeBookName", "UpdatedBy" }, new string[] { "0", ddlLedger.SelectedValue.ToString(), ChequeBook_IsActive, ViewState["Office_ID"].ToString(), ddlFinancialYear.SelectedValue.ToString(), txtChqFrom.Text, txtChqTo.Text, txtNoOfChq.Text, txtChqBkName.Text, ViewState["Emp_ID"].ToString()}, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    CleatText();
                    FillGrid();
                }
                else if(btnSave.Text == "Update")
                {
                    objdb.ByProcedure("SpFinChequeBookDetail", new string[] { "flag","ChequeBook_ID", "Ledger_ID","Year", "ChequeNoFrom", "ChequeNoTo", "NoOfCheques", "ChequeBookName", "UpdatedBy" }, new string[] { "3",ViewState["ChequeBook_ID"].ToString(),ddlLedger.SelectedValue.ToString(), ddlFinancialYear.SelectedValue.ToString(), txtChqFrom.Text, txtChqTo.Text, txtNoOfChq.Text, txtChqBkName.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    CleatText();
                    FillGrid();
                    btnSave.Text = "Save";
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
    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Gvchqdetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            Gvchqdetails.UseAccessibleHeader = true;
            if(ddlFinancialYear.SelectedIndex > 0)
            {
                FillLedger();
            }
            else
            {
                ddlLedger.Items.Clear();
                ddlLedger.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void CleatText()
    {
        try
        {
            ddlFinancialYear.ClearSelection();
            ddlLedger.ClearSelection();
            txtChqBkName.Text = "";
            txtChqFrom.Text = "";
            txtChqTo.Text = "";
            txtNoOfChq.Text = "";
            
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
            
            ds = objdb.ByProcedure("SpFinChequeBookDetail", new string[] {"flag" }, new string[] {"5" }, "dataset");  
            if(ds!= null && ds.Tables[0].Rows.Count > 0)
            {
                Gvchqdetails.DataSource = ds;
            }
            else if (ds != null && ds.Tables[0].Rows.Count == 0)
            {
                Gvchqdetails.DataSource = ds;
                
            }
            else
            {
                Gvchqdetails.DataSource = null;

            }
            Gvchqdetails.DataBind();
            Gvchqdetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            Gvchqdetails.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Gvchqdetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Gvchqdetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            Gvchqdetails.UseAccessibleHeader = true;
            string ChequeBook_ID = Gvchqdetails.SelectedDataKey.Value.ToString();
            ViewState["ChequeBook_ID"] = ChequeBook_ID;
            DataSet ds1 = objdb.ByProcedure("SpFinChequeBookDetail", new string[] { "flag", "ChequeBook_ID" }, new string[] { "6",ChequeBook_ID }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                ddlFinancialYear.ClearSelection();
                ddlFinancialYear.Items.FindByValue(ds1.Tables[0].Rows[0]["Year"].ToString()).Selected = true;
                ddlLedger.ClearSelection();
                FillLedger();
                ddlLedger.Items.FindByValue(ds1.Tables[0].Rows[0]["Ledger_ID"].ToString()).Selected = true;
                txtChqFrom.Text = ds1.Tables[0].Rows[0]["ChequeNoFrom"].ToString();
                txtChqTo.Text = ds1.Tables[0].Rows[0]["ChequeNoTo"].ToString();
                txtNoOfChq.Text = ds1.Tables[0].Rows[0]["NoOfCheques"].ToString();
                txtChqBkName.Text = ds1.Tables[0].Rows[0]["ChequeBookName"].ToString();
                btnSave.Text = "Update";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Gvchqdetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Gvchqdetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            Gvchqdetails.UseAccessibleHeader = true;
            string ChequeBook_ID = Gvchqdetails.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpFinChequeBookDetail", new string[] { "flag", "ChequeBook_ID", "UpdatedBy" }, new string[] { "4", ChequeBook_ID, ViewState["Emp_ID"].ToString() }, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}