using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMPPL.Report
{
    class LabelPrintCall
    {
        public static void PrintReprt(String SRNO )
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.strCon);
            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            string sqlString = "Select * from DuplicatePrinting" +
          " where SRNO ='" + SRNO + "'";
            SqlCommand cmd = new SqlCommand(sqlString, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "DuplicatePrinting");
            LabelPrintReport rpt = new LabelPrintReport();
            rpt.DataSource = ds.Tables["DuplicatePrinting"];
            
            using (ReportPrintTool printTool = new ReportPrintTool(rpt))
            {
                printTool.Print(Properties.Settings.Default.PrinterName);

               // printTool.ShowPreview();
               // printTool.Print("SATO CG408 (Copy 1)");
                //or printTool.PrintDialog();
            }

            if (con.State == System.Data.ConnectionState.Open)
            {
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
        }
    }
}
