using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HrEmpDynamicReport : System.Web.UI.Page
{
    AbstApiDBApi objdb = new APIProcedure();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillDropdown();
                FillDesignation();
                //  FillGrid();
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
            // Office
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "10" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                // ddlOffice.Items.Insert(0, new ListItem("All", "0"));

                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
            ds = null;
            // Class
            ds = objdb.ByProcedure("SpHRClass", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                ddlEmp_Class.DataSource = ds;
                ddlEmp_Class.DataTextField = "Class_Name";
                ddlEmp_Class.DataValueField = "Class_Name";
                ddlEmp_Class.DataBind();
               // ddlEmp_Class.Items.Insert(0, new ListItem("All", "0"));
                // ddlDesignation_ID.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlEmp_Class_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            ddlDesignation_ID.Items.Clear();
           // ddlDesignation_ID.Items.Insert(0, new ListItem("All", "0"));
            ds = null;
            FillDesignation();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal1()", true);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDesignation()
    {
        string Class = "";
        foreach (ListItem item in ddlEmp_Class.Items)
        {
            if (item.Selected)
            {
                Class += item.Value + ",";
            }
        }
        ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Class" }, new string[] { "37", Class }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlDesignation_ID.DataSource = ds;
            ddlDesignation_ID.DataTextField = "Designation_Name";
            ddlDesignation_ID.DataValueField = "Designation_ID";
            ddlDesignation_ID.DataBind();
            // ddlDesignation_ID.Items.Insert(0, new ListItem("All", "0"));
        }
    }
    protected void FillGrid()
    {
        try
        {
            ShowColumn();
            GridView1.DataSource = null;
            GridView1.DataBind();
            string Office = "";
            string Class = "";
            string NDesignation = "";

            string Emp_TypeOfPost = "";
            string msg = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            if (Office == "")
            {
                msg += "Select Office. \\n";
            }
            foreach (ListItem item in ddlEmp_Class.Items)
            {
                if (item.Selected)
                {
                    Class += item.Value + ",";
                }
            }
            if (Class == "")
            {
                msg += "Select Class. \\n";
            }
            foreach (ListItem item in ddlDesignation_ID.Items)
            {
                if (item.Selected)
                {
                    NDesignation += item.Value + ",";
                }
            }
            if (NDesignation == "")
            {
                msg += "Select Designation. \\n";
            }
            foreach (ListItem item in ddlEmp_TypeOfPost.Items)
            {
                if (item.Selected)
                {
                    Emp_TypeOfPost += item.Value + ",";
                }
            }
            if (Emp_TypeOfPost == "")
            {
                msg += "Select Type Of Post. \\n";
            }
            if (msg == "")
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
                ds = objdb.ByProcedure("SpHREmployee",
                    new string[] { "flag", "Office", "Class", "NDesignation", "TypeOfPost" },
                    new string[] { "36", Office, Class, NDesignation, Emp_TypeOfPost }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                   // HideColumn();
                }
                HideColumn();
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
    protected void ShowColumn()
    {
        try
        {
            GridView1.Columns[1].Visible = true;
            GridView1.Columns[2].Visible = true;
            GridView1.Columns[3].Visible = true;
            GridView1.Columns[4].Visible = true;
            GridView1.Columns[5].Visible = true;
            GridView1.Columns[6].Visible = true;
            GridView1.Columns[7].Visible = true;
            GridView1.Columns[8].Visible = true;
            GridView1.Columns[9].Visible = true;
            GridView1.Columns[10].Visible = true;
            GridView1.Columns[11].Visible = true;
            GridView1.Columns[12].Visible = true;
            GridView1.Columns[13].Visible = true;
            GridView1.Columns[14].Visible = true;
            GridView1.Columns[15].Visible = true;
            GridView1.Columns[16].Visible = true;
            GridView1.Columns[17].Visible = true;
            GridView1.Columns[18].Visible = true;
            GridView1.Columns[19].Visible = true;
            GridView1.Columns[20].Visible = true;
            GridView1.Columns[21].Visible = true;
            GridView1.Columns[22].Visible = true;
            GridView1.Columns[23].Visible = true;
            GridView1.Columns[24].Visible = true;
            GridView1.Columns[25].Visible = true;
            GridView1.Columns[26].Visible = true;
            GridView1.Columns[27].Visible = true;
            GridView1.Columns[28].Visible = true;
            GridView1.Columns[29].Visible = true;
            GridView1.Columns[30].Visible = true;
            GridView1.Columns[31].Visible = true;
            GridView1.Columns[32].Visible = true;
            GridView1.Columns[33].Visible = true;
            GridView1.Columns[34].Visible = true;
            GridView1.Columns[35].Visible = true;
            GridView1.Columns[36].Visible = true;
            GridView1.Columns[37].Visible = true;
            GridView1.Columns[38].Visible = true;
            GridView1.Columns[39].Visible = true;
            GridView1.Columns[40].Visible = true;
            GridView1.Columns[41].Visible = true;
            GridView1.Columns[41].Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void HideColumn()
    {
        try
        {
            if (chk1.Checked == true)
                GridView1.Columns[1].Visible = true;
            else
                GridView1.Columns[1].Visible = false;

            if (chk2.Checked == true)
                GridView1.Columns[2].Visible = true;
            else
                GridView1.Columns[2].Visible = false;

            if (chk3.Checked == true)
                GridView1.Columns[3].Visible = true;
            else
                GridView1.Columns[3].Visible = false;

            if (chk4.Checked == true)
                GridView1.Columns[4].Visible = true;
            else
                GridView1.Columns[4].Visible = false;

            if (chk5.Checked == true)
                GridView1.Columns[5].Visible = true;
            else
                GridView1.Columns[5].Visible = false;

            if (chk6.Checked == true)
                GridView1.Columns[6].Visible = true;
            else
                GridView1.Columns[6].Visible = false;

            if (chk7.Checked == true)
                GridView1.Columns[7].Visible = true;
            else
                GridView1.Columns[7].Visible = false;

            if (chk8.Checked == true)
                GridView1.Columns[8].Visible = true;
            else
                GridView1.Columns[8].Visible = false;

            if (chk9.Checked == true)
                GridView1.Columns[9].Visible = true;
            else
                GridView1.Columns[9].Visible = false;

            if (chk10.Checked == true)
                GridView1.Columns[10].Visible = true;
            else
                GridView1.Columns[10].Visible = false;

            if (chk11.Checked == true)
                GridView1.Columns[11].Visible = true;
            else
                GridView1.Columns[11].Visible = false;

            if (chk12.Checked == true)
                GridView1.Columns[12].Visible = true;
            else
                GridView1.Columns[12].Visible = false;

            if (chk13.Checked == true)
                GridView1.Columns[13].Visible = true;
            else
                GridView1.Columns[13].Visible = false;

            if (chk14.Checked == true)
                GridView1.Columns[14].Visible = true;
            else
                GridView1.Columns[14].Visible = false;

            if (chk15.Checked == true)
                GridView1.Columns[15].Visible = true;
            else
                GridView1.Columns[15].Visible = false;

            if (chk16.Checked == true)
                GridView1.Columns[16].Visible = true;
            else
                GridView1.Columns[16].Visible = false;

            if (chk17.Checked == true)
                GridView1.Columns[17].Visible = true;
            else
                GridView1.Columns[17].Visible = false;

            if (chk18.Checked == true)
                GridView1.Columns[18].Visible = true;
            else
                GridView1.Columns[18].Visible = false;

            if (chk19.Checked == true)
                GridView1.Columns[19].Visible = true;
            else
                GridView1.Columns[19].Visible = false;

            if (chk20.Checked == true)
                GridView1.Columns[20].Visible = true;
            else
                GridView1.Columns[20].Visible = false;

            if (chk21.Checked == true)
                GridView1.Columns[21].Visible = true;
            else
                GridView1.Columns[21].Visible = false;

            if (chk22.Checked == true)
                GridView1.Columns[22].Visible = true;
            else
                GridView1.Columns[22].Visible = false;

            if (chk23.Checked == true)
                GridView1.Columns[23].Visible = true;
            else
                GridView1.Columns[23].Visible = false;

            if (chk24.Checked == true)
                GridView1.Columns[24].Visible = true;
            else
                GridView1.Columns[24].Visible = false;

            if (chk25.Checked == true)
                GridView1.Columns[25].Visible = true;
            else
                GridView1.Columns[25].Visible = false;

            if (chk26.Checked == true)
                GridView1.Columns[26].Visible = true;
            else
                GridView1.Columns[26].Visible = false;

            if (chk27.Checked == true)
                GridView1.Columns[27].Visible = true;
            else
                GridView1.Columns[27].Visible = false;

            if (chk28.Checked == true)
                GridView1.Columns[28].Visible = true;
            else
                GridView1.Columns[28].Visible = false;

            if (chk29.Checked == true)
                GridView1.Columns[29].Visible = true;
            else
                GridView1.Columns[29].Visible = false;

            if (chk30.Checked == true)
                GridView1.Columns[30].Visible = true;
            else
                GridView1.Columns[30].Visible = false;

            if (chk31.Checked == true)
                GridView1.Columns[31].Visible = true;
            else
                GridView1.Columns[31].Visible = false;

            if (chk32.Checked == true)
                GridView1.Columns[32].Visible = true;
            else
                GridView1.Columns[32].Visible = false;

            if (chk33.Checked == true)
                GridView1.Columns[33].Visible = true;
            else
                GridView1.Columns[33].Visible = false;

            if (chk34.Checked == true)
                GridView1.Columns[34].Visible = true;
            else
                GridView1.Columns[34].Visible = false;

            if (chk35.Checked == true)
                GridView1.Columns[35].Visible = true;
            else
                GridView1.Columns[35].Visible = false;

            if (chk36.Checked == true)
                GridView1.Columns[36].Visible = true;
            else
                GridView1.Columns[36].Visible = false;

            if (chk37.Checked == true)
                GridView1.Columns[37].Visible = true;
            else
                GridView1.Columns[37].Visible = false;

            if (chk38.Checked == true)
                GridView1.Columns[38].Visible = true;
            else
                GridView1.Columns[38].Visible = false;

            if (chk39.Checked == true)
                GridView1.Columns[39].Visible = true;
            else
                GridView1.Columns[39].Visible = false;

            if (chk40.Checked == true)
                GridView1.Columns[40].Visible = true;
            else
                GridView1.Columns[40].Visible = false;

            if (chk41.Checked == true)
                GridView1.Columns[41].Visible = true;
            else
                GridView1.Columns[41].Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}