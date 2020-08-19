using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraBars;


using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using System.Drawing;
using MMPPL.CustomCode;

namespace MMPPL.Views.StockReportCollectionView
{
    public partial class StockReportCollectionView : XtraUserControl
    {
        public StockReportCollectionView()
        {
            InitializeComponent();
            if (!mvvmContext.IsDesignMode)
                InitBindings();
        }
        void InitBindings()
        {
            mvvmContext.RegisterService(WindowedDocumentManagerService.Create(this));
            var fluentAPI = mvvmContext.OfType<MMPPL.ViewModels.StockReportCollectionViewModel>();
            fluentAPI.WithEvent(this, "Load").EventToCommand(x => x.OnLoaded());
            // We want to show the Entities collection in grid and react on this collection external changes (Reload, server-side Filtering)
            fluentAPI.SetBinding(gridControl, gControl => gControl.DataSource, x => x.Entities);
            // We want to show loading-indicator when data is loading asynchronously
            fluentAPI.SetBinding(gridView, gView => gView.LoadingPanelVisible, x => x.IsLoading);
            // We want to synchronize the ViewModel.SelectedEntity and the GridView.FocusedRowRandle in two-way manner
            fluentAPI.WithEvent<GridView, FocusedRowObjectChangedEventArgs>(gridView, "FocusedRowObjectChanged")
                .SetBinding(x => x.SelectedEntity,
                    args => args.Row as MMPPL.StockReport,
                    (gView, entity) => gView.FocusedRowHandle = gView.FindRow(entity));
            //We want to show ribbon print preview when bbiPrintPreview clicked
            bbiPrintPreview.ItemClick += (s, e) => { gridControl.ShowRibbonPrintPreview(); };
            //We want to show RECORDS count on BarStaticItem
            fluentAPI.SetBinding(bsiRecordsCount, item => item.Caption, x => x.Entities.Count,
                    count => string.Format("RECORDS : {0}", count));
            //We want to show PopupMenu when row clicked by right button
            gridView.RowClick += (s, e) =>
            {
                if (e.Clicks == 1 && e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    popupMenu.ShowPopup(gridControl.PointToScreen(e.Location), s);
                }
            };
        }

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            Point pt = view.GridControl.PointToClient( new System.Drawing.Point(MousePosition.X, MousePosition.Y));
            DoRowDoubleClick(view, pt);
        }
        private void DoRowDoubleClick(DevExpress.XtraGrid.Views.Grid.GridView view, Point pt)
        {
            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                string colCaption = info.Column == null ? "N/A" : info.Column.GetCaption();
                
               
                frmStockDetailReport frm = new frmStockDetailReport();
                StockReport row =(StockReport) gridView.GetRow(info.RowHandle);
                frmStockDetailReport.Filterbject = row;
                frm.ShowDialog();

            }
        }
    }
}
