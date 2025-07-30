using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_Default : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster",
                        new string[] { "flag" },
                        new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlLedger.DataTextField = "Ledger_Name";
                ddlLedger.DataValueField = "Ledger_ID";
                ddlLedger.DataSource = ds;
                ddlLedger.DataBind();
                ddlLedger.Items.Insert(0, "Select");
            }
        }
    }
}