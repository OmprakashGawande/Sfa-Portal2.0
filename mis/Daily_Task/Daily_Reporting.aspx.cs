using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Drawing;
using System.IO;
using System.Net;
//using Microsoft.Reporting.WebForms;
using System.Activities.Expressions;


public partial class mis_Daily_Task_Daily_Reporting : System.Web.UI.Page
{
	APIProcedure objdb = new APIProcedure();
	DataSet ds, ds1;
	CultureInfo cult = new CultureInfo("gu-IN", true);
	IFormatProvider culture = new CultureInfo("en-US", true);
	protected void Page_Load(object sender, EventArgs e)
	{
		lblMsg.Text = string.Empty;
        if (objdb.createdBy() != null && objdb.Office_ID() != null && Session["Emp_ID"] != null)
		{
			if (!IsPostBack)
			{
				btnSendReport.Visible = false;
				txtDate.Attributes.Add("readonly", "readonly");
				txtEmp.Attributes.Add("readonly", "readonly");
				txtEmp.Text = Session["Emp_Name"].ToString();

                DateTime dd = DateTime.Now;
                txtDate.Text = (Convert.ToDateTime(dd, culture).ToString("dd/MM/yyyy"));
                fillProjects();
				fillWorkCategory();
				DataTable dt = new DataTable();
				ViewState["AddTaskDetails"] = dt;
				BindGrid_TempTable();
				clr();
            }
		}
		else
		{
			objdb.redirectToHome();
		}
	}
	protected void txtDate_TextChanged(object sender, EventArgs e)
	{
		lblMsg.Text = "";
		gridvew1.DataSource = null;
		gridvew1.DataBind();
		btnSendReport.Visible = false;
		BindGrid_TempTable();
		clr();
        DateTime dat = DateTime.Now;
        DateTime TaskDate = Convert.ToDateTime(txtDate.Text.Trim(), cult);
    //    btnAdd.Enabled = true;
    //    if (dat.Date != TaskDate.Date)
    //    {
    //        if (dat.TimeOfDay >= new TimeSpan(10, 0, 0))
    //        {
				//btnAdd.Enabled = false;
    //        }
    //    }
    }
	protected void fillProjects()
	{
		try
		{
			ddlProject.Items.Clear();
			ds1 = objdb.ByProcedure("USP_Daily_Task_GetAllProjects", new string[] { }, new string[] { }, "dataset");
			if (ds1 != null)
			{
				if (ds1.Tables.Count > 0)
				{
					if (ds1.Tables[0].Rows.Count > 0)
					{
						ddlProject.DataValueField = "Project_ID";
						ddlProject.DataTextField = "Project_Name";
						ddlProject.DataSource = ds1;
						ddlProject.DataBind();
						ddlProject.Items.Insert(0, new ListItem("Select", "0"));
					}
					else
					{
						ddlProject.Items.Insert(0, new ListItem("No Record Found", "0"));
					}
				}
				else
				{
					ddlProject.Items.Insert(0, new ListItem("No Record Found", "0"));
				}
			}
			else
			{
				ddlProject.Items.Insert(0, new ListItem("No Record Found", "0"));
			}
			if (ds1 != null) ds1.Dispose();

		}
		catch (Exception ex)
		{

			lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7: " + ex.Message.ToString());
		}
	}
	protected void fillWorkCategory()
	{
		try
		{
			ddlWorkCategoryId.Items.Clear();
			ds1 = objdb.ByProcedure("USP_Daily_Task_GetAllWorkCategory", new string[] { }, new string[] { }, "dataset");
			if (ds1 != null)
			{
				if (ds1.Tables.Count > 0)
				{
					if (ds1.Tables[0].Rows.Count > 0)
					{
						ddlWorkCategoryId.DataValueField = "WorkCategoryId";
						ddlWorkCategoryId.DataTextField = "WorkCategoryEng";
						ddlWorkCategoryId.DataSource = ds1;
						ddlWorkCategoryId.DataBind();
						ddlWorkCategoryId.Items.Insert(0, new ListItem("Select", "0"));
					}
					else
					{
						ddlWorkCategoryId.Items.Insert(0, new ListItem("No Record Found", "0"));
					}
				}
				else
				{
					ddlWorkCategoryId.Items.Insert(0, new ListItem("No Record Found", "0"));
				}
			}
			else
			{
				ddlWorkCategoryId.Items.Insert(0, new ListItem("No Record Found", "0"));
			}
			if (ds1 != null) ds1.Dispose();

		}
		catch (Exception ex)
		{

			lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7: " + ex.Message.ToString());
		}
	}

