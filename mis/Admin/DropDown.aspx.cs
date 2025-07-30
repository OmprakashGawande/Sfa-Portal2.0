using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_Admin_DropDown : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ds = objdb.ByProcedure("SpFinHeadMaster",
                        new string[] { "flag" },
                        new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {

                ddlHeadName.DataTextField = "HeadName";
                ddlHeadName.DataValueField = "Head_ID";
                ddlHeadName.DataSource = ds;
                ddlHeadName.DataBind();
                ddlHeadName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
    }
    protected void ddlHeadName_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ID = ddlHeadName.SelectedValue;
        Response.Write("<script>console.log(" + ID + ");</script>");
    }
}