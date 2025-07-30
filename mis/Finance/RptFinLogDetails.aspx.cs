using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;

public partial class mis_Finance_RptFinLogDetails : System.Web.UI.Page
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
                    FillOfficeType();
                    FillGrid();
                   
                }
                lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt");
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void FillOfficeType()
    {
        try
        {
            
            ddlOfficeType.Items.Clear();
            ds = objdb.ByProcedure("USP_FinGetOfficeType", new string[] { }, new string[] { }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    ddlOfficeType.DataSource = ds;
                    ddlOfficeType.DataTextField = "OfficeType_Title";
                    ddlOfficeType.DataValueField = "OfficeType_ID";
                    ddlOfficeType.DataBind();
                    ddlOfficeType.Items.Insert(0, new ListItem("All [PCS -Not Included]", "0"));
                    
                }
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillGrid()
    {
        try
        {
            Repeater1.DataSource = new string[] { };
            Repeater1.DataBind();
            int cssStrong = 0;
            int cssweek = 0;
            lblcssStrong.Text = "&nbsp;&nbsp;&nbsp;";
            lblcssweek.Text = "&nbsp;&nbsp;&nbsp;";
            ds = objdb.ByProcedure("SpLogDetail", new string[] { "flag", "OfficeType_ID" }, new string[] { "2",ddlOfficeType.SelectedValue }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                int rowcount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < rowcount; i++)
                {
                    string RowColor = ds.Tables[0].Rows[i]["csscolor"].ToString();
                    if (RowColor == "cssStrong")
                        cssStrong++;
                    else
                        cssweek++;

                }

            }
            lblcssStrong.Text = cssStrong.ToString();
            lblcssweek.Text = cssweek.ToString();
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            FillGrid();

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlOfficeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
}
