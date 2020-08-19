using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.Utils.MVVM;
using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;

namespace MMPPL.Views.MicronMasterView
{
    public partial class MicronMasterView : XtraUserControl
    {
        public MicronMasterView()
        {
            InitializeComponent();
            if (!mvvmContext.IsDesignMode)
                InitBindings();
        }
        void InitBindings()
        {
            var fluentAPI = mvvmContext.OfType<MMPPL.ViewModels.MicronMasterViewModel>();
            fluentAPI.WithEvent(this, "Load").EventToCommand(x => x.OnLoaded());
            fluentAPI.SetObjectDataSourceBinding(
                micronMasterViewBindingSource, x => x.Entity, x => x.Update());
            #region LabelPrintings Detail Collection
            // We want to synchronize the ViewModel.SelectedEntity and the GridView.FocusedRowRandle in two-way manner
            fluentAPI.WithEvent<GridView, FocusedRowObjectChangedEventArgs>(LabelPrintingsGridView, "FocusedRowObjectChanged")
                .SetBinding(x => x.MicronMasterLabelPrintingsDetails.SelectedEntity,
                    args => args.Row as MMPPL.LabelPrinting,
                    (gView, entity) => gView.FocusedRowHandle = gView.FindRow(entity));
            // We want to proceed the Edit command when row double-clicked
            fluentAPI.WithEvent<RowClickEventArgs>(LabelPrintingsGridView, "RowClick")
                        .EventToCommand(
                            x => x.MicronMasterLabelPrintingsDetails.Edit(null), x => x.MicronMasterLabelPrintingsDetails.SelectedEntity,
                            args => (args.Clicks == 2) && (args.Button == System.Windows.Forms.MouseButtons.Left));
            //We want to show PopupMenu when row clicked by right button
            LabelPrintingsGridView.RowClick += (s, e) =>
            {
                if (e.Clicks == 1 && e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    LabelPrintingsPopUpMenu.ShowPopup(LabelPrintingsGridControl.PointToScreen(e.Location), s);
                }
            };
            // We want to show the MicronMasterLabelPrintingsDetails collection in grid and react on this collection external changes (Reload, server-side Filtering)
            fluentAPI.SetBinding(LabelPrintingsGridControl, g => g.DataSource, x => x.MicronMasterLabelPrintingsDetails.Entities);

            fluentAPI.BindCommand(bbiLabelPrintingsNew, x => x.MicronMasterLabelPrintingsDetails.New());
            fluentAPI.BindCommand(bbiLabelPrintingsEdit, x => x.MicronMasterLabelPrintingsDetails.Edit(null), x => x.MicronMasterLabelPrintingsDetails.SelectedEntity);
            fluentAPI.BindCommand(bbiLabelPrintingsDelete, x => x.MicronMasterLabelPrintingsDetails.Delete(null), x => x.MicronMasterLabelPrintingsDetails.SelectedEntity);
            fluentAPI.BindCommand(bbiLabelPrintingsRefresh, x => x.MicronMasterLabelPrintingsDetails.Refresh());
            #endregion

            bbiCustomize.ItemClick += (s, e) => { dataLayoutControl1.ShowCustomizationForm(); };
        }
    }
}
