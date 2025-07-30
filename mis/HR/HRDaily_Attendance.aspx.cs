using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;

public partial class mis_HR_HRDaily_Attendance : System.Web.UI.Page
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
                ds = objdb.ByProcedure("SpAdminOffice",
                        new string[] { "flag" },
                        new string[] { "10" }, "dataset");
                txtDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDate.Attributes.Add("readonly", "readonly");
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    //ddlOldOffice.Attributes.Add("readonly", "readonly");
                    ddlOffice.Enabled = true;
                }
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOffice.DataSource = ds;
                    ddlOffice.DataTextField = "Office_Name";
                    ddlOffice.DataValueField = "Office_ID";
                    ddlOffice.DataBind();
                    //ddlOffice.Items.Insert(0, new ListItem("Select Office", "0"));

                }
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillData()
    {
        try
        {

            lblGridHeading.Text = "<h5><b>Office:</b> " + ddlOffice.SelectedItem.ToString() + ",  <b>Day Type:</b> " + ddlDayType.SelectedItem.ToString() + ", <b>Date:</b> " + txtDate.Text.ToString() + "</h5>";
            ds = objdb.ByProcedure("SpHRDaily_Attendance", new string[] { "flag", "AdminOffice_ID" }, new string[] { "2", ddlOffice.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                //GridView1.UseAccessibleHeader = true;

            }


            lblMsg.Text = "";
            BtnSubmit.Enabled = true;
            foreach (GridViewRow row in GridView1.Rows)
            {
                //TextBox txtLoginTime = (TextBox)row.FindControl("txtLoginTime");
                //TextBox txtLogoutTime = (TextBox)row.FindControl("txtLogoutTime");
                DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");
                TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                Button BtnSave = (Button)row.FindControl("BtnSave");
                string Emp_ID = BtnSave.ToolTip.ToString();
                string date = "";
                if (txtDate.Text != "")
                {
                    date = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy-MM-dd");
                }
                else { }

                ds = objdb.ByProcedure("SpHRDaily_Attendance", new string[] { "flag", "Emp_ID", "SelectDate", "AdminOffice_ID" }, new string[] { "1", Emp_ID, date, ddlOffice.SelectedValue }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //txtLoginTime.Text = ds.Tables[0].Rows[0]["LoginTime"].ToString();
                    //txtLogoutTime.Text = ds.Tables[0].Rows[0]["LogoutTime"].ToString();
                    ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                    txtRemark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }
                else { }

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
            //string CurrentMonth = DateTime.Now.Month.ToString();
            //string CurrentYear = DateTime.Now.Year.ToString();
            //DateTime DateMonth = Convert.ToDateTime(txtDate.Text.ToString());
             
            //int SelectedDateMonth = DateMonth.Month;
            //int SelectedDateYear = DateMonth.Year;
            //int BeforeMonth = int.Parse(CurrentMonth) - 1;

            //if (SelectedDateMonth < BeforeMonth && SelectedDateYear <= Convert.ToInt16(CurrentYear))
            //{
               // BtnSubmit.Visible = false;
            //}
            //else
            //{
               // BtnSubmit.Visible = true;
            //}


            //ds = objdb.ByProcedure("SpHRDaily_Attendance", new string[] { "flag", "AdminOffice_ID" }, new string[] { "2", ddlOffice.SelectedValue.ToString() }, "dataset");
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    GridView1.DataSource = ds.Tables[0];
            //    GridView1.DataBind();
            //    //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            //    //GridView1.UseAccessibleHeader = true;

            //}

            FillData();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlOffice.SelectedIndex == 0)
        //{
            GridView1.DataSource = null;
            GridView1.DataBind();
        //}
        //if (ddlOffice.SelectedIndex > 0)
        //{
        //    GridView1.DataSource = null;
        //    GridView1.DataBind();
        //}
        BtnSubmit.Enabled = false;
        lblGridHeading.Text = "";
        lblMsg.Text = "";
    }
    //protected void BtnSave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int selRowIndex = ((GridViewRow)(((Button)sender).Parent.Parent)).RowIndex;
    //        Button chk = (Button)GridView1.Rows[selRowIndex].FindControl("BtnSave");
    //        string Emp_ID = chk.ToolTip.ToString();
    //        lblMsg.Text = "";
    //        string msg = "";

    //        if (txtDate.Text == "")
    //        {
    //            msg += "Please Select Date. \\n";
    //        }

    //        //TextBox txtLoginTime = (TextBox)GridView1.Rows[selRowIndex].FindControl("txtLoginTime");
    //        //TextBox txtLogoutTime = (TextBox)GridView1.Rows[selRowIndex].FindControl("txtLogoutTime");
    //        DropDownList ddlStatus = (DropDownList)GridView1.Rows[selRowIndex].FindControl("ddlStatus");
    //        TextBox Remark = (TextBox)GridView1.Rows[selRowIndex].FindControl("txtRemark");
    //        //if (txtLoginTime.Text == "")
    //        //{
    //        //    msg += "Please Enter Login Time. \\n";
    //        //}
    //        //if (txtLogoutTime.Text == "")
    //        //{
    //        //    msg += "Please Enter Logout Time. \\n";
    //        //}
    //        if (ddlStatus.SelectedIndex < 0)
    //        {
    //            msg += "Select Status. \\n";
    //        }
    //        if (msg.Trim() == "")
    //        {
    //            //string LoginTime1 = txtLoginTime.Text;
    //            //string LogoutTime1 = txtLogoutTime.Text;
    //            string ddlStatus1 = ddlStatus.SelectedValue;
    //            string Remark1 = Remark.Text;
    //            String time = "";
    //            if (txtDate.Text != "")
    //            {
    //                time = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy-MM-dd");
    //            }
    //            else
    //            {
    //                time = "";
    //            }
    //            objdb.ByProcedure("SpHRDaily_Attendance",
    //                       new string[] { "flag", "SelectDate", "Emp_ID", "LoginTime", "LogoutTime", "Status", "Remark", "Attendance_UpdatedBy", "AdminOffice_ID" },
    //                       new string[] { "0", time, Emp_ID, "10:00", "5:30", ddlStatus1, Remark1, ViewState["Emp_ID"].ToString(), ddlOffice.SelectedValue.ToString(), }, "dataset");
    //            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
    //        }
    //        else
    //        {
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            String time = "";
            lblMsg.Text = "";
            string msg = "";
            if (txtDate.Text != "")
            {
                time = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy-MM-dd");
            }
            else
            {
                time = "";
            }
            if (txtDate.Text == "")
            {
                msg += "Please Select Date. \\n";
            }
            if (ddlOffice.SelectedIndex < 0)
            {
                msg += "Please Select Office. \\n";
            }
            if (msg.Trim() == "")
            {
                foreach (GridViewRow row in GridView1.Rows)
                {

                    Label EmpId = (Label)row.FindControl("LblEmployeeId");
                    Label EmpName = (Label)row.FindControl("LblEmployee");
                    Label Designation = (Label)row.FindControl("LblDesignation");
                    DropDownList Status = (DropDownList)row.FindControl("ddlStatus");
                    TextBox Remark = (TextBox)row.FindControl("txtRemark");
                    string AttendanceStatus = "";
                     if (ddlDayType.SelectedIndex == 0)
                     {
                         AttendanceStatus = Status.SelectedItem.Text;
                     }
                     else
                     {
                         AttendanceStatus = ddlDayType.SelectedItem.Text;

                     }
                   

                    objdb.ByProcedure("SpHRDaily_Attendance",
                              new string[] { "flag", "SelectDate", "Emp_ID", "LoginTime", "LogoutTime", "Status", "Remark", "Attendance_UpdatedBy", "AdminOffice_ID" },
                              new string[] { "0", time, EmpId.Text, "10:00", "5:30", AttendanceStatus.ToString(), Remark.Text, ViewState["Emp_ID"].ToString(), ddlOffice.SelectedValue.ToString(), }, "dataset");

                }
                ddlDayType.SelectedIndex = 0;
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
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
    protected void ddlDayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BtnSubmit.Enabled = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        lblGridHeading.Text = "";
        lblMsg.Text = "";
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        BtnSubmit.Enabled = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        lblGridHeading.Text = "";
        lblMsg.Text = "";
    }
}