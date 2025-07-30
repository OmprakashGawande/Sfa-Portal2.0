using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Linq;


public partial class mis_Finance_FinSupplierOrderBankPayment : System.Web.UI.Page
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
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                FillDropdown();
              
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillDropdown()
    {
        try
        {

            ds = objdb.ByProcedure("SpFinSupplierItem",
                        new string[] { "flag" },
                        new string[] { "8" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlPlant.DataSource = ds;
                ddlPlant.DataTextField = "Office_Name";
                ddlPlant.DataValueField = "Office_ID";
                ddlPlant.DataBind();
                // ddlPlant.Items.Insert(0, "Select");
                ddlPlant.Items.Insert(0, new ListItem("All", "0"));
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
            lblMsg.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();

            double SubTotal = 0.00;
            // Create DataTable  
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[9] 
            { new DataColumn("NameOfTheBank", typeof(string)),
              new DataColumn("NameOfTheBranch",typeof(string)),
              new DataColumn("NameOfAccountHolder",typeof(string)),
              new DataColumn("AcNoAsOnChkBook",typeof(string)),
              new DataColumn("IFSC",typeof(string)),
              new DataColumn("BankAddress",typeof(string)),
              new DataColumn("NetPayment",typeof(string)),
              new DataColumn("AuditNetPayment",typeof(string)),
              new DataColumn("SNo",typeof(string))
              
            });
            ds = null;
            ds = objdb.ByProcedure("SpFinSupplierOrder",
                    new string[] { "flag", "FromDate", "ToDate", "PlantID" },
                    new string[] { "21", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ddlPlant.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                //GridView1.DataSource = ds.Tables[0];
                //GridView1.DataBind();

                // Insert Row in DataTable
                int dsRowcount = ds.Tables[0].Rows.Count;
                double val123 = 0;

                double NetPaymentTotal = 0;
                double AuditNetPaymentTotal = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                   

                    dt.Rows.Add(
                          ds.Tables[0].Rows[i]["NameOfTheBank"].ToString()
                        , ds.Tables[0].Rows[i]["NameOfTheBranch"].ToString()
                         , ds.Tables[0].Rows[i]["NameOfAccountHolder"].ToString()
                        , "" + ds.Tables[0].Rows[i]["AcNoAsOnChkBook"].ToString()
                        //, "'" + ds.Tables[0].Rows[i]["Bank_AccountNo1"].ToString() + "'"
                        , ds.Tables[0].Rows[i]["IFSC"].ToString()
                        , ds.Tables[0].Rows[i]["BankAddress"].ToString()
                        , Math.Round(double.Parse(ds.Tables[0].Rows[i]["NetPayment"].ToString())).ToString()
                        , Math.Round(double.Parse(ds.Tables[0].Rows[i]["AuditNetPayment"].ToString())).ToString()
                        , (i + 1).ToString()
                        );

                     NetPaymentTotal = NetPaymentTotal + Math.Round(double.Parse(ds.Tables[0].Rows[i]["NetPayment"].ToString()));
                     AuditNetPaymentTotal = AuditNetPaymentTotal + Math.Round(double.Parse(ds.Tables[0].Rows[i]["AuditNetPayment"].ToString()));
                }
                if (dt.Rows.Count > 0)
                {
                  
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    decimal NetPayment = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("NetPayment"));
                    decimal AuditNetPayment = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("AuditNetPayment"));

                    GridView1.FooterRow.Cells[1].Text = "| TOTAL |";
                    GridView1.FooterRow.Cells[7].Text = "" + Math.Round(AuditNetPaymentTotal).ToString() + "";
                    GridView1.FooterRow.Cells[7].CssClass = "alignR";
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;

                   
                    
                    dt = null;
                }
                //GridView1.DataSource = dt;
                //GridView1.DataBind();
                //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                //GridView1.UseAccessibleHeader = true;

                //GridView1.Columns[2].Visible = true;
                //if (ddlEmp_TypeOfPost.SelectedValue.ToString() != "Permanent")
                //{
                //    GridView1.Columns[2].Visible = false;
                //}

            }
            //else if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
            //{
            //    GridView1.DataSource = ds.Tables[0];
            //    GridView1.DataBind();
            //}






        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}