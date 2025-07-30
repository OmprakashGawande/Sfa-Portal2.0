using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Finance_FinSetEditVDate : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();
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
                    FillGrid();
                    FillDate();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "!Sorry", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinSetEditVDate", new string[] { "flag"}, new string[] {"0"}, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "!Sorry", ex.Message.ToString());
        }
    }
    protected void txtBulkDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBulkDate.Text != "")
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    TextBox EditDate = (TextBox)row.FindControl("txtEditDate");
                    EditDate.Text = txtBulkDate.Text;
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "!Sorry", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            if (GridView1.Rows.Count > 0)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    TextBox txtEditDate = (TextBox)row.FindControl("txtEditDate");
                    if (txtEditDate.Text == "")
                        {
                            msg = msg + "Date cannot be Blank.\\n";
                        }
                }
            }
            if(msg == "")
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    TextBox EditDate = (TextBox)row.FindControl("txtEditDate");
                    Label ID = (Label)row.FindControl("lblRowNumber");
                    string OfficeID = ID.ToolTip.ToString();
                    string date = Convert.ToDateTime(EditDate.Text, cult).ToString("MM/dd/yyyy");
                    objdb.ByProcedure("SpFinSetEditVDate", new string[] { "flag", "Office_ID", "Date", "UpdatedBy" }, new string[] { "1", OfficeID, date.ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                }
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Opearetion Successfully Completed.");
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "!Sorry", ex.Message.ToString());
        }
    }
    protected void FillDate()
    {
        try
        {
            foreach (GridViewRow rows in GridView1.Rows)
            {

                TextBox EditDate = (TextBox)rows.FindControl("txtEditDate");
                Label ID = (Label)rows.FindControl("lblRowNumber");
                string OfficeID = ID.ToolTip.ToString();
                ds = objdb.ByProcedure("SpFinSetEditVDate", new string[] {"flag" }, new string[] {"4" }, "dataset");
                if(ds!= null && ds.Tables[0].Rows.Count > 0)
                {
                    int count = ds.Tables[0].Rows.Count;
                    for(int i=0;i < count;i++)
                    {
                        string Office_ID = ds.Tables[0].Rows[i]["Office_ID"].ToString();
                        string Date = ds.Tables[0].Rows[i]["Date"].ToString();
                        if(OfficeID == Office_ID)
                        {
                            EditDate.Text = Date.ToString();
                        }

                    }

                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "!Sorry", ex.Message.ToString());
        }
    }
}