	protected void gridvew1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
	{
		try
		{

			lblMsg.Text = "";
			int index = Convert.ToInt32(e.RowIndex);
			DataTable dt = ViewState["AddTaskDetails"] as DataTable;
			dt.Rows[index].Delete();
			ViewState["dt"] = dt;
			if (dt != null)
			{
				if (dt != null && dt.Columns.Count > 0 && dt.Rows.Count > 0)
				{
					decimal TotalAmount = 0;
					gridvew1.DataSource = dt;
					gridvew1.DataBind();
					ViewState["TotalAmount"] = TotalAmount.ToString();
				}
				else
				{
					gridvew1.DataSource = ViewState["dt"] as DataTable;
					gridvew1.DataBind();
				}
			}

			int gridRows = gridvew1.Rows.Count;
			if (gridRows > 0)
			{
				btnSendReport.Visible = true;
			}
			else
			{
				btnSendReport.Visible = false;
			}
		}

		catch (Exception ex)
		{
			lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", ex.Message.ToString());
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		try
		{
			if (Page.IsValid)
			{
				string msg = "";
				if (btnAdd.Text == "Add")
				{
					foreach (GridViewRow row in gridvew1.Rows)
					{
						Label lbProject_Id = (Label)row.FindControl("lblProject_Id");
						Label lbWorkCategoryId = (Label)row.FindControl("lblWorkCategoryId");
						if (lbProject_Id.Text == ddlProject.SelectedValue.ToString() && lbWorkCategoryId.Text == ddlWorkCategoryId.SelectedValue.ToString())
						{
							msg += "PROJECT AND WORK CATEGORY IS ALREADY AVAILABLE.\\n";
							break;
						}
					}
				}
				decimal hours = 0;
				decimal minutes = 0;
				decimal totalhours = 0;
				if (ddlProject.SelectedValue == "0") { msg += "SELECT PROJECT NAME\\n"; }
				if (ddlWorkCategoryId.SelectedValue == "0") { msg += "SELECT WORK CATEGORY\\n"; }
				if (txtTotalHours.Text == "") { msg += "ENTER HOURS\\n"; } else { hours = Convert.ToDecimal(txtTotalHours.Text.Trim()); }
				if (txtMinutes.Text == "") { msg += "ENTER MINUTES\\n"; } else { minutes = Convert.ToDecimal(txtMinutes.Text.Trim()); }
				if (txtAssignedBy.Text.Trim() == "") { msg += "ENTER NAME OF THE PERSON ASSIGNED BY\\n"; }
				if (txtDescription.Text.Trim() == "") { msg += "ENTER WORK DESCRIPTION\\n"; }
				if (msg.Trim() == "")
				{
					totalhours = (hours * 60) + minutes;
					if (totalhours <= 0)
					{
						msg += " WORKING TIME CANNOT BE ZERO\\n";
					}

				}
				if (msg.Trim() == "")
				{
					if (btnAdd.Text == "Add")
					{
						addTask_TempTable();
					}
					else
					{
						update_TempTable();
					}
				}
				else
				{
					Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
				}
			}




		}
		catch (Exception ex)
		{
			lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", ex.Message.ToString());
		}
	}

	protected void addTask_DataTable()
	{
        DateTime dat = DateTime.Now;
        DateTime TaskDate = Convert.ToDateTime(txtDate.Text.Trim(), cult);
        //if (dat.Date != TaskDate.Date)
        //{
        //    if (dat.TimeOfDay >= new TimeSpan(10, 0, 0))
        //    {
        //        lblMsg.Text = objdb.Alert("fa-exclamation", "alert-info", "Alert !", "Task cannot be added");
        //        return;
        //    }
        //}
        if (gridvew1.Rows.Count > 0)
		{
			DataTable dtAddTask = (DataTable)ViewState["AddTaskDetails"];

			dtAddTask.Rows.Add(ddlProject.SelectedValue, txtTotalHours.Text.Trim(), txtMinutes.Text.Trim(), txtDescription.Text.Trim().Replace(Environment.NewLine, "<br />"), ddlProject.SelectedItem.Text, ddlWorkCategoryId.SelectedValue, ddlWorkCategoryId.SelectedItem.Text);
			ViewState["AddTaskDetails"] = dtAddTask;
			dtAddTask.Dispose();
			this.BindGrid_DataTable();
		}
		else
		{
			DataTable dt = new DataTable();

			dt.Columns.AddRange(new DataColumn[7] { new DataColumn("Project_Id",typeof(int))
						 ,new DataColumn("Task_Hours",typeof(decimal))
						 ,new DataColumn("Task_Minutes",typeof(decimal))
						 ,new DataColumn("Work_Description",typeof(string))
						  ,new DataColumn("Project_Name",typeof(string))
						  ,new DataColumn("WorkCategoryId",typeof(int))
						   ,new DataColumn("WorkCategory",typeof(string))
							 });
			dt.Rows.Add(ddlProject.SelectedValue, txtTotalHours.Text.Trim(), txtMinutes.Text.Trim(), txtDescription.Text.Trim().Replace(Environment.NewLine, "<br />"), ddlProject.SelectedItem.Text, ddlWorkCategoryId.SelectedValue, ddlWorkCategoryId.SelectedItem.Text);
			ViewState["AddTaskDetails"] = dt;
			dt.Dispose();
			this.BindGrid_DataTable();
		}
		clr();
	}

	protected void addTask_TempTable()
	{
		DateTime dat = DateTime.Now;
		DateTime TaskDate = Convert.ToDateTime(txtDate.Text.Trim(), cult);
        //if (dat.Date != TaskDate.Date)
        //{
        //    if (dat.TimeOfDay >= new TimeSpan(10, 0, 0))
        //    {
        //        lblMsg.Text = objdb.Alert("fa-exclamation", "alert-info", "Alert !", "Task cannot be added");
        //        return;
        //    }
        //}


        string ddate = Convert.ToDateTime(txtDate.Text.Trim(), cult).ToString("yyyy/MM/dd");
		ds = objdb.ByProcedure("USP_Daily_Task_Temp",
			new string[] { "Flag",
				"Task_Id_ChildTemp",
							"Emp_Id" ,
							"Task_Hours",
							"Task_Minutes" ,
							"Work_Description",
							"Project_Id" ,
							"Project_Name" ,
							"Task_date",
							"WorkCategoryId",
							"WorkCategory",
							"AssignedBy"},
			new string[] {"1",
				hfTask_Id_ChildTemp.Value,
			Convert.ToString(Session["Emp_ID"]),
				txtTotalHours.Text.Trim(),
				txtMinutes.Text.Trim(),
				txtDescription.Text.Trim().Replace(Environment.NewLine, "<br />"),
				ddlProject.SelectedValue,
				ddlProject.SelectedItem.Text,
				ddate,
				ddlWorkCategoryId.SelectedValue,
				ddlWorkCategoryId.SelectedItem.Text,
				txtAssignedBy.Text.Trim()
			},
			"dataset"
			);
		if (ds != null && ds.Tables.Count > 0)
		{
			if (ds != null && ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
				{
					//lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You !", ds.Tables[0].Rows[0]["Errormsg"].ToString());
					clr();
					BindGrid_TempTable();
				}
				else
				{
					lblMsg.Text = objdb.Alert("fa-ban", "alert-info", "Alert !", ds.Tables[0].Rows[0]["Errormsg"].ToString());
				}
			}
		}
	}
	protected void update_TempTable()
	{
		string ddate = Convert.ToDateTime(txtDate.Text.Trim(), cult).ToString("yyyy/MM/dd");
		ds = objdb.ByProcedure("USP_Daily_Task_Temp",
			new string[] { "Flag",
				"Task_Id_ChildTemp",
							"Emp_Id" ,
							"Task_Hours",
							"Task_Minutes" ,
							"Work_Description",
							"Project_Id" ,
							"Project_Name" ,
							"Task_date",
							"WorkCategoryId",
							"WorkCategory" ,
							"AssignedBy"},
			new string[] {"4",
				hfTask_Id_ChildTemp.Value,
			Convert.ToString(Session["Emp_ID"]),
				txtTotalHours.Text.Trim(),
				txtMinutes.Text.Trim(),
				txtDescription.Text.Trim().Replace(Environment.NewLine, "<br />"),
				ddlProject.SelectedValue,
				ddlProject.SelectedItem.Text,
				ddate,
				ddlWorkCategoryId.SelectedValue,
				ddlWorkCategoryId.SelectedItem.Text,
				txtAssignedBy.Text.Trim()
			},
			"dataset"
			);
		if (ds != null && ds.Tables.Count > 0)
		{
			if (ds != null && ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
				{
					//lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You !", ds.Tables[0].Rows[0]["Errormsg"].ToString());
					clr();
					BindGrid_TempTable();
				}
				else
				{
					lblMsg.Text = objdb.Alert("fa-ban", "alert-info", "Alert !", ds.Tables[0].Rows[0]["Errormsg"].ToString());
				}
			}
		}
	}
	protected void BindGrid_DataTable()
	{
		DataTable dtAddTask = (DataTable)ViewState["AddTaskDetails"];
		if (dtAddTask.Rows.Count > 0)
		{
			gridvew1.DataSource = dtAddTask;
			gridvew1.DataBind();
		}
		else
		{
			gridvew1.DataSource = null;
			gridvew1.DataBind();
		}
		int gridRows = gridvew1.Rows.Count;
		if (gridRows > 0)
		{
			btnSendReport.Visible = true;
		}
		else
		{
			btnSendReport.Visible = false;
		}
		if (dtAddTask != null) { dtAddTask.Dispose(); }
	}
	protected void BindGrid_TempTable()
	{
		gridvew1.DataSource = null;
		gridvew1.DataBind();
		string ddate = Convert.ToDateTime(txtDate.Text.Trim(), cult).ToString("yyyy/MM/dd");
		ds = objdb.ByProcedure("USP_Daily_Task_Temp",
			new string[] { "Flag",
							"Emp_Id" ,
							"Task_date" },
			new string[] {"2",
							Convert.ToString(Session["Emp_ID"]),
							ddate},
			"dataset"
			);
		if (ds != null && ds.Tables.Count > 0)
		{
			if (ds != null && ds.Tables[0].Rows.Count > 0)
			{
				gridvew1.DataSource = ds.Tables[0];
				gridvew1.DataBind();
			}
		}
		int gridRows = 0;
		foreach (GridViewRow row in gridvew1.Rows)
		{
			Label lbl = row.FindControl("lblIsActive") as Label;
			if (lbl != null && lbl.Text == "True")
			{
				gridRows++;
			}
		}
		if (gridRows > 0)
		{
			btnSendReport.Visible = true;
		}
		else
		{
			btnSendReport.Visible = false;
		}
		if (ds != null) { ds.Dispose(); }
	}
	protected void btnSendReport_Click(object sender, EventArgs e)
	{
		try
		{
			lblMsg.Text = "";
			string msg = "";
			if (Page.IsValid)
			{
				if (gridvew1.Rows.Count <= 0) { msg += "Select atleast one project\\n"; }

				if (msg == "")
				{
					if (btnSendReport.Text == "Send Report")
					{
						SendReport_TempTable();
					}
				}
				else
				{
					Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
				}

			}
		}
		catch (Exception ex)
		{
			lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", ex.Message.ToString());
		}
	}

	protected void SendReport_TempTable()
	{
        //DateTime dat = DateTime.Now;
        //DateTime TaskDate = Convert.ToDateTime(txtDate.Text.Trim(), cult);
        //if (dat.Date != TaskDate.Date)
        //{
        //    if (dat.TimeOfDay >= new TimeSpan(10, 0, 0))
        //    {
        //        lblMsg.Text = objdb.Alert("fa-exclamation", "alert-info", "Alert !", "Task cannot be submitted");
        //        return;
        //    }
        //}
        string ddate = Convert.ToDateTime(txtDate.Text.Trim(), cult).ToString("yyyy/MM/dd");
		ds = objdb.ByProcedure("USP_Daily_Task_Temp", new string[] {"Flag", "Task_date", "Emp_ID", "Office_Id", "User_TypeID", "CreatedBy", "CreatedByIp" },
														  new string[] {"5", ddate, Session["Emp_ID"].ToString(), Session["Office_ID"].ToString(), Session["UserTypeId"].ToString(), Session["Emp_ID"].ToString().ToString(), objdb.GetLocalIPAddress() }, "TableSave");


		if (ds != null && ds.Tables.Count > 0)
		{
			if (ds != null && ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
				{
					lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You !", ds.Tables[0].Rows[0]["Errormsg"].ToString());
					gridvew1.DataSource = null;
					gridvew1.DataBind();
					btnSendReport.Visible = false;
					clr();
				}
				else
				{
					lblMsg.Text = objdb.Alert("fa-ban", "alert-info", "Alert !", ds.Tables[0].Rows[0]["Errormsg"].ToString());
				}

			}
		}
	}
	protected void SendReport_DataTable()
	{
        DateTime dat = DateTime.Now;
        DateTime TaskDate = Convert.ToDateTime(txtDate.Text.Trim(), cult);
        if (dat.Date != TaskDate.Date)
        {
            if (dat.TimeOfDay >= new TimeSpan(10, 0, 0))
            {
                lblMsg.Text = objdb.Alert("fa-exclamation", "alert-info", "Alert !", "Task cannot be submitted");
                return;
            }
        }
        string msg = "";

		DataTable dt = (DataTable)ViewState["AddTaskDetails"];

		if (dt.Rows.Count <= 0) { msg += "Select atleast one project\\n"; }

		if (msg == "")
		{

			string ddate = Convert.ToDateTime(txtDate.Text.Trim(), cult).ToString("yyyy/MM/dd");
			ds = objdb.ByProcedure("USP_Daily_Task_Insert", new string[] { "Task_date", "Emp_ID", "Office_Id", "User_TypeID", "CreatedBy", "CreatedByIp" },
															  new string[] { ddate, Session["Emp_ID"].ToString(), Session["Office_ID"].ToString(), Session["UserTypeId"].ToString(), Session["Emp_ID"].ToString().ToString(), objdb.GetLocalIPAddress() },
				new string[] { "typeDaliy_Task" }, new DataTable[] { dt }, "TableSave");


			if (ds != null && ds.Tables.Count > 0)
			{
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
					{
						lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You !", ds.Tables[0].Rows[0]["Errormsg"].ToString());
						gridvew1.DataSource = null;
						gridvew1.DataBind();
						btnSendReport.Visible = false;
						dt.Clear();
						ViewState["AddTaskDetails"] = dt;
						clr();
					}
					else
					{
						lblMsg.Text = objdb.Alert("fa-ban", "alert-info", "Alert !", ds.Tables[0].Rows[0]["Errormsg"].ToString());
					}

				}
			}

		}
		else
		{
			Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
		}
	}
	protected void clr()
	{
		txtAssignedBy.Text = "";
		txtDescription.Text = "";
		txtMinutes.Text = "0";
		txtTotalHours.Text = "0";
		ddlProject.ClearSelection();
		ddlWorkCategoryId.ClearSelection();
		hfTask_Id_ChildTemp.Value = "0";
		btnAdd.Text = "Add";
		ddlProject.Enabled = true;
		ddlWorkCategoryId.Enabled = true;
	}

