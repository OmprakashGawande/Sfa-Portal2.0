using System;
using System.Data;
using System.Web.UI.WebControls;


public partial class mis_Daily_Task_AddProject : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ClearText();
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string msg = "";
                msg += txt_ProjectsNameEng.Text.Trim() == "" ? "Enter Project Name (In English)" : "";
                msg += txt_ProjectsNameHin.Text.Trim() == "" ? " Enter Project Name (In Hindi)" : "";
                if (msg.Trim() == "")
                {
                    if (btnSave.Text == "Save")
                    {
                        ds = objdb.ByProcedure("USP_Mst_Projects",
                        new string[] { "flag", "Project_Name_Eng", "Project_Name_Hin", "CreatedBy" },
                        new string[] { "0", txt_ProjectsNameEng.Text.Trim(), txt_ProjectsNameHin.Text.Trim(), Session["Emp_ID"].ToString() }, "dataset");
                    }
                    else if (btnSave.Text == "Edit" && !string.IsNullOrEmpty(hfProjectID.Value))
                    {
                        ds = objdb.ByProcedure("USP_Mst_Projects",
                        new string[] { "flag", "Project_ID", "Project_Name_Eng", "Project_Name_Hin", "CreatedBy" },
                        new string[] { "1", hfProjectID.Value,txt_ProjectsNameEng.Text.Trim(), txt_ProjectsNameHin.Text.Trim(), Session["Emp_ID"].ToString() }, "dataset");
                    }

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["Msg"]) == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", Convert.ToString(ds.Tables[0].Rows[0]["ErrorMsg"]));
                            ClearText();
                            FillGrid();
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-exclamation", "alert-info", "Alert !", Convert.ToString(ds.Tables[0].Rows[0]["ErrorMsg"]));
                        }
                    }

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-exclamation", "alert-info", "Alert !", msg);
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
            gridProject.DataSource = null;
            gridProject.DataBind();

            ds = objdb.ByProcedure("USP_Mst_Projects",
                new string[] { "flag" },
                new string[] { "3" }, "dataset");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gridProject.DataSource = ds;
                gridProject.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        txt_ProjectsNameEng.Text = "";
        txt_ProjectsNameHin.Text = "";
        btnSave.Text = "Save";
        hfProjectID.Value = "0";

	}
    protected void gridProject_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            if (e.CommandName == "EditRecord")
            {
				Label lbProject_Name_Eng = (Label)row.FindControl("lblProject_Name_Eng");
				Label lbProject_Name_Hin = (Label)row.FindControl("lblProject_Name_Hin");
                txt_ProjectsNameEng.Text = lbProject_Name_Eng.Text;
                txt_ProjectsNameHin.Text = lbProject_Name_Hin.Text;
				btnSave.Text = "Edit";
				hfProjectID.Value = e.CommandArgument.ToString();

			}
            else if (e.CommandName == "ChangeStatus")
            {
                DataSet ds1 = new DataSet();
                ds1 = objdb.ByProcedure("USP_Mst_Projects", new string[] { "flag", "Project_ID", "CreatedBy" }, new string[] { "2",e.CommandArgument.ToString(), objdb.createdBy() }, "dataset");
                if (ds1 != null)
                {
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You !", ds1.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                FillGrid();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-info", "Alert !", ds1.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }

                        }

                    }

                }

            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 6 : ", ex.Message.ToString());
        }
    }
}