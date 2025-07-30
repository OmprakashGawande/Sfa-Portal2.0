using System;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

public partial class mis_HR_Emp_DeptAllocation : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetAllocatedDeptEmpWise();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    private void GetEmployee()
    {
        try
        {
            ddlEmployye_Name.DataSource = objdb.ByProcedure("SpEmployeeRoleMap",
                      new string[] { "flag" },
                      new string[] { "6" }, "dataset");
            ddlEmployye_Name.DataTextField = "Emp";
            ddlEmployye_Name.DataValueField = "Emp_ID";

            ddlEmployye_Name.DataBind();
            ddlEmployye_Name.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Error 1", ex.Message.ToString());
        }
    }
    private void GetDepartment()
    {
        try
        {
            ddlDepartment.DataSource = objdb.ByProcedure("SpAdminDepartment", new string[] { "flag" }, new string[] { "1" }, "dataset");
            ddlDepartment.DataTextField = "Department_Name";
            ddlDepartment.DataValueField = "Department_ID";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Error 2", ex.Message.ToString());
        }
    }
    private void GetAllocatedDeptEmpWise()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Sp_tblEmpDeptAllocation",
                                        new string[] { "flag" },
                                        new string[] { "0",}, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GetDatatableHeaderDesign();
            }
            else
            {
                ds.Clear();
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            ds.Clear();
            
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Error 2 ", ex.Message.ToString());
        }
    }
    protected void ddlEmployye_Name_Init(object sender, EventArgs e)
    {
        GetEmployee();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (btnSubmit.Text == "Save")
                {
                    lblMsg.Text = "";
                    string deptname = "";
                    foreach (ListItem item in ddlDepartment.Items)
                    {
                        if (item.Selected)
                        {
                            deptname += item.Value + ",";
                        }
                    }

                    ds = objdb.ByProcedure("Sp_tblEmpDeptAllocation",
                        new string[] { "flag", "Emp_ID", "Department", "CreatedBy", "ipaddress", "Office_Id" },
                        new string[] { "1", ddlEmployye_Name.SelectedValue, deptname, objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),objdb.Office_ID() }, "dataset");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        ds.Clear();
                        GetAllocatedDeptEmpWise();
                        Clear();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                    {
                        string warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        ds.Clear();
                        GetAllocatedDeptEmpWise();
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Warning :" + warning + " for Employee Name: " + ddlEmployye_Name.SelectedItem.Text);
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        ds.Clear();
                        GetAllocatedDeptEmpWise();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }

                }
                if (btnSubmit.Text == "Update")
                {
                    lblMsg.Text = "";
                    string deptname = "";
                    foreach (ListItem item in ddlDepartment.Items)
                    {
                        if (item.Selected)
                        {
                            deptname += item.Value + ",";
                        }
                    }

                    ds = objdb.ByProcedure("Sp_tblEmpDeptAllocation",
                        new string[] { "flag", "EmpDeptAllocation_id", "Emp_ID", "Department", "CreatedBy", "ipaddress", "PageName", "Remark" },
                        new string[] { "2", ViewState["rowid"].ToString(), ddlEmployye_Name.SelectedValue, deptname, objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Employee Department Allocation Record Updated" }, "dataset");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        lblMsg.Text = "";
                        Clear();
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        ds.Clear();
                        btnSubmit.Text = "Save";
                        btnClear.Text = "Clear";
                        GetAllocatedDeptEmpWise();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                    {
                        string warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        ds.Clear();
                        GetAllocatedDeptEmpWise();
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Warning :" + warning + " for Employee Name: " + ddlEmployye_Name.SelectedItem.Text );
                    }
                    else
                    {
                        GetAllocatedDeptEmpWise();
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    public void Clear()
    {
        try
        {
            btnSubmit.Text = "Save";
            ddlDepartment.ClearSelection();
            ddlEmployye_Name.SelectedIndex=0;
            GridView1.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlDepartment_Init(object sender, EventArgs e)
    {
        GetDepartment();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblDepartment = (Label)row.FindControl("lblDepartment");
                    Label lblEmp_ID = (Label)row.FindControl("lblEmp_ID");

                    ViewState["rowid"] = e.CommandArgument.ToString();
                    btnSubmit.Text = "Update";
                    ddlEmployye_Name.SelectedValue = lblEmp_ID.Text;
                    //for uncheck the product list
                    for (int i = 0; i < ddlDepartment.Items.Count; i++)
                    {
                        foreach (ListItem listItem in ddlDepartment.Items)
                        {
                            ddlDepartment.Items[i].Selected = false;
                        }
                    }

                    string ccat = lblDepartment.Text;
                    for (int i = 0; i < ddlDepartment.Items.Count; i++)
                    {
                        foreach (string category in ccat.ToString().Split(','))
                        {
                            if (category != ddlDepartment.Items[i].Value) continue;
                            ddlDepartment.Items[i].Selected = true;
                            break;
                        }
                    }
                   
                    foreach (GridViewRow gvRow in GridView1.Rows)
                    {
                        if (GridView1.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView1.SelectedIndex = gvRow.DataItemIndex;
                            GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LemonChiffon;
                            break;
                        }
                    }
                    GetDatatableHeaderDesign();
                }

            }
            if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                   ds = objdb.ByProcedure("Sp_tblEmpDeptAllocation",
                       new string[] { "flag", "EmpDeptAllocation_id", "CreatedBy", "PageName", "Remark", "ipaddress" },
                       new string[] { "3", e.CommandArgument.ToString(), objdb.createdBy(), Path.GetFileName(Request.Url.AbsolutePath), "Employee Department Allocation Record Deleted", objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "dataset");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        ds.Clear();
                        GetAllocatedDeptEmpWise();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        ds.Clear();
                        GetAllocatedDeptEmpWise();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
        lblMsg.Text = string.Empty;
    }
}