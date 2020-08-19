using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using MMPPL.Common.Utils;
using MMPPL.MMPPLDBEntitiesDataModel;
using MMPPL.Common.DataModel;
using MMPPL;
using MMPPL.Common.ViewModel;

namespace MMPPL.ViewModels
{
    /// <summary>
    /// Represents the single LabelPrinting object view model.
    /// </summary>
    public partial class LabelPrintingViewModel : SingleObjectViewModel<LabelPrinting, decimal, IMMPPLDBEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of LabelPrintingViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static LabelPrintingViewModel Create(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new LabelPrintingViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the LabelPrintingViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the LabelPrintingViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected LabelPrintingViewModel(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.LabelPrintings, x => x.GrossWeight)
        {
        }


        /// <summary>
		/// The view model that contains a look-up collection of ColorMasters for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<ColorMaster> LookUpColorMasters
        {
            get { return GetLookUpEntitiesViewModel((LabelPrintingViewModel x) => x.LookUpColorMasters, x => x.ColorMasters); }
        }


        /// <summary>
		/// The view model that contains a look-up collection of Dispatches for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Dispatch> LookUpDispatches
        {
            get { return GetLookUpEntitiesViewModel((LabelPrintingViewModel x) => x.LookUpDispatches, x => x.Dispatches); }
        }


        /// <summary>
		/// The view model that contains a look-up collection of GradeMasters for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<GradeMaster> LookUpGradeMasters
        {
            get { return GetLookUpEntitiesViewModel((LabelPrintingViewModel x) => x.LookUpGradeMasters, x => x.GradeMasters); }
        }


        /// <summary>
		/// The view model that contains a look-up collection of MicronMasters for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<MicronMaster> LookUpMicronMasters
        {
            get { return GetLookUpEntitiesViewModel((LabelPrintingViewModel x) => x.LookUpMicronMasters, x => x.MicronMasters); }
        }


        /// <summary>
		/// The view model that contains a look-up collection of SizeMasters for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<SizeMaster> LookUpSizeMasters
        {
            get { return GetLookUpEntitiesViewModel((LabelPrintingViewModel x) => x.LookUpSizeMasters, x => x.SizeMasters); }
        }
    }
}
