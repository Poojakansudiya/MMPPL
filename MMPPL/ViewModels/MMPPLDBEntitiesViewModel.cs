using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using MMPPL.Common.DataModel;
using MMPPL.Common.ViewModel;
using MMPPL.MMPPLDBEntitiesDataModel;
using MMPPL;

namespace MMPPL.ViewModels
{
    /// <summary>
    /// Represents the root POCO view model for the MMPPLDBEntities data model.
    /// </summary>
    public partial class MMPPLDBEntitiesViewModel : DocumentsViewModel<MMPPLDBEntitiesModuleDescription, IMMPPLDBEntitiesUnitOfWork>
    {

        const string TablesGroup = "Tables";

        const string ViewsGroup = "Views";
        INavigationService NavigationService { get { return this.GetService<INavigationService>(); } }

        /// <summary>
        /// Creates a new instance of MMPPLDBEntitiesViewModel as a POCO view model.
        /// </summary>
        public static MMPPLDBEntitiesViewModel Create()
        {
            return ViewModelSource.Create(() => new MMPPLDBEntitiesViewModel());
        }

        /// <summary>
        /// Initializes a new instance of the MMPPLDBEntitiesViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the MMPPLDBEntitiesViewModel type without the POCO proxy factory.
        /// </summary>
        protected MMPPLDBEntitiesViewModel()
            : base(UnitOfWorkSource.GetUnitOfWorkFactory())
        {
        }

        protected override MMPPLDBEntitiesModuleDescription[] CreateModules()
        {
            return new MMPPLDBEntitiesModuleDescription[] {
                new MMPPLDBEntitiesModuleDescription("Color Masters", "ColorMasterCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.ColorMasters)),
                new MMPPLDBEntitiesModuleDescription("Custmer Masters", "CustmerMasterCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.CustmerMasters)),
                new MMPPLDBEntitiesModuleDescription("Dispatches", "DispatchCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Dispatches)),
                new MMPPLDBEntitiesModuleDescription("Grade Masters", "GradeMasterCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.GradeMasters)),
                new MMPPLDBEntitiesModuleDescription("Label Printings", "LabelPrintingCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.LabelPrintings)),
                new MMPPLDBEntitiesModuleDescription("Micron Masters", "MicronMasterCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.MicronMasters)),
                new MMPPLDBEntitiesModuleDescription("Size Masters", "SizeMasterCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.SizeMasters)),
                new MMPPLDBEntitiesModuleDescription("Duplicate Printings", "DuplicatePrintingCollectionView", ViewsGroup),
                new MMPPLDBEntitiesModuleDescription("Stock Reports", "StockReportCollectionView", ViewsGroup),
            };
        }



        protected override void OnActiveModuleChanged(MMPPLDBEntitiesModuleDescription oldModule)
        {
            if (ActiveModule != null && NavigationService != null)
            {
                NavigationService.ClearNavigationHistory();
            }
            base.OnActiveModuleChanged(oldModule);
        }
    }

    public partial class MMPPLDBEntitiesModuleDescription : ModuleDescription<MMPPLDBEntitiesModuleDescription>
    {
        public MMPPLDBEntitiesModuleDescription(string title, string documentType, string group, Func<MMPPLDBEntitiesModuleDescription, object> peekCollectionViewModelFactory = null)
            : base(title, documentType, group, peekCollectionViewModelFactory)
        {
        }
    }
}