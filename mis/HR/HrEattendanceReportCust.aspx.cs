using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Drawing;

public partial class mis_HR_HrEattendanceReportCust : System.Web.UI.Page
{
    DataSet ds, dsRecord;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                lblMsg.Text = "";
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();

                txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");

                ds = objdb.ByProcedure("SpAdminOffice",
                        new string[] { "flag" },
                        new string[] { "1" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOffice.DataSource = ds;
                    ddlOffice.DataTextField = "Office_Name";
                    ddlOffice.DataValueField = "Office_ID";
                    ddlOffice.DataBind();
                    ddlOffice.Items.Insert(0, new ListItem("Select Office", "0"));
                }
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    //ddlOldOffice.Attributes.Add("readonly", "readonly");
                    ddlOffice.Enabled = true;
                }
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    protected void FillDetail()
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            lblGridHeading.Text = "<h5><b>Office:</b> " + ddlOffice.SelectedItem.ToString() + ",  <b>From:</b> " + txtFromDate.Text.ToString() + ",  <b>To:</b> " + txtToDate.Text.ToString() + "</h5>";
            lblMsg.Text = "";
            string msg = "";

            if (txtFromDate.Text == "")
            {
                msg += "Please Select From Date. \\n";
            }
            if (txtToDate.Text == "")
            {
                msg += "Please Select To Date. \\n";
            }

            if (msg.Trim() == "")
            {
                ds = objdb.ByProcedure("SpHRDaily_Attendance", new string[] { "flag", "startDate", "endDate", "OfficeId" }, new string[] { "5", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy-MM-dd"), ddlOffice.SelectedValue }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FillDetail();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Text == "General Holiday")
                    {
                        e.Row.Cells[i].BackColor = Color.LightSkyBlue;
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].Text = "Gen-Holiday";
                    }
                    if (e.Row.Cells[i].Text == "Tour" || e.Row.Cells[i].Text == "On Tour")
                    {
                        e.Row.Cells[i].BackColor = Color.CornflowerBlue;
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].Text = "Tour";
                    }
                    if (e.Row.Cells[i].Text == "Tuesday Holiday")
                    {
                        e.Row.Cells[i].BackColor = Color.LightSkyBlue;
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].Text = "Tue-Holiday";
                    }
                    if (e.Row.Cells[i].Text == "Sunday Holiday" || e.Row.Cells[i].Text == "SUNDAY")
                    {
                        e.Row.Cells[i].BackColor = Color.LightSkyBlue;
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].Text = "Sun-Holiday";
                    }
                    else if (e.Row.Cells[i].Text == "II-Saturday Holiday")
                    {
                        e.Row.Cells[i].BackColor = Color.LightSkyBlue;
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].Text = "II-Sat";
                    }
                    else if (e.Row.Cells[i].Text == "III-Saturday Holiday")
                    {
                        e.Row.Cells[i].BackColor = Color.LightSkyBlue;
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].Text = "III-Sat";
                    }
                    else if (e.Row.Cells[i].Text == "Present")
                    {
                        //e.Row.Cells[i].ForeColor = Color.White;
                        e.Row.Cells[i].BackColor = Color.ForestGreen;
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].Text = "P";
                    }
                    else if (e.Row.Cells[i].Text == "Absent")
                    {
                        //e.Row.Cells[i].ForeColor = Color.White;
                        e.Row.Cells[i].BackColor = Color.Red;
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].Text = "A";
                    }
                    else if (e.Row.Cells[i].Text == "Medical Leave")
                    {
                        //e.Row.Cells[i].ForeColor = Color.White;
                        e.Row.Cells[i].BackColor = Color.Yellow;
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].Text = "ML";
                    }

                    else if (e.Row.Cells[i].Text == "Late Login")
                    {
                        //e.Row.Cells[i].ForeColor = Color.White;
                        e.Row.Cells[i].BackColor = Color.BlueViolet;
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].Text = "Late";
                    }

                    else if (e.Row.Cells[i].Text == "Casual Leave")
                    {
                        //e.Row.Cells[i].ForeColor = Color.White;
                        e.Row.Cells[i].BackColor = Color.Yellow;
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].Text = "CL";
                    }

                    else if (e.Row.Cells[i].Text == "Earn Leave")
                    {
                        //e.Row.Cells[i].ForeColor = Color.White;
                        e.Row.Cells[i].BackColor = Color.Yellow;
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].Text = "EL";
                    }
                    else if (e.Row.Cells[i].Text == "Optional Leave")
                    {
                        //e.Row.Cells[i].ForeColor = Color.White;
                        e.Row.Cells[i].BackColor = Color.Yellow;
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].Text = "OL";
                    }
                    else if (e.Row.Cells[i].Text == "Second-Half Leave")
                    {
                        //e.Row.Cells[i].ForeColor = Color.White;
                        e.Row.Cells[i].BackColor = Color.Orange;
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].Text = "S-Half Day";
                    }
                    else if (e.Row.Cells[i].Text == "First-half Leave")
                    {
                        //e.Row.Cells[i].ForeColor = Color.White;
                        e.Row.Cells[i].BackColor = Color.Orange;
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].Text = "F-Half Day";
                    }                    
                }
            }
        }
        catch (Exception ex)
        {
            
           lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}