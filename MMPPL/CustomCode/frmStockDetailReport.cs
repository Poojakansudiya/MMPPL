using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace MMPPL.CustomCode
{
    public partial class frmStockDetailReport : DevExpress.XtraEditors.XtraForm
    {
        public static StockReport Filterbject = null;
        public frmStockDetailReport()
        {
            InitializeComponent();
        }

        private void frmStockDetailReport_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.strCon);
            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("Select SRNO, SizeName, MicronName, GradeName, ColorName, GrossWeight, PrintDate from [DuplicatePrinting] where SizeId=@SizeId and MicronId=@MicronId and GradeId=@GradeId and ColorId=@ColorId and DptId is null", con);
            if (Filterbject.SizeId == null){cmd.Parameters.AddWithValue("@SizeId", DBNull.Value);} else { cmd.Parameters.AddWithValue("@SizeId", Filterbject.SizeId); }
            if (Filterbject.MicronId == null) { cmd.Parameters.AddWithValue("@MicronId", DBNull.Value); } else { cmd.Parameters.AddWithValue("@MicronId", Filterbject.MicronId); }
            if (Filterbject.GradeId == null) { cmd.Parameters.AddWithValue("@GradeId", DBNull.Value); } else { cmd.Parameters.AddWithValue("@GradeId", Filterbject.GradeId); }
            if (Filterbject.ColorId == null) { cmd.Parameters.AddWithValue("@ColorId", DBNull.Value); } else { cmd.Parameters.AddWithValue("@ColorId", Filterbject.ColorId); }



            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds, "DuplicatePrinting");
            gridControl1.DataSource = ds.Tables[0];
        }

        private void gridViewStockDetail_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs mouseArgs = (e as MouseEventArgs);
            GridHitInfo hitInfo = (sender as GridView).CalcHitInfo(mouseArgs.Location);           
            if (mouseArgs.Button != MouseButtons.Left || hitInfo.HitTest == GridHitTest.ViewCaption == false)
                return;
            (sender as GridView).OptionsView.ShowViewCaption = false;
            (sender as GridView).ShowPrintPreview();
            (sender as GridView).OptionsView.ShowViewCaption = true;
        }
    }
}