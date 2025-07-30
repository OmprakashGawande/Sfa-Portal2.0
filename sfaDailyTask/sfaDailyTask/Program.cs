using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace sfaDailyTask
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet dsMailCategory = new DataSet();
            try
            {

                dsMailCategory = CommonMethod.byProcedure("USP_Daliy_Task_MailCategory_getAll", new string[] { }, new string[] { });

                if (dsMailCategory != null && dsMailCategory.Tables[0].Rows.Count > 0)
                {
                    string Latedate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    int rowCount = dsMailCategory.Tables[0].Rows.Count;
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataRow row = dsMailCategory.Tables[0].Rows[i];
                        string DaliyTask_MailCategory_Id = Convert.ToString(row["DaliyTask_MailCategory_Id"]??"");
                        if (DaliyTask_MailCategory_Id == "")
                            continue;
                        DataSet ds = new DataSet();
                        ds = CommonMethod.byProcedure("USP_Daily_Task_NoFill_Emp_ByTaskDate", new string[] { "DaliyTask_MailCategory_Id" }, new string[] { DaliyTask_MailCategory_Id });
                        if (ds != null)
                        {
                            if (ds.Tables.Count>1 && ds.Tables[1].Rows.Count > 0)
                            {
                                //CommonMethod.sendmail(Convert.ToString(row["EMail_To"]), Convert.ToString(row["EMail_CC"]), Latedate + " " + Convert.ToString(row["Mail_Subject"]), CommonMethod.TaskData(ds, Latedate));
                                CommonMethod.WriteToFile("Email has been successfully sent " + Convert.ToString(row["Mail_Subject"]) + Convert.ToString(row["EMail_To"]) + " " + Convert.ToString(row["EMail_CC"]) + " " + Latedate + " :" + DateTime.Now);
                            }
                        }
                        if (ds != null) { ds.Dispose(); }
                    }

                }
            }
            catch (Exception ex)
            {
                CommonMethod.WriteToFile(ex + " Exception :" + DateTime.Now);
            }
            finally
            {
                if (dsMailCategory != null) { dsMailCategory.Dispose(); }
            }


        }



    }

}
//string empMail = ConfigurationSettings.AppSettings["IsDevelopment"].ToLower() == "false" ? empEmail.Substring(0, (empEmail.Length - 1)) : ConfigurationSettings.AppSettings["IsDevelopmentCcMailId"];
//string adminMail = ConfigurationSettings.AppSettings["IsDevelopment"].ToLower() == "false" ? ConfigurationSettings.AppSettings["adminMailId"] : ConfigurationSettings.AppSettings["IsDevelopmentToMailId"];
