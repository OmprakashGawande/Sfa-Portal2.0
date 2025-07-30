using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;

public partial class mis_Finance_FinMasterHead : System.Web.UI.Page
{
    DataSet ds, ds1;
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
                ViewState["Head_ID"] = "0";
                FillHeadName();
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    protected void FillHeadName()
    {
        try
        {
            ds = null;

            ds = objdb.ByProcedure("SpFinMasterHead",
                        new string[] { "flag" },
                        new string[] { "2" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlHead_Name.DataTextField = "Head_Name";
                ddlHead_Name.DataValueField = "Head_ID";
                ddlHead_Name.DataSource = ds;
                ddlHead_Name.DataBind();
                ddlHead_Name.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlHead_Name.DataSource = null;
                ddlHead_Name.DataBind();
                ddlHead_Name.Items.Insert(0, new ListItem("Select", "0"));
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
            ds = null;
            lblMsg.Text = "";

            //GridView1.DataSource = null;
            //GridView1.DataBind();

            ds = objdb.ByProcedure("SpFinMasterHead",
                    new string[] { "flag", "Head_ID" },
                    new string[] { "4", ddlHead_Name.SelectedValue }, "dataset");

           
            if (ds!= null && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();             
            }
            else
            {
                GridView1.DataSource = new string[] {};
                GridView1.DataBind();
            }
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;
            //foreach (GridViewRow rows in GridView1.Rows)
            //{
            //    Label ID = (Label)rows.FindControl("lblHead_ID");
            //    LinkButton lnkEdit = (LinkButton)rows.FindControl("lnkEdit");
            //    LinkButton lnkDelete = (LinkButton)rows.FindControl("lnkDelete");
            //    ds = objdb.ByProcedure("SpFinMasterHead", new string[] { "flag", "Head_ID" }, new string[] { "12", ID.Text }, "dataset");
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        if (ds.Tables[0].Rows[0]["Status"].ToString() == "true")
            //        {
            //            lnkEdit.Visible = false;
            //            lnkDelete.Visible = false;
            //        }
            //        else
            //        {
            //            lnkEdit.Visible = true;
            //            lnkDelete.Visible = true;
            //        }
            //    }
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlHead_Name.SelectedIndex == 0)
            {
                msg += "Select Head \\n";
            }
            if (txtHeadName.Text == "")
            {
                msg += "Enter Head Name. \\n";
            }
            if (txtHeadCode.Text == "")
            {
                msg += "Enter Head Code. \\n";
            }
            if (msg.Trim() == "")
            {
                string Status = "0";
               
                txtHeadName.Text = FirstLetterToUpper(txtHeadName.Text);
                ds = objdb.ByProcedure("SpFinMasterHead", new string[] { "flag", "Head_Name", "Head_ParentID", "Head_ID", "Head_Code" }, new string[] { "8", txtHeadName.Text, ddlHead_Name.SelectedValue.ToString(), ViewState["Head_ID"].ToString(), txtHeadCode.Text }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Status = ds.Tables[0].Rows[i]["Status"].ToString();
                        if(Status != "0")
                        {
                            break;
                        }
                    }
                   
                  
                }
                if (BtnSubmit.Text == "Submit" && Status == "0" && ViewState["Head_ID"].ToString() == "0")
                {
                    string IsActive = "1";
                    string IsFixed = "1";
                    ds1 = objdb.ByProcedure("SpFinMasterHead",
                    new string[] { "flag", "Head_Name", "Head_Name_Hindi", "Head_Code", "Head_ParentID", "UpdatedBy", "IsActive", "IsFixed" },
                    new string[] { "0", txtHeadName.Text.Trim(),txtHeadNameHindi.Text.Trim(),txtHeadCode.Text, ddlHead_Name.SelectedValue, ViewState["Emp_ID"].ToString(),IsActive, IsFixed }, "dataset");

                   // ddlHead_Name.ClearSelection();
                    txtHeadName.Text = "";
                    txtHeadNameHindi.Text = "";
                    txtHeadCode.Text = "";
                    FillHeadName();
                    if(ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlHead_Name.Items.FindByValue(ds1.Tables[0].Rows[0]["Head_ID"].ToString()).Selected = true;
                    }
                        FillGrid();
                        
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else if (BtnSubmit.Text == "Update" && Status == "0" && ViewState["Head_ID"].ToString() != "0")
                {
                    ds1 = objdb.ByProcedure("SpFinMasterHead",
                    new string[] { "flag", "Head_ID", "Head_Name", "Head_Name_Hindi", "Head_Code", "Head_ParentID", "UpdatedBy", },
                    new string[] { "11", ViewState["Head_ID"].ToString(), txtHeadName.Text.Trim(), txtHeadNameHindi.Text.Trim(), txtHeadCode.Text, ddlHead_Name.SelectedValue, ViewState["Emp_ID"].ToString() }, "dataset");
                    
                    BtnSubmit.Text = "Submit";
                    txtHeadName.Text = "";
                    txtHeadNameHindi.Text = "";
                    txtHeadCode.Text = "";
                    FillHeadName();
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlHead_Name.Items.FindByValue(ds1.Tables[0].Rows[0]["Head_ID"].ToString()).Selected = true;
                    }
                    FillGrid();
                    
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else
                {
                    lblMsg.Text = "";
                    txtHeadName.Text = "";
                    txtHeadNameHindi.Text = "";
                    txtHeadCode.Text = "";
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Group name or Group Code is already exist.');", true);
                 
                   // FillHeadName();
                }
                ViewState["Head_ID"] = "0";
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
    protected void ddlHead_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            //txtHeadName.Text = "";
            FillGrid();
          
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public string FirstLetterToUpper(string str)
    {
        string txt = cult.TextInfo.ToTitleCase(str.ToLower());
        return txt;
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string ID = GridView1.SelectedDataKey.Value.ToString();
            ViewState["Head_ID"] = ID.ToString();
            ds = objdb.ByProcedure("SpFinMasterHead", new string[] { "flag", "Head_ID" }, new string[] { "10", ID.ToString()}, "dataset");
            if(ds!= null && ds.Tables[0].Rows.Count > 0)
            {
                txtHeadName.Text = ds.Tables[0].Rows[0]["Head_Name"].ToString();
                txtHeadNameHindi.Text = ds.Tables[0].Rows[0]["Head_Name_Hindi"].ToString();
                txtHeadCode.Text = ds.Tables[0].Rows[0]["Head_Code"].ToString();
                ddlHead_Name.ClearSelection();
                ddlHead_Name.Items.FindByValue(ds.Tables[0].Rows[0]["Head_ParentID"].ToString()).Selected = true;
                BtnSubmit.Text = "Update";
            }
            FillGrid();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["Head_ID"] = "0";
            string ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            ds = objdb.ByProcedure("SpFinMasterHead", new string[] { "flag", "Head_ID", "UpdatedBy" }, new string[] { "13", ID.ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
            FillGrid();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
}