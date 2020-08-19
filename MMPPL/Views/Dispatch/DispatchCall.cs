using DevExpress.XtraReports.UI;
using MMPPL.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMPPL.Views.Dispatch
{
    class DispatchCall
    {
        
       public static void UpdateLabelByDispatch(Decimal dptid,string SRNO,double Weight)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.strCon);
            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            string sqlString = "UPDATE LabelPrinting SET NetWeight= Cast(GrossWeight as float)-" + Weight.ToString() + ",DptId = " + dptid.ToString() +
          " where DptId IS NULL and SRNO in " + SRNO + "";
            SqlCommand cmd = new SqlCommand(sqlString, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Dispose();
        }
        public static void UpdateWeightByDispatch(Decimal dptid, Decimal SizeId, double Weight)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.strCon);
            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            string sqlString = "UPDATE LabelPrinting SET NetWeight= Cast(GrossWeight as float)-" + Weight.ToString() +
          " where DptId ="+ dptid +" and SizeId=" + SizeId + "";
            SqlCommand cmd = new SqlCommand(sqlString, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Dispose();
        }

        public static void ShowReport(Decimal dptid)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.strCon);
            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            DispatchReport rpt = new DispatchReport();
            DataSet ds = new DataSet();
           SqlCommand cmd = new SqlCommand("Select * from DispatchHeader where DptId="+ dptid.ToString(), con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds, "DispatchHeader");

             cmd = new SqlCommand("Select * from DispatchLine where DptId=" + dptid.ToString(), con);
             adp = new SqlDataAdapter(cmd);
            adp.Fill(ds, "DispatchLine");

            ds.Relations.Add("DispatchHeaderDispatchLine", ds.Tables["DispatchHeader"].Columns["DptId"], ds.Tables["DispatchLine"].Columns["DptId"]);
            rpt.DataSource = ds;
            using (ReportPrintTool printTool = new ReportPrintTool(rpt))
            {
                // printTool.Print("TSC TA210");
                printTool.ShowRibbonPreviewDialog();
                //or printTool.PrintDialog();
            }

            cmd.Dispose();
            con.Dispose();
        }
        public static void DeleteDispatchLine(string SRNO)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.strCon);
            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            string sqlString = "UPDATE LabelPrinting SET DptId = NULL " +
          " where  SRNO in " + SRNO + "";
            SqlCommand cmd = new SqlCommand(sqlString, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Dispose();
        }
    }
}