	protected void gridvew1_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		lblMsg.Text = "";
		GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
		if (e.CommandName == "btnStatus")
		{
			Label lblSts = (Label)row.FindControl("lblIsActive");
			string sts = "1";
			if (lblSts.Text == "True")
			{
				sts = "0";
			}
			ds = objdb.ByProcedure("USP_Daily_Task_Temp",
			new string[] { "Flag", "Task_Id_ChildTemp", "IsActive" },
			new string[] { "3", e.CommandArgument.ToString(), sts },
			"dataset"
			);
			if (ds != null && ds.Tables.Count > 0)
			{
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					//lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You !", ds.Tables[0].Rows[0]["Errormsg"].ToString());
					clr();
					BindGrid_TempTable();
				}
				else
				{
					lblMsg.Text = objdb.Alert("fa-ban", "alert-info", "Alert !", ds.Tables[0].Rows[0]["Errormsg"].ToString());
				}
			}
		}
		else if (e.CommandName == "btnUpdate")
		{
			Label lbProject_Id = (Label)row.FindControl("lblProject_Id");
			Label lbWorkCategoryId = (Label)row.FindControl("lblWorkCategoryId");
			Label lbTask_Hours = (Label)row.FindControl("lblTask_Hours");
			Label lbTask_Minutes = (Label)row.FindControl("lblTask_Minutes");
			Label lbWork_Description = (Label)row.FindControl("lblWork_Description");
			Label lbAssignedBy = (Label)row.FindControl("lblAssignedBy");
			ddlProject.ClearSelection();
			ddlWorkCategoryId.ClearSelection();
			if (ddlProject.Items.FindByValue(lbProject_Id.Text) != null)
			{
				ddlProject.Items.FindByValue(lbProject_Id.Text).Selected = true;
				ddlProject.Enabled = false;
			}
			if (ddlWorkCategoryId.Items.FindByValue(lbWorkCategoryId.Text) != null)
			{
				ddlWorkCategoryId.Items.FindByValue(lbWorkCategoryId.Text).Selected = true;
				ddlWorkCategoryId.Enabled = false;
			}
			txtTotalHours.Text = lbTask_Hours.Text;
			txtMinutes.Text = lbTask_Minutes.Text;
			txtDescription.Text = lbWork_Description.Text.Replace("<br />", Environment.NewLine) ;
			txtAssignedBy.Text = lbAssignedBy.Text;
			hfTask_Id_ChildTemp.Value = e.CommandArgument.ToString();
			btnAdd.Text = "Update";


		}
	}
}