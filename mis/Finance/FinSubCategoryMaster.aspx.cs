using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class mis_Finance_FinSubCategoryMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-In");
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
                    txtOpeningDate.Attributes.Add("readonly", "readonly");
                    div.Visible = false;
                    FillCategory();
                    FillGrid();
					GetVoucherDate();
                    setDate();
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
    protected void CreateDatatable()
    {
        ViewState["dt"] = null;
        DataTable dt = new DataTable();
        DataColumn RowNo = dt.Columns.Add("RowNo", typeof(string));
        dt.Columns.Add(new DataColumn("CategoryId", typeof(string)));
        dt.Columns.Add(new DataColumn("CategoryName", typeof(string)));
        dt.Columns.Add(new DataColumn("OpeningDate", typeof(string)));
        dt.Columns.Add(new DataColumn("Type", typeof(string)));
        dt.Columns.Add(new DataColumn("OpeningBalanceShow", typeof(decimal)));
        dt.Columns.Add(new DataColumn("OpeningBalance", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Status", typeof(string)));
        RowNo.AutoIncrement = true;
        RowNo.AutoIncrementSeed = 1;
        RowNo.AutoIncrementStep = 1;

        ViewState["dt"] = dt;

        GvSubCategory.DataSource = dt;
        GvSubCategory.DataBind();
    }
    protected void FillCategory()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinCategoryMaster",
                      new string[] { "flag", "OfficeID" },
                      new string[] { "6", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataValueField = "CategoryId";
                    ddlCategory.DataSource = ds;
                    ddlCategory.DataBind();
                    ddlCategory.Items.Insert(0, new ListItem("Select", "0"));
                }
               
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
            GvCategory.DataSource = null;
            GvCategory.DataBind();
            ds = objdb.ByProcedure("SpFinSubCategoryMaster",
                      new string[] { "flag", "OfficeID" },
                      new string[] { "5", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvCategory.DataSource = ds;
                    GvCategory.DataBind();
                    GvCategory.UseAccessibleHeader = true;
                    GvCategory.HeaderRow.TableSection = TableRowSection.TableHeader;
                    foreach (GridViewRow row in GvCategory.Rows)
                    {
                        Label lblStatus = (Label)row.FindControl("lblStatus");
                        LinkButton lnkDelete = (LinkButton)row.FindControl("lnkDelete");
                        if (lblStatus.Text == "True")
                        {
                            lnkDelete.Visible = false;
                        }
                        else
                        {
                            lnkDelete.Visible = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GvCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblModalMsg.Text = "";
            Clear();
            CreateDatatable();
            GvSubCategory.DataSource = null;
            GvSubCategory.DataBind();
            ViewState["SubCategoryId"] = e.CommandArgument.ToString();
            if (e.CommandName == "EditRecord")
            {
                div.Visible = true;

                ds = objdb.ByProcedure("SpFinSubCategoryMaster",
                     new string[] { "flag", "SubCategoryId" },
                     new string[] { "4", ViewState["SubCategoryId"].ToString() }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtModalSubCategory.Text = ds.Tables[0].Rows[0]["SubCategoryName"].ToString();
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        DataTable dt = (DataTable)ViewState["dt"];
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            string categoryID = ds.Tables[1].Rows[i]["CategoryId"].ToString();
                            string CategoryName = ds.Tables[1].Rows[i]["CategoryName"].ToString();
                            string OpeningDate = ds.Tables[1].Rows[i]["OpeningDate"].ToString();
                            string Type = ds.Tables[1].Rows[i]["Type"].ToString();
                            string OpeningBalanceShow = ds.Tables[1].Rows[i]["OpeningBalanceShow"].ToString();
                            string OpeningBalance = ds.Tables[1].Rows[i]["OpeningBalance"].ToString();
                            string Status = ds.Tables[1].Rows[i]["Status"].ToString();

                            dt.Rows.Add(null, categoryID, CategoryName, OpeningDate, Type, OpeningBalanceShow, OpeningBalance, Status);
                        }
                        ViewState["dt"] = dt;

                        GvSubCategory.DataSource = dt;
                        GvSubCategory.DataBind();

                        //foreach (GridViewRow row in GvSubCategory.Rows)
                        //{
                        //    Label lblStatus = (Label)row.FindControl("lblStatus");
                        //    LinkButton lnkDelete = (LinkButton)row.FindControl("lnkDelete");
                        //    if (lblStatus.Text == "True")
                        //    {
                        //        lnkDelete.Visible = false;
                        //    }
                        //    else
                        //    {
                        //        lnkDelete.Visible = true;
                        //    }
                        //}
                        btnFinalSubmit.Text = "Update";
						GetVoucherDate();
                        setDate();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowSubCategoryModal();", true);
                    }

                }
            }
            else if (e.CommandName == "DeleteRecord")
            {
                ds = objdb.ByProcedure("SpFinSubCategoryMaster",
                     new string[] { "flag", "SubCategoryId", "DeletedBy", "DeletedIP" },
                     new string[] { "3", ViewState["SubCategoryId"].ToString(), ViewState["Emp_ID"].ToString(), "" }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        FillGrid();
                    }
                }
            }
            if (GvCategory.Rows.Count > 0)
            {
                GvCategory.UseAccessibleHeader = true;
                GvCategory.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        CreateDatatable();
        Clear();
        lblModalMsg.Text = "";
        div.Visible = false;
        btnFinalSubmit.Text = "Final Submit";
        lblMsg.Text = "";
        if (GvCategory.Rows.Count > 0)
        {
            GvCategory.UseAccessibleHeader = true;
            GvCategory.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
       

        ds = objdb.ByProcedure("SpFinSubCategoryMaster",
                          new string[] { "flag", "SubCategoryName", "OfficeID", "SubCategoryId" },
                          new string[] { "10", txtSubCategory.Text.Trim(), ViewState["Office_ID"].ToString(), "0" }, "dataset");
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["Status"].ToString() == "0")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowSubCategoryModal();", true);
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Sub Category is already exists.");
            }

        }

    }
    protected void btnMAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            lblModalMsg.Text = "";
            if (ddlCategory.SelectedIndex == 0)
            {
                msg += "Select Category. \\n";
            }
            if (txtOpeningDate.Text == "")
            {
                msg += "Enter Opening Date. \\n";
            }
            if (txtOpeningBalance.Text == "")
            {
                msg += "Enter Opening Balance. \\n";
            }
            if (ddlType.SelectedIndex == 0)
            {
                msg += "Select Type. \\n";
            }

            if (msg == "")
            {
                DataTable dt = (DataTable)ViewState["dt"];
                if (btnMAdd.Text == "Add")
                {
                    int status = 0;
                    foreach (GridViewRow row in GvSubCategory.Rows)
                    {
                        Label lblCategory_ID = (Label)row.FindControl("lblCategoryId");
                        if (lblCategory_ID.Text == ddlCategory.SelectedValue.ToString())
                        {
                            status = 1;
                        }
                    }
                    if (status == 0)
                    {
                        if (btnMAdd.Text == "Add")
                        {
                            if (txtOpeningBalance.Text == "0")
                            {
                                dt.Rows.Add(null, ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, txtOpeningDate.Text, "Cr", txtOpeningBalance.Text, txtOpeningBalance.Text, "");
                            }
                            else
                            {
                                if (ddlType.SelectedValue.ToString() == "Cr")
                                {
                                    dt.Rows.Add(null, ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, txtOpeningDate.Text, ddlType.SelectedValue.ToString(), txtOpeningBalance.Text, txtOpeningBalance.Text, "");
                                }
                                else
                                {
                                    dt.Rows.Add(null, ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, txtOpeningDate.Text, ddlType.SelectedValue.ToString(), txtOpeningBalance.Text, ("-" + txtOpeningBalance.Text), "");
                                }
                            }
                        }


                        ViewState["dt"] = dt;

                        GvSubCategory.DataSource = dt;
                        GvSubCategory.DataBind();

                        Clear();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowSubCategoryModal();", true);
                    }
                    else
                    {
                        lblModalMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Category is already exists.");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowSubCategoryModal();", true);
                    }
                }
                else if (btnMAdd.Text == "Update")
                {
                    //int Count = dt.Rows.Count;
                    //for (int i = 0; i < Count; i++)
                    //{
                    //    DataRow dr = dt.Rows[i];
                    //    if (dr["RowNo"].ToString() == ViewState["RwNo"].ToString())
                    //    {
                    //        dr.Delete();
                    //        break;
                    //    }
                    //}
                    //if (txtOpeningBalance.Text == "0")
                    //{
                    //    dt.Rows.Add(null, ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, txtOpeningDate.Text, "Cr", txtOpeningBalance.Text, txtOpeningBalance.Text, "");
                    //}
                    //else
                    //{
                    //    if (ddlType.SelectedValue.ToString() == "Cr")
                    //    {
                    //        dt.Rows.Add(null, ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, txtOpeningDate.Text, ddlType.SelectedValue.ToString(), txtOpeningBalance.Text, txtOpeningBalance.Text, "");
                    //    }
                    //    else
                    //    {
                    //        dt.Rows.Add(null, ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, txtOpeningDate.Text, ddlType.SelectedValue.ToString(), txtOpeningBalance.Text, ("-" + txtOpeningBalance.Text), "");
                    //    }
                    //}
                    //ViewState["dt"] = dt;


                    DataTable dt_temp = new DataTable();
                    DataColumn RowNo = dt_temp.Columns.Add("RowNo", typeof(string));
                    dt_temp.Columns.Add(new DataColumn("CategoryId", typeof(string)));
                    dt_temp.Columns.Add(new DataColumn("CategoryName", typeof(string)));
                    dt_temp.Columns.Add(new DataColumn("OpeningDate", typeof(string)));
                    dt_temp.Columns.Add(new DataColumn("Type", typeof(string)));
                    dt_temp.Columns.Add(new DataColumn("OpeningBalanceShow", typeof(decimal)));
                    dt_temp.Columns.Add(new DataColumn("OpeningBalance", typeof(decimal)));
                    dt_temp.Columns.Add(new DataColumn("Status", typeof(string)));
                    RowNo.AutoIncrement = true;
                    RowNo.AutoIncrementSeed = 1;
                    RowNo.AutoIncrementStep = 1;


                    int Count11 = dt.Rows.Count;
                    for (int i = 0; i < Count11; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        if (dr["RowNo"].ToString() == ViewState["RwNo"].ToString())
                        {
                            //dr.Delete();
                            //break;
                            if (txtOpeningBalance.Text == "0")
                            {
                                dt_temp.Rows.Add(null, ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, txtOpeningDate.Text, "Cr", txtOpeningBalance.Text, txtOpeningBalance.Text, "");
                            }
                            else
                            {
                                if (ddlType.SelectedValue.ToString() == "Cr")
                                {
                                    dt_temp.Rows.Add(null, ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, txtOpeningDate.Text, ddlType.SelectedValue.ToString(), txtOpeningBalance.Text, txtOpeningBalance.Text, "");
                                }
                                else
                                {
                                    dt_temp.Rows.Add(null, ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, txtOpeningDate.Text, ddlType.SelectedValue.ToString(), txtOpeningBalance.Text, ("-" + txtOpeningBalance.Text), "");
                                }
                            }
                        }
                        else
                        {

                            dt_temp.Rows.Add(null, dt.Rows[i]["CategoryId"].ToString(), dt.Rows[i]["CategoryName"].ToString(), dt.Rows[i]["OpeningDate"].ToString(), dt.Rows[i]["Type"].ToString(), dt.Rows[i]["OpeningBalanceShow"].ToString(), dt.Rows[i]["OpeningBalance"].ToString(), dt.Rows[i]["Status"].ToString());
                        }
                    }

                    ViewState["dt"] = dt_temp;

                    GvSubCategory.DataSource = dt_temp;
                    GvSubCategory.DataBind();

                    Clear();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowSubCategoryModal();", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "')", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Clear()
    {
        ddlCategory.ClearSelection();
        txtOpeningDate.Text = "";
        ddlType.ClearSelection();
        txtOpeningBalance.Text = "";
        ddlCategory.Enabled = true;
        btnMAdd.Text = "Add";
    }
    protected void btnFinalSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            lblModalMsg.Text = "";
            if (btnFinalSubmit.Text == "Final Submit")
            {
                if (GvSubCategory.Rows.Count > 0)
                {
                    ds = objdb.ByProcedure("SpFinSubCategoryMaster",
                          new string[] { "flag", "SubCategoryName", "IsActive", "OfficeID", "CreatedBy", "CreatedIP" },
                          new string[] { "1", txtSubCategory.Text.Trim(), "1", ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), "" }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string SubCategoryId = ds.Tables[0].Rows[0]["SubCategoryId"].ToString();
                            foreach (GridViewRow row in GvSubCategory.Rows)
                            {
                                Label lblCategoryId = (Label)row.FindControl("lblCategoryId");
                                Label lblOpeningDate = (Label)row.FindControl("lblOpeningDate");
                                Label lblType = (Label)row.FindControl("lblType");
                                Label lblOpeningBalance = (Label)row.FindControl("lblOpeningBalance");
                                ds = objdb.ByProcedure("SpFinSubCategoryMaster",
                      new string[] { "flag", "SubCategoryId", "CategoryId", "OpeningDate", "OpeningBalance", "OfficeId", "IsActive", "CreatedBy" },
                      new string[] { "2", SubCategoryId, lblCategoryId.Text, Convert.ToDateTime(lblOpeningDate.Text, cult).ToString("yyyy/MM/dd"), lblOpeningBalance.Text, ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString() }, "dataset");
                            }
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                txtSubCategory.Text = "";
                            }
                        }
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-warning", "Info!", "Please map atleast one category.");
                }
            }
            else if (btnFinalSubmit.Text == "Update")
            {
                if (txtModalSubCategory.Text == "")
                {
                    msg += "Enter Sub Category.";
                }
                if (msg == "")
                {
                    if (GvSubCategory.Rows.Count > 0)
                    {
                        DataSet ds1 = objdb.ByProcedure("SpFinSubCategoryMaster",
                          new string[] { "flag", "SubCategoryName", "OfficeID", "SubCategoryId" },
                          new string[] { "10", txtModalSubCategory.Text.Trim(), ViewState["Office_ID"].ToString(), ViewState["SubCategoryId"].ToString() }, "dataset");
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows[0]["Status"].ToString() == "0")
                            {
                                ds = objdb.ByProcedure("SpFinSubCategoryMaster",
                                new string[] { "flag", "SubCategoryId", "SubCategoryName", "IsActive", "OfficeID", "UpdatedBy", "UpdatedIP" },
                                new string[] { "6", ViewState["SubCategoryId"].ToString(), txtModalSubCategory.Text.Trim(), "1", ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), "" }, "dataset");
                                if (ds != null && ds.Tables[0].Rows.Count > 0)
                                {
                                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                    {
                                        string SubCategoryId = ViewState["SubCategoryId"].ToString();
                                        objdb.ByProcedure("SpFinSubCategoryMaster",
                                                 new string[] { "flag", "SubCategoryId", },
                                                 new string[] { "9", SubCategoryId }, "dataset");
                                        foreach (GridViewRow row in GvSubCategory.Rows)
                                        {
                                            Label lblCategoryId = (Label)row.FindControl("lblCategoryId");
                                            Label lblOpeningDate = (Label)row.FindControl("lblOpeningDate");
                                            Label lblType = (Label)row.FindControl("lblType");
                                            Label lblOpeningBalance = (Label)row.FindControl("lblOpeningBalance");
                                            ds = objdb.ByProcedure("SpFinSubCategoryMaster",
                                             new string[] { "flag", "SubCategoryId", "CategoryId", "OpeningDate", "OpeningBalance", "OfficeId", "IsActive" },
                                             new string[] { "2", SubCategoryId, lblCategoryId.Text, Convert.ToDateTime(lblOpeningDate.Text, cult).ToString("yyyy/MM/dd"), lblOpeningBalance.Text, ViewState["Office_ID"].ToString(), "1" }, "dataset");
                                        }
                                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                        {
                                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                            txtSubCategory.Text = "";
                                            btnFinalSubmit.Text = "Final Submit";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                lblModalMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Sub Category is already exists.");
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowSubCategoryModal();", true);
                            }

                        }



                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-warning", "Info!", "Please map atleast one category.");
                    }
                }
                else
                {
                    lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Info!", msg);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowSubCategoryModal();", true);
                }
            }
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void GvSubCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblModalMsg.Text = "";
        string ID = e.CommandArgument.ToString();
        if (e.CommandName == "RecordDelete")
        {
            DataTable dt = (DataTable)ViewState["dt"];
            int Count = dt.Rows.Count;
            for (int i = 0; i < Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (dr["RowNo"].ToString() == ID.ToString())
                {
                    dr.Delete();
                    break;
                }
            }
            dt.AcceptChanges();
            ViewState["CostCentreTable"] = dt;

            GvSubCategory.DataSource = dt;
            GvSubCategory.DataBind();

            //int j = 0;
            //foreach (GridViewRow row in GvSubCategory.Rows)
            //{
            //    Label lblStatus = (Label)row.FindControl("lblStatus");
            //    LinkButton lnkDelete = (LinkButton)row.FindControl("lnkDelete");
            //    if (dt.Rows[0]["Status"].ToString() == "True")
            //    {
            //        lnkDelete.Visible = false;
            //    }
            //    else
            //    {
            //        lnkDelete.Visible = true;
            //    }
            //    j++;
            //}

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowSubCategoryModal();", true);
        }
        else if (e.CommandName == "RecordEdit")
        {
            ViewState["RwNo"] = e.CommandArgument.ToString();
            DataTable dt = (DataTable)ViewState["dt"];
            DataView dtv = new DataView();
            dtv = dt.DefaultView;
            dtv.RowFilter = "RowNo=" + e.CommandArgument.ToString();
            dt = dtv.ToTable();
            ddlCategory.SelectedValue = dt.Rows[0]["CategoryId"].ToString();
            //ddlCategory.Enabled = false;
            txtOpeningDate.Text = dt.Rows[0]["OpeningDate"].ToString();
            txtOpeningBalance.Text = dt.Rows[0]["OpeningBalanceShow"].ToString();
            ddlType.SelectedValue = dt.Rows[0]["Type"].ToString();
            btnMAdd.Text = "Update";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowSubCategoryModal();", true);
        }
    }
	 protected void GetVoucherDate()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {

                ViewState["VoucherDate"] = ds.Tables[0].Rows[0]["VoucherDate"].ToString();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void setDate()
    {
        try
        {
            string VoucherDate = ViewState["VoucherDate"].ToString();
            string sDate = (Convert.ToDateTime(VoucherDate, cult).ToString("yyyy/MM/dd")).ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            int Month = int.Parse(datevalue.Month.ToString());
            int Year = int.Parse(datevalue.Year.ToString());
            int FY = Year;
            string FinancialYear = Year.ToString();
            string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
            FinancialYear = "";
            if (Month <= 3)
            {
                FY = Year - 1;
                FinancialYear = FY.ToString() + "-" + LFY.ToString();
            }
            else
            {

                FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
            }
            txtOpeningDate.Text = "01" + '/' + "04" + '/' + FY.ToString();
          


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}