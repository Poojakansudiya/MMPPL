using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.Utils.MVVM;
using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;

namespace MMPPL.Views.CustmerMasterView
{
    public partial class CustmerMasterView : XtraUserControl
    {
        public CustmerMasterView()
        {
            InitializeComponent();
            if (!mvvmContext.IsDesignMode)
                InitBindings();
        }
        void InitBindings()
        {
            var fluentAPI = mvvmContext.OfType<MMPPL.ViewModels.CustmerMasterViewModel>();
            fluentAPI.WithEvent(this, "Load").EventToCommand(x => x.OnLoaded());
            fluentAPI.SetObjectDataSourceBinding(
                custmerMasterViewBindingSource, x => x.Entity, x => x.Update());
            #region Dispatches Detail Collection
            // We want to synchronize the ViewModel.SelectedEntity and the GridView.FocusedRowRandle in two-way manner
            fluentAPI.WithEvent<GridView, FocusedRowObjectChangedEventArgs>(DispatchesGridView, "FocusedRowObjectChanged")
                .SetBinding(x => x.CustmerMasterDispatchesDetails.SelectedEntity,
                    args => args.Row as MMPPL.Dispatch,
                    (gView, entity) => gView.FocusedRowHandle = gView.FindRow(entity));
            // We want to proceed the Edit command when row double-clicked
            fluentAPI.WithEvent<RowClickEventArgs>(DispatchesGridView, "RowClick")
                        .EventToCommand(
                            x => x.CustmerMasterDispatchesDetails.Edit(null), x => x.CustmerMasterDispatchesDetails.SelectedEntity,
                            args => (args.Clicks == 2) && (args.Button == System.Windows.Forms.MouseButtons.Left));
            //We want to show PopupMenu when row clicked by right button
            DispatchesGridView.RowClick += (s, e) =>
            {
                if (e.Clicks == 1 && e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    DispatchesPopUpMenu.ShowPopup(DispatchesGridControl.PointToScreen(e.Location), s);
                }
            };
            // We want to show the CustmerMasterDispatchesDetails collection in grid and react on this collection external changes (Reload, server-side Filtering)
            fluentAPI.SetBinding(DispatchesGridControl, g => g.DataSource, x => x.CustmerMasterDispatchesDetails.Entities);

            fluentAPI.BindCommand(bbiDispatchesNew, x => x.CustmerMasterDispatchesDetails.New());
            fluentAPI.BindCommand(bbiDispatchesEdit, x => x.CustmerMasterDispatchesDetails.Edit(null), x => x.CustmerMasterDispatchesDetails.SelectedEntity);
            fluentAPI.BindCommand(bbiDispatchesDelete, x => x.CustmerMasterDispatchesDetails.Delete(null), x => x.CustmerMasterDispatchesDetails.SelectedEntity);
            fluentAPI.BindCommand(bbiDispatchesRefresh, x => x.CustmerMasterDispatchesDetails.Refresh());
            #endregion

            bbiCustomize.ItemClick += (s, e) => { dataLayoutControl1.ShowCustomizationForm(); };
        }
    }
}
