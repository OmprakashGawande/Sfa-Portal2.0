using ClosedXML.Excel;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;
using System.Collections.Generic;

public partial class mis_Report_DailyTaskReport : System.Web.UI.Page
{

    APIProcedure objdb = new APIProcedure();
    DataSet ds, ds1;
    CultureInfo cult = new CultureInfo("gu-IN", true);
    // Declare these at class level (outside this method):
    string lastEmpName = string.Empty;
    string lastProjectName = string.Empty;
    string lastTaskAllocated = string.Empty;
    int empRowIndex = -1;
    int projRowIndex = -1;
    int taskRowIndex = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {



            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected string GetStatusCss(string status)
    {
        switch (status.ToLower())
        {
            case "task not filled":
                return "btn btn-danger btn-sm";  // Red
            case "on leave":
                return "btn btn-warning btn-sm"; // Yellow
            default:
                return "btn btn-success btn-sm"; // Optional fallback
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        BindReport();

    }

    private void BindReport()
    {
        // Convert date to string in the required format (e.g., 'yyyy-MM-dd')

        DateTime DateVal;

        string FromDate = !string.IsNullOrWhiteSpace(txtDate.Text) && DateTime.TryParse(txtDate.Text, cult, DateTimeStyles.None, out DateVal)
            ? DateVal.ToString("yyyy/MM/dd ")
            : "";
        // Call the stored procedure using objdb.ByProcedure (assumed signature)
        DataSet ds = objdb.ByProcedure("Usp_GetDailyTaskReportByDate",
            new string[] { "Date" },                     // parameter names
            new string[] { FromDate },                     // parameter values
            "dataset");

        if (ds != null && ds.Tables.Count > 0)
        {
            // Bind Filled Tasks (first result set)
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].DefaultView.Sort = "ProjectName ASC, EmployeeName ASC, TaskAllocatedOrFilled ASC";
                gvFilledTasks.DataSource = ds.Tables[0].DefaultView;
                gvFilledTasks.DataBind();

            }
            else
            {
                gvFilledTasks.DataSource = null;
                gvFilledTasks.DataBind();
            }

            // Bind Not Filled Tasks (second result set)
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                gvNotFilledTasks.DataSource = ds.Tables[1];
                gvNotFilledTasks.DataBind();
            }
            else
            {
                gvNotFilledTasks.DataSource = null;
                gvNotFilledTasks.DataBind();
            }
            // Bind Project Wise Detail 
            if (ds.Tables.Count > 1 && ds.Tables[2].Rows.Count > 0)
            {
                GridProjectwise.DataSource = ds.Tables[2];
                GridProjectwise.DataBind();
            }
            else
            {
                GridProjectwise.DataSource = null;
                GridProjectwise.DataBind();
            }
        }
        else
        {
            gvFilledTasks.DataSource = null;
            gvFilledTasks.DataBind();
            gvNotFilledTasks.DataSource = null;
            gvNotFilledTasks.DataBind();
            GridProjectwise.DataSource = null;
            GridProjectwise.DataBind();
        }
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtDate.Text))
        {
            lblSelectedDate.Text = "Date: " + txtDate.Text;
            Label1.Text = "Date: " + txtDate.Text;
            Label2.Text = "Date: " + txtDate.Text;
            gvFilledTasks.DataSource = null;
            gvFilledTasks.DataBind();

            gvNotFilledTasks.DataSource = null;
            gvNotFilledTasks.DataBind();
            GridProjectwise.DataSource = null;
            GridProjectwise.DataBind();

        }
        else
        {
            lblSelectedDate.Text = "";
        }
    }


    protected void gvFilledTasks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Serial Number
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            if (lblSerial != null)
            {
                lblSerial.Text = (e.Row.RowIndex + 1).ToString();
            }

            // Get current values
            string currentEmpName = DataBinder.Eval(e.Row.DataItem, "EmployeeName").ToString();
            string currentProjectName = DataBinder.Eval(e.Row.DataItem, "ProjectName").ToString();
            string currentTaskAllocated = DataBinder.Eval(e.Row.DataItem, "TaskAllocatedOrFilled").ToString();

            // ---- ProjectName merge (column index 1) ----
            if (lastProjectName == currentProjectName)
            {
                e.Row.Cells[1].Visible = false;
                GridViewRow prevRow = gvFilledTasks.Rows[projRowIndex];
                prevRow.Cells[1].RowSpan = (prevRow.Cells[1].RowSpan == 0 ? 1 : prevRow.Cells[1].RowSpan) + 1;
            }
            else
            {
                lastProjectName = currentProjectName;
                projRowIndex = e.Row.RowIndex;
            }

            // ---- EmployeeName merge (column index 2) ----
            if (lastEmpName == currentEmpName)
            {
                e.Row.Cells[2].Visible = false;
                GridViewRow prevRow = gvFilledTasks.Rows[empRowIndex];
                prevRow.Cells[2].RowSpan = (prevRow.Cells[2].RowSpan == 0 ? 1 : prevRow.Cells[2].RowSpan) + 1;
            }
            else
            {
                lastEmpName = currentEmpName;
                empRowIndex = e.Row.RowIndex;
            }

            // ---- TaskAllocatedOrFilled merge (column index 3) ----
            if (lastTaskAllocated == currentTaskAllocated)
            {
                e.Row.Cells[3].Visible = false;
                GridViewRow prevRow = gvFilledTasks.Rows[taskRowIndex];
                prevRow.Cells[3].RowSpan = (prevRow.Cells[3].RowSpan == 0 ? 1 : prevRow.Cells[3].RowSpan) + 1;
            }
            else
            {
                lastTaskAllocated = currentTaskAllocated;
                taskRowIndex = e.Row.RowIndex;
            }
        }
    }


    protected void gvNotFilledTasks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            if (lblSerial != null)
            {
                lblSerial.Text = (e.Row.RowIndex + 1).ToString();
            }
        }
    }

    protected void GridProjectwise_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            if (lblSerial != null)
            {
                lblSerial.Text = (e.Row.RowIndex + 1).ToString();
            }
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            // Sheet 1: Daily Reporting List
            var ws1 = wb.Worksheets.Add("Daily Reporting List");
            ws1.Cell(1, 1).Value = "Daily Reporting List";
            ws1.Cell(1, 1).Style.Font.Bold = true;
            ws1.Range(1, 1, 1, 5).Merge(); // adjust column count to match data
            ws1.Cell(2, 1).Value = lblSelectedDate.Text;
            ws1.Cell(2, 1).Style.Font.Bold = true;
            ws1.Range(2, 1, 2, 5).Merge(); // adjust column count
            AddGridViewToWorksheet(gvFilledTasks, ws1, 3);

            // Sheet 2: Project Wise Report
            var ws2 = wb.Worksheets.Add("Project Wise Report");
            ws2.Cell(1, 1).Value = "Project Wise Report";
            ws2.Cell(1, 1).Style.Font.Bold = true;
            ws2.Range(1, 1, 1, 5).Merge();
            ws2.Cell(2, 1).Value = Label2.Text;
            ws2.Cell(2, 1).Style.Font.Bold = true;
            ws2.Range(2, 1, 2, 5).Merge();
            AddGridViewToWorksheet(GridProjectwise, ws2, 3);

            // Sheet 3: Employee Task Not Filled
            var ws3 = wb.Worksheets.Add("Emp Task Not Filled");
            ws3.Cell(1, 1).Value = "Employee Task Not Filled";
            ws3.Cell(1, 1).Style.Font.Bold = true;
            ws3.Range(1, 1, 1, 5).Merge();
            ws3.Cell(2, 1).Value = Label1.Text;
            ws3.Cell(2, 1).Style.Font.Bold = true;
            ws3.Range(2, 1, 2, 5).Merge();
            AddGridViewToWorksheet(gvNotFilledTasks, ws3, 3);

            // Save and send the workbook
            using (MemoryStream stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                byte[] byteArray = stream.ToArray();

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=DailyTaskReport" + DateTime.Now + ".xlsx");
                Response.BinaryWrite(byteArray);
                Response.End();
            }
        }
    }

    //private void AddGridViewToWorksheet(GridView gridView, IXLWorksheet worksheet, int startRow)
    //{
    //    if (gridView.Rows.Count == 0)
    //    {
    //        worksheet.Cell(startRow, 1).Value = "No data available";
    //        return;
    //    }

    //    int currentRow = startRow;
    //    int excelCol = 1;

    //    // Sr. No. Header
    //    worksheet.Cell(currentRow, excelCol).Value = "Sr. No.";
    //    worksheet.Cell(currentRow, excelCol).Style.Font.Bold = true;

    //    List<int> exportColumnIndexes = new List<int>();
    //    int dataStartCol = excelCol + 1;

    //    // Add headers
    //    for (int i = 0; i < gridView.Columns.Count; i++)
    //    {
    //        if (gridView.Columns[i].Visible)
    //        {
    //            string header = gridView.Columns[i].HeaderText.Trim();
    //            if (!string.IsNullOrEmpty(header) && header != "&nbsp;")
    //            {
    //                worksheet.Cell(currentRow, dataStartCol).Value = HttpUtility.HtmlDecode(header);
    //                worksheet.Cell(currentRow, dataStartCol).Style.Font.Bold = true;

    //                // If column is Working Hours, set text format
    //                if (header.Equals("Working Hours", StringComparison.OrdinalIgnoreCase))
    //                {
    //                    worksheet.Column(dataStartCol).Style.NumberFormat.Format = "@";
    //                }

    //                exportColumnIndexes.Add(i);
    //                dataStartCol++;
    //            }
    //        }
    //    }

    //    currentRow++;

    //    // Track merged values
    //    string lastProjectName = null;
    //    int projStartRow = -1, projCount = 0;

    //    string lastEmpName = null;
    //    int empStartRow = -1, empCount = 0;

    //    string lastTaskName = null;
    //    int taskStartRow = -1, taskCount = 0;

    //    for (int rowIndex = 0; rowIndex < gridView.Rows.Count; rowIndex++)
    //    {
    //        GridViewRow row = gridView.Rows[rowIndex];
    //        int currentCol = 1;

    //        // Sr. No.
    //        worksheet.Cell(currentRow, currentCol++).Value = rowIndex + 1;

    //        for (int k = 0; k < exportColumnIndexes.Count; k++)
    //        {
    //            int colIndex = exportColumnIndexes[k];
    //            string text = HttpUtility.HtmlDecode(row.Cells[colIndex].Text.Trim());

    //            if (string.IsNullOrWhiteSpace(text) || text == "&nbsp;")
    //                text = "";

    //            int excelColIndex = currentCol;

    //            // --- ProjectName (colIndex == 1) ---
    //            if (colIndex == 1)
    //            {
    //                if (text == lastProjectName)
    //                {
    //                    projCount++;
    //                }
    //                else
    //                {
    //                    if (projCount > 1)
    //                    {
    //                        worksheet.Range(projStartRow, excelColIndex, projStartRow + projCount - 1, excelColIndex).Merge();
    //                        worksheet.Cell(projStartRow, excelColIndex).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
    //                    }

    //                    lastProjectName = text;
    //                    projStartRow = currentRow;
    //                    projCount = 1;

    //                    if (text.Contains("/") && !text.Contains(" "))
    //                        worksheet.Cell(currentRow, excelColIndex).Value = "'" + text;
    //                    else
    //                        worksheet.Cell(currentRow, excelColIndex).Value = text;
    //                }
    //            }
    //            // --- EmployeeName (colIndex == 2) ---
    //            else if (colIndex == 2)
    //            {
    //                if (text == lastEmpName)
    //                {
    //                    empCount++;
    //                }
    //                else
    //                {
    //                    if (empCount > 1)
    //                    {
    //                        worksheet.Range(empStartRow, excelColIndex, empStartRow + empCount - 1, excelColIndex).Merge();
    //                        worksheet.Cell(empStartRow, excelColIndex).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
    //                    }

    //                    lastEmpName = text;
    //                    empStartRow = currentRow;
    //                    empCount = 1;
    //                    worksheet.Cell(currentRow, excelColIndex).Value = text;
    //                }
    //            }
    //            // --- TaskAllocatedOrFilled (colIndex == 3) ---
    //            else if (colIndex == 3)
    //            {
    //                if (text == lastTaskName)
    //                {
    //                    taskCount++;
    //                }
    //                else
    //                {
    //                    if (taskCount > 1)
    //                    {
    //                        worksheet.Range(taskStartRow, excelColIndex, taskStartRow + taskCount - 1, excelColIndex).Merge();
    //                        worksheet.Cell(taskStartRow, excelColIndex).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
    //                    }

    //                    lastTaskName = text;
    //                    taskStartRow = currentRow;
    //                    taskCount = 1;
    //                    worksheet.Cell(currentRow, excelColIndex).Value = text;
    //                }
    //            }
    //            // --- All other columns ---
    //            else
    //            {
    //                if (gridView.Columns[colIndex].HeaderText.Trim().Equals("Working Hours", StringComparison.OrdinalIgnoreCase))
    //                {
    //                    // Force Excel to treat as text so "09 : 09" doesn't become datetime
    //                    worksheet.Cell(currentRow, excelColIndex).Value = "'" + text;
    //                }
    //                else
    //                {
    //                    worksheet.Cell(currentRow, excelColIndex).Value = text;
    //                }
    //            }

    //            currentCol++;
    //        }

    //        currentRow++;
    //    }

    //    // Final merge pass
    //    if (projCount > 1)
    //    {
    //        int col = exportColumnIndexes.IndexOf(1) + 2;
    //        worksheet.Range(projStartRow, col, projStartRow + projCount - 1, col).Merge();
    //        worksheet.Cell(projStartRow, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
    //    }

    //    if (empCount > 1)
    //    {
    //        int col = exportColumnIndexes.IndexOf(2) + 2;
    //        worksheet.Range(empStartRow, col, empStartRow + empCount - 1, col).Merge();
    //        worksheet.Cell(empStartRow, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
    //    }

    //    if (taskCount > 1)
    //    {
    //        int col = exportColumnIndexes.IndexOf(3) + 2;
    //        worksheet.Range(taskStartRow, col, taskStartRow + taskCount - 1, col).Merge();
    //        worksheet.Cell(taskStartRow, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
    //    }

    //    worksheet.Columns().AdjustToContents();
    //}
    private void AddGridViewToWorksheet(GridView gridView, IXLWorksheet worksheet, int startRow)
    {
        if (gridView.Rows.Count == 0)
        {
            worksheet.Cell(startRow, 1).Value = "No data available";
            return;
        }

        int currentRow = startRow;
        int excelCol = 1;

        // Add "Sr. No." header
        worksheet.Cell(currentRow, excelCol).Value = "Sr. No.";
        worksheet.Cell(currentRow, excelCol).Style.Font.Bold = true;
        excelCol++;

        List<int> exportColumnIndexes = new List<int>();

        // Add headers from GridView
        for (int i = 0; i < gridView.Columns.Count; i++)
        {
            if (gridView.Columns[i].Visible)
            {
                string header = gridView.Columns[i].HeaderText.Trim();
                if (!string.IsNullOrEmpty(header) && header != "&nbsp;" && header != "S.No")
                {
                    worksheet.Cell(currentRow, excelCol).Value = HttpUtility.HtmlDecode(header);
                    worksheet.Cell(currentRow, excelCol).Style.Font.Bold = true;

                    // If column is Working Hours, set text format
                    if (header.Equals("Working Hours", StringComparison.OrdinalIgnoreCase))
                    {
                        worksheet.Column(excelCol).Style.NumberFormat.Format = "@";
                    }

                    exportColumnIndexes.Add(i);
                    excelCol++;
                }
            }
        }

        currentRow++;

        // Track merged values
        string lastProjectName = null;
        int projStartRow = -1, projCount = 0;
        string lastEmpName = null;
        int empStartRow = -1, empCount = 0;
        string lastTaskName = null;
        int taskStartRow = -1, taskCount = 0;

        for (int rowIndex = 0; rowIndex < gridView.Rows.Count; rowIndex++)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            int currentCol = 1;

            // Sr. No.
            worksheet.Cell(currentRow, currentCol++).Value = rowIndex + 1;

            for (int k = 0; k < exportColumnIndexes.Count; k++)
            {
                int colIndex = exportColumnIndexes[k];
                string text = HttpUtility.HtmlDecode(row.Cells[colIndex].Text.Trim());
                if (string.IsNullOrWhiteSpace(text) || text == "&nbsp;")
                    text = "";

                // --- ProjectName (colIndex == 1) ---
                if (colIndex == 1)
                {
                    if (text == lastProjectName)
                    {
                        projCount++;
                    }
                    else
                    {
                        if (projCount > 1)
                        {
                            worksheet.Range(projStartRow, currentCol, projStartRow + projCount - 1, currentCol).Merge();
                            worksheet.Cell(projStartRow, currentCol).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        }
                        lastProjectName = text;
                        projStartRow = currentRow;
                        projCount = 1;
                        worksheet.Cell(currentRow, currentCol).Value = text.Contains("/") && !text.Contains(" ") ? "'" + text : text;
                    }
                }
                // --- EmployeeName (colIndex == 2) ---
                else if (colIndex == 2)
                {
                    if (text == lastEmpName)
                    {
                        empCount++;
                    }
                    else
                    {
                        if (empCount > 1)
                        {
                            worksheet.Range(empStartRow, currentCol, empStartRow + empCount - 1, currentCol).Merge();
                            worksheet.Cell(empStartRow, currentCol).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        }
                        lastEmpName = text;
                        empStartRow = currentRow;
                        empCount = 1;
                        worksheet.Cell(currentRow, currentCol).Value = text;
                    }
                }
                // --- TaskAllocatedOrFilled (colIndex == 3) ---
                else if (colIndex == 3)
                {
                    if (text == lastTaskName)
                    {
                        taskCount++;
                    }
                    else
                    {
                        if (taskCount > 1)
                        {
                            worksheet.Range(taskStartRow, currentCol, taskStartRow + taskCount - 1, currentCol).Merge();
                            worksheet.Cell(taskStartRow, currentCol).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        }
                        lastTaskName = text;
                        taskStartRow = currentRow;
                        taskCount = 1;
                        worksheet.Cell(currentRow, currentCol).Value = text;
                    }
                }
                // --- All other columns ---
                else
                {
                    if (gridView.Columns[colIndex].HeaderText.Trim().Equals("Working Hours", StringComparison.OrdinalIgnoreCase))
                    {
                        // Force Excel to treat as text so "09 : 09" doesn't become datetime
                        worksheet.Cell(currentRow, currentCol).Value = "'" + text;
                    }
                    else
                    {
                        worksheet.Cell(currentRow, currentCol).Value = text;
                    }
                }
                currentCol++;
            }
            currentRow++;
        }

        // Final merge pass
        if (projCount > 1)
        {
            int col = exportColumnIndexes.IndexOf(1) + 2;
            worksheet.Range(projStartRow, col, projStartRow + projCount - 1, col).Merge();
            worksheet.Cell(projStartRow, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        }
        if (empCount > 1)
        {
            int col = exportColumnIndexes.IndexOf(2) + 2;
            worksheet.Range(empStartRow, col, empStartRow + empCount - 1, col).Merge();
            worksheet.Cell(empStartRow, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        }
        if (taskCount > 1)
        {
            int col = exportColumnIndexes.IndexOf(3) + 2;
            worksheet.Range(taskStartRow, col, taskStartRow + taskCount - 1, col).Merge();
            worksheet.Cell(taskStartRow, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        }

        worksheet.Columns().AdjustToContents();
    }






}
