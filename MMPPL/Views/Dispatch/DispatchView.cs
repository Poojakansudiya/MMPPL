using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.Utils.MVVM;
using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using OpenNETCF.Desktop.Communication;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Threading.Tasks;

namespace MMPPL.Views.DispatchView
{
    
    public partial class DispatchView : XtraUserControl
    {
        RAPI myRapi = null;
        int timeCount = 0;
        public DispatchView()
        {
            InitializeComponent();
            if (sizeMasterTableAdapter.Connection.State !=  ConnectionState.Open)
            {
                sizeMasterTableAdapter.Connection.Open();
            }
            sizeMasterTableAdapter.Fill(mMPPLDBDataSet.SizeMaster);
          
            if (!mvvmContext.IsDesignMode)
                InitBindings();
            
        }
        void InitBindings()
        {
            var fluentAPI = mvvmContext.OfType<MMPPL.ViewModels.DispatchViewModel>();
            fluentAPI.WithEvent(this, "Load").EventToCommand(x => x.OnLoaded());
            fluentAPI.SetObjectDataSourceBinding(
                dispatchViewBindingSource, x => x.Entity, x => x.Update());
            #region LabelPrintings Detail Collection
            // We want to synchronize the ViewModel.SelectedEntity and the GridView.FocusedRowRandle in two-way manner
            fluentAPI.WithEvent<GridView, FocusedRowObjectChangedEventArgs>(LabelPrintingsGridView, "FocusedRowObjectChanged")
                .SetBinding(x => x.DispatchLabelPrintingsDetails.SelectedEntity,
                    args => args.Row as MMPPL.LabelPrinting,
                    (gView, entity) => gView.FocusedRowHandle = gView.FindRow(entity));
            // We want to proceed the Edit command when row double-clicked
            fluentAPI.WithEvent<RowClickEventArgs>(LabelPrintingsGridView, "RowClick")
                        .EventToCommand(
                            x => x.DispatchLabelPrintingsDetails.Edit(null), x => x.DispatchLabelPrintingsDetails.SelectedEntity,
                            args => (args.Clicks == 2) && (args.Button == System.Windows.Forms.MouseButtons.Left));
            //We want to show PopupMenu when row clicked by right button
            LabelPrintingsGridView.RowClick += (s, e) =>
            {
                if (e.Clicks == 1 && e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    LabelPrintingsPopUpMenu.ShowPopup(LabelPrintingsGridControl.PointToScreen(e.Location), s);
                }
            };
            // We want to show the DispatchLabelPrintingsDetails collection in grid and react on this collection external changes (Reload, server-side Filtering)
            fluentAPI.SetBinding(LabelPrintingsGridControl, g => g.DataSource, x => x.DispatchLabelPrintingsDetails.Entities);

            fluentAPI.BindCommand(bbiLabelPrintingsNew, x => x.DispatchLabelPrintingsDetails.New());
            fluentAPI.BindCommand(bbiLabelPrintingsEdit, x => x.DispatchLabelPrintingsDetails.Edit(null), x => x.DispatchLabelPrintingsDetails.SelectedEntity);
            fluentAPI.BindCommand(bbiLabelPrintingsDelete, x => x.DispatchLabelPrintingsDetails.Delete(null), x => x.DispatchLabelPrintingsDetails.SelectedEntity);
            fluentAPI.BindCommand(bbiLabelPrintingsRefresh, x => x.DispatchLabelPrintingsDetails.Refresh());
            #endregion
            // Binding for CustmerMaster LookUp editor
            fluentAPI.SetBinding(CustmerMasterLookUpEdit.Properties, p => p.DataSource, x => x.LookUpCustmerMasters.Entities);

            bbiCustomize.ItemClick += (s, e) => { dataLayoutControl1.ShowCustomizationForm(); };
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try {
              
            myRapi = new RAPI();
                bool HasDevice = true;
            if (!myRapi.DevicePresent)
            {
                if(XtraMessageBox.Show("Please Connect Device. Or\n Load Previous File.(Y/N)","Conformation", MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.No)
                    { 
                return;
                    }
                    HasDevice = false;
            }
                if (HasDevice)
                {
                    myRapi.Connect();
                    if (File.Exists(Application.StartupPath + "\\dbFile\\LocalDb.xml"))
                    {
                        if (myRapi.DeviceFileExists("\\Application\\MMPPL\\LocalDb.xml"))
                        {
                            File.Delete(Application.StartupPath + "\\dbFile\\LocalDb.xml");
                            myRapi.CopyFileFromDevice(Application.StartupPath + "\\dbFile\\LocalDb.xml", "\\Application\\MMPPL\\LocalDb.xml");
                            myRapi.DeleteDeviceFile("\\Application\\MMPPL\\LocalDb.xml");
                        }
                        else
                        {
                            MessageBox.Show("No Scan Data Found");
                            return;
                        }
                       
                    }
                    else
                    {
                        if (myRapi.DeviceFileExists("\\Application\\MMPPL\\LocalDb.xml"))
                        {
                           myRapi.CopyFileFromDevice(Application.StartupPath + "\\dbFile\\LocalDb.xml", "\\Application\\MMPPL\\LocalDb.xml");
                            myRapi.DeleteDeviceFile("\\Application\\MMPPL\\LocalDb.xml");
                        }
                        else
                        {
                            MessageBox.Show("No Scan Data Found");
                            return;
                        }
                    }
                   
               }
                if (File.Exists(Application.StartupPath + "\\dbFile\\LocalDb.xml"))
                {
                    bbiSave.PerformClick();
                    DataSet ds = new DataSet();
                    ds.ReadXml(Application.StartupPath + "\\dbFile\\LocalDb.xml");
                    string SRNO = "(";

                    foreach (DataRow dtr in ds.Tables["DeliveryChallan"].DefaultView.ToTable().Rows)
                    {
                        char[] cr = { '~' };
                        String[] str = dtr["BARCODE"].ToString().Split(cr);

                        SRNO = SRNO + "'" + str[1].ToString() + "',";
                    }
                    SRNO = SRNO.TrimEnd(',');
                    SRNO = SRNO + ")";

                    Dispatch.DispatchCall.UpdateLabelByDispatch(MMPPL.Dispatch.CurrObject.DptId, SRNO, Convert.ToDouble(txtLessWeight.Value));
                    tmrLine.Start();
                }
                else
                {
                    MessageBox.Show("No Scan Data Found");
                   
                }
                if(HasDevice)
                { 
                     myRapi.Disconnect();
                     myRapi.Dispose();
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

}
 

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string SRNO = "(";


                DevExpress.XtraGrid.Columns.GridColumn col = LabelPrintingsGridView.Columns.ColumnByFieldName("SRNO");
                if (col == null) return;
                LabelPrintingsGridView.BeginSort();
                try
                {
                    // Obtain the number of data rows. 
                    int dataRowCount = LabelPrintingsGridView.DataRowCount;
                    // Traverse data rows and change the Price field values. 
                    for (int i = 0; i < dataRowCount; i++)
                    {
                        object cellValue = LabelPrintingsGridView.GetRowCellValue(i, col);
                        SRNO = SRNO + "'" + cellValue.ToString() + "',";

                    }
                }
                finally { LabelPrintingsGridView.EndSort(); }

                 SRNO = SRNO.TrimEnd(',');
                SRNO = SRNO + ")";
                if (SRNO.Length > 2)
                {
                    Dispatch.DispatchCall.DeleteDispatchLine(SRNO);
                }
                bbiDelete.PerformClick();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void tmrLine_Tick(object sender, EventArgs e)
        {
            bbiLabelPrintingsRefresh.PerformClick();
            timeCount += 1;
            if (timeCount > 2)
            {
                tmrLine.Stop();
                timeCount = 0;
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MMPPL.Dispatch.CurrObject != null)
            {
                Dispatch.DispatchCall.ShowReport(MMPPL.Dispatch.CurrObject.DptId);
            }
        }

        private void btnWeightUpdate_Click(object sender, EventArgs e)
        {
            Dispatch.DispatchCall.UpdateWeightByDispatch(MMPPL.Dispatch.CurrObject.DptId,(decimal) txtSize.EditValue, Convert.ToDouble(txtLessWeight.Value));
            tmrLine.Start();
        }
    }
}
