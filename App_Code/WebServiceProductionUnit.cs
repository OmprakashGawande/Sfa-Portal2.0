using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;

/// <summary>
/// Summary description for WebServiceProductionUnit
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebServiceProductionUnit : System.Web.Services.WebService {

    string securityKey = "SFA_MPAGRO_PU";
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    IFormatProvider culture = new CultureInfo("en-US", true);
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    public WebServiceProductionUnit () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region=================Common WebServices=================
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetAllOfficeDetails(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = objdb.ByProcedure("SpAdminOffice",
                                       new string[] { "flag" },
                                       new string[] { "10", }, "Dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetLocation(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = objdb.ByProcedure("sp_tblPULocationMaster",
                    new string[] { "flag" },
                    new string[] { "1" }, "dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetFinancialYear(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = objdb.ByProcedure("ProcCommTablesFill", new string[] { "type" }, new string[] { "2" }, "Dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetFinancialYearSelected(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = objdb.ByProcedure("ProcCommTablesFill", new string[] { "type" }, new string[] { "12" }, "Dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetWareHouseByOffice(string Key, int Office_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = objdb.ByProcedure("Sp_tblSpItemStock",
                                       new string[] { "flag", "Office_Id" },
                                       new string[] { "0", Office_ID.ToString() }, "Dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetItemByOffice(string Key, int Office_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = objdb.ByProcedure("Sp_tblSpItemStock",
                                   new string[] { "flag", "Office_Id" },
                                   new string[] { "17", Office_ID.ToString() }, "Dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetProductByOffice(string Key, int Office_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = objdb.ByProcedure("sp_tblPuDailyProduction",
                                      new string[] { "flag", "ProducUnit_id" },
                                      new string[] { "9", Office_ID.ToString() }, "Dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetItemByWareHouse(string Key, int Office_ID, int Warehouse_id)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = objdb.ByProcedure("Sp_tblSpItemStock",
                                   new string[] { "flag", "Office_Id", "Warehouse_id" },
                                   new string[] { "17", Office_ID.ToString(), Warehouse_id.ToString() }, "Dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    #endregion=================================================

    #region=================Inward Report WebServices=================

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetInwardReport(string Key, int Office_ID, int Warehouse_id, int Item_id, string FromDate, string ToDate, int FY_id)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (FromDate != "")
                {
                    DateTime date3 = DateTime.ParseExact(FromDate, "dd/MM/yyyy", culture);
                    FromDate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    FromDate = "";
                }
                if (ToDate != "")
                {
                    DateTime date3 = DateTime.ParseExact(ToDate, "dd/MM/yyyy", culture);
                    ToDate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    ToDate = "";
                }
                ds = objdb.ByProcedure("Sp_tblSpItemStock",
                new string[] { "flag", "Office_Id", "Warehouse_id", "Item_id", "FromDate", "ToDate", "Phase_id" },
                new string[] { "16", Office_ID.ToString(), Warehouse_id.ToString(), Item_id.ToString(), FromDate, ToDate, FY_id.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    #endregion====================end of Inward Report================

    #region=================Outward Report WebServices=================

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetOutwardReport(string Key, int Office_ID, int Warehouse_id, int Item_id, string FromDate, string ToDate, int FY_id, int Location_id)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (FromDate != "")
                {
                    DateTime date3 = DateTime.ParseExact(FromDate, "dd/MM/yyyy", culture);
                    FromDate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    FromDate = "";
                }
                if (ToDate != "")
                {
                    DateTime date3 = DateTime.ParseExact(ToDate, "dd/MM/yyyy", culture);
                    ToDate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    ToDate = "";
                }
                ds = objdb.ByProcedure("Sp_tblSpItemStock",
                new string[] { "flag", "Office_Id", "Warehouse_id", "Item_id", "FromDate", "ToDate", "Phase_id", "Location_id" },
                new string[] { "18", Office_ID.ToString(), Warehouse_id.ToString(), Item_id.ToString(), FromDate, ToDate, FY_id.ToString(), Location_id.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    #endregion====================end of Inward Report================

    #region=================Available Stock Report WebServices=================

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetAvailableStockReport(string Key, int Office_ID, int Warehouse_id, int Item_id)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                
                ds = objdb.ByProcedure("Sp_tblSpItemStock",
                         new string[] { "flag", "Office_Id", "Warehouse_id", "Item_id", "Phase_id", "FromDate", "ToDate" },
                         new string[] { "19", Office_ID.ToString(), Warehouse_id.ToString(), Item_id.ToString()  }, "Dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    #endregion====================end of Inward Report================

    #region======Daily Production(Badi & Indrapuri) Report WebServices==

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetDailyProductoinReport_BPFOrRTE(string Key, int Office_ID, int Prod_id, string FromDate, string ToDate, int FY_id)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (FromDate != "")
                {
                    DateTime date3 = DateTime.ParseExact(FromDate, "dd/MM/yyyy", culture);
                    FromDate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    FromDate = "";
                }
                if (ToDate != "")
                {
                    DateTime date3 = DateTime.ParseExact(ToDate, "dd/MM/yyyy", culture);
                    ToDate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    ToDate = "";
                }
                ds = objdb.ByProcedure("sp_tblPuDailyProduction",
                     new string[] { "flag", "ProducUnit_id", "Prod_id", "FromDate", "ToDate", "Phase_id" },
                     new string[] { "8", Office_ID.ToString(), Prod_id.ToString(), FromDate, ToDate, FY_id.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    #endregion====================end of Inward Report================

    #region======Daily Production(Babai) Report WebServices==

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetDailyProductoinReport_MAF(string Key, int Office_ID, int Location_id, int PlotLocation_id, string FromDate, string ToDate, int FY_id)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (FromDate != "")
                {
                    DateTime date3 = DateTime.ParseExact(FromDate, "dd/MM/yyyy", culture);
                    FromDate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    FromDate = "";
                }
                if (ToDate != "")
                {
                    DateTime date3 = DateTime.ParseExact(ToDate, "dd/MM/yyyy", culture);
                    ToDate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    ToDate = "";
                }
                ds = objdb.ByProcedure("sp_tblPUBabaiProduction",
                                           new string[] { "flag", "Office_ID", "Location_id", "PlotLocation_id", "Phase_id", "Fromdate", "Todate" },
                     new string[] { "8", Office_ID.ToString(), Location_id.ToString(), PlotLocation_id.ToString(), FY_id.ToString(), FromDate, ToDate }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    #endregion====================end of Inward Report================

    #region=================BFP Dashboard WebServices=================

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetBFPDashboard(string Key, int Office_ID, string Month_Year)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DateTime d = Convert.ToDateTime("01/" + Month_Year.ToString());

                int year = Convert.ToInt32(d.Date.ToString("yyyy"));
                int month = Convert.ToInt32(d.Date.ToString("MM"));
                string dyasInMonth = DateTime.DaysInMonth(year, month).ToString();
                ds = objdb.ByProcedure("sp_BFPDashboard",
                     new string[] { "flag", "Office_Id", "Month", "Year", "Days" },
                     new string[] { "1", Office_ID.ToString(), month.ToString(), year.ToString(), dyasInMonth.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    #endregion====================end of Inward Report================

    #region======================MAF Dashboard========================

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetCropProduction_MAFDashboard(string Key, int Office_ID, int FY_Id)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = objdb.ByProcedure("sp_tblPUBabaiProduction",
                                 new string[] { "flag", "Office_ID", "Phase_id" },
                                   new string[] { "7", Office_ID.ToString(), FY_Id.ToString() }, "Dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetGardenSales_MAFDashboard(string Key, int Office_ID, int FY_Id)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = objdb.ByProcedure("sp_tblPUGardenSales",
                                    new string[] { "flag", "Office_ID", "Phase_id" },
                                   new string[] { "7", Office_ID.ToString(), FY_Id.ToString() }, "Dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }
    #endregion===================End of MAF Dashboard=================

    #region===============RTE Dashboard=========================
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetRTEDashboard(string Key, int Office_ID, string Month_Year)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DateTime d = Convert.ToDateTime("01/" + Month_Year);
                string transdt = Convert.ToDateTime(d, cult).ToString("yyyy/MM/dd");
                ds = objdb.ByProcedure("SpMnthlyProdTargetM"
                    , new string[] { "flag", "ProdUnit_Id", "TransactionDt" }
                    , new string[] { "4", Office_ID.ToString(), transdt }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[1];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetRTEDashboardProductWise(string Key, string Month_Year,int prod_Id)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DateTime d = Convert.ToDateTime("01/" + Month_Year);

                int year = Convert.ToInt32(d.Date.ToString("yyyy"));
                int mnth = Convert.ToInt32(d.Date.ToString("MM"));
                string dyasInMonth = DateTime.DaysInMonth(year, mnth).ToString();
                int daysinmonth = Convert.ToInt32(dyasInMonth);



                ds = objdb.ByProcedure("sp_tblPuDailyProduction",
                    new string[] { "flag", "Prod_id", "month", "Cmonth" }, 
                    new string[] { "7", prod_Id.ToString(), mnth.ToString(), daysinmonth.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }
    #endregion============End  of RTEDashboard==================
}
