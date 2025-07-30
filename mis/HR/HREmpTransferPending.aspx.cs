using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HREmpTransferPending : System.Web.UI.Page
{
    DataSet ds;
    DataSet ds1;
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
                    txtNewJoiningDate.Attributes.Add("readonly", "readonly");
                    FillGrid();
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
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();

            ds = objdb.ByProcedure("SpHRTransfer", new string[] { "flag", "NewOffice" }, new string[] { "10", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                //GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            txtNewJoiningDate.Text = "";
            ViewState["TransferID"] = GridView1.SelectedDataKey.Value.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal();", true);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            lblMsg.Text = "";
            if (txtNewJoiningDate.Text.Trim() == "")
            {
                msg += "Enter New Joining Date. \\n";
            }
            if (msg.Trim() == "")
            {
                ds = objdb.ByProcedure("SpHRTransfer", new string[] { "flag", "TransferID", "NewJoiningDate", "TransferUpdatedBy" },
                    new string[] { "11", ViewState["TransferID"].ToString(), Convert.ToDateTime(txtNewJoiningDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Emp_ID"].ToString() }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "True")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Transfer Successfully Completed");
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Transfer not Completed please try again.");
                    }

                }


                FillGrid();

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
}